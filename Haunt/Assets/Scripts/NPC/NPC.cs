using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	RaycastHit ground;
	public float HoverHeight = 2;

	UnityEngine.AI.NavMeshAgent agent;
	public Transform[] points;
	private int destPoint = 0;
	Vector3 currentWaypoint;

	Rigidbody rigid;

	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.autoBraking = false;
		GotoNextPoint ();
		rigid = GetComponent<Rigidbody> ();
	}
	
	void Update () {
		//Hovering
		if (Physics.Raycast (transform.position, Vector3.down, out ground)) 
		{
			if (ground.transform.gameObject.layer == 8)
			{
				transform.position = new Vector3(transform.position.x, ground.point.y + HoverHeight, transform.position.z);
			}
		}

		//Patrolling
		if (agent.enabled && agent.remainingDistance < 0.5f)
			GotoNextPoint ();

		if (gameObject.tag == "Possessed"){
			agent.enabled = false;
			rigid.velocity = Vector3.zero;
		} else
			agent.enabled = true;
	}

	/// <summary>
	/// Sets the agent's destination to a random point on an array of points.
	/// Does nothing if no points exists in the array.
	/// </summary>
	void GotoNextPoint() {
		// Returns if no points have been set up
		if (points.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.
		agent.destination = points[destPoint].position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		destPoint = Random.Range(1,10) % points.Length;
	}
}
