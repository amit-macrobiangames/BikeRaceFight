using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelPlanesFuncs : MonoBehaviour {
	public storeBtnHandlers sound;

	public static bool playBtnClicked;
	public static bool clickeallowed;
	private Vector3 findClickPoint;
	private Vector3 findClickReleasePoint;
	//public Vector3 additiveClickArea;
	public Vector3 cameraAboveMe;


	private Vector3 worldPos;
	private RaycastHit hit;
	public Ray ray;

	public GameObject canvasMain;
	public Image Canvas_missionName;
	public GameObject canvasBackup;

	public Image Canvas_missionBriefing;
	//public Image Canvas_LevelImage;

	private GameObject thisMap;

	public Sprite myMissionName;
	public Sprite myMissionBriefing;
	//public Sprite myMissionImage;

	public bool lerpCamera;
	public GameObject spinner;
	//public GameObject radarBlinker;
	public GameObject canvasPlayBtn;
	public float lerpSpeed = 4.5f;

	private AsyncOperation asyncvar = null;
	private int missionSelected;

	private bool allowedToPlay;
	private string loadThisLevel;

	private string clickedObjName;
//	public GameObject fadedPlayBtn;
	public static bool isPlayClicked;


//	public float letterPause = 5f;
//	public string message;
//	public string textAdded = "";
//	public Text AllText;
//	private bool isTypeText = false;



	void Awake(){
		Time.timeScale = 1;
		thisMap = GameObject.Find("Map");
	}


	// Use this for initialization
	void Start () {
		clickeallowed = false;
		isPlayClicked = false;
		playBtnClicked = false;
	
	}
	
	// Update is called once per frame
	void Update () {
				if (Input.GetKeyUp (KeyCode.Escape) && playBtnClicked ) {
					clickeallowed = false;
					this.lerpCamera = true;
					//radarBlinker.SetActive (true);
					canvasMain.SetActive (false);
					canvasBackup.SetActive (false);
					spinner.SetActive (false);
					Invoke ("StartSwipeMap", 0.0f);
					playBtnClicked=false;
		
						}
		//print ("mouse down"+isPlayClicked);
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);





		if(this.lerpCamera && !isPlayClicked){
//			print (cameraAboveMe);
			Camera.main.gameObject.transform.position = Vector3.Lerp(Camera.main.gameObject.transform.position, cameraAboveMe, Time.deltaTime*lerpSpeed);
			if(Vector3.Distance(Camera.main.transform.position, cameraAboveMe) < 0.1f){
				//print ("inside");
				this.lerpCamera = false;
			}
		}
//		if (isTypeText) {
//			textAdded = "";
//			StartCoroutine (TypeText (myMissionBriefing));
//		}
	}

	void OnMouseDown(){
//		print ("mouse down"+isPlayClicked);
		if (!isPlayClicked) {

						lerpCamera = false;
						if (this.gameObject.name != "Map")
								clickeallowed = true;

						clickedObjName = this.gameObject.name;
						if (this.GetComponent<AudioSource>())	
								this.GetComponent<AudioSource>().enabled = false;
				}
	}

	void OnMouseUp(){
		//print ("mouse up"+isPlayClicked);
		if(!isPlayClicked){
			Debug.DrawLine (ray.origin, hit.point);
				if (Physics.Raycast (ray, out hit) && clickeallowed) {
						Debug.DrawLine (ray.origin, hit.point);
						
//				print (hit.collider.gameObject.name+"  "+hit.collider.gameObject.tag +"  "+canvasMain.activeInHierarchy);
						if (clickedObjName == hit.collider.name) {


						if (hit.collider.gameObject.tag == "MissionPlace") {
						playBtnClicked=true;


							sound.btnClicksound();
						canvasPlayBtn.GetComponent<MissionSelection_CS> ().levelNumber = hit.collider.gameObject.name;
						
						canvasPlayBtn.GetComponent<MissionSelection_CS>().Locked();
									
										canvasPlayBtn.SetActive (true);
										this.lerpCamera = true;
										Invoke ("LerpStop", 1.0f);
									
										thisMap.GetComponent<TM> ().enabled = false;
				
										spinner.SetActive (true);
						spinner.transform.parent=hit.collider.gameObject.transform.GetChild(0);
						spinner.transform.localPosition = new Vector3 (0.0f, 0, 0);
						spinner.transform.localScale = new Vector3 (1.0f, 1,1);

										
										canvasMain.SetActive (true);
										canvasBackup.SetActive (true);
										Canvas_missionName.sprite = myMissionName;
										Canvas_missionBriefing.sprite = myMissionBriefing;

										//Canvas_LevelImage.sprite = myMissionImage;
									
										if (this.GetComponent<AudioSource>() && PlayerPrefs.GetString("SoundOn") == "True")	
												this.GetComponent<AudioSource>().enabled = true;
								} 



					else if (hit.collider.gameObject.tag == "LockedMissionPlace") {
										canvasPlayBtn.SetActive (false);

										this.lerpCamera = true;
										thisMap.GetComponent<TM> ().enabled = false;
						spinner.SetActive (true);
						spinner.transform.parent=hit.collider.gameObject.transform.GetChild(0);
						spinner.transform.localPosition = new Vector3 (0.0f, 0, 0);
						spinner.transform.localScale = new Vector3 (1.0f, 1,1);
							
										canvasMain.SetActive (true);
										canvasBackup.SetActive (true);
										Canvas_missionName.sprite = myMissionName;
										Canvas_missionBriefing.sprite = myMissionBriefing;
										//Canvas_LevelImage.sprite = myMissionImage;
										if (this.GetComponent<AudioSource>() && PlayerPrefs.GetString("SoundOn") == "True")	
												this.GetComponent<AudioSource>().enabled = true;
								} 





					else if (hit.collider.gameObject.tag == "OpenMissionPlace") {

										canvasPlayBtn.GetComponent<MissionSelection_CS> ().levelNumber = hit.collider.gameObject.name;
									
										canvasPlayBtn.SetActive (true);

										this.lerpCamera = true;
								
										thisMap.GetComponent<TM> ().enabled = false;
						spinner.SetActive (true);
						spinner.transform.parent=hit.collider.gameObject.transform.GetChild(0);
						spinner.transform.localPosition = new Vector3 (0.0f, 0, 0);
						spinner.transform.localScale = new Vector3 (1.0f, 1,1);
									
										canvasMain.SetActive (true);
										canvasBackup.SetActive (true);
										Canvas_missionName.sprite = myMissionName;
										Canvas_missionBriefing.sprite = myMissionBriefing;
										//Canvas_LevelImage.sprite = myMissionImage;
										if (this.GetComponent<AudioSource>() && PlayerPrefs.GetString("SoundOn") == "True")	
												this.GetComponent<AudioSource>().enabled = true;
								} else if (hit.collider.gameObject.name.Contains("Map") && canvasMain.activeInHierarchy) {
					
										clickeallowed = false;
										this.lerpCamera = true;
									
										canvasMain.SetActive (false);
										canvasBackup.SetActive (false);
										spinner.SetActive (false);
										Invoke ("StartSwipeMap", 0.0f);

				
								}



						}
				}

		}
	}

	void LerpStop(){
		this.lerpCamera = false;
	}

	void StartSwipeMap(){
		thisMap.GetComponent<TM> ().enabled = true;
	}

	public void playClicked(){

	}


	public void LoadLevel( string name)
	{
		
		asyncvar =  SceneManager.LoadSceneAsync(name);
		
		Load ();
	}
	IEnumerator Load ()
	{
		yield return asyncvar;
	}






}
