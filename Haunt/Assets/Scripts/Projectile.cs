using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	ParticleSystem ps;

	bool isExploding = false;

	void Start ()
	{
		ps = GetComponent<ParticleSystem> ();
		ps.Stop ();
	}


	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == 10) {
			collision.collider.GetComponent<NPCHealth> ().LoseHealth (collision.relativeVelocity.magnitude * 0.1f);
		}

		if (collision.gameObject.tag == "Lock") {
			collision.gameObject.GetComponent<Lock> ().ToggleLock ();
		}
		ps.Play ();
		isExploding = true;
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		GetComponent<TrailRenderer> ().enabled = false;
		GetComponent<Renderer> ().enabled = false;
		GetComponent<Light> ().enabled = false;
	}

	void Update ()
	{
		if (isExploding) {
			if (!ps.IsAlive ()) {
				Destroy (gameObject);
			}
		}
	}
}
