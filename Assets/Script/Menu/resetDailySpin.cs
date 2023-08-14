using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class resetDailySpin : MonoBehaviour {
	public MainMenuScript mainmenu;
	public AudioClip rewardSound;
	float dragWaitTime, swipeValue;
	public GameObject firework,insufficientTicket;
	public Text bonusText;
	bool start;
	public GameObject ticketImage;
	public Transform pointer,parent,target;
	public float force,velocity;
	RaycastHit hit=new RaycastHit();
	List<Transform> childs = new List<Transform>();
	Vector3 fp,lp;
	int amountOfBonus;
	string bonusWonText;
	// Use this for initialization
	bool moveTowardTarget;
	Vector3 startingPosOfChild;


	public void resetParent()
	{
		StopCoroutine(TypeText ());
		ticketImage.SetActive(true);
		transform.localPosition = new Vector3 (1.1f,0,-15.4f);
		GetComponent<Rigidbody>().velocity =Vector3.zero;
		for (int i=0; i<childs.Count; i++) {

			childs[i].GetComponent<spinChild>().reset();
			
		}
		transform.GetComponent<Animation>().Play ("spinReturn",PlayMode.StopAll);
		Invoke ("giveBonusbool",1f);

	}
	void giveBonusbool()
	{
		MainMenuScript.bonusGiven=false;
	}
	void Awake () {
		if (PlayerPrefs.GetInt ("SoundOff") == 0) {
						GetComponent<AudioSource>().enabled = true;
				} else
						GetComponent<AudioSource>().enabled = false;
		swipeValue = 1;
		foreach(Transform child in parent.transform)
		{
			childs.Add(child.transform);
		}
//		start = true;
//		rigidbody.velocity = new Vector3 (-20, 0, 0);
//		transform.rigidbody.AddForce (Vector3.left*50, ForceMode.Force);
//		StartCoroutine ("slowDown");

	}

	// Update is called once per frame
	void Update () {



		Debug.DrawRay (pointer.position+new Vector3(0,0,0.0f),Vector3.forward* 1f, Color.cyan);
		//print (transform.rigidbody.velocity.magnitude);
		if (start) {
						if (GetComponent<Rigidbody>().velocity.magnitude <= 0.05f) {

				start=false;
				//resetParent();
				GetComponent<Rigidbody>().velocity =Vector3.zero;
			
				GetComponent<Rigidbody>().drag=0;
//				print (rigidbody.velocity.magnitude);
				Debug.DrawRay (pointer.position+new Vector3(0,0,0.0f),Vector3.forward* 1f, Color.cyan);

				if (Physics.Raycast (pointer.position+new Vector3(0,0,0.0f) , Vector3.forward, out hit,1f)) {

//					print(hit.collider.name);
					ticketImage.SetActive(false);
				
					moveTowardTarget=true;
					startingPosOfChild=hit.collider.transform.position;
					StartCoroutine(	rescaleReward(hit.collider.gameObject));
					giveBonus(hit.collider.name, hit.collider.transform.GetChild(0).name);

					GetComponent<AudioSource>().PlayOneShot(rewardSound);

				}
						}
				}



	
		
		foreach (Touch touch  in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				fp = touch.position;
				lp = touch.position;
			}
			if (touch.phase == TouchPhase.Moved )
			{
				lp = touch.position;
			}
			if(touch.phase == TouchPhase.Ended)
			{ 
				
				if((fp.x - lp.x) > 30 || (fp.x - lp.x) < -30) // left swipe
				{
					print ("swipe value: "+swipeValue);
					swipeValue=(fp.x - lp.x);
					if(swipeValue>0)
						swipeValue=-swipeValue;

					startSpinner();

				
				}

			
			}
		}
		

	}



	void giveBonus(string name, string childName)
	{
		amountOfBonus=System.Int32.Parse(childName);
//		print (amountOfBonus.ToString());
		if (name.Contains ("boost")) {
			PlayerPrefs.SetInt("boosts",(PlayerPrefs.GetInt ("boosts")+amountOfBonus));
			PlayerPrefs.Save();
		
		}
		else if (name.Contains ("bullet")) {
			PlayerPrefs.SetInt("ammos",(PlayerPrefs.GetInt ("ammos")+amountOfBonus));
			PlayerPrefs.Save();
			
		}
		else if (name.Contains ("timer")) {
			PlayerPrefs.SetInt("timers",(PlayerPrefs.GetInt ("timers")+amountOfBonus));
			PlayerPrefs.Save();
			
		}
		else if (name.Contains ("helmet")) {
			PlayerPrefs.SetInt("helmets",(PlayerPrefs.GetInt ("helmets")+amountOfBonus));
			PlayerPrefs.Save();
			
		}
		else if (name.Contains ("cash")) {
			PlayerPrefs.SetInt ("cash", (PlayerPrefs.GetInt ("cash")+amountOfBonus));
			PlayerPrefs.Save();
			
		}
		else if (name.Contains ("shield")) {
			PlayerPrefs.SetInt ("shields", (PlayerPrefs.GetInt ("shields")+amountOfBonus));
			PlayerPrefs.Save();
			
		}

	
	


	}
	IEnumerator rescaleReward(GameObject reward)
	{
		//print ("distance "+Vector3.Distance(reward.transform.position,target.position));
		while (moveTowardTarget) {

						//reward.transform.GetChild(0).animation.Play ("rescaleReward",PlayMode.StopAll);
			if(reward.transform.position==target.position || Vector3.Distance(reward.transform.position,target.position)<0.5f)
			{
				firework.SetActive(true);
				moveTowardTarget=false;
				moveTowardOriginal=true;
				bonusWonText=null;
				textAdded=null;

				bonusWonText="You have won "+ reward.transform.GetChild(0).name+ " "+ reward.name+ "!!";
								if (reward.name.Contains ("cash")) {
										bonusWonText="You have won "+ reward.transform.GetChild(0).name+ " Coins !!";
								}
				bonusText.text="";
//				print (bonusWonText);
				StartCoroutine(TypeText());
			
				StartCoroutine(originalPos(reward));

		
			}
			else
						reward.transform.position = Vector3.MoveTowards (reward.transform.position, target.position, 1f);
						yield return new WaitForSeconds (0.025f);
				}


	}

	public string textAdded;
	IEnumerator TypeText () {
		

		foreach (char letter in bonusWonText.ToCharArray()) {
			textAdded += letter;
		
			yield return new WaitForSeconds (0.05f);
			bonusText.text = textAdded;
		}
	
		
	} 
	bool moveTowardOriginal;
	IEnumerator originalPos (GameObject reward)
	{	

		yield return new WaitForSeconds (2.5f);
	
		while (moveTowardOriginal) {
			if(reward.transform.position==startingPosOfChild || Vector3.Distance(reward.transform.position,startingPosOfChild)<0.5f)
			{
				moveTowardOriginal=false;
				bonusText.text="";
				Invoke(	"resetParent",0.5f);
			}
			else
			{
				reward.transform.position = Vector3.MoveTowards (reward.transform.position, startingPosOfChild, 1f);
			}
			firework.SetActive(false);
//			print ("distance "+Vector3.Distance(reward.transform.position,startingPosOfChild));
						yield return new WaitForSeconds (0.05f);
				}
	}

	IEnumerator slowDown()
	{
		while (GetComponent<Rigidbody>().velocity.magnitude>0) {
			transform.GetComponent<Rigidbody>().drag += 0.025f;
			yield return new WaitForSeconds (dragWaitTime);
		}
	}
	public void startSpinner()
	{
		if (!MainMenuScript.bonusGiven) {
						if (PlayerPrefs.GetInt ("Tickets") > 0) {
								MainMenuScript.bonusGiven = true;
								PlayerPrefs.SetInt ("Tickets", (PlayerPrefs.GetInt ("Tickets") - 1));
								PlayerPrefs.Save ();
								int random = Random.Range (0, 20);
								if (random % 8 == 0) {
										force = 50;
										velocity = -20;
										dragWaitTime=0.25f;
								} else if (random % 8 == 1) {
										force = 90;
										velocity = -40;
										dragWaitTime=0.1f;
								} else if (random % 8 == 2) {
										force = 30;
										velocity = -20;
										dragWaitTime=0.3f;
								} else if (random % 8 == 3) {
										force = 75;
										velocity = -50;
										dragWaitTime=0.35f;
								}
								else if (random % 8 == 4) {
										force = 85;
										velocity = -50;
										dragWaitTime=0.2f;
								}
								else if (random % 8 == 5) {
										force = 50;
										velocity = -5;
										dragWaitTime=0.4f;
								}
								else if (random % 8 == 6) {
										force = 75;
										velocity = -50;
										dragWaitTime=0.4f;
								}
								else if (random % 8 == 7) {
										force = 75;
										velocity = -22;
										dragWaitTime=0.3f;
								}


								GetComponent<Rigidbody>().velocity = new Vector3 (velocity+(swipeValue/3), 0, 0);
								transform.GetComponent<Rigidbody>().AddForce (Vector3.left * (force+(swipeValue/3)), ForceMode.Force); //50
								StartCoroutine ("slowDown");
//								print ("ticketsLeft:  " + PlayerPrefs.GetInt ("Tickets"));
								start = true;
								PlayerPrefs.SetInt ("ticketUsed",(PlayerPrefs.GetInt("ticketUsed")+1));
						}
			else
			{

				mainmenu.insufficientTicketFtn();
			}
//			else
//			{
//				MainMenuScript.insufficientTicket=true;
//				insufficientTicket.SetActive(true);
//			}
				}
	}
}
