using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndImage : MonoBehaviour
{
	public Sprite[] Sentence;
	public int Index;

	public float displayTime;
	private float elapsedTime;

	private Image image;
	private void Start()
	{
		image = GetComponent<Image>();
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

		image.sprite = Sentence[Index];
	}
}
