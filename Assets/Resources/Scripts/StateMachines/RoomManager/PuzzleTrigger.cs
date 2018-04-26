using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour {

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		//Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
		Gizmos.DrawCube(transform.position, 
			new Vector3(GetComponent<MeshRenderer> ().bounds.size.x * 0.8f, GetComponent<MeshRenderer> ().bounds.size.y * 2, GetComponent<MeshRenderer> ().bounds.size.z * 0.8f));
	}

	// Update is called once per frame
	void Update () {
		bool exit = true;
		Collider[] cols = Physics.OverlapBox (transform.position, 
			new Vector3(GetComponent<MeshRenderer> ().bounds.size.x * 0.8f, GetComponent<MeshRenderer> ().bounds.size.y * 2, GetComponent<MeshRenderer> ().bounds.size.z * 0.8f));
		foreach (Collider c in cols) {
			if (c.GetComponent<Enemy> () != null)
				exit = false;
		}

		if (exit) {
			bool[] memory = GetComponentInParent<PuzzleVisit> ().Memory;

			if (!memory[0])
				GetComponentInParent<WallRemoval> ().ActivateForward ();
			if (!memory[1])
				GetComponentInParent<WallRemoval> ().ActivateBack ();
			if (!memory[2])
				GetComponentInParent<WallRemoval> ().ActivateLeft ();
			if (!memory[03])
				GetComponentInParent<WallRemoval> ().ActivateRight ();

			Destroy (this);
		}
	}
}
