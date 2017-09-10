using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6MinigameManager : MonoBehaviour, ILocked {

        public List<Lock> locks;
        public List<bool> lockBools;
        public bool canStart = false;
        bool hasStarted = false;
        GameManager manager;
        
	// Use this for initialization
	public new void Start () {
            manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager> ();
	    foreach (Lock l in locks) {
                l.AddLockedObject(this);
            }	
	}
	
	// Update is called once per frame
	void Update () {
            if (canStart && !hasStarted) {
                StartCoroutine(Minigame() );
                StartCoroutine(MinigameTimer() );
            } else if (!canStart && hasStarted) {
                StopCoroutine(Minigame() );
                StopCoroutine(MinigameTimer() );
                hasStarted = false;
            }
	}

        IEnumerator Minigame ()
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

        IEnumerator MinigameTimer ()
        {
            for (int i = 0;; i++) {
                if (i == 0) {
                    yield return new WaitForSeconds(20f);
                } else {
                    StopCoroutine(Minigame() );
                    manager.won = true;
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
