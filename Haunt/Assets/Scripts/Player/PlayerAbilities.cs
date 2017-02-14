﻿using System.Collections;
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

		if (manager.level > 0)
			desolidify = true;
		if (manager.level > 1)
			possess = true;
		if (manager.level > 2)
			invisibility = true;
		if (manager.level > 3)
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