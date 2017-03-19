using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour {

	public PlayerController pc;

	/// <summary>
	/// Picks up a target.
	/// </summary>
	/// <param name="target">Target to pick up.</param>
	public void PickUp (GameObject target)
	{
		target.transform.parent = transform;
		pc.holding = target;
		pc.invComponent.holdingitem = pc.holding.GetComponent<ItemComponent> ().item;
		pc.holding.GetComponent<ItemComponent> ().isHeld = true;
		target.transform.position = transform.position;
	}

	/// <summary>
	/// Drop the currently held object.
	/// </summary>
	public void Drop ()
	{
		pc.holding.transform.parent = null;
		pc.holding.GetComponent<ItemComponent> ().isHeld = false;
		pc.invComponent.holdingitem = null;
		pc.holding = null;
	}

	/// <summary>
	/// Gets the nearest holdable GameObject in the radius.
	/// Returns null if no holdable GameObjects are found in the radius specified.
	/// </summary>
	/// <param name="radius">Radius to search within.</param>
	public GameObject GetNearestHoldable (float radius)
	{
		float dist = Mathf.Infinity;
		GameObject closest = null;

		foreach (Collider coll in Physics.OverlapSphere(transform.position, radius)) {
			if (Vector3.Distance (transform.position, coll.transform.position) < dist && coll.gameObject.layer == 11 && coll.gameObject.GetComponent<ItemComponent> ().isHeld == false) {
				dist = Vector3.Distance (transform.position, coll.transform.position);
				closest = coll.gameObject;
			}
		}
		return closest;
	}
}
