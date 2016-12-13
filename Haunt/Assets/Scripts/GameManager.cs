using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public bool won;
	public bool lost;
	public GameObject text;
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
			text = GameObject.Find ("Canvas/winText");
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
		text.SetActive (value);
		text.GetComponent<Text> ().text = "You win!";
	}

	void lose (bool value) {
		text.SetActive (value);
		text.GetComponent<Text> ().text = "You lose!";
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
