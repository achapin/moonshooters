using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Text))]
public class ScoreTracker : MonoBehaviour 
{
	public string leader = "SCORE: ";
	public int numDigits;
	public float ptsPerSecond = 5;

	private Text _myText;
	private string format;
	private int _lastNumDigits = -1;
	private float _displayScore = 0;
	private float _targetScore = 0;

	void Start()
	{
		_myText = GetComponent<Text>();
		Directory.Instance.scoreHandler.ScoreChanged += HandleScoreChanged;
		_displayScore = Directory.Instance.scoreHandler.CurrentScore;
		_targetScore = Directory.Instance.scoreHandler.CurrentScore;
		UpdateFormat();
		_myText.text = leader + _displayScore.ToString(format);
	}

	void HandleScoreChanged (int currentScore)
	{
		_targetScore = (float)currentScore;
	}

	private void UpdateFormat()
	{
		format = "";
		_lastNumDigits = numDigits;
		for(int digit = 0; digit < numDigits; digit++)
		{
			format = format + "0";
		}
	}

	void Update()
	{
		if(_lastNumDigits != numDigits)
		{
			UpdateFormat();
		}

		if(_displayScore != _targetScore)
		{
			_displayScore += ptsPerSecond * Time.deltaTime;
			if(_displayScore > _targetScore) _displayScore = _targetScore;
			_myText.text = leader + _displayScore.ToString(format);
		}
	}
}
