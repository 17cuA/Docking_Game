using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CUEngine.Pattern;
using DockingGame_Input;
using UnityEngine.SceneManagement;

public class TitleManager : StateBaseScriptMonoBehaviour
{
	[SerializeField] AcrobaticCamera acrobaticCamera;
	[SerializeField] string nextSceneName;
	[SerializeField] FadeEditor fadeEditor;
	[SerializeField] TimeFlow timeFlow;

	public void Defalt()				{ Debug.LogError("処理がありません"); }
	public void OPCameraRoll()			{ acrobaticCamera.CameraUpdate(); }
	public void TransitionNextScene()	{ SceneManager.LoadScene(nextSceneName); }
	public void Fadein()				{ StartCoroutine(fadeEditor.FadeinCol()); }
	public void Fadeout()				{ StartCoroutine(fadeEditor.FadeoutCol()); }
	public bool IsTimeFlow(float time)	{ return timeFlow.IsTimeFlow(time); }
	public bool IsTimeCurveFlow(AnimationCurve_One anim) { return timeFlow.IsTimeFlow(anim.TimeMax); }
	public bool IsInput()				{ return X_Input.ButtomB_Down; }
	public bool IsInput_Debug()			{ return Input.GetKey(KeyCode.Z); }
}