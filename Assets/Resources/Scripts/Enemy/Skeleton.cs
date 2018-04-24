using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy {

	protected override void Start ()
	{
		base.Init (
			GlobalStore.now.LifeOfSkeleton, 
			GlobalStore.now.SpeedOfSkeleton,
			GameObject.FindGameObjectWithTag ("Player")
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

	protected override void Attack ()
	{
		if (!base._Animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			base._Animator.SetTrigger ("Attack");
		}
	}

	public override void Damage ()
	{
		if (!base._Animator.GetCurrentAnimatorStateInfo (0).IsName ("Damage")) {
			base.Damage ();
			base._Animator.SetTrigger ("Damage");

			// Killed
			if (base.Life == 0) {
				base._Animator.SetTrigger ("Death");
			}
		}
	}
}
