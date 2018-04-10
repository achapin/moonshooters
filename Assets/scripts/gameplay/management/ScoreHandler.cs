using UnityEngine;
using System.Collections;

public class ScoreHandler : MonoBehaviour
{
	private int _currentScore;
	public int CurrentScore
	{ get { return _currentScore; } }

	public delegate void ScoreChangedHandler(int currentScore);
	public event ScoreChangedHandler ScoreChanged;

	private bool _isTracking;

	void Awake()
	{
		_currentScore = 0;
		Directory.Instance.scoreHandler = this;
		_isTracking = true;
		Directory.Instance.livesManager.LivesExhausted += GameOver;
		Directory.Instance.bossManager.NewBoss += HandleNewBoss;
	}

	void HandleNewBoss (string name, HealthHaver h)
	{
		h.Died += GameOver;
	}

	void GameOver ()
	{
		_isTracking = false;
	}
	
	public void AddScore(int scoreAdd)
	{
		if(!_isTracking) return;
		_currentScore += scoreAdd;
		if(ScoreChanged != null) ScoreChanged(_currentScore);
	}

}
