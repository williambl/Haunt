using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour {

	public PlayerController pc;

	public void BlastInRadius (float radius)
	{
		pc.sound.Blast ();
		foreach (Collider coll in Physics.OverlapSphere(transform.position, radius, 1 << 10)) {
			if (Vector3.Distance (transform.position, coll.transform.position) < radius / 2.5f || coll.GetComponent<NPCHealth> ().health < 0.2)
				coll.GetComponent<NPCHealth> ().LoseHealth (1);
			else
				coll.GetComponent<NPCHealth> ().LoseHealth((1 - (Vector3.Distance (transform.position, coll.transform.position) / radius)));
		}
		pc.energy.LoseEnergy (0.25f);
	}

}
