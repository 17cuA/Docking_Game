/*
 *　制作：2019/10/04
 *　作者：諸岡勇樹
 *　2019/10/04：充電ケーブルの移動
 *　2019/10/04：穴に入ったとき、ポジションスナップ
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DockingGame_Input;

public class Charger_Manager : MonoBehaviour
{
	private Rigidbody myRigidbody;    // 自身のRigidbody
	private bool isEnteredTheSlot;		// スマホの差込口に入ったか
	[Header("加速時の最大の値")]
	public float add_Max;

	#region いじるなBy諸岡
	/// <summary>
	/// 最大速度を渡す
	/// </summary>
	public float MaxSpeed { get { return new Vector3(add_Max, add_Max, add_Max).magnitude; } }
	/// <summary>
	///  現在の速度を渡す
	/// </summary>
	public float NowSpeed { get { return myRigidbody.velocity.magnitude; } }
	/// <summary>
	/// Z軸の最大速度
	/// </summary>
	public float MaxSpeed_Z { get { return add_Max; } }
	/// <summary>
	/// Z軸の現在速度
	/// </summary>
	public float NowSpeed_Z { get { return Mathf.Abs( myRigidbody.velocity.z); } }
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
	private void Movement()
	{
		//入力処理--------------------
		Vector3 saveInputNum = Vector3.zero;
		saveInputNum.x = Original_Input.StickLeft_X / 100.0f;
		saveInputNum.y = Original_Input.StickLeft_Y / 100.0f;
		//---------------------------

		// 前後入力(スティック、ボタン対応)-------------------
		saveInputNum.z = Original_Input.StickRight_Y / 100.0f;
		if (Original_Input.ButtomFront_Hold) saveInputNum.z = 0.01f;
		else if(Original_Input.ButtomBack_Hold)saveInputNum.z = -0.01f;
		// ---------------------------

		//	加速後の Velocity 値の仮保存
		Vector3 tempVelocity = myRigidbody.velocity + saveInputNum;

		// スピード制限(絶対値より)--------------------
		if (Mathf.Abs(tempVelocity.x) > add_Max) tempVelocity.x = Mathf.Abs(add_Max) * Mathf.Sign(tempVelocity.x);
		if (Mathf.Abs(tempVelocity.y) > add_Max) tempVelocity.y = Mathf.Abs(add_Max) * Mathf.Sign(tempVelocity.y);
		if (Mathf.Abs(tempVelocity.z) > add_Max) tempVelocity.z = Mathf.Abs(add_Max) * Mathf.Sign(tempVelocity.z);
		//----------------------------------------------

		// 速度適応
		myRigidbody.velocity = tempVelocity; 
	}
}
