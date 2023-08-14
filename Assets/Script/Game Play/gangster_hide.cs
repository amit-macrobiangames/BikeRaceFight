using UnityEngine;
using System.Collections;

public class gangster_hide : MonoBehaviour {
	Transform player;

	GameObject target_obj;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		//Invoke ("disable",7f);


		
	}
//	void disable()
//	{
//		Destroy (transform.gameObject);
//	}
	// Update is called once per frame
	void Update () {
		//print ((player.position.z- transform.position.z));

	

			if (transform.position.z - player.position.z <= - 10f) {
			gameObject.SetActive(false);
				//Destroy (transform.gameObject);
			}
		 


	}


}
