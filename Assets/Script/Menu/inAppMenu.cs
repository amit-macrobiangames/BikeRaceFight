using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class inAppMenu : MonoBehaviour
{

    //	public Transform player;
    public static bool once = true;
    string currentObjectName;
    string getObject;
    int noOfItemPurchased;
    // Use this for initialization

    public static bool firstTime = true;
    const string SKU = "sku";

    private string Pid;
    public string PublicKey;
    //Inventory _inventory = null;

   // public MyIAPManager unityIAPscript;

    //	private void OnEnable()
    //	{
    //		// Listen to all events for illustration purposes
    //
    //		OpenIABEventManager.purchaseSucceededEvent += purchaseSucceededEvent;
    //		OpenIABEventManager.purchaseFailedEvent += purchaseFailedEvent;
    //	
    //	}
    //	private void OnDisable()
    //	{
    //		// Remove all event handlers
    //
    //		OpenIABEventManager.purchaseSucceededEvent -= purchaseSucceededEvent;
    //		OpenIABEventManager.purchaseFailedEvent -= purchaseFailedEvent;
    //
    //	}
    //	

    void Start()
    {
        if (firstTime)
            DontDestroyOnLoad(this.gameObject);
        else
            Destroy(this.gameObject);
        firstTime = false;

      //  unityIAPscript = GameObject.Find("unity IAP").GetComponent<MyIAPManager>();


        //		if (once) {
        //						OpenIAB.mapSku (SKU, OpenIAB_Android.STORE_GOOGLE, "sku");
        //					
        //
        //				var options = new Options ();
        //						options.checkInventoryTimeoutMs = Options.INVENTORY_CHECK_TIMEOUT_MS * 2;
        //						options.discoveryTimeoutMs = Options.DISCOVER_TIMEOUT_MS * 2;
        //						options.checkInventory = false;
        //						options.verifyMode = OptionsVerifyMode.VERIFY_SKIP;
        //						options.prefferedStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
        //						options.availableStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
        //						options.storeKeys = new Dictionary<string, string> { {OpenIAB_Android.STORE_GOOGLE, PublicKey} };
        //						options.storeSearchStrategy = SearchStrategy.INSTALLER_THEN_BEST_FIT;
        //		
        //						// Transmit options and start the service
        //						OpenIAB.init (options);
        //		
        //		
        //						DontDestroyOnLoad (this);
        //			once=false;
        //				}
    }


    //	private void purchaseSucceededEvent(Purchase purchase)
    //	{
    //
    //		Debug.Log("purchase SucceededEvent: " + purchase);
    //		verify ();
    //		query ();
    //		
    //	}
    //	private void purchaseFailedEvent(int errorCode, string errorMessage)
    //	{
    //		Debug.Log("purchaseFailedEvent: " + errorMessage);
    //
    //
    //		
    //	}
    //
    //	public void query()
    //	{
    //		OpenIAB.queryInventory(new string[] { SKU });
    //	}

    //	void Start () {
    ////		INABManager.initialize ("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAtcHJJ/hIQUrbSEmrR6Md+6NT5kYo2YaiP/qzUJ0qEDq4X6xVV6L9XkCM55blgD8EFdpw13S1zH36kDHNnQApAF0dgSkfGaIlgJUQMggotFgqGUr0oBkiquZeiO4Nrsdwv77kZv3deFlT0NlVcWo85qZfOfKMBOmZqbPDG5UTLZfWhBnYfUwjSX20zJbHblDsuyRlh0oHaSmTzBLxK4uPVDlTy6i/AiXaF02H+wo5B44PhaLGIOcsIJKm4sWk2AAtSquyxxuQ8vQNLu8dXw57vXPL+srH3pyeei/3z6/4B5DmALw6VQjJLHVmGaIolUiJORyjTjfBRYrrC6YF9fu8pQIDAQAB");
    //
    //	
    //
    //		//AppTrackerAndroid.startSession("TOpLZPt3Yy96mVfKPtYKVXzdBxXJU4Co");
    //	}

    void Update()
    {
        if (PlayerPrefs.GetString("givePurchaseReward").Equals("true"))
        {
            PlayerPrefs.SetString("givePurchaseReward", "false");
            PlayerPrefs.Save();
            verify();
        }
    }

    void verify()
    {
        getObject = PlayerPrefs.GetString("currentlyPurchasedItem");
        getObject = Regex.Match(getObject, @"\d+").Value;
        noOfItemPurchased = System.Int32.Parse(getObject);
        //Debug.Log(getObject + "   noOfItemPurchased :" + noOfItemPurchased);
        //print ("purchase verify function: "+ PlayerPrefs.GetString("currentlyPurchasedItem"));
        if (PlayerPrefs.GetString("currentlyPurchasedItem").Contains("cash"))
        {
            if (noOfItemPurchased == 100)
            {
                PlayerPrefs.SetString("bundlePurchase", "true");

                if (SceneManager.GetActiveScene().name.Contains("mainMenu3"))
                {
                    HideBanner();
                }
            }
            noOfItemPurchased *= 1000;
            PlayerPrefs.SetInt("cash", (PlayerPrefs.GetInt("cash") + noOfItemPurchased));
            PlayerPrefs.Save();
        }

        else if (PlayerPrefs.GetString("currentlyPurchasedItem").Contains("helmet"))
        {
            if (noOfItemPurchased > 3)
            {
                PlayerPrefs.SetString("bundlePurchase", "true");
                if (SceneManager.GetActiveScene().name.Contains("mainMenu3"))
                {
                    HideBanner();
                }
            }
            if (noOfItemPurchased == 3)
            {
                noOfItemPurchased = 10;
            }
            else if (noOfItemPurchased == 6)
            {
                noOfItemPurchased = 20;
            }
            else if (noOfItemPurchased == 12)
            {
                noOfItemPurchased = 30;
            }
            //print ("before helmets purchased: "+PlayerPrefs.GetInt("helmets"));
            PlayerPrefs.SetInt("helmets", (PlayerPrefs.GetInt("helmets") + noOfItemPurchased));
            PlayerPrefs.Save();
            //	print ("before helmets purchased: "+PlayerPrefs.GetInt("helmets"));
            if (SceneManager.GetActiveScene().name.Contains("desert"))
            {
                //PlayerPrefs.SetInt("helmets", (PlayerPrefs.GetInt("helmets")-1));
                //PlayerPrefs.Save();
                GameObject.FindGameObjectWithTag("Player").transform.root.GetComponent<revivePlayer>().reStartPlayer();
            }
        }

        else if (PlayerPrefs.GetString("currentlyPurchasedItem").Contains("shield"))
        {
            if (noOfItemPurchased > 3)
            {
                PlayerPrefs.SetString("bundlePurchase", "true");
                if (SceneManager.GetActiveScene().name.Contains("mainMenu3"))
                {
                    HideBanner();
                }
            }
            if (noOfItemPurchased == 3)
            {
                noOfItemPurchased = 4;
            }

            PlayerPrefs.SetInt("shields", (PlayerPrefs.GetInt("shields") + noOfItemPurchased));
            PlayerPrefs.Save();
            if (SceneManager.GetActiveScene().name.Contains("desert"))
            {
                GameObject.FindGameObjectWithTag("Player").transform.root.GetComponent<weaponAI>().closeBundle();
            }
        }
        else if (PlayerPrefs.GetString("currentlyPurchasedItem").Contains("time"))
        {
            if (noOfItemPurchased > 6)
            {
                PlayerPrefs.SetString("bundlePurchase", "true");
                if (SceneManager.GetActiveScene().name.Contains("mainMenu3"))
                {
                    HideBanner();
                }
            }
            if (noOfItemPurchased == 6)
            {
                noOfItemPurchased = 8;
            }
            //print ("before timers purchased: "+PlayerPrefs.GetInt("timers"));
            PlayerPrefs.SetInt("timers", (PlayerPrefs.GetInt("timers") + noOfItemPurchased));
            PlayerPrefs.Save();
            //print ("after timers purchased: "+PlayerPrefs.GetInt("timers"));
            if (SceneManager.GetActiveScene().name.Contains("desert"))
            {
                timerCollection();
                GameObject.FindGameObjectWithTag("MainCamera").transform.root.GetComponent<PhycamViews>().closeTimerBundle();
            }
        }

        else if (PlayerPrefs.GetString("currentlyPurchasedItem").Contains("boost"))
        {
            if (noOfItemPurchased > 5)
            {
                PlayerPrefs.SetString("bundlePurchase", "true");
                if (SceneManager.GetActiveScene().name.Contains("mainMenu3"))
                {
                    HideBanner();
                }
            }
            //print ("before boost purchased: "+PlayerPrefs.GetInt("boosts"));

            PlayerPrefs.SetInt("boosts", (PlayerPrefs.GetInt("boosts") + noOfItemPurchased));
            PlayerPrefs.Save();
            //print ("after boost purchased: "+PlayerPrefs.GetInt("boosts"));
            if (SceneManager.GetActiveScene().name.Contains("desert"))
            {
                boostCollection();
                GameObject.FindGameObjectWithTag("Player").transform.root.GetComponent<weaponAI>().closeBundle();
            }
        }

        else if (PlayerPrefs.GetString("currentlyPurchasedItem").Contains("bullets"))
        {
            if (noOfItemPurchased > 6)
            {
                PlayerPrefs.SetString("bundlePurchase", "true");
                if (SceneManager.GetActiveScene().name.Contains("mainMenu3"))
                {
                    HideBanner();
                }
            }


            if (noOfItemPurchased == 6)
            {
                noOfItemPurchased = 20;
            }
            else if (noOfItemPurchased == 12)
            {
                noOfItemPurchased = 40;
            }
            else if (noOfItemPurchased == 24)
            {
                noOfItemPurchased = 80;
            }

            PlayerPrefs.SetInt("ammos", (PlayerPrefs.GetInt("ammos") + noOfItemPurchased));
            PlayerPrefs.Save();
            if (SceneManager.GetActiveScene().name.Contains("desert"))
            {

                GameObject.FindGameObjectWithTag("Player").transform.root.GetComponent<weaponAI>().closeBundle();

            }
        }

        else if (PlayerPrefs.GetString("currentlyPurchasedItem").Contains("missile"))
        {
            if (noOfItemPurchased >= 10)
            {
                PlayerPrefs.SetString("bundlePurchase", "true");
                if (SceneManager.GetActiveScene().name.Contains("mainMenu3"))
                {
                    HideBanner();
                }
            }

            if (noOfItemPurchased == 4)
            {
                noOfItemPurchased = 10;
            }
            else if (noOfItemPurchased == 10)
            {
                noOfItemPurchased = 20;
            }
            else if (noOfItemPurchased == 20)
            {
                noOfItemPurchased = 30;
            }

            PlayerPrefs.SetInt("missile", (PlayerPrefs.GetInt("missile") + noOfItemPurchased));
            PlayerPrefs.Save();
            if (SceneManager.GetActiveScene().name.Contains("desert"))
            {

                GameObject.FindGameObjectWithTag("Player").transform.root.GetComponent<weaponAI>().closeBundle();

            }
        }


        else if (PlayerPrefs.GetString("currentlyPurchasedItem").Contains("ticket"))
        {
            if (noOfItemPurchased > 15)
            {
                PlayerPrefs.SetString("bundlePurchase", "true");
                if (SceneManager.GetActiveScene().name.Contains("mainMenu3"))
                {
                    HideBanner();
                }
            }
            PlayerPrefs.SetInt("Tickets", (PlayerPrefs.GetInt("Tickets") + noOfItemPurchased));
            PlayerPrefs.Save();

        }

        //PlayerPrefs.SetString ("bundlePurchase","true");
        //				unlockAllBikes();
        //				PlayerPrefs.Save ();


        //print ("item purchased: "+getObject+ " \n No. Of item Purchased: "+ noOfItemPurchased);


    }


    public void InappFtn(string objectName)
    {


        currentObjectName = objectName;
        //print (currentObjectName);
        PlayerPrefs.SetString("currentlyPurchasedItem", currentObjectName);
        PlayerPrefs.Save();
        //print ("before boost purchased: "+PlayerPrefs.GetInt("boosts"));
        //currentObjectName="android.test.purchased";
        Pid = currentObjectName;

       // unityIAPscript.PurchaseButtonClick(currentObjectName);


    }

    void boostCollection()
    {
        weaponAI.startedValue = PlayerPrefs.GetInt("boosts");
        weaponAI.updatedValue = 0;
        GameObject.FindGameObjectWithTag("Player").transform.root.GetComponent<weaponAI>().boostFG.fillAmount = 1;
    }

    void timerCollection()
    {
        PhycamViews.startedValue = PlayerPrefs.GetInt("timers");
        PhycamViews.updatedValue = 0;
        GameObject.FindGameObjectWithTag("Player").transform.root.GetComponent<heavyBikeTurns>().mainCamera.timerFG.fillAmount = 1;
    }

    void HideBanner()
    {
#if !UNITY_EDITOR
	//Ads_Manager.Instance.HideAdmobBanner();
#endif
    }
}
