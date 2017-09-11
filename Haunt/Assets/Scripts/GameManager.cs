using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

        public static GameManager manager;
	
	public static bool won;
	public static bool lost;

	public static Difficulty difficultyLevel;

	public static int level;

	public bool existedBefore = false;

	public static GameState gameState;
	public static GameState unpausedState;

	public static int maxLevelReached = -1;

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

			GUIManager.InitMainMenu ();
		} else if (SceneManager.GetActiveScene ().name.StartsWith ("level")) {
			gameState = GameState.PLAYING;
			won = false;
			lost = false;

			GUIManager.InitPauseMenu ();
			GUIManager.InitEndMenu ();

			if (level > maxLevelReached)
				maxLevelReached = level;
		} else if (SceneManager.GetActiveScene ().name == "lobby") {
			gameState = GameState.LOBBY;
			won = false;
			lost = false;

			GUIManager.InitPauseMenu ();
		}
		GUIManager.InitSaveLoadMenu ();
		existedBefore = true;
                GameManager.manager = this;
	}
	
	void Update () {
		if (SceneManager.GetActiveScene ().name == "menu") {
		    difficultyLevel = GUIManager.GetDifficultyFromSlider();
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
                GUIManager.endMenu.SetActive(hasWon || hasLost);
		if (hasWon) {
			gameState = GameState.WON;
                        GUIManager.Win();
		} else if (hasLost){
			gameState = GameState.LOST;
                        GUIManager.Lose();
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
	public static void StartGame () {
		level = -1;
		GotoLobby ();
	}
		
	/// <summary>
	/// Changes the difficulty.
	/// </summary>
	/// <param name="value">Difficulty value.</param>
	public static void SetDifficultyLevel (int value) {
		difficultyLevel = (Difficulty)value;
	}

	/// <summary>
	/// Switches to the next level.
	/// </summary>
	public static void NextLevel () {
		gameState = GameState.LOADING;
		level++;
		SceneManager.LoadScene ("level" + level);
	}

	/// <summary>
	/// Restarts the level.
	/// </summary>
	public static void RestartLevel () {
		gameState = GameState.LOADING;
		SceneManager.LoadScene ("level" + level);
	}

	/// <summary>
	/// Goes to the specified level.
	/// </summary>
	/// <param name="level">Level to go to.</param>
	public static void GotoLevel (int targetLevel) {
		gameState = GameState.LOADING;
		level = targetLevel;
		SceneManager.LoadScene ("level" + targetLevel);
	}

	/// <summary>
	/// Exits to the main menu.
	/// </summary>
	public static void ExitToMainMenu () {
		gameState = GameState.LOADING;
		SceneManager.LoadScene ("menu");
	}

	/// <summary>
	/// Goes to the lobby.
	/// </summary>
	public static void GotoLobby () {
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
	public static void TogglePause ()
	{
            GUIManager.TogglePauseCanvas();
		if (gameState == GameState.PAUSED) {
			gameState = unpausedState;
		} else {
			unpausedState = gameState;
			gameState = GameState.PAUSED;
		}
	}

	/// <summary>
	/// Exits the game.
	/// </summary>
	public static void ExitGame ()
	{
		Application.Quit ();
	}


	public static void SaveGame (string saveName)
	{
		Game game = new Game (level, gameState, level);
		SaverLoader.Save (saveName, game);
	}

	public static void LoadGame (string saveName)
	{
		Game game = SaverLoader.Load (saveName);
		GotoLevel (game.level);
		gameState = game.gameState;
	}

	public static void SaveGameFromMenu ()
	{
		string saveName = GUIManager.saveNameField.text + ".hsv";
		SaveGame (saveName);
	}

	public static void LoadGameFromMenu ()
	{
		string saveName = GUIManager.saveNameField.text + ".hsv";
		LoadGame (saveName);
	}
}
