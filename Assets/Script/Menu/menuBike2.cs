using UnityEngine;
using System.Collections;

public class menuBike2 : MonoBehaviour {

	public Transform player;
	public Transform childplayer;	
	int level;
	public float translatespeed;
	public AnimationClip race;
	public static int count;

	public Transform wheelFL ;	
	public Transform wheelRL ;
	// Use this for initialization
	void Start () {
		checkSound ();
		count = 0;
		childplayer.GetComponent<Animation>() [race.name].speed = 2.5f;
		childplayer.GetComponent<Animation>().Play (race.name,PlayMode.StopAll);
		//animation [race.name].speed = 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		checkSound ();
		//transform.Translate (-transform.forward*40*Time.deltaTime);
		wheelFL.Rotate (0, 0, 400f * Time.deltaTime);

		wheelRL.Rotate (0, 0, 400f * Time.deltaTime);



	
	}
	void checkSound()
	{
		if (PlayerPrefs.GetInt ("SoundOff") == 0) {
			if(!GetComponent<AudioSource>().isPlaying)
						GetComponent<AudioSource>().Play ();
				}
		else
			GetComponent<AudioSource>().Pause ();
	}
}
