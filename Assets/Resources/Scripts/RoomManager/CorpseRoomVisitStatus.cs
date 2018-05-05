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
		).ShowInstantly ();
	}
}

