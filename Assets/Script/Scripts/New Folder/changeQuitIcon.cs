using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class changeQuitIcon : MonoBehaviour {
	

	public Image PPHicon;
	public Sprite icon1,icon2;

	public static float deltaTime;
	private static float _lastframetime;

	bool resetImage;
	float imageTimer=0;
	// Use this for initialization
	void Start () {
	
		_lastframetime = Time.realtimeSinceStartup;

	}
	

	// Update is called once per frame
	void Update () {
		fillBar ();
	
	}


	// Update is called once per frame
	void fillBar () {
		//		print ("revive");

			deltaTime = (Time.realtimeSinceStartup - _lastframetime);
			_lastframetime = Time.realtimeSinceStartup;
			
					imageTimer+=deltaTime;
					if(imageTimer>0.5f)
					{
						resetImages ();
						imageTimer=0;
					}
					
		
	}
	
	void resetImages (){
		if (resetImage) {
			PPHicon.sprite=icon1;

		} else {
			PPHicon.sprite=icon2;
		}
		resetImage = !resetImage;
	}

	
}