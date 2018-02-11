using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerController : MonoBehaviour {

	public string targetTag;

	public bool hasEnemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay (Collider collider) {
		
		hasEnemy = collider.tag == "Enemy" || hasEnemy;
	}

	void OnTriggerExit (Collider collider) {

		if (collider.tag == "Enemy")
			hasEnemy = false;
	}
}
