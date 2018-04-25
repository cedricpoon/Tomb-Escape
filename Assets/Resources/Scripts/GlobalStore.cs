using System;

public class GlobalStore
{
	// Singleton
	// Default Level.Easy
	public static GlobalStore now = new GlobalStore(LevelEnum.Easy);

	public enum LevelEnum { Easy, Hard }

	public LevelEnum Level;

	// Level informations
	public int Inventory_Size_NoBag;
	public int Inventory_Size_Bag;
	public bool IsFreePlayMode;

	// Item
	public int Torch_Time;
	public int Handgun_Bullet_Amount;
	public float Handgun_Bullet_Speed;

	// Enemy
	public int Skeleton_Life;
	public float Skeleton_Speed;

	public int Spider_Life;
	public float Spider_Speed;
	public float Spider_Blast_Radius;

	public int Mummy_Life;
	public float Mummy_Speed;
	public float[] Mummy_Scale;

	public GlobalStore (LevelEnum level)
	{
		GlobalStore.now = this;

		switch (level) {

		// Setup various level
		case(LevelEnum.Easy):

			Inventory_Size_Bag = 6;
			Inventory_Size_NoBag = 3;

			Skeleton_Life = 2;
			Skeleton_Speed = 1;

			Spider_Life = 1;
			Spider_Speed = 2;
			Spider_Blast_Radius = 4;

			Mummy_Life = 1;
			Mummy_Speed = 1.5f;
			Mummy_Scale = new float[] { 0.75f, 1.5f };

			Torch_Time = 40;
			Handgun_Bullet_Amount = 10;
			Handgun_Bullet_Speed = 100f;

			break;

		case (LevelEnum.Hard):

			/* To be set */

			break;
		
		}
	}
}

