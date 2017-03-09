using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : MonoBehaviour {

	public float health = 1;
	public NPC npc;

	void Start ()
	{
		npc = GetComponent<NPC> ();
	}

	void Update ()
	{
		if (health < 0)
			health = 0;
		else if (health > 1)
			health = 1;

		if (health == 0)
			npc.Die ();
	}

	public void LoseHealth (float amount)
	{
		if (health - amount < 0)
			health = 0;
		else if (health - amount > 1)
			health = 1;
		else
			health = health - amount; 
	}
}
