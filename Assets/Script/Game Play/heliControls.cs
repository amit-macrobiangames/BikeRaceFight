using UnityEngine;
using System.Collections;

public class heliControls : MonoBehaviour {

	public Transform playerBike ;
	public static bool heliMove ;
	public static bool revive;
	float val,lastPosition;
	bool check;
	void Awake ()
	{
		counter = 0;
		resetOnce=true;
		check = true;
		heliMove = false;
		if (PlayerPrefs.GetInt ("SoundOff") == 0) {
						GetComponent<AudioSource>().enabled = true;
						GetComponent<AudioSource>().Play();
				} else
						GetComponent<AudioSource>().enabled = false;
		Time.timeScale = 1;
		revive = false;
	}

	int counter=0;
	void resetBool()
	{
		if(!revive)
			heliMove = true;
		else
		{
			counter+=1;
			if(counter>2){
				revive=false;
				heliMove = false;
				counter=0;
			}
		//	revive=false;
		//	heliMove = false;
		}
		resetOnce=true;
	}

	bool resetOnce;
	void Update () {
		if (endlessmodeGraphics.gameMode.Equals ("Idle") && handleArrow.start && tiltControl.startTiltAfterCam) {
					
						if (!GetComponent<AudioSource>().isPlaying)
								GetComponent<AudioSource>().Play ();
						//if(RocketDestroy.hit == false)
						//{
						//transform.Translate (0, 0, -30 * Time.deltaTime);
			transform.position = new Vector3 (transform.position.x, transform.position.y, playerBike.transform.position.z + 50);

						//}	
		
					//	if (transform.position.z < playerBike.position.z - 10) {
							//	transform.position += new Vector3 (0, 0, 195);
				if(resetOnce){
				Invoke("resetBool",4f);
					resetOnce=false;
				}
//				if(!revive)
//					heliMove = true;
//				else
//				{
//					revive=false;
//					heliMove = false;
//				}

					//	}


			if (check == true)
				Invoke ("rand", 1);


			
			check = false;
			
			if (RocketDestroy.hit == true) {
				RocketDestroy.hit = false;
			} else {
				if (transform.position.x <= (1.25f) || transform.position.x >= 4.25f) {
					//Invoke("rand",4);
					
					if (transform.position.x <= (1.2f)) {
						transform.position = new Vector3 (1.25f,transform.position.y, transform.position.z);
						
						//enemyCar.transform.position.x = -0.69f;
					} else if (transform.position.x >= 4.25f) {
						transform.position = new Vector3 (4.1f, transform.position.y, transform.position.z);
						//enemyCar.transform.position.x = 5.1f;
					}
				}
				
				transform.Translate (val * Time.deltaTime * 20, 0, 0);
			}
			
			

		} else {
						
						GetComponent<AudioSource>().Pause();
				}

	}

	void rand(){
		//	print (val);
		if(val<0)
			val = Random.Range(0.02f,0.1f);
		else
			val = Random.Range(-0.1f,0f);
		
		
		//	val = Random.Range(-0.1f,0.1f);
		check=true;
	}
}
