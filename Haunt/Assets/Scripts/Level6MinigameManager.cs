using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6MinigameManager : MonoBehaviour, ILocked {

        public List<Lock> locks;
        public List<bool> lockBools;
        public bool hasStarted = false;
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
            if (hasStarted)
                StartCoroutine(Minigame() );
            else
                StopCoroutine(Minigame() );
	}

        IEnumerator Minigame ()
        {
            for (float i = 10;;) {
                if (Random.value > 0.5) {
                    locks[Random.Range(0, locks.Count)].ToggleLock();
                }
                i += Random.Range(-3f, 2f);
                yield return new WaitForSeconds(i);
            }
        }

        public void CheckLocks ()
        {
            	lockBools.Clear ();
		foreach (Lock l in locks)
			lockBools.Add (l.isLocked);

		if (lockBools.Contains (false))
			hasStarted = true;
		else
			hasStarted = false;
        }
}
