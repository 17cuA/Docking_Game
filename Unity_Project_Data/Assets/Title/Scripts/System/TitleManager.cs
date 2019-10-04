using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CUEngine.Pattern;

public class TitleManager : StateBaseScriptMonoBehaviour
{
	[SerializeField] AcrobaticCamera acrobaticCamera;
	[SerializeField] TimeFlow timeFlow;

	public void OPCameraRoll()			{ acrobaticCamera.CameraUpdate(); }
	public bool IsTimeFlow(float time)	{ return timeFlow.IsTimeFlow(time); }
	public bool IsInput()				{ return Input.GetKeyDown(KeyCode.Z); }
}