using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitStatus : MonoBehaviour {

	[SerializeField]
	private bool visited;
	public bool IsVisited { get { return visited; } }

	public virtual void Checkout () {
		visited = true;
	}
}
