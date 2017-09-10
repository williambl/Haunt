using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
	
	/// <summary>
	/// The level to go to.
	/// </summary>
	public int destination;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9 || other.gameObject.layer == 12) {
			GameManager.GotoLevel (destination);
		}
	}
}
