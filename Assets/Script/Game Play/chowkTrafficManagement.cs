using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class chowkTrafficManagement : MonoBehaviour {
	
	List<float> Zpos = new List<float>();
	public Transform[] hurdles;


	int totalPosition;

	void Awake () {

	


			totalPosition = 16;
			SetzPosition ();
		
	
	}
	
	
	
	void placeHurdles()
	{
		
		for (int i=0; i<hurdles.Length; i++) {
			int random=Random.Range(0,totalPosition);
		//	print (hurdles[i].name+ " "+ i+ " "+ Zpos[random]);
			hurdles[i].position=new Vector3(hurdles[i].position.x,hurdles[i].position.y,Zpos[random]);	
			Zpos.RemoveAt(random);
			totalPosition-=1;
			
		}
		
	}


	void SetzPosition()
	{


		Zpos.Add (-847.21f);
		Zpos.Add(-753f);
		Zpos.Add(-656.95f);
		Zpos.Add(-561.51f);
		Zpos.Add(-466.42f);
		Zpos.Add(-370.65f);
		Zpos.Add(-275.9f);
		Zpos.Add(-179.6f);
		Zpos.Add(-85.46f);
		Zpos.Add(10.25f);
		Zpos.Add(105.13f);
		Zpos.Add(200.91f);
		Zpos.Add(295.45f);
		Zpos.Add(390.81f);
		Zpos.Add(486.07f);
		Zpos.Add(581.47f);
//		Zpos.Add(676.76f);
//		Zpos.Add(772.06f);
//		Zpos.Add(867.2f);
//		Zpos.Add(962.58f);
//		Zpos.Add(1057.9f);
//		Zpos.Add(6625.7f);
//		Zpos.Add(6922.3f);
//		Zpos.Add(7227.7f);
//		Zpos.Add(7517.9f);

	


		
			

		placeHurdles ();
		
	}
}
