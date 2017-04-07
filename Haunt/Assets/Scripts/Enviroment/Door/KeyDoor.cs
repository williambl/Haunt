using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : Door {

	void OnTriggerEnter (Collider other)
	{
		if (other.GetComponent<InventoryComponent> () != null) {
			if (other.GetComponent<InventoryComponent> ().holdingitem == ((Item)Registry.itemRegistry.ToArray ().GetValue (0))) {
				isGoingUp = true;
			}
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.GetComponent<InventoryComponent> () != null) {
			if (other.GetComponent<InventoryComponent> ().holdingitem == ((Item)Registry.itemRegistry.ToArray ().GetValue (0))) {
				isGoingUp = false;
			}
		}
	}
}
