// 20191011

using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearManager : MonoBehaviour
{
	public enum SceneMode
	{
		eNONE,
		eFADEIN,
		eFADEOUT,
	}

	public SceneMode sceneMode;

	public FadeTime fadeTimeScr;
	public WirelessManager wirelessManagerScr;

	public float movieTime;
	private float movieTimeMax = 25.0f;

    // Start is called before the first frame update
    void Start()
    {
		SetSceneMode(SceneMode.eFADEIN);
    }

	public void SetSceneMode(SceneMode s)
	{
		switch(s)
		{
			case SceneMode.eFADEIN:
				wirelessManagerScr.SetWirelessMode(WirelessManager.WirelessMode.STAGECLEAR_1);
				fadeTimeScr.SetFadeType(FadeTime.FadeType.FADEIN);
				break;

			case SceneMode.eFADEOUT:
				fadeTimeScr.SetFadeType(FadeTime.FadeType.FADEOUT);
				break;

			default:
				break;
		}

		sceneMode = s;
	}

    // Update is called once per frame
    void Update()
    {
        switch(sceneMode)
		{
			case SceneMode.eFADEIN:
				movieTime += Time.deltaTime;
				if (movieTime >= movieTimeMax)
				{
					SetSceneMode(SceneMode.eFADEOUT);
				}
				break;

			case SceneMode.eFADEOUT:
				if(fadeTimeScr.IsFadeOutFinished())
				{
					SceneManager.LoadScene("Title");
				}
				break;

			default:
				break;
		}
    }
}
