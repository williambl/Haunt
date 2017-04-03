using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour {

	public bool isLocked;
	public MultipleLockDoor door;

	public void ToggleLock () {
		isLocked = !isLocked;
		door.CheckLocks ();
	}
}
