using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour {

	public bool isLocked = true;
	public List<ILocked> doors = new List<ILocked>();

	Renderer rend;
	[SerializeField] Material lockedMat;
	[SerializeField] Material unlockedMat;

	public void Start () {
		if(rend == null)
			rend = GetComponent<Renderer> ();
	}

	public void ToggleLock () {
		isLocked = !isLocked;
		foreach (ILocked door in doors)
			door.CheckLocks ();

		rend.material = isLocked ? lockedMat : unlockedMat;
	}

	public void AddLockedObject (ILocked item) {
		doors.Add(item);
	}
}
