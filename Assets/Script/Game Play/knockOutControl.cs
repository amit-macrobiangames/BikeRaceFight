using UnityEngine;
using System.Collections;

public class knockOutControl : MonoBehaviour {

	public OpponentWeaponAI weaponScript;
	public Transform itself;
	
	
	bool start;
	
	public bool left;
	
	public Transform player;
	
	public Transform opponent;
	public  bool crashed,unstability;
	
	public AnimationClip punch;
	
	public AnimationClip idle;
	public AnimationClip attack;
	
	public AnimationClip race;
	
	
	 bool attackOnce;
	

	
	public bool isAttacking;
	
	
	public AudioClip kickSound;
	
	bool playerCrashed;
	bool callOnce;
	int layerMask;
	bool 	playOnce = true;
	
	int bikeNo;
	public Transform leg,hand,axe,bat;
	
	Vector3 dirRaycast;
	float distanceRange;
	turnLevelcontrols harleyBikescript;
	heavyBikeTurnControls heavyBikeScript;
	public Transform wheelFL,wheelRL;
	bool levelClear,axeAttack;
	bool weaponAttack;
	void Start () {

		weaponAttack = false;
		layerMask = 1 << 9;
	
		
		
		callOnce = true;
		startRaceAnim=false;
		crashed = false;
		unstability = false;
		isAttacking = false;
		
		playOnce = true;
		
		
		attackOnce = true;
	
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
		
		
		
		
		if (itself.position.x < 1.5f)
			left = true;
		else 
			left = false;
		
		
		playername = "bikeParent";
		
		
		if (PlayerPrefs.GetInt ("SoundOff") == 0) 
			GetComponent<AudioSource>().enabled = true;
		else
			GetComponent<AudioSource>().enabled = false;
		
		
		if (itself.name.Contains ("heavybike endless") || itself.name.Contains ("opponentTallguys") ) {
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
				
				
				if(axeAttack){


					/*             rightside opponent is raycasting         bat attacking                  */



					//Debug.DrawRay (axe.position, -Vector3.up * 0.5f, Color.green);
				if (Physics.Raycast (axe.position, -Vector3.up, out hit, 0.5f,layerMask)) {
//							print (hit.collider.transform.root.name);
					if(!player.root.GetComponent<heavyBikeTurns>().flyoverStart  && !player.root.GetComponent<heavyBikeTurns>().isJumping)
					{
						//print ("first axe: "+(transform.position.x-player.root.position.x)+ " "+ hit.collider.name);
						
						distanceRange=0.5f;
					
						
						if (hit.collider.transform.root.name.Contains ("bikeParent") && (transform.position.x-player.root.position.x<=distanceRange) && (transform.position.x-player.root.position.x>=-2.2f)) {
							if (callOnce) {
									//print ("bat attaacking");
								if(	bikeNo==1)
									{
										if(!heavyBikeScript.isAttacking &&  !player.root.GetComponent<weaponAI>().isAttacking)
										{
											player.root.GetComponent<heavyBikeTurns>().isShieldedFtn();
									heavyBikeScript.CrashPlayer ("axe");
										}
									}
								else
									{
										if(!harleyBikescript.isAttacking &&  !player.root.GetComponent<weaponAI>().isAttacking)
										{
											player.root.GetComponent<heavyBikeTurns>().isShieldedFtn();
									harleyBikescript.crashPlayer("axe");
										}
									}
								callOnce = false;
							}
						}
						
					}
				}



					//Debug.DrawRay (bat.position, -Vector3.up * 0.5f, Color.red);



					/*              leftside opponent is raycasting        axe attacking                   */



					if (Physics.Raycast (bat.position,-Vector3.up, out hit, 0.5f,layerMask)) {
					
						if(!player.root.GetComponent<heavyBikeTurns>().flyoverStart && !player.root.GetComponent<heavyBikeTurns>().isJumping)
						{
							
							//print ("second axe: "+(transform.position.x-player.root.position.x)+ " "+ hit.collider.name);


							distanceRange=0.5f;
							
							
							
							if (hit.collider.transform.root.name.Contains ("bikeParent") && (transform.position.x-player.root.position.x<=distanceRange)&& (transform.position.x-player.root.position.x>=-2.2f)) {
								if (callOnce) {
//									print ("axe attacking");
									if(	bikeNo==1)
									{
										if(!heavyBikeScript.isAttacking &&  !player.root.GetComponent<weaponAI>().isAttacking )
										{
											player.root.GetComponent<heavyBikeTurns>().isShieldedFtn();
											heavyBikeScript.CrashPlayer ("bat");
										}
									}
									else
									{
										if(!harleyBikescript.isAttacking &&  !player.root.GetComponent<weaponAI>().isAttacking)
										{
											player.root.GetComponent<heavyBikeTurns>().isShieldedFtn();
											harleyBikescript.crashPlayer ("bat");
										}
									}
									
									callOnce = false;
								}
							}
							
						}
					}
			}
				
				else{
					
					
					/*             rightside opponent is raycasting         bat attacking                  */
					
					
					
					//Debug.DrawRay (axe.position, -Vector3.up * 0.5f, Color.green);
					if (Physics.Raycast (axe.position, -Vector3.up, out hit, 0.5f,layerMask)) {
						//							print (hit.collider.transform.root.name);
						if(!player.root.GetComponent<heavyBikeTurns>().flyoverStart  && !player.root.GetComponent<heavyBikeTurns>().isJumping)
						{
//							print ("first bat: "+(transform.position.x-player.root.position.x)+ " "+ hit.collider.name);
							
							distanceRange=0.5f;
							
							
							if (hit.collider.transform.root.name.Contains ("bikeParent") && (transform.position.x-player.root.position.x<=distanceRange) && (transform.position.x-player.root.position.x>=-2.2f)) {
								if (callOnce) {
									//print ("bat attaacking");
									if(	bikeNo==1)
									{
										if(!heavyBikeScript.isAttacking &&  !player.root.GetComponent<weaponAI>().isAttacking)
										{
											player.root.GetComponent<heavyBikeTurns>().isShieldedFtn();
											heavyBikeScript.CrashPlayer ("bat");
										}
									}
									else
									{
										if(!harleyBikescript.isAttacking &&  !player.root.GetComponent<weaponAI>().isAttacking)
										{
											player.root.GetComponent<heavyBikeTurns>().isShieldedFtn();
											harleyBikescript.crashPlayer("bat");
										}
									}
									callOnce = false;
								}
							}
							
						}
					}
					
					
					
					//Debug.DrawRay (bat.position, -Vector3.up * 0.5f, Color.red);
					
					
					
					/*              leftside opponent is raycasting        axe attacking                   */
					
					
					
					if (Physics.Raycast (bat.position,-Vector3.up, out hit, 0.5f,layerMask)) {
						
						if(!player.root.GetComponent<heavyBikeTurns>().flyoverStart && !player.root.GetComponent<heavyBikeTurns>().isJumping)
						{
							
							//print ("second bat : "+(transform.position.x-player.root.position.x)+ " "+ hit.collider.name);
							
							
							distanceRange=0.5f;
							
							
							
							if (hit.collider.transform.root.name.Contains ("bikeParent") && (transform.position.x-player.root.position.x<=distanceRange)&& (transform.position.x-player.root.position.x>=-2.2f)) {
								if (callOnce) {
									//									print ("axe attacking");
									if(	bikeNo==1)
									{
										if(!heavyBikeScript.isAttacking &&  !player.root.GetComponent<weaponAI>().isAttacking )
										{
											player.root.GetComponent<heavyBikeTurns>().isShieldedFtn();
											heavyBikeScript.CrashPlayer ("bat");
										}
									}
									else
									{
										if(!harleyBikescript.isAttacking &&  !player.root.GetComponent<weaponAI>().isAttacking)
										{
											player.root.GetComponent<heavyBikeTurns>().isShieldedFtn();
											harleyBikescript.crashPlayer ("bat");
										}
									}
									
									callOnce = false;
								}
							}
							
						}
					}
				}
				
					//Debug.DrawRay (leg.position, dirRaycast * 0.1f, Color.red);
				
				if (Physics.Raycast (leg.position, dirRaycast, out hit, 0.18f,layerMask)) {
							//		print (hit.collider.transform.root.name);
					if(!player.root.GetComponent<heavyBikeTurns>().flyoverStart  && !player.root.GetComponent<heavyBikeTurns>().isJumping)
					{

							distanceRange=0.43f;

	
						if (hit.collider.transform.root.name.Contains ("bikeParent") && (transform.position.x-player.root.position.x<=distanceRange) && (transform.position.x-player.root.position.x>=-0.45f)) {
							if (callOnce) {
								if(	bikeNo==1 )
								{
									if(!heavyBikeScript.isAttacking &&  !player.root.GetComponent<weaponAI>().isAttacking)
									{
										player.root.GetComponent<heavyBikeTurns>().isShieldedFtn();
									heavyBikeScript.CrashPlayer ();
									}
								}
								else
								{
									if(!harleyBikescript.isAttacking &&  !player.root.GetComponent<weaponAI>().isAttacking)
									{
										player.root.GetComponent<heavyBikeTurns>().isShieldedFtn();
									harleyBikescript.crashPlayer();
									}
								}
								callOnce = false;
							}
						}
						
					}
				}
		//Debug.DrawRay (leg.position, -dirRaycast * 0.2f, Color.cyan);
				if (Physics.Raycast (leg.position,-dirRaycast, out hit, 0.18f,layerMask)) {
					//print (hit.collider.transform.root.name);
					if(!player.root.GetComponent<heavyBikeTurns>().flyoverStart  && !player.root.GetComponent<heavyBikeTurns>().isJumping)
					{
						

							distanceRange=0.43f;
//					print ("leg second: "+(transform.position.x-player.root.position.x));
						

						if (hit.collider.transform.root.name.Contains ("bikeParent") && (transform.position.x-player.root.position.x<=distanceRange)&& (transform.position.x-player.root.position.x>=-0.45f)) {
							if (callOnce) {


								if(	bikeNo==1)
								{
									if(!heavyBikeScript.isAttacking &&  !player.root.GetComponent<weaponAI>().isAttacking)
									{
										player.root.GetComponent<heavyBikeTurns>().isShieldedFtn();
									heavyBikeScript.CrashPlayer ();
									}
								}
								else
								{
									if(!harleyBikescript.isAttacking &&  !player.root.GetComponent<weaponAI>().isAttacking)
									{
										player.root.GetComponent<heavyBikeTurns>().isShieldedFtn();
									harleyBikescript.crashPlayer();
									}
								}

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
			


				if (left && raycast) {//itself: left side
					
					
					
					
					
					
				
					if (Physics.Raycast (opponent.transform.position + new Vector3 (0.1f, 0.2f, 0.0f), Vector3.right, out hit, 0.35f, layerMask)) {
					
						if (hit.collider.tag.Contains ("Player") || hit.collider.transform.root.gameObject.name.Contains (playername)) {
							


								
								if (attackOnce) {
								isAttacking = true;
									attackOnce = false;

								if(weaponAttack)
								{
								weaponScript.playerAxeRightAttack();
								axeAttack=true;
								}
								else
								{
									
									if (itself.localScale.x <= -1.0f) {
										Vector3 theScale = itself.localScale;
										theScale.x *= -1;
										itself.localScale = theScale;
									}
									
									if (playOnce) {
										if (GetComponent<AudioSource>().enabled)
											GetComponent<AudioSource>().PlayOneShot (kickSound);
										
										playOnce = false;
									}
									Invoke ("attackOn", 0.916f);
									opponent.GetComponent<Animation>().Play (attack.name, PlayMode.StopAll);
									
								}
							}
							
						}
					} 
					
				} else if (!left && raycast) {
					RaycastHit hit = new RaycastHit ();
					
					
					
					
					
					//Debug.DrawRay (opponent.position + new Vector3 (-0.1f, 0.2f, 0.0f), Vector3.left*0.35f, Color.gray);
					if (Physics.Raycast (opponent.transform.position + new Vector3 (-0.1f, 0.2f, 0.0f), Vector3.left, out hit, 0.35f, layerMask)) {
							//Debug.DrawRay (opponent.position + new Vector3 (-0.6f, 1f, 0.0f), Vector3.left*2, Color.red);
					//	print (hit.collider.tag+ " "+ hit.collider.name+ " "+ hit.collider.transform.root.name);
						if (hit.collider.tag.Contains ("Player") || hit.collider.transform.root.gameObject.name.Contains (playername)) {



								if (attackOnce) {
								isAttacking = true;
									attackOnce = false;
								if(weaponAttack)
								{
								weaponScript.playerBatLeftAttack();
								axeAttack=false;
								}
								else
								{

									if (itself.localScale.x >= 1.0f) {
										Vector3 theScale = itself.localScale;
										theScale.x *= -1;
										itself.localScale = theScale;
									}
									if (playOnce) {
										if (GetComponent<AudioSource>().enabled)
											GetComponent<AudioSource>().PlayOneShot (kickSound);
										
										playOnce = false;
									}
									
									Invoke ("attackOn", 0.916f);
									opponent.GetComponent<Animation>().Play (attack.name, PlayMode.StopAll);
								}
								}
								
								
							
						}
					} 
					
					
				}
			}
			
		}
	}


	public void attackOn()
	{

		isAttacking = false;

		Invoke ("resetBools",0.3f);


	}

	void resetBools()
	{
		playOnce = true;
		attackOnce=true;
		startRaceAnim = false;
		callOnce = true;
		axeAttack=false;
	}
	
	
	
	
	
	
	
}
