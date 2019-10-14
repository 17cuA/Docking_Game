using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active : MonoBehaviour
{
	public CameraWork camera;
	public HUD_Manager hUD_Manager;

	void Start()
    {
		camera = GameObject.Find("Main Camera").GetComponent<CameraWork>();
		hUD_Manager = GetComponent<HUD_Manager>();
		hUD_Manager.enabled = false;
	}

	// Update is called once per frame
	void Update()
    {
        if(camera.isFPS)
		{
			hUD_Manager.enabled = true;
		}
	}
}
