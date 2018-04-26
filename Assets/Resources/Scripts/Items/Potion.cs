using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Attachable {

	static bool FirstTime = true;

	protected override void Start ()
	{
		base.Start ();

		HasTrigger = true;
	}

	public override void Attach ()
	{
		if (FirstTime) {
			MessageBox.Show (
				this, 
				"Left Click to Heal", 
				MessageBox.DURATION_SHORT, 
				GlobalStore.ON_SCREEN_LOWER_Y * 0.8f
			);
			FirstTime = false;
		}


		base.Attach ();
	}

	public override void Trigger ()
	{
		base.Trigger ();

		player.GetComponent<Health> ().Heal ();
		base.Corrupt ("Empty");
	}

	protected override void Update ()
	{
		if (IsHeld)
			player.GetComponent<Animator> ().SetInteger ("AttackType", 2);

		base.Update ();
	}
}
