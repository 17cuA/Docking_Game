using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlush : MonoBehaviour
{
	//再生時間
	public float playTime;
	//経過時間
	private float elapsedTime;

	//ライト
	private Light light;
	//最大光量
	public float maxLightIntensity;

	// Start is called before the first frame update
	void Start()
    {
		elapsedTime = 0.0f;
		light = GetComponent<Light>();

		light.intensity = maxLightIntensity;
	}

    // Update is called once per frame
    void Update()
    {
		elapsedTime += Time.deltaTime;

		//光量の変化
		light.intensity = maxLightIntensity - (maxLightIntensity * (elapsedTime / playTime));

	}
}
