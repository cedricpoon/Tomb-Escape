using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

	[Range(10f, 50f)]
	public float speed = 30f;

	[Range(0f, 10f)]
	public float secondsToTurn = 2f;

	Vector3 direction = Vector3.up;

	// Use this for initialization
	void Start () {
		StartCoroutine ("DoUpdateDirection");
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Rotate(direction, Time.deltaTime * speed);
	}

	IEnumerator DoUpdateDirection () {
		for(;;) {

			direction = (Random.Range (0, 2) == 1) ? 
				Vector3.up :
				Vector3.down
				;
			yield return new WaitForSeconds(secondsToTurn);
		}
	}
}
