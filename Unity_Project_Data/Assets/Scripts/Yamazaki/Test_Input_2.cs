using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Input_2 : MonoBehaviour
{
	// 速度低下率 0 ~ 1
	public float decreaseRate;
	public float addZAngle;
	public float maxAddZAngle;
	public float addNum;

	public float boostPower;
	public float addBoostPower;
	public float maxAddBoostPower;
	public float addOil;


    // Start is called before the first frame update
    void Start()
    {
    }

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.A))
		{
			Debug.Log("した");
			addZAngle -= addNum;
		}

		if (Input.GetKey(KeyCode.D))
		{
			Debug.Log("うえ");
			addZAngle += addNum;

		}

		if (Input.GetKey(KeyCode.Space))
		{
			Debug.Log("すすむ");
			addBoostPower += addOil;
		}

		if (addZAngle > maxAddZAngle)
		{
			addZAngle = maxAddZAngle;
		}
		else if (addZAngle < -maxAddZAngle)
		{
			addZAngle = -maxAddZAngle;
		}

		if (addBoostPower > maxAddBoostPower)
		{
			addBoostPower = maxAddBoostPower;
		}
		else if (addBoostPower < -maxAddBoostPower)
		{
			addBoostPower = -maxAddBoostPower;
		}

		transform.Rotate(0.0f, 0.0f, addZAngle);
		transform.Translate(new Vector3(1.0f, 0.0f, 0.0f) * addBoostPower);

		addZAngle *= 1.0f - decreaseRate;
		addBoostPower *= 1.0f - decreaseRate;
    }
}
