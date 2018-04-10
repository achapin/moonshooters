using UnityEngine;
using System.Collections;

public class ObjectScroller : MonoBehaviour
{
	public float CurrentSpeed
	{	get { return _currentSpeed; } }
	private float _currentSpeed;

	void Update()
	{
		transform.Translate(Vector3.up * _currentSpeed * Time.deltaTime);
	}

	public void SetSpeed(float newSpeed)
	{
		_currentSpeed = newSpeed;
	}
}
