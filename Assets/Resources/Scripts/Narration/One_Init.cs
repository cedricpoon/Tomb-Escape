using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One_Init : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		new MessageBox (
			this,
			"That bastard! Betrayed me and took all my stuff. Hope you die in the freaking trap!",
			10,
			GlobalStore.ON_SCREEN_LOWER_Y
		).SetFadedEventHandler(delegate {
			
			new MessageBox (
				this,
				"Calm down, Georgy. I should find him first and take my gears back.",
				10,
				GlobalStore.ON_SCREEN_LOWER_Y
			).Show ();

		}).Show ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
