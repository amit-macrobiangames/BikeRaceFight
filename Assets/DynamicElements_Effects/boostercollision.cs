using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class boostercollision : MonoBehaviour {
	float[] xAxisLanes =new float[4];
	public Transform player;
	//public MotionBlur cube,leftCube;
	public AudioClip audioSound;
	public PhycamViews mainCamera;
	//public static bool position;
	public MotionBlur motionBlurScript;
	float distance;
	bool heliLevels;
	// Use this for initialization
	void Start () {
		

		//position = false;
		once=true;
		xAxisLanes [0] = 4.118f;
		xAxisLanes [1] = 3.129f;
		xAxisLanes [2] = 1.828f;
		xAxisLanes [3] = 2.485f;
		
		
		
		player=player.GetComponent<heavyBikeTurns>().player.transform;		
		
		if (transform.name.Contains ("missile")) {
			if(PlayerPrefs.GetInt("levels")==13 || PlayerPrefs.GetInt("levels")==14){
				gameObject.SetActive(true);
				transform.position=new Vector3(xAxisLanes[2],transform.position.y,-550f);
			}
			else
				gameObject.SetActive(false);
		}
		
		heliLevels=false;
		if(PlayerPrefs.GetInt("levels")==15 || PlayerPrefs.GetInt("levels")==16)
			heliLevels=true;

			distance=2000f;
		
		if (PlayerPrefs.GetInt ("levels") == 5 ) {
			if(transform.name.Contains("boost"))
			{
				distance=400f;
			}
			else	if(transform.name.Contains("life"))
			{
				distance=350f;
			}
		}
		else if (PlayerPrefs.GetInt ("levels") == 6) {
			if(transform.name.Contains("ammo"))
			{
				distance=400f;
			}
			else if(transform.name.Contains("life"))
			{
				distance=350f;
			}
		}
		else if (PlayerPrefs.GetInt ("levels") == 7 || PlayerPrefs.GetInt ("levels") == 8 || PlayerPrefs.GetInt ("levels") == 11 ) {
			if(transform.name.Contains("shield"))
			{
				distance=350f;
			}
			else if(transform.name.Contains("life"))
			{
				distance=400f;
			}

		}
		else if (PlayerPrefs.GetInt ("levels") == 9 || PlayerPrefs.GetInt ("levels") == 10) {
			if(transform.name.Contains("Clock"))
			{
				distance=350f;
			}
			else if(transform.name.Contains("life"))
			{
				distance=400f;
			}
			
		}
		else if (PlayerPrefs.GetInt ("levels") == 13 || PlayerPrefs.GetInt ("levels") == 14) {
			if(transform.name.Contains("missile"))
			{
				distance=350f;
			}

			
		}
	}
	
	// Update is called once per frame
	void Update () {
		//print ((transform.position.z - player.position.z));
		if ((transform.position.z - player.position.z) <= -1f) {
			int xaxis=Random.Range(0,3);
			
			transform.position=new Vector3(xAxisLanes[xaxis],transform.position.y,transform.position.z+distance);
			
		}
		
		
	}
	bool once=true;
	void reset()
	{
		once = true;
	}
	float updatedValue;
	void OnTriggerEnter(Collider col)
	{
		
		//		print ("trigger");
	
		if (col.gameObject.tag.Equals ("Player")) {
						if (once) {
								int xaxis = Random.Range (0, 3);
								//	if ((transform.position.z - player.position.z) <= 1f && (transform.position.z - player.position.z) >= 0.75f ) 
								transform.position = new Vector3 (xAxisLanes [xaxis], transform.position.y, transform.position.z + distance);
								once = false;
								Invoke ("reset", 2f);
			
								if (player.GetComponent<AudioSource>().enabled)
										player.GetComponent<AudioSource>().PlayOneShot (audioSound);
			
								if (transform.name.Contains ("Clock")) {
										//print ("inside clock");
									
												updatedValue = ((100f / (PhycamViews.startedValue + 1)) / 100f);
												mainCamera.timerFG.fillAmount += updatedValue;
					
												PlayerPrefs.SetInt ("timers", (PlayerPrefs.GetInt ("timers") + 1));
												PlayerPrefs.Save ();
												PhycamViews.counter = Random.Range (1, 3);
												if(heliLevels)
													PhycamViews.counter=2;
//												mainCamera.changeCamView ();
										
				
				
				
								}
				if (transform.name.Contains ("missile")) {
				

					PlayerPrefs.SetInt ("missile", (PlayerPrefs.GetInt ("missile") + 3));
					PlayerPrefs.Save ();
					PhycamViews.counter = Random.Range (1, 3);
					if(heliLevels)
						PhycamViews.counter=2;
//					mainCamera.changeCamView ();

					
				}
			
				if (transform.name.Contains ("ammo")) {
					//print ("inside ammo");
									
					PlayerPrefs.SetInt ("ammos", (PlayerPrefs.GetInt ("ammos") + 6));
					PlayerPrefs.Save ();						
					PhycamViews.counter = Random.Range (1, 3);
					if(heliLevels)
						PhycamViews.counter=2;
//												mainCamera.changeCamView ();

								}
				if (transform.name.Contains ("shield")) {
						PhycamViews.counter = Random.Range (1, 3);
	
					if(heliLevels)
						PhycamViews.counter=2;
//										mainCamera.changeCamView ();
										PlayerPrefs.SetInt ("shields", (PlayerPrefs.GetInt ("shields") + 1));
										PlayerPrefs.Save ();
								}
			
			
			//			if(transform.name.Contains("stunt"))
			//			{
			//			
			//				if( Application.loadedLevelName.Contains("endlessMode") || Application.loadedLevelName.Contains ("harley"))
			//			{
			//				player.GetComponent<endlessmodeControl>().stuntbool=true;
			//			}
			//				else if(Application.loadedLevelName.Contains("heavybikeGame") || desertLevel==1)
			//			{
			//				player.GetComponent<endlessTallguyControl>().stuntbool=true;
			//			}
			//
			//
			//			}
			else if (transform.name.Contains ("boost pickup")) {

										//print ("inside boost");
										//				if( Application.loadedLevelName.Contains("endlessMode") || Application.loadedLevelName.Contains ("harley"))
										//				{
										//					player.GetComponent<endlessmodeControl>().boostbool=true;
										//
										//					GameObject.Find("Main Camera").GetComponent<MotionBlur>().enabled=true;
										//					player.FindChild("Bone01").FindChild("Cube").FindChild("Camera").GetComponent<MotionBlur>().enabled=true;
										//					player.FindChild("Bone01").FindChild("leftCube").FindChild("Camera").GetComponent<MotionBlur>().enabled=true;
										//					Invoke("tempboostoff",5f);
										//				}
										//				else if(Application.loadedLevelName.Contains("heavybikeGame"))// || Application.loadedLevelName.Contains ("desert")
										//				{
										//					player.GetComponent<endlessTallguyControl>().boostbool=true;
										//					GameObject.Find("Main Camera").GetComponent<MotionBlur>().enabled=true;
										//					player.FindChild("Bone01").FindChild("Cube").FindChild("Camera").GetComponent<MotionBlur>().enabled=true;
										//					player.FindChild("Bone01").FindChild("leftCube").FindChild("Camera").GetComponent<MotionBlur>().enabled=true;
										//					Invoke("tempboostoff",5f);
										//				}
										//				else if(Application.loadedLevelName.Equals("level5") || Application.loadedLevelName.Contains ("KnockOut") || Application.loadedLevelName.Contains ("desert"))
										//				{
										//					if(desertLevel==1)
										//					{
										//						PlayerPrefs.SetInt("boosts",(PlayerPrefs.GetInt ("boosts")+1));
										//						PlayerPrefs.Save();
										//						PhycamViews.counter=Random.Range(1,3);
										//						mainCamera.changeCamView();
										//						//startBoost();
										//
										//
										//
										//					}
										//
										//				}
										if (SceneManager.GetActiveScene().name.Contains ("desert")) {
												
														PlayerPrefs.SetInt ("boosts", (PlayerPrefs.GetInt ("boosts") + 1));
														PlayerPrefs.Save ();
														PhycamViews.counter = Random.Range (1, 3);
						if(heliLevels)
							PhycamViews.counter=2;
//														mainCamera.changeCamView ();
												
														updatedValue = ((100f / (weaponAI.startedValue + 1)) / 100f);
														player.root.GetComponent<weaponAI> ().boostFG.fillAmount += updatedValue;
						
													
						
						
												
					
										}
				
								} else if (transform.name.Contains ("life pickup")) {
									//	print ("inside life");
				
										//				if( Application.loadedLevelName.Contains("endlessMode") || Application.loadedLevelName.Contains ("harley"))
										//				{
										//					player.GetComponent<endlessmodeControl>().counter=0;
										//					player.GetComponent<endlessmodeControl>().lifeBar=true;
										//				}
										//				else if(Application.loadedLevelName.Contains("heavybikeGame") )
										//				{
										//					player.transform.GetComponent<endlessTallguyControl>().counter=0;
										//					player.GetComponent<endlessTallguyControl>().lifeBar=true;
										//				}
										//				else if(Application.loadedLevelName.Contains("level5") || Application.loadedLevelName.Contains ("KnockOut"))
										//				{
										//					player.root.transform.GetComponent<heavyBikeTurns>().counter=0;
										//					if(player.name.Contains("Player"))
										//					player.GetComponent<heavyBikeTurnControls>().lifeBar=true;
										//					else
										//						player.GetComponent<turnLevelcontrols>().lifeBar=true;
										//				}
									
												PlayerPrefs.SetInt ("helmets", (PlayerPrefs.GetInt ("helmets") + 1));
												PlayerPrefs.Save ();
												PhycamViews.counter = Random.Range (1, 3);
					if(heliLevels)
						PhycamViews.counter=2;
//												mainCamera.changeCamView ();
					
										
				
								}
			
			
						}
		
				}
		
		
		
		
	}
	
	public	void startBoost()
	{
		//PlayerPrefs.SetInt ("boosts", 10000);
		//		if (PlayerPrefs.GetInt ("boosts") > 0) {
		//			PlayerPrefs.SetInt("boosts",(PlayerPrefs.GetInt ("boosts")-1));
		//			PlayerPrefs.Save();
		//GameObject.Find ("Main Camera").GetComponent<MotionBlur> ().enabled = true;
		motionBlurScript.enabled = true;
		mainCamera.	leftCam.transform.Find ("Camera").GetComponent<MotionBlur> ().enabled = true;
		mainCamera.rightCam.transform.Find ("Camera").GetComponent<MotionBlur> ().enabled = true;
		if (player.name.Contains ("Fatguy")) {
			
			player.GetComponent<turnLevelcontrols> ().stuntbool = true;
			
		} else {
			
			player.GetComponent<heavyBikeTurnControls> ().stuntbool = true;
			
		}
		
		
		heavyBikeTurns.boostbool = true;
		Invoke ("boostOff", 5f);
		
		//}
	}
	//	void tempboostoff(){
	//		if(desertLevel==1)
	//		player.GetComponent<endlessTallguyControl>().boostbool=false;
	//		else
	//		player.GetComponent<endlessmodeControl>().boostbool=false;
	//
	//
	//		GameObject.Find("Main Camera").GetComponent<MotionBlur>().enabled=false;
	//		player.FindChild("Bone01").FindChild("Cube").FindChild("Camera").GetComponent<MotionBlur>().enabled=false;
	//		player.FindChild("Bone01").FindChild("leftCube").FindChild("Camera").GetComponent<MotionBlur>().enabled=false;
	//	}
	void boostOff()
	{
		heavyBikeTurns.boostbool=false;
	
		//	GameObject.Find ("Main Camera").GetComponent<MotionBlur> ().enabled = false;
		motionBlurScript.enabled = false;
			mainCamera.	leftCam.transform.Find ("Camera").GetComponent<MotionBlur> ().enabled = false;
			mainCamera.rightCam.transform.Find ("Camera").GetComponent<MotionBlur> ().enabled = false;
			
		
	}


	public  void boostCollection()
	{

		weaponAI.startedValue = PlayerPrefs.GetInt ("boosts");
		updatedValue = ((100f / (weaponAI.startedValue + 1)) / 100f);
		player.root.GetComponent<weaponAI> ().boostFG.fillAmount += updatedValue;
		

	}
}
