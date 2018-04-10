using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Formation : MonoBehaviour
{
	public int[] enemiesToUse;
	public int totalEnemies;

	public delegate void EnemiesDestroyed(Vector3 lastPos);
	public event EnemiesDestroyed FormationDestroyed;

	public delegate void EnemiesRetreated();
	public event EnemiesRetreated FormationRetreated;

	public virtual void Launch()
	{}

	protected List<Enemy> _enemies = new List<Enemy>();

	protected void FireRetreat()
	{
		if(FormationRetreated != null) FormationRetreated();
	}

	protected void FireDestruction(Vector3 lastPos)
	{
		if(FormationDestroyed != null) FormationDestroyed(lastPos);
	}
}
