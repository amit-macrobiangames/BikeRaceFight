using UnityEngine;
using System.Collections;

public class startAnimation : MonoBehaviour {
	public GameObject MainCamera,healthbar;
	 static int counter=0;
	public bool startCounter=true;

	// Use this for initialization
	void Start () {
		//transform.GetComponent<AudioListener> ().enabled = true;
//		print (transform.name+"  "+transform.root.name);
		
		if (startCounter) {
			counter += 1;
			
			if (counter > 3) {
				counter = 1;		
			}
			
			//print (counter);
			if (counter == 1) {
				transform.localPosition = new Vector3 (0.997f, 0.518f, 0.081f);
				transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
			} else if (counter == 2) {
				transform.localPosition = new Vector3 (-1.43f, 0.56f, 0.79f);
				transform.localEulerAngles = new Vector3 (0f, 49.81442f, 0f);
				
			} else if (counter == 3) {
				transform.localPosition = new Vector3 (0.4299995f, 0.5140793f, 0.7049575f);
				transform.localEulerAngles = new Vector3 (5.000153f, -1.525879e-05f, -1.525879e-05f);
			}
			
			
			//print ("awake : " + counter);
			startCounter=false;
		}

	}

	void Awake()
	{

//		print (transform.name+"  "+transform.root.name);
//
//		if (startCounter) {
//						counter += 1;
//
//						if (counter > 3) {
//								counter = 1;		
//						}
//
//						//print (counter);
//						if (counter == 1) {
//								transform.localPosition = new Vector3 (0.997f, 0.518f, 0.081f);
//								transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
//						} else if (counter == 2) {
//								transform.localPosition = new Vector3 (-1.43f, 0.56f, 0.79f);
//								transform.localEulerAngles = new Vector3 (0f, 49.81442f, 0f);
//
//						} else if (counter == 3) {
//								transform.localPosition = new Vector3 (0.4299995f, 0.5140793f, 0.7049575f);
//								transform.localEulerAngles = new Vector3 (5.000153f, -1.525879e-05f, -1.525879e-05f);
//						}
//
//
//						print ("awake : " + counter);
//			startCounter=false;
//				}
	}

	public void startanimating()
	{

						healthbar.SetActive (false);

//						print ("startANimating : " + counter);

						if (counter == 1)
								GetComponent<Animation>().Play ("cam3start1", PlayMode.StopAll);
						else if (counter == 2)
								GetComponent<Animation>().Play ("cam17start1", PlayMode.StopAll);
						else if (counter == 3)
								GetComponent<Animation>().Play ("cam10start1", PlayMode.StopAll); 

						Invoke ("AnimationCompleted", 5.95f);
		
	}


	void AnimationCompleted()
	{
	
		MainCamera.SetActive (true);
	//	transform.GetComponent<AudioListener> ().enabled = false;
		MainCamera.transform.position = new Vector3 (transform.position.x,transform.position.y,MainCamera.transform.position.z);

		MainCamera.transform.eulerAngles = new Vector3 (20.3678f,0,0);
		healthbar.SetActive (true);
		tiltControl.startTiltAfterCam = true;
		Invoke ("turnThisOff",0f);


	}

	void turnThisOff()
	{
		startCounter=true;
		healthbar.SetActive (true);
		tiltControl.startTiltAfterCam = true;
		gameObject.SetActive (false);

	}
}
