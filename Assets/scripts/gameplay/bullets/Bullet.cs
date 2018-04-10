using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour 
{
	public Vector2 boundsUpperRight;
	public Vector2 boundsLowerLeft;

	private Animator _myAnimator;
	private CircleCollider2D _myCollider;
	private Rigidbody2D _myRigidbody2D;

	void Awake()
	{
		_myAnimator = GetComponent<Animator>();
		_myCollider = GetComponent<CircleCollider2D>();
		_myRigidbody2D = GetComponent<Rigidbody2D>();
	}

	public void Initialize(RuntimeAnimatorController animationController, float radius, Vector2 velocity)
	{
		_myCollider.enabled = true;
		_myCollider.radius = radius;
		_myAnimator.runtimeAnimatorController = animationController;
		_myRigidbody2D.velocity = velocity;
		transform.eulerAngles = Vector3.forward * ((Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x)) - 90f);
	}

	void Update()
	{
		if(transform.localPosition.x > boundsUpperRight.x || transform.localPosition.y > boundsUpperRight.y ||
		   transform.localPosition.x < boundsLowerLeft.x || transform.localPosition.y < boundsLowerLeft.y ||
		   (_myRigidbody2D.velocity.magnitude < .1f && _myCollider.enabled && _myAnimator.enabled))
		{
			gameObject.SetActive(false);
		}
	}

	public void Cleanup()
	{
		gameObject.SetActive(false);
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		_myCollider.enabled = false;
		_myRigidbody2D.velocity = Vector2.zero;
		_myAnimator.SetTrigger("Hit");
	}
}
