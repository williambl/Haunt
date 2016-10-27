using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	RaycastHit ground;
	public float HoverHeight = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Hovering
		if (Physics.Raycast (transform.position, Vector3.down, out ground)) 
		{
			if (ground.transform.gameObject.layer == 8)
			{
				transform.position = new Vector3(transform.position.x, ground.point.y + HoverHeight, transform.position.z);
			}
		}
	}
}
