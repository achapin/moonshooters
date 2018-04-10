using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhalanxFormation : Formation {

	public Vector3[] spawnPositions;
	public Vector3[] gatherPositions;
	public Vector3 center;
	public Vector2 spacing;
	public int perRow;
	public float speed;
	public float timeBetweenLaunches;
	public float phalanxSpeed;
	public float phalanxTime;
	
	private List<float> _countdown = new List<float>();
	private List<int> _targetPositionIndicies = new List<int>();
	private List<Vector3> _targetPositions = new List<Vector3>();
	private Vector3 _lastSeenActive;
	private PHALANX_STATE _currentState;
	private float _timeInPhalanxTime;
	private float _phalanxSpeed;

	private enum PHALANX_STATE { SPAWNING, PHALANX }; 
	
	public override void Launch()
	{
		_enemies.Clear();
		_currentState = PHALANX_STATE.SPAWNING;
		_countdown.Clear();
		_targetPositionIndicies.Clear();
		_targetPositions.Clear();
		for(int enemyIndex = 0; enemyIndex < totalEnemies; enemyIndex++)
		{
			Enemy newEnemy = Directory.Instance.enemyPool.GetEnemy(enemiesToUse[enemyIndex % enemiesToUse.Length]);
			newEnemy.transform.localPosition = spawnPositions[ enemyIndex % spawnPositions.Length];
			_enemies.Add(newEnemy);
			_countdown.Add(timeBetweenLaunches * (enemyIndex + 1));
			_targetPositionIndicies.Add(enemyIndex % gatherPositions.Length);
			_targetPositions.Add(gatherPositions[_targetPositionIndicies[enemyIndex]]);
		}
	}
	
	void Update()
	{
		int activeEnemies = 0;
		int deactivatedEnemies = 0;

		if(_currentState == PHALANX_STATE.PHALANX)
		{
			_timeInPhalanxTime -= Time.deltaTime;
			if(_timeInPhalanxTime <= 0f)
			{
				_timeInPhalanxTime += phalanxTime;
				_phalanxSpeed = -_phalanxSpeed;
			}
		}

		for(int enemyIndex = 0; enemyIndex < _enemies.Count; enemyIndex++)
		{
			if(_enemies[enemyIndex] == null) continue;
			if(!_enemies[enemyIndex].gameObject.activeSelf)
			{
				_enemies[enemyIndex] = null;
				continue;
			}
			activeEnemies++;
			_lastSeenActive = _enemies[enemyIndex].transform.position;
			if(_currentState == PHALANX_STATE.SPAWNING) deactivatedEnemies += UpdateSpawnPosition(_enemies[enemyIndex].gameObject, enemyIndex);
			else if (_currentState == PHALANX_STATE.PHALANX) UpdatePhalanxState(_enemies[enemyIndex].gameObject, enemyIndex);
		}
		if(activeEnemies <= 0) FireDestruction(_lastSeenActive);
		if(deactivatedEnemies >= activeEnemies)
		{
			if(_currentState == PHALANX_STATE.SPAWNING)
			{
				_currentState = PHALANX_STATE.PHALANX;
				_timeInPhalanxTime = phalanxTime;
				_phalanxSpeed = phalanxSpeed;
			}
		}
	}

	private int UpdateSpawnPosition(GameObject gO, int enemyIndex)
	{
		if(_countdown[enemyIndex] > 0f) _countdown[enemyIndex] -= Time.deltaTime;
		else
		{
			if(Vector3.Distance(gO.transform.localPosition, _targetPositions[enemyIndex]) < speed * Time.deltaTime)
			{
				gO.transform.localPosition = _targetPositions[enemyIndex];
				if(_targetPositionIndicies[enemyIndex] == -1)
				{
					return 1;
				}else{
					_targetPositionIndicies[enemyIndex] = -1;
					_targetPositions[enemyIndex] = center + (Vector3.right * spacing.x * ((enemyIndex % perRow) - ((float)perRow / 2f))) + (Vector3.up * spacing.y * (enemyIndex/perRow));
				}
			}else{
				_enemies[enemyIndex].MoveTo(Vector3.MoveTowards(gO.transform.localPosition, _targetPositions[enemyIndex], speed * Time.deltaTime));
			}
		}
		return 0;
	}

	private void UpdatePhalanxState(GameObject gO, int enemyIndex)
	{
		_enemies[enemyIndex].MoveTo(gO.transform.localPosition + (Vector3.right * Time.deltaTime * _phalanxSpeed));
	}
}
