using UnityEngine;
using System.Collections;

public class DangerNPC : MonoBehaviour {

	RaycastHit ground;
	public float HoverHeight = 2;

	Rigidbody rigid;

	UnityEngine.AI.NavMeshAgent agent;
	public Transform player;

	// Use this for initialization
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.autoBraking = false;
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

		//Patrolling and attacking
		if (Physics.OverlapSphere (transform.position, 5f, 1 << 9).Length == 1)
			GotoPlayer ();
		else {
			agent.enabled = false;
			rigid.velocity = Vector3.zero;
		}
	}

	void GotoPlayer() {
		agent.enabled = true;
		agent.destination = player.position;
	}
}
