using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenericPool : MonoBehaviour
{
	public GameObject prefab;
	public int toPool;
	
	private List<GameObject> _inactive = new List<GameObject>();
	private List<GameObject> _active = new List<GameObject>();

	protected void SpawnNewInstances()
	{
		for(int index = 0; index < toPool; index++)
		{
			GameObject newObject = Instantiate(prefab, Vector3.right * 1000f, Quaternion.identity) as GameObject;
			newObject.transform.parent = transform;
			_inactive.Add(newObject);
			newObject.gameObject.SetActive(false);
		}
	}
	
	private List<GameObject> _toReturnToInactive = new List<GameObject>();
	void Update()
	{
		for(int itemIndex = 0; itemIndex < _active.Count; itemIndex++)
		{
			if(!_active[itemIndex].gameObject.activeSelf) _toReturnToInactive.Add(_active[itemIndex]);
		}
		for(int returnIndex = 0; returnIndex < _toReturnToInactive.Count; returnIndex++)
		{
			_active.Remove(_toReturnToInactive[returnIndex]);
			_inactive.Add(_toReturnToInactive[returnIndex]);
		}
		_toReturnToInactive.Clear();
	}

	protected GameObject GetActive()
	{
		if(_inactive.Count <= 0) SpawnNewInstances();
		GameObject gO = _inactive[0];
		_inactive.Remove(gO);
		_active.Add(gO);
		return gO;
	}
}
