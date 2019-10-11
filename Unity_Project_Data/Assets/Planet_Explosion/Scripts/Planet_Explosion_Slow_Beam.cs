using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Explosion_Slow_Beam : MonoBehaviour
{

    public GameObject planetSeparateFX;
    public GameObject planetExplodeFX;
    public GameObject planetExplodeAnim;
    public GameObject glowSphere;
    public GameObject planetAtmos;

    public GameObject orbitalBeamLaser;
    public GameObject laserEffects;
    public ParticleSystem laserSparks;
    public GameObject laserChargeBeam;
    public GameObject smokeAndSparks;
    public AudioSource laserChargeAudio;
    public AudioSource laserAudio;
    public AudioSource laserStopAudio;

    public GameObject cameraShake;


    // Use this for initialization
    void Start()
    {

        planetExplodeFX.SetActive(false);
        planetSeparateFX.SetActive(false);
        orbitalBeamLaser.SetActive(false);
        laserEffects.SetActive(false);
        laserChargeBeam.SetActive(false);
        smokeAndSparks.SetActive(false);
        laserChargeAudio.Stop();
        laserAudio.Stop();
        laserStopAudio.Stop();
        planetAtmos.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {

            StartCoroutine("ExplodePlanet");

        } 

    }


    IEnumerator ExplodePlanet()
    {

        orbitalBeamLaser.SetActive(true);
        laserChargeAudio.Play();
        laserChargeBeam.SetActive(true);

        yield return new WaitForSeconds(1.4f);
   
        laserEffects.SetActive(true);
        smokeAndSparks.SetActive(true);
        laserSparks.Play();
        laserAudio.Play();
        planetSeparateFX.SetActive(true);
        planetExplodeAnim.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(4.5f);

        cameraShake.GetComponent<Animation>().Play();
        planetAtmos.SetActive(false);
        glowSphere.SetActive(false);
        planetExplodeFX.SetActive(true);
        orbitalBeamLaser.SetActive(false);

        yield return new WaitForSeconds(6.0f);

        planetExplodeFX.SetActive(false);
        planetSeparateFX.SetActive(false);
        orbitalBeamLaser.SetActive(false);

    }


}
