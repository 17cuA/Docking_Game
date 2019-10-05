using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root_Light_Manager : MonoBehaviour
{
	[SerializeField] GameObject Hal9000;
	[SerializeField] GameObject Chager;
    void Start()
    {
		Debug.Log( calc_distance());
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
}
