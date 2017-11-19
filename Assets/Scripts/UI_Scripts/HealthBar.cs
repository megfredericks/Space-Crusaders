using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour 
{
	public ShipControllerCombat ship;
	public float maxWidth;

	private Image _healthImage;

	// Use this for initialization
	void Start ()
	{
		_healthImage = gameObject.GetComponent<Image>();
		_healthImage.rectTransform.sizeDelta = new Vector2(0f, _healthImage.rectTransform.sizeDelta.y);
	}

	// Update is called once per frame
	void Update ()
	{
        _healthImage.rectTransform.sizeDelta = new Vector2(ship.HealthPercentage() * maxWidth, _healthImage.rectTransform.sizeDelta.y);
	}
}
