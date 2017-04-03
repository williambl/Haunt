using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == 10) {
			collision.collider.GetComponent<NPCHealth> ().LoseHealth (collision.relativeVelocity.magnitude * 0.1f);
			Debug.Log (collision.relativeVelocity.magnitude * 0.1f);
		}

		if (collision.gameObject.tag == "Lock") {
			collision.gameObject.GetComponent<Lock> ().isLocked = true;
		}
		Destroy (gameObject);
	}
}
