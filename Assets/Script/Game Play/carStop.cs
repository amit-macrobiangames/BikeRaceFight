using UnityEngine;

public class carStop : MonoBehaviour
{
    public static bool setBrakeValue;
    isMove script;
    public AudioClip brakeSound;
    Transform player;
    int levelID;
    public heavyBikeTurnControls playerScript;
    public turnLevelcontrols playerScript1;
    bool levelClear, playerCrashed;
    public bool shouldStop;
    bool playAudioOnce;
    public GameObject smoke;
    public bool twoCars;
    //float carRotation;
    //bool rotate;
    bool stopOnce;

    public bool stopRedCar;
    // Use this for initialization
    void Start()
    {
        playAudioOnce = true;

        stopOnce = true;

        //rotate = false;
        script = transform.parent.GetComponent<isMove>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        player = player.GetComponent<heavyBikeTurns>().player.transform;
        if (player.name.Contains("Fatguy"))
        {

            levelID = 1;

        }
        else
        {

            levelID = 0;


        }

        //	carRotation = transform.parent.eulerAngles.y-30f;
    }
    void Update()
    {
        if (levelID == 0)
        {
            //	print ("car stopper "+playerScript.crash);
            playerCrashed = playerScript.crash;

            levelClear = heavyBikeTurnControls.levelClear;

        }
        else if (levelID == 1)
        {

            playerCrashed = playerScript1.crash;
            levelClear = turnLevelcontrols.levelClear;



        }

        //		if (stopRedCar) {
        //			//	print (transform.parent.position.x);
        //			if(transform.parent.position.x>=-6f)
        //			{
        ////				print ("inside");
        //
        //				if (stopOnce) {
        //					stopOnce = false;
        //
        //					if (!playerCrashed && !levelClear) {
        //						
        //			
        //							if (transform.parent.position.z - player.root.position.z > 0) {
        //							//	rotate = true;
        //								
        //								findTarget.carBrake = true;
        //								transform.parent.audio.Pause ();
        //								
        //								pc.emit = false;
        //								script.rotateTire = false;
        //								if (twoCars) {
        //									pc1.emit = false;
        //								}
        //							}
        //						 
        //						
        //						Invoke ("setValue", 5f);
        //					}
        //				
        //				}
        //				
        //			}
        //		}


        //			if (rotate) {
        //			transform.parent.eulerAngles=new Vector3(transform.parent.eulerAngles.x,Mathf.Lerp (transform.parent.eulerAngles.y, carRotation, 1f),transform.parent.eulerAngles.z);	
        //		
        //		}

    }
    // Update is called once per frame

    //ParticleEmitter pc,pc1;
    void OnTriggerEnter(Collider col)
    {
        if (col.name.Contains("carSound"))
        {
            if (!playerCrashed && !levelClear)
            {
                if (shouldStop && playAudioOnce)
                {
                    player.GetComponent<AudioSource>().PlayOneShot(brakeSound);
                    smoke.gameObject.SetActive(true);
                    //pc = smoke.GetComponent<ParticleEmitter>();
                    //					print ("pc:"+ pc);
                    Invoke("smokeOff", 0.75f);
                    transform.parent.GetComponent<AudioSource>().Pause();
                    playAudioOnce = false;
                    if (twoCars)
                    {
                        Transform smoke1 = (Transform)Instantiate(smoke.transform, Vector3.zero, Quaternion.identity);
                        smoke1.transform.parent = transform.parent.GetChild(0).transform;
                        smoke1.localPosition = new Vector3(0, 1, 0);
                        //pc1=smoke1.GetComponent<ParticleEmitter>();
                    }

                }
            }
        }
        if (col.name.Contains("carStopper"))
        {
            if (stopOnce)
            {
                stopOnce = false;

                if (!playerCrashed && !levelClear)
                {

                    if (shouldStop)
                    {
                        //												print (transform.parent.position.z-player.root.position.z);
                        if (transform.parent.position.z - player.root.position.z > 0)
                        {
                            //	rotate = true;

                            findTarget.carBrake = true;
                            transform.parent.GetComponent<AudioSource>().Pause();
                            //							print (transform.parent.name);
                            // pc.emit = false;//khuram
                            isMove.rotateTire = false;
                            if (twoCars)
                            {
                                // pc1.emit = false;//khuram
                            }
                        }
                    }
                    else
                    {
                        findTarget.carBrake = false;

                    }
                    Invoke("smokeOff", 0.75f);
                    Invoke("setValue", 5f);
                }
                else
                {
                    findTarget.carBrake = false;
                }
            }
        }

        if (col.name.Contains("oppositeCarSound"))
        {

            if (!playerCrashed && !levelClear)
            {

                if (shouldStop)
                {
                    player.GetComponent<AudioSource>().PlayOneShot(brakeSound);
                    smoke.gameObject.SetActive(true);
                    //pc = smoke.GetComponent<ParticleEmitter>();
                    //print(pc.transform.parent);
                    Invoke("smokeOff", 0.75f);
                    transform.parent.GetComponent<AudioSource>().Pause();
                    if (twoCars)
                    {
                        Transform smoke1 = (Transform)Instantiate(smoke.transform, Vector3.zero, Quaternion.identity);
                        smoke1.transform.parent = transform.parent.GetChild(0).transform;
                        smoke1.localPosition = new Vector3(0, 1, 0);
                        //pc1=smoke1.GetComponent<ParticleEmitter>();
                    }

                }
            }
        }
        if (col.name.Contains("oppositeCarStopper"))
        {
            if (stopOnce)
            {
                stopOnce = false;

                if (!playerCrashed && !levelClear)
                {

                    if (shouldStop)
                    {

                        if (transform.parent.position.z - player.root.position.z > 0)
                        {
                            //	rotate = true;

                            transform.parent.GetComponent<AudioSource>().Pause();
                            transform.parent.GetComponent<oppositeCar>().translate = false;
                            transform.parent.GetComponent<oppositeCar>().rotateTire = false;
                            // pc.emit = false;//khuram

                            if (twoCars)
                            {
                                // pc1.emit = false;//khuram
                            }
                        }
                    }
                    Invoke("smokeOff", 0.75f);

                    Invoke("setValue", 5f);
                }
                else
                {
                    findTarget.carBrake = false;
                }
            }
        }
    }

    void smokeOff()
    {
        if (shouldStop)
        {
            // pc.emit = false;//khuram
            if (twoCars)
            {
                // pc1.emit = false;//khuram
            }
        }
    }
    void setValue()
    {

        findTarget.carBrake = false;

        //		if(rotate)
        //		transform.parent.eulerAngles += new Vector3 (0,30f,0);
        //		rotate = false;
        transform.parent.GetComponent<AudioSource>().Play();
        if (shouldStop)
        {
            // pc.emit = false;//khuram
            if (twoCars)
            {
                // pc1.emit = false;//khuram
            }
        }
    }

}
