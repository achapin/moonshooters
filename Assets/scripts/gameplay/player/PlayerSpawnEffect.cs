using UnityEngine;
using System.Collections;

public class PlayerSpawnEffect : MonoBehaviour 
{
	public Vector3 spawnAt;
	public Vector3 moveTo;
	public float spawnEffectTime;

	private HealthHaver _health;
	private PlayerDamageTaker _damageTaker;
	private PlayerMover _mover;
	private WeaponShooter _shooter;
	private float _effectTime = float.MaxValue;

	void Start()
	{
		_health = GetComponent<HealthHaver>();
		_damageTaker = GetComponent<PlayerDamageTaker>();
		_mover = GetComponent<PlayerMover>();
		_shooter = GetComponent<WeaponShooter>();
		_health.Died += StartSpawnEffect;
		StartSpawnEffect();
	}

	private void StartSpawnEffect()
	{
		_shooter.enabled = false;
		_mover.enabled = false;
		transform.localPosition = spawnAt;
		_damageTaker.SetInvulnerable(true);
		_health.currentHealth = _health.maxHealth;
		_effectTime = 0f;
	}

	void Update()
	{
		if(_effectTime < spawnEffectTime)
		{
			transform.localPosition = Vector3.Lerp(spawnAt, moveTo, _effectTime/ spawnEffectTime);
			_effectTime += Time.deltaTime;
			if(_effectTime >= spawnEffectTime)
			{
				_mover.enabled = true;
				transform.localPosition = moveTo;
				_damageTaker.SetInvulnerable(false);
				_shooter.enabled = true;
			}
		}
	}

}
