using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	float speed;

	void Start () {
		speed = GlobalStore.now.Handgun_Bullet_Speed;
		GetComponentInChildren<TrailRenderer> ().Clear ();
	}

	void Update () {
		if (!GetComponent<Renderer> ().isVisible) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter (Collider col) {

		if (col.GetComponent<Enemy> () != null) {
			// Hit
			col.GetComponent<Enemy> ().Damage();
		}

		if (col.gameObject != GameObject.FindGameObjectWithTag ("Player") && 
			col.gameObject.GetComponent<Rigidbody>() != null) {

			// Hit anything destroy
			Destroy (this.gameObject);
		}
	}
}
