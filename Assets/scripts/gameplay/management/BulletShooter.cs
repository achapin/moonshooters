using UnityEngine;
using System.Collections;

public interface BulletShooter
{
	void FireBullet(int index, Vector3 position, Vector2 direction, string bulletLayer);
}
