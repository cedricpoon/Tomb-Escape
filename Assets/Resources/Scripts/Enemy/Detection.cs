using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour {

	public GameObject target;

	[Range(1f, 10f)]
	public float speed = 2f;

	[Range(1f, 10f)]
	public float rotationDamping = 8f;

	Dead deadRef;

	void Awake() {

		deadRef = GetComponent<Dead> ();		
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision collision) {

		if (!deadRef.isDead) {

			// iff caught
			if (collision.gameObject == target) {

				Debug.LogWarning ("Detection[Todo]: To trigger player harm / dead event");

				// To-do: To trigger player harm / dead event
				/* ProgressController pc = GameObject.Find ("GameManager")
				.GetComponent<ProgressController> ();
				pc.doLose (); */
			}
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

		if (!deadRef.isDead) {
			
			if (gameObject == target) {
				DoChase ();
			}
			return gameObject == target;
		}
		return false;
	}
}
