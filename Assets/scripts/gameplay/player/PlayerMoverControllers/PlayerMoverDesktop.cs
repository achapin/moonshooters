using UnityEngine;
using System.Collections;

public class PlayerMoverDesktop : PlayerMoverController
{

	public float mouseTimeOut = .3f;
	public float mouseDeadZone = .1f;

	private float _currentValue = 0f;
	private float _lastHeardMouse = 0f;
	
	void Update () 
	{
		_currentValue = 0f;
		if(Mathf.Abs(Input.GetAxis("HorizontalMouse")) > .1f)
		{
			_lastHeardMouse = Time.timeSinceLevelLoad;
		}

		if(_lastHeardMouse + mouseTimeOut >= Time.timeSinceLevelLoad)
		{
			float mouseXPos = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
			float shipXPos = Camera.main.WorldToViewportPoint(transform.position).x;
			if(mouseXPos + mouseDeadZone < shipXPos) _currentValue = -1f;
			if(mouseXPos - mouseDeadZone > shipXPos) _currentValue = 1f;
		}

		_currentValue = Mathf.Clamp(Input.GetAxis("Horizontal") + _currentValue, -1f, 1f);
	}
	
	public override float HorizontalMotion()
	{
		return _currentValue;
	}
	
	public override bool ForCurrentPlatform()
	{
		#if UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID
		return false;
		#elif UNITY_STANDALONE
		return true;
		#endif
	}
}
