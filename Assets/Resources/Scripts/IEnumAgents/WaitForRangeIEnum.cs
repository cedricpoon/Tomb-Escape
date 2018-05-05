using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForRangeIEnum
{
	public delegate void Action(float i);

	Action _next, _tick;
	float _from, _to, _unit;

	WaitForSecondsIEnum wait;

	public WaitForRangeIEnum (float from, float to, float unit, Action tickAction, Action nextAction)
	{
		_from = from;
		_to = to;
		_unit = unit;
		_next = nextAction;
		_tick = tickAction;

	}

	public void Stop (MonoBehaviour reference){
		wait.Stop (reference);	
	}

	public void Run(MonoBehaviour reference) {
		wait = new WaitForSecondsIEnum (_unit, delegate(object[] objects) {
			_from += _unit;

			_tick(_from);
			if (_to > _from) {
				wait.Run(reference);
			} else {
				_next(_from);
			}
		});
		wait.Run (reference);
	}
}

