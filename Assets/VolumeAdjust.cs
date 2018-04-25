using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeAdjust : MonoBehaviour {

	public Slider Volume;

	//public AudioSource volumeAudio;
 	public void VolumeController(){
     	AudioListener.volume = Volume.value;
 	}
}
