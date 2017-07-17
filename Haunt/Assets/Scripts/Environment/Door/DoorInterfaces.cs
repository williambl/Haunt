using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKey
{
    void OnTriggerEnter (Collider other);
    void OnTriggerExit (Collider other);
}

public interface ILock
{
    void Start ();
    void CheckLocks ();
}
