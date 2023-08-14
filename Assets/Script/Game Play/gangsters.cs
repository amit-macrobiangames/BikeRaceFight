using UnityEngine;
using System.Collections;

public class gangsters : MonoBehaviour {
	public Transform itself,ragdollprefab;
	
	
	private Transform ragdoll,ragdoll_rigid;
	public AnimationClip axeAttack,idle;
	bool isWalking,collideOnce;
	float forceSpeed;
	int levelID;

	public Transform scorePopup;
	Transform player;
	private bool scoreOnce=true;
	
	bool playerDeath,playerCrash;
	public static bool firstAttackBike=true,firstAttackWalk=true;
	public AudioClip hitSound;



	int bikeID;
	public turnLevelcontrols harleyBike;
	public heavyBikeTurnControls heavyBike;
	// Use this for initialization
	void Start () {
	
		playOnce = true;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		player = player.GetComponent<heavyBikeTurns> ().player.transform;
	
	
		isWalking=collideOnce = true;
	
		
		if (bikeSelection.bikeCount == 2 || bikeSelection.bikeCount == 1) {
			forceSpeed=10f;	
			
		}
		else if (bikeSelection.bikeCount == 4 || bikeSelection.bikeCount == 3 || bikeSelection.bikeCount == 7) {
			forceSpeed=35f;
			
			
		}
		else if (bikeSelection.bikeCount == 5 || bikeSelection.bikeCount == 6 ||bikeSelection.bikeCount == 8 ) {
			forceSpeed=40f;
			
			
		}
		else if (bikeSelection.bikeCount == 9 ) {
			forceSpeed=45f;
			
		}
		else if (bikeSelection.bikeCount == 10 ) {
			
			forceSpeed=50f;
		}
		
		
		

		GetComponent<Animation>() [axeAttack.name].speed =1.25f;

		
		
		
		if (player.name.Contains ("Fat")) {
			levelID = 0;
		
		} else if (player.name.Contains ("Player")) {
			levelID=1;

		}
	


		
	
	
	}

	bool playOnce=true;//moveLeft=false;
	string gameMode;
	//RaycastHit hit = new RaycastHit ();

	// Update is called once per frame
	void Update () {
		
		//	print ("hello : "+Vector3.Distance(itself.position,player.position)+ " "+ itself.name);
//		if(Vector3.Distance(itself.position,player.position)<=1.5f)
//		{
//			//print ("inside");
//			Death();
//		}
//	
	
		
	
		
		
		
		if (levelID == 0) {
			
			playerDeath = turnLevelcontrols.isdead;
			playerCrash = harleyBike.crash;
			
		}
		if (levelID == 1) {
			
			playerDeath = heavyBikeTurnControls.isdead;
			playerCrash = heavyBike.crash;
			
		}

		gameMode = endlessmodeGraphics.gameMode;
		if (!playerCrash && !playerDeath && !gameMode.Equals("Pause")) {

			
		
				if (isWalking)
					itself.GetComponent<Animation>().Play (idle.name, PlayMode.StopAll);
				
			 
			
			
			
			
		
				
					if ((itself.position.z - player.position.z) < 10f) {
						if (playOnce) {
							
							isWalking = false;
							Invoke ("attackEnd", 0.667f);
							GetComponent<Animation>().Play (axeAttack.name, PlayMode.StopAll);
							playOnce = false;
						}
					}
				
					
				
				
			

			
			
		} else if (playerCrash && !gameMode.Equals("Pause")) {
			if(!playerDeath)
			{
				Time.timeScale=1;
				Time.fixedDeltaTime=0.02f;

				playOnce=true;
				
//				if(moveLeft)
//				{
//					Invoke("leftOff",1.5f);
//					itself.Translate (Vector3.left * 2f * Time.fixedDeltaTime);
//				}
			
					if (isWalking)
						itself.GetComponent<Animation>().Play (idle.name, PlayMode.StopAll);
					
				
			
					
//					//	Debug.DrawRay (itself.transform.position + new Vector3 (0.0f, 0.5f, 0.0f), Vector3.back*20f, Color.red);
//					if (Physics.Raycast (itself.transform.position + new Vector3 (0.0f, 0.5f, 0.0f), Vector3.back, out hit, 20f, layerMask)) {
//						//					print (hit.collider.name+ " : "+ hit.collider.tag);
//						if (hit.collider.tag.Contains ("Player") || hit.collider.transform.root.GetChild(0).tag.Equals("Player") ) {
//							//					print ("player");
//							moveLeft=true;
//							
//						}
//					}
					
					
			}
		}
	}
	bool playingStuntAnim;
	public void gangsterDeath()
	{


		if (collideOnce ) {

			collideOnce = false;
			player.GetComponent<AudioSource>().PlayOneShot(hitSound);
			itself.GetComponent<CapsuleCollider>().enabled=false;
			ragdoll = (Transform)Instantiate (ragdollprefab, itself.transform.position + new Vector3 (0f, 0.5f, 2f), Quaternion.identity);
			ragdoll.Rotate (0, 90, 0);
			ragdoll_rigid = ragdoll.Find ("Bip01").transform;
			ragdoll_rigid.GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, forceSpeed);
			ragdoll_rigid.GetComponent<Rigidbody>().AddForce (new Vector3 (0, 0, forceSpeed), ForceMode.Force);//=-1*gameObject.rigidbody.velocity;
			gameObject.GetComponent<Rigidbody>().AddForce (new Vector3 (0, 0, forceSpeed), ForceMode.Force);
			
			gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
			gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			
			//					ragdoll.active = true;
			
		
				ragdoll.Find ("biker_fat-022").transform.GetComponent<Renderer>().material.mainTexture = itself.Find ("biker_fat-022").transform.GetComponent<Renderer>().material.mainTexture;
				itself.Find ("biker_fat-022").gameObject.SetActive (false);
			
			
			
			
			if (scoreOnce) {
				scoreOnce=false;
				if (levelID==1) {
					
					heavyBikeTurnControls.score += 50;
					scorePopup.GetComponent<TextMesh>().text = "+50";
					scorePopup.GetComponent<TextMesh>().color=new Color(253,246,0,255);
					Instantiate(scorePopup,itself.transform.position+new Vector3(0f,2f,6f),Quaternion.identity);
					
					
				}
				else if (levelID==0) {
					scorePopup.GetComponent<TextMesh>().text = "+50";
					scorePopup.GetComponent<TextMesh>().color=new Color(253,246,0,255);
					Instantiate(scorePopup,itself.transform.position+new Vector3(0f,2f,6f),Quaternion.identity);//Quaternion.Euler (25,0, 0f));
					turnLevelcontrols.score += 50;
					
				}
				
				
				
			}	
			
			Destroy(itself.gameObject);
		}
	}
//	void OnCollisionEnter(Collision col)
//	{
//		//print (col.gameObject.name+ " "+ col.gameObject.tag+ " "+ col.transform.root.name);
//		if (col.gameObject.tag.Equals ("Player") || col.transform.root.name.Contains(playerParentName)) {
//			Death();
//			
//			
//			
//		}
//	}
	

	void attackEnd()
	{
		isWalking = true;
	}
	
//	void leftOff()
//	{
//		moveLeft = false;
//	}
}
