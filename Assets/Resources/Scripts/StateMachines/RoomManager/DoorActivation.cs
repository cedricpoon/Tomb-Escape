using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivation : MonoBehaviour {

	public bool Forward, Back, Left, Right;

	public virtual void ActivateForward () {
		Forward = true;
	}

	public virtual void ActivateBack () {
		Back = true;
	}

	public virtual void ActivateLeft () {
		Left = true;
	}

	public virtual void ActivateRight () {
		Right = true;
	}
}
