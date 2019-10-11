using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_Manager : MonoBehaviour
{

	public Planet_Explosion_Slow explosion;

	public int frame;

	public int cnt;
    void Start()
    {
		cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
		frame++;
        if(frame > 60 && cnt == 0)
		{
			  explosion.StartCoroutine("ExplodePlanet");
			cnt++;
		}
    }
}
