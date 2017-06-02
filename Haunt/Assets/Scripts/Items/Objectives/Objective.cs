using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {

	ItemComponent itemC;
	Rigidbody rigid;
	Collider coll;

	void Start () {
		itemC = GetComponent<ItemComponent> ();

		if (!Registry.ItemExistsFromID ("objective")) {
			itemC.item = new Item ("objective", gameObject);
			Registry.RegisterItem (itemC.item);
		} else {
			Registry.FindItemByID ("objective").gObject = gameObject;
			itemC.item = Registry.FindItemByID ("objective");
		}
		
		rigid = GetComponent<Rigidbody> ();
		coll = GetComponent<Collider> ();
	}

	void Update () {
		rigid.isKinematic = itemC.isHeld;
		coll.enabled = !itemC.isHeld;
	}
}
