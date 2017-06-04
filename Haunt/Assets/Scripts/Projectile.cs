using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	private ParticleSystem ps;
	
	
	private float damageIntensity = 0.1f;
	private bool isExploding = false;

	void Start ()
	{
		if(!ps) {
			ps = GetComponent<ParticleSystem> ();
		}
		ps.Stop ();
	}


	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == 10) {
			collision.collider.GetComponent<NPCHealth> ().LoseHealth (collision.relativeVelocity.magnitude * damageIntensity);
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
				Destroy (this.gameObject);
			}
		}
	}
}
