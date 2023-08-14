using UnityEngine;
using System.Collections;

public class missileHitEffect : MonoBehaviour {
	float speed;
	// Use this for initialization
	void Awake () {
	//	speed = Random.Range (200,275);
		transform.GetComponent<Rigidbody>().velocity = new Vector3 (Random.Range(1,4),Random.Range(2,6),Random.Range(1,4));
		transform.GetComponent<Rigidbody>().AddForce (new Vector3 (Random.Range(1,4),Random.Range(2,6), Random.Range(1,4)),ForceMode.Force);

	}
	

}
