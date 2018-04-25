using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Attachable {

	public AudioClip impact;
	AudioSource pickup;
	private Light _light;

	int LifeTimeInSeconds; /* GlobalStored */

	private bool startedCountDown;

	IEnumerator CountDown () {
		while (LifeTimeInSeconds > 0) {
			if (base.IsHeld)
				_light.range = --LifeTimeInSeconds;
			// wait for one second
			yield return new WaitForSeconds (1f);
		}
		GetComponent<Torchelight> ().IntensityLight = 0;
		base.Corrupt ();
	}

	public override void Attach ()
	{
		pickup.PlayOneShot (impact, 0.7f);
		base.Attach ();

		if (!startedCountDown) {
			// first time pickup
			startedCountDown = true;

			StartCoroutine("CountDown");
		}
	}

	public override void Unattach ()
	{
		base.Unattach ();
	}

	protected override void Start ()
	{
		base.Start ();

		HeldOffset = Vector3.right * 90;

		LifeTimeInSeconds = GlobalStore.now.Torch_Time;
		_light = GetComponentInChildren<Light> ();

		_light.range = LifeTimeInSeconds;
		GetComponent<Torchelight> ().IntensityLight = GetComponent<Torchelight> ().MaxLightIntensity;
		pickup = GetComponent<AudioSource> ();
	}

	public override void Resume ()
	{
		base.Resume ();
		if (LifeTimeInSeconds > 0)
			StartCoroutine("CountDown");
	}
}
