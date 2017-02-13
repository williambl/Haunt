using UnityEngine;
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
	public Button exitToLobbyFromPauseButton;
	public Button exitToMenuFromPauseButton;

	public GameObject menuCanvas;
	public GameObject settingsCanvas;
	public Button backSettingsButton;

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

			InitMainMenu ();

			if (existedBefore)
				Destroy (gameObject);
		} else if (SceneManager.GetActiveScene ().name.StartsWith ("level")) {
			gameState = GameState.PLAYING;
			won = false;
			lost = false;

			InitPauseMenu ();
			InitEndMenu ();
		} else if (SceneManager.GetActiveScene ().name == "lobby") {
			gameState = GameState.LOBBY;
			won = false;
			lost = false;

			InitPauseMenu ();
		}
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

		endText = GameObject.Find ("Canvas/End Menu/endText").GetComponent<Text> ();
		endMenu = GameObject.Find ("Canvas/End Menu");
		progressButton = GameObject.Find ("Canvas/End Menu/Progress").GetComponent<Button> ();
		endButton = GameObject.Find ("Canvas/End Menu/End").GetComponent<Button> ();
		progressButton.onClick.RemoveAllListeners ();
		endButton.onClick.AddListener (ExitToMainMenu);
	}

	void InitPauseMenu ()
	{
		pauseMenu = GameObject.Find ("Canvas/Pause Menu");
		unpauseButton = GameObject.Find ("Canvas/Pause Menu/Unpause").GetComponent<Button> ();
		exitToLobbyFromPauseButton = GameObject.Find ("Canvas/Pause Menu/ExitToLobby").GetComponent<Button> ();
		exitToMenuFromPauseButton = GameObject.Find ("Canvas/Pause Menu/ExitToMainMenu").GetComponent<Button> ();
		unpauseButton.onClick.AddListener (TogglePause);
		exitToLobbyFromPauseButton.onClick.AddListener (GotoLobby);
		exitToMenuFromPauseButton.onClick.AddListener (ExitToMainMenu);
		pauseMenu.SetActive (false);
	}

	void InitMainMenu ()
	{
		diffSlider = GameObject.Find ("difficultySlider").GetComponent<Slider> ();
		settingsButton = GameObject.Find ("Canvas/settings").GetComponent<Button> ();
		exitButton = GameObject.Find ("Canvas/exit").GetComponent<Button> ();
		startButton = GameObject.Find ("Canvas/start").GetComponent<Button> ();
		difficultyLabel = GameObject.Find ("Canvas/difficultySlider/Handle Slide Area/Handle/difficultyLabel").GetComponent<Text> ();
		settingsCanvas = GameObject.Find ("SettingsCanvas");
		menuCanvas = GameObject.Find ("Canvas");
		backSettingsButton = GameObject.Find ("SettingsCanvas/back").GetComponent<Button> ();
		startButton.onClick.AddListener (StartGame);
		exitButton.onClick.AddListener (ExitGame);
		settingsButton.onClick.AddListener (ToggleSettings);
		backSettingsButton.onClick.AddListener (ToggleSettings);
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
}
