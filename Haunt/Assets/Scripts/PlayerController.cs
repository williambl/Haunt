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
	public EffectManager effectManager;

	public bool isBeingAttacked;
	public bool isBeingCaught;

	Rigidbody rigid;

	public bool dead;

	GameManager manager;

	public GameObject holding;
	public Item holdingitem;

	void Start () {
		camFollow.target = gameObject;
		rigid = GetComponent<Rigidbody> ();
		manager = GameObject.Find ("Manager").GetComponent<GameManager> ();
	}
	
	void Update () {

		//Movement
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
		if (isPossessing) {
			Debug.Log ("moving " + target.name);
			target.transform.Translate (0, 0, z);
			target.transform.Rotate (0, x, 0);
		} else if (!isBeingCaught && !dead) {
			transform.Translate (0, 0, z);
			transform.Rotate (0, x, 0);
		}


		//Possesion
		if (Input.GetButtonDown("Posess") && !isBeingAttacked && !isBeingCaught && !dead)
		{
			if (isPossessing) {
				UnposessTarget ();
			} else {
				PosessTarget ();
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

		//Stops player from being pushed away after attacking ends
		if (!isBeingAttacked)
		{
			rigid.velocity = Vector3.zero;
			rigid.angularVelocity = Vector3.zero;
		}
		effectManager.attacked = isBeingAttacked;
		effectManager.dead = dead;
	
		//Dying
		if (isBeingCaught) {
			Die();
		}

		//Picking up/dropping
		if (Input.GetButtonDown ("PickUp")) {
			if (GetNearestHoldable (5f) != null) {
				if (holding == null) 
					PickUp (GetNearestHoldable (5f));
				else
					Drop ();
			}
		}

	}

	/// <summary>
	/// Posesses the closest target, taking control of it.
	/// </summary>
	void PosessTarget ()
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

	/// <summary>
	/// Unposesses the currently possessed target, releasing it from control.
	/// </summary>
	void UnposessTarget ()
	{
		target.tag = "Possessable";
		camFollow.target = gameObject;
		target = null;
		isPossessing = false;
	}

	/// <summary>
	/// Makes the player die and loses the game.
	/// </summary>
	void Die ()
	{
		dead = true;
		manager.lost = true;
	}

	/// <summary>
	/// Picks up a target.
	/// </summary>
	/// <param name="target">Target to pick up.</param>
	void PickUp (GameObject target)
	{
		target.transform.parent = transform;
		holding = target;
		holdingitem = target.GetComponent<ItemComponent> ().item;
		target.transform.position = transform.position;
	}

	/// <summary>
	/// Drop the currently held object.
	/// </summary>
	void Drop ()
	{
		holding.transform.parent = null;
		holding = null;
	}
		
	/// <summary>
	/// Gets the nearest holdable GameObject in the radius.
	/// Returns null if no holdable GameObjects are found in the radius specified.
	/// </summary>
	/// <param name="radius">Radius to search within.</param>
	GameObject GetNearestHoldable (float radius)
	{
		float dist = Mathf.Infinity;
		GameObject closest = null;

		foreach (Collider coll in Physics.OverlapSphere(transform.position, radius)) {
			if (Vector3.Distance (transform.position, coll.transform.position) < dist && coll.gameObject.layer == 11) {
				dist = Vector3.Distance (transform.position, coll.transform.position);
				closest = coll.gameObject;
			}
		}
		return closest;
	}
}
