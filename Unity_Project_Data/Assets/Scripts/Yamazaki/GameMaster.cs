using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
	// ステージ上テキスト
	public Text stageText;

	// ゲームステージ上の進行ステータス
	public enum StageState
	{
		FADEIN,			// ステージフェードイン
		FADEOUT,		// ステージフェードアウト
		READY,			// スタート前
		NONE,			// なし
		PLAYING,		// プレイ中
		STAGEFAILURE,	// ゲーム失敗
		FAILURENEXT,	// 次のキーでタイトル移動
		STAGECLEAR,		// ゲーム成功
		CLEARNEXT,		// 次のキーでタイトル移動
		JUMPTITLE,		// 直接タイトル移動
		JUMPRESULT,		// 直接リザルト移動
	}

	// 本来privaateだが現状publicで動かすことを認める
	// 現在のゲームステージ上の進行ステータス
	//[SerializeField, NonEditable]
	public StageState stageState = StageState.NONE;

	// シーンが切り替わる時間経過
	[SerializeField, NonEditable]
	private float nextStageDelay;		// 現在の経過時間
	public float nextStageDelayMax;    // 最大の待ち時間

	// ゲームが開始するまでの時間経過
	[SerializeField, NonEditable]
	private float stageReadyDelay;		// 現在の経過時間
	public float stageReadyDelayMax;    // 最大の待ち時間

	// ゲームプレイ時間
	[SerializeField, NonEditable]
	private float stagePlayDelay;		// 現在の残り時間
	public float stagePlayDelayMax;		// 最大の残り時間
	public Text stageTimeText;          // 時間テキスト

	// フェード用スクリプト
	public FadeTime fadeTimeScript;

	// 開幕
	private void Start()
	{
		// 序盤のゲームステータスをNONEに
		if (fadeTimeScript)
		{
			stageState = StageState.FADEIN;
			fadeTimeScript.SetFadeType(FadeTime.FadeType.FADEIN);
		}
		else
		{
			stageState = StageState.READY;
		}
		stageText.text = "";
		stagePlayDelay = stagePlayDelayMax;
		stageTimeText.text = "Time:" + ((int)stagePlayDelay / 60).ToString("0") + "'" + (stagePlayDelay % 60.0f).ToString("00.000");
	}
	
	// 舞フレーム
	private void Update()
	{
		// Debug
		// 1キーを押したらゲームクリアとする
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			// ステージステータスをゲームクリアに変更
			SetStageState(StageState.STAGECLEAR);
		}

		// 現在のステージステータスで処理を変える
		switch(stageState)
		{
			case StageState.FADEIN:
				if(fadeTimeScript)
				{
					if(fadeTimeScript.IsFadeInFinished())
					{
						SetStageState(StageState.READY);
						break;
					}
					if(fadeTimeScript.GetFadeType() != FadeTime.FadeType.FADEIN)
					{
						fadeTimeScript.SetFadeType(FadeTime.FadeType.FADEIN);
					}
				}
				else
				{
					SetStageState(StageState.READY);
				}
				break;

			// ステージ開始前時
			case StageState.READY:
				// 時間を経過
				stageReadyDelay += Time.deltaTime;
				if(stageReadyDelay >= stageReadyDelayMax)
				{
					stageReadyDelay = 0.0f;
					// ステージステータスをプレイに変更
					SetStageState(StageState.PLAYING);
				}
				else if (stageReadyDelay >= stageReadyDelayMax * 1.0f / 2.0f)
				{
					stageText.text = "GO!";
				}
				else if (stageReadyDelay >= 0)
				{
					stageText.text = "READY...?";
				}
				else
				{
					stageText.text = "";
				}
				break;

			// ステージクリアステータス時
			case StageState.STAGECLEAR:
				// 時間を経過
				nextStageDelay += Time.deltaTime;
				// 待ち時間を達した時
				if(nextStageDelay >= nextStageDelayMax)
				{
					nextStageDelay = 0.0f;
					// ステージステータスを直接タイトルジャンプに変更
					SetStageState(StageState.CLEARNEXT);
				}
				break;

			case StageState.CLEARNEXT:
				// スペースキーかマウス左クリックかenter
				if(IsOkeyKeyDown())
				{
					if(fadeTimeScript)
					{
						SetStageState(StageState.FADEOUT);
						fadeTimeScript.SetFadeType(FadeTime.FadeType.FADEOUT);
					}
					else
					{
						SetStageState(StageState.JUMPTITLE);
					}
				}
				break;

			// ステージフェードアウト
			case StageState.FADEOUT:
				if (fadeTimeScript)
				{
					if (fadeTimeScript.IsFadeOutFinished())
					{
						SetStageState(StageState.JUMPTITLE);
						break;
					}
					if (fadeTimeScript.GetFadeType() != FadeTime.FadeType.FADEOUT)
					{
						fadeTimeScript.SetFadeType(FadeTime.FadeType.FADEOUT);
					}
				}
				else
				{
					SetStageState(StageState.JUMPTITLE);
				}
				break;

			// ステージクリアステータス時
			case StageState.STAGEFAILURE:
				// 時間を経過
				nextStageDelay += Time.deltaTime;
				// 待ち時間を達した時
				if (nextStageDelay >= nextStageDelayMax)
				{
					nextStageDelay = 0.0f;
					// ステージステータスを直接タイトルジャンプに変更
					SetStageState(StageState.FAILURENEXT);
				}
				break;

			case StageState.FAILURENEXT:
				if (IsOkeyKeyDown())
				{
					SetStageState(StageState.JUMPTITLE);
				}
				break;

			// その他
			default:
				stagePlayDelay -= Time.deltaTime;
				if(stagePlayDelay <= 0.0f)
				{
					stagePlayDelay = 0.0f;
					SetStageState(StageState.STAGEFAILURE);
				}
				stageTimeText.text = "Time:" + ((int)stagePlayDelay / 60).ToString("0") + "'" + (stagePlayDelay % 60.0f).ToString("00.000");
				break;
		}
	}

	private bool IsOkeyKeyDown()
	{
		return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.KeypadEnter);
	}

	// ステージステータス変更専用関数
	// 受け取ったステータスに応じて処理を変えます
	public void SetStageState(StageState s)
	{
		switch (s)
		{
			// ステージクリア用テキスト
			case StageState.STAGECLEAR:
				stageText.text = "ドッキング成功！\n";
				break;

			// タイトル移動前用テキスト
			case StageState.CLEARNEXT:
				stageText.text = "ドッキング成功！\nタイトルに戻る";
				break;

			// ステージ失敗用テキスト
			case StageState.STAGEFAILURE:
				stageText.text = "ドッキング失敗\n";
				break;

			// 失敗後タイトル移動前用テキスト
			case StageState.FAILURENEXT:
				stageText.text = "ドッキング失敗\nタイトルに戻る";
				break;

			// 直接タイトルにジャンプ
			case StageState.JUMPTITLE:
				SceneManager.LoadScene("Title");
				break;

			// その他テキスト削除
			default:
				stageText.text = "";
				break;
		}

		// ステージステータス更新
		stageState = s;
	}

	// 他のスクリプトからステージステータスを取得したい場合はここの窓口から
	public StageState GetStageState()
	{
		return stageState;
	}
}
