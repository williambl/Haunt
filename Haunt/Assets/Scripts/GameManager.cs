﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public bool won;
	public bool lost;

	public Text endText;
	public GameObject endMenu;
	public Button progressButton;
	public Button endButton;

	public Difficulty difficultyLevel;

	public Slider diffSlider;
	public Button startButton;
	public Button settingsButton;
	public Button exitButton;
	public Text difficultyLabel;

	public int level;

	public bool existedBefore = false;

	public GameState gameState;
	public GameState unpausedState;

	public GameObject pauseMenu;
	public Button unpauseButton;
	public Button saveFromPauseButton;
	public Button loadFromPauseButton;
	public Button exitToLobbyFromPauseButton;
	public Button exitToMenuFromPauseButton;

	public GameObject menuCanvas;
	public GameObject settingsCanvas;
	public Button backSettingsButton;

	public GameObject startGameCanvas;
	public Button backStartGameButton;
	public Button startNewButton;
	public Button loadSavedButton;

	public GameObject saveLoadMenu;
	public Text saveLoadLabel;
	public Button backFromSaveLoadButton;
	public InputField saveNameField;
	public Button saveLoadButton;

	public int maxLevelReached = -1;

	void OnEnable () {
		//Subscribes to the scene loading event
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}
		
	void Start () {
		DontDestroyOnLoad (gameObject);
	}

	/// <summary>
	/// Initialise.
	/// </summary>
	void Init () {
		if (SceneManager.GetActiveScene ().name == "menu") {
			if (existedBefore) {
				Destroy (gameObject);
				return;
			}
			
			gameState = GameState.MENU;

			InitMainMenu ();
		} else if (SceneManager.GetActiveScene ().name.StartsWith ("level")) {
			gameState = GameState.PLAYING;
			won = false;
			lost = false;

			InitPauseMenu ();
			InitEndMenu ();

			if (level > maxLevelReached)
				maxLevelReached = level;
		} else if (SceneManager.GetActiveScene ().name == "lobby") {
			gameState = GameState.LOBBY;
			won = false;
			lost = false;

			InitPauseMenu ();
		}
		InitSaveLoadMenu ();
		existedBefore = true;
	}
	
	void Update () {
		if (SceneManager.GetActiveScene ().name == "menu") {
			difficultyLevel = (Difficulty)diffSlider.value;

			switch (difficultyLevel) {
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
		} 
		else if (SceneManager.GetActiveScene ().name.StartsWith ("level")) {
			WinOrLose (won, lost);
		}
	}

	/// <summary>
	/// If the player has won, this will bring up the win screen.
	/// If the player has lost, it will bring up the lose screen.
	/// </summary>
	/// <param name="hasWon">If set to <c>true</c>, the player has won.</param>
	/// <param name="hasLost">If set to <c>true</c>, the player has lost.</param>
	void WinOrLose (bool hasWon, bool hasLost) {
		endMenu.SetActive (hasWon || hasLost);
		endText.gameObject.SetActive (hasWon || hasLost);
		if (hasWon) {
			gameState = GameState.WON;
			endText.GetComponent<Text> ().text = "You win!";
			progressButton.GetComponentInChildren<Text> ().text = "Next Level";
			progressButton.GetComponent<Button> ().onClick.RemoveListener (NextLevel);
			progressButton.GetComponent<Button> ().onClick.AddListener (NextLevel);
		} else if (hasLost){
			gameState = GameState.LOST;
			endText.GetComponent<Text> ().text = "You lose!";
			progressButton.GetComponentInChildren<Text> ().text = "Restart Level";
			progressButton.GetComponent<Button> ().onClick.RemoveListener (RestartLevel);
			progressButton.GetComponent<Button> ().onClick.AddListener (RestartLevel);
		}
	}

	void OnDisable()
	{
		//Unsubscibe from the scene loading event
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	/// <summary>
	/// Starts the game.
	/// </summary>
	public void StartGame () {
		level = -1;
		GotoLobby ();
	}
		
	/// <summary>
	/// Changes the difficulty.
	/// </summary>
	/// <param name="value">Difficulty value.</param>
	public void ChangeDifficulty (int value) {
		difficultyLevel = (Difficulty)value;
	}

	/// <summary>
	/// Switches to the next level.
	/// </summary>
	public void NextLevel () {
		gameState = GameState.LOADING;
		level++;
		SceneManager.LoadScene ("level" + level);
	}

	/// <summary>
	/// Restarts the level.
	/// </summary>
	public void RestartLevel () {
		gameState = GameState.LOADING;
		SceneManager.LoadScene ("level" + level);
	}

	/// <summary>
	/// Goes to the specified level.
	/// </summary>
	/// <param name="level">Level to go to.</param>
	public void GotoLevel (int targetLevel) {
		gameState = GameState.LOADING;
		level = targetLevel;
		SceneManager.LoadScene ("level" + targetLevel);
	}

	/// <summary>
	/// Exits to the main menu.
	/// </summary>
	public void ExitToMainMenu () {
		gameState = GameState.LOADING;
		SceneManager.LoadScene ("menu");
	}

	/// <summary>
	/// Goes to the lobby.
	/// </summary>
	public void GotoLobby () {
		gameState = GameState.LOADING;
		SceneManager.LoadScene ("lobby");
	}

	/// <summary>
	/// When the level loads, initialises.
	/// </summary>
	/// <param name="scene">Scene loaded.</param>
	/// <param name="mode">LoadSceneMode.</param>
	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		Init ();
	}

	/// <summary>
	/// Toggles the pause.
	/// </summary>
	public void TogglePause ()
	{
		if (gameState == GameState.PAUSED) {
			gameState = unpausedState;
			pauseMenu.SetActive (false);
		} else {
			unpausedState = gameState;
			gameState = GameState.PAUSED;
			pauseMenu.SetActive (true);
		}
	}

	void InitEndMenu ()
	{

		endText = GameObject.Find ("EndCanvas/End Menu/endText").GetComponent<Text> ();
		endMenu = GameObject.Find ("EndCanvas/End Menu");
		progressButton = GameObject.Find ("EndCanvas/End Menu/Progress").GetComponent<Button> ();
		endButton = GameObject.Find ("EndCanvas/End Menu/End").GetComponent<Button> ();
		progressButton.onClick.RemoveAllListeners ();
		endButton.onClick.AddListener (ExitToMainMenu);
	}

	void InitPauseMenu ()
	{
		pauseMenu = GameObject.Find ("PauseCanvas/Pause Menu");
		unpauseButton = GameObject.Find ("PauseCanvas/Pause Menu/Unpause").GetComponent<Button> ();
		saveFromPauseButton = GameObject.Find ("PauseCanvas/Pause Menu/Save").GetComponent<Button> ();
		loadFromPauseButton = GameObject.Find ("PauseCanvas/Pause Menu/Load").GetComponent<Button> ();
		exitToLobbyFromPauseButton = GameObject.Find ("PauseCanvas/Pause Menu/ExitToLobby").GetComponent<Button> ();
		exitToMenuFromPauseButton = GameObject.Find ("PauseCanvas/Pause Menu/ExitToMainMenu").GetComponent<Button> ();

		unpauseButton.onClick.AddListener (TogglePause);
		saveFromPauseButton.onClick.AddListener (ToggleSaveCanvas);
		loadFromPauseButton.onClick.AddListener (ToggleLoadCanvas);
		exitToLobbyFromPauseButton.onClick.AddListener (GotoLobby);
		exitToMenuFromPauseButton.onClick.AddListener (ExitToMainMenu);

		pauseMenu.SetActive (false);
	}

	void InitMainMenu ()
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
		exitButton.onClick.AddListener (ExitGame);

		settingsButton.onClick.AddListener (ToggleSettings);
		backSettingsButton.onClick.AddListener (ToggleSettings);

		startNewButton.onClick.AddListener (StartGame);
		loadSavedButton.onClick.AddListener (ToggleLoadCanvas);
		backStartGameButton.onClick.AddListener (ToggleStartCanvas);

		startGameCanvas.SetActive (false);
	}

	void InitSaveLoadMenu()
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
	/// Exits the game.
	/// </summary>
	public void ExitGame ()
	{
		Application.Quit ();
	}

	/// <summary>
	/// Toggles the settings menu.
	/// </summary>
	public void ToggleSettings ()
	{
		settingsCanvas.SetActive (!settingsCanvas.activeInHierarchy);
		menuCanvas.SetActive (!menuCanvas.activeInHierarchy);
	}

	/// <summary>
	/// Toggles the start canvas visibility.
	/// </summary>
	public void ToggleStartCanvas ()
	{
		startGameCanvas.SetActive (!startGameCanvas.activeInHierarchy);
		menuCanvas.SetActive (!menuCanvas.activeInHierarchy);
	}
		
	/// <summary>
	/// Toggles the save canvas visibility.
	/// </summary>
	public void ToggleSaveCanvas ()
	{
		saveLoadMenu.SetActive (!saveLoadMenu.activeInHierarchy);
		if (saveLoadMenu.activeInHierarchy) {
			saveLoadButton.onClick.RemoveListener (LoadGameFromMenu);
			saveLoadButton.onClick.RemoveListener (SaveGameFromMenu);

			saveLoadLabel.text = "SAVE";
			saveLoadButton.GetComponentInChildren<Text> ().text = "Save";
			saveLoadButton.onClick.AddListener (SaveGameFromMenu);
		}
	}

	/// <summary>
	/// Toggles the load canvas visibility.
	/// </summary>
	public void ToggleLoadCanvas ()
	{
		saveLoadMenu.SetActive (!saveLoadMenu.activeInHierarchy);
		if (saveLoadMenu.activeInHierarchy) {
			saveLoadButton.onClick.RemoveListener (LoadGameFromMenu);
			saveLoadButton.onClick.RemoveListener (SaveGameFromMenu);

			saveLoadLabel.text = "LOAD";
			saveLoadButton.GetComponentInChildren<Text> ().text = "Load";
			saveLoadButton.onClick.AddListener (LoadGameFromMenu);
		}
	}

	/// <summary>
	/// Toggles the save or load canvas visibility.
	/// </summary>
	public void ToggleSaveLoadCanvas ()
	{
		saveLoadMenu.SetActive (!saveLoadMenu.activeInHierarchy);
	}

	public void SaveGame (string saveName)
	{
		Game game = new Game (level, gameState, level);
		SaverLoader.Save (saveName, game);
	}

	public void LoadGame (string saveName)
	{
		Game game = SaverLoader.Load (saveName);
		GotoLevel (game.level);
		gameState = game.gameState;
	}

	public void SaveGameFromMenu ()
	{
		string saveName = saveNameField.text + ".hsv";
		SaveGame (saveName);
	}

	public void LoadGameFromMenu ()
	{
		string saveName = saveNameField.text + ".hsv";
		LoadGame (saveName);
	}
}
