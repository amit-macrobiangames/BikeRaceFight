using UnityEngine;
public class newLevelCrashed : MonoBehaviour {
	public PhycamViews mainCam;
	public AnimationClip survive;
	public Texture new9, new1, new0, new13;
	public Transform shadow,healthBar;
	public AnimationClip race;
	public AnimationClip unstable;
	bool playOnce;
	public Transform itself;
	public  int count;
	public Transform Player;
	public AudioClip crashSound;
	public int OppobikeNo=0;
	public AudioClip kickSound,punchSOund;
	public Transform opponent;
	public GameObject normal,weaponised;
	public bool isLeft;
	public heavyBikeTurnControls heavybikeScript;
	public turnLevelcontrols harleybikeScript;

	// Use this for initialization
	void Start () {
		countonce = true;
		count = 0;
		playOnce = true;
		Player= GameObject.FindGameObjectWithTag ("Player").transform;
		healthBar.transform.GetComponent<Renderer>().material.mainTexture= new13;	
		Player = Player.GetComponent<heavyBikeTurns> ().player.transform;
//		if (transform.root.name.Contains ("opponentTallguys")) {
//			OppobikeNo = 1;		
//		} else
//			OppobikeNo = 0; //left
	}
	
	public AudioClip batHit, AxeHit;
	public void opponentAttacked( string weaponName)
	{
		
		var oppoScript = itself.GetComponent<knockOutControl> ();

		if(scoreOnce)
		{
			if(weaponName.Contains("bat") || weaponName.Contains("Bat") )
			{
				heavyBikeTurnControls.score+=375;
				turnLevelcontrols.score+=375;
				heavyBikeTurns.opponentCrashedCash+=375;
				heavyBikeTurns.score+=375;
			}
			else if(weaponName.Contains("axe") || weaponName.Contains("pistol") || weaponName.Contains("missile"))
			{
				heavyBikeTurnControls.score+=1000;
				turnLevelcontrols.score+=1000;
				heavyBikeTurns.opponentCrashedCash+=1000;
				heavyBikeTurns.score+=1000;
			}
			else 
			{
				heavyBikeTurnControls.score+=166;
				turnLevelcontrols.score+=166;
				heavyBikeTurns.opponentCrashedCash+=166;
				heavyBikeTurns.score+=166;
			}
			//					print ("collider name"+ col.transform.name + "  score:"+ heavyBikeTurnControls.score);
			scoreOnce=false;
			
			
			
		}


//		print (weaponName);
		if (! oppoScript.crashed) {
			if(countonce)
			{

				if(Player.GetComponent<AudioSource>().enabled)
				{
					if (Player.GetComponent<Animation>().IsPlaying ("attack")) {
						
						Player.GetComponent<AudioSource>().PlayOneShot (kickSound);
					} else if (Player.GetComponent<Animation>().IsPlaying ("punch")) {
						Player.GetComponent<AudioSource>().PlayOneShot (punchSOund);
					}
					
				}
				oppoScript.unstability = true;
//				weaponised.SetActive(false);
//				normal.SetActive(true);

				itself.GetComponent<Animation>().Play (unstable.name, PlayMode.StopAll);
				Invoke ("unstableBike", unstable.length);

				count += 1;

				if(weaponName.Contains("axe")){
					if(Player.GetComponent<AudioSource>().enabled)
					{
						Player.GetComponent<AudioSource>().PlayOneShot (AxeHit);
					}

				}

				if(weaponName.Contains("bat") || weaponName.Contains("Bat") ){
					if(Player.GetComponent<AudioSource>().enabled)
				{
						Player.GetComponent<AudioSource>().PlayOneShot (batHit);
				}
					count+=1;
				}
				else if(weaponName.Contains("axe") || weaponName.Contains("pistol") || weaponName.Contains("shotgun") || weaponName.Contains("missile") )
					count+=2;
				countonce=false;	
				
				
			}
		}
//		print (weaponName +" count : "+count);
		if(count==1)
			healthBar.transform.GetComponent<Renderer>().material.mainTexture= new9;	
		
		else if(count==2)
			healthBar.transform.GetComponent<Renderer>().material.mainTexture= new1;	
		
		else if(count>=3)
			healthBar.transform.GetComponent<Renderer>().material.mainTexture= new0;	
		
		
		if (count >= 3) {
			
			
			if (playOnce) {


				if( !weaponName.Equals("pistol") &&  !weaponName.Equals("shotgun") && !weaponName.Contains("shotgun"))
				{
				
					mainCam.oppoCrashCam();
				}
				tiltControl.totalOppo-=1;
				endlessmodeGraphics.crashedOpponent+=1;
				if(weaponName.Contains("axe") )
					PlayerPrefs.SetInt("OpponentCrashedUsingAxe",(PlayerPrefs.GetInt ("OpponentCrashedUsingAxe")+1));
				else if(weaponName.Contains("pistol") )
					PlayerPrefs.SetInt("OpponentCrashedUsingPistol",(PlayerPrefs.GetInt ("OpponentCrashedUsingPistol")+1));
				else if(weaponName.Contains("shotgun") )
					PlayerPrefs.SetInt("OpponentCrashedUsingShotgun",(PlayerPrefs.GetInt ("OpponentCrashedUsingShotgun")+1));
				else
					PlayerPrefs.SetInt("totalOpponentCrashed",(PlayerPrefs.GetInt ("totalOpponentCrashed")+1));
				Invoke ("attacked", 3f);
				oppoScript.crashed = true;

					transform.root.GetComponent<newLevelHarley>().itselfCrashed=true;
				
					


				if (weaponName.Equals ("pistol")) {
//					print ((	transform.root.position.x));
					if(	transform.root.position.x<=1f)
					{
						//print ("<=-4");
						transform.root.GetComponent<newLevelHarley>().attackedThroughGun=true;
						transform.root.GetComponent<newLevelHarley>().extendedxPosition=1.2f;

						transform.root.position = new Vector3 (1.2f, transform.root.position.y, transform.root.position.z);
					}
					else if(	transform.root.position.x>=4.6f)
					{
						transform.root.GetComponent<newLevelHarley>().attackedThroughGun=true;
						transform.root.GetComponent<newLevelHarley>().extendedxPosition=4.5f;
						transform.root.position = new Vector3 (4.5f, transform.root.position.y, transform.root.position.z);
					}
					
				}
				
			}
			itself.GetComponent<Animation>().Play (survive.name, PlayMode.StopAll);
			Invoke("shadowsOn",0.5f);
			Invoke("shadowsOff",4.2f);
			
			
			if (PlayerPrefs.GetInt ("SoundOff") == 0) {
				GetComponent<AudioSource>().PlayOneShot (crashSound);
			}
			
			
			playOnce = false;
			count = 0;
			
		}
	}
	
	bool attacking,countonce;
	
	void shadowsOn()
	{
		shadow.gameObject.SetActive(true);
	}
	void shadowsOff()
	{
		shadow.gameObject.SetActive(false);
	}
	
	void unstableBike()
	{
		itself.GetComponent<knockOutControl> ().unstability = false;
		countonce = true;

		scoreOnce = true;

			transform.Find ("opponentFatguy").GetComponent<knockOutControl> ().attackOn ();

		
		
	}

	bool scoreOnce=true;
	void OnTriggerEnter(Collider col) {
				
			
			
		if (col.transform.tag.Equals ("PlayerAttack")) {
			if(col.transform.root.GetComponent<weaponAI>().isAttacking || heavybikeScript.isAttacking || harleybikeScript.isAttacking)
			{

		
			opponentAttacked(col.transform.name);

			}

						}
				
		}
	void attacked()
	{
		transform.root.GetComponent<newLevelHarley>().attackedThroughGun=false;
		countonce = true;
		count=0;
		shadow.gameObject.SetActive(false);
		itself.GetComponent<knockOutControl>().crashed = false;
		
		playOnce = true;
		itself.GetComponent<Animation>().Play (race.name,PlayMode.StopAll);
		if(PlayerPrefs.GetInt("levels")!=5 && PlayerPrefs.GetInt("levels")!=6)
		Invoke ("reset",3f);//4
		else
			Invoke ("reset",0.5f);
		if (!tiltControl.twoBiker) {
			if (OppobikeNo == 0) {
				transform.root.GetComponent<newLevelHarley> ().FollowPlayer = false;	
				opponent.transform.root.GetComponent<newLevelHarley> ().FollowPlayer = false;
				transform.root.position = new Vector3 (1f, transform.root.position.y, Player.root.position.z - 200);
				opponent.root.position = new Vector3 (4.7f, opponent.root.position.y, Player.root.position.z - 200);
			}
			if (OppobikeNo == 1) {
				transform.root.GetComponent<newLevelHarley> ().FollowPlayer = false;
				opponent.root.GetComponent<newLevelHarley> ().FollowPlayer = false;
				opponent.root.position = new Vector3 (1f, opponent.root.position.y, Player.root.position.z - 200);
				transform.root.position = new Vector3 (4.7f, transform.root.position.y, Player.root.position.z - 200);
			}
			
			
		} else {
			if (OppobikeNo == 0) {
				transform.root.GetComponent<newLevelHarley> ().FollowPlayer = false;	
				
				transform.root.position = new Vector3 (1f, transform.root.position.y, Player.root.position.z - 200);
				
			}
			if (OppobikeNo == 1) {
				transform.root.GetComponent<newLevelHarley> ().FollowPlayer = false;
				transform.root.position = new Vector3 (4.7f, transform.root.position.y, Player.root.position.z - 200);
			}

//			if (OppobikeNo == 0) {
//				transform.root.GetComponent<newLevelHarley> ().FollowPlayer = false;	
//				
//				transform.root.position = new Vector3 (1f, transform.root.position.y, Player.root.position.z - 200);
//				
//			}
//			if (OppobikeNo == 1) {
//				transform.root.GetComponent<newLevelHarley> ().FollowPlayer = false;
//				transform.root.position = new Vector3 (4.7f, transform.root.position.y, Player.root.position.z - 200);
//			}
		}
		if (OppobikeNo == 0) {
			transform.root.GetComponent<newLevelHarley> ().itselfCrashed = false;
			transform.root.GetComponent<newLevelHarley> ().isCrashed = true;
		}
		if (OppobikeNo == 1) {
			transform.root.GetComponent<newLevelHarley> ().itselfCrashed = false;
			transform.root.GetComponent<newLevelHarley>().isCrashed=true;
		}
		
		healthBar.transform.GetComponent<Renderer>().material.mainTexture= new13;	
	}
	
	
	void reset()
	{
		int random=Random.Range(0,10);
		
		
		
		
		
		//print ("position: "+ tiltControl.position + "/ "+ tiltControl.totalOppo);
		//		print ("position: "+tiltControl.position+ "/ "+ tiltControl.totalOppo);
	
			if (!tiltControl.twoBiker) {
				
					if (random % 2 == 0) {
						if (OppobikeNo == 0) {
							transform.root.GetComponent<newLevelHarley> ().FollowPlayer = true;	
							opponent.transform.root.GetComponent<newLevelHarley> ().FollowPlayer = false;
							transform.root.position = new Vector3 (1f, transform.root.position.y, Player.root.position.z - 5);
							opponent.root.position = new Vector3 (4.7f, opponent.root.position.y, Player.root.position.z - 200);
						}
						if (OppobikeNo == 1) {
							transform.root.GetComponent<newLevelHarley> ().FollowPlayer = false;
							opponent.root.GetComponent<newLevelHarley> ().FollowPlayer = true;
							opponent.root.position = new Vector3 (1f, opponent.root.position.y, Player.root.position.z - 5);
							transform.root.position = new Vector3 (4.7f, transform.root.position.y, Player.root.position.z - 200);
						}
					} else {
						if (OppobikeNo == 0) {
							transform.root.GetComponent<newLevelHarley> ().FollowPlayer = false;	
							opponent.root.GetComponent<newLevelHarley> ().FollowPlayer = true;	
							opponent.root.position = new Vector3 (4.7f, opponent.root.position.y, Player.root.position.z - 5);
							transform.root.position = new Vector3 (1f, transform.root.position.y, Player.root.position.z - 200);
						} else if (OppobikeNo == 1) {
							opponent.root.GetComponent<newLevelHarley> ().FollowPlayer = false;	
							transform.root.GetComponent<newLevelHarley> ().FollowPlayer = true;	
							transform.root.position = new Vector3 (4.7f, transform.root.position.y, Player.root.position.z - 5);
							opponent.root.position = new Vector3 (1f, opponent.root.position.y, Player.root.position.z - 200);
						}
					}

				
			} 

		else if (tiltControl.twoBiker) {
				

		
		
				
				
					transform.root.GetComponent<newLevelHarley> ().FollowPlayer = true;
				//	opponent.root.GetComponent<newLevelHarley> ().FollowPlayer = true;
					
					transform.root.position = new Vector3 (1f, transform.root.position.y, Player.root.position.z - 5);
				//	opponent.root.position = new Vector3 (4.7f, opponent.root.position.y, Player.root.position.z - 5);

		


//					random = Random.Range (0, 10);
//
//					if (random % 2 == 0) {
//						
//						if(OppobikeNo==0)
//					{
//						transform.root.GetComponent<newLevelHarley> ().FollowPlayer = true;
//						opponent.root.GetComponent<newLevelHarley> ().FollowPlayer = false;
//						
//						transform.root.position = new Vector3 (1f, transform.root.position.y, Player.root.position.z - 5);
//						opponent.root.position = new Vector3 (4.7f, opponent.root.position.y, Player.root.position.z - 200);
//					}
//					else if(OppobikeNo==1)
//					{
//						opponent.root.GetComponent<newLevelHarley> ().FollowPlayer = true;
//						transform.root.GetComponent<newLevelHarley> ().FollowPlayer = false;
//						
//						transform.root.position = new Vector3 (4.7f, transform.root.position.y, Player.root.position.z - 200);
//						opponent.root.position = new Vector3 (1f, opponent.root.position.y, Player.root.position.z - 5);
//					}
//						
//					}
//				else if (random % 2 == 1) {
//					
//					if(OppobikeNo==0)
//					{
//						transform.root.GetComponent<newLevelHarley> ().FollowPlayer = false;
//						opponent.root.GetComponent<newLevelHarley> ().FollowPlayer = true;
//						
//						transform.root.position = new Vector3 (1f, transform.root.position.y, Player.root.position.z - 200);
//						opponent.root.position = new Vector3 (4.7f, opponent.root.position.y, Player.root.position.z - 5);
//					}
//					else if(OppobikeNo==1)
//					{
//						opponent.root.GetComponent<newLevelHarley> ().FollowPlayer = false;
//						transform.root.GetComponent<newLevelHarley> ().FollowPlayer = true;
//						
//						transform.root.position = new Vector3 (4.7f, transform.root.position.y, Player.root.position.z - 5);
//						opponent.root.position = new Vector3 (1f, opponent.root.position.y, Player.root.position.z - 200);
//					}
//					
//				}
	
			}
	}
	
	
	
}
