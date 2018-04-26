using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleVisit : VisitStatus
{
	public bool[] Memory = new bool[4];

	public List<GameObject> ListOfMobs;

	void Start() {
		Checkout ();
	}

	public override void Checkout ()
	{
		base.Checkout ();

		// Re-enable walls
		if (transform.Find ("WallFront") != null) {
			Memory [0] = transform.Find ("WallFront").gameObject.activeSelf;
			transform.Find ("WallFront").gameObject.SetActive (true);
		}

		if (transform.Find ("WallBack") != null) {
			Memory [1] = transform.Find ("WallBack").gameObject.activeSelf;
			transform.Find ("WallBack").gameObject.SetActive (true);
		}

		if (transform.Find ("WallLeft") != null) {
			Memory [2] = transform.Find ("WallLeft").gameObject.activeSelf;
			transform.Find ("WallLeft").gameObject.SetActive (true);
		}

		if (transform.Find ("WallRight") != null) {
			Memory [3] = transform.Find ("WallRight").gameObject.activeSelf;
			transform.Find ("WallRight").gameObject.SetActive (true);
		}

		for (int i = 0; i < GlobalStore.now.Room_Puzzle_Mobs; i++) {

			GameObject newEnemy = (GameObject)Instantiate (ListOfMobs [Random.Range (0, ListOfMobs.Count)]);

			Vector3 spawnSize = transform.Find ("Spawner").GetComponent<MeshRenderer> ().bounds.size;

			newEnemy.transform.position = new Vector3 (
				Random.Range (-spawnSize.x / 2, spawnSize.x / 2) + transform.position.x,
				newEnemy.transform.position.y + spawnSize.y * 2,
				Random.Range (-spawnSize.z / 2, spawnSize.z / 2) + transform.position.z
			);
		}
	}
}

