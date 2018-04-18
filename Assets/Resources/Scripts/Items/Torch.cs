using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Attachable {

	private Light _light;

	[Range(5, 30)]
	[Header("Proportional to Light Range")]
	public int LifeTimeInSeconds = 5;

	private bool startedCountDown;

	IEnumerator CountDown () {
		while (LifeTimeInSeconds > 0) {
			if (base.IsHeld)
				_light.range = --LifeTimeInSeconds;
			// wait for one second
			yield return new WaitForSeconds (1f);
		}
		GetComponent<Torchelight> ().IntensityLight = 0;
	}

	public override void Attach ()
	{
		base.Attach ();

		if (!startedCountDown) {
			// first time pickup
			startedCountDown = true;
			_light.range = LifeTimeInSeconds;
			GetComponent<Torchelight> ().IntensityLight = GetComponent<Torchelight> ().MaxLightIntensity;

			StartCoroutine("CountDown");
		}
	}

	public override void Unattach ()
	{
		base.Unattach ();
	}

	public override void OnCollisionStay (Collision collision)
	{
		base.OnCollisionStay (collision);
	}

	public override void OnCollisionExit (Collision collision)
	{
		base.OnCollisionExit (collision);
	}

	public override void Start ()
	{
		base.Start ();
		_light = GetComponentInChildren<Light> ();
	}

	public override void Update ()
	{
		base.Update ();
	}
}
