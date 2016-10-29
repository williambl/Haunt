using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	RaycastHit ground;
	public float HoverHeight = 2;

	NavMeshAgent agent;
	public Transform[] points;
	private int destPoint = 0;
	Vector3 currentWaypoint;

	Rigidbody rigid;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		agent.autoBraking = false;
		GotoNextPoint ();
		rigid = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
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
