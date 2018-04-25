﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Axe : Attachable {

	int limitation;

	protected override void Start ()
	{
		base.Start ();

		HasTrigger = true;
		limitation = GlobalStore.now.Axe_Usage_Limit;
		base.HeldOffset = new Vector3 (-60, -120, 50);
	}

	protected override void Update ()
	{
		player.GetComponent<Animator> ().SetInteger ("AttackType", 2);
		base.Update ();
	}

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
			//Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
		Gizmos.DrawWireSphere (transform.position, GlobalStore.now.Axe_Attack_Range);
	}

	public override void Trigger ()
	{
		if (limitation > 0) {
			base.Trigger ();

			// Attack
			Collider[] cols = Physics.OverlapSphere (transform.position, GlobalStore.now.Axe_Attack_Range);
			foreach (Collider c in cols) {
				if (c.GetComponentInChildren<Enemy> () != null) {
					c.GetComponentInChildren<Enemy> ().Damage ();
				}
			}

			limitation--;

			if (limitation == 0) {
				base.Corrupt ();
				MessageBox.Show (this, "Axe Damaged!", MessageBox.DURATION_SHORT, GlobalStore.ON_SCREEN_NOTICE_UPPER_Y);
			}
		}
	}
}
