using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusedBlast : MonoBehaviour {

	public PlayerController pc;

	public void Blast ()
	{
		pc.sound.Blast ();
		Rigidbody projectileRigid = Instantiate (pc.projectilePrefab, transform.position, transform.rotation).GetComponent<Rigidbody> ();
		projectileRigid.AddForce (transform.forward * 200);
		pc.energy.LoseEnergy (0.25f);
	}
}
