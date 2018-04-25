using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForAnimationIEum
{
	public delegate void NextAction(object[] objects);

	NextAction _nextAction;

	Animation _animation;

	public WaitForAnimationIEum SetAnimation (Animation animation) {
		_animation = animation;
		return this;
	}

	public WaitForAnimationIEum (Animation animation, NextAction nextAction) : base() {
		_nextAction = nextAction;
		_animation = animation;
	}

	public void Run(MonoBehaviour reference, object[] objects = null) {
		reference.StartCoroutine (RunEnum (objects));
	}

	IEnumerator RunEnum(object[] objects) {
		do {
			yield return null;
		} while (_animation.isPlaying);
		_nextAction (objects);
	}
}