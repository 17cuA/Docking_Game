using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DockingGame_Input;
public class Cable_Manager_2 : MonoBehaviour
{
	//加速度×Speedで移動の速度を変更している
	public float Speed;

	//各軸に加算されていく加速度------------------
	[HideInInspector] public float addXNum;
	[HideInInspector] public float addYNum;
	[HideInInspector] public float addZNum;
	//--------------------------------------------
	[Header("加速時の最大の値")]
	public float add_Max;
	[Header("加速度")]
	public float addNum;

	public GameMaster GM;			//ゲームマスター（ゲームクリアかどうかの判定をしたりするよう）
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Forward_Move(); //前後移動の入力
		Movement();         //上下移動の入力
		Charger_Move();     //キャラクタの移動
		Num_Limit();            //加速に上限を設ける
	}
	/// <summary>
	/// 上下移動の入力
	/// </summary>
	void Movement()
	{
		//入力処理--------------------
		float y = Original_Input.StickLeft_Y;
		float x = Original_Input.StickLeft_X;
		float z = Original_Input.StickRight_Y;
		//---------------------------
		//addXNum = x;
		//addYNum = y;

		//ボタン入力されたときの移動系---------------------
		//加速していくようになってる
		if (Input.GetKey(KeyCode.W)) addYNum += addNum;
		if (Input.GetKey(KeyCode.A)) addXNum += -addNum;
		if (Input.GetKey(KeyCode.S)) addYNum += -addNum;
		if (Input.GetKey(KeyCode.D)) addXNum += addNum;
	}
	/// <summary>
	/// 前後の移動の入力
	/// </summary>
	void Forward_Move()
	{
		//if(X_Input.b)
		//ボタンを押したら移動するだけの処理
		if (Input.GetKey(KeyCode.X))
		{
			addZNum += addNum;
		}
		if (Input.GetKey(KeyCode.Z))
		{
			addZNum += -addNum;
		}

	}
	/// <summary>
	/// キャラクタの移動
	/// </summary>
	void Charger_Move()
	{
		transform.Translate(new Vector3(addXNum, addYNum, addZNum) * Speed);
		//addZNum *= 1.0f - decreaseRate;
		//addXNum *= 1.0f - decreaseRate;
		//addYNum *= 1.0f - decreaseRate;

	}
	/// <summary>
	/// 上下と前後の移動速度に制限をかける
	/// </summary>
	void Num_Limit()
	{
		if (addZNum > add_Max) addZNum = add_Max;
		else if (addZNum < -add_Max) addZNum = -add_Max;
		if (addXNum > add_Max) addXNum = add_Max;
		else if (addXNum < -add_Max) addXNum = -add_Max;
		if (addYNum > add_Max) addYNum = add_Max;
		else if (addYNum < -add_Max) addYNum = -add_Max;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Hal") GM.stageState = GameMaster.StageState.STAGECLEAR;
	}
}
