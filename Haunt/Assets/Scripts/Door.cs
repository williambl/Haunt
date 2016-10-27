using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.layer == 10) 
		{
			transform.position += new Vector3 (0, 5, 0);
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.layer == 10) 
		{
			transform.position -= new Vector3 (0, 5, 0);
		}
	}
}
