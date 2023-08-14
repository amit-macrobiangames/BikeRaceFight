using UnityEngine;

public class achievementsPreference : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey ("firstTime")) {
			PlayerPrefs.SetString("firstTime","false");
						PlayerPrefs.SetInt ("totalOpponentCrashed", 0);

						PlayerPrefs.SetInt ("boost/timerUsed", 0);
						PlayerPrefs.SetInt ("OpponentCrashedUsingPistol", 0);
						PlayerPrefs.SetString ("shotgunUnlocked", "false");
						PlayerPrefs.SetInt ("OpponentCrashedUsingShotgun", 0);
						PlayerPrefs.SetFloat ("totalDistaneCovered", 0);
						PlayerPrefs.SetInt ("5000Score", 0);
						PlayerPrefs.SetInt ("OpponentCrashedUsingAxe", 0);
		
						PlayerPrefs.SetInt ("ownBikeCostume", 0);
						PlayerPrefs.SetInt ("helmetUsed", 0);
						PlayerPrefs.SetInt ("ticketUsed", 0);
						PlayerPrefs.SetString ("fireStormDestroyed", "false");
		
						PlayerPrefs.SetInt ("shieldUsed", 0);
						PlayerPrefs.SetInt ("scoreShared", 0);
		
						PlayerPrefs.SetInt ("NoOfscorePosted", 0);

				}
		
	}
	

}
