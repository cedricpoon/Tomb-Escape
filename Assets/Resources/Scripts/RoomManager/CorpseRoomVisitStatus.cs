using UnityEngine;
using System.Collections;

public class CorpseRoomVisitStatus : VisitStatus
{
	public override void Checkout ()
	{
		base.Checkout ();

		new MessageBox (
			this,
			"Seems god wanted you dead too.",
			5,
			GlobalStore.ON_SCREEN_LOWER_Y - 20
		).SetFadedEventHandler(delegate() {
			new MessageBox (
				this,
				"My bag and my items are back!",
				5,
				GlobalStore.ON_SCREEN_LOWER_Y - 20
			).ShowInstantly();
		}).ShowInstantly ();
	}
}

