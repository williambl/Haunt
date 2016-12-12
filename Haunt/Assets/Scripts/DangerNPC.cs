using UnityEngine;
using System.Collections;

public class DangerNPC : MonoBehaviour {

	RaycastHit ground;
	public float HoverHeight = 2;

	Rigidbody rigid;

	UnityEngine.AI.NavMeshAgent agent;
	public Transform player;

	PlayerController playerController;

	//These variables are dependent on difficulty level
	public float attackStrength;
	public float fov;

	// Use this for initialization
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.autoBraking = false;
		rigid = GetComponent<Rigidbody> ();
		playerController = player.GetComponent<PlayerController> ();
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
		if (Vector3.Distance (transform.position, player.position) < 5 && LineOfSight(player)) {
			Attack (player.gameObject);
			agent.enabled = false;
			rigid.velocity = Vector3.zero;
			playerController.isBeingAttacked = true;
		} else if (Vector3.Distance (transform.position, player.position) < 10) {
			GotoPlayer ();
			playerController.isBeingAttacked = false;
		} else {
			agent.enabled = false;
			rigid.velocity = Vector3.zero;
			playerController.isBeingAttacked = false;
		}
	}

	void GotoPlayer() {
		agent.enabled = true;
		agent.destination = player.position;
	}

	void Attack(GameObject target) {
		Rigidbody targetRigid = target.GetComponent<Rigidbody> ();
		Vector3 attackForce = (transform.position - target.transform.position) * Random.value * attackStrength;
		targetRigid.AddForce (attackForce);
	}

	//Based on http://answers.unity3d.com/answers/20007/view.html
	bool LineOfSight(Transform target) {
		RaycastHit hit;
		if (Vector3.Angle (target.position - transform.position, transform.forward) <= fov &&
		    Physics.Linecast (transform.position, target.position, out hit) &&
		    hit.collider.transform == target) {
				return true;
		}
		return false;
	}
}
