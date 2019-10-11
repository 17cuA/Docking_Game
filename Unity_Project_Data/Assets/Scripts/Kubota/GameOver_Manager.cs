using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOver_Manager : MonoBehaviour
{

	public Planet_Explosion_Slow explosion;

	public int frame;

	public int cnt;

	private Text wierless;
	private string[] wirelessList = new string[3];

	void Start()
    {
		wirelessList[0] = "Ｐ「ドッキング失敗、スマフォから高エネルギー反応を感知」";
		wirelessList[1] = "ＨＱ「未知の元素を検出、コアの温度が２０００万度を突破！」";
		wirelessList[2] = "ＨＱ「こ、このままでは・・・」";
		cnt = 0;
		//Serifu
    }

    // Update is called once per frame
    void Update()
    {
		frame++;
        if(frame > 60 && cnt == 0)
		{
			  explosion.StartCoroutine("ExplodePlanet");
			cnt++;
		}
    }
}
