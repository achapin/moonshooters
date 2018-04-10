using UnityEngine;
using System.Collections;

public class BossManager : MonoBehaviour {

	public delegate void BossEnter(string name, HealthHaver h);
	public event BossEnter NewBoss;

	void Awake()
	{
		Directory.Instance.bossManager = this;
	}

	public void NewBossSpawned(string name, HealthHaver h)
	{
		if(NewBoss != null) NewBoss(name, h);
	}
}
