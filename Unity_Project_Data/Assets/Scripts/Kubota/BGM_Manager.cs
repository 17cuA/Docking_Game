using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Manager : MonoBehaviour
{
	public static BGM_Manager BGM_obj;

	[Header("呼吸音を鳴らしているもの")]
	public AudioSource Breathing;
	[Header("無線を鳴らすもの")]
	public AudioSource Wireless;
	public AudioClip Wireless_AudioClip;
	[Header("ドッキングに使うもの")]
	public AudioSource Docking;
	public AudioClip Docking_Audioclip;

	void Awake()
	{
		BGM_obj = GetComponent<BGM_Manager>();
	}

	// Start is called before the first frame update
	void Start()
    {
		Breathing.Play();
    }
	/// <summary>
	/// 無線を鳴らす際に使用する
	/// </summary>
	void Active_Wireless()
	{
		Wireless.PlayOneShot(Wireless_AudioClip);
	}
	/// <summary>
	/// ドッキング時に鳴らすもの
	/// </summary>
	void Sound_Docking()
	{
		Docking.PlayOneShot(Docking_Audioclip);
	}
}
