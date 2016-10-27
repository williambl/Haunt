using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	//credit: https://code.tutsplus.com/tutorials/unity3d-third-person-cameras--mobile-11230
	public GameObject target;
	Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = target.transform.position - transform.position;
	}
	
	// LateUpdate is called once per frame, after Update
	void LateUpdate () {
		float desiredAngle = target.transform.eulerAngles.y;
		Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
		transform.position = target.transform.position - (rotation * offset);

		transform.LookAt(target.transform);
	}
}
