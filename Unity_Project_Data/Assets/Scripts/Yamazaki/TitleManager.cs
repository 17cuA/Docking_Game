// 作成者：17CU0334 山嵜ジョニー
// 作成日：2019/10/02
// 概要：タイトル
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
		EASYMODE,
		HARDMODE,
	}

	public GameState gameState;
	public Text titleText;
	public Text subText;

	public int selectNum;

	private GameState[] menuList = new GameState[3]
	{
		GameState.EASYMODE,
		GameState.HARDMODE,
		GameState.TITLE,
	};
	
	private string[] menuStr = new string[3]
	{
		"Easy Mode\nHard Mode\nBack",
		"Easy Mode\nHard Mode\nBack",
		"Easy Mode\nHard Mode\nBack",
	};

	public Image rightArrowImg;

	private Vector3[] rightArrowRectPos = new Vector3[3]
	{
		new Vector3(-249.0f, -156.0f, 0.0f),
		new Vector3(-249.0f, -156.0f-64.0f, 0.0f),
		new Vector3(-249.0f, -156.0f-64.0f-64.0f, 0.0f),
	};

	// ステージシーン名
	public string stageSceneName;

	// フェードスクリプトをいれよう
	public FadeTime fadeTimeScript;

	// ゲームデータプレハブ
	private GameObject gameDataPrefab;

	// 開幕前
	private void Awake()
	{
		if (!GameObject.Find("GameData"))
		{
			gameDataPrefab = Resources.Load("Prefabs/GameData") as GameObject;
			GameObject g = Instantiate(gameDataPrefab, Vector3.zero, transform.rotation);
			g.name = "GameData";
		}
	}

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
		subText.text = "Please press a key";
		rightArrowImg.enabled = false;

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

			case GameState.EASYMODE:
			case GameState.HARDMODE:
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
		rightArrowImg.rectTransform.anchoredPosition = rightArrowRectPos[selectNum];
	}

	private void SetGameState(GameState g)
	{
		gameState = g;

		switch (g)
		{
			case GameState.TITLE:
				subText.text = "Please press a key";
				rightArrowImg.enabled = false;
				break;

			case GameState.MENU:
				selectNum = 0;
				subText.text = menuStr[0];
				rightArrowImg.rectTransform.anchoredPosition = rightArrowRectPos[0];
				rightArrowImg.enabled = true;
				break;

			case GameState.EASYMODE:
				GameData.gameData.SetGameMode(GameData.GameMode.EASY);
				fadeTimeScript.SetFadeType(FadeTime.FadeType.FADEOUT);
				break;

			case GameState.HARDMODE:
				GameData.gameData.SetGameMode(GameData.GameMode.HARD);
				fadeTimeScript.SetFadeType(FadeTime.FadeType.FADEOUT);
				break;

			default:
				break;
		}
	}
}
