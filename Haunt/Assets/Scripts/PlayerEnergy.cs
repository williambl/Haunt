using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour {

	public float energy = 1;
	public Image energyBar;

	public PlayerController controller;

	public float drainAmount;

	GameManager manager;

	void Start ()
	{
		energyBar = GameObject.Find ("Canvas/EnergyBackground/Energybar").GetComponent<Image> ();
		controller = GetComponent<PlayerController> ();
		manager = GameObject.Find ("Manager").GetComponent<GameManager> ();

		switch (manager.difficultyLevel) {
		case Difficulty.EASY:
			drainAmount = 1f;
			break;
		case Difficulty.NORMAL:
			drainAmount = 1.2f;
			break;
		case Difficulty.HARD:
			drainAmount = 1.5f;
			break;
		}
	}

	void Update ()
	{
		if (energy < 0)
			energy = 0;
		else
			DrainEnergy ();
		energyBar.rectTransform.sizeDelta = new Vector2(25, energy * 250);
		energyBar.rectTransform.localPosition = new Vector3(energyBar.rectTransform.localPosition.x, 0f - 0.5f*(250 - energyBar.rectTransform.sizeDelta.y), energyBar.rectTransform.localPosition.z);
	}

	void DrainEnergy ()
	{

		if (controller.isPossessing) {
			if (energy < 0.5)
				controller.UnpossessTarget ();
			else
				energy = energy - 0.001f * drainAmount;
		}
		if (controller.holding != null) {
			if (energy < 0.1)
				controller.Drop ();
			else {
				energy = energy - 0.0001f * drainAmount;
			}
		}
		if (controller.attackers.Count > 0) {
			energy = energy - 0.001f * drainAmount;
		}
		energy = energy - 0.00005f * drainAmount;
	}
}
