using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitVisitStatus : VisitStatus
{
	public override void Checkout ()
	{
		new MessageBox (
			this,
			"An escape rope! Finally find a way out!",
			5,
			GlobalStore.ON_SCREEN_LOWER_Y - 20
		).ShowInstantly ();

		base.Checkout ();
	}
}

