using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root_Light_Manager : MonoBehaviour
{
	[SerializeField] GameObject Hal9000;
	[SerializeField] GameObject Chager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void calc_distance()
	{
		Vector3 a = Hal9000.transform.position;
		Vector3 b = Hal9000.transform.position;

		float distance = Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2) + Mathf.Pow(a.z - b.z, 2));
	}
}
