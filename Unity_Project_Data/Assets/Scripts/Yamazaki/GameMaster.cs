﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
	}

	// 本来privaateだが現状publicで動かすことを認める
	// 現在のゲームステージ上の進行ステータス
	//[SerializeField, NonEditable]
	public StageState stageState = StageState.NONE;

	// ゲームが開始するまでの時間経過
	[SerializeField, NonEditable]
	private float stageReadyDelay;		// 現在の経過時間
	public float stageReadyDelayMax;    // 最大の待ち時間

	// ゲームプレイ時間
	public float stagePlayDelayMax;		// 最大の残り時間
	

	// フェード用スクリプト
	public FadeTime fadeTimeScr;

	// 無線スクリプト
	public WirelessManager wirelessManagerScr;

	// 時間表示スクリプト
	public TimeDisplay timeDisplayScr;

	// 自身
	public static GameMaster instance;

	// ゲームデータプレハブ
	public GameObject gameDataPrefab;

	// 開幕前
	private void Awake()
	{
		instance = gameObject.GetComponent<GameMaster>();

		if (!GameObject.Find("GameData"))
		{
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
				if(fadeTimeScr)
				{
					if(fadeTimeScr.IsFadeInFinished())
					{
						SetStageState(StageState.READY);
						break;
					}
					if(fadeTimeScr.GetFadeType() != FadeTime.FadeType.FADEIN)
					{
						fadeTimeScr.SetFadeType(FadeTime.FadeType.FADEIN);
					}
				}
				else
				{
					SetStageState(StageState.READY);
				}
				break;

			// ステージ開始前時
			case StageState.READY:
				// 無線が無しになった時
				if(wirelessManagerScr.GetWirelessMode() == WirelessManager.WirelessMode.NONE)
				{
					// ステージステータスをプレイに変更
					SetStageState(StageState.PLAYING);
					// タイム開始
					timeDisplayScr.SetTimeMode(TimeDisplay.TimeMode.PLAY);
				}
				break;

			// ステージプレイ中
			case StageState.PLAYING:
				if(timeDisplayScr.GetTimeMode() == TimeDisplay.TimeMode.END)
				{
					SetStageState(StageState.STAGEFAILURE);
				}
				break;

			// ステージクリアステータス時
			case StageState.STAGECLEAR:
				// 無線が無しになった時
				if (wirelessManagerScr.GetWirelessMode() == WirelessManager.WirelessMode.NONE)
				{
					// ステージステータスをプレイに変更
					SetStageState(StageState.FADEOUT);
				}
				break;

			case StageState.CLEARNEXT:
				// スペースキーかマウス左クリックかenter
				if(IsOkeyKeyDown())
				{
					if(fadeTimeScr)
					{
						SetStageState(StageState.FADEOUT);
						fadeTimeScr.SetFadeType(FadeTime.FadeType.FADEOUT);
					}
					else
					{
						SetStageState(StageState.JUMPTITLE);
					}
				}
				break;

			// ステージフェードアウト
			case StageState.FADEOUT:
				if (fadeTimeScr)
				{
					if (fadeTimeScr.IsFadeOutFinished())
					{
						SetStageState(StageState.JUMPTITLE);
						break;
					}
					if (fadeTimeScr.GetFadeType() != FadeTime.FadeType.FADEOUT)
					{
						fadeTimeScr.SetFadeType(FadeTime.FadeType.FADEOUT);
					}
				}
				else
				{
					SetStageState(StageState.JUMPTITLE);
				}
				break;

			// ステージクリアステータス時
			case StageState.STAGEFAILURE:
				// 無線が無しになった時
				if (wirelessManagerScr.GetWirelessMode() == WirelessManager.WirelessMode.NONE)
				{
					// ステージステータスをプレイに変更
					SetStageState(StageState.FADEOUT);
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
			case StageState.READY:
				// 最初の無線
				wirelessManagerScr.SetWirelessMode(WirelessManager.WirelessMode.FIRST);
				break;

			case StageState.FADEOUT:
				wirelessManagerScr.SetWirelessMode(WirelessManager.WirelessMode.NONE);
				break;
				
			// ステージクリア用テキスト
			case StageState.STAGECLEAR:
				// クリアの無線
				wirelessManagerScr.SetWirelessMode(WirelessManager.WirelessMode.STAGECLEAR);
				// タイムストップ
				timeDisplayScr.SetTimeMode(TimeDisplay.TimeMode.STOP);
				break;

			// タイトル移動前用テキスト
			case StageState.CLEARNEXT:
				break;

			// ステージ失敗用テキスト
			case StageState.STAGEFAILURE:
				// 失敗の無線
				wirelessManagerScr.SetWirelessMode(WirelessManager.WirelessMode.STAGEFAILURE);
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

			// その他テキスト削除
			default:
				wirelessManagerScr.SetWirelessMode(WirelessManager.WirelessMode.NONE);
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
		SetStageState(StageState.STAGECLEAR);
	}
}
