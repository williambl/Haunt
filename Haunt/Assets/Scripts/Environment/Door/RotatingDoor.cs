using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingDoor : MonoBehaviour {

	[Header("Door Properties")]
	public bool isOpening = false; //Up = True, Down = False
	public float speed = 0.1f;
	public float open = 90f;
	public float closed = 0f;
    public float currentrotation;
    public Vector3 rotateoffset;

    private Vector3 rotatepoint;

	public void Update ()
	{
    	Move ();
		rotatepoint = transform.position + rotateoffset;
	}

	public void Move ()
	{
		if (isOpening && currentrotation < open) {
			currentrotation += speed;
			transform.RotateAround(rotatepoint, Vector3.up, currentrotation);;
		} else if (!isOpening && currentrotation > closed) {
			currentrotation -= speed;
			transform.RotateAround(rotatepoint, Vector3.up, currentrotation);
		}
	}
}
