using UnityEngine;
using System.Collections;

public class global_var : MonoBehaviour {
	
	public static float speed;
	public static bool player_death;
	public static float increase_in_speed;
	public BoxCollider boxCol;
	public Transform COM;
	bool startGame;
		Transform player;


	public Transform wheelCollider,frontWheelCollider;

	public static int boostCounter;

	void Awake()
	{
				
		startGame = true;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		speed=100f;                 //30             //at max speed must be 160     (160 max speed otherwise turnint errors)
		increase_in_speed=0.5f;//0.35f
		player_death=false;
	

	}
	
	public static void reset()
	{
		speed=30f;                                  //at max speed must be 160     (160 max speed otherwise turnint errors)
		increase_in_speed=0.35f;
		player_death=false;

	
	}
	
	// Use this for initialization
	void Start () {
		boostCounter = 0;
		player = transform.GetComponent<heavyBikeTurns>().player.transform;
	
	
	
		if(player.name.Contains("playerFatguy"))
		{

			wheelCollider.localPosition=new Vector3(0.0f,0.475f,-1.091508f);
//			COM.localPosition=new Vector3(0,0.05f,0.1f);
//			boxCol.center=new Vector3(0f,1.21f,0.24f);
//			boxCol.size=new Vector3(0.45f,1f,3.5f);
		}
		
		
		else if(player.name.Contains("Player"))
		{

			wheelCollider.localPosition=new Vector3(0.0f,0.475f,-1.091508f);
		}
	}
	

//	void Update(){
//		if (startGame && tiltControl.startTiltAfterCam) {
//			startGame=false;
//			if(player.name.Contains("playerFatguy"))
//			{
//				boxCol.enabled=true;
//			
//			}
//		}
//	}



}
