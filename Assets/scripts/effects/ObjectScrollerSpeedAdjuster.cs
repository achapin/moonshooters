using UnityEngine;
using System.Collections;

public class ObjectScrollerSpeedAdjuster : MonoBehaviour {

	public ObjectScroller scroller;
	public Vector2 bounds;
	public float speedAtMin;

	private float _sourceSpeed = float.MinValue;

	void Update () 
	{
		if(Vector3.Distance(scroller.transform.position, transform.position) < bounds.y)
		{
			if(_sourceSpeed == float.MinValue)
				_sourceSpeed = scroller.CurrentSpeed;
			scroller.SetSpeed(Mathf.Lerp(speedAtMin, _sourceSpeed, (Vector3.Distance(scroller.transform.position, transform.position) - bounds.x) / (bounds.y - bounds.x)));
		}else{
			if(_sourceSpeed != float.MinValue)
				_sourceSpeed = float.MinValue;
		}
	}
}
