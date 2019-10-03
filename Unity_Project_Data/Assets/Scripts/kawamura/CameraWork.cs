using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
	public GameObject chargerObj;
	public float posZ;

	void Start()
	{

	}

	void Update()
	{
		posZ = chargerObj.transform.position.z - 2.2f;
		transform.position = new Vector3(transform.position.x, transform.position.y, posZ);
	}
}
