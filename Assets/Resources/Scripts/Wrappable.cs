using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrappable : MonoBehaviour {

	private List<List<Material>> materialsRef = new List<List<Material>>();
	private Renderer[] _renderers;

	[SerializeField]
	protected Material Wrapper;

	protected void AddMaterial(Material m) {

		foreach (List<Material> _ref in materialsRef) {
			if (!_ref.Contains (m)) {
				_ref.Add (m);
				_renderers[materialsRef.IndexOf(_ref)].materials = _ref.ToArray ();
			}
		}
	}

	protected void RemoveMaterial(Material m) {
		foreach (List<Material> _ref in materialsRef) {
			if (_ref.Contains (m)) {
				_ref.Remove (m);
				_renderers[materialsRef.IndexOf(_ref)].materials = _ref.ToArray ();
			}
		}
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
	}
}
