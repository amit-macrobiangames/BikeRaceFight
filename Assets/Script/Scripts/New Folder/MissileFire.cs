using UnityEngine;
using System.Collections;

public class MissileFire : MonoBehaviour {

	float lastPos,z;
	public Transform levelClearTransform;
	public GameObject car,misslePrefab,missile1,missile2;

	int minDistance;
	float playerDistance;

	int levelID;

	bool playerCrashed,playerDead,levelClear,gameOver;
	public heavyBikeTurnControls heavyBike;
	public turnLevelcontrols harleyBike;
	// Use this for initialization

	void Start () {
		if (PlayerPrefs.GetInt ("levels") == 16)
						this.enabled = true;
				else
						this.enabled = false;
		//z = 0;
		//lastPos = car.transform.position.z;
		minDistance=100;
		z=car.transform.position.z;
		lastPos = z + minDistance;
	}
	
	// Update is called once per frame
	void Update () {
//		print (lastPos+ "  :  "+ z);
		if (levelID == 2) {
			
			playerCrashed = heavyBike.crash;
			playerDead = heavyBikeTurns.isdead;
			levelClear = heavyBikeTurnControls.levelClear;
			gameOver = heavyBikeTurnControls.timeover;
			
		} else if (levelID == 3) {
			
			playerCrashed = harleyBike.crash;
			playerDead = heavyBikeTurns.isdead;
			levelClear = turnLevelcontrols.levelClear;
			gameOver = turnLevelcontrols.timeover;
			
		}
		
		if (handleArrow.start && endlessmodeGraphics.gameMode.Equals ("Idle")) {
			
			
			
			if (!playerCrashed && !playerDead && !levelClear && !gameOver ) {
		 if(playerHealthBar.Remainingdistance  > 1000 && playerHealthBar.Remainingdistance  <2000)
		{
			if(playerHealthBar.Remainingdistance  <= 1200)
			{
				minDistance = 50;
			}
			
			
			z = car.transform.position.z;
			if(z >= lastPos )
			{
				//var misslee = Instantiate(misslePrefab , new Vector3(Random.Range(1.2f,4.3f), 0.135f, z+30), transform.rotation );

				missile1.transform.position=new Vector3(Random.Range(1.2f,4.3f), 0.135f, z+30);
				missile1.SetActive(true);
				lastPos = z + minDistance;
				
				
			}
		}
		else if(playerHealthBar.Remainingdistance  <=1000)
		{
			z = car.transform.position.z;
			if(z >= lastPos )
			{
//				var misslee2 = Instantiate(misslePrefab , new Vector3(Random.Range(1.2f,2.75f), 0.135f, z+30), transform.rotation );
//				var misslee1 = Instantiate(misslePrefab , new Vector3(Random.Range(2.75f,4.3f), 0.135f, z+30), transform.rotation );
//			
					if(	(z+30)<levelClearTransform.position.z){
				missile2.transform.position=new Vector3(Random.Range(1.2f,2.7f), 0.135f, z+30);
				missile2.SetActive(true);
				missile1.transform.position=new Vector3(Random.Range(2.85f,4.3f), 0.135f, z+30);
				missile1.SetActive(true);
				lastPos = z + minDistance;
						}
				
			}
		}
	}
}
	}
}



