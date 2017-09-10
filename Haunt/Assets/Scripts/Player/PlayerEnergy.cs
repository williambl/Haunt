using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour {

	[Header("References:")]
	public Image energyBar;
	public PlayerController controller;

	[Header("Properties:")]
	public float energy = 1;
	public float drainAmount;

	void Start ()
	{
		if(!energyBar)
			energyBar = GameObject.Find ("EnergyCanvas/EnergyBackground/Energybar").GetComponent<Image> ();
		if(!controller)
			controller = GetComponent<PlayerController> ();

		switch (GameManager.difficultyLevel) {
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

	void FixedUpdate ()
	{
		if (energy < 0)
			energy = 0;
		else if (GameManager.gameState == GameState.PLAYING) {
			DrainEnergy ();
		}
		if (energy > 1)
			energy = 1;
		energyBar.rectTransform.sizeDelta = new Vector2(25, energy * 250);
		energyBar.rectTransform.localPosition = new Vector3(energyBar.rectTransform.localPosition.x, 0f - 0.5f*(250 - energyBar.rectTransform.sizeDelta.y), energyBar.rectTransform.localPosition.z);

		//Energy visual effects
		controller.transform.localScale = new Vector3(energy, energy, energy);
		controller.pointLight.intensity = 6 * energy;
		controller.pointLight.range = controller.pointLight.intensity * 20f < 2 ? controller.pointLight.intensity * 20 : 2;
		controller.effectManager.lowEnergy = energy < 0.2 && energy > 0;

		var particles = controller.particles.player.main;
		particles.startSize = new ParticleSystem.MinMaxCurve (energy, 0.75f * energy);
	}

	void DrainEnergy ()
	{

		if (controller.isPossessing) {
			if (energy < 0.5)
				controller.abilities.possess.UnpossessTarget ();
			else
				energy = energy - 0.001f * drainAmount;
		}
		if (controller.holding != null) {
			if (energy < 0.1)
				controller.abilities.hold.Drop ();
			else {
				energy = energy - 0.0001f * drainAmount;
			}
		}
		if (controller.attackers.Count > 0) {
			energy = energy - 0.001f * drainAmount;
		}
		if (controller.isInvisible) {
			if (energy < 0.3)
				controller.abilities.invisible.BecomeVisible ();
			else
				energy = energy - 0.005f * drainAmount;
		}
		if (controller.isSprinting) {
			energy -= 0.001f * drainAmount;
		}
		energy = energy - 0.00005f * drainAmount;
	}

	public void LoseEnergy (float amount)
	{
		if (GameManager.gameState != GameState.PLAYING)
			return;

		if (energy - amount < 0)
			energy = 0;
		else if (energy - amount > 1)
			energy = 1;
		else
			energy -= amount;
		controller.particles.Heal (false);
	}

	public void GainEnergy (float amount)
	{
		if (GameManager.gameState != GameState.PLAYING)
			return;

		if (energy + amount < 0)
			energy = 0;
		else if (energy + amount > 1)
			energy = 1;
		else
			energy += amount;

		controller.particles.Heal (true);
	}
}
