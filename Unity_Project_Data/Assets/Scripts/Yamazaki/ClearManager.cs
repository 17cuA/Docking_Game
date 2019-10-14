// 20191011

using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearManager : MonoBehaviour
{
	public enum SceneMode
	{
		eNONE,
		eFADEIN,
		eWIRELESS,
		eFADEOUT,
        eJUMPTITLE,
	}

	public SceneMode sceneMode;

	public FadeTime fadeTimeScr;
	public WirelessManager wirelessManagerScr;

	public float movieTime;
	public float movieTimeMax = 23.0f;

	public float startDelayTimeMax = 3.0f;

    public bool isFadeOut;

    private void Awake()
    {
        isFadeOut = false;
    }

    // Start is called before the first frame update
    void Start()
	{
		SetSceneMode(SceneMode.eFADEIN);
	}

	public void SetSceneMode(SceneMode s)
	{
		switch (s)
		{
			case SceneMode.eFADEIN:
				fadeTimeScr.SetFadeType(FadeTime.FadeType.FADEIN);
				break;

			case SceneMode.eWIRELESS:
				wirelessManagerScr.SetWirelessMode(WirelessManager.WirelessMode.STAGECLEAR_1);
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
		switch (sceneMode)
		{
			case SceneMode.eFADEIN:
				movieTime += Time.deltaTime;
				if (movieTime >= startDelayTimeMax)
				{
					SetSceneMode(SceneMode.eWIRELESS);
				}
				break;

			case SceneMode.eWIRELESS:
                movieTime += Time.deltaTime;
                if (movieTime >= movieTimeMax)
                {
                    SetSceneMode(SceneMode.eFADEOUT);
                }
                break;

			case SceneMode.eFADEOUT:
				if (fadeTimeScr.IsFadeOutFinished())
				{
					SceneManager.LoadScene("Title");
				}
				break;

            case SceneMode.eJUMPTITLE:
                SceneManager.LoadScene("Title");
                break;

			default:
				break;
		}

        if(isFadeOut)
        {
            SetSceneMode(SceneMode.eFADEOUT);
        }
	}
}
