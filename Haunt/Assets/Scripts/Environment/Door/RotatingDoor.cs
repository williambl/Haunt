using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingDoor : Door {

	[Header("Door Properties")]
	public bool isOpening = false; //Up = True, Down = False

    public float changingRot;
    public Vector3 unchangingRot;
    public float perc = 0;

    public void Start()
	{
		unchangingRot = transform.rotation.eulerAngles;
	}

	public new void Update ()
	{
    	Move ();
	}

	public new void Move ()
    {
		perc = Mathf.Clamp(perc, 0, 1);
		if (isOpening && perc < 1) {
			perc += speed;
		} else if (!isOpening && perc > 0) {
            perc -= speed;
        }
        changingRot = Mathf.LerpAngle(bottom, top, perc);
		transform.localRotation = Quaternion.Euler(unchangingRot.x, changingRot, unchangingRot.z);
	}
}
