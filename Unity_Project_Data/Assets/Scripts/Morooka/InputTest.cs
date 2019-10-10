using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
	public string[] moji;


    // Update is called once per frame
    void Update()
    {
        foreach(var _input_ in moji)
		{
			float f = Input.GetAxis(_input_);
			if (Input.GetAxis(_input_) != 0.0f)
			{
				Debug.Log(_input_ + f);
			}
		}
    }
}
