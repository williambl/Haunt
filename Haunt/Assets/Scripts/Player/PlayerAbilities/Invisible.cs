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
		pc.pointLight.enabled = false;
		pc.particles.Player (false);
	}

	/// <summary>
	/// Makes the player visible.
	/// </summary>
	public void BecomeVisible ()
	{
		pc.isInvisible = false;
		pc.pointLight.enabled = true;
		pc.particles.Player (true);
	}
}
