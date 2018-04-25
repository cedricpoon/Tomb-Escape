using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Attachable {

	protected override void Start ()
	{
		base.Start ();

		HasTrigger = true;
	}

	public override void Trigger ()
	{
		base.Trigger ();

		player.GetComponent<Health> ().Heal ();
		base.Corrupt ("Used");
	}
}
