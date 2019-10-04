using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEditor : MonoBehaviour
{
	[SerializeField] Image panel;
	EasingEditor_Simple easing;

	public bool IsFading { get; set; }
	public float FadeinTimeMax { get { return easing.Anims[0].keys[easing.Anims[0].keys.Length - 1].time; } }
	public float FadeoutTimeMax { get { return easing.Anims[1].keys[easing.Anims[1].keys.Length - 1].time; } }


	private void Awake()
	{
		easing = GetComponent<EasingEditor_Simple>();
		panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 1);
	}

	public void Fadein(float time)
	{
		panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 1 - easing.Anims[0].Evaluate(time));
	}

	public void Fadeout(float time)
	{
		panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, easing.Anims[1].Evaluate(time));
	}

	public IEnumerator FadeCap(float timeMax)
	{
		//panel.color = Color.black;
		float time = 0;
		while (time < timeMax)
		{
			panel.color = Color.clear;
			//if (time < FadeinTimeMax)
			//	panel.color = new Color(0, 0, 0, 1 - easing.Anims[0].Evaluate(time));
			//else if (time > timeMax - FadeoutTimeMax)
			//	panel.color = new Color(0, 0, 0, easing.Anims[1].Evaluate(time - (timeMax - FadeoutTimeMax)));
			//else
			//	panel.color = Color.clear;
			time += Time.deltaTime;
			yield return null;
		}
		//panel.color = Color.black;
	}

	public void FadeClear()
	{
		panel.color = Color.clear;
	}

	public IEnumerator FadeinCol()
	{
		IsFading = true;
		float time = searchValueTime(easing.Anims[0],panel.color.a);
		while (time < FadeinTimeMax)
		{
			panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 1 - easing.Anims[0].Evaluate(time));
			time += Time.deltaTime;
			yield return null;
		}
		panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0);
		IsFading = false;
	}

	public IEnumerator FadeoutCol()
	{
		IsFading = true;
		float time = searchValueTime(easing.Anims[1], panel.color.a);
		while (time < FadeoutTimeMax)
		{
			panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, easing.Anims[1].Evaluate(time));
			time += Time.deltaTime;
			yield return null;
		}
		panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 1);
		IsFading = false;
	}

	float searchValueTime(AnimationCurve curve, float value)
	{
		if (value <= 0) return 0;
		if (value >= 1) return 0;
		float time = 0;
		while (true)
		{
			if (curve.Evaluate(time) > value)
				break;
			time += 0.1f;
		}
		while (true)
		{
			if (curve.Evaluate(time - 0.01f) < value)
				break;
			time -= 0.01f;
		}
		return time;
	}
	public void ColorChange(Color color)
	{
		panel.color = color;
	}
}
