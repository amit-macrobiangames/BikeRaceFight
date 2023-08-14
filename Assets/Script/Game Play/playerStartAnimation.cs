using UnityEngine;
using System.Collections;

public class playerStartAnimation : MonoBehaviour {
	public AnimationClip accelerate,drift;
	public GameObject nitro,smoke,skid;
	// Use this for initialization
	void Start () {
		transform.GetComponent<Animation>() [accelerate.name].speed = 2f;
		StartCoroutine ("accelerateBike");
	}
	IEnumerator accelerateBike()
	{
		yield return new WaitForSeconds(2f);
		transform.GetComponent<Animation>().Play (accelerate.name,PlayMode.StopAll);
		Invoke ("driftBike", 2.75f);
		yield return new WaitForSeconds(0.75f);
		nitro.SetActive (true);

	}
	void driftBike()
	{
		transform.GetComponent<Animation>().Play (drift.name,PlayMode.StopAll);
		smoke.SetActive (true);
		Invoke ("smokeoff", 7.5f);
	}
	void smokeoff()
	{
		smoke.SetActive (false);
		skid.SetActive (false);

	}
	// Update is called once per frame
	void Update () {
	
	}
}
