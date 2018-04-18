using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachable : MonoBehaviour {

	public Handholding hand;
	public string playerTag;
	public Material highlight;

	public bool IsHeld { get { return held; } }

	private GameObject player;
	private bool held;

	private List<Material> materialsRef;
	private Renderer _renderer;

	void AddMaterial(Material m) {
		if (!materialsRef.Contains (m)) {
			materialsRef.Add (m);
			_renderer.materials = materialsRef.ToArray ();
		}
	}

	void RemoveMaterial(Material m) {
		if (materialsRef.Contains (m)) {
			materialsRef.Remove (m);
			_renderer.materials = materialsRef.ToArray ();
		}
	}

	public virtual void OnCollisionStay(Collision collision)
	{
		// selectable
		if (collision.gameObject == player && !held) {
			AddMaterial (highlight);
		}

		// check if hand entered
		if (collision.gameObject == player && !held && Input.GetButtonDown("Fire1")) {
			RemoveMaterial (highlight);
			Attach ();
		}
	}

	public virtual void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject == player) {
			RemoveMaterial (highlight);
		}
	}

	public virtual void Attach () {
		held = true;

		// reset components
		GetComponent<Rigidbody> ().useGravity = false;
		GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		GetComponent<Collider> ().enabled = false;

		// pickup
		transform.parent = hand.transform;
		transform.eulerAngles = new Vector3 (
			hand.transform.rotation.eulerAngles.x + 45,
			hand.transform.rotation.eulerAngles.y + 45,
			hand.transform.rotation.eulerAngles.z
		);
		transform.position = hand.gameObject.transform.position;

		hand.attachedItem = this;
	}

	public virtual void Unattach () {
		// reset rigidbody
		GetComponent<Rigidbody> ().useGravity = true;
		GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		GetComponent<Collider> ().enabled = true;

		// put down
		transform.SetParent (null);
		transform.position = hand.gameObject.transform.position;

		held = false;
	}

	// Use this for initialization
	public virtual void Start () {
		// get renderer reference
		_renderer = GetComponentInChildren<Renderer> ();
		materialsRef = new List<Material>(_renderer.materials);

		if (hand == null) {
			hand = GameObject.FindGameObjectWithTag (playerTag).GetComponentsInChildren<Handholding> () [0];
		}
		// ref of player
		player = hand.gameObject.transform.root.gameObject;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		
	}
}
