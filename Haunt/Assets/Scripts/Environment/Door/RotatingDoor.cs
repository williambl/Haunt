using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingDoor : Door {

	[Header("Door Properties")]
	public bool isOpening = false; //Up = True, Down = False
	public float speed = 0.1f;
	public Vector3 open;
    public Vector3 closed;

    public float currentrotation;


    public void Start()
    {
        closed = transform.rotation.eulerAngles;
	}

	public void Update ()
	{
    	Move ();
	}

	public void Move ()
	{
		if (isOpening && currentrotation < 1) {
			currentrotation += speed;
			transform.rotation = Quaternion.Euler(Vector3.Slerp(closed, open, currentrotation));
		} else if (!isOpening && currentrotation >= 0) {
			currentrotation -= speed;
			transform.rotation = Quaternion.Euler(Vector3.Slerp(closed, open, currentrotation));
		}
	}
}
