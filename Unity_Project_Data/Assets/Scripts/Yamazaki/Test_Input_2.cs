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

	// 前情報がわかるためのPrefab
	public GameObject prevPosPrefab;

	// 前の情報
	public struct PrevInfo
	{
		// 前の位置
		public Vector3 prevPos;
		// 前の角度
		public Quaternion prevRot;
		// 前の位置オブジェクト
		public GameObject prevPosObj;
	}

	public PrevInfo[] prevInfos = new PrevInfo[500];

	// 距離間隔
	public float distanceInterval;
	

	// Start is called before the first frame update
	void Start()
	{
		for (int i = 0; i < prevInfos.Length; i++)
		{
			prevInfos[i].prevPosObj = Instantiate(prevPosPrefab);
			prevInfos[i].prevPosObj.transform.position = prevInfos[i].prevPos = transform.position;
			prevInfos[i].prevPosObj.transform.rotation = prevInfos[i].prevRot = transform.rotation;
		}
	}

	// Update is called once per frame
	void Update()
	{
		AngleUpdate();
		BoostUpdate();

		if(Vector2.Distance(transform.position, prevInfos[0].prevPos) >= distanceInterval)
		{
			for (int i = prevInfos.Length - 1; i > 0; i--)
			{
				prevInfos[i].prevPosObj.transform.position = prevInfos[i].prevPos = prevInfos[i - 1].prevPos;
				prevInfos[i].prevPosObj.transform.rotation = prevInfos[i].prevRot = prevInfos[i - 1].prevRot;
			}
			prevInfos[0].prevPosObj.transform.position = prevInfos[0].prevPos = transform.position;
			prevInfos[0].prevPosObj.transform.rotation = prevInfos[0].prevRot = transform.rotation;
		}
    }

	private void AngleUpdate()
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

		if (addZAngle > maxAddZAngle)
		{
			addZAngle = maxAddZAngle;
		}
		else if (addZAngle < -maxAddZAngle)
		{
			addZAngle = -maxAddZAngle;
		}

		transform.Rotate(0.0f, 0.0f, addZAngle);
		addZAngle *= 1.0f - decreaseRate;
	}

	private void BoostUpdate()
	{
		if(Input.GetKey(KeyCode.Space))
		{
			Debug.Log("すすむ");
			addBoostPower += addOil;
		}

		if (addBoostPower > maxAddBoostPower)
		{
			addBoostPower = maxAddBoostPower;
		}
		else if (addBoostPower < -maxAddBoostPower)
		{
			addBoostPower = -maxAddBoostPower;
		}

		transform.Translate(new Vector3(1.0f, 0.0f, 0.0f) * addBoostPower);
		addBoostPower *= 1.0f - decreaseRate;
	}
}
