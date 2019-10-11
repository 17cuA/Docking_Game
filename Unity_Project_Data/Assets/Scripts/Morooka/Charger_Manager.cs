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
	/// <summary>
	/// 充電の移動向き
	/// </summary>
	public enum MOVE_DIRECTION
	{
		eFRONT,		// まえ
		eBACK,		// うしろ
		eRIGHT,		// みぎ
		eLEFT,		// ひだり
		eSTOP,		// とまれ
	}

	private Rigidbody MyRigidbody { get; set; }    // 自身のRigidbody
	private bool IsEnteredTheSlot { get; set; }      // スマホの差込口に入ったか
	public MOVE_DIRECTION Direction { private set; get; } //現在の移動向き
	[SerializeField, Tooltip("スナップtarget")]private GameObject snapTargetPos;		// スナップターゲット位置
	[SerializeField, Tooltip("加速時の最大の値")]private float add_Max;
	[SerializeField, Tooltip("ゲームマスター")] private GameMaster GM;           //ゲームマスター（ゲームクリアかどうかの判定をしたりするよう）
	[SerializeField, Tooltip("移動速度")] private float speed;
	[SerializeField, Tooltip("ブレーキ速度")] private float brakeSpeed;

	#region いじるなBy諸岡
	/// <summary>
	/// 最大速度を渡す
	/// </summary>
	public float MaxSpeed { get { return new Vector3(add_Max, add_Max, add_Max).magnitude; } }
	/// <summary>
	///  現在の速度を渡す
	/// </summary>
	public float NowSpeed { get { return MyRigidbody.velocity.magnitude; } }
	/// <summary>
	/// Z軸の最大速度
	/// </summary>
	public float MaxSpeed_Z { get { return add_Max; } }
	/// <summary>
	/// Z軸の現在速度
	/// </summary>
	public float NowSpeed_Z { get { return Mathf.Abs( MyRigidbody.velocity.z); } }
	#endregion

	//public Time
	private void Start()
	{
		MyRigidbody = GetComponent<Rigidbody>();
		IsEnteredTheSlot = false;
	}
	private void Update()
	{
		if (!IsEnteredTheSlot)
		{
			if (GameMaster.instance.stageState == GameMaster.StageState.PLAYING)
			{
				Movement_2();         //移動処理
			}
		}
		else
		{
			MyRigidbody.velocity = Vector3.zero;
			transform.position = Vector3.MoveTowards(transform.position, snapTargetPos.transform.position, 0.01f);
		}
	}
	#region ムーブ
	/// <summary>
	/// 移動処理
	/// </summary>
	private void Movement()
	{
		Vector2 saveInputNum_Right = new Vector3(Original_Input.StickRight_X, Original_Input.StickRight_Y);
		Vector2 saveInputNum_Left = new Vector3(Original_Input.StickLeft_X, Original_Input.StickLeft_Y);

		// 操作なしのとき
		if(saveInputNum_Right.magnitude == 0.0f && 0.0f == saveInputNum_Left.magnitude)
		{
			Direction = MOVE_DIRECTION.eSTOP;
			return;
		}

		Vector3 saveInputNum = Vector3.zero;

		// 左右開きで下移動
		if (saveInputNum_Right.x < 0 && saveInputNum_Left.x > 0)
		{
			saveInputNum.y -= (Mathf.Abs(saveInputNum_Right.x) + Mathf.Abs(saveInputNum_Left.x)) / 200.0f;
		}
		// 左右綴じで上移動
		else if (saveInputNum_Right.x > 0 && saveInputNum_Left.x < 0)
		{
			saveInputNum.y += (Mathf.Abs(saveInputNum_Right.x) + Mathf.Abs(saveInputNum_Left.x)) / 200.0f;
		}
		else
		{
			saveInputNum.x += (saveInputNum_Right.x + saveInputNum_Left.x) / 200.0f;

			if(saveInputNum.x < 0)
			{
				Direction = MOVE_DIRECTION.eLEFT;
			}
			else if(saveInputNum.x > 0)
			{
				Direction = MOVE_DIRECTION.eRIGHT;
			}
		}

		// スティックの向きに前後移動
		if (saveInputNum_Right.y != 0.0f || saveInputNum_Left.y != 0)
		{
			saveInputNum.z += (saveInputNum_Right.y + saveInputNum_Left.y) / 200.0f;
			if ((saveInputNum_Right.y > 0 && saveInputNum_Left.y < 0)
				|| (saveInputNum_Right.y < 0 && saveInputNum_Left.y > 0 ))
			{
				saveInputNum.z = 0.0f;
				// 音を入れるかも
			}

			if(saveInputNum.z > 0.0f)
			{
				Direction = MOVE_DIRECTION.eFRONT;
			}
			else if(saveInputNum.z < 0.0f)
			{
				Direction = MOVE_DIRECTION.eBACK;
			}
		}

		//	加速後の Velocity 値の仮保存
		Vector3 tempVelocity = MyRigidbody.velocity + saveInputNum;

		// スピード制限(絶対値より)--------------------
		if (Mathf.Abs(tempVelocity.x) > add_Max) tempVelocity.x = Mathf.Abs(add_Max) * Mathf.Sign(tempVelocity.x);
		if (Mathf.Abs(tempVelocity.y) > add_Max) tempVelocity.y = Mathf.Abs(add_Max) * Mathf.Sign(tempVelocity.y);
		if (Mathf.Abs(tempVelocity.z) > add_Max) tempVelocity.z = Mathf.Abs(add_Max) * Mathf.Sign(tempVelocity.z);
		//----------------------------------------------

		// 速度適応
		MyRigidbody.velocity = tempVelocity; 
	}
	#endregion
	#region ムーブ2
	private void Movement_2()
	{

		Vector3 saveInputNum = new Vector3((Original_Input.StickLeft_X / 100.0f) * speed, (Original_Input.StickLeft_Y / 100.0f)*speed, 0.0f);
		if(Original_Input.ButtomFront_Hold || Input.GetKey(KeyCode.Space))
		{
			saveInputNum.z -= (1 / 100.0f) * brakeSpeed;
			if(MyRigidbody.velocity.z < 0.1f)
			{
				saveInputNum.z = 0.0f;
			}
		}
		else
		{
			saveInputNum.z += (1 / 100.0f) * speed;
			Direction = MOVE_DIRECTION.eFRONT;
		}

		if(Input.GetKey(KeyCode.LeftArrow))
		{
			saveInputNum.x -= (1 / 100.0f) * speed;
		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
			saveInputNum.x += (1 / 100.0f) * speed;
		}
		if(Input.GetKey(KeyCode.UpArrow))
		{
			saveInputNum.y += (1 / 100.0f) * speed;
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			saveInputNum.y -= (1 / 100.0f) * speed;
		}

		if (saveInputNum.x < 0)
		{
			Direction = MOVE_DIRECTION.eLEFT;
		}
		else if (saveInputNum.x > 0)
		{
			Direction = MOVE_DIRECTION.eRIGHT;
		}

		//	加速後の Velocity 値の仮保存
		Vector3 tempVelocity = MyRigidbody.velocity + saveInputNum;

		// スピード制限(絶対値より)--------------------
		if (Mathf.Abs(tempVelocity.x) > add_Max) tempVelocity.x = Mathf.Abs(add_Max) * Mathf.Sign(tempVelocity.x);
		if (Mathf.Abs(tempVelocity.y) > add_Max) tempVelocity.y = Mathf.Abs(add_Max) * Mathf.Sign(tempVelocity.y);
		if (Mathf.Abs(tempVelocity.z) > add_Max) tempVelocity.z = Mathf.Abs(add_Max) * Mathf.Sign(tempVelocity.z);
		//----------------------------------------------

		// 速度適応
		MyRigidbody.velocity = tempVelocity;
	}
	#endregion

	private void OnTriggerEnter(Collider col)
	{
		// 穴に触れたとき
		if (col.tag =="Slot")
		{
			IsEnteredTheSlot = true;
		}
	}
}
