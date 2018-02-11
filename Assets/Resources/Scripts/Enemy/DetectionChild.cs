using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionChild : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay (Collider collider) {
		
		transform.parent.GetComponent<Detection> ()
			.DoCheck(collider.gameObject);
	}
}
