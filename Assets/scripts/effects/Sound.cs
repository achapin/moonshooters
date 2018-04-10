using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class Sound : MonoBehaviour {

	private AudioSource _mySource;

	void Start()
	{
		_mySource = GetComponent<AudioSource>();
	}

	void Update () 
	{
		if(!_mySource.isPlaying) gameObject.SetActive(false);
	}
}
