using UnityEngine;
using System.Collections;

public class HighscoreBoardManager : MonoBehaviour 
{
	public int placesToTrack = 10;
	public string boardName;

	void Start () 
	{
		Directory.Instance.livesManager.LivesExhausted += HandleGameOver;
		Directory.Instance.bossManager.NewBoss += HandleNewBoss;
	}

	void HandleNewBoss (string name, HealthHaver h)
	{
		h.Died += HandleGameOver;
	}
	
	private void HandleGameOver()
	{
		int score = Directory.Instance.scoreHandler.CurrentScore;
		for(int index = 0; index < placesToTrack; index++)
		{
			if(!PlayerPrefs.HasKey(boardName + "_" + index) ||
			   PlayerPrefs.GetInt(boardName + "_" + index) < score)
			{
				for(int replacementIndex = placesToTrack - 2; replacementIndex >= index; replacementIndex--)
				{
					if(PlayerPrefs.HasKey(boardName + "_" + replacementIndex))
					{
						PlayerPrefs.SetInt(boardName + "_" + (replacementIndex + 1), PlayerPrefs.GetInt(boardName + "_" + replacementIndex));
					}
				}
				PlayerPrefs.SetInt(boardName + "_" + index, score);
				return;
			}
		}
	}
}
