﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

	public bool attacked = false;
	public bool dead = false;

	public UnityStandardAssets.CinematicEffects.LensAberrations lensAbb;
	Color vignetteColour = new Color(0.372f, 0.039f, 0.086f); 
	Color chromAbbColour = new Color(0f, 1f, 0f);

	void Start () {
	}
	
	void Update () {
		AttackedEffect (attacked);
		DeadEffect (dead);
	}

	/// <summary>
	/// Enables or Disables the attacked effect.
	/// </summary>
	/// <param name="enabled">If set to <c>true</c>, the effect will be enabled. If false, it will be disabled.</param>
	void AttackedEffect (bool enabled) {
		lensAbb.vignette.enabled = enabled;
		lensAbb.chromaticAberration.enabled = enabled;

		if (enabled) {
			lensAbb.vignette.color = vignetteColour;
			lensAbb.vignette.intensity = 1.5f;
			lensAbb.vignette.blur = 0.1f;
			lensAbb.vignette.smoothness = 0.8f;

			lensAbb.chromaticAberration.color = chromAbbColour;
			lensAbb.chromaticAberration.amount = Random.value * 50;	
		}
	}

	/// <summary>
	/// Enables or Disables the dead effect.
	/// </summary>
	/// <param name="enabled">If set to <c>true</c>, the effect will be enabled. If false, it will be disabled.</param>
	void DeadEffect (bool enabled) {
		lensAbb.vignette.enabled = enabled;

		if (enabled) {
			lensAbb.vignette.color = vignetteColour;
			lensAbb.vignette.intensity = 2.0f;
			lensAbb.vignette.smoothness = 5.0f;
			lensAbb.vignette.blur = 0.5f;
		}
	}
}