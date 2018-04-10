using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransitionManager : MonoBehaviour 
{
	public Image transitionImage;
	public float transitionTime;
	private Color clearWhite = new Color(1f,1f,1f,0f);
	private bool levelLoaded = false;

	private static TransitionManager _instance;
	public static TransitionManager Instance
	{
		get { return _instance; }
	}

	void Awake()
	{
		if(_instance != null && _instance != this)
		{
			Destroy(gameObject);
			return;
		}
		_instance = this;
		DontDestroyOnLoad(gameObject);
		transitionImage.gameObject.SetActive(false);
	}

	public void TransitionTo(string newScene)
	{
		StartCoroutine(Transition(newScene));
	}

	private IEnumerator Transition(string newScene)
	{
		transitionImage.gameObject.SetActive(true);
		transitionImage.color = clearWhite;
		float fadeTime = 0f;
		while(fadeTime < transitionTime)
		{
			fadeTime += Time.deltaTime;
			transitionImage.color = Color.Lerp(clearWhite, Color.white, fadeTime / transitionTime);
			yield return new WaitForEndOfFrame();
		}
		transitionImage.color = Color.white;
		Application.LoadLevelAsync(newScene);
		levelLoaded = false;
		while(!levelLoaded) yield return new WaitForEndOfFrame();
		fadeTime = 0f;
		while(fadeTime < transitionTime)
		{
			fadeTime += Time.deltaTime;
			transitionImage.color = Color.Lerp(Color.white, clearWhite, fadeTime / transitionTime);
			yield return new WaitForEndOfFrame();
		}
		transitionImage.gameObject.SetActive(false);
	}

	void OnLevelWasLoaded(int level)
	{
		levelLoaded = true;
	}
}
