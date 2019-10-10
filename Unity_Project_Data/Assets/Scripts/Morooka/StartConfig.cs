using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DockingGame_Input;
using UnityEngine.SceneManagement;

public class StartConfig : MonoBehaviour
{
	public Text text;
	public Original_Input _Input;
    void Start()
    {
		text = GetComponent<Text>();
	}

    // Update is called once per frame
    void Update()
    {
		text.text = "左コントローラーのボタンを押してください。\n" + "右コントローラーは残りのコントローラーは自動で割り振られます。\n";
		if(_Input.SetStick())
		{
			SceneManager.LoadScene("Title");
		}
	}
}
