using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class coinManager : MonoBehaviour {
	public AudioClip coinSound;
	public GameObject collectEffect;
	bool animationOnce;
	bool moveTowardMeter;
	public GameObject meter;
	coinsParentScript parentScript;
	public Camera cam;
	public Image coinImage;
	Transform player;
	MeshRenderer itself;
	// Use this for initialization
	void Start () {


		itself = transform.GetComponent<MeshRenderer> ();
		collectEffect = transform.parent.GetChild (0).gameObject;
		animationOnce = true;
		moveTowardMeter = false;
	
		parentScript = transform.root.GetComponent<coinsParentScript> ();
		if (PlayerPrefs.GetInt("SoundOff") == 0) {
						GetComponent<AudioSource>().enabled = true;		
				} else
						GetComponent<AudioSource>().enabled = false;

	//	cam = GameObject.Find ("child").GetComponent<Camera> () as Camera;
//		pos=new Vector3(coinImage.rectTransform.anchoredPosition.x,	coinImage.rectTransform.anchoredPosition.y,10);
//		
//		position = cam.ScreenToWorldPoint (pos);
//		print (position);
//		Debug.Log(cam.ScreenToWorldPoint(pos));

		player = GameObject.FindGameObjectWithTag ("CoinLocation").transform;
		
		//MyObj.transform.position = Camera.main.WorldToScreenPoint(NonUIGameObj.transform.position); 
	}
	Vector3 position,pos;
	// Update is called once per frame
	void LateUpdate(){
		if (magnetScript.magnetOn && (transform.position.z-player.position.z)<15f) {
			
			transform.position = Vector3.Lerp (transform.position,player.position, 4f * Time.deltaTime* (15 / (transform.position.z-player.position.z))); //3

		}
	}
	void Update () {
	
//		if (moveTowardMeter) {
//			itself.enabled=false;
//					//transform.position = Vector3.Lerp (transform.position,meter.transform.position, 3f * Time.deltaTime); //3
//
//	}
//		if (magnetScript.magnetOn && (transform.position.z-player.position.z)<15f) {
//			
//			transform.position = Vector3.Lerp (transform.position,player.position, 4f * Time.deltaTime); //3
//			
//		}
		if (parentScript.resetChild) {
			reset();		
		}
	}
	void reset()
	{
		moveTowardMeter = false;
		transform.localPosition = Vector3.zero;
		animationOnce = true;
		itself.enabled=true;

	}

	void turnEffectOff(){
		collectEffect.SetActive(false);
	}
	void OnTriggerEnter(Collider col){
		if (col.tag.Equals ("Player")) {
			if(animationOnce){
				itself.enabled=false;
				collectEffect.SetActive(true);
			//print("inside");
				CancelInvoke("turnEffectOff");
				moveTowardMeter=true;
				GetComponent<AudioSource>().PlayOneShot(coinSound);
				heavyBikeTurns.coins+=1;
				heavyBikeTurnControls.score+=10;
				turnLevelcontrols.score+=10;
				heavyBikeTurns.coinCash+=10;
				heavyBikeTurns.score+=10;
		
				animationOnce=false;
				Invoke("turnEffectOff",0.3f);
			}
		}
	}
}
