using System;

public class GlobalStore
{
	// Singleton
	// Default Level.Easy
	public static GlobalStore now = new GlobalStore(LevelEnum.Easy);

	public enum LevelEnum { Easy, Hard }

	public LevelEnum Level;

	// Level informations
	public int SizeofInventoryWithoutBag;
	public int SizeofInventoryWithBag;
	public bool IsFreePlayMode;
	public int TorchLifeTime;

	public GlobalStore (LevelEnum level)
	{
		GlobalStore.now = this;

		switch (level) {

		// Setup various level
		case(LevelEnum.Easy):

			SizeofInventoryWithBag = 6;
			SizeofInventoryWithoutBag = 3;
			TorchLifeTime = 40;

			break;
		case (LevelEnum.Hard):

			/* To be set */

			break;
		
		}
	}
}

