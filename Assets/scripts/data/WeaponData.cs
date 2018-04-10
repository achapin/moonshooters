using UnityEngine;

[System.Serializable]
public class WeaponData 
{
	public Gun[] weapons;
}

[System.Serializable]
public class Gun
{
	public int bulletToFire;
	public float timeToFire;
	public Vector3 positionOffset;
	public Vector2 direction;
	public string playOnFire;
}
