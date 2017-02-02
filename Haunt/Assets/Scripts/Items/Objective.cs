using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {

	GameManager manager;

	ItemComponent itemC;

	// Use this for initialization
	void Start () {
		itemC = GetComponent<ItemComponent> ();
		itemC.item = new Item ("objective", gameObject);
		manager = GameObject.Find ("Manager").GetComponent<GameManager> ();
		Registry.RegisterItem (itemC.item);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other)
	{
		//if (other.gameObject.tag == "Player") {
		//	manager.won = true;
		//}
	}
}
