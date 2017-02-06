using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileNPC : NPC {

	public Transform player;
	protected PlayerController playerController;

	protected float fov;
	protected bool isAttacking;
	protected bool wasAttacking;

	public bool isMobile;

	//Based on http://answers.unity3d.com/answers/20007/view.html
	/// <summary>
	/// Checks the line of sight to the target.
	/// </summary>
	/// <returns><c>true</c>, if there is line of sight to the target, <c>false</c> otherwise.</returns>
	/// <param name="target">Target.</param>
	protected bool LineOfSight(Transform target) {
		RaycastHit hit;
		if (Vector3.Angle (target.position - transform.position, transform.forward) <= fov &&
			Physics.Linecast (transform.position, target.position, out hit) &&
			hit.collider.transform == target) {
			return true;
		}
		return false;
	}
}
