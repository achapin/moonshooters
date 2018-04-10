using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{

	public AudioClip[] clipList;

	private AudioSource _mySource;

	void Start()
	{
		DontDestroyOnLoad(gameObject);
		_mySource = GetComponent<AudioSource>();
		PlayNextClip();
	}

	void Update () 
	{
		if(!_mySource.isPlaying)PlayNextClip();
	}

	private void PlayNextClip()
	{
		_mySource.clip = clipList[Random.Range(0, clipList.Length)];
		_mySource.Play();
	}
}
