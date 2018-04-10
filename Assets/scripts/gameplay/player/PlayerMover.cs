using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour {

	public PlayerMoverController controller;

	public float maxX;

	public float maxSpeed;
	public float accel;

	private float _currentSpeed;
	private Rigidbody2D _myRigidbody2D;

	void Awake()
	{
		_currentSpeed = 0f;
		_myRigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Update () 
	{
		if(controller == null)
		{
			if(GetComponent<PlayerMoverController>() == null)
			{
				return;
			}else{
				controller = GetComponent<PlayerMoverController>();
			}
		}
		if(controller.HorizontalMotion() == 0f)
		{
			_currentSpeed = 0f;
		}else{

			if(Mathf.Abs(_currentSpeed / controller.HorizontalMotion()) != _currentSpeed / controller.HorizontalMotion())
				_currentSpeed = 0f;
			_currentSpeed += accel * Time.deltaTime * controller.HorizontalMotion();
			_currentSpeed = Mathf.Clamp(_currentSpeed, -maxSpeed, maxSpeed);
		}
	}

	void FixedUpdate()
	{
		_myRigidbody2D.MovePosition(transform.position + (Vector3.right * _currentSpeed * Time.deltaTime));
		if(transform.localPosition.x < -maxX)
		{
			transform.localPosition = new Vector3(-maxX, transform.localPosition.y, transform.localPosition.z);
			_currentSpeed = 0f;
		}else if(transform.localPosition.x > maxX)
		{
			transform.localPosition = new Vector3(maxX, transform.localPosition.y, transform.localPosition.z);
			_currentSpeed = 0f;
		}
		_myRigidbody2D.velocity = Vector2.zero;
	}
}

public abstract class PlayerMoverController : MonoBehaviour
{
	public abstract float HorizontalMotion();
	public abstract bool ForCurrentPlatform();
}
