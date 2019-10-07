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
	private GameObject[] transforms = new GameObject[2];
	private MeshRenderer[] r = new MeshRenderer[2];
	int frame;
	public int frame_Max;
	// Start is called before the first frame update
	void Start()
	{
		for (int i = 0; i < 2; i++)
		{
			transforms[i] = gameObject.transform.GetChild(i).gameObject;
			r[i] = transforms[i].GetComponent<MeshRenderer>();
			r[i].material.EnableKeyword("_EMISSION");		//Emissionを変更する際に必要なもの
		}
		Color_Cnt = 0;
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
		for (int i = 0; i < 2; i++)
		{
			r[i].material.SetColor("_EmissionColor", color1[Color_Cnt]);
		}
		//color2 = color1[Color_Cnt];
		if (frame > frame_Max)
		{
			Debug.Log("hei");
			if (Color_Cnt == 0)
			{
				Is_Down_color = true;
			}
			else if (Color_Cnt == 4)
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
