using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear_Animation : MonoBehaviour
{
	public RectTransform Maskpos;
	public GameObject mask;
	public float speed;
	public float speed_Max;
	public float add_speed;

	private float nowtime;
	public float activetime;
	public float OffTime;
	//色
	public Color colorA;
	public Color colorB;
	public SpriteRenderer spriteRenderer;

	[Range(0.0f,1.0f)]
	public float addcolortime;
	public float add_time;
	// Start is called before the first frame update
	void Start()
    {
		spriteRenderer.color = colorA;
	}

    // Update is called once per frame
    void Update()
    {
		//Change_Color();

		nowtime += Time.deltaTime;
		//if (elapsedTime >= cycleTime)
		//{
		//	if (nowColorA) spriteRenderer.color = colorA;
		//	else spriteRenderer.color = colorB;
		//	elapsedTime = 0.0f;
		//	nowColorA = !nowColorA;
		//}

		if(nowtime > activetime)
		{
			move_obj();
			Add_Speed();
			Change_Color();

		}

		if (nowtime > OffTime)
		{
			mask.SetActive(false);
		}
	}

	void move_obj()
	{
		Maskpos.position = new Vector3(Maskpos.position.x, Maskpos.position.y, Maskpos.position.z - speed);
	}
	void Add_Speed()
	{
		if(speed < speed_Max)
		{
			speed += add_speed;
		}
	}

	void Change_Color()
	{
		addcolortime += add_time;
		if(addcolortime < 1.0f)
		{
			spriteRenderer.color = Color.Lerp(colorA, colorB, addcolortime);
		}
	}
}
