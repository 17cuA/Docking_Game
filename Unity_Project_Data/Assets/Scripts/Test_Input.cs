using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Input : MonoBehaviour
{
	private string[] Input_Name = new string[12]
	{
		"GamePad_1_1",
		"GamePad_1_2",
		"GamePad_1_3",
		"GamePad_1_4",
		"GamePad_1_5",
		"GamePad_1_6",
		"GamePad_1_7",
		"GamePad_1_8",
		"GamePad_1_9",
		"GamePad_1_10",
		"GamePad_1_11",
		"GamePad_1_12",
	};

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var name in Input_Name)
		{
			if(Input.GetButtonDown(name))
			{
				Debug.Log(name);
			}
		}
    }
}
