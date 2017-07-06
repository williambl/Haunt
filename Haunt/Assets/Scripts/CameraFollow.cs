using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	//credit: https://code.tutsplus.com/tutorials/unity3d-third-person-cameras--mobile-11230
	public GameObject target;
	Vector3 offset;
	Vector3 originalOffset;

	Vector3 originalDir;
	float originalDist;
	float dist;
	float lerpAmount = 0f;

	int layermask = 1 << 9;

	void Start () {
		originalDir = (transform.position - target.transform.position).normalized;
		originalDist = Vector3.Distance (transform.position, target.transform.position);
		originalOffset = originalDir * originalDist;
		offset = originalOffset;
	}
	
	// LateUpdate is called once per frame, after Update
	void LateUpdate () {
		//RaycastHit hit;
		//Ray ray = new Ray (transform.position, target.transform.position - transform.position);
		//Debug.DrawRay (transform.position, target.transform.position - transform.position);


		//Physics.Raycast (ray, out hit, Vector3.Distance (transform.position, target.transform.position));
		//dist = Vector3.Distance (transform.position, target.transform.position);
		//dist = Mathf.Clamp (dist, 0, Vector3.Distance (transform.position, target.transform.position));

		/*if (Physics.Raycast (ray, out hit, 10)) {
			Debug.Log ("hit");
			if (!hit.collider.CompareTag ("Player")) {
				Debug.Log ("not hit player");
				if (dist > 0)
					offset = Vector3.Lerp(target.transform.position, transform.position, originalDist / dist);
				else
					offset = originalOffset;
			}
		}*/
		//dist = Vector3.Distance (transform.position, target.transform.position);

		Ray ray = new Ray (transform.position, target.transform.position - transform.position);
		RaycastHit hit;

		Debug.DrawRay (transform.position, target.transform.position - transform.position);

		if (Physics.SphereCast (ray, 0.5f, out hit, dist, layermask)) {
			dist -= 0.1f;
			Debug.Log ("something in the way");
		} else {
			if (dist < originalDist)
				dist += 0.1f;
			else
				dist = originalDist;
			Debug.Log ("ayy"); 
		}
		Debug.Log ("Dist: " + dist);

		offset = originalDir * dist;

		Debug.Log ("Offset: " + offset); 
		//float desiredAngle = target.transform.eulerAngles.y;
		//Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
		transform.position = target.transform.position + offset;
		transform.RotateAround (target.transform.position, Vector3.up, target.transform.eulerAngles.y);

		transform.LookAt(target.transform);
	}
}
