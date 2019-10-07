using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLeftPos : MonoBehaviour
{
	[Header("手動で入れよう！スマホ")]
	public GameObject phoneObj;

	bool once = true;
	void Start()
	{

	}

	void Update()
	{
		if (once)
		{
			transform.position = new Vector3(transform.position.x, phoneObj.transform.position.y, phoneObj.transform.position.z - 2f);
			once = false;
		}
	}
}
