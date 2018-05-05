using UnityEngine;
using System.Collections;

public class Backpack : Attachable
{
	public override void Attach ()
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Inventory> ().Enlarge ();

		MessageBox.Show (
			GameObject.FindGameObjectWithTag("Player").GetComponent<MonoBehaviour>(),
			"Your Inventory is now Expanded",
			MessageBox.DURATION_SHORT,
			GlobalStore.ON_SCREEN_NOTICE_UPPER_Y
		);

		Destroy (this.gameObject);
	}
}

