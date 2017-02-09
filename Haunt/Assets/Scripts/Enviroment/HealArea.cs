﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArea : MonoBehaviour {

	public PlayerController player;

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.tag == "Player")
			player.energy.energy = player.energy.energy + 0.001f;
	}
}