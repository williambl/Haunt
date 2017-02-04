using UnityEngine;
using System.Collections;

public class WanderingNPC : NPC {

	public Transform[] points;
	protected int destPoint = 0;

	protected Vector3 currentWaypoint;

	void Update () {
		Hover (HoverHeight);

		//Patrolling
		if (agent.enabled && agent.remainingDistance < 0.5f) {
			GotoNextPoint ();
		}

		if (gameObject.tag == "Possessed") {
			agent.enabled = false;
			rigid.velocity = Vector3.zero;
		} else
			agent.enabled = true;
	}

	/// <summary>
	/// Sets the agent's destination to a random point on an array of points.
	/// Does nothing if no points exist in the array.
	/// </summary>
	void GotoNextPoint() {

		// Returns if no points have been set up
		if (points.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.
		Transform pnt = points[destPoint];
		agent.destination = pnt.position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		destPoint = Random.Range(1,10) % points.Length;
	}
}
