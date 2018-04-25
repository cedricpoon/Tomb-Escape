using UnityEngine;
using System.Collections;

public class Vision : MonoBehaviour
{
	public GameObject Target;
	Enemy host;

	// Use this for initialization
	void Start ()
	{
		host = GetComponentInParent<Enemy> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter(Collider other) {
		OnTriggerStay (other);
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject == Target && Target != null) {
			host.Trace (Target);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject == Target && Target != null) {
			host.Flee ();
		}
	}
}

