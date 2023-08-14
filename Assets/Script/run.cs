using UnityEngine;
using System.Collections;

public class run : MonoBehaviour {

	public AnimationClip race1;
	public Rotate1 rotate;
	
	
	// Use this for initialization
	void Start () {
		
		child ();
	}
	
	
	public void	child()
	{
		transform.GetComponent<Animation>().Play (race1.name,PlayMode.StopAll);
		Invoke ("startRotation",(race1.length+0.25f));
	
	}

	public void	startRotation()
	{

		//biker.parent = bike;
		rotate.enabled = true;
	
	}
}
