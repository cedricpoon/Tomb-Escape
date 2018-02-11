using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour {

	public Vector3 fallFactor;

	public bool isDead = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void suicide (){

		isDead = true;

		Rigidbody rgbd = GetComponent<Rigidbody> ();
		rgbd.useGravity = true;

		rgbd.constraints = RigidbodyConstraints.None;

		transform.Rotate(fallFactor, Time.deltaTime);

		new List<DetectionChild> (transform.GetComponentsInChildren<DetectionChild> ()).ForEach (d => {
			d.enabled = false;
		});

		GetComponent<Rotation> ().enabled = false;
		GetComponent<Detection> ().enabled = false;

		GetComponentInChildren<Light> ().enabled = false;
	}
}
