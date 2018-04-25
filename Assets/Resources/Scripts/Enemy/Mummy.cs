using UnityEngine;
using System.Collections;

public class Mummy : Enemy
{
	float scaleFactor;

	protected override void Start ()
	{
		scaleFactor = Random.Range (
			GlobalStore.now.Mummy_Scale[0], GlobalStore.now.Mummy_Scale[1]
		);
		base.Init (
			GlobalStore.now.Mummy_Life, 
			GlobalStore.now.Mummy_Speed * scaleFactor,
			GameObject.FindGameObjectWithTag ("Player"),
			GlobalStore.now.Mummy_Damage
		);

		// Update scale
		transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

		base.Start ();
	}

	public override void Trace (GameObject target)
	{
		if (!base._Animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack") && 
			!base._Animator.GetCurrentAnimatorStateInfo (0).IsName ("Damage")
		) {
			base.Trace (target);
			base._Animator.SetInteger ("IdleWalkInt", 1);
		}
	}

	public override void Flee ()
	{
		base.Flee ();
		base._Animator.SetInteger ("IdleWalkInt", 0);
	}

	protected override void Attack ()
	{
		base._Animator.SetTrigger ("Attack");

		Target.GetComponent<Health> ().Damage (Power);
	}

	public override void Damage ()
	{
		base.Damage ();
		base._Animator.SetTrigger ("Damage");

		// Killed
		if (base.Life <= 0) {
			base._Animator.SetTrigger ("Death");
		}
	}
}

