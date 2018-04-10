using UnityEngine;
using System.Collections;

public class RectTransformMoveInOut : MonoBehaviour 
{

	public Vector2 positionOffset;
	public float lerpTime;
	public float initialRestTime;

	private RectTransform _myRT;
	private Vector2 _initialPos;

	void OnEnable()
	{
		_myRT = GetComponent<RectTransform> ();
		_initialPos = _myRT.anchoredPosition;
		StartCoroutine (MoveIn ());
	}

	void OnDisable()
	{
		if(_myRT == null) _myRT = GetComponent<RectTransform> ();
		else _myRT.anchoredPosition = _initialPos;
		StopAllCoroutines ();
	}

	private IEnumerator MoveIn()
	{
		_myRT.anchoredPosition = _initialPos + positionOffset;
		yield return new WaitForSeconds (initialRestTime);
		float totalTime = 0f;
		while (totalTime < lerpTime) 
		{
			totalTime += Time.deltaTime;
			_myRT.anchoredPosition = Vector2.Lerp(_initialPos + positionOffset, _initialPos, totalTime / lerpTime);
			yield return new WaitForEndOfFrame();
		}
		_myRT.anchoredPosition = _initialPos;
	}
}
