using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScrollingStarfield : MonoBehaviour 
{

	public Sprite[] spriteOptions;
	public Vector2 toFill;
	public string sortingOrder;
	public float minY;

	public float maxScrollSpeed;

	private float _currentScrollSpeed;
	private List<GameObject> _starfieldSprites;
	private float _topPos;

	void Start () 
	{
		int option = Random.Range(0, spriteOptions.Length);
		float size = spriteOptions[option].bounds.size.x;
		int spritesAcross = Mathf.CeilToInt(toFill.x / size);
		int spritesDown = Mathf.CeilToInt(toFill.y / size);
		_topPos = size * spritesDown;

		_starfieldSprites = new List<GameObject>();
		for(int yIndex = 0; yIndex < spritesDown; yIndex++)
		{
			for(int xIndex = 0; xIndex < spritesAcross; xIndex++)
			{
				GameObject newField = new GameObject();
				SpriteRenderer newSprite = newField.AddComponent<SpriteRenderer>();
				newSprite.sortingLayerName = sortingOrder;
				newSprite.sprite = spriteOptions[option];
				newSprite.transform.parent = transform;
				newSprite.transform.localPosition = Vector3.up * yIndex * size + Vector3.right * xIndex * size;
				_starfieldSprites.Add(newField);
			}
		}
		SetScollSpeed(1f);
	}

	public void SetScollSpeed(float pct)
	{
		_currentScrollSpeed = maxScrollSpeed * pct;
	}

	void Update () 
	{
		for(int index = 0; index < _starfieldSprites.Count; index++)
		{
			GameObject gO = _starfieldSprites[index];
			gO.transform.Translate(Vector3.up * -_currentScrollSpeed * Time.deltaTime);
			if(gO.transform.localPosition.y < minY)
			{
				gO.transform.localPosition = new Vector3(gO.transform.localPosition.x, _topPos + (gO.transform.localPosition.y - minY), 0f);
			}
		}
	}
}
