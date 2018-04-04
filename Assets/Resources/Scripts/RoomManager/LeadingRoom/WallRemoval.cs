using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRemoval : DoorActivation {

	public override void ActivateForward ()
	{
		base.ActivateForward ();
		transform.Find ("WallForward").gameObject.SetActive (false);
	}

	public override void ActivateBack ()
	{
		base.ActivateBack ();
		transform.Find ("WallBack").gameObject.SetActive (false);
	}

	public override void ActivateLeft ()
	{
		base.ActivateLeft ();
		transform.Find ("WallLeft").gameObject.SetActive (false);
	}

	public override void ActivateRight ()
	{
		base.ActivateRight ();
		transform.Find ("WallRight").gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
