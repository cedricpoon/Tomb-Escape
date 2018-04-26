using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitVisitStatus : VisitStatus
{
	public override void Checkout ()
	{
		Time.timeScale = 0;
		new MessageBox (
			this,
			"Thanks God! Finally a way out!",
			10,
			GlobalStore.ON_SCREEN_LOWER_Y
		).SetFadedEventHandler(delegate() {

			/* Win */
			SceneManager.LoadScene ("SimplyMenu");

		}).Show ();

		base.Checkout ();
	}
}

