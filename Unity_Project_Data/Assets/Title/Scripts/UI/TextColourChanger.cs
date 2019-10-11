using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColourChanger : MonoBehaviour
{
	Text text;
	AnimationCurve_One anim;
    void Start()
    {
        text = GetComponent<Text>();
		anim = GetComponent<AnimationCurve_One>();
	}

	public IEnumerator ChangeTextColour()
	{
		float time = 0;
		while (true)
		{
			while (time < anim.TimeMax)
			{
				time += Time.deltaTime;
				text.color = new Color(text.color.r,text.color.g,text.color.b,anim.Evaluate(time));
				yield return null;
			}
			time = 0;
		}
	}
}