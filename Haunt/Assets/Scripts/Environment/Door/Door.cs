using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	[Header("Door Properties")]
	public bool isGoingUp = false; //Up = True, Down = False
	public float speed = 0.1f;
	public float top = 2.75f;
	public float bottom = -3.75f;

	public void Update () 
	{
		Move ();
	}

	public void Move () 
	{
		if (isGoingUp && transform.position.y < top) {
			transform.position += new Vector3 (0, speed, 0);
		} else if (!isGoingUp && transform.position.y > bottom) {
			transform.position -= new Vector3 (0, speed, 0);
		}
	}
}
