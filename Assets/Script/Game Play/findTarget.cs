﻿using UnityEngine;
using System.Collections;

public class findTarget : MonoBehaviour {
	public GameObject target_obj;
	public Transform player;
	float maxRange;
	float dist;
	public static bool startmove;

	bool  isboost;
	//public bool boostOn,setBoost;
	//public bool isJumping;


	float upperRange,lowerRange;



	//turnLevelcontrols level3Harley;
	//heavyBikeTurnControls level3Heavy;
	int levelNumber;
	public static bool carBrake;
	isMove carScript;

	//public static bool jump;

	// Use this for initialization
	void Start () {
	
		carBrake = false;
	
		//maxRange = 50f;
		startmove = false;
	
	
	
	
		levelNumber = PlayerPrefs.GetInt ("levels");
		//levelNumber = 9;

	
		if (levelNumber != 9 && levelNumber != 10) {
			this.enabled=false;
		}
		//this.enabled=false;
//		boostOn = false;
//		setBoost = false;


			
			player = player.GetComponent<heavyBikeTurns> ().player.transform;
//			if (player.name.Contains ("Fat")) {
//				
//				level3Harley=player.GetComponent<turnLevelcontrols>();
//			} else if (player.name.Contains ("Player")) {
//				
//				level3Heavy=player.GetComponent<heavyBikeTurnControls>();
//			}
//		



	}
	

	void Update () {
				
				
						isboost = heavyBikeTurns.boostbool;
				
				//		print (endlessmodeControl.startGame + endlessmodeGraphics.gameMode);
				if (endlessmodeGraphics.gameMode == "Idle" && handleArrow.start) {
						if (levelNumber==9) {
		
								target_obj = GetTarget ();
				if (target_obj != null) {
								target_obj = target_obj.transform.gameObject;

								
										dist = Vector3.Distance (target_obj.transform.position, transform.position);
//					print(dist+ target_obj.name);

						if (dist > 0 && dist <= 25) {
												startmove = true;
											
					
						
										} else {
						
												startmove = false;
												
										}
		
						
								}
								//	print (carBrake);
								if (target_obj == null) {
										carBrake = false;
								}
				if(startmove)
				{
								if (!isboost ) {
										if (!carBrake) {
												isMove.rotateTire = true;








						
							if (target_obj.name.Equals ("heavybike") ) {
								
								target_obj.transform.Translate (0,0, -Time.fixedDeltaTime * 50f);//1.2
								
							} 
							else 	if (target_obj.name.Contains ("Low") ) {
								target_obj.transform.Translate (0,0, -Time.fixedDeltaTime * 50f);//1.2
							//	print("low");
							}
							else
							{
								target_obj.transform.Translate (0, 0, -Time.fixedDeltaTime * 1f);//-1.2
							}
							
										}
								} else  {
										if (!carBrake) {
												isMove.rotateTire = true;





							
							if (target_obj.name.Equals ("heavybike") ) {
								
								target_obj.transform.Translate (0,0, -Time.fixedDeltaTime * 70f);//1.2
								
							} 
							else 	if (target_obj.name.Contains ("Low") ) {
								target_obj.transform.Translate (0,0, -Time.fixedDeltaTime * 70f);//1.2
								//print("low");
							}
							else
							{
								target_obj.transform.Translate (0, 0, -Time.fixedDeltaTime * 1f);//-1.2
							}

						}

								}

			}
			}

						//Added for level 10




						if (levelNumber == 10)
						{

				target_obj = GetTarget();
				if (target_obj != null)
				{
					target_obj = target_obj.transform.gameObject;


					dist = Vector3.Distance(target_obj.transform.position, transform.position);
					//					print(dist+ target_obj.name);

					if (dist > 0 && dist <= 25)
					{
						startmove = true;



					}
					else
					{

						startmove = false;

					}


				}
				//	print (carBrake);
				if (target_obj == null)
				{
					carBrake = false;
				}
				if (startmove)
				{
					if (!isboost)
					{
						if (!carBrake)
						{
							isMove.rotateTire = true;









							if (target_obj.name.Equals("heavybike"))
							{

								target_obj.transform.Translate(0, 0, -Time.fixedDeltaTime * 5f);//1.2

							}
							else if (target_obj.name.Contains("Low"))
							{
								target_obj.transform.Translate(0, 0, -Time.fixedDeltaTime * 1f);//1.2
																								//	print("low");
							}
							else
							{
								target_obj.transform.Translate(0, 0, -Time.fixedDeltaTime * 3f);//-1.2
							}

						}
					}
					else
					{
						if (!carBrake)
						{
							isMove.rotateTire = true;






							if (target_obj.name.Equals("heavybike"))
							{

								target_obj.transform.Translate(0, 0, -Time.fixedDeltaTime * 5f);//1.2

							}
							else if (target_obj.name.Contains("Low"))
							{
								target_obj.transform.Translate(0, 0, -Time.fixedDeltaTime * 5f);//1.2
																								//print("low");
							}
							else
							{
								target_obj.transform.Translate(0, 0, -Time.fixedDeltaTime * 3f);//-1.2
							}

						}

					}

				}
			}


		}
		}





	GameObject GetTarget(){
		
		GameObject[] gos ;
	
		gos = GameObject.FindGameObjectsWithTag("chowkCar");

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
	
		if (closest != null) {
//			print ("closest: "+closest.transform.parent);	

			//Commemt Added
			//					carScript = closest.transform.GetComponent<isMove> ();
			////	print (closest.transform.parent.name+ "  "+ closest.transform.parent.parent.name);
			//	closest.transform.GetComponent<AudioSource>().enabled=true;
			//	closest.transform.GetComponent<AudioSource>().Play();

				}
	
		return closest;
		
		
	}


}
