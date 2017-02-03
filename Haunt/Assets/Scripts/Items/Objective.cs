using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {

	ItemComponent itemC;
	Rigidbody rigid;
	Collider coll;

	void Start () {
		itemC = GetComponent<ItemComponent> ();
		itemC.item = new Item ("objective", gameObject);
		Registry.RegisterItem (itemC.item);
		rigid = GetComponent<Rigidbody> ();
		coll = GetComponent<Collider> ();
	}

	void Update () {
		rigid.isKinematic = itemC.isHeld;
		coll.enabled = !itemC.isHeld;
	}
}
