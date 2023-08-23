using UnityEngine;
using System.Collections;

public class shieldPosition : MonoBehaviour {
	public Transform bike;
	bool isCoins;
	// Use this for initialization
	void start(){
		isCoins = false;
		if (transform.name.Equals ("coin collect Effect")) {
			isCoins=true;
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(isCoins)
			transform.position =new Vector3(transform.position.x,bike.position.y, bike.position.z);
		else
			transform.position = bike.position;
	}
}
