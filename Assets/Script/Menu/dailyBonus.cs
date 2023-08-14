using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Timers;
using System.IO;
using System.Collections.Generic;
using System.Linq;
public class dailyBonus : MonoBehaviour {

	static DateTime currentTime,lastPlayedTime,nextDay;
	static string lastPlayedstring,nextDaystring;
	static TimeSpan timeDifference,timeDiff24HrFt;

	int NoOfReward;
	double hours ;

	void giveBonus()
	{
		//PlayerPrefs.SetInt ("Tickets",(0));
		NoOfReward=(int) (hours/24);
//		print ("no.ofrewards: "+NoOfReward);
		if (hours >= 24 && NoOfReward>0) {
			PlayerPrefs.SetString ("giveBonus", "true");
			currentTime = System.DateTime.Now;
			nextDay = currentTime.AddDays (1);
			PlayerPrefs.SetString ("nextDate", nextDay.ToString ());
			PlayerPrefs.SetString ("lastPlayed", currentTime.ToString ());
			PlayerPrefs.SetInt ("Tickets",(PlayerPrefs.GetInt ("Tickets")+ NoOfReward));
			//print ("No.Oftickets "+ PlayerPrefs.GetInt ("Tickets"));

		} else {
	
			PlayerPrefs.SetString ("giveBonus", "false");
			
		}
	}

	void OnEnable() {
//		print ("enabled");
		StartCoroutine ("timer",1f);
	}
	 void Awake()
	{
		//PlayerPrefs.DeleteKey ("nextDate");
		//PlayerPrefs.DeleteAll ();
		//DontDestroyOnLoad (this);
		NoOfReward = 0;
		currentTime = System.DateTime.Now;





		if (!PlayerPrefs.HasKey ("nextDate")) {
						nextDay = currentTime.AddDays (1);
						PlayerPrefs.SetString ("nextDate", nextDay.ToString ());
				} else {
				
			nextDaystring=PlayerPrefs.GetString("nextDate");
			nextDay=System.Convert.ToDateTime (nextDaystring);
		}
		if (!PlayerPrefs.HasKey ("lastPlayed")) {
								PlayerPrefs.SetString ("lastPlayed", currentTime.ToString ());
								PlayerPrefs.Save ();
			hours=24;
			giveBonus();

						} else {
						
					lastPlayedstring=PlayerPrefs.GetString("lastPlayed");
					
					lastPlayedTime=System.Convert.ToDateTime (lastPlayedstring);					
			timeDifference=(currentTime - lastPlayedTime);
					hours = (timeDifference).TotalHours;
				
						giveBonus();
				}


		StartCoroutine ("timer",1f);


 
//		print (lastPlayedTime+"    current "+ currentTime+ "   next"+ nextDay);
	}


	double hour, minute,second;
IEnumerator timer()
	{
		while (true) {
						currentTime = System.DateTime.Now;
						lastPlayedstring = PlayerPrefs.GetString ("lastPlayed");
		
						lastPlayedTime = System.Convert.ToDateTime (lastPlayedstring);					
						nextDaystring =	PlayerPrefs.GetString ("nextDate");

					//	timeDifference=(currentTime - lastPlayedTime);
						timeDifference=	nextDay.Subtract(currentTime);
						hours = timeDifference.TotalHours;


		

		//if(hours<24){
			//if(hours>=24){
			if((timeDifference.Hours)<=0 && (timeDifference.Minutes)<=0 && (timeDifference.Seconds)<=0){
			
				giveBonus();
			}


		
//			print ("difference: "+(nextDay.Subtract(currentTime)));


			//string ft=	string.Format("{0:D2}:{1:D2}:{2:D2}",(24-timeDifference.Hours),(60-timeDifference.Minutes),( 60-timeDifference.Seconds));

//			print (hours+ "\n"+ ft);
//			}
//			else
//			{
//
//			}
			yield return new WaitForSeconds(1f);
				}
	}



	
}
