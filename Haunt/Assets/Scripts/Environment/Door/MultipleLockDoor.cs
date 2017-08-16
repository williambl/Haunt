using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleLockDoor : Door, ILocked {

	public List<Lock> locks;
	public List<bool> lockBools;

	public void Start ()
	{
		foreach (Lock l in locks) {
			l.AddLockedObject(this);
		}
	}

	public void CheckLocks ()
	{
		lockBools.Clear ();
		foreach (Lock l in locks)
			lockBools.Add (l.isLocked);

		if (!lockBools.Contains (true))
			isGoingUp = true;
		else
			isGoingUp = false;
	}
}
