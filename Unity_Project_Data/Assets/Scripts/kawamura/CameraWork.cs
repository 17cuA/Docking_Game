using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
	public GameObject chargerObj;
	public Vector3 savePos;
	Quaternion _rotation;

	public float defPosZ_Value;
	public float posZ;

	public float rotaX;
	public float rotaY;
	public float rotaX_ChangeValue;
	public float rotaY_ChangeValue;

	//Chargerの位置によって変わるカメラの回転限界
	public float nowRotaX_Max;
	public float nowRotaX_Min;
	public float nowRotaY_Max;
	public float nowRotaY_Min;

	//public float rotaX_Max;
	//public float rotaX_Min;
	//public float rotaY_Max;
	//public float rotaY_Min;

	public bool isMove = false;

	void Start()
	{
		rotaX = 15f;
		rotaY = -20f;

		savePos = chargerObj.transform.position;
	}

	void Update()
	{
		
		if (chargerObj.transform.position != savePos)
		{
			isMove = true;
			savePos = chargerObj.transform.position;
		}
		else if (chargerObj.transform.position == savePos)
		{
			isMove = false;
		}

		if(isMove)
		{
			CameraRotation();
		}

		//transform.rotation=Quaternion.Lerp(transform.rotation,)
		transform.rotation = Quaternion.Euler(rotaX, rotaY, 0);
		posZ = chargerObj.transform.position.z - defPosZ_Value;
		transform.position = new Vector3(transform.position.x, transform.position.y, posZ);
	}

	void CameraRotation()
	{
		//RotationのYを決める
		if (chargerObj.transform.position.x > 0)
		{
			nowRotaY_Max = chargerObj.transform.position.x * 10 - 20;
			if (rotaY < nowRotaY_Max)
			{
				rotaY += rotaY_ChangeValue;
				//if (rotaY > rotaY_Max)
				//{
				//	rotaY = rotaY_Max;
				//}
			}
			if (rotaY > nowRotaY_Max)
			{
				rotaY -= rotaY_ChangeValue;
			}
		}
		else if (chargerObj.transform.position.x < 0)
		{
			nowRotaY_Min = 6.6f * chargerObj.transform.position.x - 20;
			if (rotaY > nowRotaY_Min)
			{
				rotaY -= rotaY_ChangeValue;
				//if (rotaY < rotaY_Min)
				//{
				//	rotaY = rotaY_Min;
				//}
			}
			else if (rotaY < nowRotaY_Min)
			{
				rotaY += rotaY_ChangeValue;
			}
		}
		//RotationのYを決めるやつの終わり

		//RotationのXを決める
		if (chargerObj.transform.position.y < 0)
		{
			nowRotaX_Max = 7.2f * Mathf.Abs(chargerObj.transform.position.y) + 15f;
			if (rotaX < nowRotaX_Max)
			{
				rotaX += rotaX_ChangeValue;
				//if (rotaY > rotaY_Max)
				//{
				//	rotaY = rotaY_Max;
				//}
			}
			if (rotaX > nowRotaX_Max)
			{
				rotaX -= rotaX_ChangeValue;
			}
		}
		else if (chargerObj.transform.position.y > 0)
		{
			nowRotaX_Min = -11.6f * chargerObj.transform.position.y + 15f;
			if (rotaX > nowRotaX_Min)
			{
				rotaX -= rotaX_ChangeValue;
				//if (rotaY < rotaY_Min)
				//{
				//	rotaY = rotaY_Min;
				//}
			}
			else if (rotaX < nowRotaX_Min)
			{
				rotaX += rotaX_ChangeValue;
			}
		}

	}
}
