using UnityEngine;
using System.Collections;

[RequireComponent (typeof(MoveToCenterOnClick))]
public class GameOverMenu : MonoBehaviour 
{

	void Start () 
	{
		Directory.Instance.livesManager.LivesExhausted += GameOver;
		Directory.Instance.bossManager.NewBoss += HandleNewBoss;
	}

	void HandleNewBoss (string name, HealthHaver h)
	{
		h.Died += GameOver;
	}

	void GameOver ()
	{
		GetComponent<MoveToCenterOnClick>().Center();
	}
}
