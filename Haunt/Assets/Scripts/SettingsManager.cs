using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

	public Slider qualitySlider;

	public Dropdown resolutionDropdown;

	public GameObject settingsCanvas;

	// Use this for initialization
	void Start () {
		settingsCanvas = GameObject.Find ("SettingsCanvas");
		qualitySlider = GameObject.Find ("SettingsCanvas/qualitySlider").GetComponent<Slider> ();
		resolutionDropdown = GameObject.Find ("SettingsCanvas/resolutionDropdown").GetComponent<Dropdown> ();
		settingsCanvas.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (!settingsCanvas.activeInHierarchy)
			return;

		QualitySettings.SetQualityLevel ((int)qualitySlider.value);

		Debug.Log (QualitySettings.GetQualityLevel ());
	}
}
