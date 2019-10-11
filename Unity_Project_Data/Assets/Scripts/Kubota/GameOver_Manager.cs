using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver_Manager : MonoBehaviour
{
	public GameObject hal;
	public Planet_Explosion_Slow explosion;
	public GameObject Bicban;
	public GameObject Wave;
	public GameOver_Anime GOA;
	public int frame;
	private int frame_Max;
	public int cnt;

	public Text wireless;
	private string[] wirelessList = new string[4];
	public int wirelesscnt;
	private int num;
	private bool one;
	private bool two;
	void Start()
    {
		wirelessList[0] = "Ｐ「ドッキング失敗、スマフォから高エネルギー反応を感知」";
		wirelessList[1] = "ＨＱ「未知の元素を検出、コアの温度が２０００万度を突破！」";
		wirelessList[2] = "ＨＱ「こ、このままでは・・・」";
		wirelessList[3] = "";
		cnt = 0;
		num = 0;
		one = false;
		two = false;
		//frame = 
		//Serifu
    }

    // Update is called once per frame
    void Update()
    {
		Wireless_Active();
		if (num > 420 && one == false)
		{

			Instantiate(Bicban,new Vector3(-13,-1,-1.8f),new Quaternion(90,0,0,0));
			Instantiate(Bicban, new Vector3(-13, -1, -1.8f), new Quaternion(90, 0, 0, 0));
			Instantiate(Bicban, new Vector3(-13, -1, -1.8f), new Quaternion(90, 0, 0, 0));

			one = true;
		}
		if (num > 720 && two == false)
		{
			Instantiate(Wave, new Vector3(-13, -1, -1.8f), new Quaternion(0, 0, 0, 0));
			two = true;
		}
		
		if(num > 1060)
		{
			SceneManager.LoadScene("Title");
		}
        if(frame > 240 && cnt == 0 && wirelesscnt == 4)
		{
			  explosion.StartCoroutine("ExplodePlanet");
			cnt++;
		}

		//if
		if(Input.anyKey)
		{
			SceneManager.LoadScene("Title");
		}
		num++;
	}
	void Wireless_Display(int i)
	{
		wireless.text = wirelessList[i];
		if(wirelesscnt < wirelessList.Length)
		{
			wirelesscnt++;
		}
	}
	void Wireless_Active()
	{
		if(frame > frame_Max)
		{
			if (wirelesscnt < wirelessList.Length)
			{
				Wireless_Display(wirelesscnt);
			}
			frame = 0;
		}
		switch(wirelesscnt)
		{
			case 0:
				frame_Max = 0;
				break;
			case 1:
				frame_Max = 180;
				break;
			case 2:
				frame_Max = 120;
				break;
			case 3:
				frame_Max = 120;
				
				break;
			default:
				frame_Max = 240;
				break;

		}
		frame++;

	}
}
