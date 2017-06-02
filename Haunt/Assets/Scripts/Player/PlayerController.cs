using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float rotateSpeed;

	public bool isSprinting;
	public float sprintModifier;

	RaycastHit ground;
	public float HoverHeight = 1;

	public bool isPossessing;
	public GameObject target = null;
	public Camera cam;
	public CameraFollow camFollow;
	public EffectManager effectManager;

	public List<GameObject> attackers = new List<GameObject>();

	Rigidbody rigid;

	public bool dead;

	GameManager manager;

	public GameObject holding;

	public InventoryComponent invComponent;

	public PlayerAbilities abilities;
	public PlayerEnergy energy;

	public bool isInvisible;
	public MeshRenderer meshRend;
	public TrailRenderer trailRend;
	public Light pointLight;

	public PlayerSound sound;

	public GameObject projectilePrefab;

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
		float adjustedRotateSpeed = rotateSpeed;
		float adjustedMoveSpeed = moveSpeed;

		if (manager.gameState == GameState.LOST || manager.gameState == GameState.WON || manager.gameState == GameState.PAUSED)
			return;

		//Movement
		isSprinting = false;
		if (Input.GetButton ("Sprint")) {
			isSprinting = true;
			adjustedRotateSpeed *= sprintModifier;
			adjustedMoveSpeed *= sprintModifier;
		}
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * adjustedRotateSpeed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * adjustedMoveSpeed;
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
		if (Input.GetButtonDown("Possess") && !isBeingAttacked() && !dead && (abilities.abilities & Ability.POSSESS) != 0)
		{
			if (isPossessing) {
				abilities.possess.UnpossessTarget ();
			} else {
				abilities.possess.PossessTarget ();
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
				if (abilities.hold.GetNearestHoldable (2f) != null)
					abilities.hold.PickUp (abilities.hold.GetNearestHoldable (2f));
			} else {
				abilities.hold.Drop ();
			}
		}

		if (energy.energy == 0 && !dead)
			Die ();

		//Invisibility
		if (Input.GetButtonDown("Invisibility") && !dead && (abilities.abilities & Ability.INVISIBILITY) != 0)
		{
			if (isInvisible) {
				abilities.invisible.BecomeVisible ();
			} else {
				abilities.invisible.BecomeInvisible ();
			}
		}

		//Blasting
		if (Input.GetButtonDown("Blast") && !dead && (abilities.abilities & Ability.BLAST) != 0 && energy.energy > 0.25f)
		{
			abilities.blast.BlastInRadius (20);
		}

		//Energy visual effects
		transform.localScale = new Vector3(energy.energy, energy.energy, energy.energy);
		trailRend.startWidth = energy.energy;
		pointLight.intensity = 6 * energy.energy;
		pointLight.range = pointLight.intensity * 20f < 2 ? pointLight.intensity * 20 : 2;
		effectManager.lowEnergy = energy.energy < 0.2 && energy.energy > 0;

		if (Input.GetButtonDown ("FocusedBlast") && !dead && (abilities.abilities & Ability.FOCUSED_BLAST) != 0 && energy.energy > 0.25f) {
			abilities.focusedBlast.Blast ();
		}
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
}
