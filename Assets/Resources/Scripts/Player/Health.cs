using UnityEngine;
using System.Collections;

public class Health : Wrappable
{
	[SerializeField]
	int Life;

	[SerializeField]
	bool IsInvincible;

	public bool IsDamaging;

	bool IsDead;

	Animator _Animator;

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();

		Life = GlobalStore.now.Player_Life;
		_Animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void Death () {

		if (!IsDead) {
			
			IsDead = true;
			_Animator.SetTrigger ("Death");
		}
	}

	public void Heal () {
		Life++;
	}

	public void Damage (int cost) {
		if (!IsInvincible) {
			
			IsDamaging = true;

			Life -= cost;

			// Animate
			_Animator.SetTrigger("Damage");

			if (Life <= 0) {
				Death ();
			} else {

				// Align with enemy
				base.AddMaterial (Wrapper);

				new WaitForSecondsIEnum (1f, delegate {
					base.RemoveMaterial(Wrapper);
				}).Run (this);

				// Start invincibility after damaged
				IsInvincible = true;
				new WaitForSecondsIEnum (GlobalStore.now.Player_Invincible_Time, delegate(object[] objects) {
					IsInvincible = false;
				}).Run (this);
			}
		}
	}
}

