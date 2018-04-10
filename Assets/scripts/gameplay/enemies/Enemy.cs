using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public SpriteRenderer sprite;

	private int _points;
	private BoxCollider2D _boxCollider;
	private CircleCollider2D _circleCollider;
	private WeaponShooter _myShooter;
	private Rigidbody2D _myRidigbody2D;
	private HealthHaver _myHealth;
	private string _deathSound;

	void Awake()
	{
		_boxCollider = GetComponent<BoxCollider2D>();
		_circleCollider = GetComponent<CircleCollider2D>();
		_myShooter = GetComponent<WeaponShooter>();
		_myRidigbody2D = GetComponent<Rigidbody2D>();
		_myHealth = GetComponent<HealthHaver>();
		_myHealth.Died += HandleDied;
	}

	public void Initialize(EnemyData d)
	{
		sprite.sprite = d.image;
		_myHealth.currentHealth = d.hp;
		_myHealth.maxHealth = d.hp;
		_boxCollider.enabled = !d.useCircleCollider;
		_circleCollider.enabled = d.useCircleCollider;
		if(d.useCircleCollider) _circleCollider.radius = d.colliderSize;
		else _boxCollider.size = Vector2.one * d.colliderSize;
		_myShooter.SetWeaponData(d.weapon);
		_points = d.pointsGiven;
		_deathSound = d.playOnExplode;
	}

	void FixedUpdate()
	{
		_myRidigbody2D.velocity = Vector2.zero;
	}

	public void MoveTo(Vector3 position)
	{
		_myRidigbody2D.MovePosition(position);
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		_myHealth.HealthDelta(-1);
	}

	void HandleDied ()
	{
		if(_deathSound != null && _deathSound != "") Directory.Instance.soundPool.PlaySound(_deathSound);
		Directory.Instance.explosionPool.SetExplosion(2,transform.position);
		Directory.Instance.scoreHandler.AddScore(_points);
		gameObject.SetActive(false);
	}
}
