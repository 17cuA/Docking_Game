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
	private float targetMax = 34.0f;
	private float posMax = 5.0f;
	private float posMin;

	private float senter;
	private float valume_Pos;
	private float valume_HUD;

	private Vector3 DistanceNum;
	private Vector3 InitHUDPos;

	void Start()
    {
		Charger = GameObject.Find("Charger");
		Smartphone = GameObject.Find("HaL9000");
		senter = Smartphone.transform.position.y;
		posMin = senter * 2 + -posMax;

		valume_Pos = posMax - posMin;
		valume_HUD = HUDMax * 2.0f;

		InitHUDPos = new Vector3(-HUDMax, -HUDMax, 0.0f);
		TargetPointer.localPosition = InitHUDPos;
	}

	void Update()
    {
		DistanceNum = Smartphone.transform.position - Charger.transform.position;
		// 距離-------------------------------------------------------------------
		Distance.text = OrganizeText(Vector3.Magnitude(DistanceNum));
		XAxis.text = OrganizeText(DistanceNum.x);
		YAxis.text = OrganizeText(DistanceNum.y);
		ZAxis.text = OrganizeText(DistanceNum.z);
		// 距離-------------------------------------------------------------------

		Temperature.text = "TEMPERATURE : -000." + Random.Range(0, 99).ToString("D2");

		//　位置のパーセント
		var percent_X = DistanceNum.x / valume_Pos;
		var percent_Y = DistanceNum.y / valume_Pos;

		TargetPointer.localPosition = InitHUDPos + new Vector3(valume_HUD * percent_X, valume_HUD * percent_Y, 0.0f);
	}

	/// <summary>
	/// テキスト整理
	/// </summary>
	/// <param name="num"> 整理する数字 </param>
	/// <returns> テキスト </returns>
	private string OrganizeText(float num)
	{
		string rString = ((int)num).ToString("000");
		rString += (num - ((int)num)).ToString("F2"/*小数点以下2桁*/).TrimStart('0'/*先頭のゼロ削除*/);
		return rString;
	}
}
