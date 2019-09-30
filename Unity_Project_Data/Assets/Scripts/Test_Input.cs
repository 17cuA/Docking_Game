using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Input : MonoBehaviour
{
	private string[] Bottom_Name = new string[24]
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
		"GamePad_2_0",
		"GamePad_2_1",
		"GamePad_2_2",
		"GamePad_2_3",
		"GamePad_2_4",
		"GamePad_2_5",
		"GamePad_2_6",
		"GamePad_2_7",
		"GamePad_2_8",
		"GamePad_2_9",
		"GamePad_2_10",
		"GamePad_2_11",
	};

	private string[] Axis_Name = new string[12]
	{
		"GamePad_1_AxisL_X",
		"GamePad_1_AxisL_Y",
		"GamePad_1_AxisR_X",
		"GamePad_1_AxisR_Y",
		"GamePad_1_POV_X",
		"GamePad_1_POV_Y",
		"GamePad_2_AxisL_X",
		"GamePad_2_AxisL_Y",
		"GamePad_2_AxisR_X",
		"GamePad_2_AxisR_Y",
		"GamePad_2_POV_X",
		"GamePad_2_POV_Y",
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
