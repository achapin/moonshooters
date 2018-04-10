using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipSelector : MonoBehaviour 
{
	public Image image1;
	public Image image2;
	public Text shipName;

	public ShipManager shipData;

	public float spawnPos;
	public float transferSpeed;

	private bool _currentPreviewIsImage1;
	private int _currentShipId;
	private bool _transitioning;

	void Start()
	{
		_currentShipId = 0;
		if(PlayerPrefs.HasKey("PreferredShip"))
		{
			_currentShipId = PlayerPrefs.GetInt("PreferredShip");
		}
		PlayerPrefs.SetInt("PreferredShip", _currentShipId);
		ShipData currentShip = shipData.GetShip(_currentShipId);
		image1.sprite = currentShip.shipSprite;
		shipName.text = currentShip.shipName;
		_currentPreviewIsImage1 = true;
		image2.rectTransform.anchoredPosition = Vector2.right * 1000f;
		_transitioning = false;
	}

	public void NextShip()
	{
		_currentShipId++;
		if(_currentShipId >= shipData.NumShips())
			_currentShipId = 0;
		UpdateShipPreview(true);
	}

	public void PrevShip()
	{
		_currentShipId--;
		if(_currentShipId < 0)
			_currentShipId = shipData.NumShips() - 1;
		UpdateShipPreview(false);
	}

	private void UpdateShipPreview(bool nextTransition)
	{
		if(_transitioning) return;
		Image activeImage = _currentPreviewIsImage1 ? image1 : image2;
		Image newImage = _currentPreviewIsImage1 ? image2 : image1;
		PlayerPrefs.SetInt("PreferredShip", _currentShipId);
		ShipData currentShip = shipData.GetShip(_currentShipId);
		newImage.sprite = currentShip.shipSprite;
		shipName.text = currentShip.shipName;
		_currentPreviewIsImage1 = !_currentPreviewIsImage1;
		StartCoroutine(ShowShipTransition(activeImage, newImage, nextTransition));
	}

	private IEnumerator ShowShipTransition(Image activeImage, Image newImage, bool nextTransition)
	{
		_transitioning = true;
		activeImage.gameObject.SetActive(true);
		newImage.gameObject.SetActive(true);
		float transitionTime = 0f;
		while(transitionTime < transferSpeed)
		{
			activeImage.rectTransform.anchoredPosition = Vector2.Lerp(Vector2.zero, Vector2.right * spawnPos * (nextTransition ? 1f : -1f), transitionTime / transferSpeed);
			newImage.rectTransform.anchoredPosition = Vector2.Lerp(Vector3.right * spawnPos * (nextTransition ? -1f : 1f), Vector2.zero, transitionTime / transferSpeed);
			transitionTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		activeImage.gameObject.SetActive(false);
		newImage.rectTransform.anchoredPosition = Vector2.zero;

		_transitioning = false;
	}
}
