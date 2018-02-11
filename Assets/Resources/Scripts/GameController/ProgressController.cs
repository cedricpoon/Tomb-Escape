using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressController : MonoBehaviour {

	public Camera mainCamera;

	[Range(1f, 10f)]
	public float zoomOutFrame = 3f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void doEndGame () {
		
		StartCoroutine ("DoEndGameEnum");
	}

	IEnumerator DoEndGameEnum () {
		while (mainCamera.fieldOfView > 1f) {

			mainCamera.fieldOfView -= zoomOutFrame;
			if (mainCamera.fieldOfView < 1f)
				mainCamera.fieldOfView = 1f;
			
			yield return new WaitForSeconds(0.1f);
		}
		mainCamera.cullingMask = 0;
	}
}
