using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

	public bool desolidify;
	public bool possess;
	public bool invisibility;
	public bool blast;

	GameManager manager;

	void Start ()
	{
		manager = GameObject.Find ("Manager").GetComponent<GameManager> ();

		if (manager.maxLevelReached > 0)
			desolidify = true;
		if (manager.maxLevelReached > 1)
			possess = true;
		if (manager.maxLevelReached > 2)
			invisibility = true;
		if (manager.maxLevelReached > 3)
			blast = true;

		if (desolidify)
			gameObject.layer = 9;
		else
			gameObject.layer = 12;
	}

	void Update () 
	{
		
	}
}
