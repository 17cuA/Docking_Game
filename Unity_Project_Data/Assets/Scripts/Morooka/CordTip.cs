using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CordTip : MonoBehaviour
{
	[SerializeField, Tooltip("afsd")] private Rigidbody rigidbody;
	[SerializeField, Tooltip("先端")] private GameObject charger;

    private void Update()
    {
		Vector3 distance = transform.position - charger.transform.position;
		if (distance.magnitude > 1.94f * 2.0f)
		{
			Vector3 temp = Vector2.MoveTowards(transform.position, charger.transform.position, 0.01f);
			temp.z = transform.position.z + (1.94f * Mathf.Sin(distance.z));
			transform.position = temp;
		}
	}
}
