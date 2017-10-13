using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6MinigameManager : MonoBehaviour, ILocked {

        public List<Lock> locks;
        public List<bool> lockBools;
        public bool canStart = false;
        bool hasStarted = false;
        UnityEngine.UI.Text minigameText;
        bool blastReady = false;

	// Use this for initialization
	public void Start () {
	    foreach (Lock l in locks) {
                l.AddLockedObject(this);
            }
	}

	// Update is called once per frame
	void Update () {
            if (!lockBools.Contains(true) && Input.GetButton("Blast")) {
                GameManager.won = true;
            }

            if (canStart && !hasStarted) {
                StartCoroutine(ToggleLockMinigame() );
                StartCoroutine(MainMinigameTimer() );
            } else if (!canStart && hasStarted) {
                StopCoroutine(ToggleLockMinigame() );
                StopCoroutine(MainMinigameTimer() );
                hasStarted = false;
            }
	}

        IEnumerator ToggleLockMinigame ()
        {
            hasStarted = true;
            for (float i = 7;;) {
                if (Random.value > 0.5) {
                    locks[Random.Range(0, locks.Count)].ToggleLock();
                }
                i += Random.Range(-3f, 2f);
                i = Mathf.Clamp(i, 0.5f, Mathf.Infinity);
                Debug.Log(i);
                yield return new WaitForSeconds(i);
            }
        }

        IEnumerator MainMinigameTimer ()
        {
            for (int i = 0;; i++) {
                if (i == 0) { //Wait for 20sec if on first iteration
                    yield return new WaitForSeconds(20f);
                } else {
                    StopCoroutine(ToggleLockMinigame() );
                    yield break;
                }
            }
        }

        public void CheckLocks ()
        {
            	lockBools.Clear ();
		foreach (Lock l in locks)
			lockBools.Add (l.isLocked);

		if (lockBools.Contains (false))
			canStart = true;
		else
			canStart = false;
        }
}
