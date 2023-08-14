using UnityEngine;

public class missileScript : MonoBehaviour {

	public cameraShake cameraShakeScript;
	public AudioClip explosionSound;
	public static bool carhit = false;
	public static bool _enabled;
	public Transform bike;
	void Awake ()
	{
		_enabled = false;

	}


	void hide()
	{
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	if (_enabled) {
			CancelInvoke("hide");
			Invoke ("hide",2f);
			_enabled=false;
		}
		
		if (!carhit) {
			transform.position+=new Vector3(0,0,Time.deltaTime * 30);

		}
		
	}

	 void explodeCar( GameObject col)
	{
		if (!col.transform.parent.parent.GetComponent<startMove> ().missileHit) {
					if(weaponAI.noOfCarsHit>0)
						weaponAI.noOfCarsHit-=1;
						bike.GetComponent<AudioSource>().PlayOneShot (explosionSound);
		
//						print (col.transform.parent.name);	
						col.transform.parent.parent.GetComponent<startMove> ().missileHit = true;
						col.transform.parent.parent.GetChild (1).gameObject.SetActive (true);
						col.transform.parent.gameObject.SetActive (false);
						cameraShakeScript.enabled = true;
						cameraShakeScript.shakeAmount = 0.1f;
						cameraShakeScript.shakeDuration = 0.1f;
						CancelInvoke ("hide");
						hide ();
				}
	}
	void OnCollisionEnter(Collision col)
	{ 
//		print (col.transform.name+ "  "+ col.gameObject.layer);		
	if (col.gameObject.layer == 14) {
			explodeCar(col.gameObject);


		}
	}
	void OnTriggerEnter(Collider col)
	{

//				print ("trigger: " + col.transform.name + "  " + col.gameObject.layer);		
				if (col.gameObject.layer == 14) {
						explodeCar (col.gameObject);
				}
		}

	void OnTriggerExit(Collider col)
	{
		
		//print ("trigger: " + col.transform.name + "  " + col.gameObject.layer);		
		if (col.gameObject.layer == 14) {
			explodeCar (col.gameObject);
		}
	}
	void OnCollisionStay(Collision col)
	{ 
		//print ("stay: "+col.transform.name+ "  "+ col.gameObject.layer);		
		if (col.gameObject.layer == 14) {
			explodeCar(col.gameObject);
			
			
		}
	}
	
	void OnCollisionExit(Collision col)
	{ 
		//print ("exit: "+col.transform.name+ "  "+ col.gameObject.layer);		
		if (col.gameObject.layer == 14) {
			explodeCar(col.gameObject);
		}
	}

	
}
