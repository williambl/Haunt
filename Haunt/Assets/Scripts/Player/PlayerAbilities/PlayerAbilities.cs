using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

	public Ability abilities;

	GameManager manager;

	void Start ()
	{
		manager = GameObject.Find ("Manager").GetComponent<GameManager> ();

		if (manager.maxLevelReached > 0)
			abilities |= Ability.DESOLIDIFY;
		if (manager.maxLevelReached > 1)
			abilities |= Ability.POSSESS;
		if (manager.maxLevelReached > 2)
			abilities |= Ability.INVISIBILITY;
		if (manager.maxLevelReached > 3)
			abilities |= Ability.BLAST;
		if (manager.maxLevelReached > 4)
			abilities |= Ability.FOCUSED_BLAST;

		if ((abilities & Ability.DESOLIDIFY) != 0)
			gameObject.layer = 9;
		else
			gameObject.layer = 12;
	}

	void Update () 
	{
		
	}
}
