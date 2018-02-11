using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour {

	public GameObject target;

	[Range(1f, 10f)]
	public float speed = 2f;

	[Range(1f, 10f)]
	public float rotationDamping = 8f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision collision) {

		// iff caught
		if (collision.gameObject == target) {

			ProgressController pc = GameObject.Find ("GameManager")
				.GetComponent<ProgressController> ();

			pc.doEndGame();
		}
	}

	void DoChase () {

		// Rotate to player
		var rotation = Quaternion.LookRotation(
			target.transform.position - transform.position
		);

		transform.rotation = Quaternion.Slerp(
			transform.rotation, 
			rotation, 
			Time.deltaTime * rotationDamping
		);

		// Move towards player
		transform.position += 
			transform.forward * 
			speed * 
			Time.deltaTime;
	}

	public bool DoCheck (GameObject gameObject) {

		if (gameObject == target) {
			DoChase ();
		}
		return gameObject == target;
	}
}
