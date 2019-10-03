using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
	[SerializeField] private GameObject charger;
	[SerializeField] private GameObject Hal9000;
    void Start()
    {
		transform.position = charger.transform.position;
		transform.rotation = Quaternion.LookRotation(Hal9000.transform.position);
	}

	private void LateUpdate()
	{
		transform.position = charger.transform.position;
		transform.rotation = Quaternion.LookRotation(Hal9000.transform.position);
	}
}
