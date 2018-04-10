using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(RectTransform))]
[RequireComponent (typeof(Image))]
public class RectTransformImageAlpha : MonoBehaviour 
{

	public float distanceToClear;

	private static Color whiteClear = new Color(1f,1f,1f,0f);
	private RectTransform _myRectTransform;
	private Image _myImage;
	private Vector2 _lastPos;

	void Start()
	{
		_myRectTransform = GetComponent<RectTransform>();
		_myImage = GetComponent<Image>();
		_lastPos = _myRectTransform.anchoredPosition;
	}

	void Update () 
	{
		if(_myRectTransform.anchoredPosition == _lastPos) return;
		if(_myRectTransform.anchoredPosition == Vector2.zero)_myImage.color = Color.white;
		else _myImage.color = Color.Lerp(Color.white, whiteClear, _myRectTransform.anchoredPosition.magnitude / distanceToClear);
		_lastPos = _myRectTransform.anchoredPosition;
	}
}
