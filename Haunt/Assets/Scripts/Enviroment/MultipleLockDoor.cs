using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleLockDoor : Door {

	public List<Lock> locks;

	public void CheckLocks () {
		int count = 0;

		foreach (Lock l in locks) {
			if (l.isLocked)
				count++;
		}
		if (count == locks.Count)
			isGoingUp = true;
		else
			isGoingUp = false;
	}
}
