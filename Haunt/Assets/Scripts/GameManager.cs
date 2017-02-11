﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public bool won;
	public bool lost;

	public GameObject endText;
	public GameObject endMenu;
	public GameObject progressButton;

	public GameObject pauseMenu;

	public Difficulty difficultyLevel;

	public Slider diffSlider;

	public GameObject endButton;

	public int level;

	public bool existedBefore = false;

	public GameState gameState;
	public GameState unpausedState;

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
			gameState = GameState.MENU;

			diffSlider = GameObject.Find ("difficultySlider").GetComponent<Slider> ();
			if (existedBefore)
				Destroy (gameObject);
		} else if (SceneManager.GetActiveScene ().name.StartsWith ("level")) {
			gameState = GameState.PLAYING;

			endText = GameObject.Find ("Canvas/End Menu/endText");
			endMenu = GameObject.Find ("Canvas/End Menu");
			progressButton = GameObject.Find ("Canvas/End Menu/Progress");
			endButton = GameObject.Find ("Canvas/End Menu/End");
			progressButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
			endButton.GetComponent<Button> ().onClick.AddListener (ExitToMainMenu);

			pauseMenu = GameObject.Find ("Canvas/Pause Menu");

			won = false;
			lost = false;
		} else if (SceneManager.GetActiveScene ().name == "lobby") {
			gameState = GameState.LOBBY;

			pauseMenu = GameObject.Find ("Canvas/Pause Menu");
		}
		existedBefore = true;
	}
	
	void Update () {
		if (SceneManager.GetActiveScene ().name == "menu") {
			difficultyLevel = (Difficulty)diffSlider.value;
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
		endText.SetActive (hasWon || hasLost);
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
	/// <returns><c>true</c>, if game was paused, <c>false</c> if it was unpaused.</returns>
	public bool TogglePause ()
	{
		if (gameState == GameState.PAUSED) {
			unpausedState = gameState;
			gameState = GameState.PAUSED;
			pauseMenu.SetActive (true);
			return true;
		} else {
			gameState = unpausedState;
			pauseMenu.SetActive (false);
			return false;
		}
	}
}
