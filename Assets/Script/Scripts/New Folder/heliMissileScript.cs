using UnityEngine;


public class heliMissileScript : MonoBehaviour {
	public GameObject bomb ,explotion;

	public ParticleSystem explosion;
	bool once,bombHit;
	int levelID;
	float speed;
	bool follow;
	// Use this for initialization
//	void Awake(){
//		bomb = GameObject.FindGameObjectWithTag("Player");
//		transform.LookAt (bomb.transform.position);
//	}
	void Start () {
		once = true;
		bombHit = false;
		follow = true;
		bomb = GameObject.FindGameObjectWithTag("Player");
		transform.LookAt (bomb.transform.position);
		Invoke ("followPlayerOff",1f);
		levelID = PlayerPrefs.GetInt ("levels");
		if (levelID == 15) {
						speed = 40;
						Invoke ("followPlayerOff", 0.75f);
				} else if (levelID == 16 || levelID == 17 || levelID == 19 || levelID == 20) {
						speed = 80;
			Invoke ("followPlayerOff", 0.35f);
				}
	
	}
	void followPlayerOff(){
		follow = false;
	}
	// Update is called once per frame
	void Update () {
		if (!bombHit) {
						//	transform.position += new Vector3 (0,0,80*Time.deltaTime);
			if(follow)
			transform.LookAt (bomb.transform.position);
						transform.Translate (Vector3.forward * speed * Time.deltaTime);
				}
//		//if((transform.position.z-bomb.transform.position.z)<=0 && once)
//		if(gameObject.transform.position.y <= 0.175f && once)
//		{
//			
//			explotion.active = true;
//			explosion.transform.parent=null;
//			transform.GetChild(0).gameObject.SetActive(false);
//			//explosion.Play ();
//			
//			
//			once=false;
//		}


//		else if(gameObject.transform.position.y <= -3f )
//		{
//			reset();
//			
//			transform.root.gameObject.SetActive(false);
//			
//		}
	}


	void reset(){
		once = true;
	
		explotion.SetActive(false);
	
		explosion.Stop ();
	}
	void OnCollisionEnter(Collision col )
	{
		
		
			if (col.gameObject.tag == "Player") {
//				print ("Heli bomb Hitted");
				
				bombHit=true;
				explosion.gameObject.SetActive(true);
				//explotion.active = true;
				explosion.transform.parent=null;
				transform.GetChild(0).gameObject.SetActive(false);
	
				
				once=false;
	
				
				
			}
		
	}
}







