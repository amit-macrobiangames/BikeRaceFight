using UnityEngine;
using System.Collections;

public class enemy_movement : MonoBehaviour {
	
	private GameObject player;
	public Transform com;
	public WheelCollider wheel_r;
	public WheelCollider wheel_r_player;
	private float own_vel_magnitude;
	private float player_vel_magnitude;
	private bool flag;                     


	private float player_patchup_speed;
	

	Transform childPlayer;
	bool crash;
	bool skipClick;

	public bool itselfCrashed;
	public Texture[] bike;
	public Texture[] biker;
	int oppoBikeNo;
	bool death;
	void Start () {
		itselfCrashed = false;
		player = GameObject.FindWithTag ("Player");
		death = false;
		childPlayer = player.GetComponent<heavyBikeTurns> ().player.transform;
		GetComponent<Rigidbody>().centerOfMass=new Vector3(com.localPosition.x,com.localPosition.y,com.localPosition.z);
		flag = false;
		skipClick = false;
		
	
		
		//player_patchup_speed = 0.95f;

		player_patchup_speed = 0.7f;



		if (transform.root.name.Contains ("heavybike endless"))
						oppoBikeNo = 1;
				else
						oppoBikeNo = 0;
		
	}
	
	bool resetOnce=true;
	void Update () {
	if (childPlayer.name.Contains ("Player")) {
						crash = childPlayer.GetComponent<heavyBikeTurnControls> ().crash;		
						death = heavyBikeTurnControls.isdead;
				} else {
						crash = childPlayer.GetComponent<turnLevelcontrols> ().crash;
						death=turnLevelcontrols.isdead;
				}
		if (handleArrow.start && death && !itselfCrashed && !heavyBikeTurns.boostbool) {
			wheel_r.motorTorque = 30;
				}
		if (handleArrow.start && !crash &&!death && !itselfCrashed && !heavyBikeTurns.boostbool) {
						resetOnce = true;
						wheel_r.brakeTorque = 0f;
						if (transform.position.z < player.transform.position.z) {              //if(behind player)
								speed_up ();

								
						} else if (transform.position.z > player.transform.position.z) {     //if at equal adopt player
								adopt_player_speed ();
								
								
						}
				
						
			
			
			
				} else if (crash &&!death) {
						if (resetOnce) {
								resetOnce = false;
								Invoke ("resetPosition", 5f);
						}

				} else if (itselfCrashed) {
						wheel_r.motorTorque = 0;
				
				} else if (heavyBikeTurns.boostbool) {
			if(wheel_r.motorTorque>15 && own_vel_magnitude >0)
			{
				wheel_r.motorTorque -= 2.0f;
				own_vel_magnitude -= 0.5f;
				GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * own_vel_magnitude;
			}
		}


		if (transform.position.y < -0.1f || transform.position.y>=2f) {
			transform.position = new Vector3 (-3.8f,0.1f, transform.position.z);
		}
	}


	void FixedUpdate()
	{
		if (childPlayer.name.Contains ("Player")) {
			
			skipClick=childPlayer.GetComponent<heavyBikeTurnControls>().skipClicked;
			
		}
		else
			skipClick=childPlayer.GetComponent<turnLevelcontrols>().skipClicked;

		if (skipClick) {
			
			CancelInvoke("resetPosition");
			resetPosition();	

		}
	}
public	void resetPosition()
	{
	

		transform.root.position = new Vector3 (transform.root.position.x,0.2f,player.transform.root.position.z-100);
		transform.root.GetComponent<Rigidbody>().velocity = Vector3.zero;
		transform.root.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		wheel_r.motorTorque = 0.0f;
		wheel_r.brakeTorque = 1000f;
//		print ("insidr");
		if (oppoBikeNo == 0) {
			transform.Find ("opponentFatguy").Find ("biker_fat-01").transform.GetComponent<Renderer>().material.mainTexture = biker [Random.Range (0, 5)];
			transform.Find ("opponentFatguy").Find ("bike_body").transform.GetComponent<Renderer>().material.mainTexture = bike [Random.Range (0, 5)];
		} else if (oppoBikeNo == 1) {
			transform.Find ("opponentTallguys").Find ("biker-v3").transform.GetComponent<Renderer>().material.mainTexture = biker [Random.Range (0, 5)];
			transform.Find ("opponentTallguys").Find ("bike_crb").transform.GetComponent<Renderer>().material.mainTexture = bike [Random.Range (0, 5)];
		}

	}
	void speed_up()
	{
		wheel_r.motorTorque += 6f;
		//wheel_r.motorTorque += 3f;

	}



	public	void resetPos()
	{
		
		
		transform.root.position = new Vector3 (transform.root.position.x,0.2f,player.transform.root.position.z-100);


		if (oppoBikeNo == 0) {
			transform.Find ("opponentFatguy").Find ("biker_fat-01").transform.GetComponent<Renderer>().material.mainTexture = biker [Random.Range (0, 5)];
			transform.Find ("opponentFatguy").Find ("bike_body").transform.GetComponent<Renderer>().material.mainTexture = bike [Random.Range (0, 5)];
		} else if (oppoBikeNo == 1) {
			transform.Find ("opponentTallguys").Find ("biker-v3").transform.GetComponent<Renderer>().material.mainTexture = biker [Random.Range (0, 5)];
			transform.Find ("opponentTallguys").Find ("bike_crb").transform.GetComponent<Renderer>().material.mainTexture = bike [Random.Range (0, 5)];
		}
		
	}

	void OnCollisionEnter(Collision col)
	{ 
		if (col.transform.tag.Equals ("car")) {
			transform.Find ("opponentFatguy").GetComponent<Animation>().Play("death",PlayMode.StopAll);
			CancelInvoke("setPos");
				Invoke("setPos",3f);
			itselfCrashed=true;
		
			
		}
	}
	
	void setPos()
	{
		itselfCrashed=false;
		transform.Find ("opponentFatguy").GetComponent<Animation>().Play("race",PlayMode.StopAll);
		transform.root.position += new Vector3 (0,0,20);
		if (oppoBikeNo == 0) {
			transform.Find ("opponentFatguy").Find ("biker_fat-01").transform.GetComponent<Renderer>().material.mainTexture = biker [Random.Range (0, 5)];
			transform.Find ("opponentFatguy").Find ("bike_body").transform.GetComponent<Renderer>().material.mainTexture = bike [Random.Range (0, 5)];
		} else if (oppoBikeNo == 1) {
			transform.Find ("opponentTallguys").Find ("biker-v3").transform.GetComponent<Renderer>().material.mainTexture = biker [Random.Range (0, 5)];
			transform.Find ("opponentTallguys").Find ("bike_crb").transform.GetComponent<Renderer>().material.mainTexture = bike [Random.Range (0, 5)];
		}
	}

	void adopt_player_speed()
	{
		
		
		player_vel_magnitude = player.GetComponent<Rigidbody>().velocity.magnitude;
		own_vel_magnitude=GetComponent<Rigidbody>().velocity.magnitude;
		
		if (own_vel_magnitude> player_vel_magnitude  && !flag) 
		{
			if(wheel_r.motorTorque>0)
			{

			wheel_r.motorTorque -= 2.0f;
			own_vel_magnitude -= 0.5f;
			GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * own_vel_magnitude;
			}
		} 
		else 
		{
			//print(	(rigidbody.velocity.normalized*player_vel_magnitude*0.9992f)+ " : "+(rigidbody.velocity.normalized*player_vel_magnitude*0.75f) );

			flag=true;

			if(transform.position.z-player.transform.position.z <0.25f)
			{

				transform.position=new Vector3(transform.position.x,transform.position.y,player.transform.position.z);
				//rigidbody.velocity=rigidbody.velocity.normalized*player_vel_magnitude*0.9995f;
				GetComponent<Rigidbody>().velocity=GetComponent<Rigidbody>().velocity.normalized*player_vel_magnitude*0.9992f;
			}
			else
			{
				//print ("here");
				wheel_r.motorTorque =wheel_r_player.motorTorque;
				GetComponent<Rigidbody>().velocity=GetComponent<Rigidbody>().velocity.normalized*player_vel_magnitude*player_patchup_speed;
			}
		}
		
	
			transform.position = new Vector3 (-3.8f,transform.position.y, transform.position.z);
		if (transform.position.y < -0.1f || transform.position.y>=2f) {
			transform.position = new Vector3 (-3.8f,0.1f, transform.position.z);
		}
		
	}
	


	

}
