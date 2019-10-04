// 20191004
// ゲームデータ保存
using UnityEngine;

public class GameData : MonoBehaviour
{
	public static GameData gameData;

	public enum GameMode
	{
		EASY,
		HARD,
	}

	[SerializeField, NonEditable]
	private GameMode gameMode = GameMode.EASY;
	[SerializeField, NonEditable]
	private int score = 0;

	public void SetScore(int i)
	{
		score = i;
	}

	public void AddScore(int i)
	{
		score += i;
	}

	public int GetScore()
	{
		return score;
	}

	public void SetGameMode(GameMode g)
	{
		gameMode = g;
	}

	public GameMode GetGameMode()
	{
		return gameMode;
	}

	private void Awake()
	{
		SetGameMode(GameMode.EASY);
		DontDestroyOnLoad(this);
	}
}
