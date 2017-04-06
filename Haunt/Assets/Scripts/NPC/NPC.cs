using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	protected RaycastHit ground;
	public float HoverHeight = 1;

	protected UnityEngine.AI.NavMeshAgent agent;

	protected Rigidbody rigid;

	protected GameManager manager;

	public GameObject dropObject;

	protected void Start () 
	{
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.autoBraking = false;
		rigid = GetComponent<Rigidbody> ();
		manager = GameObject.Find ("Manager").GetComponent<GameManager> ();
	}

	protected void Hover (float height)
	{
		if (Physics.Raycast (transform.position, Vector3.down, out ground)) {
			if (ground.transform.gameObject.layer == 8) {
				transform.position = new Vector3 (transform.position.x, ground.point.y + height, transform.position.z);
			}
		}
	}

	public virtual void Die ()
	{
		if (dropObject != null)
			Drop (dropObject);
		Destroy (gameObject);
	}

	public virtual void Drop (GameObject obj)
	{
		Instantiate (obj, transform.position, transform.rotation);
	}
}
