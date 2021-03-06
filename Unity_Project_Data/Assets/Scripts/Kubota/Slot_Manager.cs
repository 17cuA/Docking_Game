﻿/*
 *　制作：2019/10/04
 *　作者：諸岡勇樹
 *　2019/10/04：充電ケーブルと衝突時、ゲームクリア
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_Manager : MonoBehaviour
{
	public GameMaster GM;           //ゲームマスター（ゲームクリアかどうかの判定をしたりするよう）
	[SerializeField]private SmartphoneManagement Manager;
	void OnTriggerEnter(Collider col)
	{

		if (col.tag == "Charger")
		{
			Manager.OffTriggerCollidersEnabledSet = false;
			GM.SetStageState(GameMaster.StageState.STAGECLEAR);
		}
	} 
}
