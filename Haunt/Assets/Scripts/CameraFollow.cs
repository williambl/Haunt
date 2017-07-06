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
		//Create a ray to the target
		Ray ray = new Ray (transform.position, target.transform.position - transform.position);
		RaycastHit hit;

		Debug.DrawRay (transform.position, target.transform.position - transform.position);

		if (Physics.SphereCast (ray, 0.5f, out hit, dist, layermask)) {
			dist = Mathf.SmoothDamp (dist, dist - 0.1f, ref lerpAmount, 0.2f);
			Debug.Log ("something in the way, going closer");
		} else {
			Debug.Log ("nothing is in the way"); 

			if (dist < originalDist) {
				float tmpDist = Mathf.SmoothDamp (dist, dist + 0.1f, ref lerpAmount, 0.2f);
				Vector3 tmpOffset = originalDir * tmpDist;
				Vector3 tmpPos = target.transform.position + tmpOffset;
				Ray tmpRay = new Ray (tmpPos, target.transform.position - transform.position);
				RaycastHit tmpHit;

				Debug.DrawRay (tmpPos, target.transform.position - transform.position, Color.cyan);
				if (!Physics.SphereCast (tmpRay, 0.5f, out tmpHit, tmpDist, layermask)) {
					dist = tmpDist;
					Debug.Log ("nothing will be in the way, going further");
				}
			}
			else
				dist = originalDist;
		}
		Debug.Log ("Dist: " + dist);

		offset = originalDir * dist;

		Debug.Log ("Offset: " + offset); 

		transform.position = target.transform.position + offset;
		transform.RotateAround (target.transform.position, Vector3.up, target.transform.eulerAngles.y);

		transform.LookAt(target.transform);
	}
}
