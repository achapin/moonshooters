using UnityEngine;
using System.Collections;

public class PlayerDamageDisplay : MonoBehaviour 
{
	public SpriteRenderer displaySprite;

	private Sprite[] _damageSprites;
	private string _damageSound;
	private string _deathSound;

	public void Initialize(Sprite[] damage, HealthHaver target, string damageSound, string deathSound)
	{
		_damageSprites = damage;
		_damageSound = damageSound;
		_deathSound = deathSound;
		target.HealthPct += HandleHealthPct;
		target.Died += HandleDied;
	}

	void HandleDied ()
	{
		if(_deathSound != null && _deathSound != "") Directory.Instance.soundPool.PlaySound(_deathSound);
		Directory.Instance.explosionPool.SetExplosion(0,transform.position);
		displaySprite.sprite = _damageSprites[0];
	}

	void HandleHealthPct (float pct)
	{
		if(_damageSound != null && _damageSound != "" ) Directory.Instance.soundPool.PlaySound(_damageSound);
		displaySprite.sprite = _damageSprites[Mathf.FloorToInt(Mathf.Abs(1f - pct) * _damageSprites.Length)];
	}
}
