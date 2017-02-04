using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	GameManager manager;

	void Start ()
	{
		manager = GameObject.Find ("Manager").GetComponent<GameManager> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9) {
			manager.GotoLevel (0);
		}
	}
}
