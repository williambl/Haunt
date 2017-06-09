using UnityEngine;
using System.Collections;

public class PullNPC : HostileNPC {

	LineRenderer line;

	new void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.autoBraking = false;
		rigid = GetComponent<Rigidbody> ();
		playerController = player.GetComponent<PlayerController> ();
		line = GetComponent<LineRenderer> ();
		line.enabled = false;
		manager = GameObject.FindGameObjectWithTag ("Manager").GetComponent<GameManager> ();

		switch (manager.difficultyLevel) {
		case Difficulty.EASY:
			attackStrength = 2;
			attackCooldown = 7;
			fov = 90;
			sightReach = 7 * sightReachMultiplier;
			attackReach = 4 * attackReachMultiplier;
			playerSeenCooldown = 5;
			break;
		case Difficulty.NORMAL:
			attackStrength = 5;
			attackCooldown = 5;
			fov = 120;
			sightReach = 10 * sightReachMultiplier;
			attackReach = 5 * attackReachMultiplier;
			playerSeenCooldown = 5;
			break;
		case Difficulty.HARD:
			attackStrength = 7;
			attackCooldown = 2;
			fov = 270;
			sightReach = 15 * sightReachMultiplier;
			attackReach = 8 * attackReachMultiplier;
			playerSeenCooldown = 10;
			break;
		}
	}

	void Update () {
		Hover (HoverHeight);

		if (manager.gameState == GameState.PAUSED || manager.gameState == GameState.WON || manager.gameState == GameState.LOST) {
			agent.enabled = false;
			rigid.velocity = Vector3.zero;
			return;
		}

		//Patrolling and attacking
		//TODO: rewrite and make more readable
		wasAttacking = isAttacking;
		if (Vector3.Distance (transform.position, player.position) < 0.3 && !playerController.isInvisible) {
			Catch (player.gameObject);
			agent.enabled = false;
			rigid.velocity = Vector3.zero;
			playerController.removeAttacker (gameObject);
			playerController.Die();
			isAttacking = true;
			playerSeenTimestamp = Time.time;
		} else if (Vector3.Distance (transform.position, player.position) < attackReach && LineOfSight(player, fov) && !playerController.isInvisible) {
			if (wasAttacking) {
				Attack (player.gameObject);
				agent.enabled = false;
				rigid.velocity = Vector3.zero;
				playerController.addAttacker (gameObject);
				isAttacking = true;
			} else if (Time.time > cooldownTimestamp + attackCooldown) {
				Attack (player.gameObject);
				agent.enabled = false;
				rigid.velocity = Vector3.zero;
				playerController.addAttacker (gameObject);
				isAttacking = true;
			} else {
				agent.enabled = false;
				rigid.velocity = Vector3.zero;
				playerController.removeAttacker (gameObject);
				isAttacking = false;
			}
			playerSeenTimestamp = Time.time;
		} else if (Vector3.Distance (transform.position, player.position) < sightReach && !playerController.isInvisible && HasSeenPlayer() && isMobile) {
			GotoPlayer ();
			playerController.removeAttacker (gameObject);
			line.enabled = false;
			isAttacking = false;
			playerSeenTimestamp = Time.time;
		} else if (Vector3.Distance (transform.position, player.position) < sightReach && !playerController.isInvisible && LineOfSight (player, fov) && isMobile) {
			GotoPlayer ();
			playerController.removeAttacker (gameObject);
			line.enabled = false;
			isAttacking = false;
			playerSeenTimestamp = Time.time;
		} else {
			agent.enabled = false;
			rigid.velocity = Vector3.zero;
			playerController.removeAttacker (gameObject);
			line.enabled = false;
			isAttacking = false;
		}
	}

	/// <summary>
	/// Attack the specified target.
	/// </summary>
	/// <param name="target">Target to attack.</param>
	void Attack(GameObject target) {
		cooldownTimestamp = Time.time;
		line.enabled = true;
		line.SetPositions (new Vector3[2]{ transform.position, player.transform.position });

		Rigidbody targetRigid = target.GetComponent<Rigidbody> ();
		Vector3 attackForce = (transform.position - target.transform.position) * Random.value * attackStrength;
		targetRigid.AddForce (attackForce);
	}

	/// <summary>
	/// Catch the specified target. Currently just disables the line.
	/// </summary>
	/// <param name="target">Target to catch.</param>
	void Catch(GameObject target) {
		line.enabled = false;
	}

	public override void Die()
	{
		if (dropObject != null)
			Drop (dropObject);
		playerController.removeAttacker (gameObject);
		isAttacking = false;
		Destroy (gameObject);
	}
}
