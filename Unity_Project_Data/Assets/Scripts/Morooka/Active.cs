using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active : MonoBehaviour
{
	public CameraWork camera;
	public HUD_Manager hUD_Manager;
	private bool flag;
	void Start()
    {
		camera = GameObject.Find("Main Camera").GetComponent<CameraWork>();
		hUD_Manager = GetComponent<HUD_Manager>();
		hUD_Manager.enabled = false;
		for(int i = 0; i < transform.childCount;i++)
		{
			transform.GetChild(i).gameObject.SetActive(false);
			flag = false;
		}
	}

	// Update is called once per frame
	void Update()
    {
		if (!flag)
		{
			if (camera.isFPS)
			{
				hUD_Manager.enabled = true;

				for (int i = 0; i < transform.childCount; i++)
				{
					transform.GetChild(i).gameObject.SetActive(true);
				}
				flag = true;
			}
		}
		else if(flag)
		{
			if(GameMaster.instance.stageState == GameMaster.StageState.FADEOUT)
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					transform.GetChild(i).gameObject.SetActive(false);
				}
				flag = false;
			}
		}
	}
}
