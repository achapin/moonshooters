using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponShooter : MonoBehaviour 
{
	public string bulletLayer;

	private BulletShooter _bulletShooter;
	private WeaponData _weaponData;
	private List<float> _timeToFire = new List<float>();

	public void SetWeaponData(WeaponData newData)
	{
		_weaponData = newData;
		_timeToFire.Clear();
		foreach(Gun g in _weaponData.weapons)
		{
			_timeToFire.Add(g.timeToFire);
		}
	}

	void Update () 
	{
		float dT = Time.deltaTime;
		for(int gunIndex = 0; gunIndex < _timeToFire.Count; gunIndex++)
		{
			_timeToFire[gunIndex] -= dT;
			if(_timeToFire[gunIndex] <= 0f)
			{
				if(_bulletShooter == null) _bulletShooter = Directory.Instance.bulletShooter;
				if(_weaponData.weapons[gunIndex].playOnFire != null && _weaponData.weapons[gunIndex].playOnFire != "")
					Directory.Instance.soundPool.PlaySound(_weaponData.weapons[gunIndex].playOnFire);
				_bulletShooter.FireBullet(_weaponData.weapons[gunIndex].bulletToFire, 
				                               transform.position + _weaponData.weapons[gunIndex].positionOffset,
				                               _weaponData.weapons[gunIndex].direction,
				                               bulletLayer);
				_timeToFire[gunIndex] += _weaponData.weapons[gunIndex].timeToFire;
			}
		}
	}
}
