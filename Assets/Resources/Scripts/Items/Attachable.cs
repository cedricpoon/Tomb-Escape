using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachable : MonoBehaviour {

	public string handTag = "Hand";
	public Material highlight;

	public bool IsHeld { get { return held; } }

	protected Vector3 HeldOffset;

	private GameObject player, hand;
	private bool held, corrupted, _triggerLock = true;

	private List<List<Material>> materialsRef = new List<List<Material>>();
	private Renderer[] _renderers;

	void AddMaterial(Material m) {
		foreach (List<Material> _ref in materialsRef) {
			if (!_ref.Contains (m)) {
				_ref.Add (m);
				_renderers[materialsRef.IndexOf(_ref)].materials = _ref.ToArray ();
			}
		}
	}

	void RemoveMaterial(Material m) {
		foreach (List<Material> _ref in materialsRef) {
			if (_ref.Contains (m)) {
				_ref.Remove (m);
				_renderers[materialsRef.IndexOf(_ref)].materials = _ref.ToArray ();
			}
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
		transform.localRotation = Quaternion.Euler(HeldOffset);
		transform.position = hand.gameObject.transform.position;
	}

	public void Corrupt (string name_append = "Damaged") {
		corrupted = true;
		name += " (" + name_append + ")";
	}
		
	public virtual void Trigger () { /* Not implemented */ }

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
		_renderers = GetComponentsInChildren<Renderer> ();
		foreach (Renderer _r in _renderers) {
			if (_r.GetComponent<Renderer> () != null) {
				materialsRef.Add (new List<Material> (_r.materials));
			}
		}

		hand = GameObject.FindGameObjectWithTag (handTag);
		// ref of player
		player = hand.gameObject.transform.root.gameObject;
	}
	
	// Update is called once per frame
	protected virtual void Update () {

		if (IsHeld && Input.GetButtonDown ("Fire1")) {
			if (!_triggerLock)
				Trigger ();
			else
				_triggerLock = false;
		}
	}
}
