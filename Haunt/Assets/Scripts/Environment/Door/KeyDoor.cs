using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : Door, IKey {

	public void OnTriggerEnter (Collider other)
	{
		if (other.GetComponent<InventoryComponent> () != null) {
			if (other.GetComponent<InventoryComponent> ().holdingitem.Equals (Registry.FindItemByID ("objective"))) {
				isGoingUp = true;
			}
		}
	}

	public void OnTriggerExit (Collider other)
	{
		if (other.GetComponent<InventoryComponent> () != null) {
			if (other.GetComponent<InventoryComponent> ().holdingitem.Equals (Registry.FindItemByID ("objective"))) {
				isGoingUp = false;
			}
		}
	}
}
