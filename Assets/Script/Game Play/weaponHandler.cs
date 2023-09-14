using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class weaponHandler : MonoBehaviour
{
    //Add for UI
    public GameObject NextButton;
    public GameObject CancelBtn;
    public GameObject BackButton;

    public int PlayerCostumeNum, PlayerBikeColorNum;

    public bool checker;
    public Image damage, range;
    public Image batDamage, batrange;
    public GameObject closePurchase, backBtnImage, bikeImage, shirtImage;
    public storeBtnHandlers dressPlayer;
    bool insufficientPanelActive;
    public GameObject insufficientCash;
    bool startAnimatingCash;
    int updatedCash;
    public Text cash;

    public Sprite yellowBatFree, blueBatFree, redBatFree, axeFree, shotGunFree,pistolFree,missileFree;
    public Sprite yellowBatPurchase, blueBatPurchase, redBatPurchase, axePurchase, shotGunPurchase,pistolPurchase,missilePurchase;
    public Text priceText;
    public GameObject cart;
    public GameObject startBtn, buyItem;
    public Image nextImageBG;
    public Sprite nextImage, buyImage;

    EventSystem currentBtn;
    public Color green, white;
    string selectedObjectName;
    //public Image greyBikeLock, redBikeLock, blueBikeLock, blackBikeLock;
    //public Image greyHarleyLock, redHarleyLock, blueHarleyLock, orangeHarleyLock;

    public Image[] harleyBikeColorsLock, heavyBikeColorsLock;

    public Image[] PlayerCostumeLock;

    public Image[] PlayerbatLock;

    public Image[] PlayerPistolLock;

    public Image[] LevelLock;

    public Text[] LevelText;
    //public Sprite shirt10Free,shirt2Free, shirt3Free,shirt4Free, shirt5Free,shirt6Free,shirt7Free,shirt8Free,shirt9Free;
    //public Sprite shirt10Purchase,shirt2Purchase,shirt3Purchase,shirt4Purchase,shirt5Purchase,shirt6Purchase,shirt7Purchase,shirt8Purchase,shirt9Purchase;
    void Awake()
    {
        if (!PlayerPrefs.HasKey("shirtColor"))
            PlayerPrefs.SetString("shirtColor", "shirt1");
        if (!PlayerPrefs.HasKey("bikeColor"))
            PlayerPrefs.SetString("bikeColor", "yellow");
        if (!PlayerPrefs.HasKey("batSelect"))
            PlayerPrefs.SetInt("batSelect", 0);
        if (!PlayerPrefs.HasKey("pistolSelect"))
            PlayerPrefs.SetInt("pistolSelect", 0);

        PlayerPrefs.SetString("pistolPurchases", "true");
        PlayerPrefs.SetString("shirt1Purchased", "true");
        PlayerPrefs.SetString("yellowPurchased", "true");
        PlayerPrefs.SetString("pistolPurchased", "true");

        //PlayerPrefs.SetString("missilePurchased","false");
        PlayerPrefs.SetInt ("cash",100000);
        cash.text = PlayerPrefs.GetInt("cash") + System.String.Empty;
        currentBtn = EventSystem.current;
    }


    void Start()
    {
        dressPlayer.currentShirt = PlayerPrefs.GetString("shirtColor");
        dressPlayer.currentBikeColor = PlayerPrefs.GetString("bikeColor");
        dressPlayer.dressCode();

        //if(Application.isEditor)
        //PlayerPrefs.SetInt ("cash",500000);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        insufficientPanelActive = false;
        startAnimatingCash = false;

        resetTexture();
        //startbarColor(heavybikeBarImage);

    }

    public void startbarColor(Image[] parentBar)
    {
        if (bikeSelection.bikeCount == 1)
        {
            if (PlayerPrefs.GetString("bikeColor").Equals("orange"))
            {
                PlayerPrefs.SetString("bikeColor", "black");
            }
        }
        else
        {

            if (PlayerPrefs.GetString("bikeColor").Equals("black"))
            {
                PlayerPrefs.SetString("bikeColor", "orange");
            }
        }

        //		print(currentBtn.currentSelectedGameObject.name);
        foreach (Image childs in parentBar)
        {

            childs.color = white;

        }
        //		print("bar: "+ parentBar[0].transform.parent.parent+ " : "+PlayerPrefs.GetString ("bikeColor"));
        parentBar[0].transform.parent.parent.Find(PlayerPrefs.GetString("bikeColor")).transform.GetChild(0).GetComponent<Image>().color = green;

    }
    public void bikerbarColor(Image[] parentBar)
    {
        //print(currentBtn.currentSelectedGameObject.name);
        foreach (Image childs in parentBar)
        {
            childs.color = white;
        }
        parentBar[0].transform.parent.parent.Find(PlayerPrefs.GetString("shirtColor")).transform.GetChild(0).GetComponent<Image>().color = green;
    }
    public void resetTexture()
    {
        //		print ("preference: "+PlayerPrefs.GetString ("pistolPurchased"));
        if (PlayerPrefs.GetString("axePurchased").Equals("true"))
        {
            PlayerbatLock[4].enabled = false;
            Common.changeGameDictionary("playerbat4locked", "no");
        }
        else
        {
            PlayerbatLock[4].enabled = true;
            Common.changeGameDictionary("playerbat4locked", "yes");

        }
        if (PlayerPrefs.GetString("yellowBatPurchased").Equals("true"))
        {
            PlayerbatLock[1].enabled = false;
            Common.changeGameDictionary("playerbat1locked", "no");
        }
        else
        {
            PlayerbatLock[1].enabled = true;
            Common.changeGameDictionary("playerbat1locked", "yes");

        }
        if (PlayerPrefs.GetString("blueBatPurchased").Equals("true"))
        {
            PlayerbatLock[3].enabled = false;
            Common.changeGameDictionary("playerbat3locked", "no");

        }
        else
        {
            PlayerbatLock[3].enabled = true;
            Common.changeGameDictionary("playerbat3locked", "yes");

        }
        if (PlayerPrefs.GetString("redBatPurchased").Equals("true"))
        {
            PlayerbatLock[2].enabled = false;
            Common.changeGameDictionary("playerbat2locked", "no");

        }
        else
        {
            PlayerbatLock[2].enabled = true;
            Common.changeGameDictionary("playerbat2locked", "yes");

        }
        if (PlayerPrefs.GetString("shotgunPurchased").Equals("true"))
        {
            PlayerPistolLock[1].enabled = false;
            Common.changeGameDictionary("playerpistol1locked", "no");
        }
        else
        {
            PlayerPistolLock[1].enabled = true;
            Common.changeGameDictionary("playerpistol1locked", "yes");

        }
        if (PlayerPrefs.GetString("pistolPurchased").Equals("true"))
        {
            PlayerPistolLock[0].enabled = false;
            Common.changeGameDictionary("playerpistol0locked", "no");

        }
        else
        {
            PlayerPistolLock[0].enabled = true;
            Common.changeGameDictionary("playerpistol0locked", "yes");

        }
        if (PlayerPrefs.GetString("missilePurchased").Equals("true"))
        {
            PlayerPistolLock[2].enabled = false;
            Common.changeGameDictionary("playerpistol2locked", "no");

        }
        else
        {
            PlayerPistolLock[2] .enabled = true;
            Common.changeGameDictionary("playerpistol2locked", "yes");

        }


        if(PlayerPrefs.GetString("greyPurchased").Equals("true"))
        {
            Common.changeGameDictionary("player1bikecolor1locked", "no");
            Common.changeGameDictionary("player2bikecolor1locked", "no");
        }
        if (PlayerPrefs.GetString("bluePurchased").Equals("true"))
        {
            Common.changeGameDictionary("player1bikecolor2locked", "no");
            Common.changeGameDictionary("player2bikecolor2locked", "no");
        }
        if (PlayerPrefs.GetString("redPurchased").Equals("true"))
        {
            Common.changeGameDictionary("player1bikecolor3locked", "no");
            Common.changeGameDictionary("player2bikecolor3locked", "no");
        }



        for (int i = 0; i < Common.totalPlayerCostumes; i++)
        {
            if(Common.getGameDictionaryData("player1costume" + i + "locked") == "no")
            {
                PlayerCostumeLock[i].enabled = false;
            }
            else
            {
                PlayerCostumeLock[i].enabled = true;
            }
        }
        int sLength = PlayerCostumeLock.Length;
        for (int i = 1; i <= sLength; i++)
        {
            if (PlayerPrefs.GetString("shirt" + i + "Purchased").Equals("true"))
            {
                PlayerCostumeLock[i - 1].enabled = false;
                Common.changeGameDictionary("player1costume" + (i - 1) + "locked", "no");
                //Common.changeGameDictionary("player2costume" + (i - 1) + "locked", "no");
            }
            else
            {
                PlayerCostumeLock[i - 1].enabled = true;
                Common.changeGameDictionary("player1costume" + (i - 1) + "locked", "yes");
                //Common.changeGameDictionary("player2costume" + (i - 1) + "locked", "yes");
            }
        }

        for (int i = 0; i < Common.totalPlayerBikeColor; i++)
        {
            Debug.Log(Common.getGameDictionaryData("player1bikecolor" + i + "locked")+" - "+ "player1bikecolor" + i + "locked");

          //Common.changeGameDictionary("player1bikecolor" + i + "locked", "yes"); //For Reference

            if (Common.getGameDictionaryData("player1bikecolor" + i + "locked") == "no") {

                harleyBikeColorsLock[i].enabled = false;
            }
            else
            {
                harleyBikeColorsLock[i].enabled = true;
            }

            if (Common.getGameDictionaryData("player2bikecolor" + i + "locked") == "no")
            {

                heavyBikeColorsLock[i].enabled = false;
            }
            else
            {
                heavyBikeColorsLock[i].enabled = true;
            }
        }
        

        for(int i=0; i < Common.totalPlayerBat;i++)
        {
            if(Common.getGameDictionaryData("playerbat" + i + "locked") == "no")
            {
                PlayerbatLock[i].enabled = false;
            }
            else
            {
                PlayerbatLock[i].enabled = true;
            }
        }

        for(int i = 0; i < Common.totalPlayerPistol; i++)
        {
            if(Common.getGameDictionaryData("playerpistol" + i + "locked") == "no")
            {
                PlayerPistolLock[i].enabled = false;
            }
            else
            {
                PlayerPistolLock[i].enabled = true;
            }
        }

        //For Level Lock

        for(int i = 1; i <= LevelLock.Length; i++)
        {
            if (Common.getGameDictionaryData("level" + i + "locked") == "no")
            {
                LevelLock[i-1].enabled = false;
            }
            else
            {
                LevelLock[i-1].enabled = true;
            }
        }

        for(int i = 1; i <= LevelText.Length; i++)
        {
            if(Common.getGameDictionaryData("level" + i + "locked") == "no")
            {
                LevelText[i-1].enabled = true;
            }
            else
            {
                LevelText[i-1].enabled = false;
            }
        }
    }

    string cash1;
    int cash2;
    void Update()
    {

        cash.text = PlayerPrefs.GetInt("cash") + System.String.Empty;
        if (startAnimatingCash)
        {
            //				updatedCash = int.Parse (cash.text) - int.Parse (price1);
            //				PlayerPrefs.SetInt ("cash", updatedCash);
            if (cash2 > updatedCash)
            {
                cash2 -= 50;
                cash.text = cash2 + System.String.Empty;
            }
            if (cash2 <= updatedCash)
            {
                startAnimatingCash = false;
            }
        }

    }
    string priceSubstring;

    public void resetBikePanlePrice()
    {
        priceText.text = storeBtnHandlers.lastSelectedBike;
        if (priceText.text != "")
        {
            cart.SetActive(true);
            //startBtn.SetActive (false);
            buyItem.SetActive(true);
            BackButton.SetActive(false);
            dressPlayer.nextItem.SetActive(false);

        }
        else
        {
            cart.SetActive(false);
            //startBtn.SetActive (true);
            buyItem.SetActive(false);
            BackButton.SetActive(true);
            dressPlayer.nextItem.SetActive(true);
        }
    }


    public void resetDressPanlePrice()
    {
        priceText.text = storeBtnHandlers.lastSelectedValue;
        if (priceText.text != "")
        {
            cart.SetActive(true);
            //startBtn.SetActive (false);
            buyItem.SetActive(true);
            BackButton.SetActive(false);
            dressPlayer.nextItem.SetActive(false);
        }
        else
        {
            cart.SetActive(false);
            //startBtn.SetActive (true);
            buyItem.SetActive(false);
            BackButton.SetActive(true);
            dressPlayer.nextItem.SetActive(true);
        }
    }



    public void resetBatPanelPrice()
    {
        priceText.text = storeBtnHandlers.lastSelectedBat;
        if (priceText.text != "")
        {
            cart.SetActive(true);
            //startBtn.SetActive (false);
            buyItem.SetActive(true);
            BackButton.SetActive(false);
            dressPlayer.nextItem.SetActive(false);
        }
        else
        {
            cart.SetActive(false);
            //startBtn.SetActive (true);

            buyItem.SetActive(false);
            BackButton.SetActive(true);
            dressPlayer.nextItem.SetActive(true);
        }
    }


    public void resetPistolPanelPrice()
    {
        priceText.text = storeBtnHandlers.lastSelectedPistol;
        if (priceText.text != "")
        {
            cart.SetActive(true);
            //startBtn.SetActive (false);
            buyItem.SetActive(true);
            BackButton.SetActive(false);
            dressPlayer.nextItem.SetActive(false);
        }
        else
        {
            cart.SetActive(false);
            //startBtn.SetActive (true);
            buyItem.SetActive(false);
            BackButton.SetActive(true);
            dressPlayer.nextItem.SetActive(true);
        }
    }

    public bool purchasingCostume;
    public void cancelBikeCostume()
    {
        if (purchasingCostume)
        {
            if (shirtImage.activeInHierarchy)
            {
                PlayerPrefs.SetString("shirtColor", "shirt1");

            }
            if (bikeImage.activeInHierarchy)
            {
                PlayerPrefs.SetString("bikeColor", "yellow");

            }
            
            dressPlayer.dressCode();
            shirtImage.SetActive(true);
            bikeImage.SetActive(true);
            closePurchase.SetActive(false);
            backBtnImage.SetActive(true);

            storeBtnHandlers.lastSelectedBike = "";
            priceText.text = System.String.Empty;
            cart.SetActive(false);

            //startBtn.SetActive (true);
            buyItem.SetActive(false);
            BackButton.SetActive(true);
            dressPlayer.nextItem.SetActive(true);
            purchasingCostume = false;
            NextButton.SetActive(false);
            BackButton.SetActive(false);
        }
        else if (axePanelPurchasing)
        {
            cancelaxePanelPurchasing();
        }
        else if (gunPanelPurchasing)
        {
            cancelgunPanelpurchasing();
        }
    }

    public void resettingBars()
    {
        if (bikeSelection.bikeCount == 1)
        {
            //heavy		

            if (PlayerPrefs.GetString("bikeColor").Equals("orange"))
            {
                PlayerPrefs.SetString("bikeColor", "black");

                dressPlayer.dressCode();
            }

            //			if(PlayerPrefs.GetString("shirtColor"))
            //			{
            //
            //			}
        }
        else if(bikeSelection.bikeCount == 2)
        {
            if (PlayerPrefs.GetString("bikeColor") == ("black"))
            {
                PlayerPrefs.SetString("bikeColor", "orange");
                dressPlayer.dressCode();
            }

        }

    }

    public void bikeClicked(string name)
    {
        if (!insufficientPanelActive)
        { 
            priceSubstring = name.Substring(name.LastIndexOf(',') + 1);
            name = name.Substring(0, name.IndexOf(","));
            selectedObjectName = name;
            dressPlayer.currentBikeColor = name;
            //PlayerPrefs.SetString ("bikeColor", name); //khuram
            dressPlayer.dressCode();
            if (PlayerPrefs.GetString(name + "Purchased").Equals("true"))
            {
                storeBtnHandlers.lastSelectedBike = System.String.Empty;
                priceText.text = System.String.Empty;
                cart.SetActive(false);
                PlayerPrefs.SetString("bikeColor", name);
                //startBtn.SetActive (true);
                dressPlayer.nextItem.SetActive(true);
                buyItem.SetActive(false);
                BackButton.SetActive(true);
                closePurchase.SetActive(false);
                backBtnImage.SetActive(true);
                shirtImage.SetActive(true);
                //dressPlayer.dressCode(); //khuram
                //	nextImageBG.sprite= nextImage;
            }
            else
            {

                priceText.text = System.String.Empty + priceSubstring;
                storeBtnHandlers.lastSelectedBike = System.String.Empty + priceSubstring;
                closePurchase.SetActive(true);
                backBtnImage.SetActive(false);
                //shirtImage.SetActive(false);
                purchasingCostume = true;
                //priceText.text ="$1000";
                cart.SetActive(true);
                //startBtn.SetActive (false);
                dressPlayer.nextItem.SetActive(false);
                buyItem.SetActive(true);
                BackButton.SetActive(false);
                //nextImageBG.sprite= buyImage;
            }


        }
    }

    public void shirt2Clicked(string name)
    {
        if (!insufficientPanelActive)
        {
            //	print(currentBtn.currentSelectedGameObject.name);
            priceSubstring = name.Substring(name.LastIndexOf(',') + 1);
            name = name.Substring(0, name.IndexOf(","));
            selectedObjectName = name;
            dressPlayer.currentShirt = name;
            //PlayerPrefs.SetString ("shirtColor", name); //khuram
            dressPlayer.dressCode();

            if (PlayerPrefs.GetString(name + "Purchased").Equals("true"))
            {
                storeBtnHandlers.lastSelectedValue = System.String.Empty;
                priceText.text = System.String.Empty;
                cart.SetActive(false);
                PlayerPrefs.SetString("shirtColor", name);
                //startBtn.SetActive (true);
                dressPlayer.nextItem.SetActive(true);
                buyItem.SetActive(false);
                BackButton.SetActive(true);
                //dressPlayer.dressCode(); //khuram
                closePurchase.SetActive(false);
                backBtnImage.SetActive(true);
                //bikeImage.SetActive(true);
                //	nextImageBG.sprite= nextImage;
            }
            else
            {
                storeBtnHandlers.lastSelectedValue = System.String.Empty + priceSubstring;
                priceText.text = System.String.Empty + priceSubstring;
                cart.SetActive(true);
                //startBtn.SetActive (false);
                dressPlayer.nextItem.SetActive(false);
                buyItem.SetActive(true);
                BackButton.SetActive(false);
                closePurchase.SetActive(true);
                backBtnImage.SetActive(false);
                //bikeImage.SetActive(false);
                purchasingCostume = true;
                //nextImageBG.sprite= buyImage;	
            }

        }
    }
    public void freeBatClicked()
    {
        if (!insufficientPanelActive)
        {
            PlayerPrefs.SetInt("batSelect", 0);
            storeBtnHandlers.lastSelectedBat = System.String.Empty;
            priceText.text = System.String.Empty;
            cart.SetActive(false);
            PlayerPrefs.SetInt("batSelect", 0);
            //startBtn.SetActive (true);
            dressPlayer.nextItem.SetActive(true);
            buyItem.SetActive(false);
            BackButton.SetActive(true);
            //nextImageBG.sprite= nextImage;
            closePurchase.SetActive(false);
            backBtnImage.SetActive(true);
            axePanelPurchasing = false;
            batDamage.fillAmount = 0.25f;
            batrange.fillAmount = 0.3f;
        }
    }
    public void axeClicked()
    {
        if (!insufficientPanelActive)
        {
            //print (currentBtn.currentSelectedGameObject.name);
            selectedObjectName = "axe";
            batDamage.fillAmount = 0.95f;
            batrange.fillAmount = 0.9f;
            if (PlayerPrefs.GetString("axePurchased").Equals("true"))
            {
                storeBtnHandlers.lastSelectedBat = System.String.Empty;
                priceText.text = System.String.Empty;
                cart.SetActive(false);
                PlayerPrefs.SetInt("batSelect", 1);
                //startBtn.SetActive (true);
                dressPlayer.nextItem.SetActive(true);
                buyItem.SetActive(false);
                BackButton.SetActive(true);
                closePurchase.SetActive(false);
                backBtnImage.SetActive(true);
                axePanelPurchasing = false;
                //	nextImageBG.sprite= nextImage;
            }
            else
            {
                priceText.text = "10,000";
                storeBtnHandlers.lastSelectedBat = "10000";
                cart.SetActive(true);
                //startBtn.SetActive (false);
                dressPlayer.nextItem.SetActive(false);
                buyItem.SetActive(true);
                BackButton.SetActive(false);
                closePurchase.SetActive(true);
                backBtnImage.SetActive(false);
                axePanelPurchasing = true;
                //nextImageBG.sprite= buyImage;	
            }
        }
    }


    public bool axePanelPurchasing, gunPanelPurchasing;
    public void cancelaxePanelPurchasing()
    {
        closePurchase.SetActive(false);
        backBtnImage.SetActive(true);
        axePanelPurchasing = false;
        freeBatClicked();
        NextButton.SetActive(false);
        BackButton.SetActive(false);
    }

    public void BatClicked(string name)
    {
        if (!insufficientPanelActive)
        {
            priceSubstring = name.Substring(name.LastIndexOf(',') + 1);
            name = name.Substring(0, name.IndexOf(","));
            //			print(priceSubstring+ " bat: "+ name);
            selectedObjectName = name;

            //	print (currentBtn.currentSelectedGameObject.name);
            selectedObjectName = name;
            if (PlayerPrefs.GetString(name + "Purchased").Equals("true"))
            {
                storeBtnHandlers.lastSelectedBat = System.String.Empty + priceSubstring;
                priceText.text = System.String.Empty;
                cart.SetActive(false);
                PlayerPrefs.SetInt("batSelect", 2);
                PlayerPrefs.SetString("batColor", name);
                //nextImageBG.sprite= nextImage;
                //startBtn.SetActive (true);
                dressPlayer.nextItem.SetActive(true);
                buyItem.SetActive(false);
                BackButton.SetActive(true);
                closePurchase.SetActive(false);
                backBtnImage.SetActive(true);
                axePanelPurchasing = false;
            }
            else
            {
                storeBtnHandlers.lastSelectedBat = System.String.Empty + priceSubstring;

                priceText.text = System.String.Empty + priceSubstring;
                cart.SetActive(true);
                //startBtn.SetActive (false);
                dressPlayer.nextItem.SetActive(false);
                buyItem.SetActive(true);
                BackButton.SetActive(false);
                closePurchase.SetActive(true);
                backBtnImage.SetActive(false);
                axePanelPurchasing = true;
                //	nextImageBG.sprite= buyImage;	
            }


            switch (name)
            {
                case "yellowBat":
                    batDamage.fillAmount = 0.45f;
                    batrange.fillAmount = 0.4f;
                    break;
                case "redBat":
                    batDamage.fillAmount = 0.6f;
                    batrange.fillAmount = 0.55f;
                    break;

                case "blueBat":
                    batDamage.fillAmount = 0.75f;
                    batrange.fillAmount = 0.7f;
                    break;




            }
        }
    }



    public void cancelgunPanelpurchasing()
    {
        closePurchase.SetActive(false);
        backBtnImage.SetActive(true);
        pistolClicked();
        gunPanelPurchasing = false;
        NextButton.SetActive(false);
        BackButton.SetActive(false);
    }
    public void pistolClicked()
    {
        if (!insufficientPanelActive)
        {
            //	print("preference: "+PlayerPrefs.GetString ("pistolPurchased"));
            selectedObjectName = "pistol";
            storeBtnHandlers.lastSelectedPistol = System.String.Empty;
            priceText.text = System.String.Empty;
            cart.SetActive(false);
            PlayerPrefs.SetInt("pistolSelect", 0);
            //startBtn.SetActive (true);
            dressPlayer.nextItem.SetActive(true);
            buyItem.SetActive(false);
            BackButton.SetActive(true);
            //	nextImageBG.sprite= nextImage;
            closePurchase.SetActive(false);
            backBtnImage.SetActive(true);
            gunPanelPurchasing = false;
            damage.fillAmount = 0.4f;
            range.fillAmount = 0.3f;
        }
    }
    public void shotGunClicked()
    {
        if (!insufficientPanelActive)
        {
            selectedObjectName = "shotgun";
            damage.fillAmount = 0.6f;
            range.fillAmount = 0.5f;

            if (PlayerPrefs.GetString("shotgunPurchased").Equals("true"))
            {
                storeBtnHandlers.lastSelectedPistol = System.String.Empty;
                priceText.text = System.String.Empty;
                cart.SetActive(false);
                PlayerPrefs.SetInt("pistolSelect", 1);
                //startBtn.SetActive (true);
                dressPlayer.nextItem.SetActive(true);
                buyItem.SetActive(false);
                BackButton.SetActive(true);
                //nextImageBG.sprite = nextImage;
                closePurchase.SetActive(false);
                backBtnImage.SetActive(true);
                gunPanelPurchasing = false;
            }
            else
            {
                priceText.text = "50,000";
                storeBtnHandlers.lastSelectedPistol = "50,000";
                cart.SetActive(true);
                //startBtn.SetActive (false);
                buyItem.SetActive(true);
                BackButton.SetActive(false);
                dressPlayer.nextItem.SetActive(false);
                //nextImageBG.sprite= buyImage;	
                closePurchase.SetActive(true);
                backBtnImage.SetActive(false);
                gunPanelPurchasing = true;
            }
            //barColor (batBarImage);
        }
    }

    public void missileClicked()
    {
        if (!insufficientPanelActive)
        {
            selectedObjectName = "missile";
            damage.fillAmount = 0.75f;
            range.fillAmount = 0.8f;

            if (PlayerPrefs.GetString("missilePurchased").Equals("true"))
            {
                storeBtnHandlers.lastSelectedPistol = System.String.Empty;
                priceText.text = System.String.Empty;
                cart.SetActive(false);
                PlayerPrefs.SetInt("pistolSelect", 2);
                //startBtn.SetActive (true);
                dressPlayer.nextItem.SetActive(true);
                buyItem.SetActive(false);
                BackButton.SetActive(true);
                //nextImageBG.sprite = nextImage;
                closePurchase.SetActive(false);
                backBtnImage.SetActive(true);
                gunPanelPurchasing = false;
            }
            else
            {
                priceText.text = "70,000";
                storeBtnHandlers.lastSelectedPistol = "70,000";
                cart.SetActive(true);
                //startBtn.SetActive (false);
                dressPlayer.nextItem.SetActive(false);
                buyItem.SetActive(true);
                BackButton.SetActive(false);
                //nextImageBG.sprite= buyImage;	
                closePurchase.SetActive(true);
                backBtnImage.SetActive(false);
                gunPanelPurchasing = true;
            }
            //barColor (batBarImage);
        }
    }


    public void barColor(Image[] parentBar)
    {
        if (!insufficientPanelActive)
        {
            //		print(currentBtn.currentSelectedGameObject.name);
            foreach (Image childs in parentBar)
            {

                childs.color = white;

            }
            //currentBtn.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>().color = green;
        }
    }



    string price1;
    public AudioClip purchaseSound;
    public void bikeBuyBtn()
    {
        if (!insufficientPanelActive)
        {
            CancelBtn.SetActive(false);
            price1 = priceText.text + System.String.Empty;
            price1 = price1.Replace("$", string.Empty);
            price1 = price1.Replace(",", string.Empty);
            price1 = price1.Trim();
            //		print (selectedObjectName+ "   "+currentBtn.currentSelectedGameObject.name);


            if (System.Int32.Parse(price1) <= PlayerPrefs.GetInt("cash"))
            {
                if (PlayerPrefs.GetInt("SoundOff") == 0)
                {
                    GetComponent<AudioSource>().PlayOneShot(purchaseSound);
                }
                updatedCash = int.Parse(cash.text) - int.Parse(price1);
                PlayerPrefs.SetInt("cash", updatedCash);
                PlayerPrefs.Save();
                cash.text = updatedCash + System.String.Empty;
                //				print (selectedObjectName);
                if (selectedObjectName.Contains("shotgun"))
                {
                    PlayerPrefs.SetInt("pistolSelect", 1);
                    PlayerPrefs.SetString("shotgunUnlocked", "true");
                }
                else if (selectedObjectName.Contains("missile"))
                {
                    PlayerPrefs.SetInt("pistolSelect", 2);
                }
                if (selectedObjectName.Contains("axe"))
                {
                    PlayerPrefs.SetInt("batSelect", 1);
                }
                else if (selectedObjectName.Contains("Bat"))
                {
                    PlayerPrefs.SetInt("batSelect", 2);
                    PlayerPrefs.SetString("batColor", selectedObjectName);
                }
                PlayerPrefs.SetString(selectedObjectName + "Purchased", "true");
                PlayerPrefs.Save();
                priceText.text = System.String.Empty;
                cart.SetActive(false);
                //				print (selectedObjectName+ "   "+selectedObjectName);
                if (selectedObjectName.Contains("shirt"))
                {
                    PlayerPrefs.SetInt("ownBikeCostume", (PlayerPrefs.GetInt("ownBikeCostume") + 1));
                    PlayerPrefs.SetString("shirtColor", selectedObjectName);
                    dressPlayer.dressCode();
                }
                if (!selectedObjectName.Contains("Bat") && (selectedObjectName.Contains("yellow") || selectedObjectName.Contains("red") || selectedObjectName.Contains("black") || selectedObjectName.Contains("blue") || selectedObjectName.Contains("orange") || selectedObjectName.Contains("grey")))
                {
                    //print ("inside Bat");
                    PlayerPrefs.SetInt("ownBikeCostume", (PlayerPrefs.GetInt("ownBikeCostume") + 1));


                    /*if(selectedObjectName.Contains("grey"))
                    {
                        Common.changeGameDictionary("player1bikecolor1locked", "no");
                        Common.changeGameDictionary("player2bikecolor1locked", "no");

                    }
                    if (selectedObjectName.Contains("blue"))
                    {
                        Common.changeGameDictionary("player1bikecolor2locked", "no");
                        Common.changeGameDictionary("player2bikecolor2locked", "no");

                    }
                    if (selectedObjectName.Contains("red"))
                    {
                        Common.changeGameDictionary("player1bikecolor3locked", "no");
                        Common.changeGameDictionary("player2bikecolor3locked", "no");

                    }*/
                    PlayerPrefs.SetString("bikeColor", selectedObjectName);
                    dressPlayer.dressCode();
                }
                //startBtn.SetActive (true);
                dressPlayer.nextItem.SetActive(true);
                buyItem.SetActive(false);
                BackButton.SetActive(true);
                resetTexture();
                cash1 = cash.text + System.String.Empty;
                cash1 = cash1.Trim();
                cash2 = System.Int32.Parse(cash1);
                startAnimatingCash = true;
            }
            else
            {

                insufficientCash.SetActive(true);
                insufficientPanelActive = true;
            }
        }
    }

    public void noInsufficientPanel()
    {
        insufficientCash.SetActive(false);
        insufficientPanelActive = false;
    }

    public void YesInsufficientPanel()
    {
        insufficientCash.SetActive(false);
        insufficientPanelActive = false;
        dressPlayer.cashBundleFtn();
    }
}
