using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour {

	public Vector3 fallFactor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void suicide (){
		
		Rigidbody rgbd = GetComponent<Rigidbody> ();
		rgbd.useGravity = true;

		rgbd.constraints = RigidbodyConstraints.None;

		transform.Rotate(fallFactor, Time.deltaTime);

		GetComponent<Rotation> ().enabled = false;
		GetComponent<Detection> ().enabled = false;

		GetComponentInChildren<Light> ().enabled = false;
	}
}
