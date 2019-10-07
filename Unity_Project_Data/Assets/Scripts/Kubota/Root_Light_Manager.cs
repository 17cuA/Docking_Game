using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root_Light_Manager : MonoBehaviour
{
	[SerializeField] GameObject Hal9000;
	[SerializeField] GameObject Chager;

	public GameObject Root_Light;
	int num;
	int num2;
	GameObject[] obj;

	void Start()
    {
		Debug.Log( calc_distance());
		num = Mathf.FloorToInt(calc_distance());
		num2 = -num;
		obj = new GameObject[num2];
		make_distance();

	}
	private void Update()
	{
		Move_Root();
	}
	//距離の計算
	float calc_distance()
	{
		Vector3 a = Hal9000.transform.position;
		Vector3 b = Chager.transform.position;

		return b.z - a.z;
		//return Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2) + Mathf.Pow(a.z - b.z, 2));
	}
	void make_distance()
	{
		Vector3 subscript = Chager.transform.position - Hal9000.transform.position;
		subscript = subscript / num2;
		//GameObject[] obj = new GameObject[num2];
		
		for(int i = 0; i < obj.Length; i++)
		{
			//Instantiate(Root_Light, new Vector3(Hal9000.transform.position.x, Hal9000.transform.position.y, Hal9000.transform.position.z + -i), Quaternion.identity);
			obj[i] = Instantiate(Root_Light,Hal9000.transform.position, Quaternion.identity);
			obj[i].transform.position = new Vector3(obj[i].transform.position.x + subscript.x * (i + 1), obj[i].transform.position.y + subscript.y * (i + 1), obj[i].transform.position.z + subscript.z * (i + 1));

		}
	}

	void Move_Root()
	{
		Vector3 subscript = Chager.transform.position - Hal9000.transform.position;
		subscript = subscript / num2;

		for (int i = 0; i < obj.Length; i++)
		{
			//obj[i].transform.position = new Vector3(obj[i].transform.position.x + subscript.x * (i + 1), obj[i].transform.position.y + subscript.y * (i + 1), obj[i].transform.position.z );
			//obj[i].transform.position =Hal9000.transform.position + subscript * (i + 1);
			obj[i].transform.position = new Vector3(Hal9000.transform.position.x + subscript.x * (i + 1), Hal9000.transform.position.y + subscript.y * (i + 1),obj[i].transform.position.z);


		}
	}
}
