//──────────────────────────────────────────────
// ファイル名	：CameraShaker.cs
// 概要			：カメラに手振れ補正をつける
// 作成者		：杉山 雅哉
// 作成日		：2019.05.13
// 
//──────────────────────────────────────────────
// 更新履歴：
// 2019/06/17 [杉山 雅哉] 手振れ補正を再現する
//──────────────────────────────────────────────

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
	[SerializeField] Camera camera;// カメラ
	[SerializeField] AcrobaticCamera acrobaticCamera;

	public float Height = 1.5f;
	public float AngleAttenRate = 40.0f;

	public bool EnableAtten = true;
	public float AttenRate = 3.0f;

	public bool EnableNoise = true;
	public float NoiseSpeed = 0.5f;
	public float MoveNoiseSpeed = 1.5f;
	public float NoiseCoeff = 1.3f;
	public float MoveNoiseCoeff = 2.5f;

	public bool EnableFieldOfViewAtten = true;
	public float MoveFieldOfView;

	public float ForwardDistance = 2.0f;

	private Camera cam;
	private Vector3 addForward;
	private Vector3 deltaTarget;
	private Vector3 nowPos;
	private float nowfov;

	private float nowRotAngle;
	private float nowHeightAngle;

	private float FieldOfView = 50.0f;

	private Vector3 prevTargetPos;

	void Start()
	{
		cam = GetComponent<Camera>();
		FieldOfView = camera.fieldOfView;
		nowfov = FieldOfView;
		nowPos = acrobaticCamera.Target;
	}

	void Update()
	{
		var delta = acrobaticCamera.Target - deltaTarget;
		deltaTarget = acrobaticCamera.Target;

		// 減衰
		if (EnableAtten)
		{
			var deltaPos = acrobaticCamera.Target - prevTargetPos;
			prevTargetPos = acrobaticCamera.Target;
			deltaPos *= ForwardDistance;

			addForward += deltaPos * Time.deltaTime * 20.0f;
			addForward = Vector3.Lerp(addForward, Vector3.zero, Time.deltaTime * AttenRate);

			nowPos = Vector3.Lerp(nowPos, acrobaticCamera.Target + Vector3.up * Height + addForward, Mathf.Clamp(Time.deltaTime * AttenRate, 0.0f, 1.0f));
		}
		else nowPos = acrobaticCamera.Target + Vector3.up * Height;

		// 手ブレ
		bool move = Mathf.Abs(delta.x) > 0.0f;
		var noise = new Vector3();
		if (EnableNoise)
		{
			var ns = (move ? MoveNoiseSpeed : NoiseSpeed);
			var nc = (move ? MoveNoiseCoeff : NoiseCoeff);

			var t = Time.time * ns;

			var nx = Mathf.PerlinNoise(t, t) * nc;
			var ny = Mathf.PerlinNoise(t + 10.0f, t + 10.0f) * nc;
			var nz = Mathf.PerlinNoise(t + 20.0f, t + 20.0f) * nc * 0.5f;
			noise = new Vector3(nx, ny, nz);
		}

		// FoV
		if (EnableFieldOfViewAtten) nowfov = Mathf.Lerp(nowfov, move ? MoveFieldOfView : FieldOfView, Time.deltaTime);
		else nowfov = FieldOfView;
		cam.fieldOfView = nowfov;

		// カメラ向き
		var rot = Quaternion.LookRotation((nowPos - transform.position).normalized) * Quaternion.Euler(noise);
		if (EnableAtten) transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * AngleAttenRate);
		else transform.rotation = rot;
	}
}