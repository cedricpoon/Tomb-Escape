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
			10,
			GlobalStore.ON_SCREEN_LOWER_Y
		).Show ();
	}
}

