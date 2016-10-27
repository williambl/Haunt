using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	bool isGoingUp = false; //Up = True, Down = False
	public float speed = 0.1f;
	public float top = 2.75f;
	public float bottom = -3.75f;


	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (isGoingUp && transform.position.y < top) {
			transform.position += new Vector3 (0, speed, 0);
		} else if (!isGoingUp && transform.position.y > bottom) {
			transform.position -= new Vector3 (0, speed, 0);
		}
	
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.layer == 10) 
		{
			isGoingUp = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.layer == 10) 
		{
			isGoingUp = false;
		}
	}
}
