using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
	
	public bool won;
	public GameObject text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (won) {
			text.SetActive (true);
		}
	}
}
