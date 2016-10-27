using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
	
	public bool won;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (won) {
			Debug.Log ("won");
		}
	}
}
