using UnityEngine;
using System.Collections;

public class followPlayer : MonoBehaviour {
	public float originlaXPosition;
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
	void Start () {
		bikeCostume = bikerCostume = 0;
		playerLeftSide = playerRightSide =playerAhead= false;
		layerMask = 1 << 9;
		
		avoidCar = false;
		FollowPlayer = false;
		isCrashed = false;
		
		xPosition = -3.8f;
		xOnce = true;
		itselfCrashed = false;
		player = GameObject.FindWithTag ("Player");
		print (player.name);
		death = false;
		childPlayer = player.GetComponent<heavyBikeTurns> ().player.transform;
		GetComponent<Rigidbody>().centerOfMass=new Vector3(com.localPosition.x,com.localPosition.y,com.localPosition.z);
		flag = false;
		skipClick = false;
		//player_patchup_speed = 0.7f;
		player_patchup_speed = 0.75f;
		
		StartCoroutine ("startFollow");
		
	}
	
	IEnumerator startFollow()
	{
		yield return new WaitForSeconds(5f);
		FollowPlayer = true;
		transform.position=new Vector3(originlaXPosition,transform.position.y, player.transform.position.z-400f);
		//transform.position=new Vector3(-3.8f,transform.position.y, player.transform.position.z-5f);
	}
	void Update () {
		
		
		
		
		
		if(!avoidCar)
		{
			
			if(playerAhead)
			{
				if(!tiltControl.twoBiker)
				{
					//print (FollowPlayer +"playerAhead   "+ ( transform.position.x));
					if(transform.position.z-playerTransform.position.z<=-2f ||  transform.position.z-playerTransform.position.z>=2f )
					{
						
						if (Physics.Raycast (transform.position + new Vector3 (0.6f, 1f, 0.0f), Vector3.right, out hit,4f)) {
							
							if (hit.collider.tag.Contains("Player") || hit.collider.tag.Contains("car")) {
								xPosition=playerTransform.position.x+2.25f;
							}
							else
								xPosition=playerTransform.position.x-2.25f;
						}
						else
						{
							if (transform.position.x > 6f) { //3.75
								xPosition=playerTransform.position.x-2.25f;
								//xPosition -= 2f;
								
							}
							else if (transform.position.x < 6f && transform.position.x >-1.5f ) { //3.75
								xPosition=playerTransform.position.x+2.25f;
								//xPosition += 2f;
								
							}
							else if(transform.position.x <-1.5f)
							{
								xPosition=playerTransform.position.x+2.25f;
								//xPosition += 2f;
							}
						}
					}
				}
				else if(tiltControl.twoBiker)
				{
					//				print (FollowPlayer +"playerAhead   "+ ( transform.position.x));
					if(transform.position.z-playerTransform.position.z<=-2f ||  transform.position.z-playerTransform.position.z>=2f )
					{
						
						if(playerTransform.position.x<=-3.5f)
						{
							xPosition=-5f;
						}
						else if(playerTransform.position.x<=-1.5f)
						{
							xPosition=-5f;
						}
						else
						{
							
							if (transform.position.x > 7.75f) { //3.75
								xPosition=playerTransform.position.x-2.25f;
								//xPosition -= 2f;
								
							}
							else if (transform.position.x < 7.75f && transform.position.x >-1.5f ) { //3.75
								xPosition=playerTransform.position.x-2.25f;
								//xPosition += 2f;
								
							}
							else if(transform.position.x <-1.5f)
							{
								xPosition=playerTransform.position.x-0.5f;
								//xPosition += 2f;
							}
							else if(transform.position.x <-3.75f)
							{
								xPosition=playerTransform.position.x+2.25f;
								//xPosition += 2f;
							}
							
						}
					}
				}
				
			}
			
			transform.position = new Vector3 (Mathf.Lerp (transform.position.x, xPosition, 0.1f), transform.position.y, transform.position.z);		
			
		}
		
		
		if (transform.position.y < -0.1f || transform.position.y>=2f) {
			transform.position = new Vector3 ( xPosition,0.1f, transform.position.z);
		}
		
		
		
		
		targetObj = GetTarget ();
		if (targetObj != null) {
			
			
			
			dist = Vector3.Distance (targetObj.transform.position, transform.position);
			
			
			if (dist > 0 && dist <= 35) {
				if ((transform.position.x - targetObj.transform.position.x) <= 2f && (transform.position.x - targetObj.transform.position.x) >= 0f) {
					if(!avoidCar)
					{
						
						//		print (">=0");
						//					print	(playerTransform.position.x - transform.position.x );
						avoidCar=true;
						
						//xPosition=transform.position.x+2f;
						
						
						//Debug.DrawRay(transform.position + new Vector3 (0.6f, 1f, 0.0f), Vector3.right*3,Color.cyan);
						
						if (Physics.Raycast (transform.position + new Vector3 (0.6f, 1f, 0.0f), Vector3.right, out hit,3f,layerMask)) {
							
							if (hit.collider.tag.Contains("Player")) {
								
								xPosition=targetObj.transform.position.x-2.25f;
								
								
							}
							else
							{
								
								xPosition=targetObj.transform.position.x+2.25f;
								
							}
						}
						else
						{
							
							xPosition=targetObj.transform.position.x+2.25f;
						}
					}
					
				}
				if ((transform.position.x - targetObj.transform.position.x) < 0f && (transform.position.x - targetObj.transform.position.x) >= -2f) {
					if(!avoidCar)
					{
						
						avoidCar=true;
						//						print ("<0");
						
						//Debug.DrawRay(transform.position + new Vector3 (0.6f, 1f, 0.0f), Vector3.left*3,Color.grey);
						if (Physics.Raycast (transform.position + new Vector3 (0.6f, 1f, 0.0f), Vector3.left, out hit,3f,layerMask)) {
							
							if (hit.collider.tag.Contains("Player")) {
								
								xPosition=targetObj.transform.position.x+2.25f;
								
								
							}
							else
							{
								
								xPosition=targetObj.transform.position.x-2.25f;
								
							}
						}
						else
						{
							
							xPosition=targetObj.transform.position.x-2.25f;
						}
					}
					
				}
				
			}
		}
		
		
		
		
		if (avoidCar) {
			transform.position = new Vector3 (Mathf.Lerp (transform.position.x, xPosition, 0.1f), transform.position.y, transform.position.z);	
			//			print ((transform.position.x-xPosition));
			if(transform.position.x-xPosition <0.5f && transform.position.x-xPosition >-0.5f)
			{
				avoidCar=false;
				
			}
		}
		
		
		
		
		if (childPlayer.name.Contains ("Player")) {
			crash = childPlayer.GetComponent<heavyBikeTurnControls> ().crash;		
			death = heavyBikeTurnControls.isdead;
		} else {
			crash = childPlayer.GetComponent<turnLevelcontrols> ().crash;
			death=turnLevelcontrols.isdead;
		}
		if (handleArrow.start && death && !itselfCrashed && !heavyBikeTurns.boostbool) {
			wheel_r.motorTorque = 30;
		}
		
		
		
		
		
		
		
		if (isCrashed) {
			isCrashed=false;	
			transform.root.position = new Vector3 (originlaXPosition,0.2f,transform.root.position.z);
			xPosition = originlaXPosition;
			
			
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
				//	xPosition = -3.8f;
				wheel_r.motorTorque =wheel_r_player.motorTorque;
				GetComponent<Rigidbody>().velocity=playerTransform.GetComponent<Rigidbody>().velocity;
				
			}
			
			
		} else if (crash &&!death) {
			if (resetOnce) {
				resetOnce = false;
				Invoke ("resetPosition", 5f);
			}
			
		} else if (itselfCrashed) {
			
			wheel_r.motorTorque = 0;
			if (resetOnce) {
				resetOnce = false;
				Invoke ("resetPosition", 5f);
			}
			
		} else if (heavyBikeTurns.boostbool) {
			if(wheel_r.motorTorque>15 && own_vel_magnitude >0 )
			{
				wheel_r.motorTorque -= 2.0f;
				own_vel_magnitude -= 0.5f;
				GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * own_vel_magnitude;
				xPosition=-4f;
				
				
			}
		}
		
		
		
	}
	
	
	void FixedUpdate()
	{
		if (childPlayer.name.Contains ("Player")) {
			
			skipClick=childPlayer.GetComponent<heavyBikeTurnControls>().skipClicked;
			
		}
		else
			skipClick=childPlayer.GetComponent<turnLevelcontrols>().skipClicked;
		
		if (skipClick) {
			
			CancelInvoke("resetPosition");
			resetPosition();	
			
		}
	}
	void resetPosition()
	{
		
		
		transform.root.position = new Vector3 (-5f,0.2f,player.transform.root.position.z-100);
		transform.root.GetComponent<Rigidbody>().velocity = Vector3.zero;
		transform.root.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		wheel_r.motorTorque = 0.0f;
		wheel_r.brakeTorque = 1000f;
		transform.Find ("opponentFatguy").Find ("biker_fat-01").transform.GetComponent<Renderer>().material.mainTexture = biker [bikerCostume];
		transform.Find ("opponentFatguy").Find ("bike_body").transform.GetComponent<Renderer>().material.mainTexture = bike [bikeCostume];
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
		if (col.transform.tag.Equals ("car")) {
			transform.Find ("opponentFatguy").GetComponent<Animation>().Play("death",PlayMode.StopAll);
			CancelInvoke("setPos");
			Invoke("setPos",3f);
			itselfCrashed=true;
			
			
		}
	}
	
	void setPos()
	{
		itselfCrashed=false;
		transform.Find ("opponentFatguy").GetComponent<Animation>().Play("race",PlayMode.StopAll);
		transform.root.position += new Vector3 (0,0,20);
		transform.Find ("opponentFatguy").Find ("biker_fat-01").transform.GetComponent<Renderer>().material.mainTexture = biker [bikerCostume];
		transform.Find ("opponentFatguy").Find ("bike_body").transform.GetComponent<Renderer>().material.mainTexture = bike [bikeCostume];
		if(bikerCostume<4)
			bikerCostume += 1;
		else
			bikerCostume =0;
		
		
		if(bikeCostume<4)
			bikeCostume += 1;
		else
			bikeCostume =0;
	}
	
	void adopt_player_speed()
	{
		
		
		player_vel_magnitude = player.GetComponent<Rigidbody>().velocity.magnitude;
		own_vel_magnitude=GetComponent<Rigidbody>().velocity.magnitude;
		
		if (own_vel_magnitude> player_vel_magnitude  && !flag) 
		{
			if(wheel_r.motorTorque>0 && own_vel_magnitude>0)
			{
				//				print ("slowing down");
				wheel_r.motorTorque -= 6.0f;
				own_vel_magnitude -= 2f;
				GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * own_vel_magnitude;
			}
		} 
		else 
		{
			flag=true;
			//print ((transform.position.z-player.transform.position.z));
			if(transform.position.z-player.transform.position.z <0.25f)
			{
				
				transform.position=new Vector3(transform.position.x,transform.position.y,player.transform.position.z);
				GetComponent<Rigidbody>().velocity=GetComponent<Rigidbody>().velocity.normalized*player_vel_magnitude*0.9992f;
			}
			else
			{
				
				wheel_r.motorTorque =wheel_r_player.motorTorque;
				GetComponent<Rigidbody>().velocity=GetComponent<Rigidbody>().velocity.normalized*player_vel_magnitude*player_patchup_speed;
			}
		}
		
		if (playerTransform.position.z - transform.position.z < -3f || playerTransform.position.z - transform.position.z >= 3f) {
			xPosition = originlaXPosition;
			transform.position = new Vector3 (Mathf.Lerp (transform.position.x, xPosition, 0.1f), transform.position.y, transform.position.z);		
		} else  if (playerTransform.position.z - transform.position.z < 3f || playerTransform.position.z - transform.position.z >= -3f) {
			if (!avoidCar) {
				
				
				
				
				
				if(playerLeftSide)
				{
					//print ((playerTransform.position.x - transform.position.x));
					if(transform.position.x>=8.8f)
					{
						if (xOnce) {
							xPosition =playerTransform.position.x - 2f;
							xOnce = false;
							Invoke ("resetXonce", 2f);
						}
					}
					
					
					if (playerTransform.position.x - transform.position.x <= -4f ) {
						if (xOnce) {
							xPosition -= 2f;
							xOnce = false;
							Invoke ("resetXonce", 1.5f);
						}
						
					}
					else if (playerTransform.position.x - transform.position.x <= -3f ) {
						if (xOnce) {
							xPosition -= 1f;
							xOnce = false;
							Invoke ("resetXonce", 1f);
						}
						
					}
					else	if (playerTransform.position.x - transform.position.x <= -2.2f && playerTransform.position.x - transform.position.x > -2.7f) { 
						if (xOnce) {
							xPosition -= 0.25f;
							xOnce = false;
							Invoke ("resetXonce", 1f);
						}
						
					}
					
					
				}
				else if(playerRightSide)
				{
					//print ("right side: "+ (playerTransform.position.x - transform.position.x));
					if(transform.position.x>=8.8f)
					{
						if (xOnce) {
							xPosition =playerTransform.position.x - 2f;
							xOnce = false;
							Invoke ("resetXonce", 2f);
						}
					}
					if (playerTransform.position.x - transform.position.x >= 4f) { 
						if (xOnce) {
							xPosition += 2f;
							xOnce = false;
							Invoke ("resetXonce", 1.5f);
						}
						
					}
					else if (playerTransform.position.x - transform.position.x >= 3f) { 
						if (xOnce) {
							xPosition += 1f;
							xOnce = false;
							Invoke ("resetXonce", 1f);
						}
						
					}
					else if (playerTransform.position.x - transform.position.x >= 2.2f && playerTransform.position.x - transform.position.x < 2.7f) { 
						if (xOnce) {
							xPosition += 0.25f;
							xOnce = false;
							Invoke ("resetXonce", 1f);
						}
						
					}
					
				}
				
				
				
				
				
				transform.position = new Vector3 (Mathf.Lerp (transform.position.x, xPosition, 0.1f), transform.position.y, transform.position.z);		
				
				
				
				
				
				
				
			}
		}
		//		if (playerTransform.position.x - transform.position.x >=2f) {
		//			transform.position = new Vector3 (Mathf.Lerp (transform.position.x, xPosition, 0.1f), transform.position.y, transform.position.z);		
		//
		//			
		//		}
		
		
		if (transform.position.y < -0.1f || transform.position.y>=2f) {
			transform.position = new Vector3 ( xPosition,0.1f, transform.position.z);
		}
		
	}
	
	
	
	void resetXonce()
	{
		xOnce=true;
	}
	
	
	
	GameObject GetTarget(){
		
		GameObject[] gos ;
		
		gos = GameObject.FindGameObjectsWithTag("car");
		
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
