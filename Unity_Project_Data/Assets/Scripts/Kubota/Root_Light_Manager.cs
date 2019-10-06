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

	// Update is called once per frame
	void Update()
    {

	}

	//距離の計算
	float calc_distance()
	{
		Vector3 a = Hal9000.transform.position;
		Vector3 b = Chager.transform.position;

		return Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2) + Mathf.Pow(a.z - b.z, 2));
	}
	void make_distance()
	{
		int num = Mathf.FloorToInt(calc_distance());

		for (int i = num; num > 0; i--)
		{
			Instantiate(Root_Light, new Vector3(0, Hal9000.transform.position.y, Hal9000.transform.position.z - num), Quaternion.identity);
		}
	}
}
