using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressController : MonoBehaviour {

	public Camera mainCamera;

	public Light mainLighting;

	[Range(1f, 10f)]
	public float zoomOutFrame = 3f;

	public CornerController[] winConditions;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		checkWin ();
	}

	void checkWin () {

		bool isWin = true;

		foreach (CornerController wc in winConditions) {

			if (isWin && !wc.hasEnemy)
				isWin = false;
		}

		if (isWin)
			doWin ();
	}

	void doQuit () {
		
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	public void doWin() {

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (GameObject enemy in enemies) {
			
			enemy.GetComponent<Dead> ().suicide ();
		}

		foreach (CornerController wc in winConditions) {

			wc.gameObject.SetActive (false);
		}

		mainLighting.enabled = false;

		Debug.Log ("ProgressController > doWin () Finished");
	}

	public void doLose () {
		
		StartCoroutine ("doLoseEnum");
	}

	IEnumerator doLoseEnum () {
		
		while (mainCamera.fieldOfView > 1f) {

			mainCamera.fieldOfView -= zoomOutFrame;
			if (mainCamera.fieldOfView < 1f)
				mainCamera.fieldOfView = 1f;
			
			yield return new WaitForSeconds(0.1f);
		}
			
		mainCamera.cullingMask = 0;

		Debug.Log ("ProgressController > doLoseEnum () Finished");

		doQuit ();
	}
}
