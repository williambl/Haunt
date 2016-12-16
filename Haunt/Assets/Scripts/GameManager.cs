using UnityEngine;
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

	void OnEnable () {
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}
		
	void Start () {
		DontDestroyOnLoad (gameObject);
		Init ();
	}

	void Init () {
		if (SceneManager.GetActiveScene ().name == "menu") {
			diffSlider = GameObject.Find ("difficultySlider").GetComponent<Slider> ();
		} 
		else if (SceneManager.GetActiveScene ().name == "game") {
			endText = GameObject.Find ("Canvas/End Menu/endText");
			endMenu = GameObject.Find ("Canvas/End Menu");
			progressButton = GameObject.Find("Canvas/End Menu/Progress");
		}
	}
	
	void Update () {
		if (SceneManager.GetActiveScene ().name == "menu") {
			difficultyLevel = (Difficulty)diffSlider.value;
		} 
		else if (SceneManager.GetActiveScene ().name == "game") {
			win (won);
			lose (lost);
		}
	}

	void win (bool value) {
		Debug.Log ("won");
		endMenu.SetActive (value);
		endText.SetActive (value);
		endText.GetComponent<Text> ().text = "You win!";
		progressButton.GetComponentInChildren<Text> ().text = "Next Level";
	}

	void lose (bool value) {
		Debug.Log ("lost");
		endMenu.SetActive (value);
		endText.SetActive (value);
		endText.GetComponent<Text> ().text = "You lose!";
		progressButton.GetComponentInChildren<Text> ().text = "Restart Level";
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	public void StartGame () {
		SceneManager.LoadScene("game");
	}
		
	public void ChangeDifficulty (int value) {
		difficultyLevel = (Difficulty)value;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		Init ();
	}
}
