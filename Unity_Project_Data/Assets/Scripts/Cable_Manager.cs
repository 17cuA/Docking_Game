using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable_Manager : MonoBehaviour
{
	// 速度低下率 0 ~ 1
	public float decreaseRate;
	public float addXAngle;
	public float addYAngle;
	public float maxAddZAngle;
	public float maxAddYAngle;
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

	//public PrevInfo[] prevInfos = new PrevInfo[1500];
	public List<PrevInfo> prevInfos = new List<PrevInfo>();

	public List<GameObject> aaa = new List<GameObject>();
	// 距離間隔
	public float distanceInterval;


	// Start is called before the first frame update
	void Start()
	{
		//for (int i = 0; i < 500; i++)
		//{
		//GameObject game = Instantiate(prevPosPrefab,new Vector3(10,0,0),transform.rotation);
		//aaa.Add(game);
		//prevInfos[i].prevPosObj= Instantiate(prevPosPrefab);
		//prevInfos[i].prevPosObj.transform.position = prevInfos[i].prevPos = transform.position;
		//prevInfos[i].prevPosObj.transform.rotation = prevInfos[i].prevRot = transform.rotation;
		//}
	}

	void Update()
	{
		AngleUpdate();
		BoostUpdate();

		//if (Vector2.Distance(transform.position, aaa[0].transform.position) >= distanceInterval)
		//{
		//	for (int i = aaa.Count - 1; i > 0; i--)
		//	{
		//		aaa[i].transform.position = aaa[i -1].transform.position;
		//		aaa[i].transform.rotation = aaa[i - 1].transform.rotation;
		//	}
		//	aaa[0].transform.position = transform.position;
		//	aaa[0].transform.rotation = transform.rotation;
		//}
	}

	private void AngleUpdate()
	{
		if(Input.GetKey(KeyCode.W))
		{
			Debug.Log("上");
			addYAngle += addNum;
		}
		if (Input.GetKey(KeyCode.A))
		{
			Debug.Log("左");
			addXAngle -= addNum;
		}
		if(Input.GetKey(KeyCode.S))
		{
			Debug.Log("下");
			addYAngle -= addNum;
		}
		if (Input.GetKey(KeyCode.D))
		{
			Debug.Log("右");
			addXAngle += addNum;
		}

		//回転系の制限--------------------------------------
		if (addXAngle > maxAddZAngle)
		{
			addXAngle = maxAddZAngle;
		}
		else if (addXAngle < -maxAddZAngle)
		{
			addXAngle = -maxAddYAngle;
		}
		if (addYAngle > maxAddZAngle)
		{
			addYAngle = maxAddYAngle;
		}
		else if (addYAngle < -maxAddYAngle)
		{
			addYAngle = -maxAddYAngle;
		}
		//------------------------------------------
		transform.Rotate(addYAngle,addXAngle ,0.0f);
		addXAngle *= 1.0f - decreaseRate;
		addYAngle *= 1.0f - decreaseRate;
	}

	private void BoostUpdate()
	{
		if (Input.GetKey(KeyCode.Space))
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

		transform.Translate(new Vector3(0.0f, 0.0f, 1.0f) * addBoostPower);
		addBoostPower *= 1.0f - decreaseRate;
	}
}
