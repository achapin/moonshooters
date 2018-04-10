using UnityEngine;
using System.Collections;

public class BossFormation : PhalanxFormation {

	public string bossName;

	public override void Launch()
	{
		base.Launch();
		Directory.Instance.bossManager.NewBossSpawned(bossName, _enemies[0].GetComponent<HealthHaver>());
	}
}
