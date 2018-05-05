using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour {

	public bool[] Memory = new bool[4];

	public List<GameObject> ListOfMobs;

	bool once = true;

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		//Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
		Gizmos.DrawCube(transform.position, 
			new Vector3(GetComponent<MeshRenderer> ().bounds.size.x * 0.8f, GetComponent<MeshRenderer> ().bounds.size.y * 2, GetComponent<MeshRenderer> ().bounds.size.z * 0.8f));
	}

	void OnTriggerEnter(Collider col) {

		if (GetComponentInParent<VisitStatus> ().IsVisited && once 
			&& col.gameObject == GameObject.FindGameObjectWithTag("Player")) {
			// Once only
			once = false;

			new MessageBox (
				GameObject.FindGameObjectWithTag("Player").GetComponent<MonoBehaviour>(),
				"All exits are blocked. How can I get out?",
				5,
				GlobalStore.ON_SCREEN_LOWER_Y
			).Show ();

			// Re-enable walls
			if (transform.parent.Find ("WallFront") != null) {
				Memory [0] = transform.parent.Find ("WallFront").gameObject.activeSelf;
				transform.parent.Find ("WallFront").gameObject.SetActive (true);
			}

			if (transform.parent.Find ("WallBack") != null) {
				Memory [1] = transform.parent.Find ("WallBack").gameObject.activeSelf;
				transform.parent.Find ("WallBack").gameObject.SetActive (true);
			}

			if (transform.parent.Find ("WallLeft") != null) {
				Memory [2] = transform.parent.Find ("WallLeft").gameObject.activeSelf;
				transform.parent.Find ("WallLeft").gameObject.SetActive (true);
			}

			if (transform.parent.Find ("WallRight") != null) {
				Memory [3] = transform.parent.Find ("WallRight").gameObject.activeSelf;
				transform.parent.Find ("WallRight").gameObject.SetActive (true);
			}

			for (int i = 0; i < GlobalStore.now.Room_Puzzle_Mobs; i++) {

				GameObject newEnemy = (GameObject)Instantiate (ListOfMobs [Random.Range (0, ListOfMobs.Count)]);

				Vector3 spawnSize = GetComponent<MeshRenderer> ().bounds.size;

				newEnemy.transform.position = new Vector3 (
					Random.Range (-spawnSize.x / 2, spawnSize.x / 2) + transform.position.x,
					newEnemy.transform.position.y + spawnSize.y * 2,
					Random.Range (-spawnSize.z / 2, spawnSize.z / 2) + transform.position.z
				);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (!once) {
			bool exit = true;
			Collider[] cols = Physics.OverlapBox (transform.position, 
				                 new Vector3 (GetComponent<MeshRenderer> ().bounds.size.x * 0.8f, GetComponent<MeshRenderer> ().bounds.size.y * 2, GetComponent<MeshRenderer> ().bounds.size.z * 0.8f));
			foreach (Collider c in cols) {
				if (c.GetComponent<Enemy> () != null)
					exit = false;
			}

			if (exit) {

				if (!Memory [0])
					GetComponentInParent<WallRemoval> ().ActivateForward ();
				if (!Memory [1])
					GetComponentInParent<WallRemoval> ().ActivateBack ();
				if (!Memory [2])
					GetComponentInParent<WallRemoval> ().ActivateLeft ();
				if (!Memory [3])
					GetComponentInParent<WallRemoval> ().ActivateRight ();

				new MessageBox (
					GameObject.FindGameObjectWithTag("Player").GetComponent<MonoBehaviour>(),
					"Yes! I am free!",
					5,
					GlobalStore.ON_SCREEN_LOWER_Y
				).Show ();

				Destroy (this);
			}
		}
	}
}
