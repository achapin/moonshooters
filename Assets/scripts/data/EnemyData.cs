using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemyData
{
	public Sprite image;
	public bool useCircleCollider;
	public float colliderSize;
	public int hp;
	public WeaponData weapon;
	public int pointsGiven;
	public string playOnExplode;
}
