using UnityEngine;

public class Rocketshoot : MonoBehaviour
{
    public jeepControl jeep;
    public static float shootRange;
    public heavyBikeTurnControls heavyBike;
    public turnLevelcontrols harleyBike;
    public GameObject bike, rocket;
    int levelID;

    bool playerCrashed, playerDead, levelClear, gameOver;
    public AudioClip missileFireSound;
    public static bool jeepRevive;
    void Awake()
    {
        shootRange = 5;

    }
    public void revive()
    {
        CancelInvoke("shoot");

        jeepRevive = true;
    }
    //var bullet : GameObject;
    void Start()
    {

        if (bike.GetComponent<heavyBikeTurns>().parent.name.Contains("Fatguy"))
        {
            levelID = 3;
        }
        else
            levelID = 2;


        Invoke("Shoot", 12f);
    }

    /*       void Update()
          {
              transform.LookAt(bike.transform.position); //khuram
          } */

    void Shoot()
    {
        transform.LookAt(bike.transform.position);
        if (!jeepRevive)
        {

            if (playerHealthBar.Remainingdistance <= 700)
            {
                shootRange = 2f;
            }
            else if (playerHealthBar.Remainingdistance <= 1500)
            {
                shootRange = 4f;
            }

            if (levelID == 2)
            {

                playerCrashed = heavyBike.crash;
                playerDead = heavyBikeTurns.isdead;
                levelClear = heavyBikeTurnControls.levelClear;
                gameOver = heavyBikeTurnControls.timeover;

            }
            else if (levelID == 3)
            {

                playerCrashed = harleyBike.crash;
                playerDead = heavyBikeTurns.isdead;
                levelClear = turnLevelcontrols.levelClear;
                gameOver = turnLevelcontrols.timeover;

            }

            if (handleArrow.start && endlessmodeGraphics.gameMode.Equals("Idle"))
            {


                if (!playerCrashed && !playerDead && !levelClear && !gameOver && jeep.health > 0)
                {
                    bike.GetComponent<AudioSource>().PlayOneShot(missileFireSound);
                    Instantiate(rocket, transform.position + new Vector3(0, -0.5f, 0), transform.rotation);

                }
                Invoke("Shoot", shootRange);
            }


            if (playerCrashed || playerDead)
            {
                //print ("inside iff");
                CancelInvoke("Shoot");
                Invoke("Shoot", 7f);
            }

        }

        else
        {
            CancelInvoke("Shoot");
            Invoke("reviveOff", 10f);
        }

    }

    void reviveOff()
    {
        jeepRevive = false;
        Invoke("Shoot", 1f);
    }
}
