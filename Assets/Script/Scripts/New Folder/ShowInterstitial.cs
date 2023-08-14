using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInterstitial : MonoBehaviour {

	// Use this for initialization
	void Start ()
		{
			Invoke ("InterstitialAd",0.35f);
		}
	
		public void InterstitialAd()
		{
#if !UNITY_EDITOR
		Adcontrol.instace.showAd();
#endif

	}
}
