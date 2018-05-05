using System;

public class GlobalStore
{
	// Singleton
	// Default Level.Easy
	public static GlobalStore now = new GlobalStore(LevelEnum.Easy);

	public static bool IsFreePlayMode;

	public const float ON_SCREEN_NOTICE_UPPER_Y = 100, ON_SCREEN_LOWER_Y = -100;

	public enum LevelEnum { Easy, Hard }

	public LevelEnum Level;

	// Player 
	public int Player_Life;
	public float Player_Invincible_Time;

	// Level informations
	public int Inventory_Size_NoBag;
	public int Inventory_Size_Bag;
	public int Room_Mob_Number;
	public int Room_Item_Number;
	public int Room_Puzzle_Mobs;

	// Item
	public int Torch_Time;

	public int Handgun_Bullet_Amount;
	public float Handgun_Bullet_Speed;

	public int Axe_Usage_Limit;
	public float Axe_Attack_Range;

	// Enemy
	public int Skeleton_Life;
	public float Skeleton_Speed;
	public int Skeleton_Damage;

	public int Spider_Life;
	public float Spider_Speed;
	public float Spider_Blast_Radius;
	public int Spider_Damage;

	public int Mummy_Life;
	public float Mummy_Speed;
	public float[] Mummy_Scale;
	public int Mummy_Damage;

	public GlobalStore (LevelEnum level)
	{
		GlobalStore.now = this;

		switch (level) {

		// Setup various level
		case(LevelEnum.Easy):

			Player_Life = 3;
			Player_Invincible_Time = 1.5f;

			Inventory_Size_Bag = 6;
			Inventory_Size_NoBag = 3;
			Room_Mob_Number = 1;
			Room_Item_Number = 1;
			Room_Puzzle_Mobs = 8;

			Skeleton_Life = 2;
			Skeleton_Speed = 1;
			Skeleton_Damage = 1;

			Spider_Life = 1;
			Spider_Speed = 2;
			Spider_Blast_Radius = 4;
			Spider_Damage = 2;

			Mummy_Life = 1;
			Mummy_Speed = 1.5f;
			Mummy_Scale = new float[] { 0.75f, 1.5f };
			Mummy_Damage = 1;

			Torch_Time = 40;

			Handgun_Bullet_Amount = 8;
			Handgun_Bullet_Speed = 50f;

			Axe_Usage_Limit = 20;
			Axe_Attack_Range = 0.8f;

			break;

		case (LevelEnum.Hard):

			/* To be set */

			break;
		
		}
	}
}

