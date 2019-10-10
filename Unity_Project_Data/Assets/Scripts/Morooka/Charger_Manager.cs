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
	private bool isEnteredTheSlot;      // スマホの差込口に入ったか
	[SerializeField, Tooltip("スナップtarget")]private GameObject snapTargetPos;		// スナップターゲット位置
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
	//public Time
	private void Start()
	{
		myRigidbody = GetComponent<Rigidbody>();
		isEnteredTheSlot = false;
	}
	private void Update()
	{
		if (!isEnteredTheSlot)
		{
			Movement();         //移動処理
		}
		else
		{
			myRigidbody.velocity = Vector3.zero;
			transform.position = Vector3.MoveTowards(transform.position, snapTargetPos.transform.position, 0.01f);
		}
	}
	/// <summary>
	/// 移動処理
	/// </summary>
	private void Movement()
	{
		Vector2 saveInputNum_Right = new Vector3(Original_Input.StickRight_X, Original_Input.StickRight_Y);
		Vector2 saveInputNum_Left = new Vector3(Original_Input.StickLeft_X, Original_Input.StickLeft_Y);

		Vector3 saveInputNum = Vector3.zero;

			if (saveInputNum_Right.x < 0 && saveInputNum_Left.x > 0)
			{
				saveInputNum.y -= (Mathf.Abs(saveInputNum_Right.x) + Mathf.Abs(saveInputNum_Left.x)) / 200.0f;
			}
			else if (saveInputNum_Right.x > 0 && saveInputNum_Left.x <0)
			{
				saveInputNum.y += (Mathf.Abs(saveInputNum_Right.x) + Mathf.Abs(saveInputNum_Left.x)) / 200.0f;
			}
			else
			{
				saveInputNum.x += (saveInputNum_Right.x + saveInputNum_Left.x) / 200.0f;
			}

		// Y軸入力があるとき
		if (Mathf.Sin(saveInputNum_Right.y) == Mathf.Sin(saveInputNum_Left.y))
		{
			// スティックの向きに横移動
			saveInputNum.z += (saveInputNum_Right.y + saveInputNum_Left.y) / 200.0f;
		}
		else if (Mathf.Sin(saveInputNum_Right.y) != Mathf.Sin(saveInputNum_Left.y))
		{
			// 音を入れるかも
		}

		//入力処理--------------------
		//saveInputNum.y = Original_Input.StickLeft_Y / 100.0f;
		//---------------------------
		// 前後入力(スティック、ボタン対応)-------------------
		//saveInputNum.z = Original_Input.StickRight_Y / 100.0f;
		//if (Original_Input.ButtomFront_Hold) saveInputNum.z = 0.01f;
		//else if(Original_Input.ButtomBack_Hold)saveInputNum.z = -0.01f;
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

	private void OnTriggerEnter(Collider col)
	{
		// 穴に触れたとき
		if (col.tag =="Slot")
		{
			isEnteredTheSlot = true;
		}
	}
}
