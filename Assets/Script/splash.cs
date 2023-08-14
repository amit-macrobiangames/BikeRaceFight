using UnityEngine;
using System.Collections;
using UnityEngine.UI;


using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;
public class splash : MonoBehaviour
{
    public Image LoadingBar;
    public GameObject percentBG, ads_manager;
    bool loadingbar;

    private AsyncOperation async = null; // When assigned, load is in progress.
    float xPosition;


    // Use this for initialization
    void Start()
    {
/* #if UNITY_EDITOR
        Debug.LogError("cash");
#endif
        
            //PlayerPrefs.SetInt("cash", 900000);
        for (int i = 1; i < 17; i++)
            PlayerPrefs.SetString("level" + i + "Unlocked", "true"); */
        Time.timeScale = 1;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //Invoke ("LoadMenu",3.5f);
        xPosition = percentBG.transform.localPosition.x;
        Invoke("EnableaAds_manager", 0.2f);
        //Invoke ("LoadMenu",0.2f);
        //LoadMenu();
    }

    void LoadMenu()
    {
        async = SceneManager.LoadSceneAsync("mainMenu3");
        loadingbar = true;
    }
    void EnableaAds_manager()
    {
        Invoke("LoadMenu", 2f);
        if (ads_manager)
            ads_manager.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {


        if (async != null)
        {

            LoadingBar.fillAmount = async.progress;
            if (async.progress >= 0.89f)
                LoadingBar.fillAmount = 0.97f;
            percentBG.transform.localPosition = new Vector3(xPosition + (async.progress * 750), percentBG.transform.localPosition.y, percentBG.transform.localPosition.z);

        }

        if (loadingbar)
            LoadALevel("mainMenu3");

    }

    private IEnumerator LoadALevel(string levelName)
    {
        async = SceneManager.LoadSceneAsync("levelName");
        yield return async;
    }
}
