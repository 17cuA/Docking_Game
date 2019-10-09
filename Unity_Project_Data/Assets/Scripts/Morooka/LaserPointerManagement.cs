/*
 *　制作：2019/10/09
 *　作者：諸岡勇樹
 *　2019/10/09：レーザーポインターの照射
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointerManagement : MonoBehaviour
{
	[SerializeField,Tooltip("ポインターのオブジェクト")] private GameObject hitLightPlefab;

	private RaycastHit hit;
	private GameObject hitLight;
	private Renderer hitLightRenderer;

	private void Start()
	{
		hitLight = Instantiate(hitLightPlefab, Vector3.zero, Quaternion.identity);
		hitLightRenderer = hitLight.GetComponent<Renderer>();
	}

	void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10.0f))
		{
			if (!hitLightRenderer.enabled)
			{
				hitLightRenderer.enabled = true;
			}

			hitLight.transform.position = hit.point;
			hitLight.transform.forward = hit.normal * -1.0f;
		}
		else
		{
			if(hitLightRenderer.enabled)
			{
				hitLightRenderer.enabled = false;
			}
		}
    }

	void OnDrawGizmos()
	{
		var scale = transform.lossyScale.x * 0.5f;

		var isHit = Physics.Raycast(transform.position, transform.forward, out hit, 10.0f);
		if (isHit)
		{
			Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
		}
		else
		{
			Gizmos.DrawRay(transform.position, transform.forward * 100);
		}
	}
}
