using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpHoleBehavior : MonoBehaviour
{
	//マテリアル
	private Material material;

	//再生時間
	public float playTime;
	//経過時間
	private float elapsedTime;
	//再生番号
	public int reproducingNum;
	//再生変更タイミング
	public float[] reproducingChangeTiming = new float[2] { 1.5f, 3.0f };

	//最大サイズ
	public float maxSize;

	//ライトオブジェクト
	public GameObject lightObj;

	void Start()
	{
		reproducingNum = 0;//再生番号の初期化
		elapsedTime = 0.0f; //経過時間の初期化
		material = GetComponent<MeshRenderer>().material;//コンポーネントからマテリアルを取得、保存
		transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);   //スケールを初期化
	}

	void Update()
	{
		elapsedTime += Time.deltaTime;  //経過時間の加算
		if (reproducingNum == 0)
		{
			transform.localScale = new Vector3(
				transform.localScale.x + maxSize / (reproducingChangeTiming[0] * 60.0f),
				transform.localScale.y + maxSize / (reproducingChangeTiming[0] * 60.0f),
				transform.localScale.z + maxSize / (reproducingChangeTiming[0] * 60.0f)
			);

			//時間経過処理
			if (elapsedTime > reproducingChangeTiming[reproducingNum])
			{
				reproducingNum++;
			}
		}
		if (reproducingNum == 1)
		{
			//時間経過処理
			if (elapsedTime > reproducingChangeTiming[reproducingNum])
			{
				reproducingNum++;
				lightObj.SetActive(true);
			}
		}
		if (reproducingNum == 2)
		{

			material.color = new Color(
				material.color.r,
				material.color.g,
				material.color.b,
				material.color.a - 1.0f / ((playTime - reproducingChangeTiming[1]) * 60.0f)
			);


			//時間経過処理
			if (elapsedTime > playTime)
			{
				elapsedTime = 0.0f;
				gameObject.SetActive(false);
			}
		}
	}
}
