using UnityEngine;
using System.Collections;

public class position_ramp : MonoBehaviour {

	private GameObject player;
	float pos;
	float rightPos;
	bool movePlayer;
	GameObject target_obj;


	// Use this for initialization
	void Start () {
	
		player = GameObject.FindWithTag ("Player");
		pos=transform.position.x-0.15f;//transform.position.x-2
		rightPos = transform.position.x + 0.15f;
		movePlayer = false;


	}
	
	// Update is called once per frame
	void Update () {
//		print ((transform.position.z - player.transform.position.z)+ "   xposition: "+(transform.position.x - player.transform.position.x) );
		if (transform.position.z - player.transform.position.z <= - 25f) {

		//	player.GetComponent<heavyBikeTurns>().rampControl=false;
			transform.gameObject.SetActive(false);

		}
//		if (transform.position.z - player.transform.position.z <= 0f && transform.position.z - player.transform.position.z >= -2f && (transform.position.x - player.transform.position.x >=  0.18f && transform.position.x - player.transform.position.x <=  0.25f) && (transform.position.x - player.transform.position.x <=  -0.18f && transform.position.x - player.transform.position.x >=  -0.25f)) {
//			player.GetComponent<heavyBikeTurns>().rampControl=true;
//			player.GetComponent<heavyBikeTurns>().RampStarted();
//		
//		}
	
		if ((transform.position.x - player.transform.position.x >=  0.18f && transform.position.x - player.transform.position.x <=  0.27f)&& transform.position.z - player.transform.position.z <= 3f && transform.position.z - player.transform.position.z >= -0.25f) {
			//print ((transform.position.z - player.transform.position.z )+" moveplayer "+ (transform.position.x - player.transform.position.x));
			movePlayer=true;
			player.GetComponent<heavyBikeTurns>().rampControl=true;
			player.GetComponent<heavyBikeTurns>().flyoverStart=true;
		}
		if ((transform.position.x - player.transform.position.x <=  -0.18f && transform.position.x - player.transform.position.x >=  -0.27f)&& transform.position.z - player.transform.position.z <= 3f && transform.position.z - player.transform.position.z >= -0.25f) {
			//print ((transform.position.z - player.transform.position.z )+" rightpos "+ (transform.position.x - player.transform.position.x));
			movePlayer=true;
			player.GetComponent<heavyBikeTurns>().rampControl=true;
			player.GetComponent<heavyBikeTurns>().flyoverStart=true;
			pos=rightPos;
		}
		if (movePlayer) {
		  player.GetComponent<tiltControl>().z_position= Mathf.Lerp(player.transform.position.x,pos,1f);
			//print ("inside bool "+  player.GetComponent<tiltControl>().z_position);
			if(player.transform.position.x==pos)
			{
				movePlayer=false;
			}

		}
	
	}

}
