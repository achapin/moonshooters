using UnityEngine;
using System.Collections;

public class iOSApplicationFramerate : MonoBehaviour 
{
	
	void Start () 
	{
		Application.targetFrameRate = 60;
	}
}
