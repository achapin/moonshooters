using UnityEngine;
using System.Collections;

[System.Serializable]
public class ShipData
{
	public Sprite shipSprite;
	public string shipName;
	public int health;
	public float maxSpeed;
	public float accel;
	public WeaponData[] weaponProgression;
	public Sprite livesSprite;
	public Sprite[] damageSprites;
	public string playOnHurt;
	public string playOnExplode;
}
