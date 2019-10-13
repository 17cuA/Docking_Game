using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingSound : MonoBehaviour

    
{
    [SerializeField, NonEditable, Tooltip("経過時間")]
    private float dockingSoundDelay;
    public float dockingSoundDelayMax;

    public AudioSource dockingAudioSource;
    public AudioClip dockingAudioClip;

    [SerializeField, NonEditable, Tooltip("再生済み")]
    private bool isAudioPlayed;

    private void Awake()
    {
        dockingSoundDelay = 0.0f;

        if (GetComponent<AudioSource>())
        {
            dockingAudioSource = GetComponent<AudioSource>();
        }
        else
        {
            dockingAudioSource = gameObject.AddComponent<AudioSource>();
        }

        dockingAudioSource.playOnAwake = false;
        if (dockingAudioClip)
        {
            dockingAudioSource.clip = dockingAudioClip;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isAudioPlayed = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAudioPlayed)
        {
            dockingSoundDelay += Time.deltaTime;

            if (dockingSoundDelay >= dockingSoundDelayMax)
            {
                dockingAudioSource.PlayOneShot(dockingAudioClip);

                isAudioPlayed = true;
            }
        }
        
    }
}
