using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Manager : MonoBehaviour
{

	[ColorUsage(false, true)]
	public Color[] color1;

	//[ColorUsage(false, true)]
	//public Color color2;


	private int Color_Cnt;
	private bool Is_Down_color;
	public GameObject[] prism;
	public MeshRenderer[] r;
	int frame;
	public int frame_Max;
	[Header("何番目の明るさを使用するのか")]
	public int Color_Number;
	public float Min_Decrease;
	// Start is called before the first frame update
	void Start()
	{
		r = new MeshRenderer[prism.Length];
		for (int i = 0; i < prism.Length; i++)
		{
			//prism[i] = gameObject.transform.GetChild(i).gameObject;		//
			r[i] = prism[i].GetComponent<MeshRenderer>();
			r[i].material.EnableKeyword("_EMISSION");		//Emissionを変更する際に必要なもの
			//color1[i].in
		}
		Color_Cnt = Color_Number;
		Is_Down_color = true;
	}

    // Update is called once per frame
    void Update()
    {
		Light_Change();
    }
	
	void Light_Change()
	{
		frame++;

		//child_Material.color = color1[Color_Cnt];
		for (int i = 0; i < r.Length; i++)
		{
			r[i].material.SetColor("_EmissionColor", color1[Color_Cnt]);
		}
		//color2 = color1[Color_Cnt];
		if (frame > frame_Max)
		{
			if (Color_Cnt == 0)
			{
				Is_Down_color = true;
			}
			else if (Color_Cnt == color1.Length - 1)
			{
				Is_Down_color = false;
			}

			if (Is_Down_color)
			{
				Color_Cnt++;
			}
			else
			{
				Color_Cnt--;
			}


			frame = 0;

		}
	}
}
