using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

	public Ability abilities;

	PlayerController controller;

	public Hold hold;
	public Possess possess;
	public Invisible invisible;
	public Blast blast;
	public FocusedBlast focusedBlast;

	void Start ()
	{
		controller = GetComponent<PlayerController> ();

		if (GameManager.maxLevelReached > 0)
			abilities |= Ability.DESOLIDIFY;
		if (GameManager.maxLevelReached > 1)
			abilities |= Ability.POSSESS;
		if (GameManager.maxLevelReached > 2)
			abilities |= Ability.INVISIBILITY;
		if (GameManager.maxLevelReached > 3)
			abilities |= Ability.BLAST;
		if (GameManager.maxLevelReached > 4)
			abilities |= Ability.FOCUSED_BLAST;

		if ((abilities & Ability.DESOLIDIFY) != 0)
			gameObject.layer = 9;
		else
			gameObject.layer = 12;

		hold = GetComponent<Hold> ();
		possess = GetComponent<Possess> ();
		invisible = GetComponent<Invisible> ();
		blast = GetComponent<Blast> ();
		focusedBlast = GetComponent<FocusedBlast> ();

		hold.pc = controller;
		possess.pc = controller;
		invisible.pc = controller;
		blast.pc = controller;
		focusedBlast.pc = controller;
	}
}
