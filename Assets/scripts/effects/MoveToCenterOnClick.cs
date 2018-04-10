using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class MoveToCenterOnClick : MonoBehaviour 
{
	public RectTransform toCenter;
	public RectTransform[] allElements;
	public float transitionTime;

	public EventSystem system;
	public GameObject defaultObj;

	private List<Vector2> sourcePositions = new List<Vector2>();
	private List<Vector2> targetPositions = new List<Vector2>();
	
	public void Center()
	{
		StartCoroutine(PerformCentering());
	}

	private IEnumerator PerformCentering()
	{
		if(system != null && defaultObj != null)
		{
			system.SetSelectedGameObject(defaultObj);
		}

		for(int elementIndex = 0; elementIndex < allElements.Length; elementIndex++)
		{
			sourcePositions.Add(allElements[elementIndex].anchoredPosition);
			targetPositions.Add(allElements[elementIndex].anchoredPosition - toCenter.anchoredPosition);
		}

		float currentTime = 0f;
		while(currentTime < transitionTime)
		{
			for(int elementIndex = 0; elementIndex < allElements.Length; elementIndex++)
			{
				allElements[elementIndex].anchoredPosition = Vector2.Lerp(sourcePositions[elementIndex], targetPositions[elementIndex], currentTime / transitionTime);
			}
			currentTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		for(int elementIndex = 0; elementIndex < allElements.Length; elementIndex++)
		{
			allElements[elementIndex].anchoredPosition = targetPositions[elementIndex];
		}
		sourcePositions.Clear();
		targetPositions.Clear();
	}
}
