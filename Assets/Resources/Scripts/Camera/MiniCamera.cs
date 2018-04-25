﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Game/MiniCamera")]
public class MiniCamera : MonoBehaviour {

	private Camera cam;
	// Use this for initialization
	void Start () {
		float ratio = (float)Screen.width / (float)Screen.height;
		this.GetComponent<Camera>().rect = new Rect ((1 - 0.2f), (1 - 0.2f * ratio), 0.2f, 0.2f * ratio);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
