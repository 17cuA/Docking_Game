// 作成者：17CU0334 山嵜ジョニー
// 作成日：2019/10/04
// 概要：無線
using UnityEngine;
using UnityEngine.UI;

public class WirelessManager : MonoBehaviour
{
	// ゲームが開始するまでの時間経過
	[SerializeField, NonEditable, Tooltip("ゲームが開始するまでの時間経過")]
	private float stageReadyDelay = 0.0f;      // 現在の経過時間
	[SerializeField, Tooltip("ゲームが開始するまでの最大の待ち時間")]
	private float stageReadyDelayMax = 8.0f;    // 最大の待ち時間

	// シーンが切り替わる時間経過
	[SerializeField, NonEditable, Tooltip("シーンが切り替わる時間経過")]
	private float nextStageDelay;       // 現在の経過時間
	[SerializeField, Tooltip("シーンが切り替わる最大の待ち時間")]
	public float nextStageDelayMax = 3.0f;    // 最大の待ち時間

	// 通常メッセージ無線の時間経過
	[SerializeField, NonEditable, Tooltip("通常メッセージ無線の時間経過")]
	private float displayTime;              // 現在の経過時間
	[SerializeField, Tooltip("通常メッセージ無線の最大の待ち時間")]
	public float displayTimeMax = 3.0f;     // 最大の待ち時間

	// ステージ上テキスト
	[SerializeField, Tooltip("ステージ上ジャパニーズテキスト")]
	public Text wirelessJPText;
	[SerializeField, Tooltip("ステージ上イングリシュテキスト")]
	public Text wirelessENText;

	public enum WirelessMode
	{
		NONE,
		FIRST,
		FIRST_1,
		FIRST_2,
		FIRST_3,
		FIRST_4,
		FIRST_5,
		FIRST_6,
		FIRST_7,
		STAGECLEAR,
		STAGECLEAR_1,
		STAGECLEAR_2,
		STAGECLEAR_3,
		STAGECLEAR_4,
		STAGECLEAR_5,
		STAGEFAILURE,
		STAGEFAILURE_1,
		STAGEFAILURE_2,
		STAGEFAILURE_3,
		MESSAGE_1,
		MESSAGE_2,
		DEBUG_1,
		DEBUG_2,
	}

	private struct WirelessString
	{
		public string sJP;	// 日本語
		public string sUS;	// 英語
		public string sES;    // スペイン語
		public string sCN;    // 中国語
		public string sKR;	// 韓国語

		public WirelessString(string _sJP = "", string _sUS = "", string _sES = "", string _sCN = "", string sKR = "") : this()
		{
			this.sJP = _sJP;
			this.sUS = _sUS;
		}
	}

	private WirelessString[] wirelessList = new WirelessString[33]
	{
		new WirelessString( "" , ""),
		new WirelessString( "コードＴＣー３「ドッキングに成功した」", "CodeＴＣ３「Docking successful」"),
		new WirelessString("コンロールセンター「ミッションは失敗した…」", "ControlCenter「Mission failed...」"),
		new WirelessString("コンロールセンター「慎重に…」","ControlCenter「Please be careful...」"),
		new WirelessString("コードＴＣー３「．．．」","CodeＴＣー３「．．．」"),
		new WirelessString("コードＴＣー３「ドッキングに成功した」","ＣｏｄｅＴＣー３「Ｄｏｃｋｉｎｇ　ｓｕｃｃｅｓｓｆｕｌ」"),
		new WirelessString("コンロールセンター「ミッションは失敗した．．．」","ＣｏｎｔｒｏｌＣｅｎｔｅｒ「Ｍｉｓｓｉｏｎ　ｆａｉｌｅｄ．．．」"),
		new WirelessString("コンロールセンター「慎重に．．．」","ＣｏｎｔｒｏｌＣｅｎｔｅｒ「Ｐｌｅａｓｅ　ｂｅ　ｃａｒｅｆｕｌ」"),
		new WirelessString("コードＴＣー３「．．．」","CodeＴＣー３「．．．」"),
		new WirelessString("コードＴＣー３「了解」","CodeＴＣー３「Roger」"),
		new WirelessString("コンロールセンター「慎重にドッキングを開始せよ」", "ControlCenter「CodeＴＣー３, start docking carefully"),
		new WirelessString("コードＴＣー３「了解」", "CodeＴＣー３「Roger」"),
		new WirelessString("コンロールセンター「慎重にドッキングを開始せよ」", "ＣｏｎｔｒｏｌＣｅｎｔｅｒ「ＣｏｄｅＴＣー３，ｓｔａｒｔ　ｄｏｃｋｉｎｇ　ｃａｒｅｆｕｌｌｙ」"),
		new WirelessString("コードＴＣー３「了解」", "ＣｏｄｅＴＣー３「Ｒｏｇｅｒ」"),
		new WirelessString("コンロールセンター「慎重にドッキングを開始せよ」", "ＣｏｎｔｒｏｌＣｅｎｔｅｒ「ＣｏｄｅＴＣー３，　ｓｔａｒｔ」"),
		new WirelessString("コンロールセンター「慎重にドッキングを開始せよ」", "ＣｏｎｔｒｏｌＣｅｎｔｅｒ「ｄｏｃｋｉｎｇ　ｃａｒｅｆｕｌｌｙ」"),
		new WirelessString("「・・・！？時が止まっただと・・・！？", "「．．．！Ｓｔｏｐｐｅｄ！？」"),
		new WirelessString("「時間が動いた」", "「ＴＩＭＥＩＳＭＯＶＥ」"),

		// 
		new WirelessString("「最終作戦軌道への投入準備完了。」", ""),
		new WirelessString("「了解。相対速度、再計算を開始。異常なし。」"),
		new WirelessString("「座標高度を再確認。すべて異常なしです。」"),
		new WirelessString("「了解。チャージングターミナル、これより作戦行動に移る。」"),
		new WirelessString("「現時点ですべてのリモート誘導を切断。」"),
		new WirelessString("「以後の制御はローカルで行う。」"),
		new WirelessString("「グッド・ラック」"),
		// ドッキング成功時無線字幕
		new WirelessString("Ｐｉｌｏｔ「ドッキング成功」"),
		new WirelessString("ー管制室に響き渡る歓声ー"),
		new WirelessString("Ｐｉｌｏｔ「これよりフェイズ３へ移行する。」"),
		new WirelessString("ＨＱ「難しい軌道だったがよくやってくれた。」"),
		new WirelessString("Ｐｉｌｏｔ「妻の機嫌を取るほうがよっぽど難しいさ。」"),
		// ドッキング失敗時無線字幕
		new WirelessString("Ｐｉｌｏｔ「ドッキング失敗、スマフォから高エネルギー反応を感知」"),
		new WirelessString("ＨＱ「未知の元素を検出、コアの温度が２０００万度を突破！」"),
		new WirelessString("ＨＱ「こ、このままでは・・・」"),
	};

	[SerializeField, NonEditable]
	private WirelessMode wirelessMode = WirelessMode.NONE;

	// Start is called before the first frame update
	void Start()
    {
		wirelessJPText.text = "";
		wirelessENText.text = "";
		wirelessMode = WirelessMode.NONE;
		wirelessENText.enabled = false;
		wirelessJPText.enabled = true;
		wirelessJPText.color = Color.white;
		wirelessENText.color = Color.white;
	}

	public void SetWirelessMode(WirelessMode w)
	{
		switch(w)
		{
			case WirelessMode.NONE:
				wirelessJPText.text = "";
				wirelessENText.text = wirelessList[0].sUS;
				break;

			case WirelessMode.STAGECLEAR:
				wirelessJPText.text = wirelessList[5].sJP;
				wirelessENText.text = wirelessList[5].sUS;
				break;

			case WirelessMode.STAGEFAILURE:
				wirelessJPText.text = wirelessList[6].sJP;
				wirelessENText.text = wirelessList[6].sUS;
				break;

			case WirelessMode.MESSAGE_1:
				wirelessJPText.text = wirelessList[7].sJP;
				wirelessENText.text = wirelessList[7].sUS;
				displayTime = 0.0f;
				break;

			case WirelessMode.MESSAGE_2:
				wirelessJPText.text = wirelessList[8].sJP;
				wirelessENText.text = wirelessList[8].sUS;
				displayTime = 0.0f;
				break;

			case WirelessMode.DEBUG_1:
				wirelessJPText.text = wirelessList[16].sJP;
				wirelessENText.text = wirelessList[16].sUS;
				break;

			case WirelessMode.DEBUG_2:
				wirelessJPText.text = wirelessList[17].sJP;
				wirelessENText.text = wirelessList[17].sUS;
				break;

			case WirelessMode.FIRST_1:
				BGM_Manager.BGM_obj.Active_Wireless();
				wirelessJPText.text = wirelessList[18].sJP;
				break;

			case WirelessMode.FIRST_2:
				wirelessJPText.text = wirelessList[19].sJP;
				break;

			case WirelessMode.FIRST_3:
				wirelessJPText.text = wirelessList[20].sJP;
				break;

			case WirelessMode.FIRST_4:
				wirelessJPText.text = wirelessList[21].sJP;
				break;

			case WirelessMode.FIRST_5:
				wirelessJPText.text = wirelessList[22].sJP;
				break;

			case WirelessMode.FIRST_6:
				wirelessJPText.text = wirelessList[23].sJP;
				break;

			case WirelessMode.FIRST_7:
				wirelessJPText.text = wirelessList[24].sJP;
				break;

			case WirelessMode.STAGECLEAR_1:
				wirelessJPText.text = wirelessList[25].sJP;
				break;

			case WirelessMode.STAGECLEAR_2:
				wirelessJPText.text = wirelessList[26].sJP;
				break;

			case WirelessMode.STAGECLEAR_3:
				wirelessJPText.text = wirelessList[27].sJP;
				break;

			case WirelessMode.STAGECLEAR_4:
				wirelessJPText.text = wirelessList[28].sJP;
				break;

			case WirelessMode.STAGECLEAR_5:
				wirelessJPText.text = wirelessList[29].sJP;
				break;

			case WirelessMode.STAGEFAILURE_1:
				wirelessJPText.text = wirelessList[30].sJP;
				break;
			case WirelessMode.STAGEFAILURE_2:
				wirelessJPText.text = wirelessList[31].sJP;
				break;
			case WirelessMode.STAGEFAILURE_3:
				wirelessJPText.text = wirelessList[32].sJP;
				break;

			default:
				break;
		}

		stageReadyDelay = 0.0f;
		wirelessMode = w;
	}

    public void SetTime(float readyT, float nextStageT, float displayT)
    {
        stageReadyDelayMax = readyT;
        nextStageDelayMax = nextStageT;
        displayTimeMax = displayT;
    }

	public WirelessMode GetWirelessMode()
	{
		return wirelessMode;
	}

	// Update is called once per frame
	void Update()
	{
		switch(wirelessMode)
		{
			case WirelessMode.FIRST:
				// 時間を経過
				stageReadyDelay += Time.deltaTime;
				if (stageReadyDelay >= stageReadyDelayMax)
				{
					stageReadyDelay = 0.0f;
					// 無線なしに変更
					SetWirelessMode(WirelessMode.NONE);
				}
				else if (stageReadyDelay >= stageReadyDelayMax * 1.0f / 2.0f)
				{
					wirelessJPText.text = wirelessList[13].sJP;
					wirelessENText.text = wirelessList[13].sUS;
				}
				else if (stageReadyDelay >= stageReadyDelayMax * (1.0f / 2.0f) * 0.5f)
				{
					wirelessJPText.text = wirelessList[15].sJP;
					wirelessENText.text = wirelessList[15].sUS;
					Debug.Log(wirelessList[15].sUS);
				}
				else if (stageReadyDelay >= 0)
				{
					wirelessJPText.text = wirelessList[14].sJP;
					wirelessENText.text = wirelessList[14].sUS;
				}
				else
				{
					wirelessJPText.text = "";
				}
				break;

			case WirelessMode.STAGECLEAR:
				// 時間を経過
				nextStageDelay += Time.deltaTime;
				// 待ち時間を達した時
				if (nextStageDelay >= nextStageDelayMax)
				{
					nextStageDelay = 0.0f;

					// 無線なしに変更
					SetWirelessMode(WirelessMode.NONE);
				}
				break;

			case WirelessMode.STAGEFAILURE:
				// 時間を経過
				nextStageDelay += Time.deltaTime;
				// 待ち時間を達した時
				if (nextStageDelay >= nextStageDelayMax)
				{
					nextStageDelay = 0.0f;

					// 無線なしに変更
					SetWirelessMode(WirelessMode.NONE);
				}
				break;

			case WirelessMode.MESSAGE_1:
			case WirelessMode.MESSAGE_2:
			case WirelessMode.DEBUG_1:
			case WirelessMode.DEBUG_2:
			case WirelessMode.FIRST_1:
				// 時間を経過
				displayTime += Time.deltaTime;
				// 待ち時間を達した時
				if (displayTime >= displayTimeMax)
				{
					displayTime = 0.0f;

					// 無線なしに変更
					SetWirelessMode(WirelessMode.FIRST_2);
				}
				break;

			case WirelessMode.FIRST_2:
				// 時間を経過
				displayTime += Time.deltaTime;
				// 待ち時間を達した時
				if (displayTime >= displayTimeMax)
				{
					displayTime = 0.0f;

					// 無線なしに変更
					SetWirelessMode(WirelessMode.FIRST_3);
				}
				break;

			case WirelessMode.FIRST_3:
				// 時間を経過
				displayTime += Time.deltaTime;
				// 待ち時間を達した時
				if (displayTime >= displayTimeMax)
				{
					displayTime = 0.0f;

					// 無線なしに変更
					SetWirelessMode(WirelessMode.FIRST_4);
				}
				break;

			case WirelessMode.FIRST_4:
				// 時間を経過
				displayTime += Time.deltaTime;
				// 待ち時間を達した時
				if (displayTime >= displayTimeMax)
				{
					displayTime = 0.0f;

					// 無線なしに変更
					SetWirelessMode(WirelessMode.FIRST_5);
				}
				break;

			case WirelessMode.FIRST_5:
				// 時間を経過
				displayTime += Time.deltaTime;
				// 待ち時間を達した時
				if (displayTime >= displayTimeMax)
				{
					displayTime = 0.0f;

					// 無線なしに変更
					SetWirelessMode(WirelessMode.FIRST_6);
				}
				break;

			case WirelessMode.FIRST_6:
				// 時間を経過
				displayTime += Time.deltaTime;
				// 待ち時間を達した時
				if (displayTime >= displayTimeMax)
				{
					displayTime = 0.0f;

					// 無線なしに変更
					SetWirelessMode(WirelessMode.FIRST_7);
				}
				break;

			case WirelessMode.FIRST_7:
				// 時間を経過
				displayTime += Time.deltaTime;
				// 待ち時間を達した時
				if (displayTime >= displayTimeMax)
				{
					displayTime = 0.0f;

					// 無線なしに変更
					SetWirelessMode(WirelessMode.NONE);
				}
				break;

			case WirelessMode.STAGECLEAR_1:
				// 時間を経過
				displayTime += Time.deltaTime;
				// 待ち時間を達した時
				if (displayTime >= displayTimeMax)
				{
					displayTime = 0.0f;

					// 無線なしに変更
					SetWirelessMode(WirelessMode.STAGECLEAR_2);
				}
				break;

			case WirelessMode.STAGECLEAR_2:
				// 時間を経過
				displayTime += Time.deltaTime;
				// 待ち時間を達した時
				if (displayTime >= displayTimeMax)
				{
					displayTime = 0.0f;

					// 無線なしに変更
					SetWirelessMode(WirelessMode.STAGECLEAR_3);
				}
				break;

			case WirelessMode.STAGECLEAR_3:
				// 時間を経過
				displayTime += Time.deltaTime;
				// 待ち時間を達した時
				if (displayTime >= displayTimeMax)
				{
					displayTime = 0.0f;

					// 無線なしに変更
					SetWirelessMode(WirelessMode.STAGECLEAR_4);
				}
				break;

			case WirelessMode.STAGECLEAR_4:
				// 時間を経過
				displayTime += Time.deltaTime;
				// 待ち時間を達した時
				if (displayTime >= displayTimeMax)
				{
					displayTime = 0.0f;

					// 無線なしに変更
					SetWirelessMode(WirelessMode.STAGECLEAR_5);
				}
				break;

			case WirelessMode.STAGECLEAR_5:
				// 時間を経過
				displayTime += Time.deltaTime;
				// 待ち時間を達した時
				if (displayTime >= displayTimeMax)
				{
					displayTime = 0.0f;

					// 無線なしに変更
					SetWirelessMode(WirelessMode.NONE);
				}
				break;

			case WirelessMode.STAGEFAILURE_1:
				// 時間を経過
				displayTime += Time.deltaTime;
				// 待ち時間を達した時
				if (displayTime >= displayTimeMax / 3.0f)
				{
					displayTime = 0.0f;

					// 無線なしに変更
					SetWirelessMode(WirelessMode.STAGEFAILURE_2);
				}
				break;

			case WirelessMode.STAGEFAILURE_2:
				// 時間を経過
				displayTime += Time.deltaTime;
				// 待ち時間を達した時
				if (displayTime >= displayTimeMax / 3.0f)
				{
					displayTime = 0.0f;

					// 無線なしに変更
					SetWirelessMode(WirelessMode.STAGEFAILURE_3);
				}
				break;

			case WirelessMode.STAGEFAILURE_3:
				// 時間を経過
				displayTime += Time.deltaTime;
				// 待ち時間を達した時
				if (displayTime >= displayTimeMax / 3.0f)
				{
					displayTime = 0.0f;

					// 無線なしに変更
					SetWirelessMode(WirelessMode.NONE);
				}
				break;

			default:
				break;
		}
	}
}
