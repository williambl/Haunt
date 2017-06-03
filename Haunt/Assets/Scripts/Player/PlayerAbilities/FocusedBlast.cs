using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusedBlast : MonoBehaviour {

	public PlayerController pc;

	public void Blast ()
	{
		pc.sound.Blast ();
		Rigidbody projectileRigid = Instantiate (pc.projectilePrefab, transform.position, transform.rotation).GetComponent<Rigidbody> ();

		Ray ray = pc.cam.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			projectileRigid.AddForce ((hit.point - transform.position).normalized * 200);
		} else {
			projectileRigid.AddForce (((ray.origin + (ray.direction * 20) ) - transform.position).normalized * 200);
		}
		pc.energy.LoseEnergy (0.25f);

	}
}
