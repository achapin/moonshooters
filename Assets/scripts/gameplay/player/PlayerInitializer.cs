using UnityEngine;
using System.Collections;

public class PlayerInitializer : MonoBehaviour 
{
	public ShipManager ships;
	public PlayerMoverController[] controllers;
	public PlayerMover mover;
	public SpriteRenderer displaySprite;
	public WeaponShooter shooter;

	private ShipData _myShip;
	private int _weaponIndex;

	void Start () 
	{
		_myShip = ships.GetShip(PlayerPrefs.GetInt("PreferredShip"));
		foreach(PlayerMoverController controller in controllers)
		{
			if(controller.ForCurrentPlatform())
			{
				mover.controller = controller;
			}else{
				Destroy(controller as MonoBehaviour);
			}
		}
		mover.maxSpeed = _myShip.maxSpeed;
		mover.accel = _myShip.accel;
		displaySprite.sprite = _myShip.shipSprite;
		_weaponIndex = 0;
		SetWeaponData();

		HealthHaver h = GetComponent<HealthHaver>();
		h.currentHealth = _myShip.health;
		h.maxHealth = _myShip.health;

		PlayerDamageDisplay d = GetComponent<PlayerDamageDisplay>();
		d.Initialize(_myShip.damageSprites, h, _myShip.playOnHurt, _myShip.playOnExplode);

		h.Died += HandleDied;

	}

	void HandleDied ()
	{
		//TODO: Handle death effect
		Directory.Instance.livesManager.LoseLife();
	}

#if UNITY_EDITOR
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Tab))
		{
			_weaponIndex = Mathf.Min(_weaponIndex + 1, _myShip.weaponProgression.Length - 1);
			SetWeaponData();
		}
	}
#endif

	private void SetWeaponData()
	{
		shooter.SetWeaponData(_myShip.weaponProgression[_weaponIndex]);
	}


}
