using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class preferenceAndTexture : MonoBehaviour
{

    public Color orange, green;

    public int LevelNum;
    //public string preferenceName;
    string levelNumber;
    // Use this for initialization
    void Start()
    {
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



        //		print (levelNumber+ "  "+ transform.name);



        levelNumber = this.transform.name;


        if (Common.getGameDictionaryData(this.transform.name + "locked").Equals("no"))
        {
            //transform.Find("Lock").gameObject.SetActive(false); //Added//
            transform.Find("Ring").gameObject.SetActive(true);


            if (Common.getGameDictionaryData("level" + (LevelNum+1) + "locked").Equals("yes"))
                //transform.Find ("Ring").GetComponent<Renderer>().material.SetColor("_Color", orange);
                transform.Find("Ring").GetComponent<Image>().color = orange;
            else if (Common.getGameDictionaryData(levelNumber + "locked").Equals("no"))
                transform.Find("Ring").GetComponent<Image>().color = green;
            //transform.Find ("Ring").GetComponent<Renderer>().material.SetColor("_Color", green);

        }
        else
        {
            //transform.Find("Lock").gameObject.SetActive(true); //Added//
            transform.Find("Ring").gameObject.SetActive(false);
        }



    }



}
