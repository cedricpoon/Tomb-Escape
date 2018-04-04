using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

	[Range(1, 50)]
	public int renderSize;

	[Range(1, 50)]
	public int renderEvery;

	public float roomSize = 20;

	public CurrentRoomLocator roomRef;

	[Header("Out-of-order")]
	public GameObject[] hostileRooms;

	[Header("In-order with Storyline")]
	public int eventProgress;
	public GameObject[] eventRooms;

	enum Direction { E, S, W, N };

	// Use this for initialization
	void Start () {
		// Render renderSize maze first
		StartCoroutine("RenderNext", SpawnRoomWithLoc(eventRooms[eventProgress++], Vector3.zero));
	}

	IEnumerator RenderNext(GameObject orgin) {
		Stack<GameObject> dfsStack = new Stack<GameObject> ();
		dfsStack.Push (orgin);

		for (int i = 0; i < renderSize && dfsStack.Count > 0; i++) {
			int dirSize = GetNoOfDirections ();
			int _dir = Random.Range(0, dirSize);

			bool needBackTrack = true;
			for (int j = 0; j < dirSize; j++) {
				// Get Next direction clockwise
				Direction d = ResolveDirection((_dir + j) % dirSize);

				if (CheckNextAreaValid (dfsStack.Peek(), d)) {
					// Actual spawn of new room
					GameObject nextRoomProto = hostileRooms [Random.Range (0, hostileRooms.Length)];
					// if start of rendering and story proceeding
					if (eventProgress < eventRooms.Length && i == renderSize - 1) {
						nextRoomProto = eventRooms [eventProgress++];
					}
					GameObject room = SpawnRoomWithLoc (
	  									nextRoomProto,
				                  		dfsStack.Peek ().transform.position + ResolveDirection (d)
					                  );
					// Active "from" room door
					ActivateDoor (dfsStack.Peek (), d);
					dfsStack.Push (room);
					// Active "to" room door
					ActivateDoor (room, ResolveOppositeDirection (d));
					needBackTrack = false;
					break;
				}
			}

			if (needBackTrack) {
				dfsStack.Pop ();
				// keep current i value
				continue;
			}
		}

		// Randomly active doors
		foreach (GameObject room in dfsStack) {
			for (int i = 0; i < GetNoOfDirections (); i++) {
				if (!CheckNextAreaValid(room, ResolveDirection(i))) {
					if (Random.Range (0, 2) == 1) {
						ActivateDoor (room, ResolveDirection (i));
						ActivateDoor (
							GetOccupiedRoom (room, ResolveDirection (i)), 
							ResolveOppositeDirection (ResolveDirection (i))
						);
					}
				}
			}
		}

		yield return null;
	}

	int GetNoOfDirections() {
		return System.Enum.GetNames (typeof(Direction)).Length;
	}

	void ActivateDoor (GameObject room, Direction d) {
		DoorActivation da = room.GetComponent<DoorActivation>();
		if (da != null) {
			switch (d) {
			case Direction.E:
				da.ActivateRight ();
				break;
			case Direction.N:
				da.ActivateForward ();
				break;
			case Direction.S:
				da.ActivateBack ();
				break;
			case Direction.W:
				da.ActivateLeft ();
				break;
			}
		}
	}

	Direction ResolveDirection (int i) {
		return (Direction) System.Enum.Parse (typeof(Direction), System.Enum.GetNames (typeof(Direction)) [i]);
	}

	Direction ResolveOppositeDirection (Direction dir) {
		switch (dir) {
		case Direction.E:
			return Direction.W;
		case Direction.N:
			return Direction.S;
		case Direction.S:
			return Direction.N;
		case Direction.W:
			return Direction.E;
		}
		throw new MissingReferenceException ();
	}

	Vector3 ResolveDirection (Direction dir) {
		switch (dir) {
		case Direction.E:
			return Vector3.right * roomSize;
		case Direction.N:
			return Vector3.forward * roomSize;
		case Direction.S:
			return Vector3.back * roomSize;
		case Direction.W:
			return Vector3.left * roomSize;
		}
		throw new MissingReferenceException ();
	}

	GameObject GetOccupiedRoom (GameObject room, Direction dir) {
		if (!CheckNextAreaValid (room, dir)) {
			return Physics.OverlapSphere (room.transform.position + ResolveDirection (dir), 1f) [0].gameObject;
		}
		return null;
	}

	bool CheckNextAreaValid (GameObject room, Direction dir) {
		// Check if overlapped by other gameobject (occupied)
		if (Physics.OverlapSphere(room.transform.position + ResolveDirection (dir), 1f).Length > 0)
			return false;
		else
			return true;
	}

	GameObject SpawnRoomWithLoc(GameObject _room, Vector3 location) {
		GameObject room = (GameObject)Instantiate (_room);
		room.transform.position = location;
		return room;
	}
	
	// Update is called once per frame
	void Update () {
		if (roomRef.Traveled != 0 && roomRef.Traveled % renderEvery == 0) {
			StartCoroutine ("RenderNext", roomRef.Room);
			// Reset traveled counter
			roomRef.Traveled = 0;
		}
	}
}
