using UnityEngine;
using System.Collections;

public class barrierFlew : MonoBehaviour {
	bool forceonce;
	public Transform player;
	public GameObject DestroyedObject;
	heavyBikeTurnControls heavyBikeScript;
	turnLevelcontrols harleyBikescript;
	int levelID;
	public AudioClip hitSound;
	// Use this for initialization
	void Start () {
		forceonce = true;
		Transform child= player.GetComponent<heavyBikeTurns>().player.transform;
		if (child.name.Contains ("Fat")) {
						levelID = 0;
			harleyBikescript=	child.GetComponent<turnLevelcontrols> ();
				} else if (child.name.Contains ("Player")) {
						levelID = 1;
			heavyBikeScript=	child.GetComponent<heavyBikeTurnControls> ();
				}
	}
	
	// Update is called once per frame
	
	void Update()
	{




		//if (player.GetComponent<heavyBikeTurns> ().gangsterHit) {
						if (Vector3.Distance (transform.position, player.position) <= 3f) {
								//print ("inside");
								collision ();
		
						}
				//}
	}
	void collision()
	{

			Rigidbody rb=	gameObject.AddComponent<Rigidbody>();
			transform.tag="barrel";
			rb.constraints=RigidbodyConstraints.None;
				rb.velocity = new Vector3 (0, 0, 150);
				rb.AddForce (new Vector3 (0, 0, 150), ForceMode.Force);//=-1*gameObject.rigidbody.velocity;
			if (levelID == 1)
				heavyBikeScript.colliding ();
			else
				harleyBikescript.colliding ();

				
				forceonce=false;
				Invoke("disable",3f);
				

				
				
			
			

	}
	
	void disable()
	{
		transform.gameObject.SetActive (false);
	}
}
