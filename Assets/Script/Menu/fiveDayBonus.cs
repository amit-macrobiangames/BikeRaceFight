using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Timers;
using System.IO;
using System.Collections.Generic;
using System.Linq;
public class fiveDayBonus : MonoBehaviour
{
    public AudioClip getSound;
    public Text day2Timer, day3Timer, day4Timer;
    public GameObject day1Get, day2Get, day3Get, day4Get, day2TimerGameObject, day3TimerGameObject, day4TimerGameObject, done1, done2, done3, done4;
    static DateTime currentTime, lastPlayedTime, nextDay, dayAfterTomorrow, finalDay, resetTime;
    static string lastPlayedstring, nextDaystring, dayAfterTomorrowstring, finalDaystring, resetTimestring;
    static TimeSpan nextDaySpan, dayAfterTomorrowSpan, finalDaySpan;

    int NoOfReward;
    double nextDayhours, dayAfterTomorrowhours, finalDayhours, resetTimehours;

    bool day2BonusReady, day3BonusReady, day4BonusReady;
    public static bool isDone1, isDone2, isDone3, isDone4;

    void resetDailyBonus()
    {
        Debug.Log("resetDailyBonus");
        PlayerPrefs.SetString("resetTime", currentTime.ToString());

        nextDay = currentTime.AddDays(1);
        PlayerPrefs.SetString("tomorrow", nextDay.ToString());
        dayAfterTomorrow = currentTime.AddDays(2);
        PlayerPrefs.SetString("dayAfterTomorrow", dayAfterTomorrow.ToString());
        finalDay = currentTime.AddDays(3);
        PlayerPrefs.SetString("finalDay", finalDay.ToString());

        PlayerPrefs.SetString("lastTimeButtonClicked", currentTime.ToString());
        PlayerPrefs.Save();

        PlayerPrefs.SetString("Day1Bonus", "false");
        PlayerPrefs.SetString("Day2Bonus", "false");
        PlayerPrefs.SetString("Day3Bonus", "false");
        PlayerPrefs.SetString("Day4Bonus", "false");


        day1Get.SetActive(true);
        day2TimerGameObject.SetActive(true);
        day3TimerGameObject.SetActive(true);
        day4TimerGameObject.SetActive(true);

        done1.SetActive(false);
        done2.SetActive(false);
        done3.SetActive(false);
        done4.SetActive(false);

        day2Get.SetActive(false);
        day3Get.SetActive(false);
        day4Get.SetActive(false);
        isDone1 = false;
        isDone2 = false;
        isDone3 = false;
        isDone4 = false;
    }


    void finishDailyBonus()
    {
        PlayerPrefs.SetString("finishedBonus", "true");
        PlayerPrefs.SetString("resetTime", (currentTime.AddDays(1)).ToString());
        PlayerPrefs.Save();
    }
    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt("SoundOff") == 0)
        {
            GetComponent<AudioSource>().enabled = true;
        }
        else
            GetComponent<AudioSource>().enabled = false;

        if (PlayerPrefs.GetString("Day1Bonus").Equals("true"))
        {
            done1.SetActive(true);
            day1Get.SetActive(false);
            isDone1 = true;
        }
        else
        {
            done1.SetActive(false);
            day1Get.SetActive(true);
            Debug.Log("Get1");

        }


        if (PlayerPrefs.GetString("Day2Bonus").Equals("true"))
        {
            isDone2 = false;
            done2.SetActive(true);
            day2Get.SetActive(false);
            day2TimerGameObject.SetActive(false);
        }

        if (PlayerPrefs.GetString("Day3Bonus").Equals("true"))
        {
            isDone3 = false;
            done3.SetActive(true);
            day3Get.SetActive(false);
            day3TimerGameObject.SetActive(false);
        }

        if (PlayerPrefs.GetString("Day4Bonus").Equals("true"))
        {
            isDone4 = false;
            done4.SetActive(true);
            day4Get.SetActive(false);
            day4TimerGameObject.SetActive(false);
        }



        NoOfReward = 0;
        currentTime = System.DateTime.Now;



        if (!PlayerPrefs.HasKey("resetTime"))
            PlayerPrefs.SetString("resetTime", currentTime.ToString());
        else
        {
            resetTimestring = PlayerPrefs.GetString("resetTime");
            resetTime = System.Convert.ToDateTime(resetTimestring);
            resetTimehours = (currentTime - resetTime).TotalHours;
        }
        
        if (resetTimehours >= 0 && PlayerPrefs.GetString("finishedBonus").Equals("true"))
        {
            PlayerPrefs.SetString("finishedBonus", "false");
            resetDailyBonus();
        }


        if (!PlayerPrefs.HasKey("finalDay"))
        {
            finalDay = currentTime.AddDays(3);
            PlayerPrefs.SetString("finalDay", finalDay.ToString());
        }
        else
        {

            finalDaystring = PlayerPrefs.GetString("finalDay");
            finalDay = System.Convert.ToDateTime(finalDaystring);
        }


        if (!PlayerPrefs.HasKey("tomorrow"))
        {
            nextDay = currentTime.AddDays(1);
            PlayerPrefs.SetString("tomorrow", nextDay.ToString());
        }
        else
        {

            nextDaystring = PlayerPrefs.GetString("tomorrow");
            nextDay = System.Convert.ToDateTime(nextDaystring);
        }
        if (!PlayerPrefs.HasKey("dayAfterTomorrow"))
        {
            dayAfterTomorrow = currentTime.AddDays(2);
            PlayerPrefs.SetString("dayAfterTomorrow", dayAfterTomorrow.ToString());
        }
        else
        {

            dayAfterTomorrowstring = PlayerPrefs.GetString("dayAfterTomorrow");
            dayAfterTomorrow = System.Convert.ToDateTime(dayAfterTomorrowstring);
        }




        if (!PlayerPrefs.HasKey("lastTimeButtonClicked"))
        {
            PlayerPrefs.SetString("lastTimeButtonClicked", currentTime.ToString());
            PlayerPrefs.Save();



        }
        else
        {

            lastPlayedstring = PlayerPrefs.GetString("lastTimeButtonClicked");

            lastPlayedTime = System.Convert.ToDateTime(lastPlayedstring);
            nextDaySpan = (currentTime - lastPlayedTime);
            nextDayhours = (nextDaySpan).TotalHours;


        }
        //		

        StartCoroutine("timer", 1f);



        //		print (lastPlayedTime+"    current "+ currentTime+ "   next"+ nextDay);
    }





    public void giveBonus()
    {
        if (isDone1 == false)
        {
            isDone1 = true;
            Debug.Log("Get1 Done");
            PlayerPrefs.SetString("Day1Bonus", "true");
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("Tickets", (PlayerPrefs.GetInt("Tickets") + 1));
            PlayerPrefs.SetInt("cash", (PlayerPrefs.GetInt("cash") + 1000));
            PlayerPrefs.Save();
            GetComponent<AudioSource>().PlayOneShot(getSound);

            done1.SetActive(true);
            day1Get.SetActive(false);
        }
        else
        {
            done1.SetActive(true);
            day1Get.SetActive(false);
        }
    }



    public void givetwoDayBonus()
    {
        if (isDone2 == false)
        {
            isDone2 = true;
            Debug.Log("Get2 Done");
            PlayerPrefs.SetString("Day2Bonus", "true");

            PlayerPrefs.SetInt("Tickets", (PlayerPrefs.GetInt("Tickets") + 1));
            PlayerPrefs.SetInt("cash", (PlayerPrefs.GetInt("cash") + 2000));
            PlayerPrefs.Save();
            GetComponent<AudioSource>().PlayOneShot(getSound);
            day2BonusReady = false;
            done2.SetActive(true);
            day2Get.SetActive(false);

        }

    }
    public void giveThreeDayBonus()
    {
        if (isDone3 == false)
        {
            isDone3 = true;
            Debug.Log("Get3 Done");
            PlayerPrefs.SetString("Day3Bonus", "true");

            PlayerPrefs.SetInt("Tickets", (PlayerPrefs.GetInt("Tickets") + 1));
            PlayerPrefs.SetInt("cash", (PlayerPrefs.GetInt("cash") + 5000));
            PlayerPrefs.Save();
            GetComponent<AudioSource>().PlayOneShot(getSound);
            done3.SetActive(true);
            day3Get.SetActive(false);
            day3BonusReady = false;

        }
    }
    public void givefourDayBonus()
    {
        if (isDone4 == false)
        {
            isDone4 = true;
            Debug.Log("Get4 Done");
            PlayerPrefs.SetString("Day4Bonus", "true");
            PlayerPrefs.SetInt("Tickets", (PlayerPrefs.GetInt("Tickets") + 2));
            PlayerPrefs.SetInt("cash", (PlayerPrefs.GetInt("cash") + 7000));
            PlayerPrefs.Save();
            GetComponent<AudioSource>().PlayOneShot(getSound);
            done4.SetActive(true);
            day4Get.SetActive(false);
            day4BonusReady = false;

            finishDailyBonus();
        }

    }


    IEnumerator timer()
    {
        while (true)
        {
            currentTime = System.DateTime.Now;

            lastPlayedstring = PlayerPrefs.GetString("lastTimeButtonClicked");

            resetTimestring = PlayerPrefs.GetString("resetTime");
            resetTime = System.Convert.ToDateTime(resetTimestring);
            resetTimehours = (currentTime - resetTime).TotalHours;
            //			print ("resetHours: "+resetTimehours);
            if (resetTimehours >= 0 && PlayerPrefs.GetString("finishedBonus").Equals("true"))
            {
                PlayerPrefs.SetString("finishedBonus", "false");
                resetDailyBonus();
            }



            lastPlayedTime = System.Convert.ToDateTime(lastPlayedstring);
            nextDaystring = PlayerPrefs.GetString("tomorrow");

            //	timeDifference=(currentTime - lastPlayedTime);
            nextDaySpan = nextDay.Subtract(currentTime);
            nextDayhours = nextDaySpan.TotalHours;
            day2Timer.text = string.Format("{0}:{1}:{2}", ((int)nextDaySpan.TotalHours), nextDaySpan.Minutes, nextDaySpan.Seconds);

            if ((nextDaySpan.Hours) <= 0 && (nextDaySpan.Minutes) <= 0 && (nextDaySpan.Seconds) <= 0)
            {
                if (!PlayerPrefs.GetString("Day2Bonus").Equals("true"))
                {
                    day2BonusReady = true;
                    day2TimerGameObject.SetActive(false);
                    day2Get.SetActive(true);
                    //Debug.Log("Get2");
                }
                //				if(!PlayerPrefs.GetString ("Day2Bonus").Equals("true") )
                //				{
                //				givetwoDayBonus();
                //				}


            }

            dayAfterTomorrowSpan = dayAfterTomorrow.Subtract(currentTime);
            dayAfterTomorrowhours = dayAfterTomorrowSpan.TotalHours;
            day3Timer.text = string.Format("{0}:{1}:{2}", ((int)dayAfterTomorrowSpan.TotalHours), dayAfterTomorrowSpan.Minutes, dayAfterTomorrowSpan.Seconds);

            if ((dayAfterTomorrowSpan.Hours) <= 0 && (dayAfterTomorrowSpan.Minutes) <= 0 && (dayAfterTomorrowSpan.Seconds) <= 0)
            {
                if (!PlayerPrefs.GetString("Day3Bonus").Equals("true"))
                {

                    day3BonusReady = true;
                    day3TimerGameObject.SetActive(false);
                    day3Get.SetActive(true);
                    Debug.Log("Get3");
                }
                //				if(!PlayerPrefs.GetString ("Day3Bonus").Equals("true") )
                //				{
                //					giveThreeDayBonus();
                //				}


            }




            finalDaySpan = finalDay.Subtract(currentTime);
            finalDayhours = finalDaySpan.TotalHours;
            day4Timer.text = string.Format("{0}:{1}:{2}", ((int)finalDaySpan.TotalHours), finalDaySpan.Minutes, finalDaySpan.Seconds);

            if ((finalDaySpan.Hours) <= 0 && (finalDaySpan.Minutes) <= 0 && (finalDaySpan.Seconds) <= 0)
            {
                if (!PlayerPrefs.GetString("Day4Bonus").Equals("true"))
                {
                    day4BonusReady = true;
                    day4TimerGameObject.SetActive(false);
                    day4Get.SetActive(true);
                    Debug.Log("Get4");
                }
                //				if(!PlayerPrefs.GetString ("Day4Bonus").Equals("true") )
                //				{
                //					givefourDayBonus();
                //				}


            }




            if ((day2BonusReady && day3BonusReady) || (day3BonusReady && day4BonusReady))
                resetDailyBonus(); 
    

            string time = string.Format("{0}:{1}:{2}", ((int)finalDaySpan.TotalHours), finalDaySpan.Minutes, finalDaySpan.Seconds);
            //print ("difference: "+nextDayhours+ "    "+ dayAfterTomorrowhours+ "   "+ finalDayhours);

            //			print(time);
            yield return new WaitForSeconds(1f);
        }
    }









}
