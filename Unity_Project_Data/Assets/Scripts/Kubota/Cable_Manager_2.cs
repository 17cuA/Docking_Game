using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable_Manager_2 : MonoBehaviour
{
	private float speed;
	public float Speed_Max;
	public float decreaseRate;

	private float addXNum;
	private float addYNum;
	private float addZNum;

	public float addNum;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Forward_Move();
		Movement();
		Charger_Move();

	}

	void Movement()
	{
		if (Input.GetKey(KeyCode.W))
		{
			addYNum += addNum;
		}
		if (Input.GetKey(KeyCode.A))
		{
			Debug.Log("左");
			addXNum += -addNum;

		}
		if (Input.GetKey(KeyCode.S))
		{
			Debug.Log("下");
			addYNum += -addNum;

		}
		if (Input.GetKey(KeyCode.D))
		{
			Debug.Log("右");
			addXNum += addNum;

		}
	}

	void Forward_Move()
	{
		if(Input.GetKey(KeyCode.X))
		{
			addZNum += addNum;
		}
		if(Input.GetKey(KeyCode.Z))
		{
			addZNum += -addNum;
		}

	}

	void Charger_Move()
	{
		transform.Translate(new Vector3(addXNum, addYNum, addZNum) * Speed_Max);
		//addZNum *= 1.0f - decreaseRate;
		//addXNum *= 1.0f - decreaseRate;
		//addYNum *= 1.0f - decreaseRate;

	}
}
