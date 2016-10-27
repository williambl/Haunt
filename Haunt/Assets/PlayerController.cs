using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float rotateSpeed;

	public bool isPosessing;
	GameObject target = null;
	public Camera cam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Movement
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
		if (isPosessing) {
			Debug.Log ("moving " + target.name);
			target.transform.Translate (0, 0, z);
			target.transform.Rotate (0, x, 0);
		} else {
			transform.Translate (0, 0, z);
			transform.Rotate (0, x, 0);
		}


		//Possesion
		if (Input.GetButtonDown("Posess"))
		{
			Debug.Log ("got button");
			if (isPosessing) {
				Unposess ();
			} else {
				Posess ();
			}
		}
	}

	void Posess ()
	{
		//Getting target
		Debug.Log("looking for targets");
		Collider[] potentialTargets = Physics.OverlapSphere (transform.position, 5, 1 << 10);
		Debug.Log(potentialTargets.Length.ToString());
		float maxDist = Mathf.Infinity;
		foreach(Collider potentialTarget in potentialTargets)
		{
			Debug.Log (potentialTarget.gameObject.name);
			if ((potentialTarget.transform.position - transform.position).sqrMagnitude < maxDist) {
				maxDist = (potentialTarget.transform.position - transform.position).sqrMagnitude;
				target = potentialTarget.gameObject;
				Debug.Log (target.name);
			}
		}
		Debug.Log("Final Target is: " + target.name);
		if (target != null)
		{
			target.tag = "Posessed";
			isPosessing = true;
			cam.transform.parent = target.transform;
		}
	}

	void Unposess ()
	{
		target.tag = "Posessable";
		target = null;
		isPosessing = false;
		cam.transform.parent = gameObject.transform;
	}
}
