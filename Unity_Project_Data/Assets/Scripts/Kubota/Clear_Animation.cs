using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear_Animation : MonoBehaviour
{
	public RectTransform Maskpos;

	public float speed;
	public float speed_Max;
	public float add_speed;

	//色
	public bool nowColorA;
	public Color colorA;
	public Color colorB;
	public float elapsedTime;         //経過時間
	public float cycleTime;         //周期時間
	public SpriteRenderer spriteRenderer;

	// Start is called before the first frame update
	void Start()
    {
		nowColorA = true;
		elapsedTime = 0.0f;
	}

    // Update is called once per frame
    void Update()
    {
		move_obj();
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= cycleTime)
		{
			if (nowColorA) spriteRenderer.color = colorA;
			else spriteRenderer.color = colorB;
			elapsedTime = 0.0f;
			nowColorA = !nowColorA;
		}

		Add_Speed();
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
}
