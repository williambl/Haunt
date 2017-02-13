using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

	public Slider qualitySlider;
	public Text qualityLabel;

	public Dropdown resolutionDropdown;

	public Toggle fullscreenToggle;

	public GameObject settingsCanvas;

	public List<Resolution> supportedResolutions = new List<Resolution> ();
	public List<string> supportedResolutionsAsStrings = new List<string> ();
	public int currentResolution;

	void Start () {
		settingsCanvas = GameObject.Find ("SettingsCanvas");
		qualitySlider = GameObject.Find ("SettingsCanvas/qualitySlider").GetComponent<Slider> ();
		resolutionDropdown = GameObject.Find ("SettingsCanvas/resolutionDropdown").GetComponent<Dropdown> ();
		fullscreenToggle = GameObject.Find ("SettingsCanvas/fullscreenToggle").GetComponent<Toggle> ();
		qualityLabel = GameObject.Find ("SettingsCanvas/qualitySlider/Handle Slide Area/Handle/qualityLabel").GetComponent<Text> ();

		foreach (Resolution resolution in Screen.resolutions) {
			supportedResolutions.Add (resolution);
			supportedResolutionsAsStrings.Add (resolution.ToString ());
			if (Screen.currentResolution.Equals(resolution))
				currentResolution = supportedResolutions.Count - 1;
		}
		resolutionDropdown.AddOptions (supportedResolutionsAsStrings);
		resolutionDropdown.value = currentResolution;
		resolutionDropdown.onValueChanged.AddListener (UpdateResolution);
		fullscreenToggle.isOn = Screen.fullScreen;
		fullscreenToggle.onValueChanged.AddListener (UpdateFullscreen);
		settingsCanvas.SetActive (false);
	}
	
	void Update () {
		if (!settingsCanvas.activeInHierarchy)
			return;

		QualitySettings.SetQualityLevel ((int)qualitySlider.value);
		switch (QualitySettings.GetQualityLevel ()) {
		case 0:
			qualityLabel.text = "FASTEST";
			break;
		case 1:
			qualityLabel.text = "FAST";
			break;
		case 2:
			qualityLabel.text = "SIMPLE";
			break;
		case 3:
			qualityLabel.text = "GOOD";
			break;
		case 4:
			qualityLabel.text = "BEAUTIFUL";
			break;
		case 5:
			qualityLabel.text = "FANTASTIC";
			break;
		case 6:
			qualityLabel.text = "AMAZING";
			break;
		}
	}

	public void UpdateResolution (int index)
	{
		Screen.SetResolution (supportedResolutions[index].width, supportedResolutions[index].height, Screen.fullScreen);
	}

	public void UpdateFullscreen (bool value)
	{
		Screen.fullScreen = value;
	}
}
