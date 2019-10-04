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
	private Cable_Manager_2 ChargerScript { get; set; }		// 充電ケーブルのスクリプト
	private float OverallDistance { get; set; }				// 最小から最大までの距離
	private float DistanceFromZero { get; set; }			// 最小からの距離

	private void Start()
	{
		ArrowTransform = arrowObject.GetComponent<RectTransform>();
		ChargerScript = chargerObject.GetComponent<Cable_Manager_2>();
		OverallDistance = MaxmumObject.transform.position.y - MinimumObject.transform.position.y;
	}

	void LateUpdate()
    {
		// 移動量
		var AmountOfMovement = new Vector3(Mathf.Abs(ChargerScript.addXNum), Mathf.Abs(ChargerScript.addYNum), Mathf.Abs(ChargerScript.addZNum));
		// 出てるスピードのパーセント
		var percentageSpeed = AmountOfMovement.magnitude / ChargerScript.add_Max;

		// 矢印の移動
		DistanceFromZero = OverallDistance * percentageSpeed;
		ArrowTransform.localPosition = new Vector3(ArrowTransform.localPosition.x, DistanceFromZero, ArrowTransform.localPosition.x);
	}
}
