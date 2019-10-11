using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{// ゲームステージ上の進行ステータス
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
		JUMPCLEARSCENE,			// 直接クリアシーン移動
		JUMPFAILURESCENE,		// 直接失敗シーン移動
	}

	// 現在のゲームステージ上の進行ステータス
	[SerializeField, NonEditable, Tooltip("現在のゲーム状況")]
	// 本来 private の状態で関数呼び出しで使用するので変更予定
	public StageState stageState = StageState.NONE;

	// ゲームが開始するまでの時間経過
	[SerializeField, NonEditable, Tooltip("ゲーム開始するまでの時間経過")]
	private float stageReadyDelay;      // 現在の経過時間
	[SerializeField, Tooltip("ゲームが開始するまでの時間")]
	private float stageReadyDelayMax = 5.25f;    // 最大の待ち時間

	// ゲームプレイ時間
	[SerializeField, Tooltip("ゲームプレイ時間")]
	public float stagePlayDelayMax;     // 最大の残り時間

	// フェード用スクリプト
	[SerializeField, Tooltip("フェード用スクリプト")]
	public FadeTime fadeTimeScr;

	// 無線スクリプト
	[SerializeField, Tooltip("無線スクリプト")]
	public WirelessManager wirelessManagerScr;

	// 時間表示スクリプト
	[SerializeField, Tooltip("時間表示スクリプト")]
	public TimeDisplay timeDisplayScr;

	// ゲームデータプレハブ
	private GameObject gameDataPrefab;

	// ゲームクリアシーン名
	[SerializeField, Tooltip("ゲームクリアシーン名")]
	public string gameFailureSceneName = "Title";

	// ゲーム失敗シーン名
	[SerializeField, Tooltip("ゲーム失敗シーン名")]
	public string gameClearSceneName = "Title";

	// ゲームクリア状態
	[SerializeField, NonEditable]
	private bool isGameClear;

	// 本来使用できませんので削除予定
	public static GameMaster instance;

	// 開幕前
	private void Awake()
	{
		// 本来使用できませんので削除予定
		instance = GetComponent<GameMaster>();

		if (!GameObject.Find("GameData"))
		{
			gameDataPrefab = Resources.Load("Prefabs/GameData") as GameObject;
			GameObject g = Instantiate(gameDataPrefab, Vector3.zero, transform.rotation);
			g.name = "GameData";
		}
	}

	// 開幕
	private void Start()
	{
		// 序盤のゲームステータスをNONEに
		if (fadeTimeScr)
		{
			stageState = StageState.FADEIN;
			fadeTimeScr.SetFadeType(FadeTime.FadeType.FADEIN);
		}
		else
		{
			stageState = StageState.READY;
		}

		timeDisplayScr.SetTime(stagePlayDelayMax);
        wirelessManagerScr.SetTime(stageReadyDelayMax, 4.5f, 3.0f);
		isGameClear = false;

		if (gameClearSceneName == "") gameClearSceneName = "Title";
		if (gameFailureSceneName == "") gameFailureSceneName = "Title";
	}
	
	// 毎フレーム
	private void Update()
	{
		// Debug
		// F5キーを押したらゲームクリアとする
		if (Input.GetKeyDown(KeyCode.F5))
		{
			// ステージステータスをゲームクリアに変更
			SetStageStateInTheMaster(StageState.STAGECLEAR);
		}
		// F6キーを押したらゲーム失敗とする
		else if (Input.GetKeyDown(KeyCode.F6))
		{
			// ステージステータスをゲームクリアに変更
			SetStageStateInTheMaster(StageState.STAGEFAILURE);
		}
		//// 1キーを押したらプレイ中の無線が出る
		//if (Input.GetKeyDown(KeyCode.Alpha1))
		//{
		//	// プレイ中の無線
		//	wirelessManagerScr.SetWirelessMode(WirelessManager.WirelessMode.MESSAGE_1);
		//}
		// 2キーを押したらプレイ中の無線2が出る
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			// プレイ中の無線
			wirelessManagerScr.SetWirelessMode(WirelessManager.WirelessMode.MESSAGE_2);
		}
		// 2キーを押したらプレイ中の無線2が出る
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			// TIMEの変更
			if(timeDisplayScr.GetTimeMode() == TimeDisplay.TimeMode.PLAY)
			{
				timeDisplayScr.SetTimeMode(TimeDisplay.TimeMode.STOP);
				wirelessManagerScr.SetWirelessMode(WirelessManager.WirelessMode.DEBUG_1);
			}
			else if (timeDisplayScr.GetTimeMode() == TimeDisplay.TimeMode.STOP)
			{
				timeDisplayScr.SetTimeMode(TimeDisplay.TimeMode.PLAY);
				wirelessManagerScr.SetWirelessMode(WirelessManager.WirelessMode.DEBUG_2);
			}
		}

		// 現在のステージステータスで処理を変える
		switch (stageState)
		{
			case StageState.FADEIN:
				if(fadeTimeScr)
				{
					if(fadeTimeScr.IsFadeInFinished())
					{
						SetStageStateInTheMaster(StageState.READY);
						break;
					}
					if(fadeTimeScr.GetFadeType() != FadeTime.FadeType.FADEIN)
					{
						fadeTimeScr.SetFadeType(FadeTime.FadeType.FADEIN);
					}
				}
				else
				{
					SetStageStateInTheMaster(StageState.READY);
				}
				break;

			// ステージ開始前時
			case StageState.READY:
				stageReadyDelay += Time.deltaTime;
				// 無線が無しになった時
				if (stageReadyDelay >= stageReadyDelayMax)
				{
					// ステージステータスをプレイに変更
					SetStageStateInTheMaster(StageState.PLAYING);
					// タイム開始
					timeDisplayScr.SetTimeMode(TimeDisplay.TimeMode.PLAY);
				}
				break;

			// ステージプレイ中
			case StageState.PLAYING:
				if(timeDisplayScr.GetTimeMode() == TimeDisplay.TimeMode.END)
				{
					SetStageStateInTheMaster(StageState.STAGEFAILURE);
				}
				break;

			// ステージクリアステータス時
			case StageState.STAGECLEAR:
				BGM_Manager.BGM_obj.Sound_Docking();
				// 無線が無しになった時
				if (wirelessManagerScr.GetWirelessMode() == WirelessManager.WirelessMode.NONE)
				{
					// ステージステータスをプレイに変更
					SetStageStateInTheMaster(StageState.FADEOUT);
				}
				break;

			case StageState.CLEARNEXT:
				// スペースキーかマウス左クリックかenter
				if(IsOkeyKeyDown())
				{
					if(fadeTimeScr)
					{
						SetStageStateInTheMaster(StageState.FADEOUT);
						fadeTimeScr.SetFadeType(FadeTime.FadeType.FADEOUT);
					}
					else
					{
						SetStageStateInTheMaster(StageState.JUMPCLEARSCENE);
					}
				}
				break;

			// ステージフェードアウト
			case StageState.FADEOUT:
				if (fadeTimeScr)
				{
					if (fadeTimeScr.IsFadeOutFinished())
					{
						if (isGameClear) SetStageStateInTheMaster(StageState.JUMPCLEARSCENE);
						else SetStageStateInTheMaster(StageState.JUMPFAILURESCENE);
						break;
					}
					if (fadeTimeScr.GetFadeType() != FadeTime.FadeType.FADEOUT)
					{
						fadeTimeScr.SetFadeType(FadeTime.FadeType.FADEOUT);
					}
				}
				else
				{
					if(isGameClear) SetStageStateInTheMaster(StageState.JUMPCLEARSCENE);
					else SetStageStateInTheMaster(StageState.JUMPFAILURESCENE);

				}
				break;

			// ステージクリアステータス時
			case StageState.STAGEFAILURE:
				// 無線が無しになった時
				if (wirelessManagerScr.GetWirelessMode() == WirelessManager.WirelessMode.NONE)
				{
					// ステージステータスをプレイに変更
					SetStageStateInTheMaster(StageState.FADEOUT);
				}
				break;

			case StageState.FAILURENEXT:
				if (IsOkeyKeyDown())
				{
					SetStageStateInTheMaster(StageState.JUMPFAILURESCENE);
				}
				break;

			// その他
			default:
				break;
		}
	}

	private bool IsOkeyKeyDown()
	{
		return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.KeypadEnter);
	}

	// ステージステータス変更専用関数
	// 受け取ったステータスに応じて処理を変えます
	public void SetStageState(StageState s, string key = "")
	{
		switch (s)
		{
			// 受け入れるもの
			case StageState.STAGEFAILURE:
			case StageState.STAGECLEAR:
			case StageState.JUMPTITLE:
			case StageState.JUMPRESULT:
			case StageState.JUMPCLEARSCENE:
			case StageState.JUMPFAILURESCENE:
				SetStageStateInTheMaster(s);
				break;

			// 受け入れないもの
			default:
			case StageState.FADEIN:
			case StageState.FADEOUT:
			case StageState.READY:
			case StageState.NONE:
			case StageState.PLAYING:
			case StageState.FAILURENEXT:
			case StageState.CLEARNEXT:
				break;
		}
	}

	private void SetStageStateInTheMaster(StageState s)
	{
		switch (s)
		{
			case StageState.READY:
				// 最初の無線
				wirelessManagerScr.SetWirelessMode(WirelessManager.WirelessMode.FIRST_1);
				break;

			case StageState.FADEOUT:
				wirelessManagerScr.SetWirelessMode(WirelessManager.WirelessMode.NONE);
				break;

			// ステージクリア用テキスト
			case StageState.STAGECLEAR:
				// 状況成功
				isGameClear = true;
				// クリアの無線
				wirelessManagerScr.SetWirelessMode(WirelessManager.WirelessMode.STAGECLEAR_1);
				// タイムストップ
				timeDisplayScr.SetTimeMode(TimeDisplay.TimeMode.STOP);
				break;

			// タイトル移動前用テキスト
			case StageState.CLEARNEXT:
				break;

			// ステージ失敗用テキスト
			case StageState.STAGEFAILURE:
				// 状況失敗
				isGameClear = false;
				// 失敗の無線
				wirelessManagerScr.SetWirelessMode(WirelessManager.WirelessMode.STAGEFAILURE_1);
				// タイムストップ
				timeDisplayScr.SetTimeMode(TimeDisplay.TimeMode.STOP);
				break;

			// 失敗後タイトル移動前用テキスト
			case StageState.FAILURENEXT:
				break;

			// 直接タイトルにジャンプ
			case StageState.JUMPTITLE:
				SceneManager.LoadScene("Title");
				break;

			// 直接ゲームクリアシーンにジャンプ
			case StageState.JUMPCLEARSCENE:
				SceneManager.LoadScene(gameClearSceneName);
				break;

			// 直接ゲーム失敗シーンにジャンプ
			case StageState.JUMPFAILURESCENE:
				SceneManager.LoadScene(gameFailureSceneName);
				break;

			// その他テキスト削除
			default:
				//wirelessManagerScr.SetWirelessMode(WirelessManager.WirelessMode.NONE);
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

	public void StageClear()
	{
		SetStageStateInTheMaster(StageState.STAGECLEAR);
	}
}
