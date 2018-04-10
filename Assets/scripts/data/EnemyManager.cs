using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public EnemyData[] availableEnemies;
	
	public EnemyData GetEnemy(int index)
	{
		if(availableEnemies.Length <= 0) throw new MissingReferenceException("No enemies assigned to enemy manager!");
		if(index < 0 || index >= availableEnemies.Length) return availableEnemies[0];
		return availableEnemies[index];
	}
	
	public int NumEnemies()
	{
		return availableEnemies.Length;
	}
}
