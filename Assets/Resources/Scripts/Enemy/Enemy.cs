using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

	protected Animator AnimatorRef { get; private set; }

	protected float MoveSpeed;

	protected static int RotationDamping = 20;

	protected GameObject Target { get; private set; }

	public int Life { get; private set; }

	public virtual void Damage () {
		this.Life--;
	}

	protected void Init (int life, float moveSpeed, GameObject target) {
		this.Life = life;
		this.MoveSpeed = moveSpeed;
		this.Target = target;
	}

	// Use this for initialization
	protected virtual void Start () {
		AnimatorRef = gameObject.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	protected virtual void Update () { } 

	protected virtual void Dead () {
		Destroy (gameObject);
	}

	public abstract void Move (Vector3 rotation);

	public virtual void Trace (GameObject target) { /* To be overriden if needed */ }

	public virtual void Flee () { /* To be overriden if needed */ }

	protected abstract void Attack ();

	void OnCollisionStay (Collision collisionInfo) {
		if (collisionInfo.gameObject == Target) {
			Attack ();
		}
	}
}
