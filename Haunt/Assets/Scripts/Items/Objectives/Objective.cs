using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {

	private ItemComponent itemC;
	private Rigidbody rigid;
	private Collider coll;

	void Start () {
		if(itemC == null)
			itemC = GetComponent<ItemComponent> ();

		if (!Registry.ItemExistsFromID ("objective")) {
			itemC.item = new Item ("objective", gameObject);
			Registry.RegisterItem (itemC.item);
		} else {
			Registry.FindItemByID ("objective").gObject = gameObject;
			itemC.item = Registry.FindItemByID ("objective");
		}
		
		if(rigid == null)
			rigid = GetComponent<Rigidbody> ();
		if(coll == null)
			coll = GetComponent<Collider> ();
	}

	void Update () {
		rigid.isKinematic = itemC.isHeld;
		coll.enabled = !itemC.isHeld;
	}
}
