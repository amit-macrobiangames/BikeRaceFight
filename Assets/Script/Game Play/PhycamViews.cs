using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhycamViews : MonoBehaviour
{
    public static PhycamViews instance;
    public Transform tiltPanel;
    public Text speedUI;
    public Transform needle2, needle1, heavyPanel, harleyPanel;
    public GameObject timeSlowPanel;
    public static bool slowBundleOpened;
    public GameObject timerBG;
    public Image timerFG;
    public heavyBikeTurns parentBike;
    bool crash, isdead;
    camFollow script;
    public GameObject leftCam, rightCam;
    public static int counter;
    //int totalViews;
    public Transform player, camTarget, childPlayer;
    public Texture cockpit, helmetView, needle, fatguyCockpit, tallguyCockpit;
    float rotationAngle;
    public RenderTexture leftCamView, rightCamView;
    public Material leftMaterial, rightMaterial;
    public GUIStyle labelStyle;
    float speedFactor;
    float rotation;

    bool boostOn;
    public static bool slowingDown;


    public GUIStyle slowCam, limit;

    Texture2D t;
    Color currentBlendColor;
    Color blueColor;
    Color orangeColor;


    public float speedRange, highLimit;
    float tiltLimit;
    int levelID;
    turnLevelcontrols harleyBikescript;
    heavyBikeTurnControls heavyBikeScript;
    public GameObject healthBar;
    int randomCameras;
    public newLevelHarley leftbike;
    public newLevelHarley rightbike;
    public bool deathThroughFire;
    bool isSlowed;
    public bool levelClear;
    int level;
    float cameraSwitchTime;
    public static float updatedValue, startedValue;
    void Start()
    {
        instance = this;

        startedValue = PlayerPrefs.GetInt("timers");
        if (startedValue == 0)
        {
            timerFG.fillAmount = 0;
        }
        updatedValue = 0.0f;

        slowBundleOpened = false;
        level = PlayerPrefs.GetInt("levels");

        if (level == 6)
            cameraSwitchTime = 2.0f;
        else
            cameraSwitchTime = 6.0f;
        levelClear = false;
        childPlayer = player.root.GetComponent<heavyBikeTurns>().player.transform;
        slowingDown = false;

        deathThroughFire = false;

        boostOn = false;

        counter = 2;
        //	totalViews = 12;//27;
        script = transform.GetComponent<camFollow>();
        labelStyle.fontSize = System.Convert.ToInt32(Screen.width * 0.05f);

        speedFactor = 0;

        speedRange = 160f;
        highLimit = 185f;

        t = new Texture2D(1, 1);
        orangeColor = new Color(0.88f, 0.51f, 0.05f, 0.3f);//new Color (0.88f, 0.51f, 0.05f, 0.3f);

        blueColor = new Color(0.29f, 0.5f, 0.99f, 0.2f); // Opaque red  //0.88


        leftCam = player.root.GetComponent<heavyBikeTurns>().player.transform.Find("Bone01").Find("Cube").gameObject;
        rightCam = player.root.GetComponent<heavyBikeTurns>().player.transform.Find("Bone01").Find("leftCube").gameObject;



        //counter = 11;
        //changeCamView ();


    }


    void startCameras()
    {

        counter = 1;
        changeCamView();

    }

    public RenderTexture harleyLeft, harleyRight;
    public Material harleyLeftMat, harleyRightMat;

    void Awake()
    {


        if (bikeSelection.bikeCount == 2 || bikeSelection.bikeCount == 3 || bikeSelection.bikeCount == 5 || bikeSelection.bikeCount == 6 || bikeSelection.bikeCount == 7 || bikeSelection.bikeCount == 8)
        {

            cockpit = fatguyCockpit;
            //			rightCamView=Resources.Load("harleyLeft") as RenderTexture;
            //			leftCamView=Resources.Load("harleyRight") as RenderTexture;
            //			
            //			leftMaterial=Resources.Load("harleyLeft 1") as Material;
            //			rightMaterial=Resources.Load("harleyRight 1") as Material;


            rightCamView = harleyLeft;
            leftCamView = harleyRight;

            leftMaterial = harleyLeftMat;
            rightMaterial = harleyRightMat;
            levelID = 0;
            harleyBikescript = player.root.Find("playerFatguy").GetComponent<turnLevelcontrols>();
        }
        if (bikeSelection.bikeCount == 1 || bikeSelection.bikeCount == 4 || bikeSelection.bikeCount == 9 || bikeSelection.bikeCount == 10)
        {
            cockpit = tallguyCockpit;
            levelID = 1;
            heavyBikeScript = player.root.Find("Player").GetComponent<heavyBikeTurnControls>();
        }


        leftCam.SetActive(false);
        rightCam.SetActive(false);
    }

    void LateUpdate()
    {
        if (levelID == 0)
        {
            isdead = turnLevelcontrols.isdead;
            crash = harleyBikescript.crash;
            levelClear = turnLevelcontrols.levelClear;

        }
        else if (levelID == 1)
        {
            isdead = heavyBikeTurnControls.isdead;
            crash = heavyBikeScript.crash;
            levelClear = heavyBikeTurnControls.levelClear;
        }
        if (handleArrow.start && !isdead && !crash && tiltControl.startTiltAfterCam && !levelClear)
        {
            timerBG.SetActive(true);
        }
        else
        {
            timerBG.SetActive(false);
        }
        if (crash || isdead)
        {
            counter = 2;
            changeCamView();

        }
        if (slowingDown)
        {
            boostOn = true;
            isSlowed = true;
            //	Camera.main.fieldOfView-= 0.1f;
            currentBlendColor = blueColor;
            healthBar.SetActive(false);



        }


        if (heavyBikeTurns.boostbool)
        {
            //		if (player.GetComponent<endlessTallguyControl> ().boostbool) {
            speedRange = 190f;
            highLimit = 240f;
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
            boostOn = true;
            currentBlendColor = orangeColor;
            if (counter != 1 && counter != 2)
            {
                counter = 2;
                changeCamView();
            }

            if (counter == 1)
            {
                //				script.intendedHaight =Mathf.Lerp(script.intendedHaight,0.7f,2f*Time.deltaTime);
                //				script.intendedDistance =Mathf.Lerp(script.intendedDistance,2f,2f*Time.deltaTime);



            }
            else if (counter == 2)
            {
                //				script.intendedHaight =Mathf.Lerp(script.intendedHaight,0.45f,2f*Time.deltaTime);
                //				script.intendedDistance =Mathf.Lerp(script.intendedDistance,2f,2f*Time.deltaTime);





            }



        }
        if (!heavyBikeTurns.boostbool && !slowingDown)
        {
            currentBlendColor = Color.clear;
            healthBar.SetActive(true);
            speedRange = 160f;
            highLimit = 185f;


            if (boostOn)
            {

                currentBlendColor = Color.clear;
                healthBar.SetActive(true);
                speedRange = 160f;
                highLimit = 185f;

                //randomCameras = Random.Range (0, 10);
                if (isSlowed)
                {
                    if (randomCameras % 2 == 0)
                        counter = 1;
                    else
                        counter = 2;

                    changeCamView();
                    boostOn = false;
                    isSlowed = false;
                }


                if (counter == 1)
                {
                    script.intendedHaight = Mathf.Lerp(script.intendedHaight, 0.6f, 1.5f * Time.deltaTime);
                    script.intendedDistance = Mathf.Lerp(script.intendedDistance, 1.3f, 1.5f * Time.deltaTime);

                    if ((script.intendedDistance >= 1.29f && script.intendedDistance <= 1.31f) && (script.intendedHaight >= 0.59f && script.intendedHaight <= 0.61f))
                    {
                        boostOn = false;
                        script.lerpTime = 0.25f;
                    }
                }
                else if (counter == 2)
                {
                    //					print ("counter==2");
                    script.intendedHaight = Mathf.Lerp(script.intendedHaight, 0.35f, 1.5f * Time.deltaTime);
                    script.intendedDistance = Mathf.Lerp(script.intendedDistance, 1.1f, 1.5f * Time.deltaTime);

                    if ((script.intendedDistance >= 1f && script.intendedDistance <= 1.2f) && (script.intendedHaight >= 0.34f && script.intendedHaight <= 0.36f))

                    //	if(script.intendedDistance==5.75f && script.intendedHaight == 2.25f)
                    {
                        boostOn = false;
                        script.lerpTime = 0.25f;
                    }
                }

            }
        }
        if (handleArrow.start)
        {

            speedFactor = player.root.GetComponent<Rigidbody>().velocity.magnitude * 7.5f;

        }
    }




    public void showCockpitView()
    {
        tiltPanel.gameObject.SetActive(true);
        if (bikeSelection.bikeCount == 1)
        {
            heavyPanel.gameObject.SetActive(true);
        }
        else
        {
            harleyPanel.gameObject.SetActive(true);

        }

    }
    //string limitText="LimitOn";
    //	bool limitTilt=true;
    void OnGUI()
    {


        if (handleArrow.start && !isdead && !crash && tiltControl.startTiltAfterCam && !levelClear)
        {

            //print (GUI.color);
            GUI.color = currentBlendColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), t);
            GUI.color = Color.white;

            //print (player.FindChild ("biker-v3").renderer.material.color.a);
            if (endlessmodeGraphics.gameMode.Equals("Idle") && handleArrow.start && !isdead && !crash)
            {

                GUI.depth = 5;



                if (GUI.Button(new Rect(Screen.width * 0.01f, Screen.height - Screen.width * .35f, Screen.width * 0.12f, Screen.width * 0.12f), "", labelStyle))
                {

                    if (!levelClear && !parentBike.flyoverStart && !parentBike.isJumping && endlessmodeGraphics.gameMode.Equals("Idle"))
                    {

                        /* 
                                                if (!slowingDown && !heavyBikeTurns.boostbool && !weaponAI.boostBundleOpened && !weaponAI.ammoBundleOpened && !weaponAI.shieldBundleOpened && !weaponAI.missileBundleOpened)
                                                {
                                                    if (PlayerPrefs.GetInt("timers") > 0)
                                                    {

                                                        PlayerPrefs.SetInt("boost/timerUsed", (PlayerPrefs.GetInt("boost/timerUsed") + 1));
                                                        if (counter == 11 || counter == 12)
                                                        {

                                                        }
                                                        updatedValue = (((100f / startedValue) / 100f));
                                                        timerFG.fillAmount -= updatedValue;

                                                        PlayerPrefs.SetInt("timers", (PlayerPrefs.GetInt("timers") - 1));
                                                        PlayerPrefs.Save();
                                                        if (Time.timeScale == 1f)
                                                        {

                                                            counter = 9;

                                                            slowDownCamera();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Time.timeScale = 0.0f;
                                                        if (PlayerPrefs.GetInt("SoundOff") == 0)
                                                        {
                                                            AudioListener.volume = 0.0f;
                                                        }
                                                        slowBundleOpened = true;
                                                        timeSlowPanel.SetActive(true);
                                                    }
                                                } */
                    }
                }
                //				if (counter == 11) {
                //				
                //					if (bikeSelection.bikeCount == 1) {
                //						
                //						
                //						
                //						#if UNITY_ANDROID
                //						rotationAngle = Input.acceleration.x * 6f;
                //						#endif
                //						
                //						
                //						#if UNITY_EDITOR
                //						rotationAngle = Input.GetAxis ("Horizontal") * 2.5f;
                //						#endif
                //

                //						//tiltPanel.
                //						//GUI.BeginGroup (new Rect (Screen.width * 0.0f, (Screen.height * 0.44f), Screen.width * 1f, Screen.width * 1f * 596 / 1024));
                //						GUI.BeginGroup (new Rect (Screen.width * 0.0f, (Screen.height * 0.44f), Screen.width * 1f, Screen.height * 1f ));
                //
                //						GUIUtility.RotateAroundPivot (rotationAngle, new Vector2 (Screen.width / 2, (Screen.height)));
                //						GUI.DrawTexture (new Rect (0,Screen.height*.03f, Screen.width * 1f, (Screen.width * 1f * 479 / 1024)), cockpit);
                //						
                //						Graphics.DrawTexture (new Rect (Screen.width * 0.775f, Screen.height * 0.03f, Screen.width * 0.215f, Screen.width * 0.22f), leftCamView, leftMaterial);	
                //						Graphics.DrawTexture (new Rect (Screen.width * 0.01f, Screen.height * 0.02f, Screen.width * 0.215f, Screen.width * 0.22f), rightCamView, rightMaterial);
                //						GUI.Label (new Rect ((Screen.width * 0.358f), (Screen.height * 0.32f), Screen.width * 0.2f, Screen.width * 0.2f), Mathf.Round (rotation).ToString (), labelStyle);	
                //						
                //						//rotation = Mathf.Lerp (0, highLimit, speedFactor);
                //						
                //						
                //						//if(rotation<highLimit)
                //						rotation = speedFactor;
                //						if (rotation >= highLimit) {
                //							
                //							rotation = highLimit;
                //						}
                //						
                //						
                //						
                //						GUI.matrix = Matrix4x4.identity;
                //						GUIUtility.RotateAroundPivot ((rotation), new Vector2 ((Screen.width * 0.48f), (Screen.height * 0.299f))); //new Vector2((Screen.width*0.485f),(Screen.height*0.23f)));
                //						GUIUtility.RotateAroundPivot (rotationAngle, new Vector2 (Screen.width / 2, Screen.height));
                //						GUI.DrawTexture (new Rect ((Screen.width * 0.48f), (Screen.height * 0.299f), Screen.width * 0.004f, Screen.width * 0.004f * 99 / 10), needle);	//0.458//0.29
                //						GUI.EndGroup ();
                //						
                //					
                //						
                //						
                //						
                //						
                //					} else {
                //						
                //						
                //						#if UNITY_ANDROID
                //						rotationAngle = Input.acceleration.x * 6f;
                //						#endif
                //						
                //						
                //						#if UNITY_EDITOR
                //						rotationAngle = Input.GetAxis ("Horizontal") * 2.5f;
                //						#endif
                //						
                //						
                //						
                //						
                //						
                //						GUI.BeginGroup (new Rect (Screen.width * 0.0f, (Screen.height * 0.35f), Screen.width * 1f, Screen.width * 1f * 596 / 1024));
                //						GUIUtility.RotateAroundPivot (rotationAngle, new Vector2 (Screen.width / 2, Screen.height));
                //					
                //						GUI.DrawTexture (new Rect (0, 0, Screen.width * 1f, Screen.width * 1f * 438 / 1024), cockpit);
                //						
                //						Graphics.DrawTexture (new Rect (Screen.width * 0.8675f, Screen.height * 0.01f, Screen.width * 0.126f, Screen.width * 0.126f * 143 / 214), leftCamView, leftMaterial);	
                //						Graphics.DrawTexture (new Rect (Screen.width * 0.007f, Screen.height * 0.05f, Screen.width * 0.126f, Screen.width * 0.126f * 143 / 214), rightCamView, rightMaterial);
                //
                //						rotation = Mathf.Lerp (20, highLimit, speedFactor);
                //						rotation = speedFactor;
                //						if (rotation >= highLimit)
                //							rotation = highLimit;
                //						GUI.matrix = Matrix4x4.identity;
                //						
                //						
                //						
                //						GUIUtility.RotateAroundPivot ((rotation), new Vector2 ((Screen.width * 0.492f), (Screen.height * 0.6f))); 
                //						GUIUtility.RotateAroundPivot (rotationAngle, new Vector2 (Screen.width / 2, Screen.height));
                //
                //
                //
                //						GUI.DrawTexture (new Rect ((Screen.width * 0.4875f), (Screen.height * 0.6f), Screen.width * 0.004f, Screen.width * 0.004f * 52 / 9), needle);	
                //
                //						
                //						GUI.EndGroup ();
                //						
                //					}
                //					
                //				}
                //				if (counter == 12) {
                //					
                //					#if UNITY_ANDROID
                //					rotationAngle = Input.acceleration.x * 4f;
                //					#endif
                //					
                //					
                //					#if UNITY_EDITOR
                //					rotationAngle = Input.GetAxis ("Horizontal") * 1f;
                //					#endif
                //					
                //					GUIUtility.RotateAroundPivot (rotationAngle, new Vector2 (Screen.width / 2, Screen.height / 2));
                //					GUI.DrawTexture (new Rect ((Screen.width * 0.0f - 100), ((Screen.height * 0) - 100), Screen.width + 125, Screen.height + 125), helmetView);	
                //					
                //				}	

            }
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.B))
        {
            SlowMotion();
        }
    }
    void adjustCam()
    {
        slowingDown = false;
        //		print ("inside");
        currentBlendColor = Color.clear;


        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }


    void slowDownCamera()
    {
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = 0.004f;
        slowingDown = true;
        Invoke("adjustCam", 2.0f);
    }
    public void changeCamView()
    {
        //				if (Application.isEditor)
        //						counter = 11;
        if (!levelClear)
        {
            if (counter != 14 && counter != 15 && counter != 16 && counter != 17)
            {
                script.intendedBackAngle = 0f;
            }
            if (counter != 14 && !isdead)
            {
                script.target = player;
            }
            if (counter != 11)
            {

                leftCam.SetActive(false);
                rightCam.SetActive(false);
            }
            if (counter == 11 || counter == 12)
            {
                healthBar.SetActive(false);
            }
            else
            {
                healthBar.SetActive(true);
            }
            if (counter == 1)
            {

                script.smooth = 0.3f;
                script.lerpTime = 0.25f;
                script.intendedHaight = 0.350f;
                script.intendedDistance = 1.10f;
                script.intendedxPosition = 0f;
                script.Angle = 20.3678f;
                //			}
            }
            else if (counter == 2)
            {
                script.smooth = 0.3f;
                script.intendedHaight = 0.35f;
                script.intendedDistance = 1.1f;
                script.intendedxPosition = 0f;
                script.Angle = 20.3678f;
            }
            //		else if(counter==3)
            //		{
            //			script.intendedHaight=-0.41f;
            //			script.intendedDistance=-0.57f;
            //			script.intendedxPosition=1f;
            //			script.Angle=0f;
            //		}
            //		else if(counter==4)
            //		{
            //			script.intendedHaight=0.8f;
            //			script.intendedDistance=2.96f;
            //			script.intendedxPosition=-1.25f;
            //			script.Angle=10f;
            //		}
            else if (counter == 5)
            {
                script.intendedHaight = 1.25f;//8
                script.intendedDistance = 1.75f;//12.5
                script.intendedxPosition = 0f;
                script.Angle = 31f;
            }
            else if (counter == 6)
            {
                script.intendedHaight = 0.3f;//8
                script.intendedDistance = 0.7f;//12.5
                script.intendedxPosition = 0f;
                script.Angle = 20f;
            }
            //		else if(counter==7)
            //		{
            //			script.intendedBackAngle=0f;
            //			script.intendedHaight=-0.35f;//8
            //			script.intendedDistance=1.21f;//12.5
            //			script.intendedxPosition=0.56f;
            //			script.Angle=8.29f;
            //		}
            //		
            //		else if(counter==8)
            //		{
            //			script.intendedHaight=0.7f;//8
            //			script.intendedDistance=0.31f;//12.5
            //			script.intendedxPosition=1.25f;
            //			script.Angle=10f;
            //		}

            else if (counter == 9)
            {
                script.intendedHaight = 2f;//8
                script.intendedDistance = 1.5f;//12.5
                script.intendedxPosition = 0.5f;
                script.Angle = 45f;
            }

            //		else if(counter==10)
            //		{
            //			script.intendedHaight=-0.36f;//8
            //			script.intendedDistance=-1.33f;//12.5
            //			script.intendedxPosition=0.43f;
            //			script.Angle=5f;
            //		}
            //		

            //			else if (counter == 12) {
            //				
            //				if(level==3 || level==4)
            //				{
            //					script.intendedHaight = 0.2f;//8
            //					script.intendedDistance = -0.125f;//12.5
            //					script.intendedxPosition = 0f;
            //					script.Angle = -5.5f;
            //				}
            //				else
            //				{
            //					
            //					
            //					script.intendedHaight = 0.6f;//8
            //					script.intendedDistance = .05f;//12.5
            //					script.intendedxPosition = 0f;
            //					script.Angle = -5.5f;
            //				}
            //			} else if (counter == 11) {
            //				if(level==3 || level==4)
            //				{
            //					leftCam.SetActive (true);
            //					rightCam.SetActive (true);
            //					script.intendedHaight = 0.25f;//1.95
            //					script.intendedDistance = -0.125f;//-1.26
            //					script.intendedxPosition = 0f;
            //					if(childPlayer.name.Contains("Fat"))
            //						script.Angle = 0f;//10
            //					else
            //						script.Angle = 10f;//10
            //				}
            //				else
            //				{
            //					leftCam.SetActive (true);
            //					rightCam.SetActive (true);
            //					script.intendedHaight = 0.6f;//1.95
            //					script.intendedDistance = 0.05f;//-1.26
            //					script.intendedxPosition = 0f;
            //					script.Angle = 10f;
            //				}
            //			}
            //		else if(counter==13)
            //		{
            //			
            //			
            //			script.intendedHaight=0.65f;//8
            //			script.intendedDistance=3.0f;//12.5
            //			script.intendedxPosition=-1.42f;
            //			script.Angle=0f;
            //		}
            else if (counter == 14)
            {
                script.target = camTarget;
                script.intendedBackAngle = 90f;
                script.intendedHaight = -1f;//8
                script.intendedDistance = 1f;//12.5
                script.intendedxPosition = 0f;
                script.Angle = 30f;
            }
            else if (counter == 15)
            {
                script.intendedBackAngle = 180f;
                script.intendedHaight = -0.07f;//8
                script.intendedDistance = 1.25f;//12.5
                script.intendedxPosition = 0f;
                script.Angle = 20f;

                levelClear = true;
            }
            //		else if(counter==16)
            //		{
            //			script.intendedBackAngle=120f;
            //			script.intendedHaight=-1.61f;//8
            //			script.intendedDistance=9.49f;//12.5
            //			script.intendedxPosition=0f;
            //			script.Angle=20f;
            //		}
            else if (counter == 17)
            {
                //Time.timeScale=0.3f;
                //				Time.fixedDeltaTime=0.006f;
                //script.lerpTime=0.1f;
                script.smooth = 1.3f;
                //script.lerpTime=0.05f;
                script.intendedBackAngle = -20f;
                script.intendedHaight = 0.05f;//8
                script.intendedDistance = 0.4f;//12.5
                script.intendedxPosition = 0.25f;
                script.Angle = 10f;


                //				script.intendedBackAngle=-20f;
                //				script.intendedHaight=-0.05f;//8
                //				script.intendedDistance=0.4f;//12.5
                //				script.intendedxPosition=0.25f;
                //				script.Angle=10f;
            }
            else if (counter == 18)
            {

                //	Time.timeScale=0.3f;
                //				Time.fixedDeltaTime=0.006f;
                //script.lerpTime=0.05f;
                script.smooth = 1.3f;
                script.intendedBackAngle = -40f;
                script.intendedHaight = 0.03f;//8
                script.intendedDistance = 0.4f;//12.5
                script.intendedxPosition = 0.25f;
                script.Angle = 10f;


                //				script.intendedBackAngle=-20f;
                //				script.intendedHaight=-0.05f;//8
                //				script.intendedDistance=0.4f;//12.5
                //				script.intendedxPosition=0.25f;
                //				script.Angle=10f;
            }
            else if (counter == 25)
            {

                //Time.timeScale=0.3f;
                //				Time.fixedDeltaTime=0.006f;


                script.smooth = 1.3f;
                //script.lerpTime=0.05f;
                //script.lerpTime=0.1f;
                script.intendedBackAngle = 30f;
                script.intendedHaight = 0.02f;//8
                script.intendedDistance = 0.4f;//12.5
                script.intendedxPosition = -0.25f;
                script.Angle = 10f;


                //				script.intendedBackAngle=-20f;
                //				script.intendedHaight=-0.05f;//8
                //				script.intendedDistance=0.4f;//12.5
                //				script.intendedxPosition=0.25f;
                //				script.Angle=10f;
            }
            //		else if(counter==18)
            //		{
            //			script.intendedBackAngle=0f;
            //			script.intendedHaight=-0.32f;//8
            //			script.intendedDistance=1.25f;//12.5
            //			script.intendedxPosition=0.65f;
            //			script.Angle=8.29f;
            //		}
            else if (counter == 19)
            {
                script.intendedBackAngle = 150f;
                script.intendedHaight = -0.1f;//8
                script.intendedDistance = 1.5f;//12.5
                script.intendedxPosition = 0f;
                script.Angle = 15f;

                levelClear = true;
            }
            else if (counter == 20)
            {
                script.intendedBackAngle = 220f;
                script.intendedHaight = -0.15f;//8
                script.intendedDistance = 1.5f;//12.5
                script.intendedxPosition = 0f;
                script.Angle = 20f;

                levelClear = true;
            }
            //		else if(counter==20)
            //		{
            //			script.intendedBackAngle=180f;
            //			script.intendedHaight=-0.34f;//8
            //			script.intendedDistance=3.84f;//12.5
            //			script.intendedxPosition=-0.64f;
            //			script.Angle=10f;
            //		}
            //		else if(counter==21)
            //		{
            //			script.intendedBackAngle=180f;
            //			script.intendedHaight=-0.85f;//8
            //			script.intendedDistance=5.0f;//12.5
            //			script.intendedxPosition=0f;
            //			script.Angle=28f;
            //		}
            //		else if(counter==22)
            //		{
            //			script.intendedBackAngle=190f;
            //			script.intendedHaight=-0.14f;//8
            //			script.intendedDistance=3.73f;//12.5
            //			script.intendedxPosition=-0.9f;
            //			script.Angle=0f;
            //		}
            //		else if(counter==23)
            //		{
            //			script.intendedBackAngle=160f;
            //			script.intendedHaight=-0.05f;//8
            //			script.intendedDistance=4.29f;//12.5
            //			script.intendedxPosition=-0.65f;
            //			script.Angle=0f;
            //		}
            //		else if(counter==24)
            //		{
            //			script.intendedBackAngle=180f;
            //			script.intendedHaight=-0.14f;//8
            //			script.intendedDistance=3.65f;//12.5
            //			script.intendedxPosition=0.0f;
            //			script.Angle=0f;
            //		}
            //		else if(counter==25)
            //		{
            //			script.intendedBackAngle=130f;
            //			script.intendedHaight=0.29f;//8
            //			script.intendedDistance=4.26f;//12.5
            //			script.intendedxPosition=0.6f;
            //			script.Angle=-13.93f;
            //		}
            //		else if(counter==26)
            //		{
            //			script.intendedBackAngle=136.51f;
            //			script.intendedHaight=0.8f;//8
            //			script.intendedDistance=6.2f;//12.5
            //			script.intendedxPosition=0.0f;
            //			script.Angle=-12.7f;
            //		}
            //		else if(counter==27)
            //		{
            //			script.intendedBackAngle=180f;
            //			script.intendedHaight=1.25f;//8
            //			script.intendedDistance=7f;//12.5
            //			script.intendedxPosition=0.0f;
            //			script.Angle=32.2f;
            //		}
        }
    }


    public void oppoCrashCam()
    {
        //endlessmodeGraphics.crashedOpponent+=1;
        if (!deathThroughFire && !player.root.GetComponent<heavyBikeTurns>().flyoverStart)
        {
            heavyBikeTurns.jumpAnim = true;
            childPlayer.GetComponent<Animation>()["waveHand"].speed = 1.7f;
            player.root.GetComponent<heavyBikeTurns>().isJumping = true;

            childPlayer.GetComponent<Animation>().Play("waveHand", PlayMode.StopAll);


            Invoke("changeBack", 0.5f);//0.75

            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = 0.004f;
            //healthBar.SetActive (false);
            //slowingDown = true;
        }
    }


    void changeBack()
    {

        CancelInvoke("adjustCam");

        StartCoroutine("cam11View");
        deathThroughFire = false;
        //heavyBikeTurns.jumpAnim=false;
        //player.root.GetComponent<heavyBikeTurns> ().isJumping=false;

    }



    IEnumerator cam11View()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        yield return new WaitForSeconds(0.1f);
        heavyBikeTurns.jumpAnim = false;
        //	print ("camview1 : "+player.root.GetComponent<heavyBikeTurns> ().flyoverStart);
        if ((leftbike.itselfCrashed || !leftbike.FollowPlayer) && (rightbike.itselfCrashed || !rightbike.FollowPlayer) && !player.root.GetComponent<heavyBikeTurns>().flyoverStart)
        {


            if (counter != 11 && counter != 12)
            {
                randomCameras = Random.Range(0, 10);
                if (randomCameras % 2 == 0)
                    counter = 11;
                else
                    counter = 12;
                changeCamView();
            }
            yield return new WaitForSeconds(cameraSwitchTime);
            //	print ("insidde");
            player.root.GetComponent<heavyBikeTurns>().isJumping = false;
            counter = 2;
            changeCamView();

        }
        else
        {

            player.root.GetComponent<heavyBikeTurns>().isJumping = false;
            counter = 2;
            changeCamView();
        }
    }
    public void SlowMotion()
    {

        if (endlessmodeGraphics.gameMode.Equals("Idle") && handleArrow.start && !isdead && !crash)
        {
            if (!levelClear && !parentBike.flyoverStart && !parentBike.isJumping && endlessmodeGraphics.gameMode.Equals("Idle"))
            {
                if (!slowingDown && !heavyBikeTurns.boostbool)
                {
                    if (PlayerPrefs.GetInt("timers") >= 0)
                    {

                        PlayerPrefs.SetInt("boost/timerUsed", (PlayerPrefs.GetInt("boost/timerUsed") + 1));
                        if (counter == 11 || counter == 12)
                        {

                        }
                        updatedValue = (((100f / startedValue) / 100f));
                        timerFG.fillAmount -= updatedValue;

                        PlayerPrefs.SetInt("timers", (PlayerPrefs.GetInt("timers") - 1));
                        PlayerPrefs.Save();
                        if (Time.timeScale == 1f)
                        {

                            counter = 9;

                            slowDownCamera();
                        }
                    }
                    else
                    {
                        Time.timeScale = 0.0f;
                        if (PlayerPrefs.GetInt("SoundOff") == 0)
                        {
                            AudioListener.volume = 0.0f;
                        }
                        slowBundleOpened = true;
                        timeSlowPanel.SetActive(true);
                    }
                }
            }


        }


    }

    public void closeTimerBundle()
    {
        timeSlowPanel.SetActive(false);
        slowBundleOpened = false;

        Time.timeScale = 1;
        startedValue = PlayerPrefs.GetInt("timers");
        if (PlayerPrefs.GetInt("SoundOff") == 0)
        {
            AudioListener.volume = 1.0f;
        }
    }

}
