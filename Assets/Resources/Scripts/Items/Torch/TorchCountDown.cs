using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchCountDown : MonoBehaviour {

	[SerializeField]
	private Attachable _attach;

	private Light light;

	[Range(5, 30)]
	[Header("Proportional to Light Range")]
	public int LifeTimeInSeconds = 5;

	private bool startedCountDown;

	IEnumerator CountDown () {
		while (LifeTimeInSeconds > 0) {
			light.range = --LifeTimeInSeconds;
			// wait for one second
			yield return new WaitForSeconds (1f);
		}
		GetComponent<Torchelight> ().IntensityLight = 0;
	}

	void Start () {
		light = GetComponentInChildren<Light> ();
	}

	// Update is called once per frame
	void Update () {
		if (_attach.IsHeld && !startedCountDown) {
			// first time pickup
			startedCountDown = true;
			light.range = LifeTimeInSeconds;
			GetComponent<Torchelight> ().IntensityLight = GetComponent<Torchelight> ().MaxLightIntensity;

			StartCoroutine("CountDown");
		}
	}
}
