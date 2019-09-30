using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Input : MonoBehaviour
{
	private string[] Bottom_Name = new string[12]
	{
		"GamePad_1_0",
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
	};

	private string[] Axis_Name = new string[7]
	{
		"GamePad_1_Axis_1",
		"GamePad_1_Axis_2",
		"GamePad_1_Axis_3",
		"GamePad_1_Axis_4",
		"GamePad_1_Axis_5",
		"GamePad_1_Axis_6",
		"GamePad_1_Axis_7",
	};

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var name in Bottom_Name)
		{
			if(Input.GetButtonDown(name))
			{
				Debug.Log(name);
			}
		}
        foreach(var name in Axis_Name)
		{
			var num = Input.GetAxis(name);
			if (num > 0.0f)
			{
				Debug.Log(name + ": +");
			}
			else if (num < 0.0f)
			{
				Debug.Log(name + ": -");
			}
		}
    }
}
