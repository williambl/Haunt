using UnityEngine;
using System.Collections;

public class NPCDoor : Door {

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.layer == 10) 
		{
			isGoingUp = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.layer == 10) 
		{
			isGoingUp = false;
		}
	}
}
