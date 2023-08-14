using UnityEngine;
using System.Collections;

public class magnetScript : MonoBehaviour {
	float[] xAxisLanes =new float[4];
	public GameObject magnetEffect;
	public static bool magnetOn;
	public Transform player;
	public AudioClip audioSound;

	bool once;
	// Use this for initialization
	void Start () {
		xAxisLanes [0] = 4.118f;
		xAxisLanes [1] = 3.129f;
		xAxisLanes [2] = 1.828f;
		xAxisLanes [3] = 2.485f;
		once = true;
		magnetOn = false;
		player=player.GetComponent<heavyBikeTurns>().player.transform;		
		

	}
	
	// Update is called once per frame
	void Update () {
		if ((transform.position.z - player.position.z) <= -1f) {
			int xaxis=Random.Range(0,3);
			
			transform.position=new Vector3(xAxisLanes[xaxis],transform.position.y,transform.position.z+500);
			
		}
	}
	void reset()
	{
		once = true;
		magnetOn = false;
		magnetEffect.SetActive(false);
	}
	void OnTriggerEnter(Collider col)
	{
		
		//		print ("trigger");
		
		if (col.gameObject.tag.Equals ("Player")) {
			if (once) {
				int xaxis = Random.Range (0, 3);
				transform.position = new Vector3 (xAxisLanes [xaxis], transform.position.y, transform.position.z + 500);
				once = false;
				player.GetComponent<AudioSource>().PlayOneShot(audioSound);
				Invoke ("reset", 15f);
				magnetOn=true;	
				magnetEffect.SetActive(true);
				}	
		}
	}


}
