using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Formation))]
public class LaunchFormationOnDistance : MonoBehaviour 
{
	public GameObject triggerObject;
	public float yDistance;

	private bool _triggered = false;

	void Update()
	{
		if(_triggered) return;
		if(Mathf.Abs(triggerObject.transform.position.y - transform.position.y) < yDistance)
		{
			GetComponent<Formation>().Launch();
			_triggered = true;
		}
	}
}
