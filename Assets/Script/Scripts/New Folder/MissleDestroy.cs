using UnityEngine;
using System.Collections;

public class MissleDestroy : MonoBehaviour {

	// Use this for initialization
	public static GameObject car;
	public GameObject explosionEffect;
	void Awake ()
	{
		car = GameObject.FindGameObjectWithTag("Player");
	}

	
	// Update is called once per frame
	void Update () {
		if (car.transform.position.z > transform.position.z) {
			Destroy(explosionEffect,0.25f);
					Destroy (gameObject, 0.25f);

				}
		
			
		
	}
}
