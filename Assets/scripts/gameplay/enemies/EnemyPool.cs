using UnityEngine;
using System.Collections;

public class EnemyPool : GenericPool
{
	public EnemyManager enemyData;

	void Start () 
	{
		Directory.Instance.enemyPool = this;
	}

	public Enemy GetEnemy(int enemyType)
	{
		Enemy e = GetActive().GetComponent<Enemy>();
		EnemyData data = enemyData.GetEnemy(enemyType);
		e.Initialize(data);
		e.gameObject.SetActive(true);
		return e;
	}
}
