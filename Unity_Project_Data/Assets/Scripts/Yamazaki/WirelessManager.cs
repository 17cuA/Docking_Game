// 作成者：17CU0334 山嵜ジョニー
// 作成日：2019/10/04
// 概要：無線
using UnityEngine;
using UnityEngine.UI;

public class WirelessManager : MonoBehaviour
{
	// ゲームが開始するまでの時間経過
	[SerializeField, NonEditable]
	private float stageReadyDelay = 0.0f;      // 現在の経過時間
	private float stageReadyDelayMax = 8.0f;    // 最大の待ち時間

	// シーンが切り替わる時間経過
	[SerializeField, NonEditable]
	private float nextStageDelay;       // 現在の経過時間
	public float nextStageDelayMax = 3.0f;    // 最大の待ち時間

	// 通常メッセージ無線の時間経過
	[SerializeField, NonEditable]
	private float displayTime;				// 現在の経過時間
	public float displayTimeMax = 3.0f;		// 最大の待ち時間

	// ステージ上テキスト
	public Text wirelessJPText;
	public Text wirelessENText;

	public enum WirelessMode
	{
		NONE,
		FIRST,
		STAGECLEAR,
		STAGEFAILURE,
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

		public WirelessString(string _sJP, string _sUS) : this()
		{
			this.sJP = _sJP;
			this.sUS = _sUS;
		}
	}

	private WirelessString[] wirelessList = new WirelessString[20]
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
		new WirelessString("", ""),
		new WirelessString("", ""),
	};

	[SerializeField, NonEditable]
	private WirelessMode wirelessMode = WirelessMode.NONE;

	// Start is called before the first frame update
	void Start()
    {
		wirelessJPText.text = "";
		wirelessENText.text = "";
		wirelessMode = WirelessMode.NONE;
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

			default:
				break;
		}

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
		}
	}
}
