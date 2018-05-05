using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Wrappable {

	static bool FirstTime = true;

	protected Animator _Animator { get; private set; }

	protected Animation _Animation { get; private set; }

	protected float MoveSpeed;

	protected static int RotationDamping = 20;

	protected GameObject Target { get; private set; }

	public int Life { get; private set; }

	public int Power { get; private set; }

	protected void Init (int life, float moveSpeed, GameObject target, int damage) {
		this.Life = life;
		this.MoveSpeed = moveSpeed;
		this.Target = target;
		this.Power = damage;

		// Align with vision
		if (GetComponentInChildren<Vision> () != null) {
			GetComponentInChildren<Vision> ().Target = target;
		}
	}

	// Use this for initialization
	protected override void Start () {

		base.Start ();

		_Animator = gameObject.GetComponentInChildren<Animator>();
		_Animation = gameObject.GetComponentInChildren<Animation>();
	}
	
	// Update is called once per frame
	protected virtual void Update () { } 

	public virtual void Dead () {
		Destroy (gameObject);
	}

	public virtual void Move (Vector3 rotation) {
		transform.rotation = Quaternion.Slerp(
			transform.rotation, 
			Quaternion.Euler(rotation), 
			Time.deltaTime * Enemy.RotationDamping
		);

		// Move towards player
		transform.position += 
			transform.forward * 
			MoveSpeed * 
			Time.deltaTime;
	}

	public virtual void Trace (GameObject target) {
		if (FirstTime) {
			new MessageBox (
				GameObject.FindGameObjectWithTag("Player").GetComponent<MonoBehaviour>(),
				"WTF is that?",
				5,
				GlobalStore.ON_SCREEN_LOWER_Y
			).Show ();
			FirstTime = false;
		}

		Move (Quaternion.LookRotation (
			target.transform.position - transform.position
		).eulerAngles);
	}

	public virtual void Flee () { /* To be overriden if needed */ }

	protected abstract void Attack ();

	public virtual void Damage () {
		this.Life--;

		base.AddMaterial (Wrapper);

		new WaitForSecondsIEnum (1f, delegate {
			base.RemoveMaterial(Wrapper);
		}).Run (this);
	}

	void OnCollisionStay (Collision collisionInfo) {
		if (collisionInfo.gameObject == Target) {
			Attack ();
		}
	}
}
