using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class rampManagment : MonoBehaviour {
	
	List<float> Zpos = new List<float>();
	public Transform[] hurdles;
	public GameObject hurdlesParent;
	public float zPosition;
	int totalPosition;
	int level;
	void Start () {
		level = PlayerPrefs.GetInt ("levels");
			level = 3;
		if (level == 3) {
			hurdlesParent.SetActive(true);
			totalPosition = 23;
			SetzPosition ();
		}
		else
			
			hurdlesParent.SetActive(false);
	}
	
	
	
	void placeHurdles()
	{

		hurdles[0].position=new Vector3(hurdles[0].position.x,0,Zpos[12]);	
		hurdles[1].position=new Vector3(hurdles[1].position.x,0,Zpos[16]);	
	
		Zpos.RemoveAt (16);
		Zpos.RemoveAt (12);

		for (int i=2; i<hurdles.Length; i++) {
			//print (""+ Zpos[i]);
			int random=Random.Range(0,totalPosition);
			hurdles[i].position=new Vector3(hurdles[i].position.x,0,Zpos[random]);	
			Zpos.RemoveAt(random);
			totalPosition-=1;
			
		}
		
	}
	void SetzPosition()
	{
		for (int i = 0; i<25; i++) {
			if(i<16)
				zPosition+=300f;
			else
				zPosition+=250f;
			Zpos.Add(zPosition);

			
		}
		placeHurdles ();
		
	}
}
