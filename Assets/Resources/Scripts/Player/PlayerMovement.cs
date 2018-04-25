using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[Range(1f, 10f)]
	public float speed = 5f;

	[Range(1f, 10f)]
	public float sprint = 8f;

	Animator _ani;

	// Use this for initialization
	void Start () {
		
		_ani = gameObject.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		float s = (Input.GetAxis("Sprint") != 0) ? sprint : speed;

		var z = Input.GetAxis("Vertical") * Time.deltaTime * s;

		// Animation cycle
		if (Input.GetAxis("Vertical") > 0) 
		{
			_ani.SetInteger ("animPara", 1);
		}
		else if (Input.GetAxis("Vertical") < 0) 
		{
			_ani.SetInteger ("animPara", 2);
		}
		else 
		{
			_ani.SetInteger ("animPara", 0);
		}
			

		transform.Translate(0, 0, z);


	}

	void FixedUpdate() {

		Plane playerPlane = new Plane(Vector3.up, transform.position);

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		float hitdist = 0.0f;

		if (playerPlane.Raycast (ray, out hitdist)) 
		{
			Vector3 targetPoint = ray.GetPoint(hitdist);

			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
		}
	}
}
