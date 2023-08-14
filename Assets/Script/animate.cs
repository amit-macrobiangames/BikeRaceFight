using UnityEngine;
using System.Collections;

public class animate : MonoBehaviour {
	public AnimationClip punch,kick,stunt1,onewheeling;
	public Transform player,shadow,realShadow;
	bool startAnimation;
	public bool flip;
	Vector3 startingPosition;
	// Use this for initialization
	void Start () {
		startAnimation = true;
		startingPosition = transform.root.position;
		if (flip) {
			
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
			
		}

	}
	
	// Update is called once per frame
	void Update () {
		transform.root.eulerAngles = new Vector3 (transform.root.eulerAngles.x,transform.root.eulerAngles.y,0);
		if (startAnimation) {
						player.GetComponent<Animation>().Play (punch.name, PlayMode.StopAll);
						Invoke ("punching", punch.length+1f);
			startAnimation=false;
				}
		if (player.name.Contains ("menu")) {
						if (player.GetComponent<Animation>().IsPlaying ("stunt2")) {
				shadow.gameObject.SetActive (false);	
								realShadow.gameObject.SetActive (true);

						} else {
							
								realShadow.gameObject.SetActive (false);
				shadow.gameObject.SetActive (true);
						}
				}
	}

	void punching()
	{
		player.GetComponent<Animation>().Play (onewheeling.name,PlayMode.StopAll);
		Invoke ("onewheelingon",onewheeling.length+1f);
	}

	void onewheelingon()
	{
		player.GetComponent<Animation>().Play (kick.name,PlayMode.StopAll);
		Invoke ("kicking",kick.length+1f);
	}

	void kicking()
	{
		startAnimation = true;
	}


	void OnTriggerEnter(Collider col)
	{
//		print ("trigger: "+ col.name);
				if (col.name.Contains ("respawn")) {
		//	print ("respawn");
			transform.parent.position=startingPosition;

				}
		}

}
