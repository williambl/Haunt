using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {

	GameManager manager;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("Manager").GetComponent<GameManager> ();
		Registry.RegisterItem (new Item("objective", "objective", 0, "the objective"));
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
