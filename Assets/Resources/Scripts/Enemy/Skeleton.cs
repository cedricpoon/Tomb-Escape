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

	protected override void Update ()
	{
		base.Update ();
	}

	protected override void Dead ()
	{
		base.AnimatorRef.SetTrigger ("Death");

		new WaitForAnimationIEum (base.AnimatorRef, delegate(object[] objects) {
			base.Dead();
		}).Run (this);
	}

	public override void Move (Vector3 rotation)
	{
		transform.rotation = Quaternion.Slerp(
			transform.rotation, 
			Quaternion.Euler(rotation), 
			Time.deltaTime * Enemy.RotationDamping
		);

		// Move towards player
		transform.position += 
			transform.forward * 
			base.MoveSpeed * 
			Time.deltaTime;
	}

	public override void Trace (GameObject target)
	{
		base.Trace (target);

		base.AnimatorRef.SetInteger ("IdleWalkInt", 1);

		Move (Quaternion.LookRotation (
			target.transform.position - transform.position
		).eulerAngles);
	}

	public override void Flee ()
	{
		base.Flee ();
		base.AnimatorRef.SetInteger ("IdleWalkInt", 0);
	}

	protected override void Attack ()
	{
		if (!base.AnimatorRef.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			base.AnimatorRef.SetTrigger ("Attack");
		}
	}

	public override void Damage ()
	{
		if (!base.AnimatorRef.GetCurrentAnimatorStateInfo (0).IsName ("Damage")) {
			base.Damage ();
			base.AnimatorRef.SetTrigger ("Damage");
		}
	}
}
