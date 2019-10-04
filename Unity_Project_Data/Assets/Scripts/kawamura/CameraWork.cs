using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
	public enum CameraState
	{
		Backward,
		FPS,
	}

	public CameraState cameraState;

	public GameObject chargerObj;
	//public GameObject backwardCameraPos;
	public GameObject FPS_CameraPosObj;
	public Vector3 backwardCameraPos;
	public Vector3 savePos;
	public Quaternion _rotation;

	public float rotaSpeed;

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

	public float nowRotaX_Limit;
	public float nowRotaY_Limit;

	//public float rotaX_Max;
	//public float rotaX_Min;
	//public float rotaY_Max;
	//public float rotaY_Min;

	bool once = true;
	public bool isMove = false;

	void Start()
	{
		rotaX = 15f;
		rotaY = -20f;

		nowRotaX_Limit = rotaX;
		nowRotaY_Limit = rotaY;
		//_rotation = transform.rotation;

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

		_rotation = Quaternion.Euler(nowRotaX_Limit, nowRotaY_Limit, 0);
		posZ = chargerObj.transform.position.z - defPosZ_Value;
		backwardCameraPos = new Vector3(transform.position.x, transform.position.y, posZ);
		//transform.position = new Vector3(transform.position.x, transform.position.y, posZ);

		if (chargerObj.transform.position.x > -0.1f && chargerObj.transform.position.x < 0.1f
			&& chargerObj.transform.position.y > -0.1f && chargerObj.transform.position.y < 0.1f && chargerObj.transform.position.z > -5.0f)
		{
			cameraState = CameraState.FPS;
		}
		else
		{
			cameraState = CameraState.Backward;
		}

		switch(cameraState)
		{
			case CameraState.Backward:
				transform.position = backwardCameraPos;
				transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, 1);

				break;

			case CameraState.FPS:
				transform.position = FPS_CameraPosObj.transform.position;
				transform.rotation = Quaternion.Euler(0, 0, 0);
				break;
		}
	}

	void CameraRotation()
	{
		//RotationのYを決める
		if (chargerObj.transform.position.x > 0)
		{
			//nowRotaY_Max = chargerObj.transform.position.x * 10 - 20;
			nowRotaY_Limit = chargerObj.transform.position.x * 10 - 20;
			if (rotaY < nowRotaY_Limit)
			{
				rotaY += rotaY_ChangeValue;
				//if (rotaY > rotaY_Max)
				//{
				//	rotaY = rotaY_Max;
				//}
			}
			if (rotaY > nowRotaY_Limit)
			{
				rotaY -= rotaY_ChangeValue;
			}

			//if (rotaY < nowRotaY_Max)
			//{
			//	rotaY += rotaY_ChangeValue;
			//	//if (rotaY > rotaY_Max)
			//	//{
			//	//	rotaY = rotaY_Max;
			//	//}
			//}
			//if (rotaY > nowRotaY_Max)
			//{
			//	rotaY -= rotaY_ChangeValue;
			//}
		}
		else if (chargerObj.transform.position.x < 0)
		{
			nowRotaY_Limit = 6.6f * chargerObj.transform.position.x - 20;
			if (rotaY > nowRotaY_Limit)
			{
				rotaY -= rotaY_ChangeValue;
			}
			else if (rotaY < nowRotaY_Limit)
			{
				rotaY += rotaY_ChangeValue;
			}
		}
		//RotationのYを決めるやつの終わり

		//RotationのXを決める
		if (chargerObj.transform.position.y < 0)
		{
			nowRotaX_Limit = 7.2f * Mathf.Abs(chargerObj.transform.position.y) + 15f;
			if (rotaX < nowRotaX_Limit)
			{
				rotaX += rotaX_ChangeValue;
			}
			if (rotaX > nowRotaX_Limit)
			{
				rotaX -= rotaX_ChangeValue;
			}
		}
		else if (chargerObj.transform.position.y > 0)
		{
			nowRotaX_Limit = -11.6f * chargerObj.transform.position.y + 15f;
			if (rotaX > nowRotaX_Limit)
			{
				rotaX -= rotaX_ChangeValue;
			}
			else if (rotaX < nowRotaX_Limit)
			{
				rotaX += rotaX_ChangeValue;
			}
		}
		//RotationのXを決めるやつおわり
	}
}
