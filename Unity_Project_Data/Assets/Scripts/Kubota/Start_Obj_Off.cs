using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Obj_Off : MonoBehaviour
{
	[Header("ゲーム開始時のアニメーションで見えなくしたいものを入れる")]
	[SerializeField]private GameObject[] obj;

	public float NowTime;			//今の時間
	public float DisplayTime;           //画面に表示するまでの時間

	private int cnt;				//画面内に表示をした時にカウント
    void Start()
    {
		for(int i = 0; i < obj.Length; i++)
		{
			obj[i].SetActive(false);
		}
		NowTime = 0.0f;
		cnt = 0;
    }


    void Update()
    {
		NowTime += Time.deltaTime;
		//既定の時間を越したら
		if (NowTime > DisplayTime)
		{
			Debug.Log("来てるよ");
			if (cnt < obj.Length )
			{
				obj[cnt].SetActive(true);
				cnt++;
			}

			//設定してあるものすべてがオフになるように
			for (int i = 0; i > obj.Length; i++)
			{
				obj[i].SetActive(true);
				cnt++;
			}
		}
		if (cnt == obj.Length)
		{
			//無駄な処理をしないように稼働を止める
			gameObject.SetActive(false);
		}
	}
}
