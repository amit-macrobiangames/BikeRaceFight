using UnityEngine;
using System.Collections;

public class brokenCrate : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		transform.rigidbody.velocity = new Vector3 (0.25f,2.0f, 0.25f);
//		transform.rigidbody.AddForce (new Vector3 (0.25f,2.0f, 0.25f),ForceMode.Force);

		transform.GetComponent<Rigidbody>().velocity = new Vector3 (Random.Range(1,7),Random.Range(1,7),Random.Range(1,7));
		transform.GetComponent<Rigidbody>().AddForce (new Vector3 (Random.Range(1,7),Random.Range(1,7), Random.Range(1,7)),ForceMode.Force);
	}
	


}
