using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	protected RaycastHit ground;
	public float HoverHeight = 1;

	protected UnityEngine.AI.NavMeshAgent agent;

	protected Rigidbody rigid;

	protected void Start () 
	{
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.autoBraking = false;
		rigid = GetComponent<Rigidbody> ();
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
		Destroy (gameObject);
	}
}