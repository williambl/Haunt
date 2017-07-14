using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleLockRotatingDoor : RotatingDoor {

	public List<Lock> locks;
	public List<bool> lockBools;

	public void CheckLocks ()
	{
		lockBools.Clear ();
		foreach (Lock l in locks)
			lockBools.Add (l.isLocked);

		if (!lockBools.Contains (true))
			isOpening = true;
		else
			isOpening = false;
	}
}
