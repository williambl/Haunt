using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

	GameManager manager;
	
	void Start () 
	{
		if(manager == null)
			manager = GameObject.FindGameObjectWithTag ("Manager").GetComponent<GameManager> ();
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
