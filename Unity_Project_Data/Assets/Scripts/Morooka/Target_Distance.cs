using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target_Distance : MonoBehaviour
{
	private Text text;
	private GameObject Charger;
	private List<GameObject> NextTarget;

	private void Start()
	{
		text = GetComponent<Text>();
		Charger = GameObject.Find("Charger");

		GameObject temp_1 = GameObject.Find("TargetMather");
		GameObject temp_2 = GameObject.Find("HaL9000");
		NextTarget = new List<GameObject>();

		for(int i = 0;i < temp_1.transform.childCount;i++)
		{
			NextTarget.Add(temp_1.transform.GetChild(i).gameObject);
		}
		NextTarget.Add(temp_2);
	}

	void Update()
    {
        
    }
}
