using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
	
	/// <summary>
	/// The level to go to.
	/// </summary>
	public int destination;

	GameManager manager;

	void Start ()
	{
		if(manager == null)
			manager = GameObject.FindGameObjectWithTag ("Manager").GetComponent<GameManager> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9 || other.gameObject.layer == 12) {
			manager.GotoLevel (destination);
		}
	}
}
