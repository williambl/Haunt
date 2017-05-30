using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {
	
	public bool attacked = false;
	public bool dead = false;
	public bool lowEnergy = false;

	public UnityEngine.PostProcessing.PostProcessingProfile ppp;
	Color vignetteColour = new Color(0.372f, 0.039f, 0.086f); 
	Color chromAbbColour = new Color(0f, 1f, 0f);

	void Start () {
		ppp.ambientOcclusion.enabled = QualitySettings.GetQualityLevel () > 3;
		ppp.bloom.enabled = QualitySettings.GetQualityLevel () > 2;
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
		ppp.vignette.enabled = enabled;
		ppp.chromaticAberration.enabled = enabled;

		if (enabled) {
			ppp.vignette.settings.color = vignetteColour;
			ppp.vignette.settings.intensity = 1.5f;
			ppp.vignette.settings.smoothness = 0.8f;

			ppp.chromaticAberration.settings.spectralTexture = chromAbbColour;
			ppp.chromaticAberration.settings.intensity = Random.value * 50;	
		}
	}

	/// <summary>
	/// Enables or Disables the dead effect.
	/// </summary>
	/// <param name="enabled">If set to <c>true</c>, the effect will be enabled. If false, it will be disabled.</param>
	void DeadEffect (bool enabled) {
		ppp.vignette.enabled = enabled;
		if (enabled) {
			ppp.vignette.settings.color = vignetteColour;
			ppp.vignette.settings.intensity = 2.0f;
			ppp.vignette.settings.smoothness = 5.0f;
		}
	}

	void LowEnergyEffect (bool enabled) {
		if (enabled) {
			ppp.vignette.enabled = enabled;
			ppp.vignette.settings.color = vignetteColour;
			ppp.vignette.settings.intensity = Mathf.Sin (Time.time * 10) * 0.5f > 0 ? Mathf.Sin (Time.time * 10) * 0.5f : 0;
			ppp.vignette.settings.smoothness = 5.0f;
		}
	}
}
