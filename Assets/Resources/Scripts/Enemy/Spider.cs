using UnityEngine;
using System.Collections;

public class Spider : Enemy
{
	[SerializeField]
	float randMoveSeconds = 30;

	WaitForSecondsIEnum moveAround;

	Vector3 randDir;

	int moveAroundTimes;

	bool isAttacking;

	protected override void Start ()
	{
		base.Init (
			GlobalStore.now.Spider_Life, 
			GlobalStore.now.Spider_Speed,
			GameObject.FindGameObjectWithTag ("Player"),
			GlobalStore.now.Spider_Damage
		);
		base.Start ();

		_Animation ["run"].speed = _Animation ["walk"].speed = 2;

		moveAround = new WaitForSecondsIEnum (randMoveSeconds, delegate(object[] objects) {
			if (_Animation.IsPlaying("idle")){
				
				_Animation.Play("walk");

				randDir = (Random.Range (0, 2) == 1) ? Vector3.up : Vector3.down;
				randDir *= Random.Range(0, 360);
				moveAroundTimes = (int)randMoveSeconds * Random.Range(0, 10);
			}

			// Next cycle
			moveAround.SetSeconds(Random.Range(0, randMoveSeconds)).Run(this);
		});

		moveAround.Run (this);
	}

	protected override void Update ()
	{
		base.Update ();

		// Reset animation
		if (moveAroundTimes <= 0 && _Animation.IsPlaying("walk"))
			_Animation.Play ("idle");

		if ((_Animation.IsPlaying("idle") || _Animation.IsPlaying("walk")) && moveAroundTimes > 0) {
			_Animation.Play ("walk");
			Move (randDir);
			moveAroundTimes--;
		}
	}

	public override void Dead ()
	{
		_Animation.Stop ();
		_Animation.Play ("death1");

		new WaitForSecondsIEnum (1.3f /* Length of animation */, delegate(object[] objects) {
			base.Dead ();
		}).Run (this);
	}

	public override void Move (Vector3 rotation)
	{
		base.Move (rotation);
	}

	public override void Trace (GameObject target)
	{
		if (!isAttacking && !_Animation.IsPlaying("death1")) {
			base.Trace (target);
			_Animation.Play ("run");
		}
	}

	public override void Flee ()
	{
		if (!isAttacking && !_Animation.IsPlaying("death1")) {
			base.Flee ();
			_Animation.Play ("idle");
		}
	}

	void Detonate () {
		Collider[] colliders = Physics.OverlapSphere (this.transform.position, GlobalStore.now.Spider_Blast_Radius);

		foreach (Collider col in colliders) {
			if (col.GetComponent<Rigidbody>() == null || col.gameObject == gameObject)
				continue;
			else {
				if (col.gameObject == Target) {
					Target.GetComponent<Health> ().Damage (Power);
				}

				col.GetComponent<Rigidbody>().AddExplosionForce (
					GlobalStore.now.Spider_Blast_Radius * col.attachedRigidbody.mass, 
					this.transform.position, 
					GlobalStore.now.Spider_Blast_Radius,
					0.5f,
					ForceMode.Impulse
				);
			}
		}
	}

	WaitForSecondsIEnum lit;

	protected override void Attack ()
	{
		if (!isAttacking) {
			isAttacking = true;

			_Animation.Play ("taunt");

			GetComponentInChildren<SkinnedMeshRenderer> ().material.shader = Shader.Find ("Particles/Additive (Soft)");

			lit = new WaitForSecondsIEnum (Time.deltaTime, delegate(object[] objects) {
				GetComponentInChildren<Light>().intensity += 0.1f;

				if (GetComponentInChildren<Light>().intensity < 1) {
					lit.Run(this);
				}
			});
			lit.Run(this);

			new WaitForAnimationIEum (_Animation, delegate(object[] objects) {
				// Explode
				Detonate ();
				base.Dead();
			}).Run (this);
		}
	}

	public override void Damage ()
	{
		base.Damage ();

		if (Life <= 0) {
			Dead ();
		}
	}
}

