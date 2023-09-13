using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class MainMenuScript : MonoBehaviour
{
    // New GUI Add
    public GameObject MainMenu;
    public Text[] UpdateCashText;

    //Old GUI
    public static MainMenuScript Instance;
    public GameObject dailyBonusPanel, morePanel, bgImage;
    public GameObject ticketStore, spinner, insufficientTicketPanel;
    public static bool bonusGiven, insufficientTicket;
    public static bool giveReward;
    EventSystem currentBtn;
    public GameObject comingSoon, comingSoonPlayer, spinPanel, buttonPanel, bottomBarPanel;
    bool settingPanelActive, socialMediaPanelActive, spinPanelActive;
    public Image soundBG, musicBG, dailyBonusBG;
    public Sprite soundOn, soundOff, musicOn, musicOff, bonusOn, bonusOff;
    public Color selectedColor;
    public Sprite selectedFooter, normalFooter;
    bool cashBundle, storeBundle;
    public Image plusBG, paidFooter, freeFooter, settingImage;
    public Sprite plusTexture, closeTexture;
    public GameObject plus, close, storeBundlePanel, paidItemPanel, freeItemPanel, settingPanel, socialMediaPanel, helpScreenPanel;
    public Text cashText, paidText, freeText, NoofTickets;
    public GameObject quitPanel, cashBundlePanel;
    bool playClicked;
    public Image progressBar;
    public Text progressPercent;
    private AsyncOperation async = null; // When assigned, load is in progress.
    bool loadingbar = false;
    string levelname;
    bool ticketStoreActive, moreActive;
    public static bool playsound;
    bool cashPanelActive;
    public static bool showInterstitial = true;
    bool dailyBonusActive;
    public bool mainSceen = true;
    public bool soundBool = true;
    public GameObject BgMusic;
    bool musicBool;
    public AudioClip clickSound;
    public static bool QuitMenu = false;
    bool helpbool;
    public static bool isShowInterstitial = true;
    public void socialSitesFtn()
    {
        //HideTopCenterBanner();
        if (currentBtn.currentSelectedGameObject.transform.name.Contains("fb"))
        {
            Application.OpenURL("https://www.facebook.com/TulipApps-438286343022521/");

        }
        else if (currentBtn.currentSelectedGameObject.transform.name.Contains("youtube"))
        {
            Application.OpenURL("https://www.youtube.com/channel/UC5qUyKPxiJkJ2J58LnAYJhg");

        }
        else if (currentBtn.currentSelectedGameObject.transform.name.Contains("g+"))
        {
            Application.OpenURL("https://plus.google.com/101951341971055506225");

        }
        else if (currentBtn.currentSelectedGameObject.transform.name.Contains("twitter"))
        {
            Application.OpenURL("https://twitter.com/TulipApps");
        }
    }

    public void openApp()
    {
        bool fail = false;
        string bundleId = "com.tulip.bike.attack.race"; // your target bundle id
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");

        AndroidJavaObject launchIntent = null;
        try
        {
            launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", bundleId);
        }
        catch (System.Exception e)
        {
            fail = true;
        }

        if (fail)
        { //open app in store
            Application.OpenURL("market://details?id=" + bundleId);
        }
        else //open the app
            ca.Call("startActivity", launchIntent);

        up.Dispose();
        ca.Dispose();
        packageManager.Dispose();
        launchIntent.Dispose();
    }

    public void dailyBonusFtn()
    {

        dailyBonusPanel.SetActive(true);
        dailyBonusActive = true;
        if (socialMediaPanelActive)
        {
            socialMediaPanelActive = false;
            socialMediaPanel.GetComponent<Animation>().Play("socialMedia1End", PlayMode.StopAll);
            Invoke("HideSocialMedia", 0.5f);
        }
        if (settingPanelActive)
        {
            settingPanelActive = false;
            //settingPanel.GetComponent<Animation>().Play("settingPanelClose", PlayMode.StopAll);
            Invoke("HideSetting", 0.667f);
        }

    }


    public void closeDailyPanel()
    {

        Time.timeScale = 1;
        dailyBonusActive = false;
        dailyBonusPanel.SetActive(false);
        if (PlayerPrefs.GetInt("SoundOff") == 0)
        {
            AudioListener.volume = 1.0f;
        }
        //HideTopCenterBanner();
    }
    // Use this for initialization


    public void insufficientTicketFtn()
    {
        if (PlayerPrefs.GetInt("Tickets") <= 0)
        {
            insufficientTicketPanel.SetActive(true);
            insufficientTicket = true;
        }
    }


    public void closeInsufficientTicketFtn()
    {

        insufficientTicketPanel.SetActive(false);
        insufficientTicket = false;

    }

    public void resetRewardTexture()
    {
        //PlayerPrefs.SetInt ("Tickets", 2);
        if (PlayerPrefs.GetInt("Tickets") > 0)
        {

            dailyBonusBG.sprite = bonusOn;

            dailyBonusBG.transform.localEulerAngles = Vector3.zero;
            if (!dailyBonusBG.transform.GetComponent<Animation>().IsPlaying("daily bonus"))
                dailyBonusBG.transform.GetComponent<Animation>().Play();

        }
        else
        {


            dailyBonusBG.sprite = bonusOff;
            dailyBonusBG.transform.GetComponent<Animation>().Stop();
            dailyBonusBG.transform.localEulerAngles = Vector3.zero;

        }
    }

    void Start()
    {
        Instance = this;

        if(!PlayerPrefs.HasKey("level0locked"))
            Common.savePlayerData();

        Common.getPlayerData();

        foreach(Text t in UpdateCashText)
        {
            t.text = cashText.text;
        }
        Time.timeScale = 1;
        if (!PlayerPrefs.HasKey("Remove_ads"))
        {
            PlayerPrefs.SetString("Remove_ads", "false");
        }

        /* #if UNITY_EDITOR
                Debug.LogError("cash");
        #endif
                if (!PlayerPrefs.HasKey("cash"))
                    PlayerPrefs.SetInt("cash", 900000);
                for (int i = 1; i < 17; i++)
                    PlayerPrefs.SetString("level" + i + "Unlocked", "true"); */



        endlessmodeGraphics.ShowLevelAd = 0;
        moreActive = false;
        //PlayerPrefs.DeleteAll ();
        ticketStoreActive = false;
        bonusGiven = false;

        currentBtn = EventSystem.current;
        spinPanelActive = false;
        socialMediaPanelActive = false;
        settingPanelActive = false;
        plusBG.sprite = plusTexture;
        cashBundle = storeBundle = false;
        cashPanelActive = false;
        //		PlayerPrefs.SetString ("bundlePurchase","true");
        //		PlayerPrefs.Save ();


        //myTimer=1f;

        cashText.text = PlayerPrefs.GetInt("cash") + string.Empty;

        playsound = false;



        if (PlayerPrefs.GetInt("MusicOff") == 0)
        {
            musicBool = true;
            musicBG.sprite = musicOn;

            //BgMusic.SetActive(true);
            BgMusic.GetComponent<AudioSource>().Play();
        }
        else
        {
            musicBool = false;
            BgMusic.GetComponent<AudioSource>().Pause();
            musicBG.sprite = musicOff;
            //BgMusic.SetActive(false);
        }

        if (PlayerPrefs.GetInt("SoundOff") == 0)
        {
            soundBool = true;
            soundBG.sprite = soundOn;

        }
        else
        {
            soundBool = false;
            soundBG.sprite = soundOff;

        }
        AudioListener.volume = 1.0f;
        QuitMenu = false;
        helpbool = false;
        StartCoroutine("spinBonusTexture", 0.5f);

        xPosition = percentBG.transform.localPosition.x;
        ShowTopCenterBanner();
    }



    IEnumerator spinBonusTexture()
    {
        while (true)
        {
            resetRewardTexture();
            yield return new WaitForSeconds(0.5f);
        }
    }



    void Awake()
    {
        //PlayerPrefs.DeleteAll ();
        //	PlayerPrefs.SetInt ("cash",200000);

        if (!PlayerPrefs.HasKey("Tickets"))
        {
            PlayerPrefs.SetInt("Tickets", 1);

        }
        if (!PlayerPrefs.HasKey("shields"))
            PlayerPrefs.SetInt("shields", 3);

        if (!PlayerPrefs.HasKey("helmets"))
            PlayerPrefs.SetInt("helmets", 3);

        if (!PlayerPrefs.HasKey("boosts"))
            PlayerPrefs.SetInt("boosts", 3);

        if (!PlayerPrefs.HasKey("timers"))
            PlayerPrefs.SetInt("timers", 3);

        if (!PlayerPrefs.HasKey("ammos"))
        {
            PlayerPrefs.SetInt("ammos", 20);
            PlayerPrefs.Save();
        }


        resetRewardTexture();

        mainSceen = true;


    }

    //	bool reset=true;

    public void closeMoreFtn()
    {
        moreActive = false;
        morePanel.SetActive(false);
        HideLargeBanner();
        ShowTopCenterBanner();
    }


    public void openMoreFtn()
    {

        if (!loadingbar)
        {
            moreActive = true;
            morePanel.SetActive(true);

            if (socialMediaPanelActive)
            {
                socialMediaPanelActive = false;
                socialMediaPanel.GetComponent<Animation>().Play("socialMedia1End", PlayMode.StopAll);
                Invoke("HideSocialMedia", 0.5f);
            }
            if (settingPanelActive)
            {
                settingPanelActive = false;
               // settingPanel.GetComponent<Animation>().Play("settingPanelClose", PlayMode.StopAll);
                Invoke("HideSetting", 0.667f);
            }

            if (PlayerPrefs.GetInt("SoundOff") == 0)
                GetComponent<AudioSource>().PlayOneShot(clickSound);
            HideTopCenterBanner();
            ShowLargeBanner();
        }
    }

    float progressPercentText;
    public Image LoadingBar, percentBG;
    float xPosition;
    void Update()
    {
        foreach (Text t in UpdateCashText)
        {
            t.text = cashText.text;
        }
        // PlayerPrefs.DeleteAll();
        //		if (!PlayerPrefs.HasKey("lastPlayed") || PlayerPrefs.GetString ("lastPlayed") != System.String.Empty) {
        //						resetSpinTexture ();
        //				}

        cashText.text = PlayerPrefs.GetInt("cash") + string.Empty;

        NoofTickets.text = PlayerPrefs.GetInt("Tickets") + string.Empty;

        //if (playClicked) {
        //			if(async !=null){
        //				LoadingBar.fillAmount=async.progress;
        //				if(async.progress>=0.89f)
        //					LoadingBar.fillAmount=0.97f;
        //				percentBG.transform.localPosition=new Vector3(xPosition+(async.progress *750),percentBG.transform.localPosition.y,percentBG.transform.localPosition.z);
        //			}
        //	}

        if (Input.GetKeyDown(KeyCode.Escape) && moreActive)
        {
            closeMoreFtn();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && !QuitMenu && (cashBundle || storeBundle) && !spinPanelActive && !insufficientTicket && !dailyBonusActive)
        {
            Time.timeScale = 1;
            if (PlayerPrefs.GetInt("SoundOff") == 0)
            {
                AudioListener.volume = 1.0f;
            }
            cashBundlePanel.SetActive(false);
            storeBundlePanel.SetActive(false);
            cashBundle = false;
            storeBundle = false;
            cashPanelActive = false;
            plusBG.sprite = plusTexture;
            // HideTopCenterBanner();

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !QuitMenu && (!cashBundle && !storeBundle) && !spinPanelActive && !insufficientTicket && dailyBonusActive)
        {
            closeDailyPanel();

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !helpbool && (!cashBundle && !storeBundle) && !spinPanelActive && !insufficientTicket && !dailyBonusActive)
        {
            QuitMenu = !QuitMenu;
            if (QuitMenu)
            {
                quitPanel.SetActive(true);
                //Time.timeScale=0.0f;
                /*                if (PlayerPrefs.GetInt("SoundOff") == 0)
                               {
                                   AudioListener.volume = 0.0f;
                               } */
                HideTopCenterBanner();
                ShowLargeBanner();
                Invoke("ShowQuitAd", 0.1f);
            }
            else if (!QuitMenu)
            {
                quitPanel.SetActive(false);
                Time.timeScale = 1.0f;
                /*                 if (PlayerPrefs.GetInt("SoundOff") == 0)
                                {
                                    AudioListener.volume = 1.0f;
                                } */
                HideLargeBanner();
                ShowTopCenterBanner();
            }
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && ticketStoreActive && !insufficientTicket && !dailyBonusActive)
        {
            Time.timeScale = 1;
            if (PlayerPrefs.GetInt("SoundOff") == 0)
            {
                AudioListener.volume = 1.0f;
            }
            ticketStore.SetActive(false);
            ticketStoreActive = false;
            //HideTopCenterBanner();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && helpbool && !spinPanelActive && !insufficientTicket && !dailyBonusActive)
        {
            helpbool = false;
            mainSceen = true;
            helpScreenPanel.SetActive(false);
            settingPanel.SetActive(false);
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && !helpbool && (!cashBundle && !storeBundle) && spinPanelActive && !dailyBonusActive && !ticketStoreActive && !insufficientTicket)
        {
            if (spinner.GetComponent<Rigidbody>().velocity.magnitude == 0 && !bonusGiven)
            {
                spinPanel.SetActive(false);
                bgImage.SetActive(true);
                spinPanelActive = false;
                buttonPanel.SetActive(true);
                bottomBarPanel.SetActive(false);

            }
        }


        else if (Input.GetKeyDown(KeyCode.Escape) && insufficientTicket && !dailyBonusActive)
        {
            closeInsufficientTicketFtn();
        }




        //		if (loadingbar) {
        ////			print ("inside if ");
        //			LoadALevel (levelname);
        ////			print ("async: "+(async.progress*100));
        //
        //		}
        //		
    }

    public GameObject progressPanel;
    private IEnumerator LoadALevel(string levelName)
    {
        async = SceneManager.LoadSceneAsync(levelName);
        yield return async;

    }

    public IEnumerator showAdCoroutine()
    {
        float i = 0;
        while (i < 2f)
        {
            i += Time.deltaTime * 1;
            yield return new WaitForSeconds(0);
        }
        if (isShowInterstitial)
        {
            isShowInterstitial = false;

#if !UNITY_EDITOR
            if (!PlayerPrefs.GetString("bundlePurchase").Equals("true"))
                Adcontrol.instace.showAd();
#endif
        }
        while (i < 4f)
        {
            i += Time.deltaTime * 1;
            yield return new WaitForSeconds(0);
        }

        //Invoke("startScene", 1f);
        startScene();
    }

    public void CustomAdLink()
    {
        Analytics.CustomEvent("Real_Impossible_Track");
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.impossible.tracks.real.race.games&referrer=utm_source=bike_attack_race1");
    }


    void startScene()
    {
        SceneManager.LoadScene("bikeSelectLevel 2");
        //	async = Application.LoadLevelAsync ("bikeSelectLevel 2");
        levelname = "bikeSelectLevel 2";
        loadingbar = true;
    }

    public void playFtn()
    {
        if (!playClicked && !QuitMenu && !cashPanelActive)
        {
            settingImage.color = new Color(255, 255, 255, 255);
            MainMenu.SetActive(false);
            if (socialMediaPanelActive)
            {
                socialMediaPanelActive = false;
                socialMediaPanel.GetComponent<Animation>().Play("socialMedia1End", PlayMode.StopAll);
                Invoke("HideSocialMedia", 0.5f);
            }
            if (settingPanelActive)
            {
                settingPanelActive = false;
                //settingPanel.GetComponent<Animation>().Play("settingPanelClose", PlayMode.StopAll);
                Invoke("HideSetting", 0.667f);
            }

            playClicked = true;
            bikeSelection.backFromLevel = false;
            bikeSelection.backToMode = false;
            storeBtnHandlers.levelSelectionActive = false;

            if (PlayerPrefs.GetInt("SoundOff") == 0)
                GetComponent<AudioSource>().PlayOneShot(clickSound);

            //	async = Application.LoadLevelAsync ("bikeSelectLevel 2");
            //	levelname = "bikeSelectLevel 2";
            progressPanel.SetActive(true);
            //	progressBar.transform.parent.gameObject.SetActive(true);
            StartCoroutine("showAdCoroutine");
            GameAnalytics.instance.PlayGameEvent();
            //startScene();
        }
    }


    public void NoQuit()
    {
        quitPanel.SetActive(false);
        QuitMenu = false;
        if (PlayerPrefs.GetInt("SoundOff") == 0)
            GetComponent<AudioSource>().PlayOneShot(clickSound);

        Time.timeScale = 1.0f;
        /*         if (PlayerPrefs.GetInt("SoundOff") == 0)
                {
                    AudioListener.volume = 1.0f;
                } */
        HideLargeBanner();
        ShowTopCenterBanner();

    }

    public void YesQuit()
    {
        if (PlayerPrefs.GetInt("SoundOff") == 0)
            GetComponent<AudioSource>().PlayOneShot(clickSound);
        CustomApplicationQuit();
        //System.Diagnostics.Process.GetCurrentProcess ().Kill ();
        //Application.Quit();
    }

    private void CustomApplicationQuit()
    {
#if UNITY_ANDROID
        using (AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject unityActivity = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
            unityActivity.Call<bool>("moveTaskToBack", true);
        }
#else
				Application.Quit();
#endif
    }

    public void gamePromotion()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.police.pursuit.highway.race");

    }
    public void trafficRushgamePromotion()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.traffic.rush.unlimted&hl=en");

    }

    public void cashBundleFtn()
    {
        if (!QuitMenu)
        {

            if (!cashBundle && !storeBundle)
            {

                cashBundlePanel.SetActive(true);
                //plusBG.sprite=closeTexture;	
                cashPanelActive = true;
                cashBundle = true;
                storeBundle = false;

            }
            else
            {
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("SoundOff") == 0)
                {
                    AudioListener.volume = 1.0f;
                }
                cashBundle = false;
                storeBundle = false;
                cashBundlePanel.SetActive(false);
                storeBundlePanel.SetActive(false);
                cashPanelActive = false;
                plusBG.sprite = plusTexture;
                //HideTopCenterBanner();
                //plus.SetActive (true);
                //close.SetActive (false);
            }
        }
        settingImage.color = new Color(255, 255, 255, 255);
        if (socialMediaPanelActive)
        {
            socialMediaPanelActive = false;
            socialMediaPanel.GetComponent<Animation>().Play("socialMedia1End", PlayMode.StopAll);
            Invoke("HideSocialMedia", 0.5f);
        }
        if (settingPanelActive)
        {
            settingPanelActive = false;
            //settingPanel.GetComponent<Animation>().Play("settingPanelClose", PlayMode.StopAll);
            Invoke("HideSetting", 0.667f);
        }
    }
    public void cashCloseFtn()
    {
        if (!QuitMenu)
        {
            Time.timeScale = 1;
            if (PlayerPrefs.GetInt("SoundOff") == 0)
            {
                AudioListener.volume = 1.0f;
            }
            cashBundle = false;
            storeBundle = false;
            cashBundlePanel.SetActive(false);
            storeBundlePanel.SetActive(false);
            cashPanelActive = false;
            plusBG.sprite = plusTexture;
            settingImage.color = new Color(255, 255, 255, 255);
            if (socialMediaPanelActive)
            {
                socialMediaPanelActive = false;
                socialMediaPanel.GetComponent<Animation>().Play("socialMedia1End", PlayMode.StopAll);
                Invoke("HideSocialMedia", 0.5f);
            }
            if (settingPanelActive)
            {
                settingPanelActive = false;
                //settingPanel.GetComponent<Animation>().Play("settingPanelClose", PlayMode.StopAll);
                Invoke("HideSetting", 0.667f);
            }
            //HideTopCenterBanner();
        }
    }

    public void paidItemFtn()
    {
        if (!QuitMenu)
        {
            freeItemPanel.SetActive(false);
            paidItemPanel.SetActive(true);

            paidText.color = selectedColor;
            freeText.color = new Color(255, 255, 255, 255);
            freeFooter.sprite = normalFooter;
            paidFooter.sprite = selectedFooter;
        }
    }
    public void freeItemFtn()
    {
        if (!QuitMenu)
        {
            freeItemPanel.SetActive(true);
            paidItemPanel.SetActive(false);

            paidText.color = new Color(255, 255, 255, 255);
            freeText.color = selectedColor;
            freeFooter.sprite = selectedFooter;
            paidFooter.sprite = normalFooter;
        }
    }
    public void storeBundleFtn()
    {
        if (!QuitMenu)
        {
            storeBundlePanel.SetActive(true);
            plusBG.sprite = closeTexture;
            //plus.SetActive (false);
            //close.SetActive (true);
            cashPanelActive = true;
            cashBundle = false;
            storeBundle = true;
            settingImage.color = new Color(255, 255, 255, 255);
            if (socialMediaPanelActive)
            {
                socialMediaPanelActive = false;
                socialMediaPanel.GetComponent<Animation>().Play("socialMedia1End", PlayMode.StopAll);
                Invoke("HideSocialMedia", 0.5f);
            }
            if (settingPanelActive)
            {
                settingPanelActive = false;
               // settingPanel.GetComponent<Animation>().Play("settingPanelClose", PlayMode.StopAll);
                Invoke("HideSetting", 0.667f);
            }
        }
        //HideTopCenterBanner();	
    }


    public void settingFtn()
    {
        if (!QuitMenu)
        {
            if (!settingPanelActive)
            {
                //socialMediaPanel.SetActive (false);
                settingPanel.SetActive(true);
                settingImage.color = selectedColor;
                settingPanelActive = true;
                if (socialMediaPanelActive)
                {
                    socialMediaPanelActive = false;
                    //socialMediaPanel.GetComponent<Animation>().Play("socialMedia1End", PlayMode.StopAll);
                    Invoke("HideSocialMedia", 0.5f);
                }
            }
            else
            {
                settingPanelActive = false;
               // settingPanel.GetComponent<Animation>().Play("settingPanelClose", PlayMode.StopAll);
                settingImage.color = new Color(255, 255, 255, 255);
                Invoke("HideSetting", 0.667f);
            }
        }
    }
    void HideSetting()
    {
        settingPanel.SetActive(false);
    }

    void HideSocialMedia()
    {
        socialMediaPanel.SetActive(false);
    }
    public void PrivacyPolicy()
    {
        Application.OpenURL("https://gamershivepro.wordpress.com/");
    }
    public void socialMediaFtn()
    {
        if (!QuitMenu)
        {
            if (!socialMediaPanelActive)
            {

                socialMediaPanel.SetActive(true);
                socialMediaPanelActive = true;
            }
            else
            {
                socialMediaPanelActive = false;

                socialMediaPanel.GetComponent<Animation>().Play("socialMedia1End", PlayMode.StopAll);
                Invoke("HideSocialMedia", 0.667f);

            }
            if (settingPanelActive)
            {
                settingPanelActive = false;
               // settingPanel.GetComponent<Animation>().Play("settingPanelClose", PlayMode.StopAll);
                Invoke("HideSetting", 0.667f);
            }

            settingImage.color = new Color(255, 255, 255, 255);
        }
    }

    public void soundFtn()
    {
        if (!loadingbar && !QuitMenu)
        {
            if (PlayerPrefs.GetInt("SoundOff") == 0)
                GetComponent<AudioSource>().PlayOneShot(clickSound);
            if (soundBool)
            {
                soundBool = false;
                soundBG.sprite = soundOff;
                //sound.normal.background=Resources.Load<Texture2D>("sound-off");
                PlayerPrefs.SetInt("SoundOff", 1);
                PlayerPrefs.Save();
            }
            else
            {
                soundBool = true;
                soundBG.sprite = soundOn;
                //sound.normal.background= Resources.Load<Texture2D>("sound-on");
                PlayerPrefs.SetInt("SoundOff", 0);
                PlayerPrefs.Save();
            }
        }
    }

    public void musicFtn()
    {

        if (!loadingbar && !QuitMenu)
        {
            if (PlayerPrefs.GetInt("SoundOff") == 0)
                GetComponent<AudioSource>().PlayOneShot(clickSound);
            if (musicBool)
            {
                musicBool = false;
                BgMusic.GetComponent<AudioSource>().Pause();

                //BgMusic.SetActive(false);
                musicBG.sprite = musicOff;
                //music.normal.background=Resources.Load<Texture2D>("music_offbt");
                PlayerPrefs.SetInt("MusicOff", 1);
                PlayerPrefs.Save();

            }
            else
            {
                musicBool = true;
                BgMusic.GetComponent<AudioSource>().Play();
                musicBG.sprite = musicOn;
                //BgMusic.SetActive(true);
                //music.normal.background= Resources.Load<Texture2D>("music_onbt");
                PlayerPrefs.SetInt("MusicOff", 0);
                PlayerPrefs.Save();
            }
        }
    }
    public void helpFtn()
    {
        if (!loadingbar && !QuitMenu)
        {
            if (PlayerPrefs.GetInt("SoundOff") == 0)
                GetComponent<AudioSource>().PlayOneShot(clickSound);
            helpbool = true;
            settingImage.color = new Color(255, 255, 255, 255);
            helpScreenPanel.SetActive(true);
            socialMediaPanel.SetActive(false);

            if (settingPanelActive)
            {
                settingPanelActive = false;
               // settingPanel.GetComponent<Animation>().Play("settingPanelClose", PlayMode.StopAll);
                Invoke("HideSetting", 0.667f);
            }
            //HideTopCenterBanner();
        }
    }

    public void closehelpFtn()
    {
        helpbool = false;
        mainSceen = true;
        helpScreenPanel.SetActive(false);
        settingPanel.SetActive(false);
    }
    void hideComingSoon()
    {
        comingSoonPlayer.SetActive(false);
        if (comingSoonParent != null)
            comingSoonParent.GetChild(1).gameObject.SetActive(false);
        //comingSoon.SetActive (false);
    }
    Transform comingSoonParent;
    public void ComingSoonFtn(string buttonName)
    {
        if (!QuitMenu)
        {
            if (socialMediaPanelActive)
            {
                socialMediaPanelActive = false;
                socialMediaPanel.GetComponent<Animation>().Play("socialMedia1End", PlayMode.StopAll);
                Invoke("HideSocialMedia", 0.5f);
            }
            if (settingPanelActive)
            {
                settingPanelActive = false;
                //settingPanel.GetComponent<Animation>().Play("settingPanelClose", PlayMode.StopAll);
                Invoke("HideSetting", 0.667f);
            }
            if (buttonName.Equals("multiPlayer"))
            {
                if (comingSoonParent != null)
                    comingSoonParent.GetChild(1).gameObject.SetActive(false);
                comingSoonPlayer.SetActive(true);
            }
            else
            {
                if (comingSoonParent != null)
                    comingSoonParent.GetChild(1).gameObject.SetActive(false);
                comingSoonParent = currentBtn.currentSelectedGameObject.transform;
                comingSoonParent.GetChild(1).gameObject.SetActive(true);
                //comingSoon.SetActive(true);
                //comingSoon.transform.position=new Vector3(	(currentBtn.currentSelectedGameObject.transform.position.x+150),comingSoon.transform.position.y,comingSoon.transform.position.z);
            }
            CancelInvoke("hideComingSoon");
            Invoke("hideComingSoon", 1f);
        }
    }
    public void MoreGamesFtn()
    {
        if (!loadingbar)
        {

            if (PlayerPrefs.GetInt("SoundOff") == 0)
                GetComponent<AudioSource>().PlayOneShot(clickSound);
            Application.OpenURL("https://play.google.com/store/apps/developer?id=Knock-Solutions+%28Gamtech%29+Inc.");
        }
    }
    public void shareFtn()
    {
        if (!loadingbar)
        {
            if (socialMediaPanelActive)
            {
                socialMediaPanelActive = false;
                socialMediaPanel.GetComponent<Animation>().Play("socialMedia1End", PlayMode.StopAll);
                Invoke("HideSocialMedia", 0.5f);
            }
            if (settingPanelActive)
            {
                settingPanelActive = false;
                //settingPanel.GetComponent<Animation>().Play("settingPanelClose", PlayMode.StopAll);
                Invoke("HideSetting", 0.667f);
            }
            if (PlayerPrefs.GetInt("SoundOff") == 0)
                GetComponent<AudioSource>().PlayOneShot(clickSound);
#if !UNITY_EDITOR
					SocialNetworks.ShareURL("Bike Attack Race", "https://play.google.com/store/apps/details?id=com.smashbike.racing.game.bikers");
#endif
        }
    }


    public void dailyBonus()
    {
        spinPanel.SetActive(true);
        bgImage.SetActive(false);
        spinPanelActive = true;
        buttonPanel.SetActive(false);
        bottomBarPanel.SetActive(true);
        if (socialMediaPanelActive)
        {
            socialMediaPanelActive = false;
            socialMediaPanel.GetComponent<Animation>().Play("socialMedia1End", PlayMode.StopAll);
            Invoke("HideSocialMedia", 0.5f);
        }
        if (settingPanelActive)
        {
            settingPanelActive = false;
            //settingPanel.GetComponent<Animation>().Play("settingPanelClose", PlayMode.StopAll);
            Invoke("HideSetting", 0.667f);
        }

    }


    public void spinBack()
    {
        if (spinner.GetComponent<Rigidbody>().velocity.magnitude == 0 && !bonusGiven)
        {
            spinPanel.SetActive(false);
            bgImage.SetActive(true);
            spinPanelActive = false;
            buttonPanel.SetActive(true);
            bottomBarPanel.SetActive(false);
        }
        HideTopCenterBanner();
    }


    public void ticketStoreFtn()
    {
        if (!bonusGiven)
        {
            ticketStore.SetActive(true);
            ticketStoreActive = true;
            insufficientTicketPanel.SetActive(false);
            insufficientTicket = false;

        }
    }
    public void closeticketStoreFtn()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.GetInt("SoundOff") == 0)
        {
            AudioListener.volume = 1.0f;
        }
        ticketStoreActive = false;
        ticketStore.SetActive(false);

    }

    public void ShowTopCenterBanner()
    {
#if !UNITY_EDITOR
		//Ads_Manager.Instance.ShowAdmobBanner ();
#endif
    }

    public void HideTopCenterBanner()
    {
#if !UNITY_EDITOR
		//Ads_Manager.Instance.HideAdmobBanner ();
#endif
    }
    public void HideLargeBanner()
    {
#if !UNITY_EDITOR
		//Ads_Manager.Instance.HideAdmob_LargeBanner ();
#endif
    }
    public void ShowLargeBanner()
    {
#if !UNITY_EDITOR
		//Ads_Manager.Instance.ShowAdmob_LargeBanner ();
#endif
    }
    public void ShowQuitAd()
    {
#if !UNITY_EDITOR
		//Ads_Manager.Instance.ShowUnity_ElseAdmob ();
#endif
    }
    public void btnClicksound()
    {
        if (PlayerPrefs.GetInt("SoundOff") == 0)
            GetComponent<AudioSource>().PlayOneShot(clickSound);
    }


    public void rateApp()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=com.smashbike.racing.game.bikers");
    }

    //Add For New UI
    public void OnExitButoon()
    {
        if (!helpbool && (!cashBundle && !storeBundle) && !spinPanelActive && !insufficientTicket && !dailyBonusActive)
        {
            QuitMenu = !QuitMenu;
            if (QuitMenu)
            {
                quitPanel.SetActive(true);
                //Time.timeScale=0.0f;
                /*                if (PlayerPrefs.GetInt("SoundOff") == 0)
                               {
                                   AudioListener.volume = 0.0f;
                               } */
                HideTopCenterBanner();
                ShowLargeBanner();
                Invoke("ShowQuitAd", 0.1f);
            }
            else if (!QuitMenu)
            {
                quitPanel.SetActive(false);
                Time.timeScale = 1.0f;
                /*                 if (PlayerPrefs.GetInt("SoundOff") == 0)
                                {
                                    AudioListener.volume = 1.0f;
                                } */
                HideLargeBanner();
                ShowTopCenterBanner();
            }
        }

    }
}