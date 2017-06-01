using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {
	
	public bool attacked = false;
	public bool dead = false;
	public bool lowEnergy = false;

	public UnityEngine.PostProcessing.PostProcessingBehaviour ppb;
	public UnityEngine.PostProcessing.PostProcessingProfile profile;

	Color vignetteColour = new Color(0.372f, 0.039f, 0.086f); 
	Color chromAbbColour = new Color(0f, 1f, 0f);

	void OnEnable () {
		ppb = GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour> ();
		profile = Instantiate (ppb.profile);
		ppb.profile = profile;

		ppb.profile.ambientOcclusion.enabled = QualitySettings.GetQualityLevel () > 3;
		ppb.profile.bloom.enabled = QualitySettings.GetQualityLevel () > 2;
	}
	
	void Update () {
		AttackedEffect (attacked);
		DeadEffect (dead);
		LowEnergyEffect (lowEnergy);
	}

	/// <summary>
	/// Enables or Disables the attacked effect.
	/// </summary>
	/// <param name="enabled">If set to <c>true</c>, the effect will be enabled. If false, it will be disabled.</param>
	void AttackedEffect (bool enabled) {
		ppb.profile.vignette.enabled = enabled;
		ppb.profile.chromaticAberration.enabled = enabled;

		if (enabled) {
			var vignette = profile.vignette.settings;
			vignette.color = vignetteColour;
			vignette.intensity = 1.5f;
			vignette.smoothness = 0.8f;
			profile.vignette.settings = vignette;

			var chromaticAberration = profile.chromaticAberration.settings;
			//chromaticAberration.spectralTexture = chromAbbColour;
			chromaticAberration.intensity = Random.value * 50;
			profile.chromaticAberration.settings = chromaticAberration;
		}
	}

	/// <summary>
	/// Enables or Disables the dead effect.
	/// </summary>
	/// <param name="enabled">If set to <c>true</c>, the effect will be enabled. If false, it will be disabled.</param>
	void DeadEffect (bool enabled) {
		ppb.profile.vignette.enabled = enabled;
		if (enabled) {
			var vignette = profile.vignette.settings;
			vignette.color = vignetteColour;
			vignette.intensity = 2.0f;
			vignette.smoothness = 5.0f;
			profile.vignette.settings = vignette;
		}
	}

	void LowEnergyEffect (bool enabled) {
		if (enabled) {
			ppb.profile.vignette.enabled = enabled;

			var vignette = profile.vignette.settings;
			vignette.color = vignetteColour;
			vignette.intensity = Mathf.Sin (Time.time * 10) * 0.5f > 0 ? Mathf.Sin (Time.time * 10) * 0.5f : 0;
			vignette.smoothness = 5.0f;
			profile.vignette.settings = vignette;
		}
	}
}
