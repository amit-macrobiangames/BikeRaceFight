using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class rewardedVideo : MonoBehaviour {

	string currentItemName;
	int noOfItemPurchased;

	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetString ("GetReward").Equals ("true")) {
			PlayerPrefs.SetString("GetReward","false");
			PlayerPrefs.Save();

			noOfItemPurchased=1;
			
			 if(PlayerPrefs.GetString("currentlyPurchasedItem").Contains("helmet"))
			{

				//print ("before helmets purchased: "+PlayerPrefs.GetInt("helmets"));
				PlayerPrefs.SetInt("helmets", (PlayerPrefs.GetInt("helmets")+ noOfItemPurchased));
				PlayerPrefs.Save();
				//print ("before helmets purchased: "+PlayerPrefs.GetInt("helmets"));
				if(SceneManager.GetActiveScene().name.Contains("desert"))
				{
					//PlayerPrefs.SetInt("helmets", (PlayerPrefs.GetInt("helmets")-1));
					//PlayerPrefs.Save();
					GameObject.FindGameObjectWithTag("Player").transform.root.GetComponent<revivePlayer> ().reStartPlayer();
					
				}
			}
			
			else if(PlayerPrefs.GetString("currentlyPurchasedItem").Contains("shield"))
			{

				PlayerPrefs.SetInt("shields", (PlayerPrefs.GetInt("shields")+ noOfItemPurchased));
				PlayerPrefs.Save();
				if(SceneManager.GetActiveScene().name.Contains("desert"))
				{
					
					GameObject.FindGameObjectWithTag("Player").transform.root.GetComponent<weaponAI> ().closeBundle();
					
				}
				
			}
			
			else if(PlayerPrefs.GetString("currentlyPurchasedItem").Contains("time"))
			{
				noOfItemPurchased=3;
				//print ("before timers purchased: "+PlayerPrefs.GetInt("timers"));
				
				PlayerPrefs.SetInt("timers", (PlayerPrefs.GetInt("timers")+ noOfItemPurchased));
				PlayerPrefs.Save();
				//print ("after timers purchased: "+PlayerPrefs.GetInt("timers"));
				if(SceneManager.GetActiveScene().name.Contains("desert"))
				{
					timerCollection();
					GameObject.FindGameObjectWithTag("MainCamera").transform.root.GetComponent<PhycamViews> ().closeTimerBundle();
					
				}
				
			}
			
			else if(PlayerPrefs.GetString("currentlyPurchasedItem").Contains("boost"))
			{
			
				//print ("before boost purchased: "+PlayerPrefs.GetInt("boosts"));
				
				PlayerPrefs.SetInt("boosts", (PlayerPrefs.GetInt("boosts")+ noOfItemPurchased));
				PlayerPrefs.Save();
			//	print ("after boost purchased: "+PlayerPrefs.GetInt("boosts"));
				if(SceneManager.GetActiveScene().name.Contains("desert"))
				{
					boostCollection();
					GameObject.FindGameObjectWithTag("Player").transform.root.GetComponent<weaponAI> ().closeBundle();
					
				}
			}
			
			else if(PlayerPrefs.GetString("currentlyPurchasedItem").Contains("bullets"))
			{
				noOfItemPurchased=3;
				PlayerPrefs.SetInt("ammos", (PlayerPrefs.GetInt("ammos")+ noOfItemPurchased));
				PlayerPrefs.Save();
				if(SceneManager.GetActiveScene().name.Contains("desert"))
				{
					
					GameObject.FindGameObjectWithTag("Player").transform.root.GetComponent<weaponAI> ().closeBundle();
					
				}
			}
			else if(PlayerPrefs.GetString("currentlyPurchasedItem").Contains("missile"))
			{
				noOfItemPurchased=2;
				PlayerPrefs.SetInt("missile", (PlayerPrefs.GetInt("missile")+ noOfItemPurchased));
				PlayerPrefs.Save();
				if(SceneManager.GetActiveScene().name.Contains("desert"))
				{
					
					GameObject.FindGameObjectWithTag("Player").transform.root.GetComponent<weaponAI> ().closeBundle();
					
				}
			}
		}
	}

	public void rewardedVideoFtn(string itemName)
	{
		currentItemName =itemName;
		//print (currentItemName);
		PlayerPrefs.SetString ("currentlyPurchasedItem",currentItemName);
		//#if !UNITY_EDITOR
		//Adcontrol.instace.ShowRewardedVideo();
		//#endif
	}

	void  boostCollection()
	{
		weaponAI.startedValue = PlayerPrefs.GetInt ("boosts");
		weaponAI.updatedValue = 0;
		GameObject.FindGameObjectWithTag("Player").transform.root.GetComponent<weaponAI> ().boostFG.fillAmount =1;
	}
	
	void  timerCollection()
	{
		PhycamViews.startedValue = PlayerPrefs.GetInt ("timers");
		PhycamViews.updatedValue = 0;
		GameObject.FindGameObjectWithTag("Player").transform.root.GetComponent<heavyBikeTurns> ().mainCamera.timerFG.fillAmount =1;
	}
}
