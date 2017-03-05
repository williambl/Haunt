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

	public PlayerAbilities abilities;
	public PlayerEnergy energy;

	public bool isInvisible;
	MeshRenderer meshRend;
	TrailRenderer trailRend;
	Light pointLight;

	PlayerSound sound;

	void Start () {
		camFollow.target = gameObject;
		rigid = GetComponent<Rigidbody> ();
		manager = GameObject.Find ("Manager").GetComponent<GameManager> ();
		invComponent = GetComponent<InventoryComponent> ();
		abilities = GetComponent<PlayerAbilities> ();
		energy = GetComponent<PlayerEnergy> ();
		meshRend = GetComponent<MeshRenderer> ();
		trailRend = GetComponent<TrailRenderer> ();
		pointLight = GetComponent<Light> ();
		sound = GetComponent<PlayerSound> ();
	}

	void FixedUpdate ()
	{
		if (manager.gameState == GameState.LOST || manager.gameState == GameState.WON || manager.gameState == GameState.PAUSED)
			return;

		//Movement
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
		if (isPossessing) {
			target.transform.Translate (0, 0, z);
			target.transform.Rotate (0, x, 0);
		} else if (!dead) {
			transform.Translate (0, 0, z);
			transform.Rotate (0, x, 0);
		}
	}
	
	void Update () {

		if (manager.gameState == GameState.LOST || manager.gameState == GameState.WON)
			return;

		//Pausing
		if (Input.GetButtonDown ("Cancel"))
			manager.TogglePause ();

		if (manager.gameState == GameState.PAUSED)
			return;

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
		if (Physics.Raycast (transform.position, Vector3.down, out ground, 100, 1 << 8)) 
		{
			transform.position = new Vector3 (transform.position.x, ground.point.y + HoverHeight, transform.position.z);
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
		if (Input.GetButtonDown ("Use")) {
			if (holding == null) {
				if (GetNearestHoldable (2f) != null)
					PickUp (GetNearestHoldable (2f));
			} else {
				Drop ();
			}
		}

		if (energy.energy == 0 && !dead)
			Die ();

		//Invisibility
		if (Input.GetButtonDown("Invisibility") && !dead && abilities.invisibility)
		{
			if (isInvisible) {
				BecomeVisible ();
			} else {
				BecomeInvisible ();
			}
		}

		//Blasting
		if (Input.GetButtonDown("Blast") && !dead && abilities.blast && energy.energy > 0.25f)
		{
			Blast (20);
		}

		//Energy visual effects
		transform.localScale = new Vector3(energy.energy, energy.energy, energy.energy);
		trailRend.startWidth = energy.energy;
		pointLight.intensity = 6 * energy.energy;
		pointLight.range = pointLight.intensity * 20f < 2 ? pointLight.intensity * 20 : 2;
		effectManager.lowEnergy = energy.energy < 0.2 && energy.energy > 0;

		if (Input.GetButtonDown ("FocusedBlast") && !dead && abilities.blast && energy.energy > 0.25f) {
			
		}
	}

	/// <summary>
	/// Possesses the closest target, taking control of it.
	/// </summary>
	public void PossessTarget ()
	{
		//Getting target
		Collider[] potentialTargets = Physics.OverlapSphere (transform.position, 5, 1 << 10);
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
		if (!attackers.Contains (attacker)) {
			attackers.Add (attacker);
		}
	}

	public void removeAttacker (GameObject attacker)
	{
		if (attackers.Contains (attacker)) {
			attackers.Remove (attacker);
		}
	}

	/// <summary>
	/// Makes the player invisible.
	/// </summary>
	public void BecomeInvisible ()
	{
		isInvisible = true;
		meshRend.enabled = false;
		trailRend.enabled = false;
		pointLight.enabled = false;
	}

	/// <summary>
	/// Makes the player visible.
	/// </summary>
	public void BecomeVisible ()
	{
		isInvisible = false;
		meshRend.enabled = true;
		trailRend.enabled = true;
		pointLight.enabled = true;
	}

	public void Blast (float radius)
	{
		sound.Blast ();
		foreach (Collider coll in Physics.OverlapSphere(transform.position, radius, 1 << 10)) {
			if (Vector3.Distance (transform.position, coll.transform.position) < radius / 2.5f || coll.GetComponent<NPCHealth> ().health < 0.2)
				coll.GetComponent<NPCHealth> ().LoseHealth (1);
			else
				coll.GetComponent<NPCHealth> ().LoseHealth((1 - (Vector3.Distance (transform.position, coll.transform.position) / radius)));
		}
		energy.LoseEnergy (0.25f);
	}
}
