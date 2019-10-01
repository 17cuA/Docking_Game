//ケーブルの動きを管理するスクリプト
//製作者：久保田達己

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable_Manager : MonoBehaviour
{
	//移動系に使用するもの-------------------------------------------
	Rigidbody RB;       //アタッチされているRigidbosyの情報取得
	float x;					//ｘ軸
	float y;					//ｙ軸
	//------------------------------------------------------------------

    void Start()
    {
		RB = GetComponent<Rigidbody>();
		
    }

    // Update is called once per frame
    void Update()
    {
		Cable_Move();
    }
	void Cable_Move()
	{
		y = Input.GetAxis("GamePad_1_Axis_2");              //y軸の入力

		transform.eulerAngles = new Vector3(0,0,y);


		RB.velocity = Vector3.left;
	}
}
