using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour {

	[Header("Particle System Types:")]
	public ParticleSystem blast;
	public ParticleSystem heal;
	public ParticleSystem player;

	void Start ()
	{
		if(!blast)
			blast = GameObject.Find ("Player/blastParticles").GetComponent<ParticleSystem> ();
		if(!heal)
			heal = GameObject.Find ("Player/healParticles").GetComponent<ParticleSystem> ();
		if (!player)
			player = GameObject.Find ("Player/playerParticles").GetComponent<ParticleSystem> ();

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

	/// <summary>
	/// Enable or disable player particles
	/// </summary>
	/// <param name="enabled">If set to <c>true</c> enable.</param>
	public void Player (bool enabled)
	{
		if (enabled)
			player.Play ()
		else
			player.Stop ()
	}
}
