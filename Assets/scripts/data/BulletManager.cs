using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {

	public BulletData[] bullets;

	public BulletData GetBullet(int index)
	{
		if(bullets.Length <= 0) throw new MissingReferenceException("Need to provide Bullet Definitions!");
		if(index < 0 || index >= bullets.Length) return bullets[0];
		return bullets[index];
	}
}
