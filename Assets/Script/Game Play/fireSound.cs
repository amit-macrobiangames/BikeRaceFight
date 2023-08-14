using UnityEngine;
using System.Collections;

public class fireSound : MonoBehaviour {
	public static AudioSource audioSource;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("SoundOff")==0) 
		{
			audioSource = (AudioSource)GetComponent (typeof(AudioSource));
			GetComponent<AudioSource>().playOnAwake=true;
			
			GetComponent<AudioSource>().Play();
			GetComponent<AudioSource>().enabled=true;

			
		}
		else 
		{
			GetComponent<AudioSource>().Pause();
			GetComponent<AudioSource>().enabled=false;

		
			
			
		}
	}
	

}
