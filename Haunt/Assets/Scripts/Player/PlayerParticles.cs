using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour {

	public ParticleSystem blast;
	public ParticleSystem heal;

	void Start ()
	{
		blast = GameObject.Find ("Player/blastParticles").GetComponent<ParticleSystem> ();
		heal = GameObject.Find ("Player/healParticles").GetComponent<ParticleSystem> ();

		blast.Stop ();
		heal.Stop ();
	}

	/// <summary>
	/// Enable or disable healing particles.
	/// </summary>
	/// <param name="enabled">If set to <c>true</c> enable.</param>
	public void Blast (bool enabled)
	{
		if (enabled) {
			blast.Play ();
		} else {
			blast.Stop ();
		}
	}

	/// <summary>
	/// Enable or disable healing particles.
	/// </summary>
	/// <param name="enabled">If set to <c>true</c> enable.</param>
	public void Heal (bool enabled)
	{
		if (enabled) {
			heal.Play ();
		} else {
			heal.Stop ();
		}
	}
}
