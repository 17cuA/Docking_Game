using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFlow : MonoBehaviour
{
	float time = 0;
	public bool IsTimeFlow(float timeMax)
	{
		if (time < timeMax)
		{
			time += Time.unscaledDeltaTime;
			return false;
		}
		else
		{
			time = 0;
			return true;
		}
	}
	public bool flowEasingEnd(float endTime, float add = 0)
	{
		if (time <= endTime + add)
		{
			time += Time.unscaledDeltaTime;
			return false;
		}
		else
		{
			time = 0;
			return true;
		}
	}
}
