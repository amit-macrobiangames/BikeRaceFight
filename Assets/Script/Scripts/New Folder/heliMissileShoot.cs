using UnityEngine;
using System.Collections;

public class heliMissileShoot : MonoBehaviour {
	public GameObject rocket1,target; 
	Vector3 targetPos ;
	int levelID;
	public Transform bike;
	bool playerCrashed,playerDead,levelClear,gameOver;
	public heavyBikeTurnControls heavyBike;
	public turnLevelcontrols harleyBike;
	// Use this for initialization
	void Start () {
		if(bike.GetComponent<heavyBikeTurns>().parent.name.Contains("Fatguy"))
		{
			levelID=3;
		}
		else
			levelID=2;
	}
	
	// Update is called once per frame
	void Update () {
		if (handleArrow.start && endlessmodeGraphics.gameMode.Equals ("Idle")) {
						if (heliControls.heliMove == true) {

								heliControls.heliMove = false;
								Invoke ("Shoot", 1);
								//Invoke ("Target", 1);


						}

						//rocket1.transform.LookAt (target.transform.position);
				}
	}
	public void revive()
	{
		CancelInvoke ("shoot");
		heliControls.heliMove = false;
		heliControls.revive = true;
	}
	void Shoot()
	{
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
								Instantiate (rocket1, transform.position + new Vector3 (0, -0.5f, 0), transform.rotation);
                //rocket1.SetActive(true);
                //rocket1.transform.position = transform.position + new Vector3(0, -0.5f, 0);
            }
        }
		}
	void Target()
	{
		targetPos=new Vector3(Random.Range(1.2f,4.5f),0.139f,transform.position.z-130);

		Instantiate(target , targetPos, transform.rotation);
	}
}




	



