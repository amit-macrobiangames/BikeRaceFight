using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MissionSelection_CS : MonoBehaviour {

	//public float letterPause = 0.0f;
//	public string message;
	//public string textAdded;
	//public Text AllText;
	public Image playBtnColor;
	public Sprite playUnlock, playLocked;
	public string levelNumber;
	public string LevelSelected;
	private AsyncOperation asyncvar = null;

	public GameObject LoadingTexture;
	public Text Loadingtext;
	public Image LoadingBar;


	public GameObject percentBG;

	string levelStatus;


	float xPosition;
	public Color lockedColor;


	// Use this for initialization
	void Start () {
		xPosition = percentBG.transform.localPosition.x;
//		PlayerPrefs.SetString("level2Unlocked","true");
//		PlayerPrefs.SetString("level3Unlocked","true");
//		PlayerPrefs.SetString("level4Unlocked","true");
//		PlayerPrefs.SetString("level5Unlocked","true");
//		PlayerPrefs.SetString("level6Unlocked","true");
//		PlayerPrefs.SetString("level7Unlocked","true");
//
//		PlayerPrefs.SetString("level8Unlocked","true");
//		PlayerPrefs.SetString("level9Unlocked","true");
//		PlayerPrefs.SetString("level10Unlocked","true");
//		PlayerPrefs.SetString("level11Unlocked","true");
//		PlayerPrefs.SetString("level12Unlocked","true");
//		PlayerPrefs.SetString("level13Unlocked","true");
		//StartCoroutine(TypeText ());
	}

	public void Locked()
	{
		levelStatus="level"+ levelNumber+"Unlocked";
		//print (levelStatus+ "   "+PlayerPrefs.GetString (levelStatus));
		if (!PlayerPrefs.GetString (levelStatus).Equals ("true")) {
						playBtnColor.sprite = playLocked;
						//		playBtnColor.color = lockedColor;
				} else {
			playBtnColor.sprite=playUnlock;
						playBtnColor.color = Color.white;
				}
	}
	
	// Update is called once per frame
	// void Update () {
	
//		if(asyncvar != null)
//		{
//			double LoadedPercent = asyncvar.progress * 100;
//			LoadedPercent = (int)LoadedPercent;
//			Loadingtext.text = LoadedPercent.ToString() + "%";
//		
//			LoadingBar.fillAmount=asyncvar.progress;
//		//300//150
//			percentBG.transform.localPosition=new Vector3(xPosition+(asyncvar.progress *360),percentBG.transform.localPosition.y,percentBG.transform.localPosition.z);
//
//		}



	// }

//	IEnumerator TypeText () {
//		foreach (char letter in message.ToCharArray()) {
//			textAdded += letter;
//			AllText.text = textAdded;		
//		
//		}
//	}


//	IEnumerator TypeText () {
//		
//		
//		foreach (char letter in message.ToCharArray()) {
//			textAdded += letter;
//			AllText.text = textAdded;
//			}
//			yield return new WaitForSeconds (letterPause);
//			
//		} 


	public void PlayClicked(){
		levelStatus="level"+ levelNumber+"Unlocked";
//		print (levelStatus+ "   "+PlayerPrefs.GetString (levelStatus));
		if (PlayerPrefs.GetString (levelStatus).Equals ("true")) {

			PlayerPrefs.SetInt("leaderBoardScore",0);

//						print ("play");
						LevelPlanesFuncs.clickeallowed = false;
						LevelPlanesFuncs.isPlayClicked = true;
						PlayerPrefs.SetInt ("levels", int.Parse (levelNumber.ToString ()));
						PlayerPrefs.Save ();
						LoadingTexture.SetActive (true);
						this.GetComponent<Button> ().enabled = false;
						this.GetComponent<Image> ().enabled = false;
						//	this.gameObject.SetActive (false);

				LoadLevel (LevelSelected);
			if(bikeSelection.backFromLevel){
				
				if (!PlayerPrefs.GetString ("bundlePurchase").Equals ("true")) {
					
					print("showing admob interstitial");
					//GoogleMobileAdsDemoScript.instance.showInterstitial ();
					//	manager.HideAdmobInterstitial ();
					
				}
				bikeSelection.backFromLevel=false;
			}

						if (!PlayerPrefs.GetString ("bundlePurchase").Equals ("true")) {
				
								//	manager.HideAdmobInterstitial ();
				
						}

				}
	}

	string levelNam;
	public void LoadLevel( string name)
	{
		//LoadingTexture.SetActive (true);
		levelNam = name;
		Invoke ("startLvl",3f);
	
		//asyncvar =  Application.LoadLevelAsync(name);
		
		//Load ();
	}
	void startLvl(){
		SceneManager.LoadScene(levelNam);
	}
	IEnumerator Load ()
	{
		yield return asyncvar;
	}

}
