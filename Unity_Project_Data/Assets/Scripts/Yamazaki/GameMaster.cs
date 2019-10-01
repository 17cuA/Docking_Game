using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
	// ゲームステージ上の進行ステータス
	public enum StageState
	{
		NONE,			// なし
		PLAYING,		// プレイ中
		STAGEFAILURE,	// ゲーム失敗
		STAGECLEAR,		// ゲーム成功
		JUMPTITLE,		// 直接タイトル移動
		JUMPRESULT,		// 直接リザルト移動
	}

	// 本来privaateだが現状publicで動かすことを認める
	// 現在のゲームステージ上の進行ステータス
	//[SerializeField, NonEditable]
	public StageState stageState = StageState.NONE;

	// クリア後からシーンが切り替わる時間経過
	[SerializeField, NonEditable]
	private float stageClearDelay;		// 現在の経過時間
	public float stageClearDelayMax;	// 最大の待ち時間

	// 開幕
	private void Start()
	{
		// 序盤のゲームステータスをNONEに
		stageState = StageState.NONE;
	}

	// 舞フレーム
	private void Update()
	{
		// Debug
		// 1キーを押したらゲームクリアとする
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			// ステージステータスをゲームクリアに変更
			SetStageState(StageState.STAGECLEAR);
		}

		// 現在のステージステータスで処理を変える
		switch(stageState)
		{
			// ステージクリアステータス時
			case StageState.STAGECLEAR:
				// 時間を経過
				stageClearDelay += Time.deltaTime;
				// 待ち時間を達した時
				if(stageClearDelay >= stageClearDelayMax)
				{

					// ステージステータスを直接タイトルジャンプに変更
					SetStageState(StageState.JUMPTITLE);
				}
				break;

			// その他
			default:
				break;
		}
	}

	// ステージステータス変更専用関数
	// 受け取ったステータスに応じて処理を変えます
	public void SetStageState(StageState s)
	{
		switch (s)
		{
			// ステージクリアに設定
			case StageState.STAGECLEAR:
				stageState = StageState.STAGECLEAR;
				break;

			// 直接タイトルにジャンプ
			case StageState.JUMPTITLE:
				SceneManager.LoadScene("Title");
				break;

			default:
				break;
		}
	}
}
