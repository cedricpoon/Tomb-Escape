using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : Attachable {

	[SerializeField]
	GameObject bullet;

	[SerializeField]
	string barrelExit = "Barrel End";

	int noOfBullets;

	public override void Trigger ()
	{
		if (noOfBullets > 0) {
			base.Trigger ();

			/* Shoot bullets */
			GameObject _bullet = Instantiate<GameObject> (bullet);
			// From ground
			_bullet.transform.position = new Vector3 (
				transform.Find(barrelExit).position.x,
				0.2f,
				transform.Find(barrelExit).position.z
			);
			_bullet.transform.rotation = this.transform.root.rotation;

			_bullet.GetComponent<Rigidbody> ().AddForce (
				_bullet.transform.forward * GlobalStore.now.Handgun_Bullet_Speed, 
				ForceMode.VelocityChange
			);
				
			_bullet.transform.Find ("Light").position = new Vector3(
				_bullet.transform.position.x,
				this.transform.root.GetComponent<Collider>().bounds.size.y / 2,
				_bullet.transform.position.z
			);

			noOfBullets--;

			if (noOfBullets == 0)
				base.Corrupt ("Out of Ammo");
		}
	}

	protected override void Start ()
	{
		base.Start ();

		noOfBullets = GlobalStore.now.Handgun_Bullet_Amount;
		HeldOffset = new Vector3 (0, 120, 45);
	}
}
