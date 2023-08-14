using UnityEngine;
using System.Collections;

public class ringLocation : MonoBehaviour {

	public Transform[] levels;
	// Use this for initialization

	void OnEnable(){
		print ("levles: "+PlayerPrefs.GetInt("levels"));
		for (var i = 0; i< levels.Length; i++) {
			if(PlayerPrefs.GetInt("levels")==(i)){
				transform.parent=levels[i-1];	
			}
		}
		transform.localPosition = new Vector3 (4.035f,0f,3.63f);
	}
	

}
