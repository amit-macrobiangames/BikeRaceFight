using UnityEngine;
//#if !UNITY_EDITOR

//#endif

using UnityEngine.UI;
public class playservices : MonoBehaviour
{
    public Text userName;
    public GameObject signInBtn, signOutBtn;
    private string leaderboard = "CgkI18y_lYISEAIQEg";
    static bool once = true;
    static bool setPosition = false;

    // do not initialize play game services on start
/*         void Awake()
        {
            PlayGamesPlatform.Activate();
            if (once)
            {
                if (PlayGamesPlatform.Instance.IsAuthenticated())
                {
                    userName.text = Social.localUser.userName;
                    signOutBtn.SetActive(true);
                    signInBtn.SetActive(false);
                    SetPopUp();
                }
                else
                {
                    Social.localUser.Authenticate((bool success) =>
                    {
                        if (success)
                        {
                            userName.text = Social.localUser.userName;
                            signOutBtn.SetActive(true);
                            signInBtn.SetActive(false);
                            SetPopUp();
                        }
                        else
                        {
                            userName.text = "";
                            signOutBtn.SetActive(false);
                            signInBtn.SetActive(true);
                        }
                    });
                }
                once = false;
            }
        } */
    void SetPopUp()
    {
        if (!setPosition)
        {
            setPosition = true;
            //(PlayGamesPlatform.Instance).SetGravityForPopups(GooglePlayGames.BasicApi.Gravity.TOP | GooglePlayGames.BasicApi.Gravity.RIGHT); Add

        }
    }

    public void signInFtn()
    {
        Invoke("SetinitializeLogin", 2);
        //PlayGamesPlatform.Activate(); Add
        Invoke("Sign", 0.1f);


    }

    void Sign()
    {
        Social.localUser.Authenticate((bool success) =>
                                      {
                                          if (success)
                                          {
                                              //Debug.Log ("You've successfully logged in");
                                              userName.text = Social.localUser.userName;
                                              signOutBtn.SetActive(true);
                                              signInBtn.SetActive(false);

                                              SetPopUp();
                                          }
                                          else
                                          {
                                              Debug.Log("Login failed for some reason");
                                              userName.text = "";
                                              signOutBtn.SetActive(false);
                                              signInBtn.SetActive(true);
                                          }
                                      });
    }

    public void signOutFtn()
    {

        //((PlayGamesPlatform)Social.Active).SignOut(); Add

        Debug.Log("Loged out");
        userName.text = "";
        signOutBtn.SetActive(false);
        signInBtn.SetActive(true);

    }


    public void showLeaderboardFtn()
    {
        //if (PlayGamesPlatform.Instance.IsAuthenticated()) Add
        //    Social.ShowLeaderboardUI();
        //else
        //{

        //    signInFtn();
        //    Invoke("Show_LeaderBoard", 1);
        //}
    }
    void Show_LeaderBoard()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Social.ShowLeaderboardUI();
            }
        });
    }
    public void showAchievementftn()
    { 
        //if (PlayGamesPlatform.Instance.IsAuthenticated()) Add
        //    Social.ShowAchievementsUI();
        //else
        //{
        //    signInFtn();
        //    Invoke("Show_Achieve", 1);

        //}
    }
    void Show_Achieve()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Social.ShowAchievementsUI();

            }
        });
    }
}
