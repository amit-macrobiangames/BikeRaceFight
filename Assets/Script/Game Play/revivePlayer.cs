using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class revivePlayer : MonoBehaviour {

	//Added 
	public static bool Revive;

	public heliMissileShoot heliMissileScript;
	public weaponAI weaponAIScript;
	public static bool stopTimer;
	public endlessmodeGraphics gameOverMode;
	public Transform leftBike,rightBike,MainCamera,cameraview,AnimationCamera;
	public Image imageRefill;
	public Text inapp;
	public GameObject bundlePanel;
	public Rocketshoot jeepControls;
	public Transform player;
	// Use this for initialization

	void Start () {
		stopTimer = false;
		player = transform.GetComponent<heavyBikeTurns> ().player.transform;
	}
	
	public void closeBundleFtn()
	{
		AudioListener.pause=false;
		imageRefill.transform.parent.gameObject.SetActive (false);
		bundlePanel.SetActive(false);
		endlessmodeGraphics.gameMode = "GameOver";
		gameOverMode.gameOverFtn();
	

	}
	public void revival()
	{
		
		//PlayerPrefs.SetInt ("helmets",10);
		if (PlayerPrefs.GetInt ("helmets") > 0) {
			Revive = true;
			PlayerPrefs.SetInt ("helmetUsed",(PlayerPrefs.GetInt ("helmetUsed")+1));
			PlayerPrefs.SetInt ("helmets", (PlayerPrefs.GetInt ("helmets") - 1));

			reStartPlayer();
				} else {
			//inapp.gameObject.SetActive(true);
			stopTimer=true;
			AudioListener.pause=true;
			imageRefill.transform.parent.gameObject.SetActive (false);
			bundlePanel.SetActive(true);
		
		}
	}


	void startFollow()
	{
		leftBike.root.position=new Vector3(0.95f,leftBike.root.position.y,transform.root.position.z-100);
		leftBike.root.GetComponent<newLevelHarley> ().FollowPlayer = true;
		rightBike.root.position=new Vector3(4.7f,leftBike.root.position.y,transform.root.position.z-100);
		rightBike.root.GetComponent<newLevelHarley> ().FollowPlayer = true;

	}

	public void reStartPlayer()
	{
		AudioListener.pause=false;
		if(PlayerPrefs.GetInt("levels")==15 || PlayerPrefs.GetInt("levels")==16)
		heliMissileScript.revive ();
		if(PlayerPrefs.GetInt("levels")==7 || PlayerPrefs.GetInt("levels")==8)
			jeepControls.revive ();
		imageRefill.transform.parent.gameObject.SetActive (false);
		bundlePanel.SetActive(false);
		transform.position += new Vector3 (0, 0, 20);
		endlessmodeGraphics.gameMode = "Idle";
		imageRefill.fillAmount = 0;
		imageRefill.transform.parent.gameObject.SetActive (false);
		imageRefill.GetComponent<fillingSprite> ().revivalStart ();
		Time.timeScale = 1.0f;
		Time.fixedDeltaTime = 0.02f;
		
		MainCamera.GetComponent<camFollow> ().target = cameraview;
		MainCamera.GetComponent<camFollow> ().player = cameraview;
		
		rightBike.root.GetComponent<newLevelHarley> ().FollowPlayer = false;
		leftBike.root.GetComponent<newLevelHarley> ().FollowPlayer = false;
		
		heavyBikeTurns.isdead = false;
		GetComponent<heavyBikeTurns> ().counter = 0;
		GetComponent<heavyBikeTurns> ().revivalStart ();
		leftBike.root.position = new Vector3 (0.95f, leftBike.root.position.y, transform.root.position.z - 200);
		rightBike.root.position = new Vector3 (4.7f, rightBike.root.position.y, transform.root.position.z - 200);
		Invoke ("startFollow",2.5f);
		
		//AnimationCamera.gameObject.SetActive(true);
		//AnimationCamera.GetComponent<AudioListener>().enabled=true;
		//MainCamera.gameObject.SetActive(false);
		//startAnimation.counter+=1;
		
		if (player.name.Contains ("Fatguy")) {
			player.GetComponent<turnLevelcontrols> ().revivalStart ();
		} else {
			player.GetComponent<heavyBikeTurnControls> ().RevivalStart ();
		}
		weaponAIScript.revive ();
		stopTimer=false;
	}
}
