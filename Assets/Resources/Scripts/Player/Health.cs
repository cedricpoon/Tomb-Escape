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
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;

			new MessageBox (
				this,
				"You are Dead!",
				MessageBox.DURATION_LONG,
				GlobalStore.ON_SCREEN_LOWER_Y
			).SetColor(Color.red).SetFadedEventHandler(delegate() {

				Time.timeScale = 0;
			}).Show();
		}
	}

	public void Heal () {
		Life++;
		new MessageBox (
			this, 
			"♥", 
			MessageBox.DURATION_SHORT, 
			GlobalStore.ON_SCREEN_NOTICE_UPPER_Y
		).SetColor (Color.green).Show ();
	}

	public void Damage (int cost) {
		if (!IsInvincible && !IsDamaging) {
			IsDamaging = true;
			GetComponent<PlayerMovement> ().MoveLock = true;

			Life -= cost;

			new MessageBox (
				this, 
				"♡", 
				MessageBox.DURATION_SHORT, 
				GlobalStore.ON_SCREEN_NOTICE_UPPER_Y
			).SetColor (Color.red).Show ();

			if (Life <= 0) {
				Death ();
			} else {
				// Animate
				_Animator.SetTrigger("Damage");

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

