using UnityEngine;
using System.Collections;

public class Pausable : MonoBehaviour 
{
	public Behaviour[] toPause;

	private Rigidbody2D _myRigidbody2D;
	private Vector2 _oldVelocity;
	private bool _wasKinematic;
	
	void Start()
	{
		PauseController c = Directory.Instance.pauseController;
		c.Pause += Pause;
		c.Resume += Resume;
		_myRigidbody2D = GetComponent<Rigidbody2D>();
	}

	public void Pause()
	{
		foreach(Behaviour b in toPause)
		{
			b.enabled = false;
		}
		if(_myRigidbody2D == null)
		{
			if(GetComponent<Rigidbody2D>() == null) return;
			else _myRigidbody2D = GetComponent<Rigidbody2D>();
		}
		_oldVelocity = _myRigidbody2D.velocity;
		_wasKinematic = _myRigidbody2D.isKinematic;
		_myRigidbody2D.velocity = Vector2.zero;
		_myRigidbody2D.isKinematic = true;
	}

	public void Resume()
	{
		foreach(Behaviour b in toPause)
		{
			b.enabled = true;
		}
		if(_myRigidbody2D != null)
		{
			_myRigidbody2D.isKinematic = _wasKinematic;
			_myRigidbody2D.velocity = _oldVelocity;
		}
	}
}
