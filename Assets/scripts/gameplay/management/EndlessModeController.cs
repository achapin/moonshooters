using UnityEngine;
using System.Collections;

public class EndlessModeController : MonoBehaviour 
{
	public int livesToStartWith = 3;

	public Formation[] formationsToUse;
	public float timeBetweenFormations = .5f;
	public int formationDestructionBonus;

	private Formation _currentFormation;
	private float _timeToLaunchFormation;

	public GameObject player;
	public ScrollingStarfield starfield;

	private bool _gameOver;

	void Start () 
	{
		Application.LoadLevelAdditive("gamehud");
		Directory.Instance.livesManager.SetNumLives(livesToStartWith);
		_timeToLaunchFormation = timeBetweenFormations;
		Directory.Instance.livesManager.LivesExhausted += HandleLivesExhausted;
		_gameOver = false;
	}

	void HandleLivesExhausted ()
	{
		_gameOver = true;
		StartCoroutine(SlowStarField());
		player.SetActive(false);
	}

	private IEnumerator SlowStarField()
	{
		for(int pct = 0; pct < 100; pct++)
		{
			starfield.SetScollSpeed(1f - ((float)pct / 100f));
			yield return new WaitForEndOfFrame();
		}
		starfield.SetScollSpeed(0f);
	}

	void Update()
	{
		if(_gameOver) return;
		if(_timeToLaunchFormation > 0f)
		{
			_timeToLaunchFormation -= Time.deltaTime;
			if(_timeToLaunchFormation <= 0f)
			{
				_currentFormation = formationsToUse[Random.Range(0, formationsToUse.Length)];
				_currentFormation.FormationDestroyed += HandleFormationDestroyed;
				_currentFormation.FormationRetreated += HandleFormationRetreated;
				_currentFormation.Launch();
			}
		}
	}

	void HandleFormationRetreated ()
	{
		FormationFinished();
	}

	void HandleFormationDestroyed (Vector3 lastPos)
	{
		if(_currentFormation.totalEnemies > 1) 
			Directory.Instance.scoreHandler.AddScore(formationDestructionBonus);
		FormationFinished();
	}

	private void FormationFinished()
	{
		_currentFormation.FormationDestroyed -= HandleFormationDestroyed;
		_currentFormation.FormationRetreated -= HandleFormationRetreated;
		_timeToLaunchFormation = timeBetweenFormations;
	}
}
