using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForSecondsIEnum
{
	public delegate void NextAction(object[] objects);

	NextAction _nextAction;

	float _seconds;

	public WaitForSecondsIEnum SetSeconds (float seconds) {
		_seconds = seconds;
		return this;
	}

	public WaitForSecondsIEnum (float seconds, NextAction nextAction) : base() {
		_nextAction = nextAction;
		_seconds = seconds;
	}

	public void Run(MonoBehaviour reference, object[] objects = null) {
		reference.StartCoroutine (RunEnum (objects));
	}

	IEnumerator RunEnum(object[] objects) {
		yield return new WaitForSeconds (_seconds);
		_nextAction (objects);
	}
}

