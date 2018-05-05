using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : Attachable {

	[SerializeField]
	GameObject bullet;

	[SerializeField]
	string barrelExit = "Barrel End";

	int noOfBullets;

	static bool FirstTime = true;

	public override void Trigger ()
	{
		if (noOfBullets > 0) {
			base.Trigger ();

			/* Shoot bullets */
			GameObject _bullet = Instantiate<GameObject> (bullet);
			// From ground
			_bullet.transform.localPosition = new Vector3 (
				transform.Find(barrelExit).position.x,
				0.5f,
				transform.Find(barrelExit).position.z
			);
			_bullet.transform.rotation = this.transform.root.rotation;

			// Bullet flies
			_bullet.GetComponent<Rigidbody> ().AddForce (
				_bullet.transform.forward * GlobalStore.now.Handgun_Bullet_Speed, 
				ForceMode.VelocityChange
			);
				
			_bullet.transform.Find ("Light").position = new Vector3(
				_bullet.transform.position.x,
				this.transform.root.GetComponent<Collider>().bounds.size.y / 2,
				_bullet.transform.position.z
			);

			// Light effect
			this.transform.root.GetComponentInChildren<Light>().intensity *= 2;
			this.transform.root.GetComponentInChildren<Light> ().range *= 2;

			new WaitForSecondsIEnum (0.2f, delegate(object[] objects) {
				this.transform.root.GetComponentInChildren<Light>().intensity /= 2;
				this.transform.root.GetComponentInChildren<Light> ().range /= 2;
			}).Run(this);

			noOfBullets--;

			if (noOfBullets == 0) {
				base.Corrupt ("Out of Ammo");
			}
		}
	}



	public override void Attach ()
	{
		if (FirstTime) {
			MessageBox.Show (
				GameObject.FindGameObjectWithTag("Player").GetComponent<MonoBehaviour>(), 
				"Left Click to Fire with few Ammo", 
				MessageBox.DURATION_LONG, 
				GlobalStore.ON_SCREEN_LOWER_Y * 0.8f
			);
			FirstTime = false;
		}


		base.Attach ();
	}

	protected override void Start ()
	{
		base.Start ();

		HasTrigger = true;
		noOfBullets = GlobalStore.now.Handgun_Bullet_Amount;
		HeldOffset = new Vector3 (0, 120, 45);
	}

	protected override void Update ()
	{
		if (IsHeld)
			player.GetComponent<Animator> ().SetInteger ("AttackType", 1);
		base.Update ();
	}
}
