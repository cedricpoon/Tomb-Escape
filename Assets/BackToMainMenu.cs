using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToMainMenu : MonoBehaviour {


	public void GoBack(){
		Debug.Log("Back to Main Menu");
		Application.LoadLevel("Menu");
		
	}
	
}
