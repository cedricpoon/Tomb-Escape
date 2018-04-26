using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRemoval : DoorActivation {

	public override void ActivateForward ()
	{
		base.ActivateForward ();
		if (transform.Find ("WallFront") != null)
			transform.Find ("WallFront").gameObject.SetActive (false);
	}

	public override void ActivateBack ()
	{
		base.ActivateBack ();
		if (transform.Find ("WallBack") != null)
			transform.Find ("WallBack").gameObject.SetActive (false);
	}

	public override void ActivateLeft ()
	{
		base.ActivateLeft ();
		if (transform.Find ("WallLeft") != null)
			transform.Find ("WallLeft").gameObject.SetActive (false);
	}

	public override void ActivateRight ()
	{
		base.ActivateRight ();
		if (transform.Find ("WallRight") != null)
			transform.Find ("WallRight").gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
