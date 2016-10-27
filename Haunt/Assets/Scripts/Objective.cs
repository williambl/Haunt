using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {

	GameManager manager;

	// Use this for initialization
	void Start () {
		manager = FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			manager.won = true;
		}
	}
}
