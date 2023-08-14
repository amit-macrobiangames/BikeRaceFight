using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class searchInapp : MonoBehaviour
{
    GameObject inappObject;
    inAppMenu inappScript;
    EventSystem currentBtn;
    rewardedVideo rewardedVideoScript;
    private Transform player;
    private bool isGamePlay = false;
    // Use this for initialization
    void Awake()
    {
        //PlayerPrefs.DeleteAll ();
        currentBtn = EventSystem.current;
        inappObject = GameObject.Find("inapp");
        if (inappObject)
        {
            inappScript = inappObject.GetComponent<inAppMenu>();
            rewardedVideoScript = inappObject.GetComponent<rewardedVideo>();
        }
        if (SceneManager.GetActiveScene().name.Contains("desert"))
        {
            isGamePlay = true;
            Invoke("GetPlayer", 1);
        }

    }
    void GetPlayer()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Invoke("GetPlayer", 1);
        }
    }
    string prefName;

    public void purchaseWithCoins(int info)
    {
        switch (currentBtn.currentSelectedGameObject.name)
        {
            case "boost_coin":
                prefName = "boosts";
                break;

            case "helmet_coin":
                prefName = "helmets";
                break;

            case "timer_coin":
                prefName = "timers";
                break;

            case "shield_coin":
                prefName = "shields";
                break;

            case "bullet_coin":
                prefName = "ammos";
                break;
            case "missile_coin":
                prefName = "missile";
                break;
        }

        if (PlayerPrefs.GetInt("cash") >= 500)
        {
            PlayerPrefs.SetInt("cash", (PlayerPrefs.GetInt("cash") - 500));
            PlayerPrefs.Save();
            PlayerPrefs.SetInt(prefName, (PlayerPrefs.GetInt(prefName) + info));

            if (prefName.Equals("helmets"))
            {
                if (isGamePlay.Equals(true))
                {
                    //PlayerPrefs.SetInt("helmets", (PlayerPrefs.GetInt("helmets")-1));
                    //PlayerPrefs.Save();
                    //GameObject.FindGameObjectWithTag("Player").transform.root.GetComponent<revivePlayer>().reStartPlayer();
                    player.root.GetComponent<revivePlayer>().reStartPlayer();
                }
            }
            else if (prefName.Equals("boosts"))
            {
                if (isGamePlay.Equals(true))
                {
                    player.root.GetComponent<weaponAI>().boostFG.fillAmount = 1f;
                    player.root.GetComponent<weaponAI>().closeBundle();
                }
            }
            else if (prefName.Equals("timers"))
            {
                if (isGamePlay.Equals(true))
                {
                    PhycamViews.instance.timerFG.fillAmount = 1;
                    PhycamViews.instance.closeTimerBundle();
                }
            }
            else
            {
                if (isGamePlay.Equals(true))
                {
                    player.root.GetComponent<weaponAI>().closeBundle();
                }
            }

        }

    }

    public void ftn()
    {
        inappScript.InappFtn(currentBtn.currentSelectedGameObject.name);
    }


    public void mediationVideoFtn()
    {
        rewardedVideoScript.rewardedVideoFtn(currentBtn.currentSelectedGameObject.name);
    }
}
