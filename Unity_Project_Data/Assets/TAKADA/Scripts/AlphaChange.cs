using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaChange : MonoBehaviour
{
	//色
	public bool nowColorA;
	public Color colorA;
	public Color colorB;
	public float elapsedTime;         //経過時間
	public float cycleTime;         //周期時間
	public SpriteRenderer spriteRenderer;

	void Start()
	{
		nowColorA = true;
		elapsedTime = 0.0f;
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= cycleTime)
		{
			if (nowColorA) spriteRenderer.color = colorA;
			else spriteRenderer.color = colorB;
			elapsedTime = 0.0f;
			nowColorA = !nowColorA;
		}
	}
}
