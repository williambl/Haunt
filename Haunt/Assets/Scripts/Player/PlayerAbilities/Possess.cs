using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possess : MonoBehaviour {

	public PlayerController pc;

	/// <summary>
	/// Possesses the closest target, taking control of it.
	/// </summary>
	public void PossessTarget ()
	{
		//Getting target
		Collider[] potentialTargets = Physics.OverlapSphere (transform.position, 5, 1 << 10);
		float maxDist = Mathf.Infinity;
		foreach(Collider potentialTarget in potentialTargets)
		{
			if ((potentialTarget.transform.position - transform.position).sqrMagnitude < maxDist && potentialTarget.gameObject.tag == "Possessable") {
				maxDist = (potentialTarget.transform.position - transform.position).sqrMagnitude;
				pc.target = potentialTarget.gameObject;
			}
		}
		if (pc.target != null)
		{
			pc.target.tag = "Possessed";
			pc.camFollow.target = pc.target;
			pc.isPossessing = true;
		}
	}

	/// <summary>
	/// Unpossesses the currently possessed target, releasing it from control.
	/// </summary>
	public void UnpossessTarget ()
	{
		pc.target.tag = "Possessable";
		pc.camFollow.target = gameObject;
		pc.target = null;
		pc.isPossessing = false;
	}
}
