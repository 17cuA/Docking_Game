using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class END_Text : MonoBehaviour
{
	public string[] Sentence;
	public int Index;

	private Text text;
	private void Start()
	{
		text = GetComponent<Text>();
	}

	void Update()
    {
        if(Index < Sentence.Length)
		{
			text.text = Sentence[Index];
		}
    }
}
