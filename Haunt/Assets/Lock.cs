using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour {

	public bool isLocked;
	public MultipleLockDoor door;

	Renderer rend;
	[SerializeField] Material lockedMat;
	[SerializeField] Material unlockedMat;

	public void Start () {

		rend = GetComponent<Renderer> ();
	}

	public void ToggleLock () {
		isLocked = !isLocked;
		door.CheckLocks ();

		rend.material = isLocked ? lockedMat : unlockedMat;
	}
}
