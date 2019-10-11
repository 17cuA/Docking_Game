using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class END_Text : MonoBehaviour
{
	public string[] Sentence;
	public int Index;

	public float displayTime;
	private float elapsedTime;

	private Text text;
	private void Start()
	{
		text = GetComponent<Text>();
		elapsedTime = 0.0f;
		Index = 0;
	}

	void Update()
    {
		elapsedTime += Time.deltaTime;
		if (elapsedTime > displayTime)
		{
			if (Index < Sentence.Length - 1)
			{
				Index++;
			}

			elapsedTime = 0.0f;
		}

		text.text = Sentence[Index];
	}
}
