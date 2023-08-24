using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
public class endlessmodeGraphics : MonoBehaviour
{
    //Add new UI
    public static bool OnHome = false;
    public static endlessmodeGraphics instance;
    public GameObject statsPanel;
    public Text coins, coinCash, gamePlayCoins;
    public weaponAI bundleCloseScript;
    public PhycamViews timercloseScript;
    public gamePlayService googlePlayServiceScript;
    public GameObject levelclearButtons, comingSoonButton, nextLevelPanel, shieldBtn, loadingPanel;
    static int leaderBoardScore = 0;
    public Text noOfshield;
    public Image loadingBarFG, levelNameBG, levelImageBG, levelDescriptionBG;
    public Text score, distance, enemyHit, enemyHitCash, DistanceCash;
    public GameObject canvas;
    public GameObject pausePanel, gamePlayPanel, levelCompletePanel, gameOverPanel, warningBox, notEnoughCash;
    public Transform player;
    bool loadingbar;
    private AsyncOperation async = null;
    int bikeNo;
    bool started, death;
    string levelname;
    public AudioClip clickSound;
    public static int ShowLevelAd;
    bool pauseadd;
    public int chances;
    public static string gameMode;
 //   Ads_Manager manager = Ads_Manager.Instance;
    bool showBanner;
    bool levelClear;
    int levelNumber;
    bool levelCompleteAdd;
    public static int crashedOpponent;

    public GameObject BgMusic;

    public static int reviveCounter = 0;
    public static int try_count = 0;
    public static int win_Count = 0;
    public static int fail_Count = 0;
    float adder;
    bool startTimer, TimerForTryAgain;
    public static float deltaTime;
    private static float _lastframetime;
    private int totalCoins = 0;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        startTimer = false;

        ShowLevelAd = 0;
        tryBoolOnce = true;
        if (PlayerPrefs.GetInt("MusicOff") == 0)
        {
            BgMusic.GetComponent<AudioSource>().Play();
        }
        else
        {
            BgMusic.GetComponent<AudioSource>().Pause();
        }
        WholeDistanceCovered = 0.0f;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        crashedOpponent = 0;
        //nextLevelPanel.SetActive(false);
        loadingbar = false;

        levelNumber = PlayerPrefs.GetInt("levels");

        if (levelNumber == 7 || levelNumber == 8 || levelNumber == 11 || levelNumber == 12 || levelNumber == 15 || levelNumber == 16 || levelNumber == 20)
        {
            shieldBtn.SetActive(true);
        }
        else
            shieldBtn.SetActive(false);

        levelCompleteAdd = true;
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
        gameMode = "Idle";
        showBanner = true;
        pauseadd = true;
        if (PlayerPrefs.GetInt("SoundOff") == 0)
        {
            GetComponent<AudioSource>().playOnAwake = true;
        }
        else
        {
            GetComponent<AudioSource>().Pause();
        }
        if (PlayerPrefs.GetInt("SoundOff") == 0)
            GetComponent<AudioSource>().enabled = true;
        else
            GetComponent<AudioSource>().enabled = false;

        if (PlayerPrefs.GetInt("SoundOff") == 0)
        {
            AudioListener.volume = 1.0f;
        }
        else
        {
            AudioListener.volume = 0.0f;
        }

        player = player.GetComponent<heavyBikeTurns>().player.transform;
        if (player.name.Contains("Fat"))
            bikeNo = 0;
        else if (player.name.Contains("Player"))
            bikeNo = 1;
        //Debug.Log(" cash start: " + PlayerPrefs.GetInt("cash"));
        //manager = Ads_Manager.Instance;

        ShowTopCenterBanner();


    }
    public GameObject watchVideoPanel;
    public void revivePlayer()
    {
        if (reviveCounter == 0)
        {
            if (PlayerPrefs.GetInt("helmets") < 2)
            {
                PlayerPrefs.SetInt("helmets", 2);
                PlayerPrefs.Save();
            }
        }
        reviveCounter += 1;
        if (PlayerPrefs.GetInt("helmets") <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").transform.root.GetComponent<revivePlayer>().revival();

            //	watchVideoPanel.SetActive(true);		
        }
        canvas.SetActive(true);
    }

    //private Dictionary<string, object> parameters = new Dictionary<string, object>();
    void unityAnalyticsFtn()
    {
        if (GameAnalytics.instance)
            GameAnalytics.instance.LevelCompleteEvent(PlayerPrefs.GetInt("levels"));

    }
    void unityAnalyticsLevelFailedFtn()
    {
        if (GameAnalytics.instance)
            GameAnalytics.instance.LevelFailedEvent(PlayerPrefs.GetInt("levels"));
    }
    void statsPanelOpen()
    {
        statsPanel.SetActive(true);
        distance.text = System.Math.Round(((player.root.position.z - (-900f)) / 1000), 2) + " Km";
        enemyHit.text = crashedOpponent + System.String.Empty;
        enemyHitCash.text = System.String.Empty + heavyBikeTurns.opponentCrashedCash;
        DistanceCash.text = System.String.Empty + Mathf.Round((((player.root.position.z - (-900f)) / 1000) * 500));
        totalCoins = (int)(heavyBikeTurns.score + Mathf.Round((((player.root.position.z - (-900f)) / 1000) * 500)));
        score.text = totalCoins + System.String.Empty;//total

        //Debug.Log("total score: " + score.GetComponent<Text>().text);
        coins.text = heavyBikeTurns.coins + System.String.Empty;
        coinCash.text = System.String.Empty + heavyBikeTurns.coinCash;
    }

    void statsPanelClose()
    {
        statsPanel.SetActive(false);
    }

    float WholeDistanceCovered;
    void Update()
    {
        WholeDistanceCovered = player.root.position.z - (-900f);
        gamePlayCoins.text = heavyBikeTurns.coins + System.String.Empty;
        if (bikeNo == 1)
        {
            death = heavyBikeTurnControls.isdead;
            levelClear = heavyBikeTurnControls.levelClear;
        }
        else if (bikeNo == 0)
        {
            death = turnLevelcontrols.isdead;
            levelClear = turnLevelcontrols.levelClear;
        }

        if (levelNumber == 7 || levelNumber == 8 || levelNumber == 11 || levelNumber == 12 || levelNumber == 15 || levelNumber == 16 || levelNumber == 20)
        {

            noOfshield.text = PlayerPrefs.GetInt("shields") + System.String.Empty;
        }
        started = handleArrow.start;
        if (PlayerPrefs.GetInt("SoundOff") != 0)
        {
            GetComponent<AudioSource>().Pause();
        }


        if (Input.GetKeyDown(KeyCode.Escape) && !loadingbar)
        {
            if (!weaponAI.boostBundleOpened && !PhycamViews.slowBundleOpened && !weaponAI.ammoBundleOpened && !weaponAI.shieldBundleOpened)
            {
                Paused();
            }
            else if (weaponAI.boostBundleOpened || PhycamViews.slowBundleOpened || weaponAI.ammoBundleOpened || weaponAI.shieldBundleOpened)
            {
                bundleCloseScript.closeBundle();
                timercloseScript.closeTimerBundle();
            }
        }



        if (loadingbar)
        {
            LoadALevel(levelname);
        }
        /* 		if (loadingbar) { //khuram
                    loadingBarFG.fillAmount =async.progress;

                } */


        if (startTimer)
        {
            deltaTime = (Time.realtimeSinceStartup - _lastframetime);
            _lastframetime = Time.realtimeSinceStartup;

            if (adder <= 3f)
            {
                adder += deltaTime;


                if (adder > 3)
                    adder = 3;


            }
            if (adder >= 3f)
            {
                levelCompleteFtn();
            }
            //			print (adder);

        }


    }
    private IEnumerator LoadALevel(string levelName)
    {
        async = SceneManager.LoadSceneAsync(levelName);
        yield return async;

    }
    public void Paused()
    {
      //  Adcontrol.instace.hideBanner();
     //   Adcontrol.instace.showAd();
        if (!weaponAI.boostBundleOpened && !PhycamViews.slowBundleOpened && !weaponAI.ammoBundleOpened && !levelClear && !weaponAI.shieldBundleOpened)
        {
            if (started && !death && tiltControl.startTiltAfterCam)
            {
                pausePanel.SetActive(true);
                statsPanelOpen();
                if (bikeNo == 0)
                {
                    turnLevelcontrols.cash = turnLevelcontrols.score;
                    PlayerPrefs.SetInt("cash", turnLevelcontrols.cash);

                }
                if (bikeNo == 1)
                {
                    heavyBikeTurnControls.cash = heavyBikeTurnControls.score;
                    PlayerPrefs.SetInt("cash", heavyBikeTurnControls.cash);

                }

                if (GetComponent<AudioSource>().enabled)
                    GetComponent<AudioSource>().PlayOneShot(clickSound);
                if (gameMode == "Idle")
                {
                    gameMode = "Pause";
                    Time.timeScale = 0;
                    if (PlayerPrefs.GetInt("SoundOff") == 0)
                    {
                        AudioListener.volume = 0.0f;
                    }

                    if (pauseadd)
                    {
                        pauseadd = false;
                        //Adcontrol.instace.showAd();
                        //Adcontrol.instace.showBanner();
                    }

                }
                else if (gameMode.Equals("Pause"))
                {

                    pausePanel.SetActive(false);
                    statsPanelClose();
                    gameMode = "Idle";
                    Time.timeScale = 1;
                    if (PlayerPrefs.GetInt("SoundOff") == 0)
                    {
                        AudioListener.volume = 1.0f;
                    }
                    pauseadd = true;
                    //Adcontrol.instace.showAd();
                    ShowTopCenterBanner();

                }
            }
        }
    }


    public void resumeFtn()
    {
        GetComponent<AudioSource>().PlayOneShot(clickSound);
        gameMode = "Idle";
        Time.timeScale = 1.0f;

        pausePanel.SetActive(false);
        statsPanelClose();
        pauseadd = true;
        if (PlayerPrefs.GetInt("SoundOff") == 0)
        {
            AudioListener.volume = 1.0f;
        }
        
        ShowTopCenterBanner();
    }
    public void restartFtn()
    {
        Time.timeScale = 1.0f;
        reviveCounter = 0;
        if (loadingPanel)
            loadingPanel.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(clickSound);
        PlayerPrefs.SetInt("helmetUsed", 0);
        PlayerPrefs.SetFloat("totalDistaneCovered", (PlayerPrefs.GetFloat("totalDistaneCovered") + WholeDistanceCovered));
        leaderBoardScore = 0;
        PlayerPrefs.SetInt("leaderBoardScore", 0);

        pauseadd = true;

        if (bikeNo == 0)
        {
            turnLevelcontrols.cash = turnLevelcontrols.score;
            PlayerPrefs.SetInt("cash", turnLevelcontrols.cash);

        }
        if (bikeNo == 1)
        {
            heavyBikeTurnControls.cash = heavyBikeTurnControls.score;
            PlayerPrefs.SetInt("cash", heavyBikeTurnControls.cash);

        }
        PlayerPrefs.SetInt("singelLevelScore", (int)(heavyBikeTurns.score + Mathf.Round((((player.root.position.z - (-900f)) / 1000) * 500))));

        if (gameMode.Equals("Pause"))
        {

            PlayerPrefs.SetInt("leaderBoardScore", 0);
            if (bikeNo == 1)
            {

                heavyBikeTurnControls.cash = heavyBikeTurnControls.score + (int)(Mathf.Round((((player.root.position.z - (-900f)) / 1000) * 500)));
                PlayerPrefs.SetInt("cash", heavyBikeTurnControls.cash);


            }
            else
            {
                turnLevelcontrols.cash = turnLevelcontrols.score + (int)(Mathf.Round((((player.root.position.z - (-900f)) / 1000) * 500)));
                PlayerPrefs.SetInt("cash", turnLevelcontrols.cash);

            }
        }
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       
        ShowTopCenterBanner();
    }


    public void menuFtn()
    {
        Time.timeScale = 1.0f;
        reviveCounter = 0;
        ShowLevelAd = 1;

        //Removed
        //if (loadingPanel)
        //    loadingPanel.SetActive(true);

        GetComponent<AudioSource>().PlayOneShot(clickSound);
        PlayerPrefs.SetInt("helmetUsed", 0);
        PlayerPrefs.SetFloat("totalDistaneCovered", (PlayerPrefs.GetFloat("totalDistaneCovered") + WholeDistanceCovered));
        PlayerPrefs.SetInt("leaderBoardScore", 0);
        PlayerPrefs.SetInt("singelLevelScore", (int)(heavyBikeTurns.score + Mathf.Round((((player.root.position.z - (-900f)) / 1000) * 500))));

        if (gameMode.Equals("Pause"))
        {

            if (bikeNo == 1)
            {
                heavyBikeTurnControls.cash = heavyBikeTurnControls.score + (int)(Mathf.Round((((player.root.position.z - (-900f)) / 1000) * 500)));
                PlayerPrefs.SetInt("cash", heavyBikeTurnControls.cash);
            }
            else
            {
                turnLevelcontrols.cash = turnLevelcontrols.score + (int)(Mathf.Round((((player.root.position.z - (-900f)) / 1000) * 500)));
                PlayerPrefs.SetInt("cash", turnLevelcontrols.cash);
            }
        }


        PlayerPrefs.Save();
        pauseadd = true;
        PlayerPrefs.SetInt("score", 0);

        //Removed
        //bikeSelection.backFromLevel = false;
        //bikeSelection.backToMode = true;

        SceneManager.LoadScene("bikeSelectLevel 2");

        //OnHome = true; //Removed
    }

    public void gameOverFtn()
    {
       
        win_Count = 0;
        try_count += 1;
        fail_Count += 1;

        //googlePlayServiceScript.checkAchievement();
      
        gameOverPanel.SetActive(true);
     //   Adcontrol.instace.showAd();
        statsPanelOpen();
        gamePlayPanel.SetActive(false);

        PlayerPrefs.SetInt("singelLevelScore", (int)(heavyBikeTurns.score + Mathf.Round((((player.root.position.z - (-900f)) / 1000) * 500))));

        leaderBoardScore += PlayerPrefs.GetInt("singelLevelScore");
        PlayerPrefs.SetInt("leaderBoardScore", leaderBoardScore);

        if (bikeNo == 1)
        {
            heavyBikeTurnControls.cash = heavyBikeTurnControls.score + (int)(Mathf.Round((((player.root.position.z - (-900f)) / 1000) * 500)));
            PlayerPrefs.SetInt("cash", heavyBikeTurnControls.cash);
        }
        else
        {
            turnLevelcontrols.cash = turnLevelcontrols.score + (int)(Mathf.Round((((player.root.position.z - (-900f)) / 1000) * 500)));
            PlayerPrefs.SetInt("cash", turnLevelcontrols.cash);
        }


        if (PlayerPrefs.GetInt("SoundOff") == 0)
        {
            AudioListener.volume = 0.0f;
        }


        if (PlayerPrefs.GetInt("SoundOff") == 0)
        {
            GetComponent<AudioSource>().enabled = false;
        }

        if (!PlayerPrefs.GetString("missilePurchased").Equals("true") && PlayerPrefs.GetInt("levels").Equals(8))
        {
            warningBox.SetActive(true);
        }
        Time.timeScale = 0.3f;
        if (showBanner && !PlayerPrefs.GetString("bundlePurchase").Equals("true"))
        {
            showBanner = false;
            unityAnalyticsLevelFailedFtn();


           // Adcontrol.instace.hideBanner();
           // Adcontrol.instace.showAd();
           // if (fail_Count > 1)
                //Adcontrol.instace.showAd();
        }
        Time.timeScale = 0.0f;
    }
    public void BuyMissileLauncher()
    {
        if (PlayerPrefs.GetInt("cash") >= 70000)   //price of rocket
        {
            PlayerPrefs.SetInt("pistolSelect", 2);
            PlayerPrefs.SetString("missilePurchased", "true");
            PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") - 70000);
            PlayerPrefs.Save();
            SkipMissilePurchase();
        }
        else
        {
            notEnoughCash.SetActive(true);
        }
    }
    public void SkipMissilePurchase()
    {
        warningBox.SetActive(false);

    }

    public void nextLevelFtn()
    {
        Time.timeScale = 1.0f;
        reviveCounter = 0;
        if (loadingPanel)
            loadingPanel.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(clickSound);

        PlayerPrefs.SetFloat("totalDistaneCovered", (PlayerPrefs.GetFloat("totalDistaneCovered") + WholeDistanceCovered));
        //			
        PlayerPrefs.SetInt("singelLevelScore", (int)(heavyBikeTurns.score + Mathf.Round((((player.root.position.z - (-900f)) / 1000) * 500))));


        leaderBoardScore += PlayerPrefs.GetInt("singelLevelScore");
        /*         if (bikeNo == 0)
                {
                    turnLevelcontrols.cash = turnLevelcontrols.score;
                    PlayerPrefs.SetInt("cash", turnLevelcontrols.cash);

                }
                if (bikeNo == 1)
                {
                    heavyBikeTurnControls.cash = heavyBikeTurnControls.score;
                    PlayerPrefs.SetInt("cash", heavyBikeTurnControls.cash);
                } */
        PlayerPrefs.Save();

        PlayerPrefs.SetInt("score", 0);


        loadingbar = true;
        //			async = Application.LoadLevelAsync ("desert");
        //			levelname = "desert";
        if (PlayerPrefs.GetInt("SoundOff") == 0)
        {
            AudioListener.volume = 1.0f;
        }

        bikeSelection.backToMode = true;
        bikeSelection.backFromLevel = true;
        if (PlayerPrefs.GetInt("levels") < 20)
            PlayerPrefs.SetInt("levels", PlayerPrefs.GetInt("levels") + 1);

        SceneManager.LoadScene("bikeSelectLevel 2");
     
    }
    bool tryBoolOnce;
    public GameObject rateUsPanel, feedBackPanel;
    public void rateUsFtn()
    {
        rateUsPanel.SetActive(true);

    }
    public void yesRate()
    {
        PlayerPrefs.SetString("hasRated", "true");
        PlayerPrefs.Save();
        Application.OpenURL("market://details?id=com.tulip.bike.attack.race");
        startTimer = true;

    }
    public void noRate()
    {
        rateUsPanel.SetActive(false);
        feedBackFtn();
        //levelCompleteFtn ();
    }
    public void feedBackFtn()
    {
        rateUsPanel.SetActive(false);
        //EmailUs ();
        //startTimer = true;
        feedBackPanel.SetActive(true);
    }
    public void noFeedbackFtn()
    {
        feedBackPanel.SetActive(false);
        levelCompleteFtn();
    }
    public void EmailUs()
    {
        //email Id to send the mail to
        string email = "admin@tulipapps.com";
        //	"admin@tulipapps.com";

        //subject of the mail
        string subject = MyEscapeURL("FEEDBACK/SUGGESTION");
        //body of the mail which consists of Device Model and its Operating System
        string body = MyEscapeURL("Please Enter your message here\n\n\n\n" +
                 "________" +
                 //								   "\n\nPlease Do Not Modify This\n\n" +
                 //  "Model: "+SystemInfo.deviceModel+"\n\n"+
                 //   "OS: "+SystemInfo.operatingSystem+"\n\n" +
                 "________");
        //Open the Default Mail App
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
        startTimer = true;
    }

    string MyEscapeURL(string url)
    {
        //return WWW.EscapeURL(url).Replace("+", "%20");
        return UnityWebRequest.EscapeURL(url).Replace("+", "%20");
    }

    public void levelCompleteFtn()
    {
        gamePlayPanel.SetActive(false);

        if (tryBoolOnce)
        {
            tryBoolOnce = false;
            win_Count += 1;
            try_count += 1;
            fail_Count = 0;

            if (try_count > 5)
            {
                if (!PlayerPrefs.GetString("hasRated").Equals("true"))
                {
                    rateUsFtn();
                    try_count = 0;
                    return;
                }
            }
        }

        rateUsPanel.SetActive(false);

        googlePlayServiceScript.checkAchievement();

        PlayerPrefs.SetString("level2Unlocked", "true");

        if (levelNumber == 2)
        {

            PlayerPrefs.SetString("level3Unlocked", "true");
        }
        else if (levelNumber == 3)
        {

            PlayerPrefs.SetString("level4Unlocked", "true");
        }
        else if (levelNumber == 4)
        {

            PlayerPrefs.SetString("level5Unlocked", "true");
        }
        else if (levelNumber == 5)
        {
            PlayerPrefs.SetString("level6Unlocked", "true");
        }
        else if (levelNumber == 6)
        {

            PlayerPrefs.SetString("level7Unlocked", "true");
        }
        else if (levelNumber == 7)
        {

            PlayerPrefs.SetString("level8Unlocked", "true");
        }

        else if (levelNumber == 8)
        {

            PlayerPrefs.SetString("level9Unlocked", "true");
        }
        else if (levelNumber == 9)
        {

            PlayerPrefs.SetString("level10Unlocked", "true");
        }
        else if (levelNumber == 10)
        {

            PlayerPrefs.SetString("level11Unlocked", "true");
        }
        else if (levelNumber == 11)
        {

            PlayerPrefs.SetString("level12Unlocked", "true");
        }
        else if (levelNumber == 12)
        {

            PlayerPrefs.SetString("level13Unlocked", "true");
        }
        else if (levelNumber == 13)
        {

            PlayerPrefs.SetString("level14Unlocked", "true");
        }
        else if (levelNumber == 14)
        {

            PlayerPrefs.SetString("level15Unlocked", "true");
        }
        else if (levelNumber == 15)
        {

            PlayerPrefs.SetString("level16Unlocked", "true");
        }
        else if (levelNumber == 16)
        {
            PlayerPrefs.SetString("level17Unlocked", "true");
        }
        else if (levelNumber == 17)
        {
            PlayerPrefs.SetString("level18Unlocked", "true");
        }
        else if (levelNumber == 18)
        {
            PlayerPrefs.SetString("level19Unlocked", "true");
        }
        else if (levelNumber == 19)
        {
            PlayerPrefs.SetString("level20Unlocked", "true");
        }

        PlayerPrefs.Save();
        Time.timeScale = 0.3f;
        AudioListener.volume = 0.0f;

        if (PlayerPrefs.GetInt("SoundOff") == 0)
        {
            AudioListener.volume = 0.0f;
            GetComponent<AudioSource>().enabled = false;
        }

        if (levelNumber < 20)
        {

            levelclearButtons.SetActive(true);
            comingSoonButton.SetActive(false);
        }
        else if (levelNumber >= 20)
        {

            levelclearButtons.SetActive(false);
            comingSoonButton.SetActive(true);
        }

        levelCompletePanel.SetActive(true);
        //Adcontrol.instace.showAd();

        PlayerPrefs.SetInt("singelLevelScore", (int)(heavyBikeTurns.score + Mathf.Round((((player.root.position.z - (-900f)) / 1000) * 500))));
        if (bikeNo == 1)
        {

            heavyBikeTurnControls.cash = heavyBikeTurnControls.score + (int)(Mathf.Round((((player.root.position.z - (-900f)) / 1000) * 500)));
            //Debug.Log("heavyBikeTurnControls cash : " + heavyBikeTurnControls.cash);
            //PlayerPrefs.SetInt("cash", heavyBikeTurnControls.cash);
        }
        else
        {
            turnLevelcontrols.cash = turnLevelcontrols.score + (int)(Mathf.Round((((player.root.position.z - (-900f)) / 1000) * 500)));
            //PlayerPrefs.SetInt("cash", turnLevelcontrols.cash);
            //Debug.Log("turnLevelcontrols cash : " + turnLevelcontrols.cash);
        }



        if (levelCompleteAdd)
        {
           
            statsPanelOpen();

            PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + totalCoins);
            PlayerPrefs.Save();

            levelCompleteAdd = false;
            unityAnalyticsFtn();
            //Adcontrol.instace.hideBanner();
          
            if (win_Count > 1 && try_count > 0)
            {
                //Adcontrol.instace.showAd();

            }
        }
        Time.timeScale = 1.0f;
    }


    public void ShowTopCenterBanner()
    {
       // Adcontrol.instace.showBanner();
    }
}
