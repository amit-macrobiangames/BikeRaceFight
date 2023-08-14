using UnityEngine;
using System.Collections;

public class showBike : MonoBehaviour {
	
	//private GameObject player;
	public Transform com;
	public WheelCollider wheel_r;



                
	public bool reduce;
	

	


	


	

	public Transform playerTransform;
	public float xPosition;

	bool avoidCar;
	
	public bool FollowPlayer;

	int layerMask;

	GameObject targetObj ;
	float dist;
	RaycastHit hit=new RaycastHit();


	void Start () {
	
		Invoke ("disable",15f);
	
		layerMask = 1 << 9;
		
		avoidCar = false;
		FollowPlayer = false;

		


	
		//player = GameObject.FindWithTag ("Player");
	
	
		GetComponent<Rigidbody>().centerOfMass=new Vector3(com.localPosition.x,com.localPosition.y,com.localPosition.z);
		transform.GetChild(3).GetComponent<Animation>().Play ("idle boost",PlayMode.StopAll);
		
		if (PlayerPrefs.GetInt ("SoundOff")==0) 
		{
			
			GetComponent<AudioSource>().playOnAwake=true;
			
		}
		else 
		{
			GetComponent<AudioSource>().Pause();
		}
		
	}
	
	void disable()
	{
		gameObject.SetActive (false);
	}
	void Update () {
		
		

		
		
		if (transform.position.y < -0.1f || transform.position.y>=2f) {
			transform.position = new Vector3 ( xPosition,0.1f, transform.position.z);
		}
		
		
		
		
		targetObj = GetTarget ();
		if (targetObj != null) {
			
			
			
			dist = Vector3.Distance (targetObj.transform.position, transform.position);
			
			
			if (dist > 0 && dist <= 35) {
				if ((transform.position.x - targetObj.transform.position.x) <= 2f && (transform.position.x - targetObj.transform.position.x) >= 0f) {
					if(!avoidCar)
					{
				
						avoidCar=true;
						
					
						
						if (Physics.Raycast (transform.position + new Vector3 (0.6f, 1f, 0.0f), Vector3.right, out hit,3f,layerMask)) {
							
							if (hit.collider.tag.Contains("Player")) {
								
								xPosition=targetObj.transform.position.x-2.25f;
								
								
							}
							else
							{
								
								xPosition=targetObj.transform.position.x+2.25f;
								
							}
						}
						else
						{
							
							xPosition=targetObj.transform.position.x+2.25f;
						}
					}
					
				}
				if ((transform.position.x - targetObj.transform.position.x) < 0f && (transform.position.x - targetObj.transform.position.x) >= -2f) {
					if(!avoidCar)
					{
						
						avoidCar=true;
					
						if (Physics.Raycast (transform.position + new Vector3 (0.6f, 1f, 0.0f), Vector3.left, out hit,3f,layerMask)) {
							
							if (hit.collider.tag.Contains("Player")) {
								
								xPosition=targetObj.transform.position.x+2.25f;
								
								
							}
							else
							{
								
								xPosition=targetObj.transform.position.x-2.25f;
								
							}
						}
						else
						{
							
							xPosition=targetObj.transform.position.x-2.25f;
						}
					}
					
				}
				
			}
		}
		
		
		
		
		if (avoidCar) {
			transform.position = new Vector3 (Mathf.Lerp (transform.position.x, xPosition, 0.1f), transform.position.y, transform.position.z);	
			//			print ((transform.position.x-xPosition));
			if(transform.position.x-xPosition <0.5f && transform.position.x-xPosition >-0.5f)
			{
				avoidCar=false;
				
			}
		}
		
		


		
		
		
		
		
		
		

		
		
		if (handleArrow.start ) {
			transform.GetChild(3).GetComponent<Animation>().Play ("race",PlayMode.StopAll);
			if(!FollowPlayer)
			
			{
				speedup();

			}
			
			
		}
		
		
		
	}
	
	

	void speedup()
	{
		if(reduce)
		wheel_r.motorTorque -=2f;
		else
			wheel_r.motorTorque +=1.7f;
	}

	
	
	void OnCollisionEnter(Collision col)
	{ 
		if (col.transform.tag.Equals ("car")) {
			transform.Find ("opponentFatguy").GetComponent<Animation>().Play("death",PlayMode.StopAll);
			CancelInvoke("setPos");
			Invoke("setPos",3f);
		
			
			
		}
	}
	
	void setPos()
	{
	
		transform.Find ("opponentFatguy").GetComponent<Animation>().Play("race",PlayMode.StopAll);
		transform.root.position += new Vector3 (0,0,20);
	
	
	}
	

	
	
	

	
	
	
	GameObject GetTarget(){
		
		GameObject[] gos ;
		
		gos = GameObject.FindGameObjectsWithTag("car");
		
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach(GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance && go.transform.position.z> gameObject.transform.position.z) {
				closest = go;
				distance = curDistance;
				
			}
			
		}
		
		
		return closest;
		
		
	}
	
}
