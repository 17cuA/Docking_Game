/*
 *　制作：2019/10/14
 *　作者：諸岡勇樹
 *　目的：HUDの統括
 *　2019/10/14：HUDポイントの調節
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Manager : MonoBehaviour
{
    //[SerializeField,Tooltip("上")]private RectTransform Up;
    //[SerializeField,Tooltip("下")]private RectTransform Up;
    //[SerializeField,Tooltip("右")]private RectTransform Up;
    //[SerializeField,Tooltip("左")]private RectTransform Up;

    [Header("変更オブジェクト")]
    [SerializeField, Tooltip("直線距離テキスト")]					private Text Distance;
    [SerializeField, Tooltip("X軸テキスト")]						private Text XAxis;
    [SerializeField, Tooltip("Y軸テキスト")]						private Text YAxis;
    [SerializeField, Tooltip("Z軸テキスト")]						private Text ZAxis;
    [SerializeField, Tooltip("外気少数テキスト")]					private Text Temperature;
    [SerializeField, Tooltip("ターゲットポインタートランスフォーム")]	private RectTransform TargetPointer;

	private GameObject Charger;
	private GameObject Smartphone;

	private float HUDMax = 202.0f;
	private const float StandardValue = -1.6f;
	private float posMax_Y = StandardValue;
	private float posMin_Y = -(((StandardValue + 1.978f)*2)- StandardValue);
	private float posMax_X = StandardValue + 1.978f;
	private float posMin_X = -1.978f - StandardValue;

	private float valume_PosY;
	private float valume_PosX;
	private float valume_HUD;

	private Vector3 DistanceNum;
	private Vector3 InitHUDPos;

	private Image TargetImage;
	private Color OriginalRed = new Color(1.0f, 0.0f, 0.0f, 180.0f / 255.0f);
	private Color OriginalRWhite = new Color(1.0f, 1.0f, 1.0f, 180.0f / 255.0f);

	void Start()
    {
		Charger = GameObject.Find("Charger");
		Smartphone = GameObject.Find("HaL9000");

		valume_PosY = posMax_Y - posMin_Y;
		valume_PosX = posMax_X - posMin_X;
		valume_HUD = HUDMax * 2.0f;

		InitHUDPos = new Vector3(-HUDMax, -HUDMax, 0.0f);
		TargetPointer.localPosition = InitHUDPos;

		TargetImage = TargetPointer.GetComponent<Image>();
	}

	void Update()
    {
		DistanceNum = Smartphone.transform.position - Charger.transform.position;
		// 距離-------------------------------------------------------------------
		Distance.text = "DISTANCE : " + OrganizeText(Vector3.Magnitude(DistanceNum));
		XAxis.text = "X : " + OrganizeText(DistanceNum.x);
		YAxis.text = "Y : " +OrganizeText(DistanceNum.y);
		ZAxis.text = "Z : " + OrganizeText(DistanceNum.z);
		// 距離-------------------------------------------------------------------

		Temperature.text = "TEMPERATURE : -270." + Random.Range(0, 99).ToString("D2");

		//　位置のパーセント
		var percent_X = Mathf.Abs(posMin_X - Charger.transform.position.x) / valume_PosX;
		var percent_Y = Mathf.Abs(posMin_Y - Charger.transform.position.y) / valume_PosY;

		if (percent_X > 1.0f || percent_Y > 1.0f)
		{
			var temp = Charger.transform.position - Smartphone.transform.position;
			temp.z = 0.0f;
			TargetPointer.localPosition = (temp.normalized * HUDMax);
		}
		else
		{
			TargetPointer.localPosition = InitHUDPos + new Vector3(valume_HUD * percent_X, valume_HUD * percent_Y, 0.0f);
		}
		if(percent_X < 0.54f && percent_X > 0.46f
			&& percent_Y < 0.54f && percent_Y > 0.46f)
		{
			TargetImage.color = OriginalRed;
		}
		else
		{
			TargetImage.color = OriginalRWhite;
		}

	}

	/// <summary>
	/// テキスト整理
	/// </summary>
	/// <param name="num"> 整理する数字 </param>
	/// <returns> テキスト </returns>
	private string OrganizeText(float num)
	{
		float absolute = Mathf.Abs(num); 
		string rString = ((int)absolute).ToString("000");
		rString += (absolute - ((int)absolute)).ToString("F2"/*小数点以下2桁*/).TrimStart('0'/*先頭のゼロ削除*/);
		return rString;
	}
}
