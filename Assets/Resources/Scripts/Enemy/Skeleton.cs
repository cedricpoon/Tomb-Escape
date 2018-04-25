using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy {

	/* Make actual damage to player in attack animation */
	public bool Attackable;

	public bool IsAttacking, IsDamaging;

	protected override void Start ()
	{
		base.Init (
			GlobalStore.now.Skeleton_Life, 
			GlobalStore.now.Skeleton_Speed,
			GameObject.FindGameObjectWithTag ("Player"),
			GlobalStore.now.Skeleton_Damage
		);
		base.Start ();
	}

	public override void Trace (GameObject target)
	{
		base.Trace (target);
		base._Animator.SetInteger ("IdleWalkInt", 1);
	}

	public override void Flee ()
	{
		base.Flee ();
		base._Animator.SetInteger ("IdleWalkInt", 0);
	}

	WaitForSecondsIEnum attackableWait;

	protected override void Attack ()
	{
		// Make actual damage
		if (Attackable) {
			Attackable = false;
			Target.GetComponent<Health> ().Damage (Power);
		}

		if (!IsAttacking) {
			IsAttacking = true;

			base._Animator.SetTrigger ("Attack");

			attackableWait = new WaitForSecondsIEnum (1f /* Actual animated attack */, delegate(object[] objects) {
				Attackable = true;
			});
			attackableWait.Run (this);
		}
	}

	void OnCollisionExit (Collision collisionInfo) {
		Attackable = false;
		if (attackableWait != null)
			attackableWait.Stop (this);
	}

	public override void Damage ()
	{
		if (!IsDamaging) {
			IsDamaging = true;

			base.Damage ();
			base._Animator.SetTrigger ("Damage");

			// Killed
			if (base.Life <= 0) {
				base._Animator.SetTrigger ("Death");
			}
		}
	}
}
