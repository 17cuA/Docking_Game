/*
 *　制作：2019/10/04
 *　作者：諸岡勇樹
 *　2019/10/04：充電ケーブルの移動
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DockingGame_Input;

public class Charger_Manager : MonoBehaviour
{
	private Rigidbody myRigidbody;    // 自身のRigidbody

	[Header("加速時の最大の値")]
	public float add_Max;

	#region いじるなBy諸岡
	/// <summary>
	/// 最大速度を渡す
	/// </summary>
	public float GetMaxSpeed { get { return new Vector3(add_Max, add_Max, add_Max).magnitude; } }
	/// <summary>
	///  現在の速度を渡す
	/// </summary>
	public float GetNowSpeed { get { return myRigidbody.velocity.magnitude; } }
	#endregion

	public GameMaster GM;           //ゲームマスター（ゲームクリアかどうかの判定をしたりするよう）

	private void Start()
	{
		myRigidbody = GetComponent<Rigidbody>();
	}
	private void Update()
	{
		Movement();         //移動処理
	}
	/// <summary>
	/// 移動処理
	/// </summary>
	void Movement()
	{
		//入力処理--------------------
		Vector3 saveInputNum = Vector3.zero;
		saveInputNum.x = Original_Input.StickLeft_X / 100.0f;
		saveInputNum.y = Original_Input.StickLeft_Y / 100.0f;
		saveInputNum.z = Original_Input.StickRight_Y / 100.0f;
		//---------------------------

		//	加速後の Velocity 値の仮保存
		Vector3 tempVelocity = myRigidbody.velocity + saveInputNum;

		// スピード制限(絶対値より)--------------------
		if (Mathf.Abs(tempVelocity.x) > add_Max)
		{
			float sign = Mathf.Sign(tempVelocity.x);
			tempVelocity.x = Mathf.Abs(add_Max) * sign;
		}
		if (Mathf.Abs(tempVelocity.y) > add_Max)
		{
			float sign = Mathf.Sign(tempVelocity.y);
			tempVelocity.y = Mathf.Abs(add_Max) * sign;
		}
		if (Mathf.Abs(tempVelocity.z) > add_Max)
		{
			float sign = Mathf.Sign(tempVelocity.z);
			tempVelocity.z = Mathf.Abs(add_Max) * sign;
		}
		//----------------------------------------------

		// 速度適応
		myRigidbody.velocity = tempVelocity; 
	}
}
