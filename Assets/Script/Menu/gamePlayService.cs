using UnityEngine;
//#if !UNITY_EDITOR

//#endif
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class gamePlayService : MonoBehaviour
{
    private string leaderboard = "CgkI18y_lYISEAIQEg";
    //achievement strings
    private string achievement = "CgkInJSM9PoTEAIQAg";

    // Use this for initialization

    public void postLeaderboardScoreFtn()
    {
        try
        {
            //if (PlayGamesPlatform.Instance.IsAuthenticated()) Add
            //{
            //    Social.ReportScore(PlayerPrefs.GetInt("leaderBoardScore"), leaderboard, (bool success) =>
            //  {
            //      if (success)
            //      {
            //          ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderboard);
            //          PlayerPrefs.SetInt("NoOfscorePosted", (PlayerPrefs.GetInt("NoOfscorePosted") + 1));
            //      }
            //      else
            //      {
            //          //Debug.Log("Login failed for some reason");
            //      }
            //  });
            //}
            //else
            //{
            //    Social.localUser.Authenticate((bool success) =>
            //    {
            //        if (success)
            //        {
            //            Social.ReportScore(PlayerPrefs.GetInt("leaderBoardScore"), leaderboard, (bool successful) =>
            //          {
            //              if (successful)
            //              {
            //                  ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderboard);
            //                  PlayerPrefs.SetInt("NoOfscorePosted", (PlayerPrefs.GetInt("NoOfscorePosted") + 1));
            //              }
            //              else
            //              {
            //                  //Debug.Log("Login failed for some reason");
            //              }
            //          });
            //        }
            //    });

            //}
        }
        catch (System.Exception e)
        {

        }
    }

    void unLockachievementFtn(string achievementName)
    {

        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(achievementName, 100.0f, (bool success) =>
                                  {
                                      if (success)
                                      {
                                          Debug.Log("achievement Unlocked:  " + achievementName);
                                      }
                                      else
                                      {
                                          Debug.Log("Login failed for some reason");
                                      }
                                  });
        }

    }

    public void checkAchievement()
    {

        if (PlayerPrefs.GetInt("OpponentCrashedUsingAxe") >= 20)
        {

            unLockachievementFtn(bikeAttackRaceGPSS.achievement_fightergold);

        }

        if (PlayerPrefs.GetInt("totalOpponentCrashed") >= 15)
        {
            unLockachievementFtn(bikeAttackRaceGPSS.achievement_fightersilver);
        }
        else if (PlayerPrefs.GetInt("totalOpponentCrashed") >= 5)
        {
            unLockachievementFtn(bikeAttackRaceGPSS.achievement_fighterbronze);
        }

        if (PlayerPrefs.GetString("fireStormDestroyed").Equals("true"))
        {

            unLockachievementFtn(bikeAttackRaceGPSS.achievement_shootergold);

        }

        if (PlayerPrefs.GetInt("OpponentCrashedUsingShotgun") >= 3)
        {

            unLockachievementFtn(bikeAttackRaceGPSS.achievement_shooter_silver);
        }
        if (PlayerPrefs.GetInt("OpponentCrashedUsingPistol") >= 3)
        {

            unLockachievementFtn(bikeAttackRaceGPSS.achievement_shooterbronze);
        }


        if (PlayerPrefs.GetInt("ownBikeCostume") >= 14)
        {

            unLockachievementFtn(bikeAttackRaceGPSS.achievement_richiegold);
        }
        if (PlayerPrefs.GetInt("ticketUsed") >= 50)
        {

            unLockachievementFtn(bikeAttackRaceGPSS.achievement_richiesilver);
        }

        if (PlayerPrefs.GetString("shotgunUnlocked").Equals("true"))
        {

            unLockachievementFtn(bikeAttackRaceGPSS.achievement_richiebronze);
        }


        if (PlayerPrefs.GetInt("singelLevelScore") >= 5000)
        {

            unLockachievementFtn(bikeAttackRaceGPSS.achievement_champion_gold);
        }

        if (PlayerPrefs.GetFloat("totalDistaneCovered") >= 50000)
        {


            unLockachievementFtn(bikeAttackRaceGPSS.achievement_champion_silver);
        }

        if (PlayerPrefs.GetInt("NoOfscorePosted") >= 10)
        {

            unLockachievementFtn(bikeAttackRaceGPSS.achievement_champion_bronze);
        }


        if (PlayerPrefs.GetInt("shieldUsed") >= 20)
        {

            unLockachievementFtn(bikeAttackRaceGPSS.achievement_collector_gold);
        }

        if (PlayerPrefs.GetInt("boost/timerUsed") >= 20)
        {

            unLockachievementFtn(bikeAttackRaceGPSS.achievement_collectorsilver);
        }

        if (PlayerPrefs.GetInt("helmetUsed") >= 5)
        {

            unLockachievementFtn(bikeAttackRaceGPSS.achievement_collectorbronze);
        }
    }
}
