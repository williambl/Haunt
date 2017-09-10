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

		switch (GameManager.difficultyLevel) {
		case Difficulty.EASY:
			attackStrength = 0.07f;
			attackCooldown = 1f;
			fov = 60;
			sightReach = 10 * sightReachMultiplier;
			attackReach = 6 * attackReachMultiplier;
			playerSeenCooldown = 5;
			break;
		case Difficulty.NORMAL:
			attackStrength = 0.07f;
			attackCooldown = 0.5f;
			fov = 90;
			sightReach = 10 * sightReachMultiplier;
			attackReach = 7 * attackReachMultiplier;
			playerSeenCooldown = 5;
			break;
		case Difficulty.HARD:
			attackStrength = 0.1f;
			attackCooldown = 0.5f;
			fov = 100;
			sightReach = 15 * sightReachMultiplier;
			attackReach = 10 * attackReachMultiplier;
			playerSeenCooldown = 10;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Hover (HoverHeight);

		if (GameManager.gameState == GameState.PAUSED || GameManager.gameState == GameState.WON || GameManager.gameState == GameState.LOST) {
			agent.enabled = false;
			rigid.velocity = Vector3.zero;
			return;
		}

		if (Vector3.Distance (transform.position, player.position) < attackReach && LineOfSight(player, fov) && !playerController.isInvisible && Time.time > cooldownTimestamp + attackCooldown) {
			Attack (player.gameObject);
			line.enabled = true;
			agent.enabled = false;
			rigid.velocity = Vector3.zero;
			isAttacking = true;
			playerSeenTimestamp = Time.time;
		} else if (Vector3.Distance (transform.position, player.position) < sightReach && !playerController.isInvisible && Time.time > cooldownTimestamp + attackCooldown && HasSeenPlayer() && isMobile) {
			GotoPlayer ();
			line.enabled = false;
			isAttacking = false;
			playerSeenTimestamp = Time.time;
		} else if (Vector3.Distance (transform.position, player.position) < sightReach && !playerController.isInvisible && Time.time > cooldownTimestamp + attackCooldown && LineOfSight(player, fov) && isMobile) {
			GotoPlayer ();
			line.enabled = false;
			isAttacking = false;
			playerSeenTimestamp = Time.time;
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
