using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LivesCounter : MonoBehaviour 
{
	public ShipManager shipData;
	public Image livesImage;
	public Text livesText;
	
	void Awake () 
	{
		LivesManager m = Directory.Instance.livesManager;
		m.LivesChanged += HandleLivesChanged;
		m.LivesExhausted += HandleLivesExhausted;
		livesText.text = m.Lives.ToString("00");

		ShipData _myShip = shipData.GetShip(PlayerPrefs.GetInt("PreferredShip"));
		livesImage.sprite = _myShip.livesSprite;
	}

	void HandleLivesExhausted ()
	{
		livesText.text = "00";
	}

	void HandleLivesChanged (int numLives)
	{
		livesText.text = numLives.ToString("00");
	}
}
