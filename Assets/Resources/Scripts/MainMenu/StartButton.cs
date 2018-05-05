using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	[SerializeField]
	Image _overlay;

	bool skippable;

	MessageBox skipMsg;

	// Use this for initialization
	void Start () {
		
	}

	void OnMouseDown () {

		GetComponent<BoxCollider2D> ().enabled = false;

		new WaitForRangeIEnum (0f, 1f, 0.05f, delegate(float i) {
			_overlay.color = new Color (
				_overlay.color.r,
				_overlay.color.g,
				_overlay.color.b,
				i
			);
		}, delegate(float i) {

			skipMsg = new MessageBox(
				this,
				"Press Any Key to Skip",
				MessageBox.DURATION_FOREVER,
				GlobalStore.ON_SCREEN_LOWER_Y * 0.8f
			).SetColor(Color.gray);

			skipMsg.ShowInstantly();
			skippable = true;

			new MessageBox(
				this,
				"You are Georgy Feodosiy Markov, an archaeologist",
				2,
				GlobalStore.ON_SCREEN_LOWER_Y
			).SetFadedEventHandler(delegate() {

				new MessageBox(
					this,
					"You are currently exploring an ancient tomb with Akim",
					2,
					GlobalStore.ON_SCREEN_LOWER_Y
				).SetFadedEventHandler(delegate() {

					new MessageBox(
						this,
						"An expert in adventure exploration",
						2,
						GlobalStore.ON_SCREEN_LOWER_Y
					).SetFadedEventHandler(delegate() {

						new MessageBox(
							this,
							"However ...",
							2,
							GlobalStore.ON_SCREEN_LOWER_Y
						).SetFadedEventHandler(delegate() {

							new MessageBox(
								this,
								"You are stunned by Akim in the tomb",
								2,
								GlobalStore.ON_SCREEN_LOWER_Y
							).SetFadedEventHandler(delegate() {

								new MessageBox(
									this,
									"You fainted away ...",
									2,
									GlobalStore.ON_SCREEN_LOWER_Y
								).SetFadedEventHandler(delegate() {

									SceneManager.LoadScene("Gameplay");

								}).Show();

							}).Show();

						}).Show();

					}).Show();

				}).Show();

			}).Show();

		}).Run (this);
	}
	
	// Update is called once per frame
	void Update () {
		if (skippable) {
			if (Input.anyKeyDown) {
				skipMsg.DisposeNow ();
				SceneManager.LoadSceneAsync ("Gameplay");
			}
		}
	}
}
