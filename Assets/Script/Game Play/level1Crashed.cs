using UnityEngine;
using System.Collections;

public class level1Crashed : MonoBehaviour {
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
	// Use this for initialization
	void Start () {
		countonce = true;
		count = 0;
		playOnce = true;
		Player= GameObject.FindGameObjectWithTag ("Player").transform;
		healthBar.transform.GetComponent<Renderer>().material.mainTexture= new13;	
		Player = Player.GetComponent<heavyBikeTurns> ().player.transform;

	}
	
	
	public void opponentAttacked()
	{
		
		var oppoScript = itself.GetComponent<knockOutControl> ();
		
		
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
				
				countonce=false;	
				
				
			}
		}
		if(count==1)
			healthBar.transform.GetComponent<Renderer>().material.mainTexture= new9;	
		
		else if(count==2)
			healthBar.transform.GetComponent<Renderer>().material.mainTexture= new1;	
		
		else if(count==3)
			healthBar.transform.GetComponent<Renderer>().material.mainTexture= new0;	
		
		
		if (count >= 3) {
			
			
			if (playOnce) {
				tiltControl.totalOppo-=1;
				endlessmodeGraphics.crashedOpponent+=1;
				Invoke ("attacked", 3f);
				oppoScript.crashed = true;
			
					transform.root.GetComponent<level1Harley>().itselfCrashed=true;
					
					
					
			
				
			}
			itself.GetComponent<Animation>().Play (survive.name, PlayMode.StopAll);
			Invoke("shadowsOn",1f);
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
		
		
	
			transform.Find ("opponentFatguy").GetComponent<knockOutControl> ().attackOn ();
	
		
		
	}
	
	
	//	void OnTriggerEnter(Collider col) {
	//				
	//			
	//			
	//		if (col.transform.tag.Equals ("PlayerAttack")) {
	//			opponentAttacked();
	//
	//						}
	//				
	//		}
	void attacked()
	{
		countonce = true;
		count=0;
		shadow.gameObject.SetActive(false);
		itself.GetComponent<knockOutControl>().crashed = false;
		
		playOnce = true;
		itself.GetComponent<Animation>().Play (race.name,PlayMode.StopAll);
		Invoke ("reset",5f);
		if (!tiltControl.twoBiker) {
			if (OppobikeNo == 0) {
				transform.root.GetComponent<level1Harley> ().FollowPlayer = false;	
				opponent.transform.root.GetComponent<level1Harley> ().FollowPlayer = false;
				transform.root.position = new Vector3 (-5f, transform.root.position.y, Player.root.position.z - 200);
				opponent.root.position = new Vector3 (9.4f, opponent.root.position.y, Player.root.position.z - 200);
			}
			if (OppobikeNo == 1) {
				transform.root.GetComponent<level1Harley> ().FollowPlayer = false;
				opponent.root.GetComponent<level1Harley> ().FollowPlayer = false;
				opponent.root.position = new Vector3 (-5f, opponent.root.position.y, Player.root.position.z - 200);
				transform.root.position = new Vector3 (9.4f, transform.root.position.y, Player.root.position.z - 200);
			}
			
			
		} else {
			if (OppobikeNo == 0) {
				transform.root.GetComponent<level1Harley> ().FollowPlayer = false;	
				
				transform.root.position = new Vector3 (-5f, transform.root.position.y, Player.root.position.z - 200);
				
			}
			if (OppobikeNo == 1) {
				transform.root.GetComponent<level1Harley> ().FollowPlayer = false;
				transform.root.position = new Vector3 (9f, transform.root.position.y, Player.root.position.z - 200);
			}
		}

	
			transform.root.GetComponent<level1Harley> ().itselfCrashed = false;
			transform.root.GetComponent<level1Harley>().isCrashed=true;

		
		healthBar.transform.GetComponent<Renderer>().material.mainTexture= new13;	
	}
	
	
	void reset()
	{
		int random=Random.Range(0,10);
		
		
		
		
		
	
			if (!tiltControl.twoBiker) {

					if (random % 2 == 0) {
						if (OppobikeNo == 0) {
							transform.root.GetComponent<level1Harley> ().FollowPlayer = true;	
							opponent.transform.root.GetComponent<level1Harley> ().FollowPlayer = false;
							transform.root.position = new Vector3 (-5f, transform.root.position.y, Player.root.position.z - 5);
							opponent.root.position = new Vector3 (9.4f, opponent.root.position.y, Player.root.position.z - 200);
						}
						if (OppobikeNo == 1) {
							transform.root.GetComponent<level1Harley> ().FollowPlayer = false;
							opponent.root.GetComponent<level1Harley> ().FollowPlayer = true;
							opponent.root.position = new Vector3 (-5f, opponent.root.position.y, Player.root.position.z - 5);
							transform.root.position = new Vector3 (9.4f, transform.root.position.y, Player.root.position.z - 200);
						}
					} else {
						if (OppobikeNo == 0) {
							transform.root.GetComponent<level1Harley> ().FollowPlayer = false;	
							opponent.root.GetComponent<level1Harley> ().FollowPlayer = true;	
							opponent.root.position = new Vector3 (9.4f, opponent.root.position.y, Player.root.position.z - 5);
							transform.root.position = new Vector3 (-5f, transform.root.position.y, Player.root.position.z - 200);
						} else if (OppobikeNo == 1) {
							opponent.root.GetComponent<level1Harley> ().FollowPlayer = false;	
							transform.root.GetComponent<level1Harley> ().FollowPlayer = true;	
							transform.root.position = new Vector3 (9.4f, transform.root.position.y, Player.root.position.z - 5);
							opponent.root.position = new Vector3 (-5f, opponent.root.position.y, Player.root.position.z - 200);
						}
					}
				

			} else if (tiltControl.twoBiker) {

					
					
					if (OppobikeNo == 0) {
						transform.root.GetComponent<level1Harley> ().FollowPlayer = true;	
						
						transform.root.position = new Vector3 (-5f, transform.root.position.y, Player.root.position.z - 10);
						//opponent.root.position = new Vector3 (8.4f, opponent.root.position.y, Player.root.position.z - 10);
					}
					if (OppobikeNo == 1) {
						transform.root.GetComponent<level1Harley> ().FollowPlayer = true;
						//opponent.root.GetComponent<newLevelHarley> ().FollowPlayer = true;	
						transform.root.position = new Vector3 (9.4f, transform.root.position.y, Player.root.position.z - 10);
						//opponent.root.position = new Vector3 (-3.8f, opponent.root.position.y, Player.root.position.z - 10);
					}
				

			
			}
		 
	}
	
	
	
}
