using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpHoleGeneration : MonoBehaviour
{
	//ワープホールオブジェクト
	public GameObject warpHoleGameObject;
	//生成済みワープ
	private GameObject generatedWarpHoleGameObject;

	//生成トリガー
	public bool generatingTrigger;

	//再生時間
	private float playTime = 7.0f;
	//経過時間
	private float elapsedTime;
	//再生中
	private bool nowPlay;


	void Start()
    {
		generatingTrigger = false;
		nowPlay = false;
		elapsedTime = 0.0f;
	}

    void Update()
    {
		if (generatingTrigger && !nowPlay)
		{

			generatedWarpHoleGameObject = (GameObject)Instantiate(warpHoleGameObject, transform.position, transform.rotation);
			generatedWarpHoleGameObject.transform.parent = transform;

			generatingTrigger = false;
			nowPlay = true;
		}

		if (nowPlay)
		{
			elapsedTime += Time.deltaTime;

			if(elapsedTime >= playTime)
			{
				Destroy(generatedWarpHoleGameObject);
				elapsedTime = 0.0f;
				nowPlay = false;
			}

		}
    }

	public void GeneratingProcess()
	{
		if (!generatingTrigger && !nowPlay)
		{
			generatingTrigger = true;
		}
	}
}
