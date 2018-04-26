using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adapter of MobSpawner
public class ItemSpawner : MobSpawner
{
	public List<GameObject> ListOfItems;

	protected override void Start ()
	{
		noOfMobs = GlobalStore.now.Room_Item_Number;
		ListOfMobs = ListOfItems;

		base.Start ();
	}
}

