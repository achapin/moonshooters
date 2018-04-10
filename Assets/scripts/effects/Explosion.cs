using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public void CleanUp()
	{
		gameObject.SetActive(false);
	}
}
