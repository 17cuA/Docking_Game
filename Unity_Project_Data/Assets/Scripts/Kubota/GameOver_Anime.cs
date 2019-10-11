using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_Anime : MonoBehaviour
{
	//色
	public bool nowColorA;
	public Color colorA;
	public Color colorB;
	public float elapsedTime;         //経過時間
	public float cycleTime;         //周期時間
	private SpriteRenderer spriteRenderer;
	public SpriteRenderer sprite_Frame;
	float alpha_value;  //アルファの値
	int frame;
	public int frame_Max;
	int change_cnt;
	//public int 
	//[Header("フェードアウトするまでの時間")]
	//public float FadeOut_Time;
	private float currentRemainTime;
	//private SpriteRenderer spRenderer;

	//private float 
	void Start()
	{
		nowColorA = true;
		elapsedTime = 0.0f;
		spriteRenderer = GetComponent<SpriteRenderer>();
		change_cnt = 0;
		alpha_value = 255.0f;
		currentRemainTime = 2.0f;
	}

	void Update()
	{
		if (change_cnt < 5)
		{
			Sprite_Flashing();
		}
		else
		{
			Sprite_Fade_Out();
		}
	}

	void Sprite_Flashing()
	{
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= cycleTime)
		{
			if (nowColorA) spriteRenderer.color = colorA;
			else spriteRenderer.color = colorB;
			elapsedTime = 0.0f;
			nowColorA = !nowColorA;
			change_cnt++;
		}
	}
	
	void Sprite_Fade_Out()
	{
		currentRemainTime -= Time.deltaTime;
		if (currentRemainTime <= 0f)
		{
			// 残り時間が無くなったら自分自身を消滅
			//GameObject.Destroy(gameObject);
			return;
		}
		//frame++;
		alpha_value--;

		var spriteRenderer_color = spriteRenderer.color;
		var sprite_Frame_color = sprite_Frame.color;

		alpha_value = currentRemainTime / 1.0f;

		spriteRenderer_color.a = alpha_value;
		sprite_Frame_color.a = alpha_value;

		spriteRenderer.color = spriteRenderer_color;
		sprite_Frame.color = sprite_Frame_color;

	}
	public bool Is_Finshed()
	{
		if(alpha_value <= 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
