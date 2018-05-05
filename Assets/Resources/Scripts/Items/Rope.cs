using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Rope : Attachable
{
	public override void Attach ()
	{
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy")) {
			obj.GetComponent<Enemy> ().Dead ();
		}

		GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody> ().constraints = 
			RigidbodyConstraints.FreezeAll;

		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().MoveLock = true;

		new MessageBox (
			GameObject.FindGameObjectWithTag("Player").GetComponent<MonoBehaviour>(),
			"You have successfully escaped!",
			MessageBox.DURATION_LONG,
			GlobalStore.ON_SCREEN_LOWER_Y

		).SetColor(Color.green).SetFadedEventHandler(delegate() {

			SceneManager.LoadScene("SimplyMenu");
		}).Show ();
	}
}

