using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class storeBtnHandlers : MonoBehaviour
{
    public RectTransform[] selectionImages;
    public AudioClip clickSound;
    public GameObject bgMusic;
    public weaponHandler weaponhandler;
    public Sprite heavyBikeAcceleration, heavyBikeHandling, heavyBikePower, harleyBikeAcceleration, harleyBikeHandling, harleyBikePower;
    public GameObject cashBundlePanel, warningBox;//,bikeCanvas;
    public Image plusBG, acceleration, power, handling;
    public Sprite plusTexture, closeTexture;
    public Color selectedColor;
    public Color normalColor;
    int counter;
    int bike;
    public GameObject cart;
    public Text priceText;
    int shirtVsBike;
    // public Image shirtImage,bikeImage,pistolImg,batImg;
    public GameObject batPanel, pistolPanel, bikePanel, arrowLeft, arrowRight, bikeModel, shirtPanel, heavybikeColorPanel, harleybikeColorPanel;
    public GameObject levelselection, heavyBike, harleyBike;
    bool bundlePanelActive;
    public static bool levelSelectionActive;
    string bikerTexture, bikeTexture;
    public static string lastSelectedValue, lastSelectedBike, lastSelectedBat, lastSelectedPistol;
    public GameObject nextItem, previousItem, startButton, bikeSpecs, batSpecs, weaponSpecs;
    private int characteristicNumber = 0;
    [HideInInspector] public string currentShirt, currentBikeColor;

    public void btnClicksound()
    {
        if (PlayerPrefs.GetInt("SoundOff") == 0)
            GetComponent<AudioSource>().PlayOneShot(clickSound);
    }

    int selectedType;
    /* 	public	void setSelectionSize(int type){
            selectedType = type;
            foreach (RectTransform rect in selectionImages) 
            {
                rect.anchoredPosition = new Vector2 (51.5f,rect.anchoredPosition.y);
                rect.localScale = new Vector3 (.75f,.75f,.75f);
            }
                selectionImages[type].anchoredPosition = new Vector2 (75f,selectionImages[type].anchoredPosition.y);
                selectionImages[type].localScale = new Vector3 (1f,1f,1f);		
        } */

    void Awake()
    {
        // if (bikeSelection.backToMode)
        // {

        if (bikeSelection.bikeCount == 1)
        {
            bike = 0;
            heavyBike.SetActive(true);
            harleyBike.SetActive(true);
            arrowLeft.SetActive(false);
            arrowRight.SetActive(true);
            acceleration.sprite = heavyBikeAcceleration;
            handling.sprite = heavyBikeHandling;
            power.sprite = heavyBikePower;
        }
        else if (bikeSelection.bikeCount == 2)
        {
            bike = 1;
            arrowLeft.SetActive(true);
            arrowRight.SetActive(false);
            heavyBike.SetActive(true);
            harleyBike.SetActive(true);
            acceleration.sprite = harleyBikeAcceleration;
            handling.sprite = harleyBikeHandling;
            power.sprite = harleyBikePower;
            Invoke("Move_Camera", 0.1f);

        }
        // }
        if (PlayerPrefs.GetInt("MusicOff") == 0)
        {
            bgMusic.GetComponent<AudioSource>().Play();
        }
        else if (PlayerPrefs.GetInt("MusicOff") == 1)
        {
            bgMusic.GetComponent<AudioSource>().Pause();
        }
    }
    void Move_Camera()
    {
        cameraMove.instance.cameraSwitchFtn(1);
    }
    void Start()
    {
        plusBG.sprite = plusTexture;
        bundlePanelActive = false;
        //bikeSelection.bikeCount=1;
        counter = 0;
        /*         if (bikeSelection.bikeCount == 1)
                    bike = 0;
                else
                    bike = 1; */

        harleybikeColorPanel.SetActive(false);

        heavybikeColorPanel.SetActive(false);


        /*         if (bikeSelection.bikeCount == 1)
                {
                    bike = 0;
                    heavyBike.SetActive(true);
                    harleyBike.SetActive(true);

                    acceleration.sprite = heavyBikeAcceleration;
                    handling.sprite = heavyBikeHandling;
                    power.sprite = heavyBikePower;
                    arrowRight.SetActive(true);
                    arrowLeft.SetActive(false);


                }
                else if (bikeSelection.bikeCount == 2)
                {
                    bike = 1;
                    heavyBike.SetActive(true);
                    harleyBike.SetActive(true);
                    arrowRight.SetActive(false);
                    arrowLeft.SetActive(true);
                    acceleration.sprite = harleyBikeAcceleration;
                    handling.sprite = harleyBikeHandling;
                    power.sprite = harleyBikePower;
                } */
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && warningBox.activeInHierarchy)
        {
            warningBox.SetActive(false);
        }
        else
        if (Input.GetKeyDown(KeyCode.Escape) && bundlePanelActive)
        {

            if (!levelSelectionActive)
            {
                if (bundlePanelActive)
                {
                    cashBundlePanel.SetActive(false);
                    bundlePanelActive = false;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !weaponhandler.purchasingCostume && !weaponhandler.axePanelPurchasing && !weaponhandler.gunPanelPurchasing)
        {
            if (!levelSelectionActive)
            {
                if (bundlePanelActive)
                {
                    cashBundlePanel.SetActive(false);
                    bundlePanelActive = false;

                    //plusBG.sprite = plusTexture;
                }
                else
                {
                    SceneManager.LoadScene("mainMenu3");

                }
            }
            else
            {
                if (!LevelPlanesFuncs.playBtnClicked)
                {

                    levelSelectionActive = false;
                    //bikeCanvas.SetActive(true);
                    levelselection.SetActive(false);
                    if (selectedType == 1 || selectedType == 0)
                    {
                        bikePanel.SetActive(true);
                    }
                    else if (selectedType == 2)
                    {
                        batPanel.SetActive(true);
                    }
                    else if (selectedType == 3)
                    {
                        pistolPanel.SetActive(true);
                    }
                    //										bikeImage.color = selectedColor;
                    //										shirtImage.color = normalColor;
                    //										pistolImg.color = normalColor;
                    //										batImg.color = normalColor;
                    if (bike == 1)
                    {
                        arrowLeft.SetActive(true);
                        arrowRight.SetActive(false);
                    }

                    else
                    {
                        arrowLeft.SetActive(false);
                        arrowRight.SetActive(true);
                    }

                    if (bike == 0)
                    {
                        heavybikeColorPanel.SetActive(true);
                        harleybikeColorPanel.SetActive(false);
                    }
                    else if (bike == 1)
                    {
                        harleybikeColorPanel.SetActive(true);
                        heavybikeColorPanel.SetActive(false);
                    }


                    bikeModel.SetActive(true);
                    if (!PlayerPrefs.GetString("bundlePurchase").Equals("true"))
                    {
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !bundlePanelActive && weaponhandler.purchasingCostume && !weaponhandler.axePanelPurchasing && !weaponhandler.gunPanelPurchasing)
        {
            weaponhandler.cancelBikeCostume();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !bundlePanelActive && !weaponhandler.purchasingCostume && weaponhandler.axePanelPurchasing && !weaponhandler.gunPanelPurchasing)
        {
            weaponhandler.cancelaxePanelPurchasing();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !bundlePanelActive && !weaponhandler.purchasingCostume && !weaponhandler.axePanelPurchasing && weaponhandler.gunPanelPurchasing)
        {
            weaponhandler.cancelgunPanelpurchasing();
        }
    }

    public void dressCode()
    {
        //bikerTexture=	PlayerPrefs.GetString("shirtColor"); //khuram
        bikerTexture = currentShirt;
        if (bike == 0)
        {
            //bikeTexture="heavy"+	PlayerPrefs.GetString("bikeColor"); //khuram
            bikeTexture = "heavy" + currentBikeColor;
#if UNITY_EDITOR
            Debug.Log("bikeTexture: " + bikeTexture);
#endif
            heavyBike.transform.Find("bike_crb").transform.GetComponent<Renderer>().material.mainTexture = Resources.Load(bikeTexture) as Texture;
            heavyBike.transform.Find("biker_fat-022").transform.GetComponent<Renderer>().material.mainTexture = Resources.Load(bikerTexture) as Texture;
        }
        else if (bike == 1)
        {
            //bikeTexture="harley"+	PlayerPrefs.GetString("bikeColor"); //khuram
            bikeTexture = "harley" + currentBikeColor;
#if UNITY_EDITOR
            Debug.Log("harleyTexture: " + bikeTexture);
#endif
            harleyBike.transform.Find("bike_body").transform.GetComponent<Renderer>().material.mainTexture = Resources.Load(bikeTexture) as Texture;
            harleyBike.transform.Find("biker_fat-022").transform.GetComponent<Renderer>().material.mainTexture = Resources.Load(bikerTexture) as Texture;
        }
        //bikerModel.transform.FindChild ("biker_fat-022").transform.renderer.material.mainTexture= Resources.Load(bikerTexture) as Texture;
    }
    public void shirtClicked()
    {
        if (!bundlePanelActive)
        {
            weaponhandler.resetDressPanlePrice();
            //priceText.text = "";
            //cart.SetActive(false);
            shirtVsBike = 1;
            bikePanel.SetActive(true);
            batPanel.SetActive(false);
            pistolPanel.SetActive(false);
            /* 						shirtImage.color = selectedColor;
                                    bikeImage.color = normalColor;
                                    pistolImg.color = normalColor;
                                    batImg.color = normalColor; */
            shirtPanel.SetActive(true);
            heavybikeColorPanel.SetActive(false);
            harleybikeColorPanel.SetActive(false);
        }
    }
    public void bikeClicked()
    {
        if (!bundlePanelActive)
        {
            weaponhandler.resetBikePanlePrice();
            //	priceText.text = "";
            //	cart.SetActive(false);
            shirtVsBike = 0;
            bikePanel.SetActive(true);
            batPanel.SetActive(false);
            pistolPanel.SetActive(false);

            /* 						bikeImage.color = selectedColor;
                                    shirtImage.color = normalColor;
                                    pistolImg.color = normalColor;
                                    batImg.color = normalColor; */
            shirtPanel.SetActive(false);
            if (bike == 0)
            {
                heavybikeColorPanel.SetActive(true);
                harleybikeColorPanel.SetActive(false);
            }
            else if (bike == 1)
            {
                harleybikeColorPanel.SetActive(true);
                heavybikeColorPanel.SetActive(false);
            }
        }
    }




    public void home()
    {
        //		 if ( weaponhandler.purchasingCostume ) {
        //			weaponhandler.cancelBikeCostume();
        //		}
        //		if (weaponhandler.axePanelPurchasing) {
        //			weaponhandler.cancelaxePanelPurchasing();
        //		}
        //				if (weaponhandler.gunPanelPurchasing) {
        //						weaponhandler.cancelgunPanelpurchasing ();
        //				} 
        SceneManager.LoadScene("mainMenu3");



    }
    public void previousBike()
    {
        if (!bundlePanelActive)
        {
            if (bike == 1)
            {


                //priceText.text = "";
                //cart.SetActive(false);
                bikeSelection.bikeCount = 1;
                bike = 0;
                //heavyBike.SetActive (true);
                //harleyBike.SetActive (false);
                arrowLeft.SetActive(false);
                arrowRight.SetActive(true);
                acceleration.sprite = heavyBikeAcceleration;
                handling.sprite = heavyBikeHandling;
                power.sprite = heavyBikePower;
                //				heavyBike.GetComponent<runing>().startRotation();
                // if (shirtVsBike == 0) {
                // shirtPanel.SetActive (false);
                // heavybikeColorPanel.SetActive (true);
                // harleybikeColorPanel.SetActive (false);
                // } else {
                // shirtPanel.SetActive (true);
                // heavybikeColorPanel.SetActive (false);
                // harleybikeColorPanel.SetActive (false);
                // }
            }
            dressCode();
            weaponhandler.resettingBars();
        }
    }
    public void nextBike()
    {
        //		print (bundlePanelActive+ "  "+ bike);
        if (!bundlePanelActive)
        {
            if (bike == 0)
            {
                bikeSelection.bikeCount = 2;
                bike = 1;
                //heavyBike.SetActive (false);
                //harleyBike.SetActive (true);
                arrowRight.SetActive(false);
                arrowLeft.SetActive(true);
                acceleration.sprite = harleyBikeAcceleration;
                handling.sprite = harleyBikeHandling;
                power.sprite = harleyBikePower;
            }
            dressCode();
            weaponhandler.resettingBars();
        }
    }
    public void nextBtnFtn()
    {
        if (!bundlePanelActive)
        {
            levelselection.SetActive(true);
            bikePanel.SetActive(false);
            batPanel.SetActive(false);
            pistolPanel.SetActive(false);
            arrowLeft.SetActive(false);
            arrowRight.SetActive(false);
            bikeModel.SetActive(false);
            levelSelectionActive = true;
            /*             Debug.Log(PlayerPrefs.GetString("bikeColor"));
                        Debug.Log(PlayerPrefs.GetString("shirtColor"));
                        Debug.Log(PlayerPrefs.GetString("batColor"));
                        Debug.Log(PlayerPrefs.GetInt("batSelect"));
                        Debug.Log(PlayerPrefs.GetInt("pistolSelect")); */

            if (GameAnalytics.instance)
            {
                GameAnalytics.instance.SelectedItemEvent(bike + System.String.Empty);
                GameAnalytics.instance.SelectedItemEvent(PlayerPrefs.GetString("bikeColor"));
                GameAnalytics.instance.SelectedItemEvent(PlayerPrefs.GetString("shirtColor"));
                GameAnalytics.instance.SelectedItemEvent(PlayerPrefs.GetString("batColor"));
                GameAnalytics.instance.SelectedItemEvent(PlayerPrefs.GetInt("batSelect") + System.String.Empty);
                GameAnalytics.instance.SelectedItemEvent(PlayerPrefs.GetInt("pistolSelect") + System.String.Empty);
            }
            // HideTopCenterBanner();
        }
    }


    public void changeStoreOptionsFtn(int counter)
    {
        if (!bundlePanelActive)
        {
            //						print ("batselect: " + PlayerPrefs.GetInt ("batSelect"));

            if (counter == 0)
            {
                counter += 1;
                bikePanel.SetActive(false);
                batPanel.SetActive(true);
                pistolPanel.SetActive(false);
                arrowLeft.SetActive(false);
                weaponhandler.resetBatPanelPrice();
            }
            else if (counter == 1)
            {
                counter += 1;
                bikePanel.SetActive(false);
                batPanel.SetActive(false);
                pistolPanel.SetActive(true);
                weaponhandler.resetPistolPanelPrice();
            }
            else if (counter == 2)
            {
                //bikeCanvas.SetActive(false);
                levelselection.SetActive(true);
                bikePanel.SetActive(false);
                batPanel.SetActive(false);
                pistolPanel.SetActive(false);
                arrowLeft.SetActive(false);
                //								arrowRight.SetActive (false);
                bikeModel.SetActive(false);
                levelSelectionActive = true;
                //HideTopCenterBanner();

            }
        }
    }

    public void backBtn()
    {
        if (warningBox.activeInHierarchy)
        {
            warningBox.SetActive(false);
            return;
        }
        if (!levelSelectionActive)
        {
            if (!bundlePanelActive)
            {
                if (counter == 0)
                {
                    SceneManager.LoadScene("mainMenu3");

                }
                else
                {

                    counter = 1;

                    bikePanel.SetActive(true);
                    batPanel.SetActive(false);
                    pistolPanel.SetActive(false);
                    if (bike == 1)
                        arrowLeft.SetActive(true);
                    else
                        arrowRight.SetActive(true);

                    if (!PlayerPrefs.GetString("bundlePurchase").Equals("true"))
                    {
                    }


                }

            }
        }
        else
        {
            if (!LevelPlanesFuncs.playBtnClicked)
            {

                levelSelectionActive = false;
                //bikeCanvas.SetActive (true);
                levelselection.SetActive(false);
                if (selectedType == 1 || selectedType == 0)
                {
                    bikePanel.SetActive(true);
                }
                else if (selectedType == 2)
                {
                    batPanel.SetActive(true);
                }
                else if (selectedType == 3)
                {
                    pistolPanel.SetActive(true);
                }

                if (bike == 1)
                {
                    arrowLeft.SetActive(true);
                    arrowRight.SetActive(false);
                }
                else
                {
                    arrowLeft.SetActive(false);
                    arrowRight.SetActive(true);
                }

                if (bike == 0)
                {
                    heavybikeColorPanel.SetActive(true);
                    harleybikeColorPanel.SetActive(false);
                }
                else if (bike == 1)
                {
                    harleybikeColorPanel.SetActive(true);
                    heavybikeColorPanel.SetActive(false);
                }
                //				bikePanel.SetActive (true);
                //				batPanel.SetActive (false);
                //				pistolPanel.SetActive (false);
                //			

                bikeModel.SetActive(true);
                if (!PlayerPrefs.GetString("bundlePurchase").Equals("true"))
                {
                }

            }
        }
    }


    public void cashBundleFtn()
    {
        if (!bundlePanelActive)
        {

            cashBundlePanel.SetActive(true);
            bundlePanelActive = true;

            //plusBG.sprite = closeTexture;
        }
        //		else {
        //			cashBundlePanel.SetActive (false);
        //			bundlePanelActive = false;
        //			//plusBG.sprite = plusTexture;
        //		}
    }
    public void cashCloseFtn()
    {
        cashBundlePanel.SetActive(false);
        bundlePanelActive = false;

    }
    public void NextPlayerCharacteristic()
    {
        characteristicNumber += 1;
        SwitchCharacteristic();

        btnClicksound();
    }
    public void SwitchCharacteristic()
    {
        switch (characteristicNumber)
        {
            case 0:
                heavybikeColorPanel.SetActive(false);
                harleybikeColorPanel.SetActive(false);
                bikeSpecs.SetActive(true);
                previousItem.SetActive(false);
                if (bike == 0)
                {
                    arrowLeft.SetActive(false);
                    arrowRight.SetActive(true);
                }
                else
                {
                    arrowLeft.SetActive(true);
                    arrowRight.SetActive(false);
                }
                break;
            case 1:
                if (bike == 0)
                {
                    heavybikeColorPanel.SetActive(true);
                    harleybikeColorPanel.SetActive(false);
                }
                else
                {
                    heavybikeColorPanel.SetActive(false);
                    harleybikeColorPanel.SetActive(true);
                }
                bikeSpecs.SetActive(false);
                arrowLeft.SetActive(false);
                arrowRight.SetActive(false);
                previousItem.SetActive(true);
                shirtPanel.SetActive(false);
                break;
            case 2:
                heavybikeColorPanel.SetActive(false);
                harleybikeColorPanel.SetActive(false);
                shirtPanel.SetActive(true);
                batPanel.SetActive(false);
                batSpecs.SetActive(false);
                break;
            case 3:
                batPanel.SetActive(true);
                batSpecs.SetActive(true);
                shirtPanel.SetActive(false);
                pistolPanel.SetActive(false);
                weaponSpecs.SetActive(false);
                break;
            case 4:
                pistolPanel.SetActive(true);
                weaponSpecs.SetActive(true);
                batPanel.SetActive(false);
                batSpecs.SetActive(false);
                startButton.SetActive(false);
                nextItem.SetActive(true);
                break;
            case 5:
                pistolPanel.SetActive(false);
                weaponSpecs.SetActive(false);
                nextItem.SetActive(false);
                startButton.SetActive(true);
                break;
        }
    }
    public void PreviousPlayerCharacteristic()
    {
        characteristicNumber -= 1;
        SwitchCharacteristic();
        btnClicksound();
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

}
