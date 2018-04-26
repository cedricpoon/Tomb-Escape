using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour {

	public int noOfMobs;

	public List<GameObject> ListOfMobs;

	// Use this for initialization
	protected virtual void Start () {
		noOfMobs = GlobalStore.now.Room_Mob_Number;

		if (ListOfMobs.Count > 0) {
			for (int i = 0; i < noOfMobs; i++) {

				GameObject newEnemy = (GameObject)Instantiate (ListOfMobs [Random.Range (0, ListOfMobs.Count)]);

				Vector3 spawnSize = transform.Find ("Floor").GetComponent<MeshRenderer> ().bounds.size;

				newEnemy.transform.position = new Vector3 (
					Random.Range (-spawnSize.x / 2, spawnSize.x / 2) + transform.position.x,
					transform.Find ("WallBack").GetComponent<MeshRenderer> ().bounds.size.y / 2,
					Random.Range (-spawnSize.z / 2, spawnSize.z / 2) + transform.position.z
				);
			}
		}
	}
}
