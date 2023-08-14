﻿
using UnityEngine;
using UnityEngine.Analytics;
public class GameAnalytics : MonoBehaviour
{
    public static GameAnalytics instance;
    string versionCode = "5.1.03";
    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            DestroyImmediate(this.gameObject);

    }
    void Start()
    {
        Analytics.CustomEvent(versionCode);//sending version of apk
    }
    public void PlayGameEvent()
    {
        Analytics.CustomEvent(versionCode + "Play");//on play button press
    }
    public void SelectedItemEvent(string _name)
    {
        Analytics.CustomEvent(versionCode + _name);//pass name of selected item like, vehicle/weapon name
                                                   //GameAnalytics.instance.SelectedItemEvent("jeep");
                                                   //GameAnalytics.instance.SelectedItemEvent("monster_truck");
    }
    public void ModeSelectionEvent(string _modeName)
    {
        Analytics.CustomEvent(versionCode + _modeName);//pass name of selected mode like, time free/trial/challenge name
                                                       //GameAnalytics.instance.ModeSelectionEvent("time");
    }
    public void LevelStartEvent(int _levelNUmber)
    {
        AnalyticsEvent.LevelStart(_levelNUmber); //pass level number like 1,2,3,4....
                                                 //GameAnalytics.instance.LevelStartEvent(1);
                                                 //GameAnalytics.instance.LevelStartEvent(2);
    }
    public void LevelCompleteEvent(int _levelNUmber)
    {
        AnalyticsEvent.LevelComplete(_levelNUmber); //pass level number like 1,2,3,4....
    }
    public void LevelFailedEvent(int _levelNUmber)
    {
        AnalyticsEvent.LevelFail(_levelNUmber); //pass level number like 1,2,3,4....
    }

    /// Onboarding/First-time User Experience
    public void TutorialStartEvent()
    {
        AnalyticsEvent.TutorialStart(); //call when tutorial starts
    }
    public void TutorialSkipEvent()
    {
        AnalyticsEvent.TutorialSkip(); //call when tutorial skipped
    }
    public void TutorialCompleteEvent()
    {
        AnalyticsEvent.TutorialComplete(); //call when tutorial Completed
    }
    public void TutorialStepsEvent(int _stepNumber)
    {
        AnalyticsEvent.TutorialStep(_stepNumber); //event requires you to pass in steps of tutorial progress , (1,2,3,4..) Must be in increasing order
                                                  //GameAnalytics.instance.TutorialStepsEvent(1);
                                                  //GameAnalytics.instance.TutorialStepsEvent(2);
    }

}
