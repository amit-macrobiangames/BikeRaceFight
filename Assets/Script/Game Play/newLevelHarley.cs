using UnityEngine;
using System.Collections;

public class newLevelHarley : MonoBehaviour {
	public AudioClip explosionSound;
	public bool attackedThroughGun;
	private GameObject player;
	public Transform com;
	public WheelCollider wheel_r;
	public WheelCollider wheel_r_player;
	private float own_vel_magnitude;
	private float player_vel_magnitude;
	private bool flag;                     
	
	
	private float player_patchup_speed;
	
	
	Transform childPlayer;
	bool crash;
	bool skipClick;
	
	public bool itselfCrashed;
	public Texture[] bike;
	public Texture[] biker;
	
	bool death;
	public Transform playerTransform;
	float xPosition;
	bool xOnce;
	bool avoidCar;
	
	public bool FollowPlayer;
	public bool isCrashed;
	int layerMask;
	bool resetOnce=true;
	GameObject targetObj ;
	float dist;
	RaycastHit hit=new RaycastHit();
	int bikeCostume,bikerCostume;
	public bool playerLeftSide,playerRightSide,playerAhead;
	public int oppoBikeID,isleft;
	public float intendedxPosition=-3.8f;
	public float extendedxPosition=-4.85f;
	int playerBikeID;
	public turnLevelcontrols harleyPlayer;
	public heavyBikeTurnControls heavyPlayer;
	int levelID;
	void Start () {
		levelID = PlayerPrefs.GetInt ("levels");
//		intendedxPosition = -3.8f;
//		extendedxPosition = -4.85f;
		bikeCostume = bikerCostume = 0;
		playerLeftSide = playerRightSide =playerAhead= false;
		layerMask = 1 << 9;
		
		avoidCar = false;
		FollowPlayer = false;
		isCrashed = false;
		
		xPosition =intendedxPosition;
		xOnce = true;
		itselfCrashed = false;
		player = GameObject.FindWithTag ("Player");
		death = false;
		childPlayer = player.GetComponent<heavyBikeTurns> ().player.transform;
		if (childPlayer.name.Contains ("Fatguy")) {
			playerBikeID=0;		
		}
		else
			playerBikeID=1;		
		GetComponent<Rigidbody>().centerOfMass=new Vector3(com.localPosition.x,com.localPosition.y,com.localPosition.z);
		flag = false;
		skipClick = false;
	
		//player_patchup_speed = 0.75f;
		player_patchup_speed = 0.75f;
		StartCoroutine ("startFollow");
		
	}
	
	IEnumerator startFollow()
	{
//		if (isleft == 0) {
//						yield return new WaitForSeconds (2f);
//						FollowPlayer = true;
//	
//						transform.position = new Vector3 (intendedxPosition, transform.position.y, player.transform.position.z +30f);//-5
//				} else {
//			yield return new WaitForSeconds(2f);
//			FollowPlayer = false;
//			transform.position=new Vector3(intendedxPosition,transform.position.y, player.transform.position.z-200f);	
//		}

		if (isleft == 0) {
			yield return new WaitForSeconds (2f);
			FollowPlayer = true;
			
			transform.position = new Vector3 (intendedxPosition, transform.position.y, player.transform.position.z +30f);//-5
		} else {
			yield return new WaitForSeconds(2f);
			FollowPlayer = true;
			transform.position=new Vector3(intendedxPosition,transform.position.y, player.transform.position.z+30f);	
		}
	}
	void Update () {
		
		
		if (attackedThroughGun) {
			xPosition=extendedxPosition;		
		}
		
		if (playerBikeID==1) {
			crash = heavyPlayer.crash;		
			death = heavyBikeTurnControls.isdead;
		} else {
			crash = harleyPlayer.crash;
			death=turnLevelcontrols.isdead;
		}
		if(!avoidCar)
		{
			
			if(playerAhead)
			{
				if(!tiltControl.twoBiker)
				{
					//print ("value: "+(transform.position.z-playerTransform.position.z));
					if(transform.position.z-playerTransform.position.z<=-2f ||  transform.position.z-playerTransform.position.z>=2f ) 
					{
						
						if (Physics.Raycast (transform.position + new Vector3 (0.6f, 0.1f, 0.0f), Vector3.right, out hit,0.5f)) {
							
							if (hit.collider.tag.Contains("Player")|| hit.collider.tag.Contains("newLevelHurdle") || hit.collider.tag.Contains("car")|| hit.collider.tag.Contains("Mine")|| hit.collider.transform.root.name.Contains("bikeParent")) {
								xPosition=playerTransform.position.x-0.5f;
							}
							else
								xPosition=playerTransform.position.x+0.5f;
						}
						else
						{
							if (transform.position.x >4f) { //3.75
								xPosition=playerTransform.position.x-0.5f;
								//xPosition -= 2f;
								
							}
							else if (transform.position.x < 4f && transform.position.x >1.1f ) { //3.75
								xPosition=playerTransform.position.x+0.5f;
								//xPosition += 2f;
								
							}
							else if(transform.position.x <=1.1f)
							{
								xPosition=playerTransform.position.x+0.5f;
								//xPosition += 2f;
							}
						}
					}
				}
				else if(tiltControl.twoBiker)
				{
//								print (FollowPlayer +"playerAhead   "+ ( transform.position.x));

					if(isleft==0)
					{
					if(transform.position.z-playerTransform.position.z<=-3f ||  transform.position.z-playerTransform.position.z>=3f )
					{





						
						 if(playerTransform.position.x<=1f)
						{
						
							xPosition=extendedxPosition;// -4.85f;
						}
						else	if(playerTransform.position.x<=1.2f)
							{
								
								xPosition=extendedxPosition;// -4.85f;
							}
						else
						{
							
							if (transform.position.x > 4.75f) { //3.75
								xPosition=playerTransform.position.x-0.5f;
								//xPosition -= 2f;
								
							}
							else if (transform.position.x < 4.5f && transform.position.x >1f ) { //3.75
								xPosition=playerTransform.position.x-0.5f;
								//xPosition += 2f;
								
							}
							else if(transform.position.x <1f)
							{
								xPosition=playerTransform.position.x+0.5f;
								//xPosition += 2f;
							}
							
							
						}
					}
					}
					else if(isleft==1)
					{

						if(transform.position.z-playerTransform.position.z<=-3f ||  transform.position.z-playerTransform.position.z>=3f )
						{

												if(playerTransform.position.x>4.5f)
												{
													xPosition=4.7f;
												}
							else if(playerTransform.position.x>=4.45f)
							{
								xPosition=extendedxPosition;// -4.85f;
							}
											
						else
						{
							
							if (transform.position.x > 4.5f) { //3.75
								xPosition=4.75f;
								//xPosition -= 2f;
								
							}
							else if (transform.position.x < 4.5f && transform.position.x >1f ) { //3.75
								xPosition=playerTransform.position.x+0.5f;
								//xPosition += 2f;
								
							}
							else if(transform.position.x <1f)
							{
								xPosition=playerTransform.position.x-0.5f;
								//xPosition += 2f;
							}
						
							
						}

					}
					}
				}
				
			}
			
			transform.position = new Vector3 (Mathf.Lerp (transform.position.x, xPosition, 0.1f), transform.position.y, transform.position.z);		
			
		}
		
		if (levelID != 5) {
						if (transform.position.y < -0.1f || transform.position.y >= 2f) {
								transform.position = new Vector3 (xPosition, 0.08f, transform.position.z);
						}
				}
		else if (levelID == 5) {
			if (transform.position.y < -0.1f || transform.position.y >= 7f) {
				transform.position = new Vector3 (xPosition, 0.08f, transform.position.z);
			}
		}
	//	Debug.DrawRay (transform.position + new Vector3 (0.1f, 0.2f, 0.0f),Vector3.right* 0.5f, Color.cyan);

		
		if (levelID != 5 && levelID != 6) {
						targetObj = GetTarget ();
						if (targetObj != null) {
			
		
			
								dist = Vector3.Distance (targetObj.transform.position, transform.position);
		
			
								if (dist > 0 && dist <= 25) {
//				print ("xpositon: " +(transform.position.x - targetObj.transform.position.x));
										if ((transform.position.x - targetObj.transform.position.x) <= 0.5f && (transform.position.x - targetObj.transform.position.x) >= 0f) {
												//	print ("xpositon: " +(transform.position.x - targetObj.transform.position.x));
												if (!avoidCar) {
						
					
														avoidCar = true;
						
				
//						print ("hurdle x-1.5: "+(targetObj.transform.position.x-0.75f)+ "  "+ "hurdle x-1.5: "+(targetObj.transform.position.x+0.7f) );
														//Debug.DrawRay (transform.position + new Vector3 (0.1f, 0.2f, 0.0f),Vector3.right* 0.3f, Color.green);
														if (Physics.Raycast (transform.position + new Vector3 (0.1f, 0.2f, 0.0f), Vector3.right, out hit, 0.4f, layerMask)) {
							
																if (hit.collider.tag.Contains ("Player") || hit.collider.transform.root.name.Contains ("bikeParent")) {

																		//	print ("playerhit  "+(targetObj.transform.position.x));
																		if ((targetObj.transform.position.x - 0.5f) >= 1.1f)
																				xPosition = targetObj.transform.position.x - 0.5f;
																		else if ((targetObj.transform.position.x - 0.4f) >= 0.8f)
																				xPosition = targetObj.transform.position.x - 0.4f;
																		else
																				transform.position -= new Vector3 (extendedxPosition, 0, 5);
																		//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z,20, 5 * Time.deltaTime));

																} else {
																		//	print ("else playerhit  "+(targetObj.transform.position.x));

																		if ((targetObj.transform.position.x + 0.5f) <= 4.5)
																				xPosition = targetObj.transform.position.x + 0.5f;
																		else if ((targetObj.transform.position.x + 0.4f) <= 4.9)
																				xPosition = targetObj.transform.position.x + 0.4f;
																		else
																				transform.position -= new Vector3 (extendedxPosition, 0, 5);
																		//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z,-20, 5 * Time.deltaTime));

																}
														} else {
//							print ("elsee "+(targetObj.transform.position.x));

																if ((targetObj.transform.position.x + 0.5f) <= 4.5)
																		xPosition = targetObj.transform.position.x + 0.5f;
																else if ((targetObj.transform.position.x + 0.4f) <= 4.9)
																		xPosition = targetObj.transform.position.x + 0.4f;
																else
																		transform.position -= new Vector3 (extendedxPosition, 0, 5);

																//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z,-20, 5 * Time.deltaTime));

														}
												}
					
										} else if ((transform.position.x - targetObj.transform.position.x) < 0f && (transform.position.x - targetObj.transform.position.x) >= -0.5f) {
												//print ("xpositon 2nd: " +(transform.position.x - targetObj.transform.position.x));
												if (!avoidCar) {
						
														avoidCar = true;
					
						
					
														if (Physics.Raycast (transform.position + new Vector3 (0.1f, 0.2f, 0.0f), Vector3.left, out hit, 0.4f, layerMask)) {
																//print ("2nd playerhit "+(targetObj.transform.position.x));
																if (hit.collider.tag.Contains ("Player") || hit.collider.transform.root.name.Contains ("bikeParent")) {

																		if ((targetObj.transform.position.x + 0.5f) <= 4.5)
																				xPosition = targetObj.transform.position.x + 0.5f;
																		else if ((targetObj.transform.position.x + 0.4f) <= 4.9)
																				xPosition = targetObj.transform.position.x + 0.4f;
																		else
																				transform.position -= new Vector3 (extendedxPosition, 0, 5);
						
																		//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z,-20, 5 * Time.deltaTime));

								
																} else {
																		//print ("2nd playerhit else "+(targetObj.transform.position.x));
							
																		if ((targetObj.transform.position.x - 0.5f) >= 1.1f)
																				xPosition = targetObj.transform.position.x - 0.5f;
																		else if ((targetObj.transform.position.x - 0.4f) >= 0.8f)
																				xPosition = targetObj.transform.position.x - 0.4f;
																		else
																				transform.position -= new Vector3 (extendedxPosition, 0, 5);
																		//	transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z,20, 5 * Time.deltaTime));

																}
														} else {

																//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z,20, 5 * Time.deltaTime));

																//print ("2nd else "+(targetObj.transform.position.x));
																if ((targetObj.transform.position.x - 0.5f) >= 1.1f) {
																		xPosition = targetObj.transform.position.x - 0.5f;
																		//print ("1st "+(targetObj.transform.position.x-0.75f));
																} else if ((targetObj.transform.position.x - 0.4f) >= 0.8f) {
																		xPosition = targetObj.transform.position.x - 0.4f;
																		//print ("2nd "+(targetObj.transform.position.x-0.7f));
																} else
																		transform.position -= new Vector3 (extendedxPosition, 0, 5);
																//print ("3rd "+(targetObj.transform.position.x));
														}
												}
					
										}
				
								}
						}
				} 

		else if (levelID == 5 && FollowPlayer &&!itselfCrashed) {
			targetObj=GetRamp();
			if(targetObj!=null)
			{
				dist = Vector3.Distance (targetObj.transform.position, transform.position);
				
				
				if (dist > 0 && dist <= 30) {
					if(!playerAhead){
//					print ("ramp xpositon: " +(transform.position.x - targetObj.transform.position.x));
//					if(crash || death){
//					
//
//					avoidCar = true;	
//				//	transform.position = Vector3.MoveTowards(transform.position, targetObj.transform.position, 0.25f);
//						xPosition = targetObj.transform.position.x ;
//					
//					}
					if ((transform.position.x - targetObj.transform.position.x) >= 0.1f ) {
						//	print ("xpositon: " +(transform.position.x - targetObj.transform.position.x));
						if (!avoidCar ) {
							
							
							avoidCar = true;
							if (Physics.Raycast (transform.position + new Vector3 (0.1f, 0.2f, 0.0f), Vector3.left, out hit, 0.5f, layerMask)) {
								
								if (hit.collider.tag.Contains ("Player") || hit.collider.transform.root.name.Contains ("bikeParent")) {
//									print ("bikeHit");
									
									
								} else {
									//xPosition = targetObj.transform.position.x ;
									xPosition = xPosition - 0.15f;
								}
							} 
							else
							{
								//xPosition = targetObj.transform.position.x ;
								xPosition = xPosition - 0.15f;
							}

						}
						
					} else if ((transform.position.x - targetObj.transform.position.x) < -0.1f ) {
				
						if (!avoidCar ) {
							
							avoidCar = true;
							

							
							if (Physics.Raycast (transform.position + new Vector3 (0.1f, 0.2f, 0.0f), Vector3.right, out hit, 0.5f, layerMask)) {

								if (hit.collider.tag.Contains ("Player") || hit.collider.transform.root.name.Contains ("bikeParent")) {
//									print ("bikeHit");
								
									
								} else {
									//xPosition = targetObj.transform.position.x ;
									xPosition = xPosition + 0.15f;
								}
							} 
							else
							{
								//xPosition = targetObj.transform.position.x ;
								xPosition = xPosition + 0.15f;
							}

						}
						
					}
				}
			}
			}
		
		}
		
		
		
		if (avoidCar) {
			transform.position = new Vector3 (Mathf.Lerp (transform.position.x, xPosition, 0.1f), transform.position.y, transform.position.z);
			if(transform.position.x-xPosition <0.1f && transform.position.x-xPosition >-0.1f)
			{
				avoidCar=false;
				//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z,0, 5 * Time.deltaTime));

				
			}
		}
		
		
		
		
	
		if (handleArrow.start && death && !itselfCrashed && !heavyBikeTurns.boostbool) {
			wheel_r.motorTorque = 30;
		}
		
		
		
		
		
		
		
		if (isCrashed) {
			isCrashed=false;	
			transform.root.position = new Vector3 (extendedxPosition,0.2f,transform.root.position.z);
			xPosition = extendedxPosition;
			
			
		}
		
		
		if (handleArrow.start && !crash &&!death && !itselfCrashed && !heavyBikeTurns.boostbool) {
			
			if(FollowPlayer)
			{
				resetOnce = true;
				wheel_r.brakeTorque = 0f;
				if (transform.position.z < player.transform.position.z-3f) {  //if(behind player)
					
					
					speed_up ();
					
					
				} else if (transform.position.z > player.transform.position.z) {     //if at equal adopt player
					adopt_player_speed ();
					
					
				}
				if(playerTransform.position.z - transform.position.z < -170f )
				{
					transform.position=new Vector3(transform.position.x,transform.position.y,playerTransform.position.z+130f);
					wheel_r.motorTorque =20;
				}
			}
			else
			{
			
				wheel_r.motorTorque =wheel_r_player.motorTorque;
				GetComponent<Rigidbody>().velocity=playerTransform.GetComponent<Rigidbody>().velocity;
				
			}
			
			
		} else if (crash &&!death) {
			if (resetOnce) {
				resetOnce = false;
				Invoke ("resetPosition", 4f);
			}
			
		} else if (itselfCrashed) {
			
			wheel_r.motorTorque = 0;
			if (resetOnce) {
				resetOnce = false;
				if(levelID==6)
				Invoke ("resetPosition", 0.5f);
			else
					Invoke ("resetPosition", 4f);
			}
			
		} else if (heavyBikeTurns.boostbool) {
			if(wheel_r.motorTorque>15 && own_vel_magnitude >0 )
			{
				wheel_r.motorTorque -= 2.0f;
				own_vel_magnitude -= 0.5f;
				GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * own_vel_magnitude;
				xPosition=intendedxPosition;
				
				
			}
		}
		
		
		
	}
	
	
	void FixedUpdate()
	{
		if (playerBikeID==1) {
			
			skipClick=heavyPlayer.skipClicked;
			
		}
		else
			skipClick=harleyPlayer.skipClicked;
		
		if (skipClick) {
			
			CancelInvoke("resetPosition");
			resetPosition();	
			
		}
	}
	void resetPosition()
	{
		
		
		transform.root.position = new Vector3 (extendedxPosition,0.2f,player.transform.root.position.z-100);
		transform.root.GetComponent<Rigidbody>().velocity = Vector3.zero;
		transform.root.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		wheel_r.motorTorque = 0.0f;
		wheel_r.brakeTorque = 1000f;
		if (oppoBikeID == 0) {
						transform.Find ("opponentFatguy").Find ("biker_fat-01").transform.GetComponent<Renderer>().material.mainTexture = biker [bikerCostume];
						transform.Find ("opponentFatguy").Find ("bike_body").transform.GetComponent<Renderer>().material.mainTexture = bike [bikeCostume];
				} else if (oppoBikeID == 1) {
			transform.Find ("opponentFatguy").Find ("biker-v3").transform.GetComponent<Renderer>().material.mainTexture = biker [bikerCostume];
			transform.Find ("opponentFatguy").Find ("bike_crb").transform.GetComponent<Renderer>().material.mainTexture = bike [bikeCostume];
		
		}
		if(bikerCostume<4)
			bikerCostume += 1;
		else
			bikerCostume =0;
		
		
		if(bikeCostume<4)
			bikeCostume += 1;
		else
			bikeCostume =0;
		
	}
	void speed_up()
	{
		wheel_r.motorTorque += 2f; //4f;//6
		
		
		
	}
	
	
	

	
	void OnCollisionEnter(Collision col)
	{ 
		//print ("oncollision: "+col.transform.tag+ " "+ col.transform.name);
		if (col.transform.tag.Equals ("car") ) {

			transform.Find ("opponentFatguy").GetComponent<Animation>().Play("death",PlayMode.StopAll);
			CancelInvoke("setPos");
			Invoke("setPos",0.5f);
			itselfCrashed=true;
			
			
		}
		if ((col.transform.tag.Equals ("newLevelHurdle")||col.transform.tag.Equals ("car") || col.transform.tag.Equals ("chowkCar")  ||col.transform.tag.Equals ("rocket")|| col.transform.tag.Equals ("barrel") ) && (levelID==5 ||  levelID==6 || levelID==8|| levelID==9 || levelID==10 ) && endlessmodeGraphics.gameMode.Equals("Idle")  ) {
			if(FollowPlayer && (playerTransform.position.z - transform.position.z < 7f && playerTransform.position.z - transform.position.z >= -1.5f))
			{
				//			print ("ontrigger");
				transform.Find ("opponentFatguy").GetComponent<Animation>().Play("death",PlayMode.StopAll);
				CancelInvoke("setPos");
				Invoke("setPos",0.5f);
				itselfCrashed=true;
				resetOnce=false;
				transform.GetComponent<Rigidbody>().velocity=Vector3.zero;
				transform.GetComponent<Rigidbody>().angularVelocity=Vector3.zero;
			}
			else
			{
				transform.position+=new Vector3(0,0,3f);
			}
			
		}
		if ((col.transform.tag.Equals ("Mine") || col.transform.tag.Equals ("smallMine")) &&  (levelID==11 || levelID==12)&& endlessmodeGraphics.gameMode.Equals("Idle")  ) {
			if(FollowPlayer && (playerTransform.position.z - transform.position.z < 7f && playerTransform.position.z - transform.position.z >= -1.5f))
			{
				//			print ("ontrigger");
				col.transform.parent.GetChild(0).gameObject.SetActive(true);
				col.transform.parent.GetChild(1).gameObject.SetActive(false);
				if(PlayerPrefs.GetInt ("SoundOff")==0)
				{
					GetComponent<AudioSource>().PlayOneShot(explosionSound);
				}
				transform.Find ("opponentFatguy").GetComponent<Animation>().Play("death",PlayMode.StopAll);
				CancelInvoke("setPos");
				Invoke("setPos",0.5f);
				itselfCrashed=true;
				resetOnce=false;
				transform.GetComponent<Rigidbody>().velocity=Vector3.zero;
				transform.GetComponent<Rigidbody>().angularVelocity=Vector3.zero;
			}
			else
			{
				transform.position+=new Vector3(0,0,3f);
			}
			
		}
		if ((col.transform.tag.Equals ("newLevelHurdle")||col.transform.tag.Equals ("car")|| col.transform.tag.Equals ("barrel") ) && (levelID!=5 &&  levelID!=6) && endlessmodeGraphics.gameMode.Equals("Idle")  ) {
			if(FollowPlayer && (playerTransform.position.z - transform.position.z < 7f && playerTransform.position.z - transform.position.z >= -1.5f))
			{
				//			print ("ontrigger");
				transform.Find ("opponentFatguy").GetComponent<Animation>().Play("death",PlayMode.StopAll);
				CancelInvoke("setPos");
				Invoke("setPos",0.5f);
				itselfCrashed=true;
				resetOnce=false;
				transform.GetComponent<Rigidbody>().velocity=Vector3.zero;
				transform.GetComponent<Rigidbody>().angularVelocity=Vector3.zero;
			}
			else
			{
				transform.position+=new Vector3(0,0,3f);
			}
			
		}
//		if ((col.transform.tag.Equals ("newLevelHurdle")||col.transform.tag.Equals ("car")|| col.transform.tag.Equals ("barrel") ) && levelID==5 ) {
//			if(FollowPlayer && (playerTransform.position.z - transform.position.z < 7f || playerTransform.position.z - transform.position.z >= -3f))
//			{
//				print ("oncollider");
//				transform.FindChild ("opponentFatguy").animation.Play("death",PlayMode.StopAll);
//				CancelInvoke("setPos");
//				Invoke("setPos",1.5f);
//				itselfCrashed=true;
//				transform.rigidbody.velocity=Vector3.zero;
//				transform.rigidbody.angularVelocity=Vector3.zero;
//			}
//			
//		}
	}
	void OnTriggerEnter(Collider col)
	{
		//print ("Ontrigger   "+col.transform.tag+ " "+ col.transform.name);
		if ((col.transform.tag.Equals ("newLevelHurdle")||col.transform.tag.Equals ("car")|| col.transform.tag.Equals ("barrel") ) && (levelID==5 || levelID==6) && endlessmodeGraphics.gameMode.Equals("Idle")  ) {
			if(FollowPlayer && (playerTransform.position.z - transform.position.z < 7f && playerTransform.position.z - transform.position.z >= -1.5f))
			{
//			print ("ontrigger");
			transform.Find ("opponentFatguy").GetComponent<Animation>().Play("death",PlayMode.StopAll);
			CancelInvoke("setPos");
			Invoke("setPos",0.5f);
			itselfCrashed=true;
			resetOnce=false;
			transform.GetComponent<Rigidbody>().velocity=Vector3.zero;
			transform.GetComponent<Rigidbody>().angularVelocity=Vector3.zero;
			}
			else
			{
				transform.position+=new Vector3(0,0,3f);
			}
			
		}
		if (col.tag.Equals ("rampStart")) {
//			print ("ontrigger "+ col.tag);
			transform.GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX;
			Invoke("resetConstraints",1f);
		}
	}

	void resetConstraints()
	{
		transform.GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
		transform.eulerAngles = new Vector3 (0,0,0);
	}
	void setPos()
	{
				//print ("setpos");
				if (!crash && !death) {
						itselfCrashed = false;

						resetOnce = true;
						if (oppoBikeID == 0)
								transform.Find ("opponentFatguy").GetComponent<Animation>().Play ("race", PlayMode.StopAll);
						else
								transform.Find ("opponentFatguy").GetComponent<Animation>().Play ("race", PlayMode.StopAll);
						transform.root.position += new Vector3 (0, 0, 1.5f);
						xPosition = extendedxPosition;
	
						transform.GetComponent<Rigidbody>().velocity += new Vector3 (0, 0, 50);
						if (oppoBikeID == 0) {
								transform.Find ("opponentFatguy").Find ("biker_fat-01").transform.GetComponent<Renderer>().material.mainTexture = biker [bikerCostume];
								transform.Find ("opponentFatguy").Find ("bike_body").transform.GetComponent<Renderer>().material.mainTexture = bike [bikeCostume];
						} else if (oppoBikeID == 1) {
								transform.Find ("opponentFatguy").Find ("biker-v3").transform.GetComponent<Renderer>().material.mainTexture = biker [bikerCostume];
								transform.Find ("opponentFatguy").Find ("bike_crb").transform.GetComponent<Renderer>().material.mainTexture = bike [bikeCostume];
			
						}
						if (bikerCostume < 4)
								bikerCostume += 1;
						else
								bikerCostume = 0;
		
		
						if (bikeCostume < 4)
								bikeCostume += 1;
						else
								bikeCostume = 0;
				}
		}
	void adopt_player_speed()
	{
		
		
		player_vel_magnitude = player.GetComponent<Rigidbody>().velocity.magnitude;
		own_vel_magnitude=GetComponent<Rigidbody>().velocity.magnitude;
		
		if (own_vel_magnitude> player_vel_magnitude  && !flag) 
		{
			if(wheel_r.motorTorque>0 && own_vel_magnitude>0)
			{
//								print ("slowing down");
				wheel_r.motorTorque -= 6.0f;
				own_vel_magnitude -= 2f;

			
				GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * own_vel_magnitude;
			}
		} 
		else 
		{
			flag=true;
			//print ("flag ");
			if(transform.position.z-player.transform.position.z <0.32f)//0.5
			{
//				print ("flag ");
				transform.position=new Vector3(transform.position.x,transform.position.y,player.transform.root.position.z);
				GetComponent<Rigidbody>().velocity=GetComponent<Rigidbody>().velocity.normalized*player_vel_magnitude*1.01f;//*0.99995f; //0.9992
			}
			else
			{
				//print ("else ");
				wheel_r.motorTorque =wheel_r_player.motorTorque;
				GetComponent<Rigidbody>().velocity=GetComponent<Rigidbody>().velocity.normalized*player_vel_magnitude*player_patchup_speed;
			}
		}
		
		if ((playerTransform.position.z - transform.position.z < -3f || playerTransform.position.z - transform.position.z >=3f) ) {
			xPosition =extendedxPosition;
			transform.position = new Vector3 (Mathf.Lerp (transform.position.x, xPosition, 0.1f), transform.position.y, transform.position.z);		
		} else  if (playerTransform.position.z - transform.position.z < 3f || playerTransform.position.z - transform.position.z >= -3f) {
			if (!avoidCar) {
				
				

				
				
				if(playerLeftSide)
				{
					//print ("left "+(playerTransform.position.x - transform.position.x));
					if(transform.position.x>=4.8f)
					{
						if (xOnce) {
							//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 25, 5 * Time.deltaTime));

							xPosition =playerTransform.position.x - 0.4f;
							xOnce = false;
							Invoke ("resetXonce", 1.5f);
						}
					}
					
					
					if (playerTransform.position.x - transform.position.x <= -1f ) {
					//	print ("left "+(playerTransform.position.x - transform.position.x));
						if (xOnce) {
							//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 25, 5 * Time.deltaTime));

							xPosition -= 0.4f;
							xOnce = false;
							Invoke ("resetXonce", 1.5f);//1.3
						}
						
					}
					else if (playerTransform.position.x - transform.position.x <= -0.6f ) {
						if (xOnce) {
							//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 25, 5 * Time.deltaTime));

							//print ("inside -3 "+ (playerTransform.position.x - transform.position.x));
							xPosition -= 0.15f;
							xOnce = false;
							Invoke ("resetXonce", 1f);//1.2
						}
						
					}
					else if (playerTransform.position.x - transform.position.x <= -0.53f) { 
						if (xOnce) {
							//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 25, 5 * Time.deltaTime));

							xPosition -= 0.1f;
							xOnce = false;
							Invoke ("resetXonce", 1f);
						}
						
					}
					else	if (playerTransform.position.x - transform.position.x < -0.49f && playerTransform.position.x - transform.position.x > -0.54f) { //-2.7
						if (xOnce) {
							//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 25, 5 * Time.deltaTime));

							//print ("opponent: "+(playerTransform.position.x - transform.position.x));
							//print ("inside -2.5 "+ (playerTransform.position.x - transform.position.x));
							xPosition -= 0.05f;
							xOnce = false;
							Invoke ("resetXonce", 1.0f); //1.2
						}
						
					}

					else	if (playerTransform.position.x - transform.position.x < -0.01f && playerTransform.position.x - transform.position.x > -0.3f) { //-2.7
						if (xOnce) {
							//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 25, 5 * Time.deltaTime));
							
							//print ("opponent: "+(playerTransform.position.x - transform.position.x));
							//print ("inside -2.5 "+ (playerTransform.position.x - transform.position.x));
							if((xPosition+0.1f)<=4.75f)
							{
								xPosition += 0.1f;
							xOnce = false;
							Invoke ("resetXonce", 1.0f); //1.2
							}
						}
						
					}
					else
					{
						//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, 0, 3 * Time.deltaTime));

					}
					
					
				}
				else if(playerRightSide)
				{
					//print ("right side: "+ (playerTransform.position.x - transform.position.x));
					if(transform.position.x>=4.5f)
					{
						if (xOnce) {
							//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z,-25, 5 * Time.deltaTime));

							xPosition =playerTransform.position.x - 0.4f;
							xOnce = false;
							Invoke ("resetXonce", 1.5f);
						}
					}
					if (playerTransform.position.x - transform.position.x >= 1f) { 
						if (xOnce) {
							//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, -25, 5 * Time.deltaTime));
						
							xPosition += 0.4f;
							xOnce = false;
							Invoke ("resetXonce", 1.5f);
						}
						
					}
					else if (playerTransform.position.x - transform.position.x >= 0.6f) { 
						if (xOnce) {
							//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, -25, 5 * Time.deltaTime));

							xPosition += 0.15f;
							xOnce = false;
							Invoke ("resetXonce", 1f);
						}
						
					}
					else if (playerTransform.position.x - transform.position.x >= 0.53f) { 
						if (xOnce) {
							//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, -25, 5 * Time.deltaTime));

							xPosition += 0.1f;
							xOnce = false;
							Invoke ("resetXonce", 1f);
						}
						
					}
					else if (playerTransform.position.x - transform.position.x > 0.47f && playerTransform.position.x - transform.position.x < 0.53f) { //-2.9
						if (xOnce) {
							//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, -25, 5 * Time.deltaTime));

							//print ("opponent: "+(playerTransform.position.x - transform.position.x));
							xPosition += 0.05f;
							xOnce = false;
							Invoke ("resetXonce", 1f);
						}
						
					}
					else if (playerTransform.position.x - transform.position.x > 0.01f && playerTransform.position.x - transform.position.x < 0.3f) { //-2.9
						if (xOnce) {
							//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z, -25, 5 * Time.deltaTime));
							
							//print ("opponent: "+(playerTransform.position.x - transform.position.x));
							if((xPosition-0.1f)>=0.95f)
							{
							xPosition -= 0.1f;
							xOnce = false;
							Invoke ("resetXonce", 1f);
							}else
								xPosition=extendedxPosition;
						}
						
					}
					else
					{
						//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle (transform.eulerAngles.z,0, 5 * Time.deltaTime));

					}
					
				}
				
				
				
				
				
				transform.position = new Vector3 (Mathf.Lerp (transform.position.x, xPosition, 0.1f), transform.position.y, transform.position.z);		
				
				
				
				
				
				
				
			}
		}

		
		if (levelID != 5) {
						if (transform.position.y < -0.1f || transform.position.y >= 2f) {
								transform.position = new Vector3 (xPosition, 0.08f, transform.position.z);
						}
				} else {
			if (transform.position.y < -0.1f || transform.position.y >= 7f) {
				transform.position = new Vector3 (xPosition, 0.08f, transform.position.z);
			}
		}
	}
	
	
	
	void resetXonce()
	{
		xOnce=true;
	}
	
	
	
	GameObject GetTarget(){
		
		GameObject[] gos ;
	

		 if(levelID==12)
			gos = GameObject.FindGameObjectsWithTag ("Mine");
		else if(levelID==11)
			gos = GameObject.FindGameObjectsWithTag ("smallMine");
		else
			gos = GameObject.FindGameObjectsWithTag ("newLevelHurdle");
				
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach(GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance && go.transform.position.z> gameObject.transform.position.z) {
				closest = go;
				distance = curDistance;
				
			}
			
		}
		

		return closest;
		
		
	}
	GameObject GetRamp(){
		
		GameObject[] gos ;
		
		
		gos = GameObject.FindGameObjectsWithTag ("rampStart");
		
		
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach(GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance && go.transform.position.z> gameObject.transform.position.z) {
				closest = go;
				distance = curDistance;
				
			}
			
		}
		
		
		return closest;
		
		
	}
}
