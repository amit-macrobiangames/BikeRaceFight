using UnityEngine;
using System.Collections;

public class hideit : MonoBehaviour {
	Transform player;
	//public Transform scorePopup;
	//public AudioClip carPass;
	private bool scoreOnce=true;
	//int levelID;

//	GameObject target_obj;

	//int levelNumber;

//	int bike;
	// Use this for initialization
	void Start () {

		//levelNumber = PlayerPrefs.GetInt ("levels");
	//	levelNumber = 5;
				player = GameObject.FindGameObjectWithTag ("Player").transform;

				scoreOnce = true;
//				if (Application.loadedLevelName.Contains ("endlessMode")) {
//						levelID = 0;
//				} else if (Application.loadedLevelName.Contains ("heavybikeGame")) {
//						levelID = 1;
//				}
	//	else if (Application.loadedLevelName.Contains ("level5") || Application.loadedLevelName.Contains ("KnockOut") || Application.loadedLevelName.Contains ("desert")) {
	//		levelID = 2;
			player=player.transform.GetComponent<heavyBikeTurns>().player.transform;
//			if(player.name.Contains("Fat"))
//				bike=0;
//			else if(player.name.Contains("Player"))
//				bike=1;
	//	}
	

			
	}
	
	// Update is called once per frame
	void Update () {
	
//	
//		if (levelNumber == 4 ) {
//
//			if (transform.position.z - player.position.z > - 10f && (transform.position.z - player.position.z) <= -05) {
//				
//				if (scoreOnce) {
//					if (levelID == 1) {
//						
//						endlessTallguyControl.score += 50;
//						scorePopup.GetComponent<TextMesh> ().text = "+50";
//						scorePopup.GetComponent<TextMesh> ().color = new Color (253, 246, 0, 255);
//						Instantiate (scorePopup, player.transform.position + new Vector3 (0f, 2f, 6f), Quaternion.identity);
//						
//						
//					} else if (levelID == 0) {
//						scorePopup.GetComponent<TextMesh> ().text = "+50";
//						scorePopup.GetComponent<TextMesh> ().color = new Color (253, 246, 0, 255);
//						Instantiate (scorePopup, player.transform.position + new Vector3 (0f, 2f, 6f), Quaternion.identity);//Quaternion.Euler (25,0, 0f));
//						endlessmodeControl.score += 50;
//						
//					} 
//					Invoke ("disable", 1f);
//					scoreOnce = false;
//
//					if (transform.root.name.Contains ("Car")) {
//						player.audio.PlayOneShot (carPass);
//					}
//					
//				}
//				
//			}
//				} 


		//else if (levelNumber==5) {

			if(!turnLevelcontrols.levelClear && !heavyBikeTurnControls.levelClear)
			{
			if (transform.position.z - player.position.z > - 10f && (transform.position.z - player.position.z) <= -5f) {
				
				if (scoreOnce) {
//					if (levelID == 1) {
//						
//						endlessTallguyControl.score += 50;
//						scorePopup.GetComponent<TextMesh> ().text = "+50";
//						scorePopup.GetComponent<TextMesh> ().color = new Color (253, 246, 0, 255);
//						Instantiate (scorePopup, player.transform.position + new Vector3 (0f, 2f, 6f), Quaternion.identity);
//						
//						
//					} else if (levelID == 0) {
//						scorePopup.GetComponent<TextMesh> ().text = "+50";
//						scorePopup.GetComponent<TextMesh> ().color = new Color (253, 246, 0, 255);
//						Instantiate (scorePopup, player.transform.position + new Vector3 (0f, 2f, 6f), Quaternion.identity);//Quaternion.Euler (25,0, 0f));
//						endlessmodeControl.score += 50;
//						
//					} 
					Invoke ("disable", 1f);
					scoreOnce = false;

				}
				
			}
			}
		//}

//		else {
//						//	print (transform.position.z - player.position.z);
//	
//						if (transform.position.z - player.position.z > - 105f && (transform.position.z - player.position.z) <= -100f) {
//
//								if (scoreOnce) {
//										if (levelID == 1) {
//			
//												endlessTallguyControl.score += 50;
//												scorePopup.GetComponent<TextMesh> ().text = "+50";
//												scorePopup.GetComponent<TextMesh> ().color = new Color (253, 246, 0, 255);
//												Instantiate (scorePopup, player.transform.position + new Vector3 (0f, 2f, 6f), Quaternion.identity);
//				
//				
//										} else if (levelID == 0) {
//												scorePopup.GetComponent<TextMesh> ().text = "+50";
//												scorePopup.GetComponent<TextMesh> ().color = new Color (253, 246, 0, 255);
//												Instantiate (scorePopup, player.transform.position + new Vector3 (0f, 2f, 6f), Quaternion.identity);//Quaternion.Euler (25,0, 0f));
//												endlessmodeControl.score += 50;
//					
//										} else if (levelID == 2) {
//												scorePopup.GetComponent<TextMesh> ().text = "+50";
//												scorePopup.GetComponent<TextMesh> ().color = new Color (253, 246, 0, 255);
//												Instantiate (scorePopup, player.transform.position + new Vector3 (0f, 2f, 6f), Quaternion.identity);//Quaternion.Euler (25,0, 0f));
//												if (bike == 0)
//														turnLevelcontrols.score += 50;
//												else if (bike == 1)
//														heavyBikeTurnControls.score += 50;
//												Invoke ("disable", 1f);
//
//					
//										}
//
//										scoreOnce = false;
//										if (transform.root.name.Contains ("Car")) {
//												player.audio.PlayOneShot (carPass);
//										}
//								}
//
//						}
//				}
	}

	void disable()
	{
		transform.gameObject.SetActive(false);
	}

}
