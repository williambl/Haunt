using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingNPC : HostileNPC {

	LineRenderer line;

	PlayerEnergy playerEnergy;

	// Use this for initialization
	new void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.autoBraking = false;
		rigid = GetComponent<Rigidbody> ();
		playerController = player.GetComponent<PlayerController> ();
		line = GetComponent<LineRenderer> ();
		line.enabled = false;
		manager = GameObject.Find ("Manager").GetComponent<GameManager> ();

		switch (manager.difficultyLevel) {
		case Difficulty.EASY:
			attackStrength = 0.07f;
			attackCooldown = 1f;
			fov = 60;
			sightReach = 10;
			attackReach = 6;
			break;
		case Difficulty.NORMAL:
			attackStrength = 0.07f;
			attackCooldown = 0.5f;
			fov = 90;
			sightReach = 10;
			attackReach = 7;
			break;
		case Difficulty.HARD:
			attackStrength = 0.1f;
			attackCooldown = 0.5f;
			fov = 100;
			sightReach = 15;
			attackReach = 10;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Hover (HoverHeight);

		if (Vector3.Distance (transform.position, player.position) < attackReach && LineOfSight(player) && !playerController.isInvisible && Time.time > cooldownTimestamp + attackCooldown) {
			Attack (player.gameObject);
			line.enabled = true;
			agent.enabled = false;
			rigid.velocity = Vector3.zero;
			isAttacking = true;
		} else if (Vector3.Distance (transform.position, player.position) < sightReach && !playerController.isInvisible && Time.time > cooldownTimestamp + attackCooldown && isMobile) {
			GotoPlayer ();
			line.enabled = false;
			isAttacking = false;
		} else {
			line.enabled = false;
			agent.enabled = false;
			rigid.velocity = Vector3.zero;
			isAttacking = false;
		}
	}

	/// <summary>
	/// Attack the specified target.
	/// </summary>
	/// <param name="target">Target to attack.</param>
	void Attack (GameObject target) {
		line.SetPositions (new Vector3[2]{ transform.position, player.transform.position });
		cooldownTimestamp = Time.time;
		playerEnergy = target.GetComponent<PlayerEnergy> ();
		playerEnergy.energy = playerEnergy.energy - attackStrength * playerEnergy.drainAmount;
	}
}
