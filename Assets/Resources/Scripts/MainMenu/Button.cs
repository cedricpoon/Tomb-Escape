using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Button : MonoBehaviour
{

	void OnMouseEnter () {
		GetComponent<Image> ().color = new Color (
			GetComponent<Image> ().color.r,
			GetComponent<Image> ().color.g,
			GetComponent<Image> ().color.b,
			0.5f
		);
	}

	void OnMouseExit () {
		GetComponent<Image> ().color = new Color (
			GetComponent<Image> ().color.r,
			GetComponent<Image> ().color.g,
			GetComponent<Image> ().color.b,
			1f
		);
	}
}

