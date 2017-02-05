using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective1 : MonoBehaviour {

	GameManager manager;
	ItemComponent itemC;
	Rigidbody rigid;
	Collider coll;

	void Start () {
		itemC = GetComponent<ItemComponent> ();
		itemC.item = new Item ("objective1", gameObject);
		Registry.RegisterItem (itemC.item);
		rigid = GetComponent<Rigidbody> ();
		coll = GetComponent<Collider> ();
		manager = GameObject.Find ("Manager").GetComponent<GameManager> ();
	}

	void Update () {
		rigid.isKinematic = itemC.isHeld;
		coll.enabled = !itemC.isHeld;

		if (itemC.isHeld) {
			manager.won = true;
		}
	}
}
