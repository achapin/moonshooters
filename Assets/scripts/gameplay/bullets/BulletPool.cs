using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletPool : GenericPool, BulletShooter
{
	public BulletManager bulletData;

	void Start()
	{
		Directory.Instance.bulletShooter = this;
	}

	public void FireBullet(int index, Vector3 position, Vector2 direction, string bulletLayer)
	{
		Bullet newBullet = GetActive().GetComponent<Bullet>();
		newBullet.gameObject.layer = LayerMask.NameToLayer(bulletLayer);
		BulletData thisData = bulletData.GetBullet(index);
		newBullet.gameObject.SetActive(true);
		newBullet.Initialize(thisData.animator, thisData.radius, direction.normalized * thisData.speed);
		newBullet.transform.position = position;
	}
}
