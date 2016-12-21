using UnityEngine;
using System.Collections;

public class DangerNPC : MonoBehaviour {

	RaycastHit ground;
	public float HoverHeight = 2;

	Rigidbody rigid;

	GameManager manager;

	UnityEngine.AI.NavMeshAgent agent;
	public Transform player;

	PlayerController playerController;

	LineRenderer line;

	bool isAttacking;
	bool wasAttacking;
	float cooldownTimestamp = 0;

	//These variables are dependent on difficulty level
	public float attackStrength;
	public float attackCooldown;
	public float fov;
	public float sightReach;
	public float attackReach;


	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.autoBraking = false;
		rigid = GetComponent<Rigidbody> ();
		playerController = player.GetComponent<PlayerController> ();
		line = GetComponent<LineRenderer> ();
		line.enabled = false;
		manager = GameObject.Find ("Manager").GetComponent<GameManager> ();

		switch (manager.difficultyLevel) {
		case Difficulty.EASY:
			attackStrength = 2;
			attackCooldown = 7;
			fov = 90;
			sightReach = 7;
			attackReach = 4;
			break;
		case Difficulty.NORMAL:
			attackStrength = 5;
			attackCooldown = 5;
			fov = 120;
			sightReach = 10;
			attackReach = 5;
			break;
		case Difficulty.HARD:
			attackStrength = 7;
			attackCooldown = 2;
			fov = 270;
			sightReach = 15;
			attackReach = 8;
			break;
		}
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
		wasAttacking = isAttacking;
		if (Vector3.Distance (transform.position, player.position) < 0.3) {
			Catch (player.gameObject);
			agent.enabled = false;
			rigid.velocity = Vector3.zero;
			playerController.isBeingAttacked = false;
			playerController.isBeingCaught = true;
			isAttacking = true;
		} else if (Vector3.Distance (transform.position, player.position) < attackReach && LineOfSight(player)) {
			if (wasAttacking) {
				Attack (player.gameObject);
				agent.enabled = false;
				rigid.velocity = Vector3.zero;
				playerController.isBeingAttacked = true;
				playerController.isBeingCaught = false;
				isAttacking = true;
			} else if (Time.time > cooldownTimestamp + attackCooldown) {
				Attack (player.gameObject);
				agent.enabled = false;
				rigid.velocity = Vector3.zero;
				playerController.isBeingAttacked = true;
				playerController.isBeingCaught = false;
				isAttacking = true;
			} else {
				agent.enabled = false;
				rigid.velocity = Vector3.zero;
				playerController.isBeingAttacked = false;
				playerController.isBeingCaught = false;
				isAttacking = false;
			}
		} else if (Vector3.Distance (transform.position, player.position) < sightReach) {
			GotoPlayer ();
			playerController.isBeingAttacked = false;
			playerController.isBeingCaught = false;
			line.enabled = false;
			isAttacking = false;

		} else {
			agent.enabled = false;
			rigid.velocity = Vector3.zero;
			playerController.isBeingAttacked = false;
			playerController.isBeingCaught = false;
			line.enabled = false;
			isAttacking = false;
		}
	}

	void GotoPlayer() {
		agent.enabled = true;
		agent.destination = player.position;
	}

	void Attack(GameObject target) {
		cooldownTimestamp = Time.time;
		line.enabled = true;
		line.SetPositions (new Vector3[2]{ transform.position, player.transform.position });

		Rigidbody targetRigid = target.GetComponent<Rigidbody> ();
		Vector3 attackForce = (transform.position - target.transform.position) * Random.value * attackStrength;
		targetRigid.AddForce (attackForce);
	}

	void Catch(GameObject target) {
		line.enabled = false;
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
