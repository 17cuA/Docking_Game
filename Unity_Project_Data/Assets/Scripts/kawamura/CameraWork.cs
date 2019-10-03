using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
	public GameObject chargerObj;
	public float defPosZ_Value;
	public float posZ;

	public float rotaX_Max;
	public float rotaX_Min;
	public float rotaY_Max;
	public float rotaY_Min;

	void Start()
	{

	}

	void Update()
	{
		posZ = chargerObj.transform.position.z - defPosZ_Value;
		transform.position = new Vector3(transform.position.x, transform.position.y, posZ);
	}
}
