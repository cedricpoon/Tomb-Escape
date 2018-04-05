using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitStatus : MonoBehaviour {

	[SerializeField]
	private bool visited;
	public bool IsVisited { get { return visited; } }

	public void Checkout () {
		visited = true;
		OnVisit ();
	}

	public virtual void OnVisit () {
		/* virtually abstracted to be overridden */
	}
}
