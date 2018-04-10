using UnityEngine;
using System.Collections;

public class PauseTrigger : MonoBehaviour {
	
	private PauseController _pauseController;

	void Awake()
	{
		_pauseController = Directory.Instance.pauseController;
	}

	public void Pause()
	{
		_pauseController.SetPaused(true);
	}

	public void Resume()
	{
		_pauseController.SetPaused(false);
	}
}
