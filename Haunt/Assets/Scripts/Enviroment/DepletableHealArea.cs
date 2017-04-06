using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepletableHealArea : MonoBehaviour {

	public PlayerController player;

	public float energy;

	/// <summary>
	/// Set to true if this is a physical healing object, false if it's just a healing area.
	/// </summary>
	public bool isReal;

	/// <summary>
	/// Whether to destroy the object on depletion.
	/// </summary>
	public bool destroyOnDeplete;

	public void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}

	public void OnTriggerStay (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			player.energy.GainEnergy (0.001f);
			energy -= 0.001f;
		}
	}

	public void Update ()
	{
		if (isReal)
		    transform.localScale = new Vector3 (energy, energy, energy);

		if (energy < 0) {
			if (destroyOnDeplete)
				Destroy (gameObject);
			energy = 0;
		}
	}
}
