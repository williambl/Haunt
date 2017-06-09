using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective1 : MonoBehaviour {

	private GameManager manager;
	private ItemComponent itemC;
	private Rigidbody rigid;
	private Collider coll;

	void Start () {
		if(itemC == null)
			itemC = GetComponent<ItemComponent> ();

		if (!Registry.ItemExistsFromID ("objective1")) {
			itemC.item = new Item ("objective1", gameObject);
			Registry.RegisterItem (itemC.item);
		} else {
			Registry.FindItemByID ("objective1").gObject = gameObject;
			itemC.item = Registry.FindItemByID ("objective1");
		}

		if(rigid == null)
			rigid = GetComponent<Rigidbody> ();
		if(coll == null)
			coll = GetComponent<Collider> ();
		if(manager == null)
			manager = GameObject.FindGameObjectWithTag ("Manager").GetComponent<GameManager> ();
	}

	void Update () {
		rigid.isKinematic = itemC.isHeld;
		coll.enabled = !itemC.isHeld;

		if (itemC.isHeld) {
			manager.won = true;
		}
	}
}
