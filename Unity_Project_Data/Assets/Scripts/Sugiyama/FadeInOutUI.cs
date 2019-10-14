using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutUI : MonoBehaviour
{
	[Header("最大表示時間")]
	[SerializeField] float waitTimeMax;
	Image image;
	EasingEditor_Simple easing;
	float time = 0;
	bool isCalled;
	void Start()
	{
		easing = GetComponent<EasingEditor_Simple>();
		image = GetComponent<Image>();
		image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
	}

	void Update()
	{
		if(gameObject.active == true)
			if(isCalled == false)
				StartCoroutine(Fadeinout());
	}

	IEnumerator Fadeinout()
	{
		print(1);
		isCalled = true;
		while (time < easing.GetEndkey(0).time)
		{
			image.color = new Color(image.color.r, image.color.g, image.color.b, easing.Anims[0].Evaluate(time));
			time += Time.deltaTime;
			yield return null;
		}
		image.color = new Color(image.color.r, image.color.g, image.color.b, easing.GetEndkey(0).value);
		time = 0;
		yield return new WaitForSeconds(waitTimeMax);
		while (time < easing.GetEndkey(1).time)
		{
			image.color = new Color(image.color.r, image.color.g, image.color.b, 1 - easing.Anims[1].Evaluate(time));
			time += Time.deltaTime;
			yield return null;
		}
		image.color = new Color(image.color.r, image.color.g, image.color.b, 1 - easing.GetEndkey(1).value);
	}
}
