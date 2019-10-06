using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root_Light_Manager : MonoBehaviour
{
	[SerializeField] GameObject Hal9000;
	[SerializeField] GameObject Chager;

	public GameObject Root_Light;
    void Start()
    {
		Debug.Log( calc_distance());
		calc_distance();
		make_distance();

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
		int num = Mathf.FloorToInt(calc_distance());

		int num2 = -num;
		for(int i = 0; i < -(num + 2); i++)
		{
			Instantiate(Root_Light, new Vector3(Hal9000.transform.position.x, Hal9000.transform.position.y, Hal9000.transform.position.z + -i), Quaternion.identity);
		}
	}
}
