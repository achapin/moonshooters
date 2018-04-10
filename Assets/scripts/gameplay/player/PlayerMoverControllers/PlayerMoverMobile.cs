using UnityEngine;
using System.Collections;

public class PlayerMoverMobile : PlayerMoverController
{
	public float deadZone = .1f;

	private float _currentValue = 0f;

	void Update () 
	{
		_currentValue = 0f;
		for(int touchIndex = 0; touchIndex < Input.touchCount; touchIndex++)
		{
			Touch t = Input.GetTouch(touchIndex);
			if(t.phase == TouchPhase.Canceled || t.phase == TouchPhase.Ended) continue;
			float screenPos = t.position.x / Screen.width;
			if(screenPos <= .5f - deadZone) _currentValue -= 1f;
			else if(screenPos >= .5f + deadZone) _currentValue += 1f;
		}
	}

	public override float HorizontalMotion()
	{
		return _currentValue;
	}

	public override bool ForCurrentPlatform()
	{
#if UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID
		return true;
#elif UNITY_STANDALONE
		return false;
#endif
	}
}
