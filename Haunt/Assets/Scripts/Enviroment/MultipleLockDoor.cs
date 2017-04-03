using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleLockDoor : Door {

	public List<Lock> locks;
	public List<bool> lockBools;

	public void Start ()
	{
		foreach (Lock l in locks) {
			lockBools.Add (l.isLocked);
		}
	}

	public void CheckLocks ()
	{
		if (!lockBools.Contains (false))
			isGoingUp = true;
		else
			isGoingUp = false;
	}
}
