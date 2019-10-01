//------------------------------------------------------------
//汎用particle管理
//
//再生 : ParticleReproducing()
//停止 : ParticleStop()
//時間で停止 : playTimeに数値を入れる、playTime=0.0fでループ
//------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour
{
	//ParticleSystem
	private ParticleSystem particleSystemComponent;

	//制限時間で止めるか
	private bool playTimeStop = false;
	//状態
	private bool nowPlay;
	//particleの再生時間
	public float playTime = 0.0f;
	//経過時間
	private float elapsedTime;

	void Start()
	{
		//コンポーネント取得
		particleSystemComponent = this.GetComponent<ParticleSystem>();

		//制限時間の可否
		if (playTime != 0.0f)
		{
			playTimeStop = true;
			elapsedTime = 0.0f;
			nowPlay = true;
		}
	}

	void Update()
	{

		//制限時間で止める
		if (playTimeStop) { PlayTimeParticleStop(); }

		/*test
		if (Input.GetKeyDown(KeyCode.A)) { ParticleReproducing(); }
		if (Input.GetKeyDown(KeyCode.S)) { ParticleStop(); }
		*/
	}

	//再生
	public void ParticleReproducing()
	{
		particleSystemComponent.Play(true);

		if (playTimeStop)
		{
			nowPlay = true;
		}
	}

	//停止
	public void ParticleStop()
	{
		particleSystemComponent.Stop(true, ParticleSystemStopBehavior.StopEmitting);

		if (playTimeStop)
		{
			nowPlay = false;
			elapsedTime = 0.0f;
		}
	}

	//時間停止
	public void PlayTimeParticleStop()
	{
		if (nowPlay)
		{
			elapsedTime += Time.deltaTime;

			if (playTime < elapsedTime)
			{
				particleSystemComponent.Stop(true, ParticleSystemStopBehavior.StopEmitting);
				elapsedTime = 0.0f;
				nowPlay = false;
			}
		}
	}
}
