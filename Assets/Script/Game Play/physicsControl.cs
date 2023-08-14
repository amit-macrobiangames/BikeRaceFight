using UnityEngine;
using System.Collections;

public class physicsControl : MonoBehaviour {
	
	public Transform itself;
	

	bool start;
	
	bool left;
	
	public Transform player;
	
	public Transform opponent;
	public  bool crashed,unstability;
	
	public AnimationClip punch;
	
	public AnimationClip idle;
	public AnimationClip attack;
	
	public AnimationClip race;
	
	
	private bool attackOnce;
	
	bool punchOnce;
	
	public bool isAttacking;
	
	
	public AudioClip kickSound;
	
	bool playerCrashed;
	bool callOnce;
	int layerMask;
	bool 	playOnce = true;

	int bikeNo;
	public Transform leg,hand;

	Vector3 dirRaycast;
	float distanceRange;
	turnLevelcontrols harleyBikescript;
	heavyBikeTurnControls heavyBikeScript;
	public Transform wheelFL,wheelRL;
	bool levelClear;
	void Start () {
		
		layerMask = 1 << 9;
		//layerMask = 1 << 8;
		//layerMask = ~layerMask;

		
		callOnce = true;
		startRaceAnim=false;
		crashed = false;
		unstability = false;
		isAttacking = false;
		
		playOnce = true;
		
		
		attackOnce = true;
		punchOnce = true;
		opponent.GetComponent<Animation>() [punch.name].layer = 1;
		opponent.GetComponent<Animation>() [attack.name].layer = 2;
		
		opponent.GetComponent<Animation>() [race.name].layer = 4;
		
		
		opponent.GetComponent<Animation>() [punch.name].speed = 2f;
		opponent.GetComponent<Animation>() [attack.name].speed = 2f;
		
		itself.GetComponent<Animation>().Play ("idle",PlayMode.StopAll);
	

			player=player.GetComponent<heavyBikeTurns>().player.transform;
			
		
			if (player.name.Contains ("Fat")) {
						bikeNo = 0;
			harleyBikescript=player.GetComponent<turnLevelcontrols>();
				} else if (player.name.Contains ("Player")) {
						bikeNo = 1;
			heavyBikeScript=player.GetComponent<heavyBikeTurnControls>();
				}
			
		
		
		
		if (itself.position.x < -1.0f)
			left = true;
		else 
			left = false;
		

		playername = "bikeParent";
		
		
		if (PlayerPrefs.GetInt ("SoundOff") == 0) 
			GetComponent<AudioSource>().enabled = true;
		else
			GetComponent<AudioSource>().enabled = false;


		if (itself.name.Contains ("heavybike endless")) {
			dirRaycast=Vector3.left;
		} else
			dirRaycast=Vector3.right;
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	bool startRaceAnim=false;
	
	void FixedUpdate()
	{

		if (!startRaceAnim) {

				start = heavyBikeTurns.startRace;
				
				
				

			
			
			
			
			
			if (!crashed && !isAttacking && !unstability && start && !startRaceAnim) {
				
				opponent.GetComponent<Animation>().Play (race.name, PlayMode.StopAll);
				startRaceAnim = true;
			}
			
			
			
		}
		if (crashed) {
			
		//	itself.root.rigidbody.angularVelocity = Vector3.zero;

			
			
		}
		

	}
	
	string playername;
	bool raycast;
	RaycastHit hit = new RaycastHit ();
	// Update is called once per frame
	void Update () {
	
		

		if (handleArrow.start && !crashed) {
			wheelFL.Rotate (0, 0, 700.0f * Time.deltaTime);
			wheelRL.Rotate (0, 0, 700.0f * Time.deltaTime);		
		}
			
	

			if (bikeNo == 0) {
						playerCrashed = harleyBikescript.crash;
						levelClear = turnLevelcontrols.levelClear;
				} else if (bikeNo == 1) {
						playerCrashed = heavyBikeScript.crash;
						levelClear = heavyBikeTurnControls.levelClear;
				}
			
	
		if (!playerCrashed && !crashed &&!levelClear) {

						if (isAttacking) {
							




								//		Debug.DrawRay (leg.position, dirRaycast * 0.5f, Color.red);

				if (Physics.Raycast (leg.position, dirRaycast, out hit, 0.5f,layerMask)) {
//					print (hit.collider.name);
					if(!player.root.GetComponent<heavyBikeTurns>().flyoverStart  && !player.root.GetComponent<heavyBikeTurns>().isJumping)
					{
					if (itself.name.Contains ("opponentTallguys")) {
						distanceRange=2.15f;
					}
					else
					{
						distanceRange=-1.78f;
					}
					if (hit.collider.transform.root.name.Contains ("bikeParent") && (transform.position.x-player.root.position.x<=distanceRange)) {
						if (callOnce) {
							if(	bikeNo==1)
									heavyBikeScript.CrashPlayer ();
							else
									harleyBikescript.crashPlayer();
							callOnce = false;
												}
										}
										
								}
				}
					
			}
				}
		if (!playerCrashed && !isAttacking && !crashed && !levelClear) {
			
						if (!player.root.GetComponent<heavyBikeTurns> ().flyoverStart  && !player.root.GetComponent<heavyBikeTurns> ().isJumping) {
								if (player.position.z - itself.position.z <= 10f && player.position.z - itself.position.z >= -10f) {
										raycast = true;
								} else {
										raycast = false;
								}
			
								if (left && raycast) {
				
			
				
					
				
					
			
										if (Physics.Raycast (opponent.transform.position + new Vector3 (0.6f, 1f, 0.0f), Vector3.right, out hit, 2f, layerMask)) {
												//	Debug.DrawRay (opponent.position, Vector3.right, Color.red);
												if (hit.collider.tag.Contains ("Player") || hit.collider.transform.root.gameObject.name.Contains (playername)) {
						
														int y = Random.Range (0, 1);
														if (y == 0) {
																isAttacking = true;
																if (attackOnce) {
																		attackOnce = false;
								
								
								
																		if (itself.localScale.x <= -1.5f) {
																				Vector3 theScale = itself.localScale;
																				theScale.x *= -1;
																				itself.localScale = theScale;
																		}
								
																		if (playOnce) {
																				if (GetComponent<AudioSource>().enabled)
																						GetComponent<AudioSource>().PlayOneShot (kickSound);
									
																				playOnce = false;
																		}
																		Invoke ("attackOn", 0.92f);
																		opponent.GetComponent<Animation>().Play (attack.name, PlayMode.StopAll);
								
																}
							
														} else if (y == 1) {
																isAttacking = true;
																if (punchOnce) {
																		punchOnce = false;
																		if (itself.localScale.x >= 1.5f) {
																				Vector3 theScale = itself.localScale;
																				theScale.x *= -1;
																				itself.localScale = theScale;
																		}
																		if (playOnce) {
																				if (GetComponent<AudioSource>().enabled)
																						GetComponent<AudioSource>().PlayOneShot (kickSound);
									
																				playOnce = false;
																		}
																		opponent.GetComponent<Animation>().Play (punch.name, PlayMode.StopAll);
																		Invoke ("punchOn", 1f);
																}
														}
												}
										} 
				
								} else if (!left && raycast) {
										RaycastHit hit = new RaycastHit ();
				
			
					
					
					

										if (Physics.Raycast (opponent.transform.position + new Vector3 (-0.6f, 1f, 0.0f), Vector3.left, out hit, 2f, layerMask)) {
												//	Debug.DrawRay (opponent.position + new Vector3 (-0.4f, 0.5f, 0.0f), Vector3.left, Color.red);
												if (hit.collider.tag.Contains ("Player") || hit.collider.transform.root.gameObject.name.Contains (playername)) {
														int z = Random.Range (0, 1);
														if (z == 0) {
																isAttacking = true;
																if (attackOnce) {
																		attackOnce = false;
								
																		if (itself.localScale.x >= 1.5f) {
																				Vector3 theScale = itself.localScale;
																				theScale.x *= -1;
																				itself.localScale = theScale;
																		}
																		if (playOnce) {
																				if (GetComponent<AudioSource>().enabled)
																						GetComponent<AudioSource>().PlayOneShot (kickSound);
									
																				playOnce = false;
																		}
								
																		Invoke ("attackOn", 0.92f);
																		opponent.GetComponent<Animation>().Play (attack.name, PlayMode.StopAll);
								
																}
							
							
														} else if (z == 1) {
							
																isAttacking = true;
																if (punchOnce) {
																		punchOnce = false;
								
								
																		if (itself.localScale.x <= -1.5f) {
																				Vector3 theScale = itself.localScale;
																				theScale.x *= -1;
																				itself.localScale = theScale;
																		}
								
																		if (playOnce) {
																				if (GetComponent<AudioSource>().enabled)
																						GetComponent<AudioSource>().PlayOneShot (kickSound);
									
																				playOnce = false;
																		}
																		opponent.GetComponent<Animation>().Play (punch.name, PlayMode.StopAll);
																		Invoke ("punchOn", 1f);
																}
														}
												}
										} 
				
				
								}
						}
		
				}
	}
	void attackOn()
	{
		playOnce = true;
		attackOnce=true;
		isAttacking = false;
		startRaceAnim = false;
		callOnce = true;
		//opponent.animation.Play (race.name,PlayMode.StopAll);
		
	}
	void punchOn()
	{
		
		playOnce = true;
		punchOnce=true;
		isAttacking = false;
		startRaceAnim = false;
		callOnce = true;
		//opponent.animation.Play (race.name,PlayMode.StopAll);
	}
	
	
	
	
	
	
	
	
}
