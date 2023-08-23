using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class heavyBikeTurnControls : MonoBehaviour {

	public PhycamViews mainCamera;
	//	public Transform oppoheavybike,oppoharleybike;
	Transform ragdoll,ragdoll_rigid;
	public Transform timefinish,ragdollprefab;
	public Transform nitroLeft,nitroRight;
	
	public Texture purchase,notEnoughCash,buyIt;
	public Transform shadow;
	public static AudioSource audioSource;
	public static bool playAnimationOnce,lifeEnd;
	public static bool hitOnce;
	
	public Transform scorePopup;
	
	public Transform frontWheel, rearWheel;
	public static int cash;
	
	public static int score;
	public Transform player;
	public static float speed,triggerDistance;
	public static bool scoreOnce ;
	public static float distance;
	
	
	public bool lifeBar;
	
	public GameObject raceSound,rampSound;
	
	
	public AnimationClip race,brake,boost;
	public AnimationClip idle;
	
	public AnimationClip kick;
	public AnimationClip punch;
	public AnimationClip death;
	
	public bool skipClicked;
	
	public Transform hand,leg;
	
	
	Rect rightPunchRect,rightKickRect;
	
	bool audioplayOnce,attackOnce,leftPunchbool,leftAttackbool,punchOnce;
	public AudioClip deathSound,kickSound,hitgrunt,grunt,hurdleHit;
	
	
	
	public static bool isdead,startGame;
	
	
	//public GUIStyle punchstyle,kickstyle;
	public bool crash;
	public bool isAttacking;
	endlessmodeGraphics script;
	
	public static bool timeover;
	bool countonce;
	
	
	bool collideOnce;
//	Rect skipRect;
//	public GUIStyle skipStyle;
	public WheelCollider front,rear;
	WheelHit hit;
	
	public AudioClip rampAudioClip;
	public bool isStunting,stuntbool;
	public AnimationClip oneWheeling;

	
	bool oppoattack;
	public bool unstability;
	int stuntCount,desertLevel;
	// Use this for initialization
	void soundOn()
	{
		if (PlayerPrefs.GetInt ("SoundOff")==0) 
		{
			audioSource = (AudioSource)GetComponent (typeof(AudioSource));
			GetComponent<AudioSource>().playOnAwake=true;
			//AudioListener.volume=1.0f;
			GetComponent<AudioSource>().Play();
			GetComponent<AudioSource>().enabled=true;
			raceSound.GetComponent<AudioSource>().enabled=true;
			rampSound.GetComponent<AudioSource>().enabled=true;
			
		}
		else 
		{
			GetComponent<AudioSource>().Pause();
			GetComponent<AudioSource>().enabled=false;
			raceSound.GetComponent<AudioSource>().enabled=false;
			rampSound.GetComponent<AudioSource>().enabled=false;
			
		}
		
	}
	
	
	string biker;
	string bike;
	bool brakeOnce;
	bool setTimeScale ;
	int layerMask;
	
	void Start () {
		
		if (SceneManager.GetActiveScene().name.Contains ("desert")) {
			desertLevel=1;
		}
		attackCount = 0;
		
		stuntCount = 0;
		setTimeScale = false;
	
		layerMask = 1 << 8;
		lifeBar = false;
		levelClear = false;
		//PlayerPrefs.DeleteAll ();
		//PlayerPrefs.SetInt ("show",0);
		skipClicked=false;
		
		brakeOnce = true;
		collideOnce=true;
		playAnimationOnce = true;
		script = GameObject.Find ("graphics").GetComponent<endlessmodeGraphics> ();
		
		
		
		countonce = true;
		
		
		timeover = false;
		lifeEnd = false;
		
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		
		startGame = false;
		
		
		isAttacking = false;
		
		isdead = false;
		
		attackOnce = true;
		punchOnce = true;
		
		
		hitOnce = true;
		
		score = 0;
		cash = 0;
		//skipRect=new Rect(Screen.width*.62f, (Screen.height-Screen.width*0.3f*59/365)/2, Screen.width*.3f, Screen.width *.3f*59/365 );
		
		
		
		cash = PlayerPrefs.GetInt("cash");
		score = PlayerPrefs.GetInt("cash");
		//cash = score = 2000;
		speed = 1000f;
		audioplayOnce = true;
		
		
		rightKickRect = new Rect(Screen.width-Screen.width*0.125f, Screen.height-Screen.width*0.125f, Screen.width*.12f, Screen.width * .125f );
		rightPunchRect=new Rect(Screen.width*0.0f, Screen.height-Screen.width*0.125f, Screen.width*.125f, Screen.width *.125f );
		
		
		
		
		
		player.GetComponent<Animation>() [idle.name].layer = 0;
		player.GetComponent<Animation>() [race.name].layer = 1;
		
		
		player.GetComponent<Animation>() [kick.name].layer = 2;
		player.GetComponent<Animation>() [punch.name].layer = 3;
		player.GetComponent<Animation>() [death.name].layer = 4;
		
		
		player.GetComponent<Animation>().Play (race.name,PlayMode.StopAll);
		//starting ();
		Invoke ("starting",0.05f);
		Invoke ("soundOn",1.5f);
		
		
//		biker=	PlayerPrefs.GetString("bikerMesh");
//		bike=PlayerPrefs.GetString("bikeMesh");
		//		print (biker);
		dressCode ();
		//player.FindChild ("biker-v3").transform.renderer.material.mainTexture= Resources.Load(biker) as Texture;
		//player.FindChild ("bike_crb").transform.renderer.material.mainTexture= Resources.Load(bike) as Texture;
		player.GetComponent<Animation>() [punch.name].speed = 2f;
		player.GetComponent<Animation>() [kick.name].speed = 2f;
		
		
		
		
		
		
		
	}
	
	
		public Texture[] shirts,bikeTexture;
		public Material bikerMat,bikeMat;
	void dressCode ()
	{
	bike="heavy"+	PlayerPrefs.GetString("bikeColor");
	biker=	PlayerPrefs.GetString("shirtColor");
				switch (biker) {
				case "shirt1":
						bikerMat.mainTexture = shirts [0];
						break;
				case "shirt2":
						bikerMat.mainTexture = shirts [1];
						break;
				case "shirt3":
						bikerMat.mainTexture = shirts [2];
						break;
				case "shirt4":
						bikerMat.mainTexture = shirts [3];
						break;
				case "shirt5":
						bikerMat.mainTexture = shirts [4];
						break;
				case "shirt6":
						bikerMat.mainTexture = shirts [5];
						break;
				case "shirt7":
						bikerMat.mainTexture = shirts [6];
						break;
				case "shirt8":
						bikerMat.mainTexture = shirts [7];
						break;
				case "shirt9":
						bikerMat.mainTexture = shirts [8];
						break;
				case "shirt10":
						bikerMat.mainTexture = shirts [9];
						break;
				}

				switch (bike) {
				case "heavyyellow":
						bikeMat.mainTexture = bikeTexture [0];
						break;
				case "heavyred":
						bikeMat.mainTexture = bikeTexture [1];
						break;
				case "heavygrey":
						bikeMat.mainTexture = bikeTexture [2];
						break;
				case "heavyblue":
						bikeMat.mainTexture = bikeTexture [3];
						break;
				case "heavyblack":
						bikeMat.mainTexture = bikeTexture [4];
						break;


				}
			
		//player.Find ("biker-v3").transform.GetComponent<Renderer>().material.mainTexture= Resources.Load(biker) as Texture;
	//	player.Find ("bike_crb").transform.GetComponent<Renderer>().material.mainTexture= Resources.Load(bike) as Texture;
	}
	
	
	public void RevivalStart()
	{
		if (transform.localScale.x <= -1f) {
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
		transform.root.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		transform.root.GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeRotationZ;
		handleArrow.start=true;
		heavyBikeTurns.startRace = true;
		startGame=true;
		
		tiltControl.startTiltAfterCam = true;

		script.chances = 3;
		shadowsOff ();
		StartCoroutine ("flash");
		attackCount = 0;
		player.root.GetComponent<Rigidbody>().constraints =~ RigidbodyConstraints.FreezeAll;
		player.root.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
		
		player.Find ("biker-v3").gameObject.SetActive (true);
		if(ragdoll!=null)
			ragdoll.gameObject.SetActive (false);
		
		setTimeScale = false;
	
		
		lifeBar = false;
		levelClear = false;
		
		skipClicked=false;
		
		brakeOnce = true;
		collideOnce=true;
		playAnimationOnce = true;
		
		
		countonce = true;
		
		
		timeover = false;
		lifeEnd = false;
		
		crash = false;
		
		startGame = false;
		
		
		isAttacking = false;
		
		isdead = false;
		
		attackOnce = true;
		punchOnce = true;
		
		
		hitOnce = true;
		
		
		
		Invoke ("soundOn",0.25f);
		
		cash = PlayerPrefs.GetInt("cash");
		score = PlayerPrefs.GetInt("cash");
		
		speed = 1000f;
		audioplayOnce = true;

		
		player.GetComponent<Animation>().Play (race.name,PlayMode.StopAll);

			
	}
	
	
	void starting()
	{
		
		
		if ( desertLevel==1) {

			transform.root.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			transform.root.GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeRotationZ;
			handleArrow.start=true;
			heavyBikeTurns.startRace = true;
			startGame=true;
			GameObject.Find("AnimationCamera").GetComponent<startAnimation>().startanimating();
			
		} 
		
		
		
		
		
	}
	
	void FixedUpdate()
	{

		if (startGame  && !isdead) {
			
			distance =timefinish.position.z-player.position.z;
			
		}

		if(lifeBar)
		{
			if(	player.root.GetComponent<heavyBikeTurns>().counter==0 && attackCount<3)
			{
				attackCount=0;
			}
			else if(player.root.GetComponent<heavyBikeTurns>().counter==0 && attackCount>3 && attackCount<6)
			{
				attackCount=3;
			}
			else if(player.root.GetComponent<heavyBikeTurns>().counter==0 && attackCount>6 && attackCount<9)
			{
				attackCount=6;
			}
			lifeBar=false;
		}
		

	
		if (!Application.isEditor) {
			
			if(handleArrow.start)
			{
				
				startGame = true;
				
				
			}
			if(startGame  && !isdead && !levelClear && tiltControl.startTiltAfterCam ){	
				
				for (int k = 0; k < Input.touchCount; ++k) {
					Touch touch = Input.GetTouch (k);
					if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled) { 
						Vector2 touchPos = new Vector2 (touch.position.x, Screen.height - touch.position.y);     
						
						
						if(rightPunchRect.Contains (touchPos))
						{
							
							//rightPunchRect=new Rect(Screen.width*0.0f, Screen.height-Screen.width*0.15f, Screen.width*.16f, Screen.width *.16f );
							
							//	print (	!player.root.GetComponent<heavyBikeTurns> ().flyoverStart + "  "+ !player.root.GetComponent<heavyBikeTurns> ().gangsterHit+ "  "+ player.root.GetComponent<heavyBikeTurns> ().isJumping+ "  "+ transform.root.GetComponent<weaponAI>().isAttacking);
							if(	!player.root.GetComponent<heavyBikeTurns> ().flyoverStart  &&!player.root.GetComponent<heavyBikeTurns> ().isJumping&& !transform.root.GetComponent<weaponAI>().isAttacking )
							{
								if(punchOnce)
								{
									if(PhycamViews.counter==11 || PhycamViews.counter==12)
									{
										PhycamViews.counter=2;
										mainCamera.changeCamView();
									}
									leftPunchbool=true;
									punchOnce=false;
									Invoke("setter", 1f);
									
								}
							}
							
						}

						if(rightKickRect.Contains (touchPos))
						{
							
							//rightKickRect = new Rect(Screen.width-Screen.width*0.15f, Screen.height-Screen.width*0.15f, Screen.width*.16f, Screen.width * .16f );
							if(!player.root.GetComponent<heavyBikeTurns> ().isJumping &&	!player.root.GetComponent<heavyBikeTurns> ().flyoverStart  && !transform.root.GetComponent<weaponAI>().isAttacking )
							{
								if(PhycamViews.counter==11 || PhycamViews.counter==12)
								{
									PhycamViews.counter=2;
									mainCamera.changeCamView();
								}
								if(attackOnce)
								{
									leftAttackbool=true;
									attackOnce=false;
									Invoke("setter", 1f);
								}
							}
						}
					}
					else
					{
						//rightKickRect = new Rect(Screen.width-Screen.width*0.14f, Screen.height-Screen.width*0.14f, Screen.width*.14f, Screen.width * .14f );
						//rightPunchRect=new Rect(Screen.width*0.0f, Screen.height-Screen.width*0.14f, Screen.width*.14f, Screen.width *.14f );
						leftPunchbool=false;
						leftAttackbool=false;
						
					}
	
				}
	
			}
		}
	}
	
	
	
	void setter()
	{
		attackOnce = true;
		punchOnce = true;
		//	stuntOnce = true;
	}
	
	
	//	
	//	bool hover=true;
	//	float myTimer=2f;
	//	
	//	void reset1(){
	//		if (hover) {
	//			
	//			hover=false;
	//		} else {
	//			
	//			hover=true;
	//		}
	//		
	//		
	//	}
	
	
	//public static bool makestaminaempty;
	bool callOnce=true;
	
	
	
	void Update()
	{

		//Add New Control Left Punch

		if (!player.root.GetComponent<heavyBikeTurns>().flyoverStart && !player.root.GetComponent<heavyBikeTurns>().isJumping && !transform.root.GetComponent<weaponAI>().isAttacking && Input.GetKey(KeyCode.J))
		{
			if (punchOnce)
			{
				if (PhycamViews.counter == 11 || PhycamViews.counter == 12)
				{
					PhycamViews.counter = 2;
					mainCamera.changeCamView();
				}
				leftPunchbool = true;
				punchOnce = false;
				Invoke("setter", 1f);

			}
		}

		//Add New Control Right Punch

		if (!player.root.GetComponent<heavyBikeTurns>().isJumping && !player.root.GetComponent<heavyBikeTurns>().flyoverStart && !transform.root.GetComponent<weaponAI>().isAttacking && Input.GetKey(KeyCode.K))
		{
			if (PhycamViews.counter == 11 || PhycamViews.counter == 12)
			{
				PhycamViews.counter = 2;
				mainCamera.changeCamView();
			}
			if (attackOnce)
			{
				leftAttackbool = true;
				attackOnce = false;
				Invoke("setter", 1f);
			}
		}






		//		if (staminaBarCollision) {
		//			if(staminaBarCounter<endlessmodeGraphics.INITIAL_HEALTH)
		//			{
		//				staminaBarCounter+=1;
		//				endlessmodeGraphics.MAX_HEALTH+=1;
		//				//AddingstaminaTexture();
		//			}
		//			staminaBarCollision=false;
		//			
		//		}

		//		
		//		if (staminaBarCounter == 0) {
		//			myTimer -= Time.deltaTime;
		//			
		//			if(myTimer<=1.5f)
		//			{
		//				reset1();
		//				myTimer=2f;
		//			}
		//			if(hover)	
		//				script.staminaBar= Resources.Load<Texture2D>("staminabr-Green_BG");
		//			else
		//				script.staminaBar= Resources.Load<Texture2D>("staminabr-red_BG");
		//		}

		if (timeover) {
			raceSound.SetActive(false);
			
			if(brakeOnce)
			{
				player.GetComponent<Animation>().Play (brake.name, PlayMode.StopAll);
				transform.root.GetComponent<Rigidbody>().velocity=Vector3.zero;
				brakeOnce=false;
				Invoke("gameover",brake.length);
			}
		}
		if (timeover || levelClear) {
			raceSound.SetActive(false);		
		}
		if (levelClear) {
			rampSound.SetActive(true);	
				
		}
		if (levelClear && endlessmodeGraphics.gameMode.Equals("Idle")) {
			frontWheel.Rotate (0,0,80);
			rearWheel.Rotate (0,0,80);		
		}
		if (isdead) {
			rampSound.SetActive(false);	
			raceSound.SetActive(false);
			if(PlayerPrefs.GetInt("levels")!=11 && PlayerPrefs.GetInt("levels")!=12 )
			transform.Find ("biker-v3").gameObject.SetActive (false);
			
		}
		
		if (startGame && !isdead && !crash && !levelClear) {
			
			if (setTimeScale) {
				
				if (Time.timeScale < 1.0f) {
					Time.timeScale = Mathf.Lerp (Time.timeScale, 1, 0.1f);		
				}
				if (Time.fixedDeltaTime < 0.02f) {
					Time.fixedDeltaTime = Mathf.Lerp (Time.fixedDeltaTime, 0.02f, 0.1f);		
				}
				if (Time.timeScale >= 0.985 && Time.fixedDeltaTime >= 0.0195f) {
					setTimeScale = false;
					
					
					
				}
			}
		}
		
		if (!isAttacking && startGame  &&!isdead   && !timeover &&!unstability &&!crash &&!levelClear ) {
			
			//			if (front.GetGroundHit(out hit) && rear.GetGroundHit(out hit)) {
			//				
			//				raceSound.SetActive (true);
			//				rampSound.SetActive (false);
			//		
			//				
			//			}
			//			else
			//			{
			//				raceSound.SetActive (false);
			//
			//				rampSound.SetActive (true);
			//		
			//			}
			if ((front.GetGroundHit(out hit) && rear.GetGroundHit(out hit)) ) {
				if(!raceSound.activeSelf)
					raceSound.SetActive (true);
				rampSound.SetActive (false);
				if(heavyBikeTurns.Onramp)
				{
					player.root.GetComponent<heavyBikeTurns>().rampControl=false;
					heavyBikeTurns.Onramp=false;
				}
				
				
			}
			else if( !front.GetGroundHit(out hit) || !rear.GetGroundHit(out hit))
			{
				
				
				raceSound.SetActive (false);
				if(!rampSound.activeSelf)
					rampSound.SetActive (true);
				
			}
			
			//			print ("run"+ !isStunting+ "  "+!heavyBikeTurns.jumpAnim );
			
			if(!isStunting && !heavyBikeTurns.jumpAnim)
			{	
				if(!heavyBikeTurns.boostbool)
				{
					
					player.GetComponent<Animation>().Play (race.name, PlayMode.StopAll);
					
					//audio.Pause();
					//raceSound.SetActive(true);
					
					raceSound.GetComponent<AudioSource>().pitch=1.2f;
					nitroLeft.gameObject.SetActive(false);
					nitroRight.gameObject.SetActive(false);
					
				}
				else if(heavyBikeTurns.boostbool )
				{
					player.GetComponent<Animation>().Play (boost.name, PlayMode.StopAll);	
					nitroLeft.gameObject.SetActive(true);
					nitroRight.gameObject.SetActive(true);
					raceSound.GetComponent<AudioSource>().pitch=1.45f;
					//raceSound.SetActive(false);
					//nitroSound.SetActive (true);
				}
				
				 if(PhycamViews.slowingDown )
				{

					raceSound.GetComponent<AudioSource>().pitch=0.4f;
					//raceSound.SetActive(false);
					//nitroSound.SetActive (true);
				}
				
			}
			
			//			if(isStunting)
			//			{
			////				print ("inside");
			//				nitroLeft.gameObject.SetActive(false);
			//				nitroRight.gameObject.SetActive(false);
			//			}
			
			if(!heavyBikeTurns.boostbool)
			{
				nitroLeft.gameObject.SetActive(false);
				nitroRight.gameObject.SetActive(false);
			}
			
		}
		
		
		
		if (startGame && !isdead &&!crash &&!levelClear ) {
			
			
			RaycastHit hit = new RaycastHit ();
			
			if(isAttacking)
			{
				
				//Debug.DrawRay (hand.position, Vector3.left*0.1f, Color.red);
				if (Physics.Raycast (hand.position  , Vector3.left, out hit, 0.12f,layerMask)) {
					
					//	print (hit.collider.transform.root.name+ " "+ hit.collider.name+ "   "+ (hit.collider.transform.root.position.x-player.root.position.x));
					if (hit.collider.transform.root.name.Contains ("opponentFatguy") && (hit.collider.transform.root.position.x-player.root.position.x<=-0.35f)) {
						if(callOnce)
						{
						
								hit.collider.transform.root.GetComponent<newLevelCrashed>().opponentAttacked("punch");
		
							callOnce=false;
						}
					}
					if ((hit.collider.transform.root.name.Contains ("heavybike endless") ||hit.collider.transform.root.name.Contains ("opponentTallguys")) && (hit.collider.transform.root.position.x-player.root.position.x<=-0.35f)) {
						if(callOnce)
						{

								hit.collider.transform.root.GetComponent<newLevelCrashed>().opponentAttacked("punch");
						
							callOnce=false;
						}
					}
				}
				
				
				//Debug.DrawRay (leg.position+ new Vector3(0.1f,0,0), Vector3.right*0.1f, Color.red);
				if (Physics.Raycast (leg.position+ new Vector3(0.1f,0,0)  , Vector3.right, out hit, 0.12f,layerMask)) {
					//Debug.Log (hit.collider.transform.root.name+ " "+ (hit.collider.transform.root.position.x-player.root.position.x));
					//Debug.DrawRay (leg.position+ new Vector3(0.1f,0,0), Vector3.right*0.75f, Color.red);
					//				print (hit.collider.transform.root.name+ " "+ hit.collider.name);
					if ((hit.collider.transform.root.name.Contains ("heavybike endless")||hit.collider.transform.root.name.Contains ("opponentTallguys")) && (hit.collider.transform.root.position.x-player.root.position.x<=0.5f)) {
						if(callOnce)
						{
							if ( desertLevel==1) 
								hit.collider.transform.root.GetComponent<newLevelCrashed>().opponentAttacked("kick");
						
							callOnce=false;
						}
					}
					if (hit.collider.transform.root.name.Contains ("opponentFatguy") && (hit.collider.transform.root.position.x-player.root.position.x<=0.5f)) {//2.16
						if(callOnce)
						{
							if ( desertLevel==1) 
								hit.collider.transform.root.GetComponent<newLevelCrashed>().opponentAttacked("kick");

							//Instantiate (scorePopup, player.transform.position + new Vector3 (0f, 2f, 4f), Quaternion.identity);
							callOnce=false;
						}
					}	
				}
				
			}
			
			if (leftPunchbool ) {
				//print (	!player.root.GetComponent<heavyBikeTurns> ().flyoverStart + "  "+ !player.root.GetComponent<heavyBikeTurns> ().gangsterHit+ "  "+ !player.root.GetComponent<heavyBikeTurns> ().isJumping);
				
				if(	!player.root.GetComponent<heavyBikeTurns> ().flyoverStart &&!player.root.GetComponent<heavyBikeTurns> ().isJumping )
				{
					isAttacking = true;
					
					
					
					GetComponent<AudioSource>().PlayOneShot (kickSound);
					GetComponent<AudioSource>().PlayOneShot(grunt);
					player.GetComponent<Animation>().Play (punch.name, PlayMode.StopAll);
					Invoke ("punchOn", 1f);
					
					
				}
				leftPunchbool=false;
			}
			
			
			
			
			if (leftAttackbool ) {
				if(	!player.root.GetComponent<heavyBikeTurns> ().flyoverStart  &&!player.root.GetComponent<heavyBikeTurns> ().isJumping )
				{
					isAttacking = true;
					GetComponent<AudioSource>().PlayOneShot (kickSound);
					GetComponent<AudioSource>().PlayOneShot(grunt);
					player.GetComponent<Animation>().Play (kick.name, PlayMode.StopAll);
					Invoke ("attackOn", 1f);
					
				}
				leftAttackbool=false;
			}
			
			
			
			if (stuntbool) {
				
				if ( desertLevel!=1) 
				{
					if(heavyBikeTurns.boostbool)
					{
						nitroLeft.gameObject.SetActive(false);
						nitroRight.gameObject.SetActive(false);
					}
					
					isStunting=true;
					
					
					
					player.GetComponent<Animation>().Play (oneWheeling.name, PlayMode.StopAll);
					if(bikeSelection.bikeCount<6)
						Invoke("stuntOn", 3.667f);
					
					if(bikeSelection.bikeCount>=6)
						Invoke("stuntOn", 2.5f);
					
					
					
					
					
					stuntbool=false;
				}
				else
				{
					if (stuntbool && !transform.root.GetComponent<weaponAI>().isAttacking) {
						
						
						
						isStunting=true;
						stuntCount+=1;
						
						if(stuntCount>=5)
						{
							stuntCount=1;
						}
						if(heavyBikeTurns.boostbool)
						{
							stuntCount=1;
						}
						if(stuntCount==1)
						{
							player.GetComponent<Animation>().Play (oneWheeling.name, PlayMode.StopAll);
							
							Invoke("stuntOn", 3.667f);
						}
						else if(stuntCount==2)
						{
							if (player.position.y > -5.495418f && player.position.y < 14.03454f)
							{
								if(player.localScale.x<=-1.5f){
									
									Vector3 theScale = player.localScale;
									theScale.x *= -1;
									player.localScale = theScale;
									
								}
							}
							else if (player.position.y >=14.03454 && player.position.y <17.64518f)
							{
								if(player.localScale.x>=1.5f){
									
									Vector3 theScale = player.localScale;
									theScale.x *= -1;
									player.localScale = theScale;
									
								}
							}
							player.GetComponent<Animation>().Play ("stunt2", PlayMode.StopAll);
							Invoke("stuntOn", 2.16f);
						}
						else if(stuntCount==3)
						{
							player.GetComponent<Animation>().Play ("stunt4", PlayMode.StopAll);
							Invoke("stuntOn", 6.5f);
						}
						else if(stuntCount==4)
						{
							player.GetComponent<Animation>().Play ("stunt5", PlayMode.StopAll);
							Invoke("stuntOn", 3.167f);
						}
						
						
						
						
						
						stuntbool=false;
					}
				}
			}
			
			
			
			
		}
		
		
	}
	
	
	
	public void colliding()
	{
		
		if (!crash && !isdead) {
			if (countonce) {
				attackCount += 1;
				player.root.GetComponent<heavyBikeTurns> ().counter += 1;
				countonce = false;
			}
			
			
			player.root.GetComponent<heavyBikeTurns> ().flyoverStart=true;
			
			if ( attackCount >= 3 ) {
				script.chances -= 1;
				playerDeath ();
				
				
				
			}
			 else if (attackCount != 3 ) {
				if (!crash) {
					unstability = true;
					if (audioplayOnce) {
						if(GetComponent<AudioSource>().enabled)
						{
							
							GetComponent<AudioSource>().PlayOneShot (hurdleHit);
							GetComponent<AudioSource>().PlayOneShot (hitgrunt);
						}
						audioplayOnce = false;

						Invoke ("setTime", 0.1f);
					}
					
					
					
					player.GetComponent<Animation>().Play (unstable.name, PlayMode.StopAll);
					Invoke ("unstablebike", unstable.length);
					
				}
			}
			
			
			
			
			
		}
		
		
	}
	void setTime()
	{
		
		setTimeScale = true;
		
		
	}
	public void  carCrash()
	{
		if (!crash && !isdead) {
		
			

				attackCount = 3;
				script.chances -= 1;
				playerDeath ();
				
				
				
			
		}
	}
	void deathOn()
	{
		
		tiltControl.playerCrashed = true;
		if(tiltControl.totalOppo<10)
			tiltControl.totalOppo += 1;
		

		if (heavyBikeTurns.boostbool) {
			nitroRight.gameObject.SetActive (true);
			nitroLeft.gameObject.SetActive (true);		
		}
		
		player.root.GetComponent<heavyBikeTurns> ().flyoverStart=false;
		player.root.GetComponent<heavyBikeTurns> ().jump = false;
//		player.root.GetComponent<findTarget>().isJumping=true;
		CameraSettings.rampstart = false;
		CameraSettings.rampstart2 = false;
		CameraSettings.rampstart3 = false;
		CameraSettings.jumpstart = false;
		CameraSettings.jumpstart2 = false;
		CameraSettings.jumpstart3 = false;
		

		skipClicked=true;
		Invoke( "skipp",1f);
		
		collideOnce = true;
		
		shadow.gameObject.SetActive (false);
		countonce = true;
		crash = false;
		
		audioplayOnce = true;
		
		player.root.GetComponent<Rigidbody>().constraints =~ RigidbodyConstraints.FreezeAll;
		player.root.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
		player.root.GetComponent<heavyBikeTurns>().counter=0;
		
		
		player.localPosition = new Vector3 (0,0f,0);
		player.root.position = new Vector3 (2.2f,	-0.01882806f,heavyBikeTurns.hurdlePosition+130f);
		

		Time.timeScale = 1;
		Time.fixedDeltaTime = 0.02f;
		
		//	
		player.GetComponent<Animation>().Play (race.name,PlayMode.StopAll);
		raceSound.SetActive(true);
		
		//	player.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		
		
		StartCoroutine ("flash");
		
	}
	
	void punchOn()
	{
		
		scoreOnce = true;
		
		hitOnce = true;
		
		isAttacking = false;
		callOnce = true;
		punchOnce = true;

		playAnimationOnce = true;
		
	}
	//player.animation.Play (race.name, PlayMode.StopAll);
	
	void attackOn()
	{
		
		scoreOnce = true;
		hitOnce = true;
		callOnce = true;
		isAttacking = false;
	
		attackOnce=true;
		playAnimationOnce = true;
		
		//player.animation.Play (race.name, PlayMode.StopAll);
		
	}

	
	void skipp()
	{
		skipClicked = false;
		
	}
	
	bool playOnce;
	int attackCount;
	
	
	public static	bool levelClear;
	public AnimationClip survive,unstable;
	public AudioClip surviveSound,hitSound;
	
	
	void OnCollisionEnter(Collision col)
	{
		print (col.gameObject.name+ " "+ col.gameObject.tag);
		
		
	}
	
	public  void CrashPlayer()
	{
		
		//		print ("crash");
		if (!isAttacking && !timeover && !levelClear) {
			if (!crash && !isdead) {
				if(!heavyBikeTurns.isShielded)
				{
				if (countonce) {

					attackCount += 1;
					player.root.GetComponent<heavyBikeTurns>().counter+=1;
					
					countonce = false;
					
					
					if ( attackCount >= 3) {
						script.chances -= 1;
						playerSurvivalDeath();
						//playerDeath ();
						
						
						
					}
					 else if (attackCount != 3 ) {
						if (!crash) {
							unstability = true;
							if (audioplayOnce) {
								GetComponent<AudioSource>().PlayOneShot (hitSound);
								GetComponent<AudioSource>().PlayOneShot (hitgrunt);
								audioplayOnce = false;
							}
							
							
							//						print ("unstability");
							
							player.GetComponent<Animation>().Play (unstable.name, PlayMode.StopAll);
							Invoke ("unstablebike", unstable.length);
						}
					}
					
					
					
					
					
				}
				
				
				
			}
			
			
			}	
		}	
		
		
		
		
	}
	
	public  void CrashPlayer(string weaponName)
	{
		
		if (!isAttacking && !timeover && !levelClear) {
			if (!crash && !isdead) {
					if(!heavyBikeTurns.isShielded)
					{
				if (countonce) {
					attackCount += 1;
					player.root.GetComponent<heavyBikeTurns>().counter+=1;
					if(weaponName.Contains("bat"))
					{
						if(attackCount!=3 && attackCount!=6 && attackCount!=9 )
						{
							attackCount += 1;
							player.root.GetComponent<heavyBikeTurns>().counter+=1;
						}
					}
					if(weaponName.Contains("axe"))
					{
						if(attackCount!=3 && attackCount!=6 && attackCount!=9 )
						{
							attackCount += 2;
							player.root.GetComponent<heavyBikeTurns>().counter+=1;
						}
					}
					countonce = false;
					//					print ("weapon name: "+ weaponName+ "   "+ attackCount);
					
					
					
					if (attackCount >= 3) {
						script.chances -= 1;
						playerDeath ();
						
						
						
					}
					 else if (attackCount != 3 ) {
						if (!crash) {
							unstability = true;
							if (audioplayOnce) {
								GetComponent<AudioSource>().PlayOneShot (hitSound);
								GetComponent<AudioSource>().PlayOneShot (hitgrunt);
								audioplayOnce = false;
							}
							
							
							//						print ("unstability");
							
							player.GetComponent<Animation>().Play (unstable.name, PlayMode.StopAll);
							Invoke ("unstablebike", unstable.length);
						}
					}
					
					
					
				}		
				
			}
			
				}
			
			
			
			
			
		}	
		
		
		
		
	}
	
	public void stuntOn()
	{
	
		if(heavyBikeTurns.boostbool)
		{
			nitroLeft.gameObject.SetActive(true);
			nitroRight.gameObject.SetActive(true);
		}
		isStunting = false;

	}
	
	void crashOn()
	{
		tiltControl.playerCrashed = true;
		if(tiltControl.totalOppo<10)
			tiltControl.totalOppo += 1;

		player.root.GetComponent<Rigidbody>().constraints =~ RigidbodyConstraints.FreezeAll;
		player.root.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
		
		
		countonce = true;
		audioplayOnce = true;
		crash = false;
		shadow.gameObject.SetActive(false);
		
		player.GetComponent<Animation>().Play (race.name,PlayMode.StopAll);
		raceSound.SetActive(true);
		player.root.GetComponent<heavyBikeTurns>().counter=0;
		
		if (heavyBikeTurns.boostbool) {
			nitroLeft.gameObject.SetActive(true);
			nitroRight.gameObject.SetActive(true);		
		}

	}
	
	void shadowsOn()
	{
		shadow.gameObject.SetActive(true);
	}
	void shadowsOff()
	{
		shadow.gameObject.SetActive(false);
	}

	
	void unstablebike()
	{
		unstability = false;
		audioplayOnce = true;
		countonce = true;
		//player.animation.Play (race.name, PlayMode.StopAll);
		
	}
	public 	void playerSurvivalDeath()
	{
		
		
		if (collideOnce) {

			if (audioplayOnce) {
//				print ("survival death");
				if (PlayerPrefs.GetInt ("SoundOff")==0) 
				transform.root.GetComponent<AudioSource>().PlayOneShot (deathSound);
				audioplayOnce = false;
			}
			Time.timeScale = 0.2f;
			Time.fixedDeltaTime=0.006f;

			//			print (transform.root.position.x+"  "+transform.localScale.x);
			if(transform.root.position.x<=1f)
			{
				transform.root.position+=new Vector3(0.15f,0,0);
				
				
			}
			if(transform.root.position.x<=1.2f)
			{
				//print (transform.localScale.x);
				if (transform.localScale.x >= 1.5f) {
					Vector3 theScale = transform.localScale;
					theScale.x *= -1;
					transform.localScale = theScale;
				}
			}

			player.root.GetComponent<heavyBikeTurns>().counter=3;
			heavyBikeTurns.isdead=true;
			nitroLeft.gameObject.SetActive(false);
			nitroRight.gameObject.SetActive(false);

			Invoke ("shadowsOff", 3f);
			
			player.root.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			player.root.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
			player.root.eulerAngles=new Vector3(0,0,0);
			
			player.root.position = new Vector3 (player.root.position.x, 0.1f, player.root.position.z);
			player.root.GetComponent<Rigidbody>().velocity = Vector3.zero;
			
			
			
			player.root.GetComponent<Rigidbody>().angularVelocity=Vector3.zero;
			player.GetComponent<Animation>() [survive.name].speed = 1.7f;
			player.GetComponent<Animation>().Play (survive.name, PlayMode.StopAll);
			revivalScreen();
			
			Time.timeScale = 1.0f;
			
			
			
			crash=true;
			isdead = true;
			lifeEnd = true;
			
			raceSound.SetActive (false);
			
			collideOnce=false;
			
		}	
		
	}
	
	
	public 	void playerDeath()
	{
		
		
		if (collideOnce) {
			if (audioplayOnce) {
				GetComponent<AudioSource>().PlayOneShot (deathSound);
				audioplayOnce = false;
			}
			Time.timeScale = 0.2f;
			Time.fixedDeltaTime=0.006f;

			player.root.GetComponent<heavyBikeTurns>().counter=3;
			heavyBikeTurns.isdead=true;
			nitroLeft.gameObject.SetActive(false);
			nitroRight.gameObject.SetActive(false);
		
			shadowsOff ();
			player.root.GetComponent<Rigidbody>().velocity=Vector3.zero;
			player.root.GetComponent<Rigidbody>().angularVelocity=Vector3.zero;
			
			player.GetComponent<Animation>().Play (death.name, PlayMode.StopAll);
//			
			if(PlayerPrefs.GetInt("levels")!=11 && PlayerPrefs.GetInt("levels")!=12)
			{
			ragdoll = (Transform)Instantiate (ragdollprefab, player.transform.position + new Vector3 (0f, 0.5f, 0f), Quaternion.identity);
			ragdoll.Rotate (270, 90, 0);
			ragdoll_rigid = ragdoll.Find ("Bip01").transform;
			
			ragdoll.Find ("biker-v3").transform.GetComponent<Renderer>().material.mainTexture = Resources.Load (biker) as Texture;
			

			ragdoll.transform.position = new Vector3 (ragdoll.position.x, player.position.y + 0.5f, player.position.z + 1f);
			transform.Find ("biker-v3").gameObject.SetActive (false);		
			

			GameObject.Find ("Main Camera").GetComponent<camFollow> ().target = ragdoll_rigid;
			GameObject.Find ("Main Camera").GetComponent<camFollow> ().player = ragdoll_rigid;
		
			
			ragdoll_rigid.GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, 20);
			
			ragdoll_rigid.GetComponent<Rigidbody>().AddForce (new Vector3 (0, 0, 20), ForceMode.Force);//=-1*gameObject.rigidbody.velocity;
			
			
			}

			Invoke ("revivalScreen", 1.5f);
	
			
			
			
			isdead = true;
			lifeEnd = true;
			
			raceSound.SetActive (false);
			
			collideOnce=false;
			
		}	
		
	}
	
	
	void revivalScreen()
	{
		if (endlessmodeGraphics.gameMode.Equals ("Idle")) {
						endlessmodeGraphics.gameMode = "Revival";
						script.revivePlayer ();
						GetComponent<AudioSource>().enabled = false;
						raceSound.GetComponent<AudioSource>().enabled = false;
				}
	}

	void gameover()
	{
		endlessmodeGraphics.gameMode="GameOver";
		script.gameOverFtn ();
		GetComponent<AudioSource>().enabled=false;
		raceSound.GetComponent<AudioSource>().enabled=false;
	}
	IEnumerator flash()
	{
		transform.Find("biker-v3").GetComponent<Renderer>().enabled=false;
		transform.Find("bike_crb").GetComponent<Renderer>().enabled=false;
		yield return new WaitForSeconds(0.05f);
		transform.Find("biker-v3").GetComponent<Renderer>().enabled=true;
		transform.Find("bike_crb").GetComponent<Renderer>().enabled=true;
		yield return new WaitForSeconds(0.05f);
		transform.Find("biker-v3").GetComponent<Renderer>().enabled=false;
		transform.Find("bike_crb").GetComponent<Renderer>().enabled=false;
		yield return new WaitForSeconds(0.05f);
		transform.Find("biker-v3").GetComponent<Renderer>().enabled=true;
		transform.Find("bike_crb").GetComponent<Renderer>().enabled=true;
		yield return new WaitForSeconds(0.05f);
		transform.Find("biker-v3").GetComponent<Renderer>().enabled=false;
		transform.Find("bike_crb").GetComponent<Renderer>().enabled=false;
		yield return new WaitForSeconds(0.05f);
		transform.Find("biker-v3").GetComponent<Renderer>().enabled=true;
		transform.Find("bike_crb").GetComponent<Renderer>().enabled=true;
		yield return new WaitForSeconds(0.05f);
		transform.Find("biker-v3").GetComponent<Renderer>().enabled=false;
		transform.Find("bike_crb").GetComponent<Renderer>().enabled=false;
		yield return new WaitForSeconds(0.05f);
		transform.Find("biker-v3").GetComponent<Renderer>().enabled=true;
		transform.Find("bike_crb").GetComponent<Renderer>().enabled=true;
		yield return new WaitForSeconds(0.05f);
		transform.Find("biker-v3").GetComponent<Renderer>().enabled=false;
		transform.Find("bike_crb").GetComponent<Renderer>().enabled=false;
		yield return new WaitForSeconds(0.05f);
		transform.Find("biker-v3").GetComponent<Renderer>().enabled=true;
		transform.Find("bike_crb").GetComponent<Renderer>().enabled=true;
		
	}	
	
}
