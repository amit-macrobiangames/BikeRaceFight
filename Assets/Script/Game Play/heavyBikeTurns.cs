using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class heavyBikeTurns : MonoBehaviour {
	public AudioClip explosionSound;
	public cameraShake camshakeScript;
	public static bool isShielded;
	public static int opponentCrashedCash;
	public static int score;
	public static int coins;
	public static int coinCash;
//	public static int helmetCollected;
	public GameObject explosion;
	public static bool shieldOn,activateShield;
	public GameObject shieldEffect;
	int shieldCounter;
	public Transform frontWheel, rearWheel;
	public PhycamViews mainCamera;
private static int motor_power;
	int currentPower;
	public GameObject parent, heavyBike_shadow,	harleyBike_shadow;
	public float z_position;
	public WheelCollider wheel_F;
	public WheelCollider wheel_r;
	public Transform COM;
	public GameObject player,tallguy,fatguy;
	public static bool startRace;
	public static bool Onramp;


	public static float speed_now;

	bool playerdead;
	public bool flyoverStart,rampControl;
	bool crash;
	public static float hurdlePosition;                //either bike is performing stunt or not
	int layerMask;
	int levelID;
	int index;
	public int counter;
	RaycastHit hit = new RaycastHit ();
	//timeAttackGraphics script;
	// Use this for initialization
	bool rampOnce;
	turnLevelcontrols harleyBikescript;
	heavyBikeTurnControls heavyBikeScript;
	public static bool boostbool;
	int boost_power;
	public static bool isdead;
	bool once;
	 public bool jump;
	bool playAnimOnce;
	//public Rect jumpRect,jumpFGRect;
	//public GUIStyle jumpStyleBg;
	//public Texture jumpStyleFg;
	bool Ramponce;
	public bool isJumping;
	void Start () 
	{
		if (!PlayerPrefs.HasKey ("singelLevelScore"))
			PlayerPrefs.SetInt ("singelLevelScore",0);
		isShielded = false;
		score = 0;
		coins = 0;
		coinCash = 0;
		activateShield = false;
		opponentCrashedCash = 0;
		if (PlayerPrefs.GetInt ("levels") == 7 || PlayerPrefs.GetInt ("levels") == 8 || PlayerPrefs.GetInt ("levels") == 11 || PlayerPrefs.GetInt("levels")==12 || PlayerPrefs.GetInt ("levels") == 15 || PlayerPrefs.GetInt("levels")==16) {
						shieldOn = true;	
						shieldCounter = 3;
						shieldEffect.SetActive (true);
						isShielded = true;
				} else {
						shieldEffect.SetActive (false);
						shieldOn=false;
						shieldCounter=0;
						isShielded = false;
				}

		Onramp = false;
		velocityOnce = true;
		Ramponce = true;
		//moveup = true;
		playAnimOnce = true;
		jump = false;
	
	
		once = true;
		counter = 0;
		isdead = false;
		flyoverStart = false;
		rampControl = false;
		rampOnce = true;
		boostbool = false;
		z_position = parent.transform.position.x;
	
		startRace = false;

		layerMask = (1 << 11)| (1 << 8);
		speed_now=0;
//		print (rigidbody.centerOfMass);
	
		global_var.player_death=false;
		GetComponent<Rigidbody>().isKinematic=false;
	//	rigidbody.centerOfMass += new Vector3 (0.6f,-1,0.5f);
		GetComponent<Rigidbody>().centerOfMass=new Vector3(COM.localPosition.x,COM.localPosition.y,COM.localPosition.z);
		//jumpRect = new Rect (Screen.width-Screen.width * 0.15f, Screen.height * 0.48f, Screen.width * 0.14f, Screen.width * 0.14f );
		//jumpFGRect = new Rect (Screen.width-Screen.width * 0.12f, Screen.height * 0.52f, Screen.width * 0.08f, Screen.width * 0.08f*86/81 );
		wheel_r.motorTorque = 20;
		speed_now=20;
		player.GetComponent<Animation>() ["bikejump"].speed = 0.5f;
		//player.animation ["jumpNew"].speed = 0.5f;


						wheel_r.motorTorque = 20;
						speed_now = 20;
					
				 
		motor_power=150;
		currentPower=motor_power;
		boost_power=165;
	}


	public void revivalStart () 
	{
	camshakeScript.enabled=false;
		velocityOnce=true;
		explosion.SetActive (false);
		Onramp = false;
		velocityOnce = true;
		Ramponce = true;
		//moveup = true;
		playAnimOnce = true;
		jump = false;
	
	
		once = true;
		counter = 0;
		isdead = false;
		flyoverStart = false;
		rampControl = false;
		rampOnce = true;
		boostbool = false;
		z_position = parent.transform.position.x;
		
		startRace = true;
		
		

		
		speed_now=20;
		
		
		tiltControl.startTiltAfterCam = true;
		
		global_var.player_death=false;
		GetComponent<Rigidbody>().isKinematic=false;
		
		
	
		//rigidbody.centerOfMass=new Vector3(COM.localPosition.x,COM.localPosition.y,COM.localPosition.z);
		transform.eulerAngles = new Vector3 (0,0,0);
		
	
	
	

	//rigidbody.velocity=
		GetComponent<Rigidbody>().AddForce (new Vector3 (0, 0, 500));//=-1*gameObject.rigidbody.velocity;

		StartCoroutine ("setVelocity");

		wheel_r.motorTorque = 20;
		speed_now = 20;
	}

	IEnumerator setVelocity()
	{

		GetComponent<Rigidbody>().AddForce (new Vector3 (0, 0, 100));//=-1*gameObject.rigidbody.velocity;
		yield return new WaitForSeconds (0.33f);
		GetComponent<Rigidbody>().AddForce (new Vector3 (0, 0, 100));//=-1*gameObject.rigidbody.velocity;
		yield return new WaitForSeconds (0.33f);
		GetComponent<Rigidbody>().AddForce (new Vector3 (0, 0, 100));//=-1*gameObject.rigidbody.velocity;
		yield return new WaitForSeconds (0.33f);

	}
	void Awake()
	{
		//bikeSelection.bikeCount = 2;

		if (bikeSelection.bikeCount == 2) {
			player = fatguy;
			fatguy.SetActive(true);
			tallguy.SetActive(false);
			levelID=0;
			player.GetComponent<Animation>() ["race"].speed = 0.2f;
			harleyBikescript=player.GetComponent<turnLevelcontrols>();


		}
		if (bikeSelection.bikeCount == 1 ) {
			player = tallguy;
			tallguy.SetActive(true);
			fatguy.SetActive(false);
			levelID=1;

			heavyBikeScript=player.GetComponent<heavyBikeTurnControls>();

		}

			motor_power=37;
			currentPower=motor_power;
			boost_power=44;

		


	
	
	
			
			


	}

	bool velocityOnce=true;
	void FixedUpdate () 
	{



		if (levelID == 0) {
			playerdead=turnLevelcontrols.isdead;
			crash=harleyBikescript.crash;

				} else if (levelID == 1) {
			playerdead=heavyBikeTurnControls.isdead;	
			crash=heavyBikeScript.crash;
		
		}
		if(velocityOnce)
		{
		if (handleArrow.start) {

			wheel_r.motorTorque = 40;
			speed_now=40;
				velocityOnce=false;
			}
		}
//		print (motor_power+ " "+ wheel_r.motorTorque+ " "+ rigidbody.velocity);

		if(!global_var.player_death  && startRace && !playerdead && !crash)
		{
			if(boostbool)
			{
				motor_power=boost_power;

			}
			else
			{
				motor_power=currentPower;
			}

			turnLevelcontrols.startGame=true;
			heavyBikeTurnControls.startGame=true;


	


	
		}
	}

//	WheelHit wheelhit;



	public static bool jumpAnim;
	//bool moveup,movedown;


	public void turnShieldOn()
	{
		shieldOn = true;	
		shieldCounter = 3;
		shieldEffect.SetActive (true);
	}

	void Update () 
	{
		if (activateShield) {
			activateShield=false;
			turnShieldOn();
		}










		if(!global_var.player_death  && startRace && !crash && !playerdead && endlessmodeGraphics.gameMode=="Idle" )
		{



			speed_now = parent.GetComponent<Rigidbody>().velocity.magnitude;
			wheel_r.brakeTorque=0;
			if (wheel_r.motorTorque < motor_power)
				wheel_r.motorTorque += 1;
			else if (wheel_r.motorTorque > motor_power && wheel_r.motorTorque>=0 )
				wheel_r.motorTorque -= 1;
//			



		} 
		else 
		{
			wheel_r.brakeTorque = wheel_r.motorTorque+150;
		}





		if (handleArrow.start && !isdead && !crash) {

			if (Physics.Raycast (transform.position + new Vector3 (0, 0.5f, 0.75f), Vector3.forward, out hit, 1f, layerMask)) {
				
			
				if (hit.collider.tag.Contains ("Gangster")|| hit.collider.gameObject.tag.Contains ("gangsterAttack")) {
					//print ("inside");
					hit.collider.gameObject.GetComponent<CapsuleCollider>().radius=0.4f;
					//hit.collider.enabled = false;
					if (levelID == 1)
						heavyBikeScript.carCrash ();
					else
						harleyBikescript.carCrash ();
					
					
					
				}
				
				
				
			}

		
						//Debug.DrawRay (transform.position+ new Vector3(0,0.07f,0.5f), Vector3.forward*0.3f, Color.green);
						if (Physics.Raycast (transform.position + new Vector3 (0, 0.07f, 0.5f), Vector3.forward, out hit, 0.3f, layerMask)) {
//									print ((transform.position.z-hit.transform.position.z));
								//Debug.DrawRay (transform.position+ new Vector3(0,0.5f,1.5f), Vector3.forward, Color.red);
								if (hit.collider.transform.root.name.Contains ("Ramp") && (transform.position.z - hit.transform.position.z <= 0.2f)) {
				
										if (rampOnce) {
															//print ("ramp ahead");
												RampStarted ();
												rampOnce = false;
										}
								}
						}
				}
		if (flyoverStart) {
		
//			print("ramponce: "+ once);
			if(once )
			{
//				print ("flyoverStart");
				Invoke ("boolFalse",1.25f); 	
				once=false;
			}
		}
		
		
	}





	void OnTriggerEnter(Collider col) {
		//print (col.transform.tag+ "  "+ col.transform.root.name+ "  "+ col.transform.name);
				if (handleArrow.start && !isdead && !crash) {

						if (col.transform.tag.Equals ("rampStart")) {
		
								if (rampOnce) {
										//	print ("on ramp trigger" );
										RampStarted ();
										rampOnce = false;
								}
						}
			
								if (col.transform.tag.Equals ("barrel") ) {
										hurdlePosition = col.gameObject.transform.position.z;
					if(col.transform.tag.Equals ("barrel") )
					{
						//col.collider.enabled=false;
					
										Time.timeScale = 0.2f;
										Time.fixedDeltaTime = 0.004f;
										
										Invoke ("adjustCam", 0.25f);
					}


										if (col.gameObject.name.Equals ("rightCollider")) {
					
												z_position -= 1f;
										} else if (col.gameObject.name.Equals ("leftCollider"))
												z_position += 1f;
				
										if (levelID == 1)
												heavyBikeScript.colliding ();
										else
												harleyBikescript.colliding ();
//					print (col.transform.tag);
								}
						
			if (col.transform.tag.Equals ("car") || col.transform.tag.Contains ("chowkCar") || col.transform.tag.Contains ("hurdle") ||col.transform.tag.Equals ("Gangster") || col.gameObject.tag.Contains ("gangsterAttack")|| col.transform.tag.Contains ("hurdles")|| col.transform.tag.Equals ("newLevelHurdle") ) {
								hurdlePosition = col.transform.position.z;
								if (levelID == 1)
										heavyBikeScript.carCrash ();
								else
										harleyBikescript.carCrash ();
//				print (col.collider.gameObject.layer);
				if(col.GetComponent<Collider>().gameObject.layer==14)
				{
					col.GetComponent<Collider>().transform.parent.Translate(Vector3.back*15f*Time.deltaTime);
				}
			
						}
				}
		}


	void	adjustCam()
	{
		
		
		Time.timeScale=1f;
		Time.fixedDeltaTime=0.02f;
	}
	public void RampStarted()
	{
		//print ("rampstart:  "+ Ramponce);
		if (Ramponce) {
						flyoverStart = true;
			transform.GetComponent<tiltControl>().flyoverStart=true;
			rampJumpStart();
			rampControl=true;
						motor_power = currentPower;
						wheel_r.motorTorque = currentPower;
						print (GetComponent<Rigidbody>().velocity.magnitude);
			if(GetComponent<Rigidbody>().velocity.magnitude<10)
						GetComponent<Rigidbody>().AddForce (new Vector3 (0, 0, 500), ForceMode.Force);//20//=-1*gameObject.rigidbody.velocity;
//			else if(rigidbody.velocity.magnitude>15)
//				rigidbody.AddForce (new Vector3 (0, 0, -5), ForceMode.Force);
//						
			//rigidbody.velocity = new Vector3 (0, 0, currentPower);


	//		mainCamera.rampAnimation();
			Time.timeScale=0.3f;
						index = Random.Range (0, 9);

						if (index%3 == 0)
							PhycamViews.counter=17;
							if (index%3== 1)
							PhycamViews.counter=18;
							if (index%3 == 2)
							PhycamViews.counter=25;
	
			mainCamera.changeCamView();
						heavyBike_shadow.SetActive (false);
						harleyBike_shadow.SetActive (false);
			CancelInvoke("boolFalse");
						Invoke ("boolFalse", 1.15f); //	Invoke ("boolFalse",3.5f);
					
			Ramponce=false;
		
			//Invoke("rampJumpStart",0.1f);

		
			
				}
	}


	void rampJumpStart()
	{
		player.GetComponent<Animation>() ["bikejump"].speed = 2.5f;
		player.GetComponent<Animation>().Play ("bikejump", PlayMode.StopAll);
		Invoke ("rampJump", 0.35f);
		jumpAnim=true;
	}
	void rampJump()
	{
	
		jumpAnim=false;
		player.GetComponent<Animation>() ["bikejump"].speed = 0.5f;
		player.GetComponent<Animation>().Play ("race", PlayMode.StopAll);
	}
	void boolFalse(){
		if(boostbool)
		{
			motor_power=boost_power;
			
		}
		if (flyoverStart) {
			rampOnce=true;
			flyoverStart = false;
			PhycamViews.counter=2;
			mainCamera.changeCamView();		
			Time.timeScale=1f;
			transform.GetComponent<tiltControl>().flyoverStart=false;
		//	mainCamera.rampAnimationEnd();
		}
		//print ("boolfalse");
		once = true;
		transform.GetComponent<tiltControl>().flyoverStart=false;
		flyoverStart = false;
		rampControl=false;
		Onramp = true;

		heavyBike_shadow.SetActive(true);
		harleyBike_shadow.SetActive(true);

		Ramponce=true;
	}



	

	void OnCollisionEnter(Collision col)
	{ 
		//print (col.gameObject.name+ "  "+ col.gameObject.tag);
				if (handleArrow.start && !isdead && !crash) {


			if (col.transform.tag.Equals ("Mine")) {

				col.collider.enabled=false;
				if(!shieldOn)
				{
					isShielded=false;
					shieldOn=false;
					shieldCounter=0;
					if(PlayerPrefs.GetInt("levels")==11 || PlayerPrefs.GetInt("levels")==12)
					{	if(PlayerPrefs.GetInt ("SoundOff")==0)
						{
							GetComponent<AudioSource>().PlayOneShot(explosionSound);
						}
						camshakeScript.enabled=true;
						camshakeScript.shakeAmount = 0.2f;
						camshakeScript.shakeDuration = 0.1f;
						explosion.SetActive(true);
						col.transform.parent.GetChild(0).gameObject.SetActive(true);
						col.transform.parent.GetChild(1).gameObject.SetActive(false);
					}
					
					hurdlePosition = col.transform.position.z;
					if (levelID == 1)
						heavyBikeScript.carCrash ();
					else
						harleyBikescript.carCrash ();
					
					
					if(col.collider.gameObject.layer==14)
					{
						col.collider.transform.parent.Translate(Vector3.back*15f*Time.deltaTime);
					}
					
				}
				else if(shieldOn)
				{
					isShielded=true;
					shieldOn=false;
					shieldCounter=0;
					shieldEffect.SetActive(false);
				}




			}
			if (col.transform.tag.Equals ("smallMine")) {
				
				col.collider.enabled=false;
				if(PlayerPrefs.GetInt("levels")==11 || PlayerPrefs.GetInt("levels")==12)
				{	if(PlayerPrefs.GetInt ("SoundOff")==0)
					{
						GetComponent<AudioSource>().PlayOneShot(explosionSound);
					}
					camshakeScript.enabled=true;
					camshakeScript.shakeAmount = 0.2f;
					camshakeScript.shakeDuration = 0.1f;
				//	explosion.SetActive(true);
					col.transform.parent.GetChild(0).gameObject.SetActive(true);
					col.transform.parent.GetChild(1).gameObject.SetActive(false);
				}


				if(shieldOn)
				{
					isShielded=true;
					if(shieldCounter>0)
						shieldCounter-=1;
					if(shieldCounter==0)
					{
						shieldOn=false;
						shieldEffect.SetActive(false);
					}
				}
				else
				{
					isShielded=false;
					hurdlePosition = col.transform.position.z;
					if (levelID == 1)
						heavyBikeScript.colliding ();
					else
						harleyBikescript.colliding ();
					
					
					if(col.collider.gameObject.layer==14)
					{
						col.collider.transform.parent.Translate(Vector3.back*15f*Time.deltaTime);
					}
					
				}
				
				
				
			}
								//print (col.transform.tag);
								if (col.transform.tag.Equals ("barrel") ) {
					if(shieldOn)
					{
						isShielded=true;
						if(shieldCounter>0)
							shieldCounter-=1;
						if(shieldCounter==0)
						{
							shieldOn=false;
							shieldEffect.SetActive(false);
						}
					}
					else
					{
						isShielded=false;
										hurdlePosition = col.transform.position.z;
					if(col.collider.name.Equals("barrier hurdle") )
					{
						col.collider.enabled=false;
					}
										if (col.gameObject.name.Equals ("rightCollider")) {

												z_position -= 1f;
										} else if (col.gameObject.name.Equals ("leftCollider"))
												z_position += 1f;

										if (levelID == 1)
												heavyBikeScript.colliding ();
										else
												harleyBikescript.colliding ();
								}
						}
			
			if (col.transform.tag.Equals ("car") || col.transform.tag.Equals ("newLevelHurdle")|| col.transform.tag.Equals ("Gangster") || col.gameObject.tag.Contains ("gangsterAttack")) {
//				print ("oncollision "+ col.collider.gameObject.layer);

				if(shieldOn)
				{
					isShielded=true;
					if(shieldCounter>0)
						shieldCounter-=1;
					if(shieldCounter==0)
					{
						shieldOn=false;
						shieldEffect.SetActive(false);
					}
				}
				else
				{
					isShielded=false;
								hurdlePosition = col.transform.position.z;
								if (levelID == 1)
										heavyBikeScript.carCrash ();
								else
										harleyBikescript.carCrash ();

			
				if(col.collider.gameObject.layer==14)
				{
					col.collider.transform.parent.Translate(Vector3.back*15f*Time.deltaTime);
				}
				
						}

				}


			if (col.transform.tag.Equals ("rocket")) {

				if(!shieldOn)
				{
					isShielded=false;
					shieldOn=false;
					shieldCounter=0;
					if(PlayerPrefs.GetInt("levels")==7)
					{	if(PlayerPrefs.GetInt ("SoundOff")==0)
						{
							GetComponent<AudioSource>().PlayOneShot(explosionSound);
						}
					
						explosion.SetActive(true);
					
					}
			
					hurdlePosition = col.transform.position.z;
					if (levelID == 1)
						heavyBikeScript.carCrash ();
					else
						harleyBikescript.carCrash ();
					
					
					if(col.collider.gameObject.layer==14)
					{
						col.collider.transform.parent.Translate(Vector3.back*15f*Time.deltaTime);
					}
					
				}
				else if(shieldOn)
				{
					isShielded=true;
					shieldOn=false;
					shieldCounter=0;
					shieldEffect.SetActive(false);
				}
			}
		}
//
//		if (col.transform.name.Contains ("barrier hurdle")) {
//						
//							hit.collider.enabled = false;
//						
//						}
	
		}
	


	
	public void isShieldedFtn()
	{
				if (shieldOn) {
			isShielded=true;
						if (shieldCounter > 0)
			
								shieldCounter -= 1;
			
						if (shieldCounter == 0) {
								shieldOn = false;
								shieldEffect.SetActive (false);
						}
				} else {
			isShielded=false;
				}
		}

}
