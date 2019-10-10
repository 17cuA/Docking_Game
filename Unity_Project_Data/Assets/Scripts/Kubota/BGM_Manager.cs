using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Manager : MonoBehaviour
{
	[Header("呼吸音を鳴らしている")]
	public AudioSource Breathing;
	[Header("無線を鳴らしてる関係")]
	public AudioSource Wireless;
	public AudioClip Wireless_Clip;
    // Start is called before the first frame update
    void Start()
    {
		Breathing.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	//void Wireless
}
