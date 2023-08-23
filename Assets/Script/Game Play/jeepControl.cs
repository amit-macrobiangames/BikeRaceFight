using UnityEngine;

public class jeepControl : MonoBehaviour
{
    public AudioClip hitSound;
    public Transform levelClear;
    //public ParticleEmitter smoke;
    public GameObject impactGameobject;
    public AudioClip exploisionSound;
    public GameObject explodedJeep;
    public static string gunName = "";
    public Texture new13, new9, new1, new0;
    public GameObject healthBar;
    public GameObject enemyCar, shieldPickup;
    public Transform playerBike;
    float val, lastPosition;
    bool check;
    public static bool towardPlayer;
    int levelID;
    public heavyBikeTurnControls heavyBike;
    public turnLevelcontrols harleyBike;
    bool playerCrashed, playerDead;
    public int health;
    float previousValue;
    int levelNumber;
    void _Awake()
    {
        // smoke.maxEmission=7;//khuram
        // smoke.enabled=false;
        levelNumber = PlayerPrefs.GetInt("levels");
        health = 10; //20
        //health = 1;
       // playerBike = endlessmodeGraphics.instance.player;
        enemyCar.transform.position = new Vector3(enemyCar.transform.position.x, enemyCar.transform.position.y, playerBike.position.z + 205);
        //enemyCar.transform.position=new Vector3(enemyCar.transform.position.x,enemyCar.transform.position.y, playerBike.transform.position.z + 1000);
        towardPlayer = true;
        if (levelNumber != 7 && levelNumber != 8 && levelNumber != 20 && levelNumber != 18)
        {
            shieldPickup.SetActive(false);
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            shieldPickup.SetActive(true);
        }
        if (levelNumber == 7 || levelNumber == 8 || levelNumber == 20 || levelNumber == 18)
        {
            healthBar.SetActive(true);
            healthBar.transform.GetComponent<Renderer>().material.mainTexture = new13;
        }
        else
            healthBar.SetActive(false);

        if (playerBike.GetComponent<heavyBikeTurns>().parent.name.Contains("Fatguy"))
        {
            levelID = 3;
        }
        else
            levelID = 2;
    }
    void Start()
    {
        _Awake();
        check = true;
        Time.timeScale = 1;
    }
    void explode()
    {
#if UNITY_EDITOR
        print("explode");
#endif
        playerBike.GetComponent<AudioSource>().PlayOneShot(exploisionSound);
        explodedJeep.SetActive(true);
        explodedJeep.transform.parent = null;
        gameObject.SetActive(false);
        if (playerHealthBar.Remainingdistance >= 1000)
        {
            PlayerPrefs.SetString("fireStormDestroyed", "true");
        }
        levelClear.position = new Vector3(2.85f, -0.075f, transform.position.z + 10f);
    }
    void offEffect()
    {
        impactGameobject.SetActive(false);
    }
    public void onFire()
    {
        if (health > 0)
        {
            playerBike.GetComponent<AudioSource>().PlayOneShot(hitSound);
            if (gunName.Equals("pistol"))
                health -= 1;
            else if (gunName.Equals("shotgun"))
            {
                health -= 2; //4
                             // smoke.maxEmission+=2;//khuram
            }
            else if (gunName.Equals("missile"))
            {
                health -= 3; //4
                             // smoke.maxEmission+=3;//khuram
            }
            if (health >= 8 && health < 10)
            {
                healthBar.transform.GetComponent<Renderer>().material.mainTexture = new13;
            }
            else if (health >= 6 && health < 8)
            {
                // smoke.enabled=true;//khuram
                healthBar.transform.GetComponent<Renderer>().material.mainTexture = new9;
            }
            else if (health >= 3 && health < 6)
            {
                healthBar.transform.GetComponent<Renderer>().material.mainTexture = new1;
            }
            else if (health < 3)
            {
                healthBar.transform.GetComponent<Renderer>().material.mainTexture = new0;
            }

            //impactEffect.Play();
            // smoke.maxEmission+=2;//khuram
            impactGameobject.SetActive(true);

            Invoke("offEffect", 0.25f);
            //			if(health>=15 && health<20)
            //				healthBar.transform.renderer.material.mainTexture= new13;	
            //			else 	if(health>=10 && health<15)
            //				healthBar.transform.renderer.material.mainTexture= new9;
            //			else 	if(health>=5 && health<10)
            //				healthBar.transform.renderer.material.mainTexture= new1;
            //			else 	if(health<5)
            //				healthBar.transform.renderer.material.mainTexture= new0;
        }
        if (health <= 0)
        {
            explode();
        }
        //	print (gunName+ " "+ health);
    }
    void Update()
    {
        //print (45f * Time.fixedDeltaTime);
        if (handleArrow.start && endlessmodeGraphics.gameMode.Equals("Idle"))
        {
            if (levelID == 2)
            {
                playerCrashed = heavyBike.crash;
                playerDead = heavyBikeTurns.isdead;
            }
            else if (levelID == 3)
            {
                playerCrashed = harleyBike.crash;
                playerDead = heavyBikeTurns.isdead;
            }
            if ((playerCrashed || playerDead))
            {
                transform.Translate(Vector3.forward * 20f * Time.deltaTime);
            }
            else if (!playerCrashed && !playerDead)
            {
                if (towardPlayer)
                {
                    transform.Translate(Vector3.back * 40f * Time.fixedDeltaTime);
                    //	enemyCar.transform.position = new Vector3 (enemyCar.transform.position.x, enemyCar.transform.position.y, Mathf.Lerp (transform.position.z, playerBike.transform.position.z + 50, Time.timeScale * 0.02f));

                    if ((transform.position.z - playerBike.position.z) <= 20)
                    {
                        towardPlayer = false;
                    }
                }
                else
                {
                    if (check == true)
                        Invoke("rand", 1);
                    //enemyCar.transform.position=new Vector3(enemyCar.transform.position.x,enemyCar.transform.position.y, playerBike.transform.position.z + 100);
                    enemyCar.transform.position = new Vector3(enemyCar.transform.position.x, enemyCar.transform.position.y, playerBike.position.z + 20);
                    check = false;

                    if (RocketDestroy.hit == true)
                    {
                        RocketDestroy.hit = false;
                    }
                    else
                    {
                        if (enemyCar.transform.position.x <= (1.25f) || enemyCar.transform.position.x >= 4.25f)
                        {
                            //Invoke("rand",4);
                            if (enemyCar.transform.position.x <= (1.2f))
                            {
                                enemyCar.transform.position = new Vector3(1.25f, enemyCar.transform.position.y, enemyCar.transform.position.z);
                                //enemyCar.transform.position.x = -0.69f;
                            }
                            else if (enemyCar.transform.position.x >= 4.25f)
                            {
                                enemyCar.transform.position = new Vector3(4.1f, enemyCar.transform.position.y, enemyCar.transform.position.z);
                                //enemyCar.transform.position.x = 5.1f;
                            }
                        }
                        enemyCar.transform.Translate(val * Time.deltaTime * 10, 0, 0);
                    }
                }
            }
        }
    }
    void rand()
    {
        //	print (val);
        if (val < 0)
            val = Random.Range(0.02f, 0.1f);
        else
            val = Random.Range(-0.1f, 0f);
        //	val = Random.Range(-0.1f,0.1f);
        check = true;
    }
    //val = Random.Range(-0.1f,0.1f);
}
