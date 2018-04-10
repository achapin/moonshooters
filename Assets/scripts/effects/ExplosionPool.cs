using UnityEngine;
using System.Collections;

public class ExplosionPool : GenericPool {
	
	public RuntimeAnimatorController[] explosions;

	void Start () 
	{
		Directory.Instance.explosionPool = this;
	}
	
	public void SetExplosion(int explosionColor, Vector3 position)
	{
		Animator a = GetActive().GetComponent<Animator>();
		a.gameObject.SetActive(true);
		a.runtimeAnimatorController = explosions[explosionColor];
		a.SetTrigger("Fire");
		a.transform.position = position;
	}
}
