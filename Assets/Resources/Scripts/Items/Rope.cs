using UnityEngine;
using System.Collections;

public class Rope : Attachable
{
	public override void Attach ()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody> ().constraints = 
			RigidbodyConstraints.FreezeAll;

		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().MoveLock = true;

		new MessageBox (
			this,
			"You have successfully escaped!",
			MessageBox.DURATION_LONG,
			GlobalStore.ON_SCREEN_LOWER_Y

		).SetColor(Color.green).SetFadedEventHandler(delegate() {
			
			
		}).Show ();
	}
}

