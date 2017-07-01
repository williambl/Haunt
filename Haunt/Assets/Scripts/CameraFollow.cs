using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	//credit: https://code.tutsplus.com/tutorials/unity3d-third-person-cameras--mobile-11230
	public GameObject target;
	Vector3 offset;
	Vector3 originalOffset;
	float originalDist;
	float dist;
	float lerpAmount = 0f;

	void Start () {
		originalOffset = (target.transform.position - transform.position).normalized * 3;
		offset = originalOffset;
		originalDist = Vector3.Distance (transform.position, target.transform.position);
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

		Ray ray = new Ray (transform.position, target.transform.position);
		RaycastHit hit;

		Debug.DrawRay (transform.position, target.transform.position);

		if (Physics.SphereCast (ray, 0.5f, out hit, dist, ~(1 << 9))) {
			dist -= 0.01f;
		} else
			dist = originalDist;

		offset = (target.transform.position - transform.position).normalized * dist;
		float desiredAngle = target.transform.eulerAngles.y;
		Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
		transform.position = target.transform.position - (rotation * offset);

		transform.LookAt(target.transform);
	}
}
