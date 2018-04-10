using UnityEngine;
using System.Collections;

public class LivesManager : MonoBehaviour 
{
	private int _numLivesRemaining;

	public delegate void LivesChangedHandler(int numLives);
	public event LivesChangedHandler LivesChanged;
	public delegate void LivesExhaustedHandler();
	public event LivesExhaustedHandler LivesExhausted;

	void Awake()
	{
		Directory.Instance.livesManager = this;
	}

	public void SetNumLives(int currentNumLives)
	{
		_numLivesRemaining = currentNumLives;
		if(LivesChanged != null) LivesChanged(_numLivesRemaining);
	}

	public void LoseLife()
	{
		_numLivesRemaining -= 1;
		if(_numLivesRemaining <= 0)
		{
			if(LivesExhausted != null) LivesExhausted();
		}else{
			LivesChanged(_numLivesRemaining);
		}
	}

	public int Lives
	{
		get { return _numLivesRemaining; } 
	}
}
