using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handholding : MonoBehaviour {

	public Attachable attachedItem;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire2") && attachedItem != null) {
			// unattach
			attachedItem.Unattach();
			attachedItem = null;
		}
	}
}
