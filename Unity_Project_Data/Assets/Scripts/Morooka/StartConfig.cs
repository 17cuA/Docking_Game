using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DockingGame_Input;

public class StartConfig : MonoBehaviour
{
	public Text text;
	public Original_Input _Input;
	public bool leftFlag;
	public bool RightFlag;
    void Start()
    {
		text = GetComponent<Text>();
		leftFlag = RightFlag = false;

	}

    // Update is called once per frame
    void Update()
    {
		text.text = "Press the trigger of the stick you want to use. \n";
		if(!leftFlag)
		{
			
		}

	}
}
