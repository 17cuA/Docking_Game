//---------------------------------------
// ゲージ系のUIにつかえる
//---------------------------------------
// 作成者:高野
// 作成日:2019.07.14
//--------------------------------------
// 更新履歴
// 2019.07.14 作成
// 2019.10.10 ドッキングへ渡すために不要なメソッドを削除
//--------------------------------------
// 仕様 ゲージの増減
//-------------------------------------
// MEMO
// Canvas
//	(親) マスク用のオブジェクト	- UI_Guage,Image,Mask,
//	(子) ゲージの中身のオブジェクト - Image
//
// 使い方
// (1)Call_SetmaxValueを呼んで最大値を設定する(動的に最大値が変わらないならインスペクタで良い)
// (2)Call_UpdateGuageを呼んでゲージを更新する(面倒だったらこのクラスのUpdateで呼び続けて、currentValueの値を外部から操作すれば動く)
// (3)Call_ResetParameterを呼んでリセット
//
// Mask用のImageとゲージImageの大きさは一緒にして下さい
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Mask))]

public class UI_Gauge : MonoBehaviour
{
	[SerializeField] private GameObject guage;

	[SerializeField] private RectTransform guagePosition;

	 private float guageWidth;

	[SerializeField] private int initValue = 0;     //初期値
	[SerializeField] private int maxValue = 0;      //最大値
	[SerializeField] private int currentValue = 0;  //現在値

	/// <summary>
	/// ゲージの更新をするときに呼ぶ
	/// </summary>
	/// <param name="currentValue">現在値</param>
	public void Call_UpdateGuage(int currentValue)
	{
		//最大値より大きい動きはしないように	
		if(maxValue > currentValue)
		{
			guagePosition.localPosition = new Vector3(CalcMove(maxValue, currentValue), 0, 0);
		}
	}

	/// <summary>
	/// ゲージの最大値をセットするときに呼ぶ
	/// </summary>
	/// <param name="_maxValue"></param>
	public void Call_SetMaxValue(int _maxValue)
	{
		maxValue = _maxValue;
	}

	/// <summary>
	/// リセットするときに呼ぶ
	/// </summary>
	public void Call_ResetParameter()
	{
		currentValue = initValue;
	}

	/// <summary>
	/// ゲージの中身の移動量の計算
	/// </summary>
	/// <returns></returns>
	private float CalcMove(float _valueMax, float _value)
	{
		float temp = _valueMax + _value;

		if (temp != 0)
		{
			return guageWidth * (_valueMax + _valueMax - temp) / _valueMax - 1;
		}
		return 0;
	}

	private void Awake()
	{
		if (guage == null || guagePosition == null)
		{
			Debug.LogError("参照エラー : UI_Guageに必要な参照がありません。インスペクタで設定して下さい！");
		}
	}
	private void Start()
	{
		//画像の大きさを取得
		guageWidth = guage.GetComponent<RectTransform>().sizeDelta.x;
		//初期化
		currentValue = initValue;
	}

	private void Update()
	{
		Call_UpdateGuage(currentValue);
	}
}
