using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable_Manager : MonoBehaviour
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

	//public PrevInfo[] prevInfos = new PrevInfo[1500];
	public List<PrevInfo> prevInfos = new List<PrevInfo>();

	public List<GameObject> aaa = new List<GameObject>();
	// 距離間隔
	public float distanceInterval;


	// Start is called before the first frame update
	void Start()
	{
		for (int i = 0; i < 500; i++)
		{
			GameObject game = Instantiate(prevPosPrefab,new Vector3(10,0,0),transform.rotation);
			aaa.Add(game);
			//prevInfos[i].prevPosObj= Instantiate(prevPosPrefab);
			//prevInfos[i].prevPosObj.transform.position = prevInfos[i].prevPos = transform.position;
			//prevInfos[i].prevPosObj.transform.rotation = prevInfos[i].prevRot = transform.rotation;
		}
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

	/// <summary>
	/// ストラクトの情報の
	/// </summary>
	/// <param name="i">要素数番号</param>
	/// <param name="Is_NotFirst">最初かどうか</param>
	/// <returns></returns>
	private PrevInfo Pos_Update(int i , bool Is_NotFirst)
	{
		PrevInfo obj = new PrevInfo();
		obj.prevPosObj = prevInfos[i - 1].prevPosObj;
		if (Is_NotFirst)
		{
			obj.prevPosObj.transform.position = obj.prevPos = prevInfos[i - 1].prevPos;
			obj.prevPosObj.transform.rotation = obj.prevRot = prevInfos[i - 1].prevRot;
		}
		else
		{
			obj.prevPosObj.transform.position = obj.prevPos = transform.position;
			obj.prevPosObj.transform.rotation = obj.prevRot = transform.rotation;
		}
		return obj;

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

		transform.Translate(new Vector3(1.0f, 0.0f, 0.0f) * addBoostPower);
		addBoostPower *= 1.0f - decreaseRate;
	}
}
