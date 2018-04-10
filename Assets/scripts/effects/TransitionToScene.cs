using UnityEngine;
using System.Collections;

public class TransitionToScene : MonoBehaviour 
{
	public string sceneName;

	public void Go()
	{
		if(sceneName == null || sceneName == "")
			TransitionManager.Instance.TransitionTo(Application.loadedLevelName);
		else
			TransitionManager.Instance.TransitionTo(sceneName);
	}
}
