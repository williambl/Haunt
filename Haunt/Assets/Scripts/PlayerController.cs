using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public List<GameObject> attackers = new List<GameObject>();

	Rigidbody rigid;

	public bool dead;

	GameManager manager;

	public GameObject holding;

	InventoryComponent invComponent;

	public Abilities abilities;
	public PlayerEnergy energy;

	void Start () {
		camFollow.target = gameObject;
		rigid = GetComponent<Rigidbody> ();
		manager = GameObject.Find ("Manager").GetComponent<GameManager> ();
		invComponent = GetComponent<InventoryComponent> ();
		abilities = GetComponent<Abilities> ();
		energy = GetComponent<PlayerEnergy> ();
	}
	
	void Update () {
		
		//Movement
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
		if (isPossessing) {
			Debug.Log ("moving " + target.name);
			target.transform.Translate (0, 0, z);
			target.transform.Rotate (0, x, 0);
		} else if (!dead) {
			transform.Translate (0, 0, z);
			transform.Rotate (0, x, 0);
		}


		//Possesion
		if (Input.GetButtonDown("Possess") && !isBeingAttacked() && !dead && abilities.possess)
		{
			if (isPossessing) {
				UnpossessTarget ();
			} else {
				PossessTarget ();
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
		if (!isBeingAttacked())
		{
			rigid.velocity = Vector3.zero;
			rigid.angularVelocity = Vector3.zero;
		}
		effectManager.attacked = isBeingAttacked();
		effectManager.dead = dead;

		//Picking up/dropping
		if (Input.GetButtonDown ("PickUp")) {
			if (holding == null) {
				if (GetNearestHoldable (2f) != null)
					PickUp (GetNearestHoldable (2f));
			} else {
				Drop ();
			}
		}

		if (energy.energy == 0 && !dead)
			Die ();
	}

	/// <summary>
	/// Possesses the closest target, taking control of it.
	/// </summary>
	void PossessTarget ()
	{
		//Getting target
		Collider[] potentialTargets = Physics.OverlapSphere (transform.position, 5, 1 << 10);
		Debug.Log(potentialTargets.Length.ToString());
		float maxDist = Mathf.Infinity;
		foreach(Collider potentialTarget in potentialTargets)
		{
			if ((potentialTarget.transform.position - transform.position).sqrMagnitude < maxDist && potentialTarget.gameObject.tag == "Possessable") {
				maxDist = (potentialTarget.transform.position - transform.position).sqrMagnitude;
				target = potentialTarget.gameObject;
			}
		}
		if (target != null)
		{
			target.tag = "Possessed";
			camFollow.target = target;
			isPossessing = true;
		}
	}

	/// <summary>
	/// Unpossesses the currently possessed target, releasing it from control.
	/// </summary>
	public void UnpossessTarget ()
	{
		target.tag = "Possessable";
		camFollow.target = gameObject;
		target = null;
		isPossessing = false;
	}

	/// <summary>
	/// Makes the player die and loses the game.
	/// </summary>
	public void Die ()
	{
		dead = true;
		manager.lost = true;
		energy.energy = 0;
	}

	/// <summary>
	/// Picks up a target.
	/// </summary>
	/// <param name="target">Target to pick up.</param>
	public void PickUp (GameObject target)
	{
		target.transform.parent = transform;
		holding = target;
		invComponent.holdingitem = holding.GetComponent<ItemComponent> ().item;
		holding.GetComponent<ItemComponent> ().isHeld = true;
		target.transform.position = transform.position;
	}

	/// <summary>
	/// Drop the currently held object.
	/// </summary>
	public void Drop ()
	{
		holding.transform.parent = null;
		holding.GetComponent<ItemComponent> ().isHeld = false;
		invComponent.holdingitem = null;
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
			if (Vector3.Distance (transform.position, coll.transform.position) < dist && coll.gameObject.layer == 11 && coll.gameObject.GetComponent<ItemComponent> ().isHeld == false) {
				dist = Vector3.Distance (transform.position, coll.transform.position);
				closest = coll.gameObject;
			}
		}
		return closest;
	}

	public bool isBeingAttacked ()
	{
		return !(attackers.Count == 0);
	}

	public void addAttacker (GameObject attacker)
	{
		if (!attackers.Contains (attacker))
			attackers.Add (attacker);
	}

	public void removeAttacker (GameObject attacker)
	{
		if (attackers.Contains (attacker))
			attackers.Remove (attacker);
	}
}
