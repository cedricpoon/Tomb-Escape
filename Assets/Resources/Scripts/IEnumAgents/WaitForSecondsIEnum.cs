using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForSecondsIEnum
{
	public delegate void NextAction(object[] objects);

	NextAction _nextAction;

	Coroutine running;

	float _seconds;

	public WaitForSecondsIEnum SetSeconds (float seconds) {
		_seconds = seconds;
		return this;
	}

	public WaitForSecondsIEnum (float seconds, NextAction nextAction) : base() {
		_nextAction = nextAction;
		_seconds = seconds;
	}

	public void Stop (MonoBehaviour reference){
		reference.StopCoroutine (running);	
	}

	public void Run(MonoBehaviour reference, object[] objects = null) {
		running = reference.StartCoroutine (RunEnum (objects));
	}

	IEnumerator RunEnum(object[] objects) {
		yield return new WaitForSeconds (_seconds);
		_nextAction (objects);
	}
}

