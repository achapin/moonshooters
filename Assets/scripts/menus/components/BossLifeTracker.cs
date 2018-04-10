using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossLifeTracker : MonoBehaviour 
{
	public GameObject bossLifeObject;

	public Text nameText;
	public Image healthBar;

	void Start()
	{
		Directory.Instance.bossManager.NewBoss += HandleNewBoss;
		bossLifeObject.SetActive(false);
	}

	void HandleNewBoss (string bossName, HealthHaver h)
	{
		bossLifeObject.SetActive(true);
		nameText.text = bossName;
		healthBar.fillAmount = 1f;
		h.HealthPct += HandleHealthPct;
		h.Died += HandleDied;;
	}

	void HandleDied ()
	{
		bossLifeObject.SetActive(false);
	}

	void HandleHealthPct (float pct)
	{
		healthBar.fillAmount = pct;
	}
}
