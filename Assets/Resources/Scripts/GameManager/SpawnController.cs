using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

	public GameObject spawnArea;

	public int noOfEnemies;

	public GameObject enemy;

	public GameObject player;

	// Use this for initialization
	void Start () {
		
		for (int i = 0; i < noOfEnemies; i++) {
			
			GameObject newEnemy = (GameObject)Instantiate (enemy);

			Vector3 spawnSize = spawnArea.GetComponent<MeshRenderer> ().bounds.size;

			newEnemy.transform.position = new Vector3 (
				Random.Range(-spawnSize.x / 2, spawnSize.x / 2),
				newEnemy.transform.position.y,
				Random.Range(-spawnSize.z / 2, spawnSize.z / 2)
			);

			newEnemy.GetComponent<Detection> ().target = player;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
