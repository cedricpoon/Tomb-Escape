using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensJittering : MonoBehaviour {

	[Range(1f, 2f)]
	public float speed = 1f;

	[Range(0f, 5f)]
	public float sprintExtra = 1f;

	[SerializeField]
	string playerTag = "Player";

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		// Set original offset
		offset = transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {

		float z = Input.GetAxis ("Vertical");
		float s = Input.GetAxis ("Sprint");

		Vector2 v2 = DegreeToVector2 (
			GameObject.FindGameObjectWithTag (playerTag).transform.rotation.eulerAngles.y
		);

		transform.rotation = Quaternion.Euler (new Vector3 (
			offset.x + speed * -z * v2.x + s * sprintExtra * -z,
			offset.y + speed * -z * v2.y + s * sprintExtra * -z,
			offset.z
		));
	}

	Vector2 RadianToVector2(float radian)
	{
		return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
	}

	Vector2 DegreeToVector2(float degree)
	{
		return RadianToVector2(degree * Mathf.Deg2Rad);
	}
}
