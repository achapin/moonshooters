using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TravelingFormation : Formation
{
	public Vector3[] positions;
	public float speed;
	public float timeBetweenLaunches;

	private List<float> _timeToLaunch = new List<float>();
	private List<int> _targetPositions = new List<int>();
	private Vector3 _lastSeenActive;

	public override void Launch()
	{
		_enemies.Clear();
		_timeToLaunch.Clear();
		_targetPositions.Clear();
		for(int enemyIndex = 0; enemyIndex < totalEnemies; enemyIndex++)
		{
			Enemy newEnemy = Directory.Instance.enemyPool.GetEnemy(enemiesToUse[enemyIndex % enemiesToUse.Length]);
			newEnemy.transform.localPosition = positions[0];
			_enemies.Add(newEnemy);
			_timeToLaunch.Add(timeBetweenLaunches * (enemyIndex + 1));
			_targetPositions.Add(1);
		}
	}

	void Update()
	{
		int activeEnemies = 0;
		int deactivatedEnemies = 0;
		for(int enemyIndex = 0; enemyIndex < _enemies.Count; enemyIndex++)
		{
			if(_enemies[enemyIndex] == null) continue;
			if(!_enemies[enemyIndex].gameObject.activeSelf)
			{
				_enemies[enemyIndex] = null;
				continue;
			}
			activeEnemies++;
			if(_timeToLaunch[enemyIndex] > 0f) _timeToLaunch[enemyIndex] -= Time.deltaTime;
			else
			{
				if(Vector3.Distance(_enemies[enemyIndex].transform.localPosition, positions[_targetPositions[enemyIndex]]) < speed * Time.deltaTime)
				{
					_enemies[enemyIndex].transform.localPosition = positions[_targetPositions[enemyIndex]];
					if(_targetPositions[enemyIndex] == positions.Length - 1)
					{
						_enemies[enemyIndex].gameObject.SetActive(false);
						deactivatedEnemies++;
					}else{
						_targetPositions[enemyIndex]++;
						_lastSeenActive = _enemies[enemyIndex].transform.position;
					}
				}else{
					Vector3 delta = Vector3.MoveTowards(_enemies[enemyIndex].transform.localPosition, positions[_targetPositions[enemyIndex]], speed * Time.deltaTime) - _enemies[enemyIndex].transform.localPosition;
					_enemies[enemyIndex].MoveTo(_enemies[enemyIndex].transform.position + delta);
				}
			}
		}
		if(activeEnemies <= 0) FireDestruction(_lastSeenActive);
		if(deactivatedEnemies >= activeEnemies) FireRetreat();
	}
}
