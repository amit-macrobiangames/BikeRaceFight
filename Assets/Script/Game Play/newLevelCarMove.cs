using UnityEngine;
using System.Collections;

public class newLevelCarMove : MonoBehaviour
{


    public float currentLane;
    public bool moveTowardLane1, moveTowardLane2, moveTowardLane3, moveTowardLane4;
    float Lane1, Lane2, Lane3, Lane4, target;
    public Transform player;

    public bool checkForLaneChange;

    string move;

    float normalSpeed, tiltSpeed;
    public Transform leftIndicator, rightIndicator, leftspotlight, rightspotlight;
    int levelNumber;
    public bool movingLeft, movingRight;
    bool turnLeftIndicatorOn, turnrightIndicatorOn;
    float myTimer;//,playerSpeed;

    public Transform WheelFL, wheelRL, wheelFR, wheelRR;
    bool keepMoving;
    public bool leftLane;
    startMove script;
    public AudioClip carPassingBy;
    // Use this for initialization
    void Start()
    {

        levelNumber = PlayerPrefs.GetInt("levels");
        //levelNumber = 4;

        keepMoving = true;
        currentLane = transform.parent.position.x;
        Lane1 = 1.46f;
        Lane2 = 2.42f;
        Lane3 = 3.32f;
        Lane4 = 4.11f;

        move = "start";

        script = transform.parent.GetComponent<startMove>();


        leftIndicator.gameObject.SetActive(false);
        rightIndicator.gameObject.SetActive(false);
        leftspotlight.gameObject.SetActive(false);
        rightspotlight.gameObject.SetActive(false);

        turnLeftIndicatorOn = false;
        turnrightIndicatorOn = false;
        myTimer = 1f;
        if (levelNumber == 4 || levelNumber == 14)
        {
            checkForLaneChange = true;

        }

        if (bikeSelection.bikeCount == 2 || bikeSelection.bikeCount == 1)
        {
            normalSpeed = 5f;
            tiltSpeed = 0.3f;
            //	playerSpeed=65f;	

        }
        else if (bikeSelection.bikeCount == 4 || bikeSelection.bikeCount == 3 || bikeSelection.bikeCount == 7)
        {
            normalSpeed = 5f;
            tiltSpeed = 0.3f;
            //	playerSpeed=75f;	
        }
        else if (bikeSelection.bikeCount == 5 || bikeSelection.bikeCount == 6 || bikeSelection.bikeCount == 8)
        {
            normalSpeed = 5f;
            tiltSpeed = 0.3f;
            //playerSpeed=85f;	


        }
        else if (bikeSelection.bikeCount == 9)
        {
            normalSpeed = 5.5f;
            tiltSpeed = 0.35f;
            //	playerSpeed=95f;	

        }
        else if (bikeSelection.bikeCount == 10)
        {
            normalSpeed = 5.5f;
            tiltSpeed = 0.35f;

            //playerSpeed=105f;	
        }



    }

    bool reset = true;
    void leftIndicatorSetting()
    {


        if (reset)
        {
            leftIndicator.gameObject.SetActive(true);
        }
        else
        {
            leftIndicator.gameObject.SetActive(false);
        }
        reset = !reset;
    }
    void rightIndicatorSetting()
    {


        if (reset)
        {
            rightIndicator.gameObject.SetActive(true);
        }
        else
        {
            rightIndicator.gameObject.SetActive(false);
        }
        reset = !reset;
    }

    void laneReset()
    {

        checkForLaneChange = true;
        rightspotlight.gameObject.SetActive(false);
        leftspotlight.gameObject.SetActive(false);
        currentLane = target;
        moveTowardLane1 = moveTowardLane2 = moveTowardLane3 = moveTowardLane4 = false;
        if (currentLane < Lane4 && currentLane > Lane2)
        {
            if (Random.Range(0, 10) % 2 == 0)
                moveTowardLane2 = true;
            else
                moveTowardLane4 = true;

        }
        if (currentLane < Lane3 && currentLane > Lane1)
        {
            if (Random.Range(0, 10) % 2 != 0)
                moveTowardLane3 = true;
            else
                moveTowardLane1 = true;

        }
        if (currentLane <= Lane1 || currentLane < Lane2)
        {
            moveTowardLane2 = true;


        }
        if (currentLane >= Lane4 || currentLane > Lane3)
        {
            moveTowardLane3 = true;


        }

    }



    void checkForIndicator()
    {
        rightIndicator.gameObject.SetActive(false);
        leftIndicator.gameObject.SetActive(false);
        if (currentLane > target)
        {
            turnLeftIndicatorOn = true;
            turnrightIndicatorOn = false;
            movingLeft = true;
            movingRight = false;
            leftIndicator.gameObject.SetActive(true);
            leftspotlight.gameObject.SetActive(true);
            leftLane = false;
        }
        else if (currentLane < target)
        {
            turnLeftIndicatorOn = false;
            rightIndicator.gameObject.SetActive(true);
            rightspotlight.gameObject.SetActive(true);
            turnrightIndicatorOn = true;
            movingRight = true;
            movingLeft = false;
            leftLane = true;
        }
        else
        {
            movingRight = false;
            movingLeft = false;
            turnLeftIndicatorOn = false;
            turnrightIndicatorOn = false;
            rightIndicator.gameObject.SetActive(false);
            rightspotlight.gameObject.SetActive(false);
            leftIndicator.gameObject.SetActive(false);
            leftspotlight.gameObject.SetActive(false);

        }
    }

    RaycastHit hit = new RaycastHit();

    void turnOffIndicator()
    {
        rightIndicator.gameObject.SetActive(false);
        leftIndicator.gameObject.SetActive(false);
        turnrightIndicatorOn = false;
        turnLeftIndicatorOn = false;

    }
    void Update()
    {


        //Debug.DrawRay (transform.position + new Vector3 (0.0f, 0.25f, 1.0f), Vector3.forward, Color.red);
        if (Physics.Raycast(transform.position + new Vector3(0.0f, 0.2f, 1.0f), Vector3.forward, out hit, 0.75f))
        {
            //	Debug.DrawRay (transform.position + new Vector3 (0.6f, 1f, 4.0f), Vector3.forward*3, Color.red);
            //			print (hit.collider.gameObject.layer+" " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.layer == 14 && (hit.collider.name.Contains("Car") || hit.collider.name.Contains("barrel")))
            {
                hit.transform.parent.Translate(Vector3.back * 65);
                hit.transform.parent.gameObject.SetActive(false);

            }
        }

        if (script.startMoving)
        {

            if (checkForLaneChange)
            {

                checkForLaneChange = false;

                if (moveTowardLane1)
                {


                    target = Lane1;
                    checkForIndicator();
                }
                else if (moveTowardLane2)
                {

                    target = Lane2;
                    checkForIndicator();
                }
                else if (moveTowardLane3)
                {


                    target = Lane3;
                    checkForIndicator();
                }
                if (moveTowardLane4)
                {


                    target = Lane4;
                    checkForIndicator();
                }

                //								if (leftLane) {
                //
                //										turnLeftIndicatorOn = true;
                //										turnrightIndicatorOn = false;
                //										movingLeft = true;
                //										leftIndicator.gameObject.SetActive (true);
                //										leftspotlight.gameObject.SetActive (true);
                //										leftLane = false;
                //								} else {
                //										turnLeftIndicatorOn = false;
                //										rightIndicator.gameObject.SetActive (true);
                //										rightspotlight.gameObject.SetActive (true);
                //										turnrightIndicatorOn = true;
                //										movingRight = true;
                //										leftLane = true;
                //
                //								} 

                Invoke("laneReset", 20f);
            }

            if (turnLeftIndicatorOn)
            {
                myTimer -= Time.deltaTime;
                if (myTimer < 0.8f)
                {
                    myTimer = 1.0f;
                    leftIndicatorSetting();
                }
            }
            else if (turnrightIndicatorOn)
            {
                myTimer -= Time.deltaTime;
                if (myTimer < 0.8f)
                {
                    myTimer = 1.0f;
                    rightIndicatorSetting();
                }
            }


            if (movingLeft)
            {

                if (transform.parent.position.x > target)
                {

                    transform.parent.Translate(Vector3.right * tiltSpeed * Time.deltaTime);
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(transform.localEulerAngles.y, -4, 3 * Time.deltaTime), transform.localEulerAngles.z);
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Mathf.LerpAngle(transform.localEulerAngles.z, -1.0f, 3 * Time.deltaTime));
                }
                if (transform.parent.position.x <= target)
                {
                    //turnLeftIndicatorOn = false;
                    Invoke("turnOffIndicator", 1.5f);
                    //leftIndicator.gameObject.SetActive (false);
                    movingLeft = false;
                }
            }
            else if (movingRight)
            {
                if (transform.parent.position.x < target)
                {
                    transform.parent.Translate(Vector3.left * tiltSpeed * Time.deltaTime);
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(transform.localEulerAngles.y, 4, 3 * Time.deltaTime), transform.localEulerAngles.z);
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Mathf.LerpAngle(transform.localEulerAngles.z, 1.0f, 3 * Time.deltaTime));
                }
                if (transform.parent.position.x >= target)
                {

                    Invoke("turnOffIndicator", 1.5f);
                    //		rightIndicator.gameObject.SetActive (false);

                    movingRight = false;
                }
            }
            else
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(transform.localEulerAngles.y, 0, 3 * Time.deltaTime), transform.localEulerAngles.z);
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Mathf.LerpAngle(transform.localEulerAngles.z, 0, 3 * Time.deltaTime));

            }


        }

        move = endlessmodeGraphics.gameMode;




        if (script.startMoving)
        {
            //	print (keepMoving+"  "+(transform.position.z - player.position.z));
            if (keepMoving)
            {

                if (!move.Equals("Pause") && !move.Equals("GameOver"))
                {

                    WheelFL.Rotate(-1000 * Time.deltaTime, 0, 0);
                    wheelRL.Rotate(-1000 * Time.deltaTime, 0, 0);
                    wheelRR.Rotate(-1000 * Time.deltaTime, 0, 0);
                    wheelFR.Rotate(-1000 * Time.deltaTime, 0, 0);
                    //					
                    transform.parent.Translate(Vector3.back * normalSpeed * Time.deltaTime);



                }
                if (transform.parent.position.z - player.position.z <= -1f)
                {
                    rightspotlight.gameObject.SetActive(false);
                    leftspotlight.gameObject.SetActive(false);
                    keepMoving = false;


                }
            }
        }




    }



}
