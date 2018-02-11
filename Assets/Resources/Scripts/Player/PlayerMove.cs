using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	[Range(1f, 10f)]
	public float speed = 5f;

	[Range(1f, 10f)]
	public float sprint = 8f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		float s = (Input.GetAxis("Sprint") != 0) ? sprint : speed;

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * s;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * s;

		transform.Translate(x, 0, z);
	}
}
