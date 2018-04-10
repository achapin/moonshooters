using UnityEngine;
using System;
using System.Collections;

public class SoundPool : GenericPool 
{
	public SoundManager soundData;

	void Start()
	{
		Directory.Instance.soundPool = this;
	}

	public void PlaySound(string name)
	{
		if(name == null || name == "") return;
		GameObject gO = GetActive();
		gO.SetActive(true);
		AudioSource a = gO.GetComponent<AudioSource>();
		try{
		a.clip = soundData.GetSound(name);
		a.Play();
		}catch(Exception e)
		{
			Debug.LogError("TRIED TO PLAY SOUND: " + name + " WHICH DOESN'T EXIST " + e.Message);
		}
	}
}
