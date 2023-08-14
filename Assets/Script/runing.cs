using UnityEngine;
using System.Collections;

public class runing : MonoBehaviour {
	public Transform biker,bike;
	public AnimationClip race1,race;

	
	
	// Use this for initialization
	void Start () {

		child();
	}

//	void OnEnable()
//	{
//		print (transform.name +" enabled");
//		animation [race.name].speed = 2f;
//		transform.animation.Play (race.name,PlayMode.StopAll);
//		startRotation ();
//		//Invoke ("startRotation",(race1.length+0.25f));
//	}
public void	child()
	{
		transform.GetComponent<Animation>().Play (race1.name,PlayMode.StopAll);
		print ("child");
		Invoke ("startRotation",(race1.length+0.25f));

	}

	public void	startRotation()
	{
		
		biker.parent = bike;
		bike.GetComponent<Animation>().Play ("heavyBike");
		
	}
}
