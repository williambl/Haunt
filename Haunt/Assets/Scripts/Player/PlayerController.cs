﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	[Header("References:")]
	public Camera cam;
	public CameraFollow camFollow;
	public EffectManager effectManager;
	public GameObject target = null;
	public List<GameObject> attackers = new List<GameObject>();
	public GameObject holding;
	public InventoryComponent invComponent;
	public PlayerAbilities abilities;
	public PlayerEnergy energy;
	public Light pointLight;
	public PlayerSound sound;
	public PlayerParticles particles;
	public GameObject projectilePrefab;

	[Header("Properties:")]
	public float moveSpeed;
	public float rotateSpeed;
	public float sprintModifier;
	public float HoverHeight = 1;

	[Header("Player States:")]
	public bool isSprinting;
	public bool isPossessing;
	public bool dead;
	public bool isInvisible;
	
	//Private Variables...
	RaycastHit ground;
	Rigidbody rigid;
	Vector3 hoverDestinationLerp;
	float totalLerpTime;
	float currentLerpTime;

	void Start () {
		camFollow.target = gameObject;
		PlayerInit();
	}
	
	private void PlayerInit() {
		if(!rigid)
			rigid = GetComponent<Rigidbody>();
		if(!invComponent)
			invComponent = GetComponent<InventoryComponent> ();
		if(!abilities)
			abilities = GetComponent<PlayerAbilities> ();
		if(!energy)
			energy = GetComponent<PlayerEnergy> ();
		if(!pointLight)
			pointLight = GetComponent<Light> ();
		if(!sound)
			sound = GetComponent<PlayerSound> ();
		if(!particles)
			particles = GetComponent<PlayerParticles> ();
	
	}

	void FixedUpdate ()
	{
		float adjustedRotateSpeed = rotateSpeed;
		float adjustedMoveSpeed = moveSpeed;

		if (GameManager.gameState == GameState.LOST || GameManager.gameState == GameState.WON || GameManager.gameState == GameState.PAUSED)
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

		if (GameManager.gameState == GameState.LOST || GameManager.gameState == GameState.WON)
			return;

		//Pausing
		if (Input.GetButtonDown ("Cancel"))
			GameManager.TogglePause ();

		if (GameManager.gameState == GameState.PAUSED)
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
		Hover();

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
		GameManager.lost = true;
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

	public void Hover ()
	{
		//Based on https://chicounity3d.wordpress.com/2014/05/23/how-to-lerp-like-a-pro/

		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > totalLerpTime) {
			currentLerpTime = totalLerpTime;
		}
		if (Physics.Raycast (transform.position, Vector3.down, out ground, 100, 1 << 8)) {
			hoverDestinationLerp = new Vector3 (transform.position.x, ground.point.y + HoverHeight, transform.position.z);
			totalLerpTime = Vector3.Distance (transform.position, hoverDestinationLerp);
			//Debug.Log (totalLerpTime);
		} else {
			currentLerpTime = 0f;
		}
		if (currentLerpTime != 0) {
			float perc = currentLerpTime / totalLerpTime;
			transform.position = Vector3.Lerp(transform.position, hoverDestinationLerp, perc);
		}
	}
}
