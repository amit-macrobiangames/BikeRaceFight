using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class levelScript : MonoBehaviour
{
    //New Add
    public int CurrentLevel;

    public string[] levelObjs;
    public string[] levelNames;
    public string[] levelDistance;

    public Text levelObjText, levelNameText, levelDistText;//, levelRewradText;
    public Button playBtn;
    int levelNumber;
    private AsyncOperation asyncvar = null;
    public Text Loadingtext;
    public Image selectedImg;
    public GameObject LevelLoading, warningBox, loadingSimple;
    bool playClickBool;
    public Image panelParent;
    void Start()
    {
        playClickBool = false;
    }
    void OnEnable()
    {
        //if (PlayerPrefs.GetString("level1Unlocked") != "true")
        //{
        //    PlayerPrefs.SetString("level1Unlocked", "true");
        //}
        if(Common.getGameDictionaryData("level1locked").Equals("yes"))
        {
            Common.changeGameDictionary("level1locked","no");
        }
        Debug.Log("level number: "+levelNumber);

        if(levelNumber == 0)
        {
            levelNumber = 1;
        }
        levelSelection(levelNumber);
        if (levelNumber <= 2)
        {
            panelParent.rectTransform.anchoredPosition = new Vector2(0, 0);
        }
        else if (levelNumber > 2 && levelNumber < 5)
        {
            panelParent.rectTransform.anchoredPosition = new Vector2(-490, 0);
        }
        else if (levelNumber >= 5 && levelNumber < 8)
        {
            panelParent.rectTransform.anchoredPosition = new Vector2(-1431, 0);
        }
        else if (levelNumber >= 8 && levelNumber < 11)
        {
            panelParent.rectTransform.anchoredPosition = new Vector2(-2490, 0);
        }
        else if (levelNumber >= 11 && levelNumber < 13)
        {
            panelParent.rectTransform.anchoredPosition = new Vector2(-3521, 0);
        }
        else if (levelNumber >= 13 && levelNumber < 16)
        {
            panelParent.rectTransform.anchoredPosition = new Vector2(-4457, 0);
        }
        else if(levelNumber >= 16 && levelNumber < 18)
        {
            panelParent.rectTransform.anchoredPosition = new Vector2(-5506, 0);
        }
        else if(levelNumber >=18 && levelNumber <= 20)
        {
            panelParent.rectTransform.anchoredPosition = new Vector2(-6762, 0);
        }
    }
    public void levelSelection(int level)
    {

            levelNumber = level;
            PlayerPrefs.SetInt("levels", level);
            PlayerPrefs.Save();
            levelObjText.text = levelObjs[(level - 1)];
            levelNameText.text = levelNames[(level - 1)];
            levelDistText.text = levelDistance[(level - 1)];
            //levelRewradText.text = (level * 1000) + System.String.Empty;
            selectedImg.transform.parent = GameObject.Find(("level" + (levelNumber))).transform;
            selectedImg.rectTransform.anchoredPosition = new Vector2(0, 0);


        if (Common.getGameDictionaryData("level" + level + "locked").Equals("no"))
        {
            playBtn.interactable = true;
        }
        else
        {
            playBtn.interactable = false;
        }

    }
    public void LetsGo()
    {
        PlayClicked();
        //if (!PlayerPrefs.GetString("missilePurchased").Equals("true") && levelNumber.Equals(8))
        //{
        //    warningBox.SetActive(true);
        //}
        //else
        //{
        //    PlayClicked();
        //}
    }

    public void PlayClicked()
    {
        if (!playClickBool)
        {
            string levelStatus = "level" + levelNumber + "Unlocked";
            //print (levelStatus + "   " + PlayerPrefs.GetString (levelStatus));
            //if (PlayerPrefs.GetString(levelStatus).Equals("true"))
            //{
                playClickBool = true;
                PlayerPrefs.SetInt("leaderBoardScore", 0);
                PlayerPrefs.SetInt("levels", int.Parse(levelNumber.ToString()));
                PlayerPrefs.Save();
                LevelLoading.SetActive(true);
                this.gameObject.SetActive(false);
                LoadLevel("desert");
                
            // }
        }
    }
    public void BuyMissileLauncher()
    {
        if (PlayerPrefs.GetInt("cash") >= 70000)   //price of rocket
        {
            PlayerPrefs.SetInt("pistolSelect", 2);
            PlayerPrefs.SetString("missilePurchased", "true");
            PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") - 70000);
            PlayerPrefs.Save();
            PlayClicked();
        }
    }
    string levelNam;
    public void LoadLevel(string name)
    {
        //LoadingTexture.SetActive (true);
        levelNam = name;
        Invoke("startLvl", 0.3f);
        //asyncvar =  Application.LoadLevelAsync(name);
        //LoadALevel("desert");
    }
    void startLvl()
    {
        asyncvar = SceneManager.LoadSceneAsync(levelNam);
    }

    private IEnumerator LoadALevel(string levelName)
    {
        asyncvar = SceneManager.LoadSceneAsync(levelName);
        yield return asyncvar;
    }
    // Update is called once per frame  
    void Update()
    {
        if (playClickBool)
        {
            if (asyncvar != null)
            {
                double LoadedPercent = asyncvar.progress * 100;
                LoadedPercent = (int)LoadedPercent;
                Loadingtext.text = "Loading... " + LoadedPercent.ToString() + "%";
                //Loadingtext.rectTransform.anchoredPosition=new Vector2(0,12.4f);
                LoadALevel("desert");
            }
        }
        
    }
}
