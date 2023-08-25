using UnityEngine;
using System.Collections;

public class oppositeCar : MonoBehaviour {
	Transform player;
	public bool translate;
	Vector3 pos;
	//public bool bikerAhead;
	public bool rotateTire;
	public Transform WheelFL,wheelRL,wheelFR,wheelRR;
	int carWheelType;
	bool bike,low;
	// Use this for initialization
	void Start () {
		rotateTire = false;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		pos = transform.localPosition;
		bike = false;
		if (transform.name.Contains ("bike")) {
			bike=true;		
		}
		if (transform.name.Contains ("Low")) {
			low=true;		
		}
	
//			if (PlayerPrefs.GetInt ("SoundOff") == 0) {
//				
//				audio.Play ();
//				audio.enabled = true;
//				
//			} else {
//				audio.Pause ();
//				audio.enabled = false;
//				
//			}	

	}
	
	// Update is called once per frame

void Update () {
		//	  


		if (transform.position.z - player.position.z <= - 10f) {
//			print ("inside");
			//Destroy (transform.gameObject);
			transform.localPosition = pos;
			rotateTire=false;
			GetComponent<AudioSource>().Pause ();
			GetComponent<AudioSource>().enabled = false;
			
		}

				

		if (!endlessmodeGraphics.gameMode.Equals ("Idle")) {
			
			GetComponent<AudioSource>().Pause ();
			GetComponent<AudioSource>().enabled = false;
			
			
		}
		
		
		if (translate) {

			 if (bike ) {
				

				WheelFL.Rotate(0,0,400*Time.deltaTime);	
				wheelRL.Rotate(0,0,400*Time.deltaTime);	
				wheelRR.Rotate(0,0,400*Time.deltaTime);	
				wheelFR.Rotate(0,0,400*Time.deltaTime);	

			}
			else if (low )
			{

				WheelFL.Rotate(0,0,200*Time.deltaTime);	
				wheelRL.Rotate(0,0,200*Time.deltaTime);	
				wheelRR.Rotate(0,0,200*Time.deltaTime);	
				wheelFR.Rotate(0,0,200*Time.deltaTime);

			}
			else
			{

				WheelFL.Rotate(200*Time.deltaTime,0,0);	
				wheelRL.Rotate(200*Time.deltaTime,0,0);	
				wheelRR.Rotate(200*Time.deltaTime,0,0);	
				wheelFR.Rotate(200*Time.deltaTime,0,0);
			}
		}
	}
	

}
