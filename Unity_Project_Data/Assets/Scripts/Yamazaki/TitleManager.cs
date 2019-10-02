using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
	public enum GameState
	{
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
	// ステージシーン名
	public string stageSceneName;

	// 入力受付の有無
	private bool isActive = false;

	// フェードスクリプトをいれよう
	public FadeTime fadeTimeScript;

	// Start is called before the first frame update
	void Start()
    {
		gameState = GameState.TITLE;
		titleText.enabled = true;
		subText.enabled = true;
		subText.text = "Please input anykey down";

		selectNum = 0;

		isActive = false;

		if (fadeTimeScript) fadeTimeScript.SetFadeType(FadeTime.FadeType.FADEIN);
	}

    // Update is called once per frame
    void Update()
	{
		if(!isActive)
		{
			if (fadeTimeScript)
			{
				if (fadeTimeScript.IsFadeInFinished())
				{
					isActive = true;
				}
				else return;
			}
			else
			{
				isActive = true;
			}
		}

		switch (gameState)
		{
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

		switch(selectNum)
		{
			case 0:
				subText.text = "↑↓ゲームスタート";
				break;

			case 1:
				subText.text = "↑↓もどる";
				break;

			default:
				break;
		}
	}

	private void SetGameState(GameState g)
	{
		gameState = g;

		switch (g)
		{
			case GameState.TITLE:
				subText.text = "Please input anykey down";
				break;

			case GameState.MENU:
				selectNum = 0;
				subText.text = "↑↓ゲームスタート";
				break;

			case GameState.GAMESTART:
				fadeTimeScript.SetFadeType(FadeTime.FadeType.FADEOUT);
				break;

			default:
				break;
		}
	}
}
