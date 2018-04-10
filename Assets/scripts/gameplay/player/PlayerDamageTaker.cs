using UnityEngine;
using System.Collections;

public class PlayerDamageTaker : MonoBehaviour 
{
	public SpriteRenderer[] sprites;
	public float flashTime = 1f;

	private HealthHaver _myHealthHaver;
	private bool _isInvulnerable = false;

	void Start()
	{
		_myHealthHaver = GetComponent<HealthHaver>();
	}

	void Update()
	{
		if(_isInvulnerable)
		{
			for(int spriteIndex = 0; spriteIndex < sprites.Length; spriteIndex++)
			{
				sprites[spriteIndex].enabled = Mathf.PingPong(Time.timeSinceLevelLoad * flashTime, 1f) > .5f;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(_isInvulnerable) return;
		if(collision.gameObject.GetComponent<Bullet>() != null)
		{
			_myHealthHaver.HealthDelta(-1);
			Directory.Instance.explosionPool.SetExplosion(1,collision.contacts[0].point);
		}
	}

	public void SetInvulnerable(bool nowInvulnerable)
	{
		_isInvulnerable = nowInvulnerable;
		if(!_isInvulnerable)
			for(int spriteIndex = 0; spriteIndex < sprites.Length; spriteIndex++)
			{
				sprites[spriteIndex].enabled = true;
			}
	}
}
