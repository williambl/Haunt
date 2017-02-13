using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileNPC : NPC {

	public Transform player;
	protected PlayerController playerController;

	//These variables are dependent on difficulty level
	public float attackStrength;
	public float attackCooldown;
	public float sightReach;
	public float attackReach;
	public float fov;
	public float playerSeenCooldown;

	protected bool isAttacking;
	protected bool wasAttacking;

	public bool isMobile;

	protected float cooldownTimestamp = 0;

	public float playerSeenTimestamp;

	//Based on http://answers.unity3d.com/answers/20007/view.html
	/// <summary>
	/// Checks the line of sight to the target.
	/// </summary>
	/// <returns><c>true</c>, if there is line of sight to the target, <c>false</c> otherwise.</returns>
	/// <param name="target">Target.</param>
	/// <param name="fieldOfView">Field of view.</param>
	protected bool LineOfSight(Transform target, float fieldOfView) {
		RaycastHit hit;
		if (Vector3.Angle (target.position - transform.position, transform.forward) <= fieldOfView &&
			Physics.Linecast (transform.position, target.position, out hit) &&
			hit.collider.transform == target) {
			return true;
		}
		return false;
	}

	/// <summary>
	/// Determines whether this instance has seen the player.
	/// </summary>
	/// <returns><c>true</c> if this instance has seen the player; otherwise, <c>false</c>.</returns>
	protected bool HasSeenPlayer ()
	{
		if (LineOfSight(player, 360)) {
			playerSeenTimestamp = Time.time;
			return true;
		}
		if (playerSeenTimestamp + playerSeenCooldown > Time.time) {
			return true;
		}
		return false;
	}

	/// <summary>
	/// Set the nav agent destination to the player and enable the nav agent.
	/// </summary>
	protected void GotoPlayer() {
		agent.enabled = true;
		agent.destination = player.position;
	}
}
