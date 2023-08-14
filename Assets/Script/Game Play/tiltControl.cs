using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class tiltControl : MonoBehaviour {
	public newLevelHarley leftbikeScript;
	public newLevelHarley rightbikeScript;
	public static bool startTiltAfterCam;
	public Texture full;
	public Transform parentPlayer;
	public float z_position;
	public GameObject player,tallguy,fatguy;
	bool playerdead;
	public bool flyoverStart,rampControl;
	bool crash;
	int levelID;
	turnLevelcontrols harleyBikescript;
	heavyBikeTurnControls heavyBikeScript;	
	public static bool playerCrashed;
	public Transform leftBike,rightBike;
	public static int totalOppo;
	public static bool twoBiker;
	public static int position;
	//public static totalOpponent;
	GameObject target_obj;
	float dist;
	RaycastHit hit = new RaycastHit ();
	int layerMask;
	//bool boostPosition;
	public static int showPosition;
	public float tiltSpeed;
	bool desertLevel;
	
	void Start () 
	{
		if (!PlayerPrefs.HasKey ("tiltSpeed")) {

			PlayerPrefs.SetFloat ("tiltSpeed", 0.003f);
			PlayerPrefs.Save ();
		}
		sal.value = PlayerPrefs.GetFloat ("tiltSpeed");
		tiltSpeed = PlayerPrefs.GetFloat ("tiltSpeed");
		startTiltAfterCam = false;
		//boostPosition = false;
		layerMask = 1 << 8;
		
		setOnce = false;
		//PlayerPrefs.SetInt("KnockOutLevel",10);
		twoBiker = false;
		position = 5;
		showPosition = 6;
		
		totalOppo = 10;
		
		flyoverStart = false;
		rampControl = false;
		//parentPlayer = endlessmodeGraphics.instance.player;
		z_position = parentPlayer.position.x;
		
		playerCrashed = false;
		
		
		if (SceneManager.GetActiveScene().name.Equals ("desert")) 
		{
				
						desertLevel = true;
				}
		twoBiker = true;

	}
		public Slider sal;

		public void changeSliderFtn(){
				PlayerPrefs.SetFloat ("tiltSpeed", sal.value);
				PlayerPrefs.Save ();
				tiltSpeed = sal.value;
		}

	void Awake()
	{
		//	bikeSelection.bikeCount = 7;
		
		if (bikeSelection.bikeCount == 2 ||bikeSelection.bikeCount == 3||  bikeSelection.bikeCount == 5 || bikeSelection.bikeCount == 6 || bikeSelection.bikeCount == 7 || bikeSelection.bikeCount == 8) {
			player = fatguy;
			fatguy.SetActive(true);
			tallguy.SetActive(false);
			levelID=0;
			player.GetComponent<Animation>() ["race"].speed = 0.2f;
			harleyBikescript=player.GetComponent<turnLevelcontrols>();
			
			
		}
		if (bikeSelection.bikeCount == 1 || bikeSelection.bikeCount == 4 || bikeSelection.bikeCount == 9 || bikeSelection.bikeCount == 10) {
			player = tallguy;
			tallguy.SetActive(true);
			fatguy.SetActive(false);
			levelID=1;
			
			heavyBikeScript=player.GetComponent<heavyBikeTurnControls>();
		}
	}
	
	bool setOnce;
	
	void Update () 
	{
		
		if (levelID == 0) {
			playerdead=turnLevelcontrols.isdead;
			crash=harleyBikescript.crash;
			
		} else if (levelID == 1) {
			playerdead=heavyBikeTurnControls.isdead;	
			crash=heavyBikeScript.crash;
		}
		
		//		print (position+ " / "+ totalOppo + "   : showPosition:  "+ showPosition);
		
		if (playerCrashed) {
		//	boostPosition=false;

				rightbikeScript.FollowPlayer = false;
				leftbikeScript.FollowPlayer = false;

			leftBike.root.position=new Vector3(-3.8f,leftBike.root.position.y,transform.root.position.z-200);
			rightBike.root.position=new Vector3(8.4f,rightBike.root.position.y,transform.root.position.z-200);
			Invoke("resetFollowing",5f);
			playerCrashed=false;		
		}

		if (heavyBikeTurns.boostbool) {
			setOnce=true;	
		
		} else {
			
			if (setOnce) 
			{
			//	boostPosition=true;
				leftbikeScript.FollowPlayer = false;
				rightbikeScript.FollowPlayer = false;
				leftBike.root.position=new Vector3(-5f,leftBike.root.position.y,transform.root.position.z-200);
				rightBike.root.position=new Vector3(9f,rightBike.root.position.y,transform.root.position.z-200);
				CancelInvoke("resetFollowing");
				Invoke("resetFollowing",1.5f);
				setOnce = false;
			}
		}
		
		 if (parentPlayer.position.x <= 1.15f) {
			parentPlayer.position = new Vector3 (1.15f, parentPlayer.position.y,parentPlayer.position.z );
			z_position = 1.15f;
		}
			else if (parentPlayer.position.x >= 4.5f) {
			parentPlayer.position = new Vector3 (4.5f, parentPlayer.position.y,parentPlayer.position.z );
			z_position = 4.5f;
		}
		
		if(!global_var.player_death  && heavyBikeTurns.startRace && !crash && !playerdead && endlessmodeGraphics.gameMode=="Idle" && startTiltAfterCam)
		{
		//	print ("z_position "+ z_position);
		//	#if UNITY_ANDROID
			//			
			
			//print((transform.position.z-leftBike.root.position.z));
			if(Time.timeScale>0){
			if(!Application.isEditor)
			{

			if(!flyoverStart &&!rampControl  ){
				
				if (Input.acceleration.x    > 0.06f && z_position != 4.5f) {
					
					if( transform.position.z-rightBike.root.position.z>-2.0f && transform.position.z-rightBike.root.position.z<7.5f ){
						/////////////-0.47

						if	( (transform.position.x-rightBike.root.position.x<-0.55f )|| transform.position.x-rightBike.root.position.x>0.0f){
							//Debug.Log(" first  : "+ ( transform.position.x-rightBike.root.position.x));

							if(parentPlayer.position.x <4.5f)
							{
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, -20, 3 * Time.deltaTime));
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
																				parentPlayer.position += new Vector3 (tiltSpeed* heavyBikeTurns.speed_now,0, 0);
							
								z_position = parentPlayer.position.x;
							}
							else	 if (parentPlayer.position.x >= 4.5f) {
								parentPlayer.position = new Vector3 (4.5f, parentPlayer.position.y,parentPlayer.position.z );
								z_position = 4.5f;
								
							}
							
						//	Debug.Log(" zeroth  : "+ ( transform.position.x-rightBike.root.position.x));

						}
						if	( (transform.position.x-rightBike.root.position.x<-0.45f )|| transform.position.x-rightBike.root.position.x>0.0f){

							if(parentPlayer.position.x <4.5f)
							{
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, -20, 3 * Time.deltaTime));
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
																				parentPlayer.position += new Vector3 ((tiltSpeed-0.0005f) * heavyBikeTurns.speed_now,0, 0);
								
								z_position = parentPlayer.position.x;
							}
							else	 if (parentPlayer.position.x >= 4.5f) {
								parentPlayer.position = new Vector3 (4.5f, parentPlayer.position.y,parentPlayer.position.z );
								z_position = 4.5f;
								
							}
							
						//	Debug.Log(" first  : "+ ( transform.position.x-rightBike.root.position.x));

						}
						else 	if	( transform.position.x-rightBike.root.position.x>-0.2f && transform.position.x-rightBike.root.position.x<0f ){
							if (parentPlayer.position.x > 1.15f) {
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 20, 3 * Time.deltaTime));
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
				
																				parentPlayer.position -= new Vector3 ((tiltSpeed+0.0005f) * heavyBikeTurns.speed_now,0, 0);
								z_position = parentPlayer.position.x;
								
							}
							else if (parentPlayer.position.x <= 1.15f) {
								parentPlayer.position = new Vector3 (1.15f, parentPlayer.position.y,parentPlayer.position.z );
								z_position = 1.15f;
							}
							//print ("second");
							//	Debug.Log(" second  : "+( transform.position.x-rightBike.root.position.x));
						}
//						
						
					} 
					
					else	if( transform.position.z-leftBike.root.position.z>-2.0f && transform.position.z-leftBike.root.position.z<7.5f ){
						//print("third: "+(transform.position.x-leftBike.root.position.x));
						if( (transform.position.x-leftBike.root.position.x<-0.44f || transform.position.x-leftBike.root.position.x>0.185f )  ){//1.59
							if (parentPlayer.position.x < 4.5f)
							{
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, -20, 3 * Time.deltaTime));
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
								
								parentPlayer.position += new Vector3 (tiltSpeed * heavyBikeTurns.speed_now,0, 0 );
								z_position = parentPlayer.position.x;
								
							}
							else	 if (parentPlayer.position.x >= 4.5f) {
								parentPlayer.position = new Vector3 (4.5f, parentPlayer.position.y,parentPlayer.position.z );
								z_position = 4.5f;
								
							}
						//	Debug.Log(" third : "+ ( transform.position.x-leftBike.root.position.x));
						}
						else if( (transform.position.x-leftBike.root.position.x>-0.3f && transform.position.x-leftBike.root.position.x<0f ) ){
							//	print("fourth: "+(transform.position.x-leftBike.root.position.x));
							if(parentPlayer.position.x >1.15f)
							{
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 20, 3 * Time.deltaTime));
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
								//parent.transform.position -= new Vector3 (0.003f * heavyBikeTurns.speed_now,0, 0);
																				parentPlayer.position -= new Vector3 (tiltSpeed * heavyBikeTurns.speed_now,0, 0);
								z_position = parentPlayer.position.x;
							}
							else if (parentPlayer.position.x <= 1.15f) {
								parentPlayer.position = new Vector3 (1.15f, parentPlayer.position.y,parentPlayer.position.z );
								z_position = 1.15f;
							}
						//	Debug.Log(" fourth : "+ ( transform.position.x-leftBike.root.position.x));
						} 
						
						
					}
					else if (parentPlayer.position.x < 4.5f) {
						//print ("after crash");
						transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, -20, 3 * Time.deltaTime));
						transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
																parentPlayer.position += new Vector3 (tiltSpeed * heavyBikeTurns.speed_now,0, 0 );
						
						z_position = parentPlayer.position.x;
						
					}
					else	 if (parentPlayer.position.x >= 4.5f) {
						parentPlayer.position = new Vector3 (4.5f, parentPlayer.position.y,parentPlayer.position.z );
						z_position = 4.5f;
						
					}
					if (parentPlayer.position.x <= 1.15f) {
						parentPlayer.position = new Vector3 (1.15f, parentPlayer.position.y,parentPlayer.position.z );
						z_position = 1.15f;
					}
				}
				
				
				
				
				
				
				else if (Input.acceleration.x  < -0.06f && z_position != 1.15f) {
					
					
					if( transform.position.z-leftBike.root.position.z>-2.0f && transform.position.z-leftBike.root.position.z<7.5f ){
							//print("fifth first: "+(transform.position.x-leftBike.root.position.x));
						////////////0.47
						if	( ( transform.position.x-leftBike.root.position.x>0.55f)|| (transform.position.x-leftBike.root.position.x)<0f ){
							if(parentPlayer.position.x >1.15f)
							{
								//print("fifth first: "+(transform.position.x-leftBike.root.position.x));
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 20, 3 * Time.deltaTime));
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
																				parentPlayer.position -= new Vector3 (tiltSpeed * heavyBikeTurns.speed_now,0, 0 );
								z_position = parentPlayer.position.x;
								
							}
							else if (parentPlayer.position.x <1.15f) {
								parentPlayer.position = new Vector3 (1.15f, parentPlayer.position.y,parentPlayer.position.z );
								z_position = 1.15f;
								
							}
						//Debug.Log("fifth : "+ ( transform.position.x-leftBike.root.position.x));
						}
						else if	( ( transform.position.x-leftBike.root.position.x>0.45f)|| (transform.position.x-leftBike.root.position.x)<0f ){
							if(parentPlayer.position.x >1.15f)
							{
								//print("fifth first: "+(transform.position.x-leftBike.root.position.x));
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 20, 3 * Time.deltaTime));
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
																				parentPlayer.position -= new Vector3 ((tiltSpeed- 0.0005f) * heavyBikeTurns.speed_now,0, 0 );
								z_position = parentPlayer.position.x;
								
							}
							else if (parentPlayer.position.x <1.15f) {
								parentPlayer.position = new Vector3 (1.15f, parentPlayer.position.y,parentPlayer.position.z );
								z_position = 1.15f;
								
							}
						//	Debug.Log("sixth : "+ ( transform.position.x-leftBike.root.position.x));
						}
						else 	if	( ((transform.position.x-leftBike.root.position.x<0.2f && transform.position.x-leftBike.root.position.x>0f ) )  ){
								if(parentPlayer.position.x <4.5f)
								{
									transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, -20, 3 * Time.deltaTime));
									transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
								
																				parentPlayer.position += new Vector3 ((tiltSpeed+ 0.0005f)  * heavyBikeTurns.speed_now,0, 0);
									z_position = parentPlayer.position.x;
								}
								else if (parentPlayer.position.x > 4.5f) {
									parentPlayer.position = new Vector3 (4.5f, parentPlayer.position.y,parentPlayer.position.z );
									z_position = 4.5f;
								}
							//	Debug.Log("seventh : "+ ( transform.position.x-leftBike.root.position.x));	
							}
						
						
					} 
					else if( transform.position.z-rightBike.root.position.z>-2.0f && transform.position.z-rightBike.root.position.z<7.5f ){
						//print ((transform.position.x-rightBike.root.position.x));
						if	( (transform.position.x-rightBike.root.position.x>0.44f||  transform.position.x-rightBike.root.position.x<0f ) ){
							if(parentPlayer.position.x >1.15f){
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 20, 3 * Time.deltaTime));
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
						
																				parentPlayer.position -= new Vector3 (tiltSpeed * heavyBikeTurns.speed_now,0, 0);
								z_position = parentPlayer.position.x;
							}
							else if (parentPlayer.position.x <1.15f) {
								parentPlayer.position = new Vector3 (1.15f, parentPlayer.position.y,parentPlayer.position.z );
								z_position = 1.15f;
								
							}
						//	Debug.Log("eighth : "+ ( transform.position.x-rightBike.root.position.x));
						}
						else if	( (transform.position.x-rightBike.root.position.x<0.35f &&  transform.position.x-rightBike.root.position.x>0f ) ){
							if(parentPlayer.position.x <4.5f)
							{
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, -20, 3 * Time.deltaTime));
								transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
							
																				parentPlayer.position+= new Vector3 ((tiltSpeed- 0.0005f)  * heavyBikeTurns.speed_now,0, 0);
								z_position = parentPlayer.position.x;
							}
							else if (parentPlayer.position.x > 4.5f) {
								parentPlayer.position = new Vector3 (4.5f, parentPlayer.position.y,parentPlayer.position.z );
								z_position = 4.5f;
							}
							//Debug.Log("nineth : "+ ( transform.position.x-rightBike.root.position.x));
						}
						
						
					}
					else if (parentPlayer.position.x > 1.15f) {
					//Debug.Log("ninth : ");
						transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 20, 3 * Time.deltaTime));
						transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
																parentPlayer.position -= new Vector3 (tiltSpeed * heavyBikeTurns.speed_now,0, 0);//	parent.transform.position -= new Vector3 (0.005f * speed_now,0, 0);
						z_position = parentPlayer.position.x;
						
					} 
					
					else if (parentPlayer.position.x <1.15f) {
						//Debug.Log("tenth : ");
						parentPlayer.position = new Vector3 (1.15f, parentPlayer.position.y,parentPlayer.position.z );
						z_position = 1.15f;
						
					}
					if (parentPlayer.position.x >= 4.5f) {
						//Debug.Log("eleventh : ");
						parentPlayer.position = new Vector3 (4.5f, parentPlayer.position.y,parentPlayer.position.z );
						z_position = 4.5f;
					}
					
				} else {
					transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 8 * Time.deltaTime), Mathf.LerpAngle (transform.eulerAngles.z, 0, 5 * Time.deltaTime));
					parentPlayer.position = new Vector3 (z_position, parentPlayer.position.y,parentPlayer.position.z );
					
				}
				
				
			}
			else if(flyoverStart || rampControl )
			{
				transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 8 * Time.deltaTime), Mathf.LerpAngle (transform.eulerAngles.z, 0, 5 * Time.deltaTime));
				parentPlayer.position = new Vector3 (z_position, parentPlayer.position.y,parentPlayer.position.z );
				
			}
			

			
			
			
			

					
			
			
			
			}
			

			else{


								if(!flyoverStart &&!rampControl  ){
									
						if (Input.GetAxis("Horizontal")     > 0.06f && z_position != 4.5f) {
										
										if( transform.position.z-rightBike.root.position.z>-2.0f && transform.position.z-rightBike.root.position.z<7.5f ){
											/////////////-0.47
					
											if	( (transform.position.x-rightBike.root.position.x<=-0.57f )|| transform.position.x-rightBike.root.position.x>0.0f){
												//Debug.Log(" first  : "+ ( transform.position.x-rightBike.root.position.x));
					
												if(parentPlayer.position.x <4.5f)
												{
													transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, -20, 3 * Time.deltaTime));
													transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
																				parentPlayer.position += new Vector3 (tiltSpeed * heavyBikeTurns.speed_now,0, 0);
												
													z_position = parentPlayer.position.x;
												}
												else	 if (parentPlayer.position.x >= 4.5f) {
													parentPlayer.position = new Vector3 (4.5f, parentPlayer.transform.position.y,parentPlayer.transform.position.z );
													z_position = 4.5f;
													
												}
												
											//	Debug.Log(" zeroth  : "+ ( transform.position.x-rightBike.root.position.x));
					
											}
											if	( (transform.position.x-rightBike.root.position.x<=-0.45f )|| transform.position.x-rightBike.root.position.x>0.0f){
					
												if(parentPlayer.position.x <4.5f)
												{
													transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, -20, 3 * Time.deltaTime));
													transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
																				parentPlayer.position += new Vector3 (tiltSpeed* heavyBikeTurns.speed_now,0, 0);
													
													z_position = parentPlayer.position.x;
												}
												else	 if (parentPlayer.position.x >= 4.5f) {
													parentPlayer.position = new Vector3 (4.5f, parentPlayer.position.y,parentPlayer.position.z );
													z_position = 4.5f;
													
												}
												
											//	Debug.Log(" first  : "+ ( transform.position.x-rightBike.root.position.x));
					
											}
											else 	if	( transform.position.x-rightBike.root.position.x>-0.2f && transform.position.x-rightBike.root.position.x<0f ){
												if (parentPlayer.position.x > 1.15f) {
													transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 20, 3 * Time.deltaTime));
													transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
									
																				parentPlayer.position -= new Vector3 ((tiltSpeed+0.0005f)  * heavyBikeTurns.speed_now,0, 0);
													z_position = parentPlayer.position.x;
													
												}
												else if (parentPlayer.position.x <= 1.15f) {
													parentPlayer.position = new Vector3 (1.15f, parentPlayer.position.y,parentPlayer.position.z );
													z_position = 1.15f;
												}
												//print ("second");
												//	Debug.Log(" second  : "+( transform.position.x-rightBike.root.position.x));
											}
					//						
											
										} 
										
										else	if( transform.position.z-leftBike.root.position.z>-2.0f && transform.position.z-leftBike.root.position.z<7.5f ){
											//print("third: "+(transform.position.x-leftBike.root.position.x));
											if( (transform.position.x-leftBike.root.position.x<-0.44f || transform.position.x-leftBike.root.position.x>0.185f )  ){//1.59
												if (parentPlayer.position.x < 4.5f)
												{
													transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, -20, 3 * Time.deltaTime));
													transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
													
																				parentPlayer.position += new Vector3 (tiltSpeed * heavyBikeTurns.speed_now,0, 0 );
													z_position = parentPlayer.position.x;
													
												}
												else	 if (parentPlayer.position.x >= 4.5f) {
													parentPlayer.position = new Vector3 (4.5f, parentPlayer.position.y,parentPlayer.position.z );
													z_position = 4.5f;
													
												}
											//	Debug.Log(" third : "+ ( transform.position.x-leftBike.root.position.x));
											}
											else if( (transform.position.x-leftBike.root.position.x>-0.3f && transform.position.x-leftBike.root.position.x<0f ) ){
												//	print("fourth: "+(transform.position.x-leftBike.root.position.x));
												if(parentPlayer.position.x >1.15f)
												{
													transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 20, 3 * Time.deltaTime));
													transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
													//parent.transform.position -= new Vector3 (0.003f * heavyBikeTurns.speed_now,0, 0);
																				parentPlayer.position -= new Vector3 (tiltSpeed * heavyBikeTurns.speed_now,0, 0);
													z_position = parentPlayer.position.x;
												}
												else if (parentPlayer.position.x <= 1.15f) {
													parentPlayer.position = new Vector3 (1.15f, parentPlayer.position.y,parentPlayer.position.z );
													z_position = 1.15f;
												}
											//	Debug.Log(" fourth : "+ ( transform.position.x-leftBike.root.position.x));
											} 
											
											
										}
										else if (parentPlayer.position.x < 4.5f) {
											//print ("after crash");
											transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, -20, 3 * Time.deltaTime));
											transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
																parentPlayer.position += new Vector3 (tiltSpeed * heavyBikeTurns.speed_now,0, 0 );
											
											z_position = parentPlayer.position.x;
											
										}
										else	 if (parentPlayer.position.x >= 4.5f) {
											parentPlayer.position = new Vector3 (4.5f, parentPlayer.position.y,parentPlayer.position.z );
											z_position = 4.5f;
											
										}
										if (parentPlayer.position.x <= 1.15f) {
											parentPlayer.position = new Vector3 (1.15f, parentPlayer.position.y,parentPlayer.position.z );
											z_position = 1.15f;
										}
									}
									
									
									
									
									
									
						else if (Input.GetAxis("Horizontal")   < -0.06f && z_position != 1.15f) {
										
										
										
										
										
										if( transform.position.z-leftBike.root.position.z>-2.0f && transform.position.z-leftBike.root.position.z<7.5f ){
												//print("fifth first: "+(transform.position.x-leftBike.root.position.x));
											////////////0.47
											if	( ( transform.position.x-leftBike.root.position.x>0.55f)|| (transform.position.x-leftBike.root.position.x)<0f ){
												if(parentPlayer.position.x >1.15f)
												{
													//print("fifth first: "+(transform.position.x-leftBike.root.position.x));
													transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 20, 3 * Time.deltaTime));
													transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
																				parentPlayer.position -= new Vector3 ((tiltSpeed+0.0001f) * heavyBikeTurns.speed_now,0, 0 );
													z_position = parentPlayer.position.x;
													
												}
												else if (parentPlayer.position.x <1.15f) {
													parentPlayer.position = new Vector3 (1.15f, parentPlayer.position.y,parentPlayer.position.z );
													z_position = 1.15f;
													
												}
											//Debug.Log("fifth : "+ ( transform.position.x-leftBike.root.position.x));
											}
											else if	( ( transform.position.x-leftBike.root.position.x>0.45f)|| (transform.position.x-leftBike.root.position.x)<0f ){
												if(parentPlayer.position.x >1.15f)
												{
													//print("fifth first: "+(transform.position.x-leftBike.root.position.x));
													transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 20, 3 * Time.deltaTime));
													transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
																				parentPlayer.position -= new Vector3 (tiltSpeed * heavyBikeTurns.speed_now,0, 0 );
													z_position = parentPlayer.position.x;
													
												}
												else if (parentPlayer.position.x <1.15f) {
													parentPlayer.position = new Vector3 (1.15f, parentPlayer.position.y,parentPlayer.position.z );
													z_position = 1.15f;
													
												}
											//	Debug.Log("sixth : "+ ( transform.position.x-leftBike.root.position.x));
											}
											else 	if	( ((transform.position.x-leftBike.root.position.x<0.25f && transform.position.x-leftBike.root.position.x>0f ) )  ){
													if(parentPlayer.position.x <4.5f)
													{
														transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, -20, 3 * Time.deltaTime));
														transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
													
																				parentPlayer.position += new Vector3 ((tiltSpeed+ 0.0005f)  * heavyBikeTurns.speed_now,0, 0);
														z_position = parentPlayer.position.x;
													}
													else if (parentPlayer.position.x > 4.5f) {
														parentPlayer.position = new Vector3 (4.5f, parentPlayer.position.y,parentPlayer.position.z );
														z_position = 4.5f;
													}
												//	Debug.Log("seventh : "+ ( transform.position.x-leftBike.root.position.x));	
												}
											
											
										} 
										else if( transform.position.z-rightBike.root.position.z>-2.0f && transform.position.z-rightBike.root.position.z<7.5f ){
											//print ((transform.position.x-rightBike.root.position.x));
											if	( (transform.position.x-rightBike.root.position.x>0.44f||  transform.position.x-rightBike.root.position.x<0f ) ){
												if(parentPlayer.position.x >1.15f){
													transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 20, 3 * Time.deltaTime));
													transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
													//parent.transform.position -= new Vector3 (0.003f * heavyBikeTurns.speed_now,0, 0);
																				parentPlayer.position -= new Vector3 (tiltSpeed * heavyBikeTurns.speed_now,0, 0);
													z_position = parentPlayer.position.x;
												}
												else if (parentPlayer.position.x <1.15f) {
													parentPlayer.position = new Vector3 (1.15f, parentPlayer.position.y,parentPlayer.position.z );
													z_position = 1.15f;
													
												}
											//	Debug.Log("eighth : "+ ( transform.position.x-rightBike.root.position.x));
											}
											else if	( (transform.position.x-rightBike.root.position.x<0.35f &&  transform.position.x-rightBike.root.position.x>0f ) ){
												if(parentPlayer.position.x <4.5f)
												{
													transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, -20, 3 * Time.deltaTime));
													transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
													//parent.transform.position+= new Vector3 (0.003f * heavyBikeTurns.speed_now,0, 0);
																				parentPlayer.position+= new Vector3 ((tiltSpeed- 0.001f)  * heavyBikeTurns.speed_now,0, 0);
													z_position = parentPlayer.position.x;
												}
												else if (parentPlayer.position.x > 4.5f) {
													parentPlayer.position = new Vector3 (4.5f, parentPlayer.position.y,parentPlayer.position.z );
													z_position = 4.5f;
												}
												//Debug.Log("nineth : "+ ( transform.position.x-rightBike.root.position.x));
											}
											
											
										}
										else if (parentPlayer.position.x > 1.15f) {
										//Debug.Log("ninth : ");
											transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 20, 3 * Time.deltaTime));
											transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 3 * Time.deltaTime), transform.eulerAngles.z);
																parentPlayer.position -= new Vector3 (tiltSpeed * heavyBikeTurns.speed_now,0, 0);//	parent.transform.position -= new Vector3 (0.005f * speed_now,0, 0);
											z_position = parentPlayer.position.x;
											
										} 
										
										else if (parentPlayer.position.x <1.15f) {
											//Debug.Log("tenth : ");
											parentPlayer.position = new Vector3 (1.15f, parentPlayer.position.y,parentPlayer.position.z );
											z_position = 1.15f;
											
										}
										if (parentPlayer.position.x >= 4.5f) {
											//Debug.Log("eleventh : ");
											parentPlayer.position = new Vector3 (4.5f, parentPlayer.position.y,parentPlayer.position.z );
											z_position = 4.5f;
										}
										
									} else {
										transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 8 * Time.deltaTime), Mathf.LerpAngle (transform.eulerAngles.z, 0, 5 * Time.deltaTime));
										parentPlayer.position = new Vector3 (z_position, parentPlayer.position.y,parentPlayer.position.z );
										
									}
									
									
								}
								else if(flyoverStart || rampControl )
								{
									transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.LerpAngle (transform.eulerAngles.y, 0, 8 * Time.deltaTime), Mathf.LerpAngle (transform.eulerAngles.z, 0, 5 * Time.deltaTime));
									parentPlayer.position = new Vector3 (z_position, parentPlayer.position.y,parentPlayer.position.z );
									
								}
								
		
		}
			
			}
			

			
		} 

	}
	

	void resetFollowing()
	{
		

						leftBike.root.GetComponent<newLevelCrashed> ().count = 0;
						leftBike.root.GetComponent<newLevelCrashed> ().healthBar.transform.GetComponent<Renderer>().material.mainTexture = full;
						rightBike.root.GetComponent<newLevelCrashed> ().count = 0;
						rightBike.root.GetComponent<newLevelCrashed> ().healthBar.transform.GetComponent<Renderer>().material.mainTexture = full;	
				

						int random = Random.Range (0, 10);
						if (!twoBiker) {
				
				
								if (random % 2 == 0) {
						
						
										leftbikeScript.FollowPlayer = true;
										rightbikeScript.FollowPlayer = false;
						
						
										leftBike.root.position = new Vector3 (1f, leftBike.root.position.y, transform.root.position.z+20);//-5
										rightBike.root.position = new Vector3 (4.5f, rightBike.root.position.y, transform.root.position.z - 100);
						
								} else {
						
						
									leftbikeScript.FollowPlayer = false;
									rightbikeScript.FollowPlayer = true;
						
						
										rightBike.root.position = new Vector3 (1f, rightBike.root.position.y, transform.root.position.z +20);
										leftBike.root.position = new Vector3 (4.5f, leftBike.root.position.y, transform.root.position.z - 100);
						
								}
					

						} else if (twoBiker) {
				


					random = Random.Range (0, 10);
					if (random % 2 == 0) {
						
						
						leftbikeScript.FollowPlayer = true;
						rightbikeScript.FollowPlayer = true;
						
						leftBike.root.position = new Vector3 (1f, leftBike.root.position.y, transform.root.position.z +20);
						rightBike.root.position = new Vector3 (4.5f, rightBike.root.position.y, transform.root.position.z +20);
						
					} else {
						
						leftbikeScript.FollowPlayer = true;
						rightbikeScript.FollowPlayer = true;
						
						rightBike.root.position = new Vector3 (4.5f, rightBike.root.position.y, transform.root.position.z +20);
						leftBike.root.position = new Vector3 (1f, leftBike.root.position.y, transform.root.position.z +20);
					}
			


//					random = Random.Range (0, 10);
//					if (random % 2 == 0) {
//						
//						
//						leftbikeScript.FollowPlayer = true;
//						rightbikeScript.FollowPlayer = false;
//						
//						leftBike.root.position = new Vector3 (1f, leftBike.root.position.y, transform.root.position.z +20);
//						rightBike.root.position = new Vector3 (4.5f, rightBike.root.position.y, transform.root.position.z - 200);
//						
//					} else {
//						
//						leftbikeScript.FollowPlayer = false;
//						rightbikeScript.FollowPlayer = true;
//						
//						rightBike.root.position = new Vector3 (4.5f, rightBike.root.position.y, transform.root.position.z +20);
//						leftBike.root.position = new Vector3 (1f, leftBike.root.position.y, transform.root.position.z - 200);
//					}
//


						}
	
		}	
}
