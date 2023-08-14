using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class preferenceAndTexture : MonoBehaviour
{

    public Color orange, green;

    //public string preferenceName;
    string levelUnlocked, preferenceName;
    string levelNumber;
    int levelNumberINT;
    // Use this for initialization
    void Start()
    {
        if (!PlayerPrefs.HasKey("level1Unlocked"))
            PlayerPrefs.SetString("level1Unlocked", "true");

        //		PlayerPrefs.SetString ("level16Unlocked","true");
        //		PlayerPrefs.SetString ("level15Unlocked","true");
        //		PlayerPrefs.SetString("level13Unlocked","true");
        //		PlayerPrefs.SetString("level14Unlocked","true");

        //		PlayerPrefs.SetString("level2Unlocked","true");
        //
        //		PlayerPrefs.SetString("level3Unlocked","true");
        //		PlayerPrefs.SetString ("level4Unlocked","true");
        //		PlayerPrefs.SetString("level5Unlocked","true");
        //		PlayerPrefs.SetString("level6Unlocked","true");
        //		PlayerPrefs.SetString("level7Unlocked","true");
        //		PlayerPrefs.SetString("level8Unlocked","true");
        //		PlayerPrefs.SetString("level9Unlocked","true");
        //		PlayerPrefs.SetString("level10Unlocked","true");
        //
        //		PlayerPrefs.SetString("level11Unlocked","true");
        //		PlayerPrefs.SetString("level12Unlocked","true");
        //
        //				PlayerPrefs.SetString("level15Unlocked","true");
        //				PlayerPrefs.SetString("level16Unlocked","true");


        levelNumber = (transform.name.ToString()).Remove(0, 5);
        levelNumberINT = System.Int32.Parse(levelNumber);
        //		print (levelNumber+ "  "+ transform.name);
        preferenceName = transform.name + "Unlocked";
        levelUnlocked = PlayerPrefs.GetString(preferenceName);




        if (levelUnlocked.Equals("true"))
        {
            transform.Find("Lock").gameObject.SetActive(false);
            transform.Find("Ring").gameObject.SetActive(true);

            if (PlayerPrefs.GetString("level" + (levelNumberINT + 1) + "Unlocked").Equals("true"))
                transform.Find("Ring").GetComponent<Image>().color = green;
            //transform.Find ("Ring").GetComponent<Renderer>().material.SetColor("_Color", green);
            else if (!PlayerPrefs.GetString("level" + (levelNumberINT + 1) + "Unlocked").Equals("true"))
                //transform.Find ("Ring").GetComponent<Renderer>().material.SetColor("_Color", orange);
                transform.Find("Ring").GetComponent<Image>().color = orange;

        }
        else
        {
            transform.Find("Lock").gameObject.SetActive(true);
            transform.Find("Ring").gameObject.SetActive(false);
        }



    }



}
