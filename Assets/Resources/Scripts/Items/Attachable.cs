using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachable : MonoBehaviour {

	public Handholding hand;
	public string playerTag;

	public bool IsHeld { get { return held; } }

	private GameObject player;
	private bool held;

	void OnCollisionStay(Collision collision)
	{
		// check if hand entered
		if (collision.gameObject == player && !held && Input.GetButtonDown("Fire1")) {
			held = true;

			// reset rotation
			transform.rotation = Quaternion.Euler (Vector3.zero);
			GetComponent<Rigidbody> ().useGravity = false;
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			GetComponent<Collider> ().enabled = false;

			// pickup
			transform.SetParent(player.transform);
			transform.position = hand.gameObject.transform.position;

			hand.attachedItem = this;
		}
	}

	public void Unattach () {
		if (held) {
			// reset rigidbody
			GetComponent<Rigidbody> ().useGravity = true;
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			GetComponent<Collider> ().enabled = true;

			// put down
			transform.SetParent (null);
			transform.position = hand.gameObject.transform.position + Vector3.back;
			transform.Rotate (Vector3.one * 10);

			held = false;
		}
	}

	// Use this for initialization
	void Start () {
		if (hand == null)
			hand = GameObject.FindGameObjectWithTag (playerTag).GetComponentsInChildren<Handholding> ()[0];
		// ref of player
		player = hand.gameObject.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
