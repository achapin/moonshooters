using UnityEngine;
using System.Collections;

public class HealthHaver : MonoBehaviour 
{
	public delegate void DeathHandler();
	public event DeathHandler Died;

	public delegate void PercentHandler(float pct);
	public event PercentHandler HealthPct;

	public int currentHealth;
	public int maxHealth;

	public void HealthDelta(int amount)
	{
		currentHealth += amount;
		if(currentHealth <= 0)
		{
			if(Died != null) Died();
		}else{
			if(HealthPct != null)
			{
				HealthPct((float)currentHealth / (float) maxHealth);
			}
		}
	}
}
