using UnityEngine;
using System.Collections;

public class StoryModeController : MonoBehaviour {

	public int livesToStartWith = 3;
	public ObjectScroller scroller;
	public GameObject player;

	void Start () 
	{
		Application.LoadLevelAdditive("gamehud");
		Directory.Instance.livesManager.SetNumLives(livesToStartWith);
		Directory.Instance.livesManager.LivesExhausted += HandleLivesExhausted;
		scroller.SetSpeed(-.5f);

	}
	
	void HandleLivesExhausted ()
	{
		player.SetActive(false);
	}
}
