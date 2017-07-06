using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	//credit: https://code.tutsplus.com/tutorials/unity3d-third-person-cameras--mobile-11230
	public GameObject target;
	Vector3 offset;

	Vector3 dir;
	float originalDist;
	float dist;

	float moveAmount = 0.1f;
	float smoothAmount = 0f;
	float smoothTime = 0.1f;

	float sphereCastRadius = 0.5f;

	int layermask = 1 << 9;

	void Start () {
		dir = (transform.position - target.transform.position).normalized;
		originalDist = Vector3.Distance (transform.position, target.transform.position);
		offset = dir * originalDist;
	}
	
	// LateUpdate is called once per frame, after Update
	void LateUpdate () {
		//Create a ray to the target
		Ray ray = new Ray (transform.position, target.transform.position - transform.position);
		RaycastHit hit;

		// If a 0.5 radius spherecast hitting everything but the player hits anything, reduce distance to player
		if (Physics.SphereCast (ray, sphereCastRadius, out hit, dist, layermask)) {
			dist = Mathf.SmoothDamp (dist, dist - moveAmount, ref smoothAmount, smoothTime);
		} else {
			
			/* 
			 * If we are closer than we were originally,
			 * we check whether if we did go further away
			 * we would just end up with something in the way.
			 */

			if (dist < originalDist) {
				float tmpDist = Mathf.SmoothDamp (dist, dist + moveAmount, ref smoothAmount, smoothTime);
				Vector3 tmpOffset = dir * tmpDist;
				Vector3 tmpPos = target.transform.position + tmpOffset;
				Ray tmpRay = new Ray (tmpPos, target.transform.position - transform.position);
				RaycastHit tmpHit;

				/*
				 * If we will not end up with anything in the way,
				 * then go further.
				 */

				if (!Physics.SphereCast (tmpRay, sphereCastRadius, out tmpHit, tmpDist, layermask)) {
					dist = tmpDist;
				}
			}
			else
				dist = originalDist;
		}

		// This is the offset between the player and the camera - direction times distance.
		offset = dir * dist;

		//Position is target position plus offset between us and player.
		transform.position = target.transform.position + offset;

		//Rotate ourselves the players rotation around the player, then look at the player.
		transform.RotateAround (target.transform.position, Vector3.up, target.transform.eulerAngles.y);
		transform.LookAt(target.transform);
	}
}
