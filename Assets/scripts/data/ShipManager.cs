using UnityEngine;
using System.Collections;

public class ShipManager : MonoBehaviour 
{
	public ShipData[] availableShips;

	public ShipData GetShip(int index)
	{
		if(availableShips.Length <= 0) throw new MissingReferenceException("No ships assigned to ship manager!");
		if(index < 0 || index >= availableShips.Length) return availableShips[0];
		return availableShips[index];
	}

	public int NumShips()
	{
		return availableShips.Length;
	}
}
