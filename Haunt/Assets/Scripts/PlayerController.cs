using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float rotateSpeed;

	RaycastHit ground;
	public float HoverHeight = 1;

	public bool isPossessing;
	GameObject target = null;
	public Camera cam;
	public CameraFollow camFollow;

	// Use this for initialization
	void Start () {
		camFollow.target = gameObject;
	}
	
	// Update is called once per frame
	void Update () {

		//Movement
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
		if (isPossessing) {
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
			if (isPossessing) {
				Unposess ();
			} else {
				Posess ();
			}
		}

		//Hovering
		if (Physics.Raycast (transform.position, Vector3.down, out ground)) 
		{
			if (ground.transform.gameObject.layer == 8)
			{
				transform.position = new Vector3(transform.position.x, ground.point.y + HoverHeight, transform.position.z);
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
			if ((potentialTarget.transform.position - transform.position).sqrMagnitude < maxDist && potentialTarget.gameObject.tag == "Possessable") {
				maxDist = (potentialTarget.transform.position - transform.position).sqrMagnitude;
				target = potentialTarget.gameObject;
				Debug.Log (target.name);
			}
		}
		Debug.Log("Final Target is: " + target.name);
		if (target != null)
		{
			target.tag = "Possessed";
			camFollow.target = target;
			isPossessing = true;
		}
	}

	void Unposess ()
	{
		target.tag = "Possessable";
		camFollow.target = gameObject;
		target = null;
		isPossessing = false;
	}
}
