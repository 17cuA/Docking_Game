using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTime : MonoBehaviour
{
	// フェードイン情報
	public Image displayFadeInPlane;
	// フェードイン時間
	[SerializeField, NonEditable]
	private float fadeInTime = 0.0f;      // 経過時間
	public float fadeInTimeMax = 2.0f;    // 最大待機時間

	// フェードアウト情報
	public Image displayFadeOutPlane;
	// フェードアウト時間
	[SerializeField, NonEditable]
	private float fadeOutTime = 0.0f;      // 経過時間
	public float fadeOutTimeMax = 2.98f;    // 最大時間
	public float blackTimeMax = 0.02f;      // 黒時間

	public enum FadeType
	{
		NONE,
		FADEIN,
		FADEOUT,
	}

	private FadeType fadeType;
	private bool isFadeInFinished = false;
	private bool isFadeOutFinished = false;

	// Start is called before the first frame update
	void Start()
	{
		fadeType = FadeType.NONE;
		fadeInTime = 0.0f;
		fadeOutTime = 0.0f;
		SetDisplayFadeInPlane(new Color(0.0f, 0.0f, 0.0f, 0.0f));
		SetDisplayFadeOutPlane(new Color(0.0f, 0.0f, 0.0f, 0.0f));
		isFadeInFinished = false;
		isFadeOutFinished = false;
	}

	private void SetDisplayFadeInPlane(Color c)
	{
		if (displayFadeInPlane)
		{
			displayFadeInPlane.color = c;
		}
	}

	private void SetDisplayFadeOutPlane(Color c)
	{
		if(displayFadeOutPlane)
		{
			displayFadeOutPlane.color = c;
		}
	}

	public void SetFadeType(FadeType f)
	{
		fadeType = f;
		switch(f)
		{
			case FadeType.FADEIN:
				isFadeInFinished = false;
				isFadeOutFinished = false;
				fadeInTime = 0.0f;
				fadeOutTime = 0.0f;
				break;

			case FadeType.FADEOUT:
				isFadeInFinished = false;
				isFadeOutFinished = false;
				fadeInTime = 0.0f;
				fadeOutTime = 0.0f;
				break;

			default:
				break;
		}
	}

	public bool IsFadeInFinished()
	{
		return isFadeInFinished;
	}

	public bool IsFadeOutFinished()
	{
		return isFadeOutFinished;
	}

	// Update is called once per frame
	void Update()
	{
		switch (fadeType)
		{
			case FadeType.FADEIN:
				// フェードイン時間経過
				fadeInTime += Time.deltaTime;
				// 最大時間経過した時
				if (fadeInTime >= fadeInTimeMax)
				{
					SetDisplayFadeInPlane(new Color(0.0f, 0.0f, 0.0f, 1.0f));

					if (fadeInTime >= fadeInTimeMax + blackTimeMax)
					{
						SetFadeType(FadeType.NONE);
						isFadeInFinished = true;
					}
				}
				else
				{
					SetDisplayFadeInPlane(new Color(0.0f, 0.0f, 0.0f, fadeInTime / fadeInTimeMax));
				}
				break;

			case FadeType.FADEOUT:
				// フェードアウト時間経過
				fadeOutTime += Time.deltaTime;
				// 最大時間経過した時
				if (fadeOutTime >= fadeOutTimeMax)
				{
					SetDisplayFadeOutPlane(new Color(0.0f, 0.0f, 0.0f, 1.0f));

					if (fadeOutTime >= fadeOutTimeMax + blackTimeMax)
					{
						SetFadeType(FadeType.NONE);
						isFadeOutFinished = true;
					}
				}
				else
				{
					SetDisplayFadeOutPlane(new Color(0.0f, 0.0f, 0.0f, fadeOutTime / fadeOutTimeMax));
				}
				break;

			default:
				break;
		}
	}
}
