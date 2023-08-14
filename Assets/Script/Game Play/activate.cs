using UnityEngine;
using System.Collections;

public class activate : MonoBehaviour {
	public GameObject biker3;
	// Use this for initialization
	void Start () {
		Invoke ("startBike",1.25f);
	}
	
	// Update is called once per frame
	void startBike()
	{
		biker3.SetActive (true);
	}
}
