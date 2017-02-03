using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

	GameManager manager;

	void Start () 
	{
		manager = GameObject.Find ("Manager").GetComponent<GameManager> ();
	}

	void OnTriggerStay (Collider other)
	{
		if (other.GetComponent<ItemComponent> () != null) {
			if (other.GetComponent<ItemComponent> ().item.id == "objective" && other.GetComponent<ItemComponent> ().isHeld == false) {
				manager.won = true;
			}
		}
	}
}
