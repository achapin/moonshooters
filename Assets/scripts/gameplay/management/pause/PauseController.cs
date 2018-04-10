using UnityEngine;
using System;
using System.Collections;

public class PauseController : MonoBehaviour {

	public delegate void PauseHandler();
	public event PauseHandler Pause;
	public event PauseHandler Resume;

	void Awake()
	{
		Directory.Instance.pauseController = this;
	}

	public void SetPaused(bool isPaused)
	{
		if(isPaused)
		{
			if(Pause != null)
				Pause();
		}else{
			if(Resume != null)
				Resume();
		}
	}
}
