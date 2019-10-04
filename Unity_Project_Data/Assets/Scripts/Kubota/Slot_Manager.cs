using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_Manager : MonoBehaviour
{
	public GameMaster GM;           //ゲームマスター（ゲームクリアかどうかの判定をしたりするよう）
	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Charger") GM.stageState = GameMaster.StageState.STAGECLEAR;
	}
}
