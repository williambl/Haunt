using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class GUIManager {

	public static Text endText;
	public static GameObject endMenu;
	public static Button progressButton;
	public static Button endButton;


	public static Slider diffSlider;
	public static Button startButton;
	public static Button settingsButton;
	public static Button exitButton;
	public static Text difficultyLabel;

	public static GameObject pauseMenu;
	public static Button unpauseButton;
	public static Button saveFromPauseButton;
	public static Button loadFromPauseButton;
	public static Button exitToLobbyFromPauseButton;
	public static Button exitToMenuFromPauseButton;

	public static GameObject menuCanvas;
	public static GameObject settingsCanvas;
	public static Button backSettingsButton;

	public static GameObject startGameCanvas;
	public static Button backStartGameButton;
	public static Button startNewButton;
	public static Button loadSavedButton;

	public static GameObject saveLoadMenu;
	public static Text saveLoadLabel;
	public static Button backFromSaveLoadButton;
	public static InputField saveNameField;
	public static Button saveLoadButton;

        
        public static void Win () {
    	    endMenu.SetActive (true);
            endText.gameObject.SetActive (true);
            endText.GetComponent<Text> ().text = "You win!";
            progressButton.GetComponentInChildren<Text> ().text = "Next Level";
	    progressButton.GetComponent<Button> ().onClick.RemoveListener (GameManager.NextLevel);
            progressButton.GetComponent<Button> ().onClick.AddListener (GameManager.NextLevel);
        }

        public static void Lose () {
    	    endMenu.SetActive (true);
            endText.gameObject.SetActive (true);
            endText.GetComponent<Text> ().text = "You Lose!";
            progressButton.GetComponentInChildren<Text> ().text = "Restart Level";
	    progressButton.GetComponent<Button> ().onClick.RemoveListener (GameManager.RestartLevel);
	    progressButton.GetComponent<Button> ().onClick.AddListener (GameManager.RestartLevel);
        }

	public static void InitEndMenu ()
	{

		endText = GameObject.Find ("EndCanvas/End Menu/endText").GetComponent<Text> ();
		endMenu = GameObject.Find ("EndCanvas/End Menu");
		progressButton = GameObject.Find ("EndCanvas/End Menu/Progress").GetComponent<Button> ();
		endButton = GameObject.Find ("EndCanvas/End Menu/End").GetComponent<Button> ();
		progressButton.onClick.RemoveAllListeners ();
		endButton.onClick.AddListener (GameManager.ExitToMainMenu);
	}


	public static void InitPauseMenu ()
	{
		pauseMenu = GameObject.Find ("PauseCanvas/Pause Menu");
		unpauseButton = GameObject.Find ("PauseCanvas/Pause Menu/Unpause").GetComponent<Button> ();
		saveFromPauseButton = GameObject.Find ("PauseCanvas/Pause Menu/Save").GetComponent<Button> ();
		loadFromPauseButton = GameObject.Find ("PauseCanvas/Pause Menu/Load").GetComponent<Button> ();
		exitToLobbyFromPauseButton = GameObject.Find ("PauseCanvas/Pause Menu/ExitToLobby").GetComponent<Button> ();
		exitToMenuFromPauseButton = GameObject.Find ("PauseCanvas/Pause Menu/ExitToMainMenu").GetComponent<Button> ();

		unpauseButton.onClick.AddListener (GameManager.TogglePause);
		saveFromPauseButton.onClick.AddListener (ToggleSaveCanvas);
		loadFromPauseButton.onClick.AddListener (ToggleLoadCanvas);
		exitToLobbyFromPauseButton.onClick.AddListener (GameManager.GotoLobby);
		exitToMenuFromPauseButton.onClick.AddListener (GameManager.ExitToMainMenu);

		pauseMenu.SetActive (false);
	}

	public static void InitMainMenu ()
	{
		menuCanvas = GameObject.Find ("MainMenuCanvas");
		settingsButton = GameObject.Find ("MainMenuCanvas/settings").GetComponent<Button> ();
		exitButton = GameObject.Find ("MainMenuCanvas/exit").GetComponent<Button> ();
		startButton = GameObject.Find ("MainMenuCanvas/start").GetComponent<Button> ();

		settingsCanvas = GameObject.Find ("SettingsCanvas");
		backSettingsButton = GameObject.Find ("SettingsCanvas/back").GetComponent<Button> ();

		startGameCanvas = GameObject.Find ("StartGameCanvas");
		difficultyLabel = GameObject.Find ("StartGameCanvas/difficultySlider/Handle Slide Area/Handle/difficultyLabel").GetComponent<Text> ();
		diffSlider = GameObject.Find ("StartGameCanvas/difficultySlider").GetComponent<Slider> ();
		startNewButton = GameObject.Find ("StartGameCanvas/startnew").GetComponent<Button> ();
		loadSavedButton = GameObject.Find ("StartGameCanvas/loadsaved").GetComponent<Button> ();
		backStartGameButton = GameObject.Find ("StartGameCanvas/back").GetComponent<Button> ();

		startButton.onClick.AddListener (ToggleStartCanvas);
		exitButton.onClick.AddListener (GameManager.ExitGame);

		settingsButton.onClick.AddListener (ToggleSettings);
		backSettingsButton.onClick.AddListener (ToggleSettings);

		startNewButton.onClick.AddListener (GameManager.StartGame);
		loadSavedButton.onClick.AddListener (ToggleLoadCanvas);
		backStartGameButton.onClick.AddListener (ToggleStartCanvas);

		startGameCanvas.SetActive (false);
	}

	public static void InitSaveLoadMenu()
	{
		saveLoadMenu = GameObject.Find ("SaveLoadCanvas/SaveLoad Menu");
		saveLoadLabel = GameObject.Find ("SaveLoadCanvas/SaveLoad Menu/SAVELOAD").GetComponent<Text> ();
		backFromSaveLoadButton = GameObject.Find ("SaveLoadCanvas/SaveLoad Menu/Back").GetComponent<Button> ();
		saveNameField = GameObject.Find ("SaveLoadCanvas/SaveLoad Menu/SaveName").GetComponent<InputField> ();
		saveLoadButton = GameObject.Find ("SaveLoadCanvas/SaveLoad Menu/SaveLoad").GetComponent<Button> ();

		backFromSaveLoadButton.onClick.AddListener (ToggleSaveLoadCanvas);
		saveLoadButton.onClick.AddListener (ToggleSaveLoadCanvas);

		saveLoadMenu.SetActive (false);
	}

	/// <summary>
	/// Toggles the settings menu.
	/// </summary>
	public static void ToggleSettings ()
	{
		settingsCanvas.SetActive (!settingsCanvas.activeInHierarchy);
		menuCanvas.SetActive (!menuCanvas.activeInHierarchy);
	}

	/// <summary>
	/// Toggles the start canvas visibility.
	/// </summary>
	public static void ToggleStartCanvas ()
	{
		startGameCanvas.SetActive (!startGameCanvas.activeInHierarchy);
		menuCanvas.SetActive (!menuCanvas.activeInHierarchy);
	}
		
	/// <summary>
	/// Toggles the save canvas visibility.
	/// </summary>
	public static void ToggleSaveCanvas ()
	{
		saveLoadMenu.SetActive (!saveLoadMenu.activeInHierarchy);
		if (saveLoadMenu.activeInHierarchy) {
			saveLoadButton.onClick.RemoveListener (GameManager.LoadGameFromMenu);
			saveLoadButton.onClick.RemoveListener (GameManager.SaveGameFromMenu);

			saveLoadLabel.text = "SAVE";
			saveLoadButton.GetComponentInChildren<Text> ().text = "Save";
			saveLoadButton.onClick.AddListener (GameManager.SaveGameFromMenu);
		}
	}

	/// <summary>
	/// Toggles the load canvas visibility.
	/// </summary>
	public static void ToggleLoadCanvas ()
	{
		saveLoadMenu.SetActive (!saveLoadMenu.activeInHierarchy);
		if (saveLoadMenu.activeInHierarchy) {
			saveLoadButton.onClick.RemoveListener (GameManager.LoadGameFromMenu);
			saveLoadButton.onClick.RemoveListener (GameManager.SaveGameFromMenu);

			saveLoadLabel.text = "LOAD";
			saveLoadButton.GetComponentInChildren<Text> ().text = "Load";
			saveLoadButton.onClick.AddListener (GameManager.LoadGameFromMenu);
		}
	}

	/// <summary>
	/// Toggles the save or load canvas visibility.
	/// </summary>
	public static void ToggleSaveLoadCanvas ()
	{
		saveLoadMenu.SetActive (!saveLoadMenu.activeInHierarchy);
	}

	/// <summary>
	/// Toggles the pause canvas visibility.
	/// </summary>
        public static void TogglePauseCanvas ()
        {
	        pauseMenu.SetActive (!pauseMenu.activeInHierarchy);
        }

        /// <summary>
        /// Gets the value of the difficulty slider on the main menu,
        /// and sets the label of the slider.
        /// </summary>
        public static Difficulty GetDifficultyFromSlider ()
        {
	    switch ((Difficulty)diffSlider.value) {
            case Difficulty.EASY:
                difficultyLabel.text = "EASY";
	    	break;
	    case Difficulty.NORMAL:
		difficultyLabel.text = "NORMAL";
		break;
	    case Difficulty.HARD:
	        difficultyLabel.text = "HARD";
	        break;
            }
	    return (Difficulty)diffSlider.value;
        }
}
