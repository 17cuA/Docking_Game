using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DockingGame_Input;
public class Cable_Manager : MonoBehaviour
{
	// 速度低下率 0 ~ 1
	//public float decreaseRate;
	//public float addYAngle;
	//public float addXAngle;
	//public float maxAddZAngle;
	//public float maxAddYAngle;
	//public float addNum;

	//回転系の変数
	private float addXAngle;            //Xの加算の値
	private float addYAngle;            //Yの加算の値
	public float XAngle_Max;    //最大の値X
	public float YAngle_Max;    //最大の値Y
	[Range(0, 1)]
	public float addNun;                //加算する値の上昇率
	[Range(0, 1)]
	public float decreaseRate;      //減少する値

	//移動系の変数
	public float boostPower;
	public float addBoostPower;
	public float maxAddBoostPower;
	public float addOil;

	// 前情報がわかるためのPrefab
	public GameObject prevPosPrefab;

	// 前の情報
	//public struct PrevInfo
	//{
	//	// 前の位置
	//	public Vector3 prevPos;
	//	// 前の角度
	//	public Quaternion prevRot;
	//	// 前の位置オブジェクト
	//	public GameObject prevPosObj;
	//}

	//public PrevInfo[] prevInfos = new PrevInfo[1500];
	//public List<PrevInfo> prevInfos = new List<PrevInfo>();

	//public List<GameObject> aaa = new List<GameObject>();
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
	void AngleUpdate()
	{
		float x = Original_Input.StickLeft_X;
		float y = Original_Input.StickRight_Y;

		if(x > 0)
		{
			addXAngle += addNun;
		}
		else if(x < 0)
		{
			addYAngle -= addNun;
		}
		if(y > 0)
		{
			addYAngle += addNun;
		}
		else if(y < 0)
		{
			addYAngle -= addNun;
		}

		if (addXAngle > XAngle_Max) addXAngle = XAngle_Max;
		else if (addXAngle < -XAngle_Max) addXAngle = -XAngle_Max;
		if (addYAngle > YAngle_Max) addYAngle = YAngle_Max;
		else if (addYAngle < -YAngle_Max) addYAngle = -YAngle_Max;

		//if(transform.eulerAngles  new Vector3())

		//transform.Rotate(addXAngle, addYAngle, 0,Space.World);		//オブジェクトの回転
	}

	//private void AngleUpdate()
	//{
	//	if (Input.GetKey(KeyCode.W))
	//	{
	//		//Debug.Log(transform.rotation.x);
	//		addXAngle += addNum;
	//		//if (transform.eulerAngles.x > 90 || transform.eulerAngles.x < 0) addXAngle = 0;
	//	}
	//	if (Input.GetKey(KeyCode.A))
	//	{
	//		Debug.Log("左");
	//		addYAngle -= addNum;

	//	}
	//	if (Input.GetKey(KeyCode.S))
	//	{
	//		Debug.Log("下");
	//		addXAngle -= addNum;

	//	}
	//	if (Input.GetKey(KeyCode.D))
	//	{
	//		Debug.Log("右");
	//		addYAngle += addNum;

	//	}


	//	//回転系の制限--------------------------------------
	//	if (addYAngle > maxAddZAngle) addYAngle = maxAddZAngle;
	//	else if (addYAngle < -maxAddZAngle) addYAngle = -maxAddYAngle;
	//	if (addXAngle > maxAddZAngle) addXAngle = maxAddYAngle;
	//	if (addXAngle < -maxAddYAngle) addXAngle = -maxAddYAngle;

	//	if (transform.eulerAngles.x > 45)
	//	{
	//		addXAngle = 0;
	//		transform.eulerAngles = new Vector3(44.0f, transform.eulerAngles.y, transform.eulerAngles.z);
	//	}
	//	if (transform.eulerAngles.x < -45)
	//	{
	//		addXAngle = 0;
	//		transform.eulerAngles = new Vector3(-44.0f, transform.eulerAngles.y, transform.eulerAngles.z);
	//	}
	//	if (transform.eulerAngles.y > 45)
	//	{
	//		addYAngle = 0;
	//		transform.eulerAngles = new Vector3(transform.eulerAngles.x, 44.0f, transform.eulerAngles.z);
	//	}
	//	if (transform.eulerAngles.y < -45)
	//	{
	//		addYAngle = 0;
	//		transform.eulerAngles = new Vector3(transform.eulerAngles.x, -44.0f, transform.eulerAngles.z);
	//	}

	//	Debug.Log("だれだてめ" + transform.eulerAngles);
	//	//------------------------------------------
	//	transform.Rotate(addXAngle, addYAngle, 0.0f);
	//	//transform.eulerAngles = new Vector3(addYAngle, addXAngle, 0.0f);
	//	addYAngle *= 1.0f - decreaseRate;
	//	addXAngle *= 1.0f - decreaseRate;
	//}

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
