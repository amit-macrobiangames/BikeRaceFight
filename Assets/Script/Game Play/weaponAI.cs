using UnityEngine;
using System.Collections;
using Xft;
using UnityEngine.UI;
public class weaponAI : MonoBehaviour
{
    //Added
    public Image shieldFG,ammoFG;
    public static float shieldStartV,shieldUpdateV,ammoStartV,ammoUpdateV; 

    public Image bulletBtnBG;
    public Sprite bulletBtn, missileBtn;
    public Text carHitText, totalCoinsText;
    float missileAnimLength;
    public WeaponLauncher missileControl;
    public static bool boostBundleOpened, ammoBundleOpened, shieldBundleOpened, missileBundleOpened;
    public GameObject boostPanel, ammoPanel, shieldPanel, missilePanel, cashBg;
    public Text bullets;
    public GameObject canvas;
    public Image axeLeft, axeRight;
    public Sprite axeLeftSprite, axeRightSprite, batLeftSprite, batRightSprite;

    public heavyBikeTurns parentBike;
    public GameObject boostBG;
    public Image boostFG;
    bool levelClear;
    public PhycamViews mainCamera;
    public boostercollision booster;
    public float lane1, lane2, lane3, lane4, lane5;
    public Transform leftBike, rightBike;
    public XWeaponTrail SimpleTrail, batLeftTrail, axeRightTrail, batRightTrail;
    public Transform leftRaycast, rightRaycast;
    //	public GameObject leftTrail,rightTrail;
    public bool isAttacking;
    public GameObject tallguy, fatguy, tallWeaponised, fatWeaponised, normalPlayer, weaponisedPlayer, shotgunNormal, shotGunWeaponised, shotgunInner, batNormal, axeNormal, batWeaponised, axeWeaponised, axeLeftInner, axeRightInner, batLeftInner, batRightInner, missileNormal, missileWeaponised, missileInner;
    public AnimationClip batRight, batLeft;
    bool BatLeftAttack, BatRightAttack, batLeftOnce, batRightOnce, axeLeftAttack, axeRightAttack, axeLeftOnce, axeRightOnce, isPistolAttack, isShotgunAttack, pistolAttackOnce, shotgunAttackOnce, missileOnce;
    Rect rightBatRect, rightAxeRect, leftAxeRect, pistolRect;//,shotgunRect,leftBatRect;
    bool crash, isdead, unstable;
    public AudioClip axeSound, batSound, missileSound;
    bool missileAttack;

    public GameObject pistolNormal, pistolWeaponised, pistolInner;
    public AnimationClip pistol, shotgun, missile;
    public GUIStyle labelStyle;

    public WeaponLauncher pistolControl, shotgunControl;
    // Use this for initialization

    turnLevelcontrols harleyBikescript;
    heavyBikeTurnControls heavyBikeScript;

    int bikeID;
    int layerMask;
    //Rect leftRect,rightRect,kickRect,punchRect,leftBatTextureRect,rightBatTextureRect,pistolTextureRect,boostTextureRect,timerRect;
    //	public Texture leftTexture,rightTexture,kickTexture,punchTexture,leftAxeTexture,rightAxeTexture,leftBatTexture,rightBatTexture,pistolTexture;
    RaycastHit hit = new RaycastHit();
    bool callOnce;

    int levelID;
    int batSelected, pistolSelected;
    public static float updatedValue, startedValue;
    public Texture[] batColor;
    string batColorPref;
    GameObject target_obj;
    float dist;

    public void revive()
    {
        resetAll();
        turnPlayerOn();
    }

    void Awake()
    {

        //PlayerPrefs.SetInt ("pistolSelect",2);
        //	print ("pistol: "+PlayerPrefs.GetInt("pistolSelect"));
        boostBundleOpened = false;
        shieldBundleOpened = false;
        missileBundleOpened = false;
        batColorPref = PlayerPrefs.GetString("batColor");
        if (!PlayerPrefs.HasKey("missile"))
            PlayerPrefs.SetInt("missile", 20);

        startedValue = PlayerPrefs.GetInt("boosts");
        shieldStartV = PlayerPrefs.GetInt("shieds");
        ammoStartV = PlayerPrefs.GetInt("ammos");
        if (startedValue == 0)
        {
            boostFG.fillAmount = 0;
        }
        if(shieldStartV == 0)
        {
            shieldFG.fillAmount = 0;
        }
        if(ammoStartV == 0)
        {
            ammoFG.fillAmount = 0;
        }
        levelID = PlayerPrefs.GetInt("levels");
        if (levelID == 13 || levelID == 14)
        {
            carHitText.transform.parent.gameObject.SetActive(true);
        }
        else
            carHitText.transform.parent.gameObject.SetActive(false);

        updatedValue = 0.0f;
        shieldUpdateV = 0.0f;
        ammoUpdateV = 0.0f;
        layerMask = 1 << 8;
        if (levelID == 13)
            noOfCarsHit = 10;
        else if (levelID == 14)
            noOfCarsHit = 15;
        //PlayerPrefs.SetInt ("batSelect",0);

        batSelected = PlayerPrefs.GetInt("batSelect");
        //		print ("batselected: "+ batSelected);
        //	batSelected = 0;
        if (batSelected == 0 || batSelected == 2)
        {
            axeLeft.sprite = batLeftSprite;
            axeRight.sprite = batRightSprite;
        }
        else if (batSelected == 1)
        {
            axeLeft.sprite = axeLeftSprite;
            axeRight.sprite = axeRightSprite;
        }
        pistolSelected = PlayerPrefs.GetInt("pistolSelect");
        if (pistolSelected != 0 && pistolSelected != 1 && pistolSelected != 2)
        {
            bullets.transform.parent.gameObject.SetActive(false);
        }
        if (pistolSelected == 1 || pistolSelected == 0)
        {
            bulletBtnBG.sprite = bulletBtn;
        }
        else
            bulletBtnBG.sprite = missileBtn;
        callOnce = true;
        if (bikeSelection.bikeCount == 2 || bikeSelection.bikeCount == 3 || bikeSelection.bikeCount == 5 || bikeSelection.bikeCount == 6 || bikeSelection.bikeCount == 7 || bikeSelection.bikeCount == 8)
        {
            bikeID = 0;
        }
        if (bikeSelection.bikeCount == 1 || bikeSelection.bikeCount == 4 || bikeSelection.bikeCount == 9 || bikeSelection.bikeCount == 10)
        {
            bikeID = 1;
        }

        //bikeID = 0;
        weaponisedPlayer.transform.Find("biker-v3").transform.GetComponent<Renderer>().material.mainTexture = Resources.Load(PlayerPrefs.GetString("shirtColor")) as Texture;

        if (bikeID == 0)
        {
            normalPlayer = fatguy.transform.Find("biker-v3").gameObject;
            weaponisedPlayer = fatWeaponised.gameObject;
            pistolNormal = normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/RootNode(0)/Bip01 Pelvis 1/pistol").gameObject;
            axeNormal = normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/geo_axe").gameObject;
            batNormal = normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/baseball bat").gameObject;
            normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").gameObject.SetActive(false);
            if (batSelected == 2)
            {
                normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").gameObject.SetActive(true);
                batNormal = normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").gameObject;
                normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/baseball bat").gameObject.SetActive(false);
            }
            shotgunNormal = normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/shotgun/Shotgun_Mesh").gameObject;
            batLeft = weaponisedPlayer.GetComponent<Animation>()["axe_left"].clip;
            batRight = weaponisedPlayer.GetComponent<Animation>()["axe_right"].clip;
            pistol = weaponisedPlayer.GetComponent<Animation>()["pistel"].clip;
            shotgun = weaponisedPlayer.GetComponent<Animation>()["shotgun"].clip;
            missile = weaponisedPlayer.GetComponent<Animation>()["missile"].clip;
            shotGunWeaponised = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/shotgun/Shotgun_Mesh").gameObject;
            pistolWeaponised = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/RootNode(0)/Bip01 Pelvis 1/pistol").gameObject;
            axeWeaponised = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Axe").gameObject;
            batWeaponised = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/baseball bat").gameObject;
            weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").gameObject.SetActive(false);
            if (batSelected == 2)
            {
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").gameObject.SetActive(true);
                batWeaponised = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").gameObject;
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/baseball bat").gameObject.SetActive(false);
            }

            shotgunInner = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Shotgun_Mesh").gameObject;

            batLeftInner = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/baseball bat left").gameObject;
            axeLeftInner = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/geo_axe left").gameObject;
            batRightInner = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/baseball bat right").gameObject;
            axeRightInner = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/geo_axe right").gameObject;
            pistolInner = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/pistol").gameObject;



            SimpleTrail = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/geo_axe left/axe left Trail").GetComponent<XWeaponTrail>();
            batLeftTrail = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/baseball bat left/baseball left Trail").GetComponent<XWeaponTrail>();

            axeRightTrail = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/geo_axe right/axe right Trail").GetComponent<XWeaponTrail>();
            batRightTrail = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/baseball bat right/bat right Trail").GetComponent<XWeaponTrail>();


            missileNormal = normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/missileLauncher").gameObject;
            missileWeaponised = weaponisedPlayer.transform.Find("group1/missile launcher").gameObject;
            missileInner = weaponisedPlayer.transform.Find("group1/missile inner").gameObject;

            leftRaycast = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/left raycast").transform;
            rightRaycast = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/right raycast").transform;

            harleyBikescript = normalPlayer.transform.parent.GetComponent<turnLevelcontrols>();

            if (batSelected == 0)
            {
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/baseball bat right/basebat RightInner").gameObject.SetActive(true);
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/baseball bat left/basebat LeftInner").gameObject.SetActive(true);
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/baseball bat right/longBat").gameObject.SetActive(false);
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/baseball bat left/longBat").gameObject.SetActive(false);

            }
            else if (batSelected == 2)
            {
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/baseball bat right/basebat RightInner").gameObject.SetActive(false);
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/baseball bat left/basebat LeftInner").gameObject.SetActive(false);
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/baseball bat right/longBat").gameObject.SetActive(true);
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/baseball bat left/longBat").gameObject.SetActive(true);

                if (batColorPref.Contains("red"))
                {
                    weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/baseball bat left/longBat").transform.GetComponent<Renderer>().material.mainTexture = batColor[1];
                    weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").transform.GetComponent<Renderer>().material.mainTexture = batColor[1];
                    normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").transform.GetComponent<Renderer>().material.mainTexture = batColor[1];
                    weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/baseball bat right/longBat").transform.GetComponent<Renderer>().material.mainTexture = batColor[1];

                }
                else if (batColorPref.Contains("blue"))
                {
                    weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/baseball bat left/longBat").transform.GetComponent<Renderer>().material.mainTexture = batColor[0];
                    weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").transform.GetComponent<Renderer>().material.mainTexture = batColor[0];
                    normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").transform.GetComponent<Renderer>().material.mainTexture = batColor[0];
                    weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/baseball bat right/longBat").transform.GetComponent<Renderer>().material.mainTexture = batColor[0];

                }
                if (batColorPref.Contains("yellow"))
                {
                    weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/baseball bat left/longBat").transform.GetComponent<Renderer>().material.mainTexture = batColor[2];
                    weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").transform.GetComponent<Renderer>().material.mainTexture = batColor[2];
                    normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").transform.GetComponent<Renderer>().material.mainTexture = batColor[2];
                    weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/baseball bat right/longBat").transform.GetComponent<Renderer>().material.mainTexture = batColor[2];
                }
            }

            if (batSelected == 0 || batSelected == 2)
            {
                axeNormal.SetActive(false);
                axeWeaponised.SetActive(false);
                axeLeftInner.SetActive(false);
                axeRightInner.SetActive(false);
            }
            else if (batSelected == 1)
            {
                batNormal.SetActive(false);
                batWeaponised.SetActive(false);
                batLeftInner.SetActive(false);
                batRightInner.SetActive(false);
            }

            if (pistolSelected == 0)
            {
                shotgunNormal.SetActive(false);
                shotGunWeaponised.SetActive(false);
                shotgunInner.SetActive(false);
                missileNormal.SetActive(false);
                missileWeaponised.SetActive(false);
            }
            else if (pistolSelected == 1)
            {
                pistolNormal.SetActive(false);
                pistolWeaponised.SetActive(false);
                pistolInner.SetActive(false);
                missileNormal.SetActive(false);
                missileWeaponised.SetActive(false);
            }
            else if (pistolSelected == 2)
            {
                pistolNormal.SetActive(false);
                pistolWeaponised.SetActive(false);
                pistolInner.SetActive(false);
                shotgunNormal.SetActive(false);
                shotGunWeaponised.SetActive(false);
                shotgunInner.SetActive(false);
            }
            else
            {
                shotgunNormal.SetActive(false);
                shotGunWeaponised.SetActive(false);
                shotgunInner.SetActive(false);
                pistolNormal.SetActive(false);
                pistolWeaponised.SetActive(false);
                pistolInner.SetActive(false);

                missileNormal.SetActive(false);
                missileWeaponised.SetActive(false);
            }


            missileAnimLength = missile.length / 2f;

        }
        else if (bikeID == 1)
        {


            missileAnimLength = 0.8334f;

            normalPlayer = tallguy.transform.Find("biker-v3").gameObject;
            weaponisedPlayer = tallWeaponised.gameObject;
            pistolNormal = normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/RootNode(0)/Bip01 Pelvis 1/pistol").gameObject;
            axeNormal = normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Axe").gameObject;


            normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/baseball bat").gameObject.SetActive(true);
            batNormal = normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/baseball bat").gameObject;

            normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").gameObject.SetActive(false);
            if (batSelected == 2)
            {
                normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").gameObject.SetActive(true);
                batNormal = normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").gameObject;
                normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/baseball bat").gameObject.SetActive(false);
            }
            shotgunNormal = normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/shotgun/Shotgun_Mesh").gameObject;
            batLeft = weaponisedPlayer.GetComponent<Animation>()["axe_left"].clip;
            batRight = weaponisedPlayer.GetComponent<Animation>()["axe_right"].clip;
            pistol = weaponisedPlayer.GetComponent<Animation>()["pistel"].clip;
            shotgun = weaponisedPlayer.GetComponent<Animation>()["shotgun"].clip;
            missile = weaponisedPlayer.GetComponent<Animation>()["missile"].clip;
            shotGunWeaponised = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/shotgun/Shotgun_Mesh").gameObject;

            pistolWeaponised = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/RootNode/Bip01 Pelvis 1/pistol").gameObject;
            axeWeaponised = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Axe").gameObject;

            weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/baseball bat").gameObject.SetActive(true);

            batWeaponised = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/baseball bat").gameObject;


            weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").gameObject.SetActive(false);
            if (batSelected == 2)
            {
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").gameObject.SetActive(true);
                batWeaponised = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").gameObject;
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/baseball bat").gameObject.SetActive(false);
            }

            shotgunInner = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Shotgun inner").gameObject;
            batLeftInner = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/basebat LeftInner").gameObject;
            axeLeftInner = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/geo_axe left").gameObject;
            batRightInner = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/basebat RightInner").gameObject;



            axeRightInner = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/geo_axe right").gameObject;
            pistolInner = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/pistol LeftInner").gameObject;



            SimpleTrail = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/geo_axe left/axe left Trail").GetComponent<XWeaponTrail>();
            batLeftTrail = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/basebat LeftInner/batLeft trail").GetComponent<XWeaponTrail>();

            axeRightTrail = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/geo_axe right/axe right Trail").GetComponent<XWeaponTrail>();
            batRightTrail = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/basebat RightInner/batRight trail").GetComponent<XWeaponTrail>();


            leftRaycast = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/left raycast").transform;
            rightRaycast = weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/right raycast").transform;



            missileNormal = normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/missileLauncher").gameObject;
            missileWeaponised = weaponisedPlayer.transform.Find("missileLauncher").gameObject;
            missileInner = weaponisedPlayer.transform.Find("inner missile").gameObject;


            heavyBikeScript = normalPlayer.transform.parent.GetComponent<heavyBikeTurnControls>();


            if (batSelected == 0)
            {
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/basebat LeftInner/skin").gameObject.SetActive(true);
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/basebat LeftInner/longBat").gameObject.SetActive(false);
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/basebat RightInner/skin").gameObject.SetActive(true);
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/basebat RightInner/longBat").gameObject.SetActive(false);

            }
            else if (batSelected == 2)
            {
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/basebat LeftInner/skin").gameObject.SetActive(false);
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/basebat LeftInner/longBat").gameObject.SetActive(true);
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/basebat RightInner/skin").gameObject.SetActive(false);
                weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/basebat RightInner/longBat").gameObject.SetActive(true);

                if (batColorPref.Contains("red"))
                {
                    weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/basebat LeftInner/longBat").transform.GetComponent<Renderer>().material.mainTexture = batColor[1];
                    weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/basebat RightInner/longBat").transform.GetComponent<Renderer>().material.mainTexture = batColor[1];

                    normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").transform.GetComponent<Renderer>().material.mainTexture = batColor[1];
                    weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").transform.GetComponent<Renderer>().material.mainTexture = batColor[1];
                }
                else if (batColorPref.Contains("blue"))
                {
                    weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/basebat LeftInner/longBat").transform.GetComponent<Renderer>().material.mainTexture = batColor[0];
                    normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").transform.GetComponent<Renderer>().material.mainTexture = batColor[0];
                    weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").transform.GetComponent<Renderer>().material.mainTexture = batColor[0];
                    weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/basebat RightInner/longBat").transform.GetComponent<Renderer>().material.mainTexture = batColor[0];

                }
                if (batColorPref.Contains("yellow"))
                {
                    weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/basebat LeftInner/longBat").transform.GetComponent<Renderer>().material.mainTexture = batColor[2];
                    normalPlayer.transform.parent.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").transform.GetComponent<Renderer>().material.mainTexture = batColor[2];
                    weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/long bat").transform.GetComponent<Renderer>().material.mainTexture = batColor[2];
                    weaponisedPlayer.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/basebat RightInner/longBat").transform.GetComponent<Renderer>().material.mainTexture = batColor[2];

                }
            }


            if (batSelected == 0 || batSelected == 2)
            {
                axeNormal.SetActive(false);
                axeWeaponised.SetActive(false);
                axeLeftInner.SetActive(false);
                axeRightInner.SetActive(false);
            }
            else if (batSelected == 1)
            {
                batNormal.SetActive(false);
                batWeaponised.SetActive(false);
                batLeftInner.SetActive(false);
                batRightInner.SetActive(false);
            }
            if (pistolSelected == 0)
            {
                shotgunNormal.SetActive(false);
                shotGunWeaponised.SetActive(false);
                shotgunInner.SetActive(false);
                missileNormal.SetActive(false);
                missileWeaponised.SetActive(false);
            }
            else if (pistolSelected == 1)
            {
                pistolNormal.SetActive(false);
                pistolWeaponised.SetActive(false);
                pistolInner.SetActive(false);
                missileNormal.SetActive(false);
                missileWeaponised.SetActive(false);
            }
            else if (pistolSelected == 2)
            {
                pistolNormal.SetActive(false);
                pistolWeaponised.SetActive(false);
                pistolInner.SetActive(false);
                shotgunNormal.SetActive(false);
                shotGunWeaponised.SetActive(false);
                shotgunInner.SetActive(false);
            }
            else
            {
                shotgunNormal.SetActive(false);
                shotGunWeaponised.SetActive(false);
                shotgunInner.SetActive(false);
                pistolNormal.SetActive(false);
                pistolWeaponised.SetActive(false);
                pistolInner.SetActive(false);
                missileNormal.SetActive(false);
                missileWeaponised.SetActive(false);
            }
        }


        weaponisedPlayer.transform.Find("biker-v3").transform.GetComponent<Renderer>().material.mainTexture = Resources.Load(PlayerPrefs.GetString("shirtColor")) as Texture;


    }
    void Start()
    {
        if(PlayerPrefs.GetInt("shields") != 0)
        {
            shieldFG.fillAmount = 1f;
        }

        labelStyle.fontSize = System.Convert.ToInt32(Screen.width * 0.025f);
        if (levelID.Equals(7) || levelID.Equals(8) || levelID.Equals(20) || levelID.Equals(18))
        {
            target_obj = GameObject.FindGameObjectWithTag("jeep");
        }
        isAttacking = false;
        batLeftOnce = batRightOnce = axeLeftOnce = axeRightOnce = true;
        shotgunAttackOnce = pistolAttackOnce = true;
        missileOnce = true;
        missileAttack = false;
        //	weaponisedPlayer.transform.FindChild ("biker-v3").transform.renderer.material.mainTexture= Resources.Load(biker) as Texture;

        //	rightBatRect = new Rect (Screen.width - Screen.width * 0.12f, Screen.height - Screen.width * 0.2f, Screen.width * .12f, Screen.width * .07f);
        //	leftBatRect = new Rect (Screen.width * 0.0f, Screen.height - Screen.width * 0.2f, Screen.width * .12f, Screen.width * .07f);

        pistolRect = new Rect(Screen.width - Screen.width * 0.12f, Screen.height - Screen.width * 0.2f, Screen.width * .12f, Screen.width * .07f);


        rightAxeRect = new Rect(Screen.width - Screen.width * 0.23f, Screen.height - Screen.width * 0.135f, Screen.width * .1f, Screen.width * .135f);



        leftAxeRect = new Rect(Screen.width * 0.13f, Screen.height - Screen.width * 0.135f, Screen.width * .1f, Screen.width * .135f);


        if (bikeID == 1)
            weaponisedPlayer.GetComponent<Animation>()[shotgun.name].speed = 1.5f;
        weaponisedPlayer.GetComponent<Animation>()[pistol.name].speed = 2f;

        //pistolRect = new Rect (Screen.width - Screen.width * 0.16f, Screen.height - Screen.width * 0.395f, Screen.width * .15f, Screen.width * .06f);
        //	shotgunRect = new Rect (Screen.width - Screen.width * 0.16f, Screen.height - Screen.width * 0.475f, Screen.width * .15f, Screen.width * .06f);

        weaponisedPlayer.GetComponent<Animation>()[batLeft.name].speed = 2f;
        weaponisedPlayer.GetComponent<Animation>()[batRight.name].speed = 2f;
        weaponisedPlayer.GetComponent<Animation>()[missile.name].speed = 1f;

        //		SimpleTrail.Init();
        //		SimpleTrail.Deactivate();
        //		batLeftTrail.Init();
        //		batLeftTrail.Deactivate();
        //
        //		axeRightTrail.Init();
        //		axeRightTrail.Deactivate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            shieldBtnClicked();
        }
        if(Input.GetKey(KeyCode.Space) && !parentBike.flyoverStart && !parentBike.isJumping && endlessmodeGraphics.gameMode.Equals("Idle"))
        {
            BoostClick();
        }
        if (bikeID == 0)
        {
            isdead = turnLevelcontrols.isdead;
            crash = harleyBikescript.crash;
            unstable = harleyBikescript.unstability;
            levelClear = turnLevelcontrols.levelClear;


        }
        else if (bikeID == 1)
        {
            isdead = heavyBikeTurnControls.isdead;
            crash = heavyBikeScript.crash;
            unstable = heavyBikeScript.unstability;
            levelClear = heavyBikeTurnControls.levelClear;
        }
        if (pistolSelected == 2)
            bullets.text = PlayerPrefs.GetInt("missile") + System.String.Empty;
        else
        {
            bullets.text = PlayerPrefs.GetInt("ammos") + System.String.Empty;
        }

        carHitText.text = noOfCarsHit.ToString();
        if (unstable || crash)
        {

            resetAll();
            turnPlayerOn();
        }
        //		print (handleArrow.start +" "+ !isdead +" "+ !crash  +" "+ tiltControl.startTiltAfterCam  +" "+ !levelClear);
        if (handleArrow.start && !isdead && !crash && tiltControl.startTiltAfterCam && !levelClear)
        {
            boostBG.SetActive(true);
        }
        else
        {
            boostBG.SetActive(false);
        }
        if (isdead)
        {
            resetAll();
            isAttacking = false;
            boostBG.SetActive(false);
            weaponisedPlayer.SetActive(false);

            axeNormal.SetActive(false);
            pistolNormal.SetActive(false);
            shotgunNormal.SetActive(false);
            batNormal.SetActive(false);
            missileNormal.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            resetAll();
            BatLeftAttack = true;
            turnPlayerOff();
            BatAttack();

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            resetAll();
            BatRightAttack = true;
            turnPlayerOff();
            BatAttack();

        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!ammoBundleOpened && !boostBundleOpened && !PhycamViews.slowBundleOpened && !shieldBundleOpened && !missileBundleOpened)
            {
                if (pistolSelected == 0)
                {
                    if (pistolAttackOnce)
                    {
                        //audio.PlayOneShot (axeSound);

                        if (PlayerPrefs.GetInt("ammos") > 0)
                        {
                            resetAll();
                            isPistolAttack = true;
                            turnPlayerOff();
                            pistolAttack();
                            ammoUpdateV = (((100f / ammoStartV) / 100f));
                            ammoFG.fillAmount -= ammoUpdateV;
                            pistolAttackOnce = false;
                        }
                        else
                        {
                            ammoPanel.SetActive(true);
                            ammoBundleOpened = true;
                            CashBgStatus(true);
                            ammoUpdateV = (((100f / ammoStartV) / 100f));
                            ammoFG.fillAmount -= ammoUpdateV;
                            Time.timeScale = 0f;
                        }

                    }

                }
                else if (pistolSelected == 1)
                {
                    if (shotgunAttackOnce)
                    {
                        //audio.PlayOneShot (axeSound);
                        resetAll();
                        isShotgunAttack = true;
                        turnPlayerOff();
                        shotgunAttack();
                        //ammoUpdateV = (((100f / ammoStartV) / 100f));
                        //ammoFG.fillAmount -= ammoUpdateV;
                        shotgunAttackOnce = false;
                    }
                }
                else if (pistolSelected == 2)
                {
                    if (missileOnce)
                    {
                        //audio.PlayOneShot (axeSound);
                        resetAll();
                        missileAttack = true;
                        turnPlayerOff();
                        missileAttackFtn();
                        missileOnce = false;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            resetAll();
            isShotgunAttack = true;
            turnPlayerOff();
            shotgunAttack();
        }


        if (handleArrow.start && !isdead && !crash && tiltControl.startTiltAfterCam && !levelClear)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }

        if (!Application.isEditor)
        {

            //print (!parentBike.flyoverStart + "  "+  !parentBike.isJumping);
            if (handleArrow.start && !isdead && !crash && !levelClear && tiltControl.startTiltAfterCam && !parentBike.flyoverStart && !parentBike.isJumping)
            {


                for (int k = 0; k < Input.touchCount; ++k)
                {
                    Touch touch = Input.GetTouch(k);
                    if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                    {
                        Vector2 touchPos = new Vector2(touch.position.x, Screen.height - touch.position.y);


                        /*                         if (leftAxeRect.Contains(touchPos))  //khuram
                                                {
                                                    if (!ammoBundleOpened && !boostBundleOpened && !PhycamViews.slowBundleOpened && !shieldBundleOpened && !missileBundleOpened)
                                                    {
                                                        //	leftAxeRect = new Rect (Screen.width * 0.0f, Screen.height - Screen.width * 0.265f, Screen.width * .17f, Screen.width * .09f);
                                                        if (batSelected == 0 || batSelected == 2)
                                                        {
                                                            if (batLeftOnce)
                                                            {

                                                                if (PhycamViews.counter == 11 || PhycamViews.counter == 12)
                                                                {
                                                                    PhycamViews.counter = 2;
                                                                    mainCamera.changeCamView();
                                                                }
                                                                GetComponent<AudioSource>().PlayOneShot(batSound);
                                                                resetAll();
                                                                batLeftOnce = false;
                                                                BatLeftAttack = true;
                                                                turnPlayerOff();
                                                                BatAttack();
                                                            }
                                                        }
                                                        else if (batSelected == 1)
                                                        {
                                                            if (axeLeftOnce)
                                                            {
                                                                if (PhycamViews.counter == 11 || PhycamViews.counter == 12)
                                                                {
                                                                    PhycamViews.counter = 2;
                                                                    mainCamera.changeCamView();
                                                                }
                                                                GetComponent<AudioSource>().PlayOneShot(axeSound);
                                                                resetAll();
                                                                axeLeftAttack = true;
                                                                turnPlayerOff();
                                                                BatAttack();
                                                                axeLeftOnce = false;
                                                            }
                                                        }
                                                    }
                                                } */

                        /*                         if (rightAxeRect.Contains(touchPos)) //axe right
                                                {
                                                    if (!ammoBundleOpened && !boostBundleOpened && !PhycamViews.slowBundleOpened && !shieldBundleOpened && !missileBundleOpened)
                                                    {

                                                        if (batSelected == 1)
                                                        {
                                                            if (axeRightOnce)
                                                            {
                                                                if (PhycamViews.counter == 11 || PhycamViews.counter == 12)
                                                                {
                                                                    PhycamViews.counter = 2;
                                                                    mainCamera.changeCamView();
                                                                }
                                                                GetComponent<AudioSource>().PlayOneShot(axeSound);
                                                                resetAll();
                                                                axeRightAttack = true;
                                                                turnPlayerOff();
                                                                BatAttack();
                                                                axeRightOnce = false;
                                                            }

                                                        }
                                                        else if (batSelected == 0 || batSelected == 2)
                                                        {
                                                            if (batRightOnce)
                                                            {
                                                                if (PhycamViews.counter == 11 || PhycamViews.counter == 12)
                                                                {
                                                                    PhycamViews.counter = 2;
                                                                    mainCamera.changeCamView();
                                                                }
                                                                GetComponent<AudioSource>().PlayOneShot(batSound);
                                                                resetAll();
                                                                batRightOnce = false;
                                                                BatRightAttack = true;
                                                                turnPlayerOff();
                                                                BatAttack();

                                                            }
                                                        }

                                                    }
                                                } */

                        /*                         if (pistolRect.Contains(touchPos)) //khuram
                                                {
                                                    if (!ammoBundleOpened && !boostBundleOpened && !PhycamViews.slowBundleOpened && !shieldBundleOpened && !missileBundleOpened)
                                                    {
                                                        if (pistolSelected == 0)
                                                        {
                                                            if (pistolAttackOnce)
                                                            {

                                                                if (PlayerPrefs.GetInt("ammos") > 0)
                                                                {
                                                                    resetAll();
                                                                    isPistolAttack = true;
                                                                    turnPlayerOff();
                                                                    pistolAttack();
                                                                    pistolAttackOnce = false;
                                                                }
                                                                else
                                                                {
                                                                    ammoPanel.SetActive(true);
                                                                    ammoBundleOpened = true;
                                                                    CashBgStatus(true);
                                                                    Time.timeScale = 0f;
                                                                    if (PlayerPrefs.GetInt("SoundOff") == 0)
                                                                    {
                                                                        AudioListener.volume = 0.0f;
                                                                    }
                                                                }

                                                            }
                                                        }
                                                        else if (pistolSelected == 1)
                                                        {
                                                            if (shotgunAttackOnce)
                                                            {
                                                                if (PlayerPrefs.GetInt("ammos") > 0)
                                                                {
                                                                    resetAll();
                                                                    isShotgunAttack = true;
                                                                    turnPlayerOff();
                                                                    shotgunAttack();
                                                                    shotgunAttackOnce = false;
                                                                }
                                                                else
                                                                {
                                                                    ammoPanel.SetActive(true);
                                                                    ammoBundleOpened = true;
                                                                    CashBgStatus(true);
                                                                    Time.timeScale = 0f;
                                                                    if (PlayerPrefs.GetInt("SoundOff") == 0)
                                                                    {
                                                                        AudioListener.volume = 0.0f;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else if (pistolSelected == 2)
                                                        {
                                                            if (PlayerPrefs.GetInt("missile") > 0)
                                                            {
                                                                if (missileOnce)
                                                                {
                                                                    resetAll();
                                                                    missileAttack = true;
                                                                    turnPlayerOff();
                                                                    missileAttackFtn();
                                                                    missileOnce = false;

                                                                }
                                                            }
                                                            else
                                                            {
                                                                missilePanel.SetActive(true);
                                                                missileBundleOpened = true;
                                                                CashBgStatus(true);
                                                                Time.timeScale = 0f;
                                                                if (PlayerPrefs.GetInt("SoundOff") == 0)
                                                                {
                                                                    AudioListener.volume = 0.0f;
                                                                }
                                                            }
                                                        }
                                                    }
                                                } */
                    }
                }
            }
            /*             if (levelID == 6)  //khuram
                        {

                            if (handleArrow.start && !isdead && !crash && !levelClear && tiltControl.startTiltAfterCam)
                            {
                                for (int k = 0; k < Input.touchCount; ++k)
                                {
                                    Touch touch = Input.GetTouch(k);
                                    if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                                    {
                                        Vector2 touchPos = new Vector2(touch.position.x, Screen.height - touch.position.y);

                                        if (pistolRect.Contains(touchPos))
                                        {
                                            if (!ammoBundleOpened && !boostBundleOpened && !PhycamViews.slowBundleOpened && !shieldBundleOpened && !missileBundleOpened)
                                            {
                                                Time.timeScale = 1;
                                                Time.fixedDeltaTime = 0.02f;
                                                if (pistolSelected == 0)
                                                {
                                                    if (pistolAttackOnce)
                                                    {

                                                        if (PlayerPrefs.GetInt("ammos") > 0)
                                                        {
                                                            resetAll();
                                                            isPistolAttack = true;
                                                            turnPlayerOff();
                                                            pistolAttack();
                                                            pistolAttackOnce = false;
                                                        }
                                                        else
                                                        {
                                                            ammoPanel.SetActive(true);
                                                            ammoBundleOpened = true;
                                                            CashBgStatus(true);
                                                            Time.timeScale = 0f;
                                                            if (PlayerPrefs.GetInt("SoundOff") == 0)
                                                            {
                                                                AudioListener.volume = 0.0f;
                                                            }
                                                        }

                                                    }
                                                }
                                                else if (pistolSelected == 1)
                                                {
                                                    if (shotgunAttackOnce)
                                                    {

                                                        if (PlayerPrefs.GetInt("ammos") > 0)
                                                        {

                                                            resetAll();
                                                            isShotgunAttack = true;
                                                            turnPlayerOff();
                                                            shotgunAttack();
                                                            shotgunAttackOnce = false;
                                                        }
                                                        else
                                                        {
                                                            ammoPanel.SetActive(true);
                                                            ammoBundleOpened = true;
                                                            CashBgStatus(true);
                                                            Time.timeScale = 0f;
                                                            if (PlayerPrefs.GetInt("SoundOff") == 0)
                                                            {
                                                                AudioListener.volume = 0.0f;
                                                            }
                                                        }

                                                    }
                                                }
                                                else if (pistolSelected == 2)
                                                {
                                                    if (PlayerPrefs.GetInt("missile") > 0)
                                                    {
                                                        if (missileOnce)
                                                        {

                                                            resetAll();
                                                            missileAttack = true;
                                                            turnPlayerOff();
                                                            missileAttackFtn();
                                                            missileOnce = false;

                                                        }
                                                    }
                                                    else
                                                    {
                                                        missilePanel.SetActive(true);
                                                        missileBundleOpened = true;
                                                        CashBgStatus(true);
                                                        Time.timeScale = 0f;
                                                        if (PlayerPrefs.GetInt("SoundOff") == 0)
                                                        {
                                                            AudioListener.volume = 0.0f;
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        } */

        }



        if (Input.GetKeyDown(KeyCode.N))
        {
            resetAll();
            axeLeftAttack = true;
            turnPlayerOff();

            BatAttack();

        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            resetAll();
            axeRightAttack = true;
            turnPlayerOff();
            BatAttack();

        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            resetAll();
            missileAttack = true;
            turnPlayerOff();
            missileAttackFtn();
        }
    }

    bool outOfRange;

    void OnGUI()
    {

        if (handleArrow.start && !isdead && !crash && tiltControl.startTiltAfterCam && !levelClear)
        {

            if (outOfRange)
            {
                GUI.Label(new Rect(Screen.width * 0.4f, Screen.height * 0.3f, Screen.width * 0.3f, Screen.width * 0.5f), "Out Of Range!!", labelStyle);
            }

            GUI.backgroundColor = Color.clear;
            //khuram
            /*             if (GUI.Button(new Rect(Screen.width - Screen.width * 0.13f, Screen.height - Screen.width * .35f, Screen.width * .12f, Screen.width * .12f), ""))
                        {
                            if (!parentBike.flyoverStart && !parentBike.isJumping && endlessmodeGraphics.gameMode.Equals("Idle"))
                            {

                                if (!heavyBikeTurns.boostbool && !PhycamViews.slowingDown && !PhycamViews.slowBundleOpened && !ammoBundleOpened && !shieldBundleOpened && !missileBundleOpened)
                                {
                                    if (PhycamViews.counter == 11 || PhycamViews.counter == 12)
                                    {
                                        PhycamViews.counter = 2;
                                        mainCamera.changeCamView();
                                    }
                                    if (PlayerPrefs.GetInt("boosts") > 0)
                                    {
                                        PlayerPrefs.SetInt("boost/timerUsed", (PlayerPrefs.GetInt("boost/timerUsed") + 1));
                                        updatedValue = (((100f / startedValue) / 100f));
                                        boostFG.fillAmount -= updatedValue;
                                        PlayerPrefs.SetInt("boosts", (PlayerPrefs.GetInt("boosts") - 1));
                                        PlayerPrefs.Save();
                                        booster.startBoost();
                                    }
                                    else
                                    {
                                        boostBundleOpened = true;
                                        boostPanel.SetActive(true);
                                        Time.timeScale = 0.0f;
                                        if (PlayerPrefs.GetInt("SoundOff") == 0)
                                        {
                                            AudioListener.volume = 0.0f;
                                        }
                                    }
                                }
                            }
                        } */
        }


    }
    void FixedUpdate()
    {
        if (isAttacking)
        {

            //Debug.DrawRay (leftRaycast.position, -Vector3.up*0.3f, Color.cyan);
            if (Physics.Raycast(leftRaycast.position, -Vector3.up, out hit, 0.3f, layerMask))
            {

                //print (hit.collider.transform.root.name+ " "+ hit.collider.name+ "  "+ (hit.collider.transform.root.position.x-transform.root.position.x));
                if ((hit.collider.transform.root.name.Contains("opponentFatguy") || hit.collider.transform.root.name.Contains("opponentTallguys")) && (hit.collider.transform.root.position.x - transform.root.position.x <= -0.75f))
                { //-1.75f
                    if (callOnce)
                    {
                        if (BatLeftAttack || BatRightAttack)
                            hit.collider.transform.root.GetComponent<newLevelCrashed>().opponentAttacked("bat");
                        else
                            hit.collider.transform.root.GetComponent<newLevelCrashed>().opponentAttacked("axe");
                        if (bikeID == 0)
                        {

                            turnLevelcontrols.score += 100;
                            harleyBikescript.scorePopup.GetComponent<TextMesh>().text = "+100";
                            harleyBikescript.scorePopup.GetComponent<TextMesh>().color = new Color(253, 246, 0, 255);
                            Instantiate(harleyBikescript.scorePopup, transform.position + new Vector3(0f, 2f, 4f), Quaternion.identity);

                        }
                        else if (bikeID == 1)
                        {

                            heavyBikeTurnControls.score += 100;
                            heavyBikeScript.scorePopup.GetComponent<TextMesh>().text = "+100";
                            heavyBikeScript.scorePopup.GetComponent<TextMesh>().color = new Color(253, 246, 0, 255);
                            Instantiate(heavyBikeScript.scorePopup, transform.position + new Vector3(0f, 2f, 4f), Quaternion.identity);
                        }
                        //score+=100;



                        callOnce = false;
                    }
                }

            }


            //Debug.DrawRay (rightRaycast.position, -Vector3.up*0.3f, Color.green);
            if (Physics.Raycast(rightRaycast.position, -Vector3.up, out hit, 0.3f, layerMask))
            {

                //				print (hit.collider.transform.root.name+ " "+ (hit.collider.transform.root.position.x-transform.root.position.x));
                if ((hit.collider.transform.root.name.Contains("opponentTallguys") || hit.collider.transform.root.name.Contains("opponentFatguy")) && (hit.collider.transform.root.position.x - transform.root.position.x <= 0.75f))
                {//2.16
                    if (callOnce)
                    {
                        if (BatLeftAttack || BatRightAttack)
                            hit.collider.transform.root.GetComponent<newLevelCrashed>().opponentAttacked("bat");
                        else
                            hit.collider.transform.root.GetComponent<newLevelCrashed>().opponentAttacked("axe");
                        if (bikeID == 0)
                        {

                            turnLevelcontrols.score += 100;
                            harleyBikescript.scorePopup.GetComponent<TextMesh>().text = "+100";
                            harleyBikescript.scorePopup.GetComponent<TextMesh>().color = new Color(253, 246, 0, 255);
                            Instantiate(harleyBikescript.scorePopup, transform.position + new Vector3(0f, 2f, 4f), Quaternion.identity);

                        }
                        else if (bikeID == 1)
                        {

                            heavyBikeTurnControls.score += 100;
                            heavyBikeScript.scorePopup.GetComponent<TextMesh>().text = "+100";
                            heavyBikeScript.scorePopup.GetComponent<TextMesh>().color = new Color(253, 246, 0, 255);
                            Instantiate(heavyBikeScript.scorePopup, transform.position + new Vector3(0f, 2f, 4f), Quaternion.identity);
                        }

                        callOnce = false;
                    }
                }


            }

        }
    }

    void shotgunAttack()
    {

        if (bikeID == 1)
        {
            Invoke("Changepistol", 0.8f / 1.5f);
            Invoke("ChangepistolBack", 2.1f / 1.5f);

            weaponisedPlayer.transform.GetComponent<Animation>().Play(shotgun.name, PlayMode.StopAll);
            Invoke("ShotGunFire", weaponisedPlayer.GetComponent<Animation>()[shotgun.name].length * 0.5f / 1.5f);
        }
        else
        {

            Invoke("Changepistol", 0.4f);
            Invoke("ChangepistolBack", 1.6f);

            weaponisedPlayer.transform.GetComponent<Animation>().Play(shotgun.name, PlayMode.StopAll);
            Invoke("ShotGunFire", weaponisedPlayer.GetComponent<Animation>()[shotgun.name].length * 0.5f);
        }
        Invoke("turnPlayerOn", shotgun.length);
        Invoke("shotgunAttackEnd", shotgun.length);


    }


    void ShotGunFire()
    {
        shotgunControl.Shoot();

        if (levelID.Equals(7) || levelID.Equals(8) || levelID.Equals(20) || levelID.Equals(18))
        {
            jeepControl.gunName = "shotgun";
            //			print (jeepControl.gunName+ "  "+ levelID);

        }
        pistolDamage("shotgun", 50);
    }



    void timeSet()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }


    IEnumerator outofRangeWarning()
    {
        outOfRange = true;
        yield return new WaitForSeconds(1f);
        outOfRange = false;
    }
    void pistolDamage(string name, float range)
    {
        if (levelID == 6 || levelID == 17)
        {

            target_obj = GetGangster();
            if (target_obj != null)
            {

                dist = Vector3.Distance(target_obj.transform.parent.position, transform.position);
                //print (transform.position.x + " "+target_obj.transform.position.x );
                if (dist > 1f && dist <= range)
                {
                    if ((target_obj.transform.position.x >= lane1 && transform.position.x >= lane1))
                    {
                        target_obj.transform.GetComponent<gangsters>().gangsterDeath();
                        mainCamera.oppoCrashCam();
                        //						Time.timeScale=0.2f;
                        //						Time.fixedDeltaTime=0.006f;
                        //						Invoke("timeSet",1.5f);
                    }
                    else if ((target_obj.transform.position.x >= lane2 && transform.position.x >= lane2) && (target_obj.transform.position.x <= lane1 && transform.position.x <= lane1))
                    {
                        target_obj.transform.GetComponent<gangsters>().gangsterDeath();
                        mainCamera.oppoCrashCam();
                        //						Time.timeScale=0.2f;
                        //						Time.fixedDeltaTime=0.006f;
                        //						Invoke("timeSet",1.5f);
                    }
                    else if ((target_obj.transform.position.x >= lane3 && transform.position.x >= lane3) && (target_obj.transform.position.x <= lane2 && transform.position.x <= lane2))
                    {
                        target_obj.transform.GetComponent<gangsters>().gangsterDeath();
                        mainCamera.oppoCrashCam();
                        //						Time.timeScale=0.2f;
                        //						Time.fixedDeltaTime=0.006f;
                        //						Invoke("timeSet",1.5f);
                    }
                    else if ((target_obj.transform.position.x < lane3 && transform.position.x < lane3))
                    {
                        target_obj.transform.GetComponent<gangsters>().gangsterDeath();
                        mainCamera.oppoCrashCam();
                        //						Time.timeScale=0.2f;
                        //						Time.fixedDeltaTime=0.006f;
                        //						Invoke("timeSet",1.5f);
                    }
                }
                else if (dist > range)
                {
                    StartCoroutine("outofRangeWarning");
                }
            }
        }
        else if (levelID.Equals(8) || levelID.Equals(7) || levelID.Equals(20) || levelID.Equals(18))
        {
            // print(transform.position.x + " " + target_obj.transform.position.x);
            if (target_obj != null)
            {
                dist = Vector3.Distance(target_obj.transform.position, transform.position);

                if (dist > 1f && dist <= range)
                {
                    if ((target_obj.transform.position.x >= lane1 && transform.position.x >= lane1))
                    {
                        target_obj.transform.GetComponent<jeepControl>().onFire();

                    }
                    else if ((target_obj.transform.position.x >= lane2 && transform.position.x >= lane2) && (target_obj.transform.position.x <= lane1 && transform.position.x <= lane1))
                    {
                        target_obj.transform.GetComponent<jeepControl>().onFire();


                    }
                    else if ((target_obj.transform.position.x >= lane3 && transform.position.x >= lane3) && (target_obj.transform.position.x <= lane2 && transform.position.x <= lane2))
                    {
                        target_obj.transform.GetComponent<jeepControl>().onFire();


                    }
                    else if ((target_obj.transform.position.x < lane3 && transform.position.x < lane3))
                    {
                        target_obj.transform.GetComponent<jeepControl>().onFire();

                    }
                }
                else if (dist > range)
                {
                    StartCoroutine("outofRangeWarning");
                }
            }
        }
        else if (levelID == 13 || levelID == 14)
        {
            target_obj = GetCars();

            if (target_obj != null)
            {
                print(target_obj.name + " " + target_obj.tag);
                dist = Vector3.Distance(target_obj.transform.position, transform.position);
                //print (transform.position.x + " "+target_obj.transform.position.x );
                if (dist > 1f && dist <= range)
                {
                    if ((target_obj.transform.position.x >= lane1 && transform.position.x >= lane1))
                    {
                        explodeCar(target_obj, name);


                    }
                    else if ((target_obj.transform.position.x >= lane2 && transform.position.x >= lane2) && (target_obj.transform.position.x <= lane1 && transform.position.x <= lane1))
                    {

                        explodeCar(target_obj, name);

                    }
                    else if ((target_obj.transform.position.x >= lane3 && transform.position.x >= lane3) && (target_obj.transform.position.x <= lane2 && transform.position.x <= lane2))
                    {

                        explodeCar(target_obj, name);
                    }
                    else if ((target_obj.transform.position.x < lane3 && transform.position.x < lane3))
                    {

                        explodeCar(target_obj, name);
                    }
                }
                else if (dist > range)
                {
                    StartCoroutine("outofRangeWarning");
                }
            }
        }
        if ((leftBike.position.z - transform.position.z) <= (range + 10) && (leftBike.position.z - transform.position.z) >= 5)
        {
            if ((leftBike.position.x >= lane1 && transform.position.x >= lane1))
            {
                leftBike.GetComponent<newLevelCrashed>().opponentAttacked(name);
            }
            else if ((leftBike.position.x >= lane2 && transform.position.x >= lane2))
            {
                leftBike.GetComponent<newLevelCrashed>().opponentAttacked(name);
            }
            else if ((leftBike.position.x >= lane3 && transform.position.x >= lane3))
            {
                leftBike.GetComponent<newLevelCrashed>().opponentAttacked(name);
            }
            else if ((leftBike.position.x < lane3 && transform.position.x < lane3))
            {
                leftBike.GetComponent<newLevelCrashed>().opponentAttacked(name);
            }
        }

        if ((rightBike.position.z - transform.position.z) <= (range + 10) && (rightBike.position.z - transform.position.z) >= 5)
        {


            if ((rightBike.position.x <= lane3 && transform.position.x <= lane3)) //-1.81
            {
                rightBike.GetComponent<newLevelCrashed>().opponentAttacked(name);
            }
            else if ((rightBike.position.x <= lane2 && transform.position.x <= lane2)) //1.7
            {
                rightBike.GetComponent<newLevelCrashed>().opponentAttacked(name);
            }
            else if ((rightBike.position.x <= lane1 && transform.position.x <= lane1))
            {  //5.2
                rightBike.GetComponent<newLevelCrashed>().opponentAttacked(name);
            }

            else if ((rightBike.position.x > lane1 && transform.position.x > lane1))
            {     //5.2
                rightBike.GetComponent<newLevelCrashed>().opponentAttacked(name);
            }

        }
    }

    public AudioClip explosionSound, carHitSound;
    public cameraShake cameraShakeScript;

    void explodeCar(GameObject col, string weaponName)
    {
        if (weaponName.Equals("pistol"))
            col.transform.parent.parent.GetComponent<startMove>().hitCounter += 1;
        else if (weaponName.Equals("shotgun"))
            col.transform.parent.parent.GetComponent<startMove>().hitCounter += 2;
        if (col.transform.parent.parent.GetComponent<startMove>().hitCounter >= 3)
        {
            if (!col.transform.parent.parent.GetComponent<startMove>().missileHit)
            {
                if (noOfCarsHit > 0)
                    noOfCarsHit -= 1;
                transform.GetComponent<AudioSource>().PlayOneShot(explosionSound);

                print(col.transform.parent.name);
                col.transform.parent.parent.GetComponent<startMove>().missileHit = true;
                col.transform.parent.parent.GetChild(1).gameObject.SetActive(true);
                col.transform.parent.gameObject.SetActive(false);
                cameraShakeScript.enabled = true;
                cameraShakeScript.shakeAmount = 0.1f;
                cameraShakeScript.shakeDuration = 0.1f;

            }
        }
        else
        {
            transform.GetComponent<AudioSource>().PlayOneShot(carHitSound);
        }
    }
    GameObject GetGangster()
    {

        GameObject[] gos;

        gos = GameObject.FindGameObjectsWithTag("Gangster");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && go.transform.position.z > gameObject.transform.position.z)
            {
                closest = go;
                distance = curDistance;




            }

        }


        return closest;


    }


    GameObject GetCars()
    {

        GameObject[] gos;

        gos = GameObject.FindGameObjectsWithTag("newLevelHurdle");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && go.transform.position.z > gameObject.transform.position.z)
            {
                closest = go;
                distance = curDistance;




            }

        }


        return closest;


    }
    void PistolFire()
    {
        pistolControl.Shoot();
        //		print ((leftBike.position.z - transform.position.z));
        if (levelID.Equals(7) || levelID.Equals(8) || levelID.Equals(20) || levelID.Equals(18))
        {
            jeepControl.gunName = "pistol";
            //			print (jeepControl.gunName+ "  "+ levelID);

        }
        pistolDamage("pistol", 40);


    }


    void pistolAttack()
    {
        Invoke("Changepistol", 0.6f / 2);
        Invoke("ChangepistolBack", 1.85f / 2);

        weaponisedPlayer.transform.GetComponent<Animation>().Play(pistol.name, PlayMode.StopAll);
        Invoke("PistolFire", weaponisedPlayer.GetComponent<Animation>()[pistol.name].length / 4.0f);
        Invoke("turnPlayerOn", pistol.length / 2);
        Invoke("pistolAttackEnd", pistol.length / 2);
    }

    void Changepistol()
    {
        if (isPistolAttack)
        {
            pistolWeaponised.SetActive(false);

            pistolInner.SetActive(true);
        }
        if (isShotgunAttack)
        {
            shotGunWeaponised.SetActive(false);
            shotgunInner.SetActive(true);
        }
    }
    void ChangepistolBack()
    {
        if (isPistolAttack)
        {
            pistolWeaponised.SetActive(true);
            pistolInner.SetActive(false);
        }
        if (isShotgunAttack)
        {
            shotGunWeaponised.SetActive(true);
            shotgunInner.SetActive(false);
        }
    }
    void pistolAttackEnd()
    {
        isPistolAttack = false;
        pistolAttackOnce = true;
    }

    void shotgunAttackEnd()
    {
        isShotgunAttack = false;
        shotgunAttackOnce = true;
    }

    void resetAll()
    {
        CancelInvoke();
        if (bikeID == 1 && !crash && !isdead && !unstable)
        {
            //			if(tallguy.GetComponent<heavyBikeTurnControls>().stuntbool)
            //			{
            //			tallguy.GetComponent<heavyBikeTurnControls>().stuntOn();
            //			tallguy.GetComponent<heavyBikeTurnControls>().stuntbool=false;
            //			}
            tallguy.GetComponent<Animation>().Play("race", PlayMode.StopAll);
        }
        else if (bikeID == 0 && !crash && !isdead && !unstable)
        {

            fatguy.GetComponent<Animation>().Play("race", PlayMode.StopAll);
        }
        batLeftTrail.Deactivate();
        SimpleTrail.Deactivate();
        axeRightTrail.Deactivate();
        batRightTrail.Deactivate();

        if (batSelected == 0 || batSelected == 2)
            batWeaponised.SetActive(true);
        else if (batSelected == 1)
            axeWeaponised.SetActive(true);

        batLeftInner.SetActive(false);

        batRightInner.SetActive(false);
        axeLeftInner.SetActive(false);

        axeRightInner.SetActive(false);
        if (pistolSelected == 0)
            pistolWeaponised.SetActive(true);
        else if (pistolSelected == 1)
            shotGunWeaponised.SetActive(true);
        else if (pistolSelected == 2)
            missileWeaponised.SetActive(true);

        pistolInner.SetActive(false);
        shotgunInner.SetActive(false);

        axeRightAttack = false;
        axeLeftAttack = false;
        BatLeftAttack = false;
        BatRightAttack = false;
        isPistolAttack = false;
        isShotgunAttack = false;

        batLeftOnce = true;
        batRightOnce = true;
        axeLeftOnce = true;
        axeRightOnce = true;
        pistolAttackOnce = true;
        shotgunAttackOnce = true;


        missileAttack = false;
        missileOnce = true;

    }

    bool soundOnce;


    void missileAttackFtn()
    {
        if (PlayerPrefs.GetInt("missile") > 0)
            PlayerPrefs.SetInt("missile", PlayerPrefs.GetInt("missile") - 1);
        missileInner.SetActive(true);
        Invoke("turnPlayerOn", missile.length);
        Invoke("missileDamage", missileAnimLength);


        weaponisedPlayer.transform.GetComponent<Animation>().Play(missile.name, PlayMode.StopAll);

        Invoke("missileAttackEnd", missile.length);


    }
    public GameObject rocket;
    public static int noOfCarsHit;
    void missileDamage()
    {
        missileInner.SetActive(false);
        rocket.SetActive(true);
        missileScript._enabled = true;
        if (bikeID == 1)
            rocket.transform.position = transform.position + new Vector3(0, 0.25f, 0.1f);
        else
            rocket.transform.position = transform.position + new Vector3(0.05f, 0.387f, 0.205f);
        //Instantiate (rocket, transform.position + new Vector3 (0, 0.25f, 0.1f), transform.rotation);
        GetComponent<AudioSource>().PlayOneShot(missileSound);
        if (levelID.Equals(7) || levelID.Equals(8) || levelID.Equals(20) || levelID.Equals(18))
        {
            jeepControl.gunName = "missile";
        }
        if (levelID != 13 && levelID != 14)
            pistolDamage("missile", 60);
    }
    void BatAttack()
    {
        Invoke("ChangeBat", 0.4f / 2);
        Invoke("ChangeBatBack", 1.1f / 2);
        Invoke("turnPlayerOn", batLeft.length / 2);
        if (BatLeftAttack || axeLeftAttack)
        {
            weaponisedPlayer.transform.GetComponent<Animation>().Play(batLeft.name, PlayMode.StopAll);
            if (BatLeftAttack)
                Invoke("LeftbatAttackEnd", batLeft.length / 2);
            if (axeLeftAttack)
                Invoke("LeftAxeAttackEnd", batLeft.length / 2);
        }

        else if (BatRightAttack || axeRightAttack)
        {
            weaponisedPlayer.transform.GetComponent<Animation>().Play(batRight.name, PlayMode.StopAll);
            if (BatRightAttack)
                Invoke("RightbatAttackEnd", batLeft.length / 2);
            if (axeRightAttack)
                Invoke("RightAxeAttackEnd", batLeft.length / 2);
        }
    }
    void ChangeBat()
    {
        if (axeLeftAttack)
            SimpleTrail.Activate();
        //	leftTrail.SetActive(true);
        if (BatLeftAttack)
            batLeftTrail.Activate();
        //		if (BatRightAttack || axeRightAttack) 
        //		rightTrail.SetActive (true);

        if (axeRightAttack)
            axeRightTrail.Activate();

        if (BatRightAttack)
            batRightTrail.Activate();

        if (BatLeftAttack || BatRightAttack)
            batWeaponised.SetActive(false);
        if (axeLeftAttack || axeRightAttack)
            axeWeaponised.SetActive(false);

        if (BatLeftAttack)
            batLeftInner.SetActive(true);
        else if (BatRightAttack)
            batRightInner.SetActive(true);

        if (axeLeftAttack)
            axeLeftInner.SetActive(true);
        else if (axeRightAttack)
            axeRightInner.SetActive(true);
    }

    void ChangeBatBack()
    {
        if (axeLeftAttack)
            SimpleTrail.Deactivate();
        //	leftTrail.SetActive(true);
        if (BatLeftAttack)
            batLeftTrail.Deactivate();

        if (axeRightAttack)
            axeRightTrail.Deactivate();

        if (BatRightAttack)
            batRightTrail.Deactivate();

        //		if (BatRightAttack || axeRightAttack) 
        //			rightTrail.SetActive (false);

        if (BatLeftAttack || BatRightAttack)
            batWeaponised.SetActive(true);
        if (axeLeftAttack || axeRightAttack)
            axeWeaponised.SetActive(true);
        if (BatLeftAttack)
            batLeftInner.SetActive(false);
        else if (BatRightAttack)
            batRightInner.SetActive(false);
        if (axeLeftAttack)
            axeLeftInner.SetActive(false);
        else if (axeRightAttack)
            axeRightInner.SetActive(false);
    }
    void LeftbatAttackEnd()
    {
        batLeftOnce = true;
        BatLeftAttack = false;
    }

    void missileAttackEnd()
    {
        missileOnce = true;
        missileAttack = false;
    }
    void LeftAxeAttackEnd()
    {
        axeLeftOnce = true;
        axeLeftAttack = false;
    }

    void RightbatAttackEnd()
    {

        BatRightAttack = false;
        batRightOnce = true;
    }

    void RightAxeAttackEnd()
    {

        axeRightAttack = false;
        axeRightOnce = true;
    }

    void turnPlayerOff()
    {
        if (PhycamViews.counter == 11 || PhycamViews.counter == 12)
        {
            PhycamViews.counter = 2;
            mainCamera.changeCamView();
        }
        isAttacking = true;
        normalPlayer.SetActive(false);
        weaponisedPlayer.SetActive(true);
        batNormal.SetActive(false);
        axeNormal.SetActive(false);
        pistolNormal.SetActive(false);
        shotgunNormal.SetActive(false);
        missileNormal.SetActive(false);

    }

    void turnPlayerOn()
    {
        isAttacking = false;
        normalPlayer.SetActive(true);
        weaponisedPlayer.SetActive(false);
        if (batSelected == 0 || batSelected == 2)
            batNormal.SetActive(true);
        else if (batSelected == 1)
            axeNormal.SetActive(true);
        if (pistolSelected == 0)
            pistolNormal.SetActive(true);
        else if (pistolSelected == 1)
            shotgunNormal.SetActive(true);
        else if (pistolSelected == 2)
            missileNormal.SetActive(true);
        callOnce = true;

    }


    public void closeBundle()
    {
        startedValue = PlayerPrefs.GetInt("boosts");
        shieldStartV = PlayerPrefs.GetInt("shields");
        ammoStartV = PlayerPrefs.GetInt("ammos");
        shieldBundleOpened = false;
        missileBundleOpened = false;
        missilePanel.SetActive(false);
        shieldPanel.SetActive(false);
        ammoPanel.SetActive(false);
        ammoBundleOpened = false;
        boostPanel.SetActive(false);
        boostBundleOpened = false;
        CashBgStatus(false);
        Time.timeScale = 1;
        if (PlayerPrefs.GetInt("SoundOff") == 0)
        {
            AudioListener.volume = 1.0f;
        }
    }


    public void shieldBtnClicked()
    {
        if (!heavyBikeTurns.shieldOn)
        {
            if (PlayerPrefs.GetInt("shields") > 0)
            {
                PlayerPrefs.SetInt("shieldUsed", (PlayerPrefs.GetInt("shieldUsed") + 1));
                shieldUpdateV = (((100f / shieldStartV) / 100f));
                shieldFG.fillAmount -= shieldUpdateV;
                PlayerPrefs.SetInt("shields", (PlayerPrefs.GetInt("shields") - 1));
                PlayerPrefs.Save();
                heavyBikeTurns.activateShield = true;
            }
            else
            {
                shieldPanel.SetActive(true);
                shieldBundleOpened = true;
                CashBgStatus(true);
                Time.timeScale = 0;
                if (PlayerPrefs.GetInt("SoundOff") == 0)
                {
                    AudioListener.volume = 1.0f;
                }

            }

        }
    }
    public void BoostClick()
    {
        if (!parentBike.flyoverStart && !parentBike.isJumping && endlessmodeGraphics.gameMode.Equals("Idle"))
        {
            if (!heavyBikeTurns.boostbool && !PhycamViews.slowingDown)
            {
                if (PhycamViews.counter == 11 || PhycamViews.counter == 12)
                {
                    PhycamViews.counter = 2;
                    mainCamera.changeCamView();
                }
                if (PlayerPrefs.GetInt("boosts") > 0)
                {
                    PlayerPrefs.SetInt("boost/timerUsed", (PlayerPrefs.GetInt("boost/timerUsed") + 1));
                    updatedValue = (((100f / startedValue) / 100f));
                    boostFG.fillAmount -= updatedValue;
                    PlayerPrefs.SetInt("boosts", (PlayerPrefs.GetInt("boosts") - 1));
                    PlayerPrefs.Save();
                    booster.startBoost();
                }
                else
                {
                    boostBundleOpened = true;
                    boostPanel.SetActive(true);
                    CashBgStatus(true);
                    Time.timeScale = 0.0f;
                    if (PlayerPrefs.GetInt("SoundOff") == 0)
                    {
                        AudioListener.volume = 0.0f;
                    }
                }
            }
        }
        

    }
    public void WeaponFire()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        if (pistolSelected == 0)
        {
            if (pistolAttackOnce)
            {
                if (PlayerPrefs.GetInt("ammos") > 0)
                {
                    resetAll();
                    isPistolAttack = true;
                    turnPlayerOff();
                    pistolAttack();
                    pistolAttackOnce = false;
                }
                else
                {
                    ammoPanel.SetActive(true);
                    ammoBundleOpened = true;
                    CashBgStatus(true);
                    Time.timeScale = 0f;
                    if (PlayerPrefs.GetInt("SoundOff") == 0)
                    {
                        AudioListener.volume = 0.0f;
                    }
                }
            }
        }
        else if (pistolSelected == 1)
        {
            if (shotgunAttackOnce)
            {
                if (PlayerPrefs.GetInt("ammos") > 0)
                {
                    resetAll();
                    isShotgunAttack = true;
                    turnPlayerOff();
                    shotgunAttack();
                    shotgunAttackOnce = false;
                }
                else
                {
                    ammoPanel.SetActive(true);
                    ammoBundleOpened = true;
                    CashBgStatus(true);
                    Time.timeScale = 0f;
                    if (PlayerPrefs.GetInt("SoundOff") == 0)
                    {
                        AudioListener.volume = 0.0f;
                    }
                }
            }
        }
        else if (pistolSelected == 2)
        {
            if (PlayerPrefs.GetInt("missile") > 0)
            {
                if (missileOnce)
                {
                    resetAll();
                    missileAttack = true;
                    turnPlayerOff();
                    missileAttackFtn();
                    missileOnce = false;
                }
            }
            else
            {
                missilePanel.SetActive(true);
                missileBundleOpened = true;
                CashBgStatus(true);
                Time.timeScale = 0f;
                if (PlayerPrefs.GetInt("SoundOff") == 0)
                {
                    AudioListener.volume = 0.0f;
                }
            }
        }
    }
    public void AxeAttack_Right()
    {
        if (batSelected == 1)
        {
            if (axeRightOnce)
            {
                if (PhycamViews.counter == 11 || PhycamViews.counter == 12)
                {
                    PhycamViews.counter = 2;
                    mainCamera.changeCamView();
                }
                GetComponent<AudioSource>().PlayOneShot(axeSound);
                resetAll();
                axeRightAttack = true;
                turnPlayerOff();
                BatAttack();
                axeRightOnce = false;
            }

        }
        else if (batSelected == 0 || batSelected == 2)
        {
            if (batRightOnce)
            {
                if (PhycamViews.counter == 11 || PhycamViews.counter == 12)
                {
                    PhycamViews.counter = 2;
                    mainCamera.changeCamView();
                }
                GetComponent<AudioSource>().PlayOneShot(batSound);
                resetAll();
                batRightOnce = false;
                BatRightAttack = true;
                turnPlayerOff();
                BatAttack();
            }
        }
    }
    public void AxeAttack_Left()
    {
        if (batSelected == 0 || batSelected == 2)
        {
            if (batLeftOnce)
            {
                if (PhycamViews.counter == 11 || PhycamViews.counter == 12)
                {
                    PhycamViews.counter = 2;
                    mainCamera.changeCamView();
                }
                GetComponent<AudioSource>().PlayOneShot(batSound);
                resetAll();
                batLeftOnce = false;
                BatLeftAttack = true;
                turnPlayerOff();
                BatAttack();
            }
        }
        else if (batSelected == 1)
        {
            if (axeLeftOnce)
            {
                if (PhycamViews.counter == 11 || PhycamViews.counter == 12)
                {
                    PhycamViews.counter = 2;
                    mainCamera.changeCamView();
                }
                GetComponent<AudioSource>().PlayOneShot(axeSound);
                resetAll();
                axeLeftAttack = true;
                turnPlayerOff();
                BatAttack();
                axeLeftOnce = false;
            }
        }

    }
    void CashBgStatus(bool isActive)
    {
        cashBg.SetActive(isActive);
        ShowCash();
    }
    void ShowCash()
    {
        totalCoinsText.text = PlayerPrefs.GetInt("cash") + string.Empty;
    }
}
