﻿//20191004
// 無線
using UnityEngine;
using UnityEngine.UI;

public class WirelessManager : MonoBehaviour
{
	// ゲームが開始するまでの時間経過
	[SerializeField, NonEditable]
	private float stageReadyDelay = 0.0f;      // 現在の経過時間
	public float stageReadyDelayMax = 6.0f;    // 最大の待ち時間

	// シーンが切り替わる時間経過
	[SerializeField, NonEditable]
	private float nextStageDelay;       // 現在の経過時間
	public float nextStageDelayMax;    // 最大の待ち時間

	// ステージ上テキスト
	public Text stageText;

	public enum WirelessMode
	{
		NONE,
		FIRST,
		STAGECLEAR,
		STAGEFAILURE,
	}

	[SerializeField, NonEditable]
	private WirelessMode wirelessMode = WirelessMode.NONE;

	// Start is called before the first frame update
	void Start()
    {
		stageText.text = "";
		wirelessMode = WirelessMode.NONE;
	}

	public void SetWirelessMode(WirelessMode w)
	{
		switch(w)
		{
			case WirelessMode.NONE:
				stageText.text = "";
				break;

			case WirelessMode.STAGECLEAR:
				stageText.text = "コード「ドッキングに成功した」\nCode「Docking successful」";
				break;

			case WirelessMode.STAGEFAILURE:
				stageText.text = "コンロールセンター「ミッションは失敗した…」\nControlCenter「Mission failed...」";
				break;

			default:
				break;
		}

		wirelessMode = w;
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
					stageText.text = "コード「了解」\nCode「Roger」";
				}
				else if (stageReadyDelay >= 0)
				{
					stageText.text = "コンロールセンター「慎重にドッキングを開始せよ」\nControlCenter「Code, start docking carefully」";
				}
				else
				{
					stageText.text = "";
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
		}
	}
}