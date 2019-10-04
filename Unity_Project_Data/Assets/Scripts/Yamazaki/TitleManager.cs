using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
	public enum GameState
	{
		FADEIN,
		TITLE,
		MENU,
		GAMESTART,
	}

	public GameState gameState;
	public Text titleText;
	public Text subText;

	public int selectNum;

	private GameState[] menuList = new GameState[2]
	{
		GameState.GAMESTART,
		GameState.TITLE,
	};
	
	private string[] menuStr = new string[2]
	{
		"▼ Game Start\nBack",
		"Game Start\n▲ Back",
	};

	// ステージシーン名
	public string stageSceneName;

	// フェードスクリプトをいれよう
	public FadeTime fadeTimeScript;

	// Start is called before the first frame update
	void Start()
    {
		if (fadeTimeScript)
		{
			gameState = GameState.FADEIN;
			fadeTimeScript.SetFadeType(FadeTime.FadeType.FADEIN);
		}
		else
		{
			gameState = GameState.TITLE;
		}
		titleText.enabled = true;
		subText.enabled = true;
		subText.text = "Please press a Key";

		selectNum = 0;

		if (fadeTimeScript) fadeTimeScript.SetFadeType(FadeTime.FadeType.FADEIN);
	}

    // Update is called once per frame
    void Update()
	{
		switch (gameState)
		{
			case GameState.FADEIN:
				if (fadeTimeScript)
				{
					if (fadeTimeScript.IsFadeInFinished())
					{
						SetGameState(GameState.TITLE);
						break;
					}
					if (fadeTimeScript.GetFadeType() != FadeTime.FadeType.FADEIN)
					{
						fadeTimeScript.SetFadeType(FadeTime.FadeType.FADEIN);
					}
				}
				else
				{
					SetGameState(GameState.TITLE);
				}
				break;

			case GameState.TITLE:
				if (Input.anyKeyDown)
				{
					SetGameState(GameState.MENU);
				}
				break;

			case GameState.MENU:
				if (Input.GetKeyDown(KeyCode.DownArrow))
				{
					AddSelectNum(1);
				}
				else if (Input.GetKeyDown(KeyCode.UpArrow))
				{
					AddSelectNum(-1);
				}
				else if (Input.anyKeyDown)
				{
					SetGameState(menuList[selectNum]);
				}
				break;

			case GameState.GAMESTART:
				// ゲームステージに移動
				if (fadeTimeScript)
				{
					if(fadeTimeScript.IsFadeOutFinished())
					{
						SceneManager.LoadScene(stageSceneName);
					}
				}
				else
				{
					SceneManager.LoadScene(stageSceneName);
				}
				break;

			default:
				break;
		}
    }

	private void AddSelectNum(int i)
	{
		selectNum += i;
		if (selectNum < 0)
		{
			selectNum = menuList.Length - 1;
		}
		else if (selectNum >= menuList.Length)
		{
			selectNum = 0;
		}

		subText.text = menuStr[selectNum];
	}

	private void SetGameState(GameState g)
	{
		gameState = g;

		switch (g)
		{
			case GameState.TITLE:
				subText.text = "Please press a Key";
				break;

			case GameState.MENU:
				selectNum = 0;
				subText.text = menuStr[0];
				break;

			case GameState.GAMESTART:
				fadeTimeScript.SetFadeType(FadeTime.FadeType.FADEOUT);
				break;

			default:
				break;
		}
	}
}
