using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : MonoBehaviour {

	public PlayerController pc;

	/// <summary>
	/// Makes the player invisible.
	/// </summary>
	public void BecomeInvisible ()
	{
		pc.isInvisible = true;
		pc.meshRend.enabled = false;
		pc.trailRend.enabled = false;
		pc.pointLight.enabled = false;
	}

	/// <summary>
	/// Makes the player visible.
	/// </summary>
	public void BecomeVisible ()
	{
		pc.isInvisible = false;
		pc.meshRend.enabled = true;
		pc.trailRend.enabled = true;
		pc.pointLight.enabled = true;
	}
}
