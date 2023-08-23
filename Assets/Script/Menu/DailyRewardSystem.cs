using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Timers;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class DailyRewardSystem : MonoBehaviour
{
    public AudioClip getSound;
    private AudioSource _audioSource;
    public Text day2TimerText, day3TimerText, day4TimerText;// to show time remaining
    public GameObject day1Get, day2Get, day3Get, day4Get, done1, done2, done3, done4;
    static DateTime currentTime, day2Time, day3Time, day4Time; //times of four days
    static TimeSpan day2TimeSpan, day3TimeSpan, day4TimeSpan;//format strings
    bool isReward2Collected, isReward3Collected, isReward4Collected;// on reward collected set them true to avoid calculation
    bool isReward2Ready, isReward3Ready, isReward4Ready; //true when reward ready to collect
    void Awake()
    {

        currentTime = System.DateTime.Now;
        if (!PlayerPrefs.HasKey("Day1Bonus"))
        {
            PlayerPrefs.SetString("Day1Bonus", "false");
        }
        if (!PlayerPrefs.HasKey("Day2Bonus"))
        {
            PlayerPrefs.SetString("Day2Bonus", "false");
            day2Time = currentTime.AddDays(1);
            PlayerPrefs.SetString("day2Time", day2Time.ToString());
        }
        if (!PlayerPrefs.HasKey("Day3Bonus"))
        {
            PlayerPrefs.SetString("Day3Bonus", "false");
            day3Time = currentTime.AddDays(2);
            PlayerPrefs.SetString("day3Time", day3Time.ToString());
        }
        if (!PlayerPrefs.HasKey("Day4Bonus"))
        {
            PlayerPrefs.SetString("Day4Bonus", "false");
            day4Time = currentTime.AddDays(3);
            PlayerPrefs.SetString("day4Time", day4Time.ToString());
        }
        PlayerPrefs.Save();
        ResetDailyBonus();
    }
    void ResetDailyBonus()
    {
        if (PlayerPrefs.GetString("Day1Bonus").Equals("true") && PlayerPrefs.GetString("Day2Bonus").Equals("true") && PlayerPrefs.GetString("Day3Bonus").Equals("true") && PlayerPrefs.GetString("Day4Bonus").Equals("true"))
        {
            currentTime = System.DateTime.Now;
            PlayerPrefs.SetString("Day1Bonus", "false");
            PlayerPrefs.SetString("Day2Bonus", "false");
            day2Time = currentTime.AddDays(1);
            day3Time = currentTime.AddDays(2);
            day4Time = currentTime.AddDays(3);
            PlayerPrefs.SetString("day2Time", day2Time.ToString());
            PlayerPrefs.SetString("Day3Bonus", "false");
            PlayerPrefs.SetString("day3Time", day3Time.ToString());
            PlayerPrefs.SetString("Day4Bonus", "false");
            PlayerPrefs.SetString("day4Time", day4Time.ToString());
            PlayerPrefs.Save();
            GetDaysTime();
            isReward2Collected = false;
            isReward3Collected = false;
            isReward4Collected = false;
            done1.SetActive(true);
            done2.SetActive(false);
            done3.SetActive(false);
            done4.SetActive(false);
        }
    }
    void GetDaysTime()
    {
        //Debug.Log("day2Time: " + PlayerPrefs.GetString("day2Time"));
        day2Time = System.Convert.ToDateTime(PlayerPrefs.GetString("day2Time"));
        day3Time = System.Convert.ToDateTime(PlayerPrefs.GetString("day3Time"));
        day4Time = System.Convert.ToDateTime(PlayerPrefs.GetString("day4Time"));
    }
    void Start()
    {

        if (PlayerPrefs.GetString("Day1Bonus").Equals("true"))
        {
            done1.SetActive(true);
            day1Get.SetActive(false);
        }
        else
        {
            done1.SetActive(false);
            day1Get.SetActive(true);
        }
        if (PlayerPrefs.GetString("Day2Bonus").Equals("true"))
        {
            done2.SetActive(true);
            day2Get.SetActive(false);
            isReward2Collected = true;
            day2TimerText.transform.parent.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetString("Day3Bonus").Equals("true"))
        {

            done3.SetActive(true);
            day3Get.SetActive(false);
            isReward3Collected = true;
            day3TimerText.transform.parent.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetString("Day4Bonus").Equals("true"))
        {

            done4.SetActive(true);
            day4Get.SetActive(false);
            isReward4Collected = true;
            day3TimerText.transform.parent.gameObject.SetActive(false);
        }
        GetDaysTime();
        //PlayerPrefs.DeleteAll();

        StartCoroutine(BonusTimeCounter());
        _audioSource = GetComponent<AudioSource>();
    }
    public void GetBonus(int _number)
    {
        switch (_number)
        {
            case 1:
                PlayerPrefs.SetString("Day1Bonus", "true");
                PlayerPrefs.SetInt("Tickets", (PlayerPrefs.GetInt("Tickets") + 1));
                PlayerPrefs.SetInt("cash", (PlayerPrefs.GetInt("cash") + 1000));
                PlayerPrefs.Save();
                done1.SetActive(true);
                Debug.Log("Day1");
                day1Get.SetActive(false);
                PlaySound();
                break;
            case 2:
                isReward2Collected = true;
                PlayerPrefs.SetString("Day2Bonus", "true");
                PlayerPrefs.SetInt("Tickets", (PlayerPrefs.GetInt("Tickets") + 1));
                PlayerPrefs.SetInt("cash", (PlayerPrefs.GetInt("cash") + 2000));
                PlayerPrefs.Save();
                done2.SetActive(true);
                day2Get.SetActive(false);
                PlaySound();
                break;
            case 3:
                isReward3Collected = true;
                PlayerPrefs.SetString("Day3Bonus", "true");
                PlayerPrefs.SetInt("Tickets", (PlayerPrefs.GetInt("Tickets") + 1));
                PlayerPrefs.SetInt("cash", (PlayerPrefs.GetInt("cash") + 5000));
                PlayerPrefs.Save();
                done3.SetActive(true);
                day3Get.SetActive(false);
                PlaySound();
                break;
            case 4:
                isReward4Collected = true;
                PlayerPrefs.SetString("Day4Bonus", "true");
                PlayerPrefs.SetInt("Tickets", (PlayerPrefs.GetInt("Tickets") + 1));
                PlayerPrefs.SetInt("cash", (PlayerPrefs.GetInt("cash") + 5000));
                PlayerPrefs.Save();
                done4.SetActive(true);
                day4Get.SetActive(false);
                PlaySound();
                break;
        }
    }
    void PlaySound()
    {
        _audioSource.PlayOneShot(getSound); 
    }
    IEnumerator BonusTimeCounter()
    {
        while (true)
        {
            currentTime = System.DateTime.Now;
            if (isReward2Collected.Equals(false) && !isReward2Ready)
            {
                day2TimeSpan = day2Time.Subtract(currentTime);
                day2TimerText.text = string.Format("{0}:{1}:{2}", ((int)day2TimeSpan.TotalHours), day2TimeSpan.Minutes, day2TimeSpan.Seconds);
                if (day2TimeSpan.Hours <= 0 && day2TimeSpan.Minutes <= 0 && day2TimeSpan.Seconds <= 0)
                {
                    done2.SetActive(false);
                    day2Get.SetActive(true);
                    isReward2Ready = true;
                    day2TimerText.transform.parent.gameObject.SetActive(false);
                }
            }
            if (isReward3Collected.Equals(false) && !isReward3Ready)
            {
                day3TimeSpan = day3Time.Subtract(currentTime);
                day3TimerText.text = string.Format("{0}:{1}:{2}", ((int)day3TimeSpan.TotalHours), day3TimeSpan.Minutes, day3TimeSpan.Seconds);
                if (day3TimeSpan.Hours <= 0 && day3TimeSpan.Minutes <= 0 && day3TimeSpan.Seconds <= 0)
                {
                    done3.SetActive(false);
                    day3Get.SetActive(true);
                    isReward3Ready = true;
                    day3TimerText.transform.parent.gameObject.SetActive(false);
                }
            }
            if (isReward4Collected.Equals(false) && !isReward4Ready)
            {
                day4TimeSpan = day4Time.Subtract(currentTime);
                day4TimerText.text = string.Format("{0}:{1}:{2}", ((int)day4TimeSpan.TotalHours), day4TimeSpan.Minutes, day4TimeSpan.Seconds);
                if (day4TimeSpan.Hours <= 0 && day4TimeSpan.Minutes <= 0 && day4TimeSpan.Seconds <= 0)
                {
                    done4.SetActive(false);
                    day4Get.SetActive(true);
                    isReward3Ready = true;
                    day4TimerText.transform.parent.gameObject.SetActive(false);
                }
            }

            yield return new WaitForSeconds(1);

            yield return new WaitForSeconds(0);
        }

    }


}
