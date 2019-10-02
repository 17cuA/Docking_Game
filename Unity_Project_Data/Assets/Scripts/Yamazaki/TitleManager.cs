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

	// 入力待機時間
	[SerializeField, NonEditable]
	private float unavailableTime;		// 経過時間
	public float unavailableTimeMax;    // 最大待機時間

	// フェードアウト情報
	public Image displayPlaneFadeOut;
	// フェードアウト時間
	[SerializeField, NonEditable]
	private float fadeOutTime;      // 経過時間
	public float fadeOutTimeMax;    // 最大時間
	public float blackTimeMax;		// 黒時間

	// Start is called before the first frame update
	void Start()
    {
		gameState = GameState.TITLE;
		titleText.enabled = true;
		subText.enabled = true;
		subText.text = "Please input anykey down";

		selectNum = 0;

		unavailableTime = 0.0f;
		displayPlaneFadeOut.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
	}

    // Update is called once per frame
    void Update()
	{
		// 待機時間経過するまで入力を受け付けない
		if (unavailableTime < unavailableTimeMax) unavailableTime += Time.deltaTime;
		if (unavailableTime < unavailableTimeMax) return;

		switch(gameState)
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
				// フェードアウト時間経過
				fadeOutTime += Time.deltaTime;
				// 最大時間経過した時
				if (fadeOutTime >= fadeOutTimeMax)
				{
					displayPlaneFadeOut.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

					if(fadeOutTime >= fadeOutTimeMax + blackTimeMax)
					{
						// ゲームステージに移動
						SceneManager.LoadScene(stageSceneName);
					}
				}
				else
				{
					displayPlaneFadeOut.color = new Color(0.0f, 0.0f, 0.0f, fadeOutTime / fadeOutTimeMax);
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
				break;

			default:
				break;
		}
	}
}
