using UnityEngine;
using System.Collections;

public class spinChild : MonoBehaviour {
	public Vector3 startingPos;
	public AudioClip tringSound;
	// Use this for initialization
	void Awake () {
		startingPos = transform.localPosition;
		
		if(PlayerPrefs.GetInt("SoundOff")!=0)
		{

			transform.GetComponent<AudioSource>().enabled=false;
			
		}
	
		

	}
	

	public void reset(){
		transform.localPosition = startingPos;
	}
	void OnTriggerEnter(Collider col) {
		//print (col.name);
		if (col.name.Equals ("reset")) {
			transform.localPosition+=new Vector3(33,0,0);
		}
		if (col.name.Equals ("soundTrigger")) {
			GetComponent<AudioSource>().PlayOneShot(tringSound);
		}
	}
}
