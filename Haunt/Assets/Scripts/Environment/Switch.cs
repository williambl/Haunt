using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

	void OnTriggerStay (Collider other)
	{
		if (other.GetComponent<ItemComponent> () != null) {
			if (other.GetComponent<ItemComponent> ().item.id == "objective" && other.GetComponent<ItemComponent> ().isHeld == false) {
				GameManager.won = true;
			}
		}
	}
}
