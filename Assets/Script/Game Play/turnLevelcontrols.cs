using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class turnLevelcontrols : MonoBehaviour
{

    public PhycamViews mainCamera;
    public Transform shadow;
    Transform ragdoll, ragdoll_rigid;
    public Transform timefinish, ragdollprefab;
    public Transform shadowLevelClear;
    public Texture purchase, notEnoughCash, buyIt;

    public Transform frontWheel, rearWheel;
    public Transform scorePopup;
    public static int cash;
    public static float distance;

    public static float triggerDistance;
    public static bool scoreOnce, lifeEnd;
    public static bool soundOnce;

    public bool crash;

    bool brakeOnce;

    bool leftAttackbool;

    bool leftAttackOnce;
    bool leftPunchOnce;
    bool leftPunchbool;


    public static bool levelClear;

    public AudioClip hitSound, hitgrunt, grunt;
    public GameObject raceSound, rampSound;


    public AudioClip deathSound;
    public static AudioSource audioSource;
    public AudioClip punchSound, kickSound;


    endlessmodeGraphics script;

    public Transform player;



    public AnimationClip idle, boost;
    public AnimationClip race;
    public AnimationClip death;
    public AnimationClip punch;
    public AnimationClip attack, brake;



    //public GUIStyle punchstyle;
    //public GUIStyle kickstyle;


    public static float playerPosition;
    public static int score;
    public static bool startGame;
    public static bool isdead;


    public bool isAttacking;







    public AnimationClip oneWheeling;






    Rect leftPunchRect;

    Rect leftAttackRect;




    private Vector2 fp;  // first finger position
    private Vector2 lp;


    public static bool timeover;




    bool audioplayOnce;



    //	public static int staminaBarCounter;
    //	public static bool emptyStamina;
    //	public bool staminaBarCollision;
    //	
    string biker;
    string bike;
    bool collideOnce;

    public Transform nitroLeft, nitroRight;

    public Transform hand, leg;
    public bool lifeBar = false;
    //Rect skipRect;
    //public GUIStyle skipStyle;
    public bool skipClicked = false;
    int layerMask;
    public Transform WheelFL, WheelRL;
    heavyBikeTurns parentScript;
    public bool isStunting, stuntbool;
    public WheelCollider front, rear;
    WheelHit hit;
    bool setTimeScale;

    public AudioClip hurdleHit;


    bool countonce;
    public bool unstability;

    int desertLevel;

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Contains("desert"))
        {
            desertLevel = 1;
        }

        countonce = true;
        setTimeScale = false;
        attackCount = 0;
        callOnce = true;
        skipClicked = false;
        lifeBar = false;
        attackCount = 0;
        levelClear = false;
        //Instantiate(dustParticle,dustPosition.position,Quaternion.identity);
        collideOnce = true;
        layerMask = 1 << 8;
        //	staminaBarCollision = false;

        brakeOnce = true;
        timeover = false;

        lifeEnd = false;
        script = GameObject.Find("graphics").GetComponent<endlessmodeGraphics>();
        //skipRect=new Rect(Screen.width*.62f, (Screen.height-Screen.width*0.3f*59/365)/2, Screen.width*.3f, Screen.width *.3f*59/365 );


        parentScript = player.root.GetComponent<heavyBikeTurns>();

        scoreOnce = true;
        soundOnce = true;


        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        leftAttackbool = false;


        leftPunchbool = false;
        leftAttackOnce = true;
        leftPunchOnce = true;








        audioplayOnce = true;










        //staminaRect = new Rect (0.0f, Screen.height * 0.22f, Screen.width * 0.8f* 45 / 464, Screen.height*0.5f);
        leftPunchRect = new Rect(Screen.width * 0.0f, Screen.height - Screen.width * 0.14f, Screen.width * .14f, Screen.width * .14f);
        leftAttackRect = new Rect(Screen.width - Screen.width * 0.14f, Screen.height - Screen.width * 0.14f, Screen.width * .14f, Screen.width * .14f);








        startGame = false;


        isAttacking = false;


        isdead = false;

        score = 0;
        cash = 0;

        cash = PlayerPrefs.GetInt("cash");
        score = PlayerPrefs.GetInt("cash");


        player.GetComponent<Animation>()[idle.name].layer = 0;
        player.GetComponent<Animation>()[race.name].layer = 1;
        player.GetComponent<Animation>()[death.name].layer = 5;
        player.GetComponent<Animation>()[punch.name].layer = 7;
        player.GetComponent<Animation>()[attack.name].layer = 8;


        player.GetComponent<Animation>().Play(idle.name, PlayMode.StopAll);

        Invoke("soundOn", 1.5f);
        player.GetComponent<Animation>()[race.name].speed = 2f;
        player.GetComponent<Animation>()[punch.name].speed = 2f;
        player.GetComponent<Animation>()[attack.name].speed = 2f;

        //		biker=	PlayerPrefs.GetString("bikerMesh");
        //		bike=	PlayerPrefs.GetString("bikeMesh");
        //		
        //		player.FindChild ("biker_fat-01").transform.renderer.material.mainTexture= Resources.Load(biker) as Texture;
        //		player.FindChild ("bike_body").transform.renderer.material.mainTexture= Resources.Load(bike) as Texture;


        race.wrapMode = WrapMode.Once;
        boost.wrapMode = WrapMode.Once;
        dressCode();

        Invoke("starting", 0.05f);

    }

    public Texture[] shirts, bikeTexture;
    public Material bikerMat, bikeMat;

    void dressCode()
    {

        bike = "harley" + PlayerPrefs.GetString("bikeColor");
#if UNITY_EDITOR
        Debug.Log("" + bike);
#endif
        biker = PlayerPrefs.GetString("shirtColor");



        switch (biker)
        {
            case "shirt1":
                bikerMat.mainTexture = shirts[0];
                break;
            case "shirt2":
                bikerMat.mainTexture = shirts[1];
                break;
            case "shirt3":
                bikerMat.mainTexture = shirts[2];
                break;
            case "shirt4":
                bikerMat.mainTexture = shirts[3];
                break;
            case "shirt5":
                bikerMat.mainTexture = shirts[4];
                break;
            case "shirt6":
                bikerMat.mainTexture = shirts[5];
                break;
            case "shirt7":
                bikerMat.mainTexture = shirts[6];
                break;
            case "shirt8":
                bikerMat.mainTexture = shirts[7];
                break;
            case "shirt9":
                bikerMat.mainTexture = shirts[8];
                break;
            case "shirt10":
                bikerMat.mainTexture = shirts[9];
                break;
        }

        switch (bike)
        {
            case "harleyyellow":
                bikeMat.mainTexture = bikeTexture[0];
                break;
            case "harleyred":
                bikeMat.mainTexture = bikeTexture[1];
                break;
            case "harleygrey":
                bikeMat.mainTexture = bikeTexture[2];
                break;
            case "harleyblue":
                bikeMat.mainTexture = bikeTexture[3];
                break;
            case "harleyblack":
                bikeMat.mainTexture = bikeTexture[4];
                break;


        }
        //player.Find ("biker-v3").transform.GetComponent<Renderer>().material.mainTexture= Resources.Load(biker) as Texture;
        //player.Find ("bike_body").transform.GetComponent<Renderer>().material.mainTexture= Resources.Load(bike) as Texture;
    }



    void starting()
    {


        if (desertLevel == 1)
        {

            //print ("turn level");

            transform.root.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            transform.root.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
            handleArrow.start = true;
            heavyBikeTurns.startRace = true;
            startGame = true;
            GameObject.Find("AnimationCamera").GetComponent<startAnimation>().startanimating();

        }





    }





    public void revivalStart()
    {

        if (transform.localScale.x <= -1f)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        Invoke("soundOn", 1.5f);

        transform.root.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        transform.root.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
        handleArrow.start = true;
        heavyBikeTurns.startRace = true;
        startGame = true;

        //GameObject.Find("AnimationCamera").GetComponent<startAnimation>().startanimating();
        brakeOnce = true;
        StartCoroutine("flash");
        attackCount = 0;
        if (ragdoll != null)
            ragdoll.gameObject.SetActive(false);
        player.Find("biker-v3").transform.gameObject.SetActive(true);

        //	player.root.rigidbody.constraints =~ RigidbodyConstraints.FreezeAll;
        //	player.root.rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;


        countonce = true;
        setTimeScale = false;
        attackCount = 0;
        callOnce = true;
        skipClicked = false;
        lifeBar = false;
        attackCount = 0;
        levelClear = false;

        collideOnce = true;


        timeover = false;

        lifeEnd = false;
        shadowsOff();
        crash = false;

        scoreOnce = true;
        soundOnce = true;



        leftAttackbool = false;


        leftPunchbool = false;
        leftAttackOnce = true;
        leftPunchOnce = true;


        audioplayOnce = true;


        startGame = false;


        isAttacking = false;


        isdead = false;

        score = 0;
        cash = 0;

        cash = PlayerPrefs.GetInt("cash");
        score = PlayerPrefs.GetInt("cash");



        player.GetComponent<Animation>().Play(race.name, PlayMode.StopAll);

        //Invoke ("soundOn",1.5f);
        //Invoke ("starting",0.05f);

        //		biker=	PlayerPrefs.GetString("bikerMesh");
        //		bike=	PlayerPrefs.GetString("bikeMesh");
        //		
        //		//		player.FindChild ("biker-v3").transform.renderer.material.mainTexture= Resources.Load(biker) as Texture;
        //		player.FindChild ("bike_body").transform.renderer.material.mainTexture= Resources.Load(bike) as Texture;


        race.wrapMode = WrapMode.Once;
        boost.wrapMode = WrapMode.Once;




    }
    // Update is called once per frame



    void soundOn()
    {

        if (PlayerPrefs.GetInt("SoundOff") == 0)
        {
            audioSource = (AudioSource)GetComponent(typeof(AudioSource));
            GetComponent<AudioSource>().playOnAwake = true;

            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().enabled = true;
            raceSound.GetComponent<AudioSource>().enabled = true;
            rampSound.GetComponent<AudioSource>().enabled = true;

        }
        else
        {
            GetComponent<AudioSource>().Pause();
            GetComponent<AudioSource>().enabled = false;
            raceSound.GetComponent<AudioSource>().enabled = false;
            rampSound.GetComponent<AudioSource>().enabled = false;


        }

    }









    void FixedUpdate()
    {

        //	Instantiate(dustParticle,dustPosition.position,Quaternion.identity);


        if (lifeBar)
        {
            if (player.root.GetComponent<heavyBikeTurns>().counter == 0 && attackCount < 3)
            {
                attackCount = 0;
            }
            else if (player.root.GetComponent<heavyBikeTurns>().counter == 0 && attackCount > 3 && attackCount < 6)
            {
                attackCount = 3;
            }
            else if (player.root.GetComponent<heavyBikeTurns>().counter == 0 && attackCount > 6 && attackCount < 9)
            {
                attackCount = 6;
            }
            lifeBar = false;
        }




        if (startGame && !isdead)
        {
            distance = timefinish.position.z - player.position.z;

        }

        if (timeover)
        {
            raceSound.SetActive(false);
            rampSound.SetActive(false);

            if (brakeOnce)
            {
                player.GetComponent<Animation>().Play(brake.name, PlayMode.StopAll);
                brakeOnce = false;
                Invoke("gameover", brake.length);
            }
        }

        playerPosition = player.position.z;
        if (timeover || levelClear)
        {
            raceSound.SetActive(false);
            nitroLeft.gameObject.SetActive(false);
            nitroRight.gameObject.SetActive(false);
        }
        if (isdead)
        {
            rampSound.SetActive(false);
            raceSound.SetActive(false);
            if (PlayerPrefs.GetInt("levels") != 11 && PlayerPrefs.GetInt("levels") != 12)
                player.Find("biker-v3").gameObject.SetActive(false);
        }
        if (levelClear)
        {
            shadowLevelClear.gameObject.SetActive(true);
            rampSound.SetActive(true);
            transform.Find("shadow").gameObject.SetActive(false);
        }
        //		if (levelClear && endlessmodeGraphics.gameMode.Equals("Idle")) {
        //			//frontWheel.Rotate (0,0,80);
        //			//rearWheel.Rotate (0,0,80);		
        //		}
        if (!heavyBikeTurns.boostbool)
        {

            nitroLeft.gameObject.SetActive(false);
            nitroRight.gameObject.SetActive(false);


        }
        if (!isAttacking && !isdead && !timeover && startGame && !crash && !levelClear && !unstability)
        {

            if ((front.GetGroundHit(out hit) && rear.GetGroundHit(out hit)))
            {
                if (!raceSound.activeSelf)
                    raceSound.SetActive(true);
                rampSound.SetActive(false);
                if (heavyBikeTurns.Onramp)
                {
                    player.root.GetComponent<heavyBikeTurns>().rampControl = false;
                    heavyBikeTurns.Onramp = false;
                }


            }
            else if (!front.GetGroundHit(out hit) || !rear.GetGroundHit(out hit))
            {


                raceSound.SetActive(false);
                if (!rampSound.activeSelf)
                    rampSound.SetActive(true);

            }



            if (!isStunting && !heavyBikeTurns.jumpAnim)
            {
                if (!heavyBikeTurns.boostbool)
                {
                    player.GetComponent<Animation>().Play(race.name, PlayMode.StopAll);

                    //audio.Pause ();

                    nitroLeft.gameObject.SetActive(false);
                    nitroRight.gameObject.SetActive(false);
                    raceSound.GetComponent<AudioSource>().pitch = 1.25f;

                }
                else if (heavyBikeTurns.boostbool)
                {
                    player.GetComponent<Animation>().Play(boost.name, PlayMode.StopAll);
                    nitroLeft.gameObject.SetActive(true);
                    nitroRight.gameObject.SetActive(true);
                    raceSound.GetComponent<AudioSource>().pitch = 2f;

                }
                if (PhycamViews.slowingDown)
                {

                    raceSound.GetComponent<AudioSource>().pitch = 0.5f;
                    //raceSound.SetActive(false);
                    //nitroSound.SetActive (true);
                }
            }
        }




        if (Application.isEditor)
        {




        }
        else
        {

            if (handleArrow.start)
            {
                startGame = true;
            }
            if (startGame && !isdead && !crash)
            {



                for (int k = 0; k < Input.touchCount; ++k)
                {
                    Touch touch = Input.GetTouch(k);


                    if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                    {
                        Vector2 touchPos = new Vector2(touch.position.x, Screen.height - touch.position.y);



                        if (leftPunchRect.Contains(touchPos))
                        {


                            //leftPunchRect=new Rect(Screen.width*0.0f, Screen.height-Screen.width*0.15f, Screen.width*.16f, Screen.width *.16f );
                            if (!parentScript.flyoverStart && !parentScript.isJumping && !transform.root.GetComponent<weaponAI>().isAttacking)
                            {
                                if (PhycamViews.counter == 11 || PhycamViews.counter == 12)
                                {
                                    PhycamViews.counter = 2;
                                    mainCamera.changeCamView();
                                }
                                if (leftPunchOnce)
                                {
                                    leftPunchbool = true;
                                    leftPunchOnce = false;
                                    Invoke("setter", 1f);

                                }
                            }

                        }


                        if (leftAttackRect.Contains(touchPos))
                        {

                            if (!parentScript.flyoverStart && !parentScript.isJumping && !transform.root.GetComponent<weaponAI>().isAttacking)
                            {
                                if (leftAttackOnce)
                                {
                                    if (PhycamViews.counter == 11 || PhycamViews.counter == 12)
                                    {
                                        PhycamViews.counter = 2;
                                        mainCamera.changeCamView();
                                    }
                                    leftAttackbool = true;
                                    leftAttackOnce = false;
                                    Invoke("setter", 0.92f);
                                }
                            }

                        }





                    }
                    else
                    {

                        leftPunchbool = false;
                        leftAttackbool = false;

                    }








                }










            }






        }






    }
    void setter()
    {
        leftAttackOnce = true;
        leftPunchOnce = true;

    }




    //	
    //	bool hover=true;
    //	float myTimer=2f;
    //	
    //	void reset1(){
    //		if (hover) {
    //			
    //			hover=false;
    //		} else {
    //			
    //			hover=true;
    //		}
    //		
    //		
    //	}

    bool callOnce = true;
    void Update()
    {

        //
        //		if (staminaBarCounter == 0) {
        //			myTimer -= Time.deltaTime;
        //			if(myTimer<=1.5f)
        //			{
        //				reset1();
        //				myTimer=2f;
        //			}
        //			if(hover)	
        //				script.staminaBar= Resources.Load<Texture2D>("staminabr-Green_BG");
        //			else
        //				script.staminaBar= Resources.Load<Texture2D>("staminabr-red_BG");
        //		}
        //		
        //		if (staminaBarCollision) {
        //			if(staminaBarCounter<endlessmodeGraphics.INITIAL_HEALTH)
        //			{
        //				staminaBarCounter+=1;
        //				endlessmodeGraphics.MAX_HEALTH+=1;
        //			}
        //			//	AddingstaminaTexture();
        //			
        //			staminaBarCollision=false;
        //		}
        if (startGame && !isdead && !crash && !levelClear)
        {

            WheelFL.Rotate(0, 0, 700.0f * Time.deltaTime);
            WheelRL.Rotate(0, 0, 700.0f * Time.deltaTime);



            if (setTimeScale)
            {

                if (Time.timeScale < 1.0f)
                {
                    Time.timeScale = Mathf.Lerp(Time.timeScale, 1, 0.1f);
                }
                if (Time.fixedDeltaTime < 0.02f)
                {
                    Time.fixedDeltaTime = Mathf.Lerp(Time.fixedDeltaTime, 0.02f, 0.1f);
                }
                if (Time.timeScale >= 0.985 && Time.fixedDeltaTime >= 0.0195f)
                {
                    setTimeScale = false;



                }
            }


            if (player.localPosition.y < -0.05)
            {
                player.position = new Vector3(player.position.x, 0.0294692f, player.position.z);
            }
            RaycastHit hit = new RaycastHit();

            if (isAttacking)
            {


                if (Physics.Raycast(hand.position, Vector3.left, out hit, 0.1f, layerMask))
                {

                    //	print ((hit.collider.transform.root.position.x-player.root.position.x)+"  "+hit.collider.transform.root.name+ " "+ hit.collider.name);
                    if (hit.collider.transform.root.name.Contains("opponentFatguy") && (hit.collider.transform.root.position.x - player.root.position.x <= -0.35f))
                    { //-1.75f
                        if (callOnce)
                        {

                            hit.collider.transform.root.GetComponent<newLevelCrashed>().opponentAttacked("punch");



                            callOnce = false;
                        }
                    }
                    if (hit.collider.transform.root.name.Contains("heavybike endless") && (hit.collider.transform.root.position.x - player.root.position.x <= -0.35f))
                    { //-1.75f
                        if (callOnce)
                        {

                            hit.collider.transform.root.GetComponent<newLevelCrashed>().opponentAttacked("punch");



                            callOnce = false;
                        }
                    }
                }



                if (Physics.Raycast(leg.position + new Vector3(0.1f, 0, 0), Vector3.right, out hit, 0.1f, layerMask))
                {
                    //Debug.DrawRay (leg.position, Vector3.right*0.5f, Color.red);
                    //print (hit.collider.transform.root.name+ " "+ (hit.collider.transform.root.position.x-player.root.position.x));
                    if (hit.collider.transform.root.name.Contains("heavybike endless") && (hit.collider.transform.root.position.x - player.root.position.x <= 0.5f))
                    {//2.16
                        if (callOnce)
                        {

                            hit.collider.transform.root.GetComponent<newLevelCrashed>().opponentAttacked("kick");


                        }
                    }
                    if (hit.collider.transform.root.name.Contains("opponentFatguy") && (hit.collider.transform.root.position.x - player.root.position.x <= 0.5f))
                    {//2.16
                        if (callOnce)
                        {

                            hit.collider.transform.root.GetComponent<newLevelCrashed>().opponentAttacked("kick");


                        }
                    }

                }

            }





            if (leftPunchbool)
            {
                leftPunchbool = false;

                isAttacking = true;
                if (GetComponent<AudioSource>().enabled)
                {
                    GetComponent<AudioSource>().PlayOneShot(punchSound);
                    GetComponent<AudioSource>().PlayOneShot(grunt);
                }
                player.GetComponent<Animation>().Play(punch.name, PlayMode.StopAll);
                Invoke("punchOn", 1f);


            }




            if (leftAttackbool)
            {
                leftAttackbool = false;

                isAttacking = true;
                if (GetComponent<AudioSource>().enabled)
                {
                    GetComponent<AudioSource>().PlayOneShot(kickSound);
                    GetComponent<AudioSource>().PlayOneShot(grunt);
                }
                player.GetComponent<Animation>().Play(attack.name, PlayMode.StopAll);

                Invoke("attackOn", 0.92f);

            }


            if (stuntbool && !transform.root.GetComponent<weaponAI>().isAttacking)
            {

                if (heavyBikeTurns.boostbool)
                {
                    nitroLeft.gameObject.SetActive(false);
                    nitroRight.gameObject.SetActive(false);
                }
                isStunting = true;

                player.GetComponent<Animation>().Play(oneWheeling.name, PlayMode.StopAll);
                if (bikeSelection.bikeCount < 6)
                    Invoke("stuntOn", 3.33f);
                else if (bikeSelection.bikeCount >= 6)
                    Invoke("stuntOn", 2.8f);

                stuntbool = false;
            }





        }



    }




    public void stuntOn()
    {


        if (heavyBikeTurns.boostbool)
        {
            nitroLeft.gameObject.SetActive(true);
            nitroRight.gameObject.SetActive(true);
        }
        isStunting = false;
        if (SceneManager.GetActiveScene().name.Equals("level5"))
            //		player.root.GetComponent<findTarget>().startStunt=true;
            CameraSettings.rampstart = false;
        CameraSettings.rampstart2 = false;
        CameraSettings.rampstart3 = false;
        CameraSettings.jumpstart = false;
        CameraSettings.jumpstart2 = false;
        CameraSettings.jumpstart3 = false;


        score += 50;
        scorePopup.GetComponent<TextMesh>().text = "+50";
        scorePopup.GetComponent<TextMesh>().color = new Color(253, 246, 0, 255);
        Instantiate(scorePopup, player.position + new Vector3(0f, 2f, 6f), Quaternion.identity);//Quaternion.Euler (25,0, 0f));


    }



    void skipp()
    {
        skipClicked = false;
    }
    void punchOn()
    {



        scoreOnce = true;
        soundOnce = true;
        //	raycastpunch.collider.enabled=false;
        //	Destroy (raycastpunch.rigidbody);

        callOnce = true;
        isAttacking = false;
        leftPunchOnce = true;

    }
    void attackOn()
    {

        callOnce = true;
        scoreOnce = true;
        soundOnce = true;
        //raycastkick.collider.enabled=false;
        //Destroy (raycastkick.rigidbody);
        isAttacking = false;

        leftAttackOnce = true;


    }


    void unstablebike()
    {
        unstability = false;
        audioplayOnce = true;
        countonce = true;
        //player.animation.Play (race.name, PlayMode.StopAll);

    }





    int attackCount = 0;
    public AnimationClip survive, unstable;
    public AudioClip surviveSound;
    public void playerSurvivalDeath()
    {


        if (collideOnce)
        {
            if (transform.root.position.x <= 1f)
            {
                transform.root.position += new Vector3(0.15f, 0, 0);
            }
            if (transform.root.position.x <= 1.2f)
            {
                print(transform.localScale.x);
                if (transform.localScale.x >= 1.5f)
                {
                    Vector3 theScale = transform.localScale;
                    theScale.x *= -1;
                    transform.localScale = theScale;
                }
            }

            player.root.GetComponent<heavyBikeTurns>().counter = 3;
            heavyBikeTurns.isdead = true;
            nitroLeft.gameObject.SetActive(false);
            nitroRight.gameObject.SetActive(false);
            if (audioplayOnce)
            {
                GetComponent<AudioSource>().PlayOneShot(deathSound);
                audioplayOnce = false;
            }
            Invoke("shadowsOff", 3f);


            player.root.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            player.root.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            player.root.position = new Vector3(player.root.position.x, 0.1f, player.root.position.z);
            player.root.eulerAngles = new Vector3(0, 0, 0);


            player.root.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.root.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            player.GetComponent<Animation>()[survive.name].speed = 1.7f;
            player.GetComponent<Animation>().Play(survive.name, PlayMode.StopAll);
            revivalScreen();

            Time.timeScale = 1.0f;



            crash = true;
            isdead = true;
            lifeEnd = true;

            raceSound.SetActive(false);

            collideOnce = false;

        }

    }


    public void crashPlayer()
    {
        if (!isAttacking && !timeover && !levelClear)
        {
            if (!crash && !isdead)
            {
                if (!heavyBikeTurns.isShielded)
                {
                    if (countonce)
                    {
                        attackCount += 1;
                        player.root.GetComponent<heavyBikeTurns>().counter += 1;
                        countonce = false;

                    }
                    if (attackCount >= 3)
                    {
                        script.chances -= 1;
                        playerSurvivalDeath();
                    }
                    else if (attackCount != 3)
                    {
                        if (!crash)
                        {
                            unstability = true;
                            if (audioplayOnce)
                            {
                                if (GetComponent<AudioSource>().enabled)
                                {
                                    GetComponent<AudioSource>().PlayOneShot(hitSound);
                                    GetComponent<AudioSource>().PlayOneShot(hitgrunt);
                                }
                                audioplayOnce = false;
                            }
                            player.GetComponent<Animation>().Play(unstable.name, PlayMode.StopAll);
                            Invoke("unstablebike", unstable.length);
                        }
                    }






                }
            }


        }
    }



    public void crashPlayer(string weaponName)
    {
        if (!isAttacking && !timeover && !levelClear)
        {
            if (!crash && !isdead)
            {
                if (!heavyBikeTurns.isShielded)
                {
                    if (countonce)
                    {
                        attackCount += 1;
                        player.root.GetComponent<heavyBikeTurns>().counter += 1;
                        if (weaponName.Contains("bat"))
                        {
                            if (attackCount != 3 && attackCount != 6 && attackCount != 9)
                            {
                                attackCount += 1;
                                player.root.GetComponent<heavyBikeTurns>().counter += 1;
                            }
                        }
                        if (weaponName.Contains("axe"))
                        {
                            if (attackCount != 3 && attackCount != 6 && attackCount != 9)
                            {
                                attackCount += 2;
                                player.root.GetComponent<heavyBikeTurns>().counter += 1;
                            }
                        }
                        countonce = false;

                    }


                    if (attackCount >= 3)
                    {
                        script.chances -= 1;
                        playerSurvivalDeath();
                    }

                    else if (attackCount != 3)
                    {
                        if (!crash)
                        {
                            unstability = true;
                            if (audioplayOnce)
                            {
                                if (GetComponent<AudioSource>().enabled)
                                {
                                    GetComponent<AudioSource>().PlayOneShot(hitSound);
                                    GetComponent<AudioSource>().PlayOneShot(hitgrunt);
                                }
                                audioplayOnce = false;
                            }
                            player.GetComponent<Animation>().Play(unstable.name, PlayMode.StopAll);
                            Invoke("unstablebike", unstable.length);
                        }
                    }



                }
            }
        }



    }

    void crashOn()
    {
        tiltControl.playerCrashed = true;
        if (tiltControl.totalOppo < 10)
            tiltControl.totalOppo += 1;



        global_var.boostCounter -= 1;

        countonce = true;
        player.root.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeAll;
        player.root.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
        crash = false;
        player.root.GetComponent<heavyBikeTurns>().counter = 0;
        audioplayOnce = true;

        player.GetComponent<Animation>().Play(race.name, PlayMode.StopAll);
        raceSound.SetActive(true);
        if (heavyBikeTurns.boostbool)
        {
            nitroLeft.gameObject.SetActive(true);
            nitroRight.gameObject.SetActive(true);
        }



    }


    //



    void gameover()
    {
        GetComponent<AudioSource>().enabled = false;
        raceSound.GetComponent<AudioSource>().enabled = false;
        rampSound.GetComponent<AudioSource>().enabled = false;

        endlessmodeGraphics.gameMode = "GameOver";
        script.gameOverFtn();
    }

    void shadowsOn()
    {
        shadow.gameObject.SetActive(true);
    }
    void shadowsOff()
    {
        shadow.gameObject.SetActive(false);
    }

    public void playerDeath()
    {

        if (collideOnce)
        {


            heavyBikeTurns.isdead = true;
            player.root.GetComponent<heavyBikeTurns>().counter = 3;

            if (audioplayOnce)
            {
                if (GetComponent<AudioSource>().enabled)
                    GetComponent<AudioSource>().PlayOneShot(deathSound);
                audioplayOnce = false;
            }
            isStunting = false;
            player.root.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.root.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            nitroLeft.gameObject.SetActive(false);
            nitroRight.gameObject.SetActive(false);

            if (PlayerPrefs.GetInt("levels") != 11 && PlayerPrefs.GetInt("levels") != 12)
            {
                ragdoll = (Transform)Instantiate(ragdollprefab, player.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
                ragdoll.Rotate(0, 90, 0);
                ragdoll_rigid = ragdoll.Find("Bip01").transform;


                ragdoll.Find("biker_fat-01").transform.GetComponent<Renderer>().material.mainTexture = Resources.Load(biker) as Texture;


                //			ragdoll.active = true;
                ragdoll.position = new Vector3(ragdoll.position.x, player.position.y + 0.5f, player.position.z + 1f);


                GameObject.Find("Main Camera").GetComponent<camFollow>().target = ragdoll_rigid;
                GameObject.Find("Main Camera").GetComponent<camFollow>().player = ragdoll_rigid;

                player.Find("biker-v3").gameObject.SetActive(false);
                ragdoll_rigid.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 20);

                ragdoll_rigid.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 20), ForceMode.Force);//=-1*gameObject.rigidbody.velocity;

            }
            Time.timeScale = 0.5f;

            Invoke("revivalScreen", 1.7f);


            player.GetComponent<Animation>().Play(death.name, PlayMode.StopAll);
            //Invoke("gameover",death.length);
            isdead = true;
            lifeEnd = true;

            raceSound.SetActive(false);
            rampSound.SetActive(false);

            collideOnce = false;

        }

    }

    void revivalScreen()
    {
        endlessmodeGraphics.gameMode = "Revival";
        script.revivePlayer();
        GetComponent<AudioSource>().enabled = false;
        raceSound.GetComponent<AudioSource>().enabled = false;
    }
    public void colliding()
    {
        if (!timeover && !levelClear)
        {


            if (!crash && !isdead)
            {


                if (countonce)
                {
                    attackCount += 1;
                    player.root.GetComponent<heavyBikeTurns>().counter += 1;
                    countonce = false;

                }


                if (attackCount >= 3)
                {
                    script.chances -= 1;
                    playerDeath();
                }

                else if (attackCount != 3)
                {
                    if (!crash)
                    {
                        unstability = true;
                        if (audioplayOnce)
                        {
                            if (GetComponent<AudioSource>().enabled)
                            {

                                GetComponent<AudioSource>().PlayOneShot(hurdleHit);
                                GetComponent<AudioSource>().PlayOneShot(hitgrunt);
                            }
                            audioplayOnce = false;
                        }
                        player.GetComponent<Animation>().Play(unstable.name, PlayMode.StopAll);
                        Invoke("unstablebike", unstable.length);
                        CancelInvoke("setTime");
                        Invoke("setTime", 0.1f);
                        Time.timeScale = 0.3f;
                        Time.fixedDeltaTime = 0.006f;

                    }
                }



            }
        }
    }




    public void carCrash()
    {
        if (!timeover && !levelClear)
        {


            if (!crash && !isdead)
            {
                if (!heavyBikeTurns.isShielded)
                {
                    script.chances -= 1;
                    transform.root.GetComponent<heavyBikeTurns>().counter = 3;

                    attackCount = 3;
                    script.chances -= 1;
                    playerDeath();

                }
            }
        }
    }
    void setTime()
    {

        setTimeScale = true;


    }

    void deathOn()
    {
        if (tiltControl.totalOppo < 10)
            tiltControl.totalOppo += 1;

        tiltControl.playerCrashed = true;



        global_var.boostCounter -= 1;



        player.root.GetComponent<heavyBikeTurns>().flyoverStart = false;
        if (heavyBikeTurns.boostbool)
        {
            nitroRight.gameObject.SetActive(true);
            nitroLeft.gameObject.SetActive(true);
        }


        skipClicked = true;
        Invoke("skipp", 1f);
        collideOnce = true;

        shadow.gameObject.SetActive(false);
        countonce = true;
        crash = false;
        //	counter = 0;
        audioplayOnce = true;


        crash = false;
        player.root.GetComponent<heavyBikeTurns>().counter = 0;
        player.root.GetComponent<heavyBikeTurns>().jump = false;

        CameraSettings.rampstart = false;
        CameraSettings.rampstart2 = false;
        CameraSettings.rampstart3 = false;
        CameraSettings.jumpstart = false;
        CameraSettings.jumpstart2 = false;
        CameraSettings.jumpstart3 = false;

        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        player.root.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeAll;
        player.root.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
        if (SceneManager.GetActiveScene().name.Equals("level5"))
        {
            //player.root.GetComponent<findTarget> ().isJumping = true;

            player.localPosition = new Vector3(0, 0f, 0);
            player.root.position = new Vector3(2.2f, -0.01882806f, heavyBikeTurns.hurdlePosition + 130f);
        }




        player.GetComponent<Animation>().Play(race.name, PlayMode.StopAll);
        raceSound.SetActive(true);



        StartCoroutine("flash");

    }



    IEnumerator flash()
    {
        transform.Find("biker-v3").GetComponent<Renderer>().enabled = false;
        transform.Find("bike_body").GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.05f);
        transform.Find("biker-v3").GetComponent<Renderer>().enabled = true;
        transform.Find("bike_body").GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(0.05f);
        transform.Find("biker-v3").GetComponent<Renderer>().enabled = false;
        transform.Find("bike_body").GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.05f);
        transform.Find("biker-v3").GetComponent<Renderer>().enabled = true;
        transform.Find("bike_body").GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(0.05f);
        transform.Find("biker-v3").GetComponent<Renderer>().enabled = false;
        transform.Find("bike_body").GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.05f);
        transform.Find("biker-v3").GetComponent<Renderer>().enabled = true;
        transform.Find("bike_body").GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(0.05f);
        transform.Find("biker-v3").GetComponent<Renderer>().enabled = false;
        transform.Find("bike_body").GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.05f);
        transform.Find("biker-v3").GetComponent<Renderer>().enabled = true;
        transform.Find("bike_body").GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(0.05f);
        transform.Find("biker-v3").GetComponent<Renderer>().enabled = false;
        transform.Find("bike_body").GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.05f);
        transform.Find("biker-v3").GetComponent<Renderer>().enabled = true;
        transform.Find("bike_body").GetComponent<Renderer>().enabled = true;

    }
}
