using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachable : MonoBehaviour {

	public string handTag = "Hand";
	public Material highlight;

	public bool IsHeld { get { return held; } }

	private GameObject player, hand;
	private bool held, corrupted;

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

	protected virtual void OnCollisionStay(Collision collision)
	{
		// selectable
		if (collision.gameObject == player && !held) {
			AddMaterial (highlight);
		}

		// check if hand entered
		if (collision.gameObject == player && !held && Input.GetButtonDown("Fire1")) {
			player.GetComponent<Inventory> ().Add (this);
		}
	}

	protected virtual void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject == player) {
			RemoveMaterial (highlight);
		}
	}

	public virtual void Resume () {
		gameObject.SetActive (true);
	}

	public virtual void Pause () {
		gameObject.SetActive (false);
	}

	public virtual void Attach () {
		RemoveMaterial (highlight);

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
	}

	public void Corrupt () {
		corrupted = true;
	}
		
	public virtual void Trigger () {
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

		if (corrupted)
			Destroy (this);
	}

	// Use this for initialization
	protected virtual void Start () {
		// get renderer reference
		_renderer = GetComponentInChildren<Renderer> ();
		materialsRef = new List<Material>(_renderer.materials);

		hand = GameObject.FindGameObjectWithTag (handTag);
		// ref of player
		player = hand.gameObject.transform.root.gameObject;
	}
	
	// Update is called once per frame
	protected virtual void Update () {

		if (IsHeld && Input.GetButtonDown ("Fire1")) {
			Trigger ();
		}
	}
}
