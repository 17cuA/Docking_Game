//作成者：川村良太
//作成日：2019/10/07
//上から見るカメラ視点の位置オブジェクトの位置を決める

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTopPos : MonoBehaviour
{
	[Header("手動で入れよう！スマホ")]
	public GameObject phoneObj;

	bool once = true;
	void Start()
    {
        
    }

    void Update()
    {
        if(once)
		{
			transform.position = new Vector3(phoneObj.transform.position.x, transform.position.y, transform.position.z);
			once = false;
		}
    }
}
