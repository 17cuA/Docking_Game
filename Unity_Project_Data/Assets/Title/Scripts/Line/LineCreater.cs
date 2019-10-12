//────────────────────────────────────────────
// ファイル名	：LineCreater.cs
// 概要			：線をつないで描写する機能
// 作成者		：杉山 雅哉
// 作成日		：2018.3.8
// 
//────────────────────────────────────────────
// 更新履歴：
// 2019/03/08 [杉山 雅哉] クラス作成。LineRendererを使い線をつないでいく
// 2019/03/08 [杉山 雅哉] 実行中ではなくエディター上で動くようにする
// 2019/03/08 [杉山 雅哉] マウスの左クリックでオブジェクトを置けるように修正　←撤廃。editモードでマウス入力が受け付けられない。
// 2019/03/11 [杉山 雅哉] マウスの左クリックでオブジェクトを置くための機能を発見。実装を行う。
// 2019/03/11 [杉山 雅哉] これ以降の実装のためのタスクが多いためマウスでの操作は一旦保留する。
// 2019/04/16 [杉山 雅哉] 4点以上置かれたときのプログラムを作成
// 2019/04/16 [杉山 雅哉] 二次ベジェ曲線から三次ベジェ曲線に修正
// 2019/04/16 [杉山 雅哉] 新たに点が置かれたときに、ハンドルを生成させる
// 2019/04/19 [杉山 雅哉] アンカークラスを組み込む。
// 2019/05/08 [杉山 雅哉] 親子関係の中で新たにアンカーが追加されたとき、自動的に追加する。
// 2019/05/09 [杉山 雅哉] 次のアンカーが生成されたとき、自動的にいい感じの角度に変える
// 2019/05/09 [杉山 雅哉] クリックしたときクリックされた場所にアンカーを配置する。
// 2019/05/09 [杉山 雅哉] 実行と同時に一定区間ごとにオブジェクトを置く
// 2019/05/09 [杉山 雅哉] アタッチされているはずのものが存在しない場合、警告文を出す
// 2019/05/13 [杉山 雅哉] エディタ上のみで更新処理を行い、それ以外では処理を行わないようにする
// 2019/05/13 [杉山 雅哉] 角度調整機能を一部調整
// 2019/05/13 [杉山 雅哉] 長さに応じて間隔を自動調整する(失敗した)
// 2019/05/22 [杉山 雅哉] 使い勝手の改善を行う（角度をいい感じに調整する）
// 2019/05/23 [杉山 雅哉] 特定のオブジェクトのみに配置できるように修正
// 2019/05/24 [杉山 雅哉] Ctrlキーを押しながらクリックをした場合、点を置かなくする(失敗)
// 2019/05/29 [杉山 雅哉] 新しくアンカーを生成したとき、そのアンカーのハンドルの長さをいい感じにする
// 2019/05/29 [杉山 雅哉] アンカーの大きさを一括調整する機能を追加
// 2019/06/18 [杉山 雅哉] ラインパッククラスを作成し処理負荷を軽減する（後回し）
// 2019/06/18 [杉山 雅哉] Updateの廃止
// 2019/06/18 [杉山 雅哉] 処理の重さを軽減するために、前フレームの情報から変更があるかを確認する
// 
// TODO：
//
//────────────────────────────────────────────
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
//SceneViewを取得するために宣言、エディタ外では使えないのでUNITY_EDITORで囲む
using UnityEditor;
#endif

[ExecuteInEditMode]
public class LineCreater : MonoBehaviour
{

	// アンカー情報受け取り用クラス
	public class AnkerData
	{
		public Vector3 anker;
		public Vector3 prevHandle;
		public Vector3 nextHandle;

		public void set(Anker a)
		{
			anker = a.AnkerPos;
			prevHandle = a.PrevHandlePos;
			nextHandle = a.NextHandlePos;
		}
	}
	//プロパティ───────────────────────────────────────
	[Header("線の編集許可フラグ")]
	[SerializeField] private bool createLine;
	[Header("アンカー配置可能フラグ")]
	[SerializeField] private bool clickPut;
	[Header("アンカープレハブ")]
	[SerializeField] private GameObject AnkerPrefab;
	[Header("調整間隔")]
	[SerializeField] private float interval;
	[Header("自分のLineRenderer情報")]
	[SerializeField] private LineRenderer lineRenderer;
	[Header("線のサイズ")]
	[SerializeField] private float lineScale;
	[Header("直線化ボタン")]
	[SerializeField] private bool linearization;

	[SerializeField] private Anker[] ankers;
	private Vector3[] prevAnkerPositions;
	private int prevChildCount;
	private EventType prevEventType;
	private const int debugDivision = 20;			// 分割数
	//───────────────────────────────────────────────
	public int AnkersLength { get { return ankers.Length; } }
	public Anker[] Ankers { get { return ankers; } }
	//初期化─────────────────────────────────────────
	private void Start()
	{
		//if (EditorApplication.isPlaying)
		//{
			//Debug.LogWarning("この状態だと書き出しできないので上のif文を消してください");
			gameObject.SetActive(false);
		//}
	}
	//────────────────────────────────────────────
#if UNITY_EDITOR
	//内部呼び出しメソッド──────────────────────────────────
	/// <summary>
	/// 直線を結ぶ座標を取得する
	/// </summary>
	/// <returns></returns>
	Vector3[] GetLinePositions()
	{
		UpdateAnkerCount();
		//!< Center>>Next>>Prev>>Center>>Next>>Prev>>Center
		Vector3[] positions = new Vector3[ankers.Length + 2 * (ankers.Length - 1)];

		//!< 座標の追加
		int i, n;
		for (i = 0, n = 0; n < ankers.Length - 1; i += 3, ++n)
		{
			positions[i] = ankers[n].AnkerPos;
			positions[i + 1] = ankers[n].NextHandlePos;
			positions[i + 2] = ankers[n + 1].PrevHandlePos;
		}

		//!< 最後の座標を追加
		positions[positions.Length - 1] = ankers[ankers.Length - 1].AnkerPos;

		return positions;
	}
	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// アンカーの数を更新する
	/// </summary>
	/// <returns></returns>
	void UpdateAnkerCount()
	{
		//!< 前回と変わりなければ終了
		if (transform.childCount == prevChildCount) return;

		//!< すべての子要素を取得
		Transform children = GetComponentInChildren<Transform>();

		//!< 一時的にリスト管理を行い、のちに配列化
		List<Anker> allAnkers = new List<Anker>(0);
		foreach (Transform ob in children)
		{
			allAnkers.Add(ob.gameObject.GetComponent<Anker>());
		}
		ankers = allAnkers.ToArray();

		//!< アンカーの角度をいい感じにする
		//!< 2019/05/13 実行終了後にこの処理が原因で不具合が出るため、一回目はこの処理を行わない。
		if (prevChildCount != 0 && prevChildCount < transform.childCount)
		{
			AdjustAnkersHandleRange();
			AdjustAnkersAngle();
		}
		prevChildCount = transform.childCount;
	}
	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// アンカーのハンドルの長さを調整する
	/// </summary>
	/// <returns></returns>
	void AdjustAnkersHandleRange()
	{
		if (ankers.Length > 1)
		{
			ankers[ankers.Length - 1].AdjustPrevHandleRange(ankers[ankers.Length - 2]);
			ankers[ankers.Length - 2].AdjustNextHandleRange(ankers[ankers.Length - 1]);
		}
	}

	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// アンカーの角度をいい感じにする
	/// </summary>
	/// <returns></returns>
	void AdjustAnkersAngle()
	{
		switch (ankers.Length)
		{
			//!< 直線を描画
			case 2:
				//!< 末端のアンカーの角度をいい感じにする。
				ankers[0].AdjustFirstAnkerAngle(ankers[1]);
				ankers[1].AdjustSecondAnkerAngle(ankers[0]);
				break;
			//!< 曲線を描画
			case 3:
				//!< 終わりから2番目のアンカー角度をいい感じにする
				ankers[ankers.Length - 2].AdjustCenterAnkerAngle(ankers[ankers.Length - 3], ankers[ankers.Length - 1]);

				//!< 終わりから2番目のアンカーの前後のアンカーの角度をいい感じにする
				ankers[ankers.Length - 1].AdjustPrevAnkerAngle(ankers[ankers.Length - 2]);
				ankers[ankers.Length - 3].AdjustNextAnkerAngle(ankers[ankers.Length - 2]);
				break;
			default:
				//!< 終わりから2番目のアンカー角度をいい感じにする
				ankers[ankers.Length - 2].AdjustCenterAnkerAngle(ankers[ankers.Length - 3], ankers[ankers.Length - 1]);
				//!< 終わりから2番目のアンカーの次のアンカーの角度をいい感じにする
				ankers[ankers.Length - 1].AdjustPrevAnkerAngle(ankers[ankers.Length - 2]);
				break;
		}
	}
	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// ベジェ曲線を更新する
	/// </summary>
	/// <returns></returns>
	Vector3[] UpdateCurveLine()
	{
		// 直線の座標を取得
		Vector3[] linePositions = GetLinePositions();
		// 曲線の座標数（点の数 + 点と点を分割する点の数）
		Vector3[] positions = new Vector3[0];

		// 点と点の間を曲線にしていく
		for (int i = 0; i < linePositions.Length; i += 3)
		{
			if (i > linePositions.Length - 4) { break; }
			int temp = positions.Length;
			//!< 配列の長さを変更するためのリストを作成
			List<Vector3> list = new List<Vector3>(positions);

			//!< 2線の二次元ベジェ曲線を取得
			Vector3[] beje1 = BezierCurve2(linePositions[i], linePositions[i + 1], linePositions[i + 2], debugDivision);
			Vector3[] beje2 = BezierCurve2(linePositions[i + 1], linePositions[i + 2], linePositions[i + 3], debugDivision);

			//!< 次の座標の情報を挿入
			list.AddRange(BezierCurve3(beje1, beje2, debugDivision));
			lineRenderer.positionCount = list.Count;
			positions = list.ToArray();
		}
		lineRenderer.positionCount = positions.Length;
		for (int i = 0; i < lineRenderer.positionCount; ++i)
		{
			lineRenderer.SetPosition(i, positions[i]);
		}

		return positions;
	}
	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// 3点の座標から二次元ベジェ曲線座標を返す
	/// </summary>
	/// <param name="p1">開始点</param>
	/// <param name="p2">中間点</param>
	/// <param name="p3">終着点</param>
	/// <param name="division">分割数</param>
	/// <returns>二次元ベジェ曲線の座標配列</returns>
	/// <returns></returns>
	Vector3[] BezierCurve2(Vector3 p1, Vector3 p2, Vector3 p3, int division)
	{
		Vector3[] positions = new Vector3[division + 1];
		positions[0] = p1;
		for (int d = 1; d < division; ++d)
		{
			float t = 1.0f / division * d;

			Vector3 v1 = (1 - t) * p1 + t * p2;
			Vector3 v2 = (1 - t) * p2 + t * p3;

			positions[d] =
				t * v2 + (1 - (1.0f / division * d)) * v1;
		}
		positions[positions.Length - 1] = p3;
		return positions;
	}
	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// 2線の二次元ベジェ曲線を合成し、三次元ベジェ曲線を作成する
	/// </summary>
	/// <param name="startBeje">開始点を持つベジェ曲線</param>
	/// <param name="endBeje">終着点を持つベジェ曲線</param>
	/// <param name="division">分割数</param>
	/// <returns>三次元ベジェ曲線の座標配列</returns>
	/// <returns></returns>
	Vector3[] BezierCurve3(Vector3[] startBeje, Vector3[] endBeje, int division)
	{
		Vector3[] positions = new Vector3[division];
		positions[0] = startBeje[0];
		for (int d = 0; d < division; ++d)
		{
			float t = 1.0f / division * d;
			//3次元ベジェ曲線の計算↓（t = 時間,division = 分割数,)
			positions[d] =
				t * endBeje[d] + (1 - (1.0f / division * d)) * startBeje[d];
		}
		return positions;
	}
	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// ベジェ曲線を指定された分割数で調整する
	/// </summary>
	/// <param name="divisions">分割数配列</param>
	/// <returns></returns>
	Vector3[] AjustCurveLine(int[] divisions)
	{
		// 直線の座標を取得
		Vector3[] linePositions = GetLinePositions();
		// 曲線の座標数（点の数 + 点と点を分割する点の数）
		Vector3[] positions = new Vector3[0];

		int d = 0;  //分割数の要素数を表す変数
					// 点と点の間を曲線にしていく(三次元ベジェ曲線で作成を行うため点４つでひとつの線)
		for (int i = 0; i < linePositions.Length - 3; i += 3)
		{
			int temp = positions.Length;
			//!< 配列の長さを変更するためのリストを作成
			List<Vector3> list = new List<Vector3>(positions);

			//!< 2線の二次元ベジェ曲線を取得
			Vector3[] beje1 = BezierCurve2(linePositions[i], linePositions[i + 1], linePositions[i + 2], divisions[d]);
			Vector3[] beje2 = BezierCurve2(linePositions[i + 1], linePositions[i + 2], linePositions[i + 3], divisions[d]);

			//!< 次の座標の情報を挿入
			list.AddRange(BezierCurve3(beje1, beje2, divisions[d]));
			//カウントを加算
			lineRenderer.positionCount = list.Count;
			//配列に変換
			positions = list.ToArray();

			++d;
		}
		return positions;
	}
	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// 必要なものがアタッチされているか確認し、存在しないのであれば警告を出す
	/// </summary>
	/// <returns></returns>
	bool CheckError()
	{
		if (interval <= 0) { Debug.LogError("分割数間隔が設定されていませんせん"); return false; }
		if (!lineRenderer) { Debug.LogError("ラインレンダラーが存在しません"); return false; }
		else { return true; }
	}
	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// 点間隔修正処理
	/// </summary>
	/// <returns></returns>
	void AdjustInterval()
	{
		//!< 分割数配列
		int[] adjustDivisions = new int[ankers.Length - 1];
		//!< 現在の距離の合計
		float distance = 0;
		//!< アンカーの数だけループ
		for (int a = 0; a < ankers.Length - 1; ++a)
		{
			//!< 分割数の計測
			for (int i = 0; i < debugDivision - 1; ++i)
			{
				//!< 点間距離を加算
				distance += Vector3.Distance(lineRenderer.GetPosition(i + a * debugDivision),
					lineRenderer.GetPosition(i + 1 + a * debugDivision));
			}
			//!< 分割数を計算
			adjustDivisions[a] = (int)(distance / interval);
			distance %= interval;
		}
		lineRenderer.SetPositions(AjustCurveLine(adjustDivisions));
	}

	/// <summary>
	/// 前フレームから変化があったか確認する
	/// </summary>
	/// <returns></returns>
	bool IsTransitionLine()
	{
		if (transform.childCount == 0)
		{
			lineRenderer.positionCount = 0;
			return false;
		}
		//!< 現在の情報を取得
		Vector3[] nowPos = GetLinePositions();
		//!< 前フレーム情報が存在しないのであれば
		if(prevAnkerPositions == null)
		{
			//!< 現在の情報を入れ処理
			prevAnkerPositions = nowPos;
			return true;
		}
		if(prevAnkerPositions.Length != nowPos.Length)
		{
			prevAnkerPositions = nowPos;
			return true;
		}
		for (int i = 0;i<prevAnkerPositions.Length;++i)
		{
			if(prevAnkerPositions[i] != nowPos[i])
			{
				prevAnkerPositions = nowPos;
				return true;
			}
		}
		return false;
	}
	//マウスクリック判定処理─────────────────────────────────
	private void Awake()
	{
		if(EditorApplication.isPlaying)
		{
			gameObject.SetActive(false);
		}
	}
	private void OnDrawGizmos()
	{
		if(createLine)
		{
			if (clickPut)
			{
				PutAnter();
			}
		}
	}
	private void OnGUI()
	{
		if (IsTransitionLine())
		{
			CreateLine();
		}
		if (linearization == true)
		{
			ankers[1].AdjustPrevHandleRange(ankers[0]);
			ankers[0].AdjustNextHandleRange(ankers[1]);
			ankers[0].AdjustFirstAnkerAngle(ankers[1]);
			ankers[1].AdjustSecondAnkerAngle(ankers[0]);
			linearization = false;
		}
	}

	void CreateLine()
	{
		//!< エラー確認
		if (!CheckError()) return;
		//!< 子どもが少なければ動作なし
		if (transform.childCount == 0)
		{
			lineRenderer.positionCount = 0;
			return;
		}
		//!< ラインレンダラー情報を更新
		lineRenderer.SetPositions(UpdateCurveLine());
		//!< ラインレンダラーの横幅を修正
		lineRenderer.SetWidth(lineScale, lineScale);
		//!< 点間隔を調整
		//AdjustInterval();
	}

	void PutAnter()
	{
		if (prevEventType == EventType.MouseDown)
		{
			prevEventType = EventType.Repaint;
			return;
		}
		if (Event.current == null || Event.current.type != EventType.MouseUp)
		{
			prevEventType = EventType.MouseUp;
			return;
		}
		//マウスの位置情報の取得
		Vector3 mousePos = Event.current.mousePosition;
		//Y軸方向の補間
		mousePos.y = SceneView.currentDrawingSceneView.camera.pixelHeight - mousePos.y;
		//Ray..伸びる線のこと
		//シーンビューでマウスをクリックすると伸びる線を作成（画面には見えない）
		Ray ray = SceneView.currentDrawingSceneView.camera.ScreenPointToRay(mousePos);
		//当たり判定用の変数作成
		RaycastHit hit = new RaycastHit();
		//当たり判定の処理
		//シーンビューから見てオブジェクトに当たったら処理を開始する
		if (Physics.Raycast(ray, out hit))
		{
			//オブジェクトを作成
			GameObject obj = Instantiate(AnkerPrefab, hit.point, Quaternion.identity);
			//自分の子供にする
			obj.transform.parent = transform;

			Anker temp = obj.GetComponent<Anker>();
			temp.Init(lineScale, hit.normal);

			//でばっぐよー
			//Debug.DrawRay(hit.point,hit.normal * 10000.0f, Color.blue, 100.0f, false);

			//オブジェクトの法線の向きをそろえる
			obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
		}
		//現在のイベントのタイプの更新
		prevEventType = EventType.MouseDown;

		//!< 線情報を更新
		CreateLine();
	}
#endif
	//────────────────────────────────────────────
}