//──────────────────────────────────────────────
// ファイル名	：AcrobaticCamera.cs
// 概要			：引かれた線上に沿ってカメラを移動させる
// 作成者		：杉山 雅哉
// 作成日		：2019.05.13
// 
//──────────────────────────────────────────────
// 更新履歴：
// 2019/06/17 [杉山 雅哉] 線上にカメラを移動させる（角度は関係なし）
// 2019/06/17 [杉山 雅哉] カメラでターゲットをとらえる
// 2019/06/17 [杉山 雅哉] 移動にかかる秒数の値を設け、どれぐらいの時間をかけて移動するかを調整する。
// 2019/06/17 [杉山 雅哉] CameraSpeedEditorから緩急情報を引き抜き、適応させる
// 2019/06/20 [杉山 雅哉] 書き直し！！ベジェ曲線の計算式から値を取得する
// 2019/06/27 [杉山 雅哉] カメラの切り替えに応じて、フェードイン、フェードアウトを挟む
// 2019/06/27 [杉山 雅哉] アニメーションカーブを一つに絞る
// 2019/06/27 [杉山 雅哉] 1つの線を通った後は、次の線へ移動する
// 2019/06/27 [杉山 雅哉] ラインごとに見る点を変えられる機能をつける
// 2019/06/27 [杉山 雅哉] カメラに意図的に揺れをつける
// 2019/06/27 [杉山 雅哉] 最初のみフェードをつける機能をつける
// 2019/06/27 [杉山 雅哉] フェードイン/アウトにイージングをつける
// 2019/08/24 [杉山 雅哉] 手ブレ補正をつける
// 2019/08/31 [杉山 雅哉] 移動をフラッシュに変更
//──────────────────────────────────────────────

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Cameras;

public class AcrobaticCamera : MonoBehaviour
{
	//プロパティ───────────────────────────────────────
	[Header("LineRenderer情報")]
	[SerializeField] private LineCreater[] lineCreaters;
	[Header("向く方向")]
	[SerializeField] private GameObject[] targets;
	[Header("フェードは最初のみ行うかのフラグ")]
	[SerializeField] private bool startOnryFade;
	[SerializeField] private FadeEditor fadeEditor;
	[SerializeField] private CameraShaker cameraShaker;
	[Header("線番号")]
	[SerializeField] private int lineNum;
	float elapsedTime;						// 経過時間
	Camera mainCamera;						// カメラ情報
	Vector3 nowPosition;					// 現在の座標
	EasingEditor easingEditor;				// カメラスピードエディタ
	LineCreater.AnkerData[][] ankers;		// アンカー情報配列
	//読み取り用変数─────────────────────────────────────
	public int LineCount { get { return lineCreaters.Length; } }
	public Vector3 Target { get { return targets[lineNum].transform.position; } }
	//初期化─────────────────────────────────────────
	private void Awake()
	{
		mainCamera = GetComponent<Camera>();
		easingEditor = GetComponent<EasingEditor>();
		SetAnkers(lineCreaters);
		transform.position = ankers[lineNum][0].anker;
		transform.LookAt(targets[lineNum].transform);
	}
	//更新処理────────────────────────────────────────
	public void CameraUpdate(bool unFade)
	{
		MoveOnRail();
		MeasTime(unFade);
	}
	//内部処理────────────────────────────────────────
	/// <summary>
	/// 2線の二次元ベジェ曲線から出た値と割合を使い、その割合の時の値を返す
	/// </summary>
	/// <param name="startBeje">開始点を持つベジェ曲線</param>
	/// <param name="endBeje">終着点を持つベジェ曲線</param>
	/// <param name="percentage">割合</param>
	/// <returns>三次元ベジェ曲線の座標配列</returns>
	/// <returns></returns>
	Vector3 BezierCurve3(Vector3 startBeje, Vector3 endBeje, float percentage)
	{
		return startBeje * (1 - percentage) + endBeje * percentage;
	}
	//ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
	/// <summary>
	/// 3点の座標と割合から、その割合の時の座標を返す
	/// </summary>
	/// <param name="p1">開始点</param>
	/// <param name="p2">中間点</param>
	/// <param name="p3">終着点</param>
	/// <param name="percentage">割合</param>
	/// <returns>二次元ベジェ曲線の座標配列</returns>
	/// <returns></returns>
	Vector3 BezierCurve2(Vector3 p1, Vector3 p2, Vector3 p3, float percentage)
	{
			Vector3 v1 = (1 - percentage) * p1 + percentage * p2;
			Vector3 v2 = (1 - percentage) * p2 + percentage * p3;

		return v1 * (1 - percentage) + v2 * percentage;
	}
	/// <summary>
	/// ラインクリエイター配列からデータを引っこ抜く
	/// </summary>
	/// <param name="lines">ラインクリエイター配列</param>
	/// <returns>二次元ベジェ曲線の座標配列</returns>
	/// <returns></returns>
	void SetAnkers(LineCreater[] lines)
	{
		ankers = new LineCreater.AnkerData[lines.Length][];
		for(int i = 0;i < lineCreaters.Length; ++i)
		{
			//!< 06/27 2つに絞るためにマジックナンバーを使用
			ankers[i] = new LineCreater.AnkerData[2];
			ankers[i][0] = new LineCreater.AnkerData();
			ankers[i][1] = new LineCreater.AnkerData();
			ankers[i][0].set(lines[i].Ankers[0]);
			ankers[i][1].set(lines[i].Ankers[1]);
		}
	}
	//座標更新処理────────────────────────────────────────
	void MoveOnRail()
	{
		//!< 2次元ベジェ2線を更新
		//!< 06/27 2つに絞るためにマジックナンバーを使用
		Vector3 beje1 = BezierCurve2(ankers[lineNum][0].anker, ankers[lineNum][0].nextHandle, ankers[lineNum][1].prevHandle, easingEditor.Anims[lineNum].Evaluate(elapsedTime));
		Vector3 beje2 = BezierCurve2(ankers[lineNum][0].nextHandle, ankers[lineNum][1].prevHandle, ankers[lineNum][1].anker, easingEditor.Anims[lineNum].Evaluate(elapsedTime));
		//!< 3次元ベジェ取得
		transform.position = BezierCurve3(beje1, beje2, easingEditor.Anims[lineNum].Evaluate(elapsedTime));
		//transform.LookAt(targets[lineNum].transform.position);
	}
	//時間遷移処理────────────────────────────────────────
	void MeasTime(bool unFade)
	{
		//!< 時間を計測
		elapsedTime += Time.deltaTime;
		if(unFade == false)
			Fade();
		//!< 指定した時間を経過した
		if (elapsedTime > easingEditor.Anims[lineNum].keys[easingEditor.Anims[lineNum].keys.Length - 1].time)
		{
			//経過時間から観測時間を引く
			elapsedTime -= easingEditor.Anims[lineNum].keys[easingEditor.Anims[lineNum].keys.Length - 1].time;
			++lineNum;
			if (lineNum == lineCreaters.Length)
				lineNum = 0;
			transform.position = ankers[lineNum][0].anker;
			transform.LookAt(targets[lineNum].transform);
		}
	}
	//時間遷移処理────────────────────────────────────────
	void Fade()
	{
		if(fadeEditor.IsFading == true) { return; }
		if (elapsedTime < fadeEditor.FadeinTimeMax)
		{
			StartCoroutine(fadeEditor.FadeinCol());
		}
		else if (elapsedTime > easingEditor.Anims[lineNum].keys[easingEditor.Anims[lineNum].keys.Length - 1].time - fadeEditor.FadeoutTimeMax)
		{
			StartCoroutine(fadeEditor.FadeoutCol());
		}
	}
}