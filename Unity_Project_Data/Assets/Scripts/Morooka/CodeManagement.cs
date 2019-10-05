using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeManagement : MonoBehaviour
{
	[SerializeField, Tooltip("充電器本体")] private GameObject charger;
	[SerializeField, Tooltip("コードのターゲット")] private GameObject codeTarget;
	[SerializeField, Tooltip("充電器本体のスクリプト")] private Charger_Manager ChargerScript;

	private List<Transform> CodesTransforms { get; set; }
	private List<bool> IsACertainDistance { get; set; }

	private float CodesLocalPositionInterval = -0.046f;

	void Start()
    {
		// コードの設定
		List<Transform> tempTransform = new List<Transform>();
		CodesTransforms = new List<Transform>();
		IsACertainDistance = new List<bool>();

		// 子供のコードのTransform取り出し
		for(int i = 0; i < transform.childCount; i++)
		{
			tempTransform.Add(transform.GetChild(i));
		}
		
		// 子供のPositionなどの設定
		float zPos = 0.0f;
		for (int i = 0; i < tempTransform.Count; i++)
		{
			// ポジション設定
			Vector3 tempPos = tempTransform[i].transform.localPosition;
			tempPos.z = zPos;
			tempTransform[i].transform.localPosition = tempPos;
			// 一緒に使うBoolの設定
			IsACertainDistance.Add(false);
			//次の位置設定
			zPos += CodesLocalPositionInterval;
		}
		CodesTransforms.Add(charger.transform);
		CodesTransforms.AddRange(tempTransform);
	}

    void Update()
    {
		// 移動するのか確認
        for(int i = 1; i < CodesTransforms.Count;i++)
		{
			// 一定の位置にいる判定のとき
			if(!IsACertainDistance[i-1])
			{
				Vector2 vector2 = (CodesTransforms[i-1].transform.position - CodesTransforms[i].transform.position);
				if(vector2.magnitude > 0.05)
				{
					IsACertainDistance[i - 1] = true;
				}
			}
		}

		//移動処理
		for(int i = 0;i < IsACertainDistance.Count; i++)
		{
			if(IsACertainDistance[i])
			{
				float temp = CodesTransforms[i + 1].transform.position.z;

				CodesTransforms[i + 1].transform.position 
					= new Vector3( Mathf.MoveTowards( CodesTransforms[i + 1].transform.position.x, CodesTransforms[i].transform.position.x, ChargerScript.NowSpeed / 50.0f),
					Mathf.MoveTowards(CodesTransforms[i + 1].transform.position.y, CodesTransforms[i].transform.position.y, ChargerScript.NowSpeed / 50.0f),
					CodesTransforms[i + 1].transform.position.z);

				if((Vector2)CodesTransforms[i + 1].transform.position == (Vector2)CodesTransforms[i].transform.position)
				{
					IsACertainDistance[i] = false;
				}
			}
		}
    }
	private void LateUpdate()
	{
		Vector3 temp = transform.position;
		temp.z = codeTarget.transform.position.z;
		transform.position = temp;
	}
}
