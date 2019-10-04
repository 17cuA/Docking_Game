/*
 *　制作：2019/10/03
 *　作者：諸岡勇樹
 *　目的：スピードメータの制御
 *　2019/10/03：Arrow移動
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedmeter : MonoBehaviour
{
	[SerializeField, Tooltip("最大時の位置指定オブジェクト")] GameObject MaxmumObject;
	[SerializeField, Tooltip("最小時の位置指定オブジェクト")] GameObject MinimumObject;
	[SerializeField, Tooltip("動かす矢印オブジェクト")] GameObject arrowObject;
	[SerializeField, Tooltip("充電ケーブルオブジェクト")] GameObject chargerObject;

	private RectTransform ArrowTransform { get; set; }		// 矢印のTransform
	private Charger_Manager ChargerScript { get; set; }		// 充電ケーブルのスクリプト
	private float OverallDistance { get; set; }				// 最小から最大までの距離
	private float DistanceFromZero { get; set; }			// 最小からの距離
	private float Maxmum { get; set; }
	private float Minimum { get; set; }

	private void Start()
	{
		ArrowTransform = arrowObject.GetComponent<RectTransform>();
		ChargerScript = chargerObject.GetComponent<Charger_Manager>();
		Maxmum = MaxmumObject.transform.localPosition.y;
		Minimum = MinimumObject.transform.localPosition.y;
		OverallDistance = Maxmum - Minimum;
	}

	void LateUpdate()
    {
		// 出てるスピードのパーセント
		var percentageSpeed = ChargerScript.NowSpeed_Z / ChargerScript.MaxSpeed_Z;

		// 矢印の移動
		DistanceFromZero = Minimum + (OverallDistance * percentageSpeed);
		ArrowTransform.localPosition = new Vector3(ArrowTransform.localPosition.x, DistanceFromZero, ArrowTransform.localPosition.x);
	}
}
