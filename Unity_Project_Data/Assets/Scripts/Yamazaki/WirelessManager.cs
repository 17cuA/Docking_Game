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
	}

	private struct WirelessString
	{
		public string jPStr;
		public string eNStr;

		public WirelessString(string v1, string v2) : this()
		{
			this.jPStr = v1;
			this.eNStr = v2;
		}
	}

	private WirelessString[] wirelessList = new WirelessString[20]
	{
		new WirelessString( "" , ""),
		new WirelessString( "コード「ドッキングに成功した」", "Code「Docking successful」"),
		new WirelessString("コンロールセンター「ミッションは失敗した…」", "ControlCenter「Mission failed...」"),
		new WirelessString("コンロールセンター「慎重に…」","ControlCenter「Please be careful...」"),
		new WirelessString("コード「...」","Code「...」"),
		new WirelessString("コード「ドッキングに成功した」","Ｃｏｄｅ「Ｄｏｃｋｉｎｇ　ｓｕｃｃｅｓｓｆｕｌ」"),
		new WirelessString("コンロールセンター「ミッションは失敗した…」","ＣｏｎｔｒｏｌＣｅｎｔｅｒ「Ｍｉｓｓｉｏｎ　ｆａｉｌｅｄ．．．」"),
		new WirelessString("コンロールセンター「慎重に…」","ＣｏｎｔｒｏｌＣｅｎｔｅｒ「Ｐｌｅａｓｅ　ｂｅ　ｃａｒｅｆｕｌ」"),
		new WirelessString("コード「．．．」","Code「．．．」"),
		new WirelessString("コード「了解」","Code「Roger」"),
		new WirelessString("コンロールセンター「慎重にドッキングを開始せよ」", "ControlCenter「Code, start docking carefully"),
		new WirelessString("コード「了解」", "Code「Roger」"),
		new WirelessString("コンロールセンター「慎重にドッキングを開始せよ」", "ＣｏｎｔｒｏｌＣｅｎｔｅｒ「Ｃｏｄｅ，ｓｔａｒｔ　ｄｏｃｋｉｎｇ　ｃａｒｅｆｕｌｌｙ」"),
		new WirelessString("コード「了解」", "Ｃｏｄｅ「Ｒｏｇｅｒ」"),
		new WirelessString("コンロールセンター「慎重にドッキングを開始せよ」", "ＣｏｎｔｒｏｌＣｅｎｔｅｒ「Ｃｏｄｅ，　ｓｔａｒｔ」"),
		new WirelessString("コンロールセンター「慎重にドッキングを開始せよ」", "ＣｏｎｔｒｏｌＣｅｎｔｅｒ「ｄｏｃｋｉｎｇ　ｃａｒｅｆｕｌｌｙ」"),
		new WirelessString("", ""),
		new WirelessString("", ""),
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
				wirelessENText.text = wirelessList[0].eNStr;
				break;

			case WirelessMode.STAGECLEAR:
				wirelessJPText.text = wirelessList[5].jPStr;
				wirelessENText.text = wirelessList[5].eNStr;
				break;

			case WirelessMode.STAGEFAILURE:
				wirelessJPText.text = wirelessList[6].jPStr;
				wirelessENText.text = wirelessList[6].eNStr;
				break;

			case WirelessMode.MESSAGE_1:
				wirelessJPText.text = wirelessList[7].jPStr;
				wirelessENText.text = wirelessList[7].eNStr;
				displayTime = 0.0f;
				break;

			case WirelessMode.MESSAGE_2:
				wirelessJPText.text = wirelessList[8].jPStr;
				wirelessENText.text = wirelessList[8].eNStr;
				displayTime = 0.0f;
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
					wirelessJPText.text = wirelessList[13].jPStr;
					wirelessENText.text = wirelessList[13].eNStr;
				}
				else if (stageReadyDelay >= stageReadyDelayMax * (1.0f / 2.0f) * 0.25f)
				{
					wirelessJPText.text = wirelessList[15].jPStr;
					wirelessENText.text = wirelessList[15].eNStr;
					Debug.Log(wirelessList[15].eNStr);
				}
				else if (stageReadyDelay >= 0)
				{
					wirelessJPText.text = wirelessList[14].jPStr;
					wirelessENText.text = wirelessList[14].eNStr;
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
