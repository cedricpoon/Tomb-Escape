using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensJittering : MonoBehaviour {

	[Range(1f, 5f)]
	public float xSpeed = 5f;

	[Range(1f, 5f)]
	public float ySpeed = 5f;

	[Range(0f, 5f)]
	public float sprintExtra = 1f;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		// Set original offset
		offset = transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		var x = Input.GetAxis("Horizontal");
		var z = Input.GetAxis("Vertical");

		var s = Input.GetAxis ("Sprint");

		transform.rotation = Quaternion.Euler (new Vector3 (
			offset.x + xSpeed * -z + sprintExtra * s * -z,
			offset.y + ySpeed * x + sprintExtra * s * x,
			offset.z
		));
	}
}
