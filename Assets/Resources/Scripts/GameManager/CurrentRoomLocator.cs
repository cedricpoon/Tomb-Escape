using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomLocator : MonoBehaviour {

	public GameObject Room;

	public int Traveled;

	private GameObject PrevRoom;

	public GameObject Player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		PrevRoom = Room;
		Collider[] cldrs = Physics.OverlapSphere (Player.transform.position, 1f);
		// Immediate colliding parent should be room
		Room = cldrs [0].transform.parent.gameObject;

		if (PrevRoom != null && PrevRoom != Room && !Room.GetComponent<VisitStatus> ().IsVisited) {
			Traveled++;
			// Checkout room
			Room.GetComponent<VisitStatus>().Checkout();
		}
	}
}
