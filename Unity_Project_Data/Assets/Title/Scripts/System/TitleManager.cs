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

	public void Defalt()					{ Debug.LogError("処理がありません"); }
	public void OPCameraRoll(bool unFade)	{ acrobaticCamera.CameraUpdate(unFade); }
	public void ScriptActivate(MonoBehaviour script) { script.enabled = true; }
	public void ScriptUnActivate(MonoBehaviour script) { script.enabled = false; }
	public void TransitionNextScene()		{ SceneManager.LoadScene(nextSceneName); }
	public void Fadein()					{ StartCoroutine(fadeEditor.FadeinCol()); }
	public void Fadeout()					{ StartCoroutine(fadeEditor.FadeoutCol()); }
	public bool IsTimeFlow(float time)		{ return timeFlow.IsTimeFlow(time); }
	public bool IsTimeCurveFlow(AnimationCurve_One anim) { return timeFlow.IsTimeFlow(anim.TimeMax); }
	public bool IsInput()					{ return Original_Input.ButtomFront_Hold; }
	public bool IsInput_Debug()				{ return Input.GetKey(KeyCode.Z); }
}