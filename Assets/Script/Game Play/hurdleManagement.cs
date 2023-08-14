using UnityEngine;
using System.Collections.Generic;

public class hurdleManagement : MonoBehaviour
{
    public GameObject[] tracks, TotalHurdles;
    List<float> Zpos = new List<float>();
    List<Transform> levelHurdles = new List<Transform>();
    public Transform[] hurdles;
    public GameObject currentHurdle;
    public float zPosition;
    float distance;
    int totalPosition;
    int level;
    int i = 3;
    public GameObject player, opponent1, opponent2, coins1, coins2;
    void Awake()
    {
        //	PlayerPrefs.SetInt ("levels", 15);

        level = PlayerPrefs.GetInt("levels");
        if (GameAnalytics.instance != null)
            GameAnalytics.instance.LevelStartEvent(level);
        if (player)
            player.SetActive(true);
        if (opponent1)
            opponent1.SetActive(true);
        if (opponent2)
            opponent2.SetActive(true);
        if (coins1)
            coins1.SetActive(true);
        if (coins2)
            coins2.SetActive(true);
        //		print (level);
        //	level =11;
        //level = 13;

        if (level == 7 || level == 11 || level == 14 || level == 1 || level == 13)
        {

            tracks[0].SetActive(true);
            tracks[0] = null;

        }
        else if (level == 2 || level == 4 || level == 5 || level == 6 || level == 8)
        {

            tracks[2].SetActive(true);
            tracks[2] = null;

        }
        else if (level == 3 || level == 9 || level == 10 || level == 12 || level == 15 || level == 16)
        {

            tracks[1].SetActive(true);
            tracks[1] = null;

        }



        for (var i = 0; i < tracks.Length; i++)
        {
            Destroy(tracks[i]);
        }

        if (level == 1)
        {

            TotalHurdles[0].SetActive(true);
            currentHurdle = TotalHurdles[0];
            TotalHurdles[0] = null;


        }
        else if (level == 2)
        {


            TotalHurdles[1].SetActive(true);
            currentHurdle = TotalHurdles[1];
            TotalHurdles[1] = null;

        }
        else if (level == 3 || level == 4)
        {


            TotalHurdles[2].SetActive(true);
            currentHurdle = TotalHurdles[2];
            TotalHurdles[2] = null;

        }
        else if (level == 5)
        {


            TotalHurdles[3].SetActive(true);
            currentHurdle = TotalHurdles[3];
            TotalHurdles[3] = null;

        }
        else if (level == 6)
        {


            TotalHurdles[4].SetActive(true);
            currentHurdle = TotalHurdles[4];
            TotalHurdles[4] = null;

        }

        else if (level == 7 || level == 8)
        {

            TotalHurdles[5].SetActive(true);
            currentHurdle = TotalHurdles[5];
            TotalHurdles[5] = null;

        }
        else if (level == 9)
        {

            TotalHurdles[6].SetActive(true);
            currentHurdle = TotalHurdles[6];
            TotalHurdles[6] = null;

        }
        else if (level == 10)
        {

            TotalHurdles[7].SetActive(true);
            currentHurdle = TotalHurdles[7];
            TotalHurdles[7] = null;

        }
        else if (level == 11)
        {

            TotalHurdles[8].SetActive(true);
            currentHurdle = TotalHurdles[8];
            TotalHurdles[8] = null;

        }
        else if (level == 12)
        {

            TotalHurdles[9].SetActive(true);
            currentHurdle = TotalHurdles[9];
            TotalHurdles[9] = null;

        }
        else if (level == 13 || level == 14)
        {

            TotalHurdles[10].SetActive(true);
            currentHurdle = TotalHurdles[10];
            TotalHurdles[10] = null;

        }
        else if (level == 15 || level == 16)
        {

            TotalHurdles[11].SetActive(true);
            currentHurdle = TotalHurdles[11];
            TotalHurdles[11] = null;

        }


        for (var i = 0; i < TotalHurdles.Length; i++)
        {
            Destroy(TotalHurdles[i]);
        }
    }
    void Start()
    {
        i = 3;


        if (level == 1)
        {

            totalPosition = 10;
            distance = 62.5f;


        }
        else if (level == 2)
        {

            distance = 62.5f;
            totalPosition = 10;

        }
        else if (level == 3 || level == 4)
        {

            distance = 62.5f;
            totalPosition = 18;

        }
        else if (level == 5)
        {

            distance = 100f;
            totalPosition = 8;

        }
        else if (level == 6)
        {

            distance = 100f;
            totalPosition = 8;

        }
        else if (level == 11)
        {
            distance = 80f;
            totalPosition = 20;
        }
        else if (level == 12)
        {
            distance = 55f;
            totalPosition = 30;
        }
        else if (level == 13 || level == 14)
        {

            distance = 47f;
            totalPosition = 35;

        }
        if ((level < 7 || level >= 11) && level != 15 && level != 16)
        {
            foreach (Transform childs in currentHurdle.transform)
            {
                i += 1;

                levelHurdles.Add(childs.transform);
            }

            SetzPosition();

        }
        else if (level == 7 || level == 8 || level == 9 || level == 10 || level == 15 || level == 16)
        {

            placePickups();
        }
    }

    void placePickups()
    {
        hurdles[2].position = new Vector3(hurdles[0].position.x, hurdles[0].position.y, -600f);
        hurdles[1].position = new Vector3(hurdles[1].position.x, hurdles[1].position.y, -300f);
        hurdles[0].position = new Vector3(hurdles[0].position.x, hurdles[2].position.y, 0f);
        hurdles[3].position = new Vector3(hurdles[1].position.x, hurdles[3].position.y, 300f);
        hurdles[4].position = new Vector3(hurdles[1].position.x, hurdles[3].position.y, -450f);
    }

    void placeHurdles()
    {
        if (level == 6)
        {
            hurdles[2].position = new Vector3(hurdles[0].position.x, hurdles[0].position.y, Zpos[2]);
            hurdles[1].position = new Vector3(hurdles[1].position.x, hurdles[1].position.y, Zpos[5]);
            hurdles[0].position = new Vector3(hurdles[0].position.x, hurdles[2].position.y, Zpos[8]);
            hurdles[3].position = new Vector3(hurdles[1].position.x, hurdles[3].position.y, Zpos[11]);
        }
        else
        {
            hurdles[0].position = new Vector3(hurdles[0].position.x, hurdles[0].position.y, Zpos[2]);
            hurdles[1].position = new Vector3(hurdles[1].position.x, hurdles[1].position.y, Zpos[5]);
            hurdles[2].position = new Vector3(hurdles[0].position.x, hurdles[2].position.y, Zpos[8]);
            hurdles[3].position = new Vector3(hurdles[1].position.x, hurdles[3].position.y, Zpos[11]);
        }
        //print (Zpos.Count);
        Zpos.RemoveAt(11);
        Zpos.RemoveAt(8);
        Zpos.RemoveAt(5);
        Zpos.RemoveAt(2);



        for (int i = 0; i < levelHurdles.Count; i++)
        {
            int random = Random.Range(0, totalPosition);

            levelHurdles[i].position = new Vector3(levelHurdles[i].position.x, levelHurdles[i].position.y, Zpos[random]);
            Zpos.RemoveAt(random);
            totalPosition -= 1;

        }
    }
    void SetzPosition()
    {
        for (int i = 0; i < (totalPosition + 4); i++)
        {

            zPosition += distance;
            Zpos.Add(zPosition);
        }

        placeHurdles();

    }
}




