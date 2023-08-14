using UnityEngine;



public class desertTrack : MonoBehaviour
{
    public endlessmodeGraphics levelClearMode;
    private Transform player;

    public PhycamViews mainCamera;

    public GameObject TimeFinishLine;
    public jeepControl jeep;
    public GameObject levelClearObject;
    //bool tunnel;

    //Vector3 position = new Vector3 (5f,-2.14f,0.0f);

    /* desert values 
	 * 
	 * 	Vector3 position = new Vector3 (10.0f,-8.3f,0.0f);

	 * 	public float endPoint = -288f;   
		public float other = 432.1f;
*/

    /* city large values 
	 * 
	 * 	Vector3 position = new Vector3 (10.0f,-8.3f,0.0f);

	 * 	public float endPoint = -223.55f;   
		public float other = 496.565f;
*/


    /* city large values 
	 * 
	 * 	Vector3 position = new Vector3 (10.0f,-8.3f,0.0f);

	 * 	public float endPoint = -630.63f;   
		public float other = 171.57f;
*/


    /* snow values 
	 * 
	 * 	Vector3 position = new Vector3 (10.0f,-8.3f,0.0f);

	 * 	public float endPoint = -501.93f;   
		public float other = 300.27f;
*/



    //	public float endPoint = -288f;   
    //	public float other = 432.1f;
    //	public GameObject[] Track = new GameObject[3];
    //
    //	GameObject previousTime;
    //	float distance;
    //
    //
    //
    //	bool triggered;
    ////

    GameObject trackfinale, childPlayer;


    void Start()
    {
        player = endlessmodeGraphics.instance.player;

        childPlayer = transform.GetComponent<heavyBikeTurns>().player;


        //				triggered = false;
        //				
        //
        //				instantiate = true;
        //			
        //				endPoint += other;
        //				
        //				previousTime = (GameObject)Instantiate ((GameObject)TimeFinishLine, new Vector3 (2.83f, 0, endPoint-100 ), Quaternion.identity);
        //				
        //				trackfinale = (GameObject)Track.GetValue (0);
        //				trackfinale.transform.position = new Vector3 (position.x, position.y, endPoint);
        //
        //				
        //				tunnel = true;
        //	
        //	

    }

    bool instantiateOnce;
    float distance;
    void Update()
    {

        distance = (levelClearObject.transform.position.z - player.position.z);

        //		print (distance);
        if (distance <= -1f)
        {
            if (playOnce)
            {

                if (PlayerPrefs.GetInt("levels") != 8 && PlayerPrefs.GetInt("levels") != 13 && PlayerPrefs.GetInt("levels") != 14)
                {

                    camFollow.reverse = true;
                    turnLevelcontrols.levelClear = true;
                    heavyBikeTurnControls.levelClear = true;

                    childPlayer.GetComponent<Animation>().Play("stunt2", PlayMode.StopAll);

                    int cam = Random.Range(0, 3);
                    if (cam == 0)
                        PhycamViews.counter = 15;
                    if (cam == 1)
                        PhycamViews.counter = 20;
                    if (cam == 2)
                        PhycamViews.counter = 19;

                    //PhycamViews.counter = 20;
                    //PhycamViews.counter = 19;//15
                    mainCamera.changeCamView();
                    Invoke("levelclear", 3f);
                }

                else if (PlayerPrefs.GetInt("levels") == 8)
                {
                    if (jeep.health <= 0)
                    {
                        camFollow.reverse = true;
                        turnLevelcontrols.levelClear = true;
                        heavyBikeTurnControls.levelClear = true;

                        childPlayer.GetComponent<Animation>().Play("stunt2", PlayMode.StopAll);

                        int cam = Random.Range(0, 3);
                        if (cam == 0)
                            PhycamViews.counter = 15;
                        if (cam == 1)
                            PhycamViews.counter = 20;
                        if (cam == 2)
                            PhycamViews.counter = 19;

                        //PhycamViews.counter = 20;
                        //PhycamViews.counter = 19;//15
                        mainCamera.changeCamView();
                        Invoke("levelclear", 2.7f);
                    }
                    else
                    {
                        turnLevelcontrols.timeover = true;
                        heavyBikeTurnControls.timeover = true;
                    }
                }
                else if (PlayerPrefs.GetInt("levels") == 13 || PlayerPrefs.GetInt("levels") == 14)
                {
                    if (weaponAI.noOfCarsHit <= 0)
                    {
                        camFollow.reverse = true;
                        turnLevelcontrols.levelClear = true;
                        heavyBikeTurnControls.levelClear = true;

                        childPlayer.GetComponent<Animation>().Play("stunt2", PlayMode.StopAll);

                        int cam = Random.Range(0, 3);
                        if (cam == 0)
                            PhycamViews.counter = 15;
                        if (cam == 1)
                            PhycamViews.counter = 20;
                        if (cam == 2)
                            PhycamViews.counter = 19;


                        mainCamera.changeCamView();
                        Invoke("levelclear", 2.7f);
                    }
                    else
                    {
                        turnLevelcontrols.timeover = true;
                        heavyBikeTurnControls.timeover = true;

                    }
                }
                playOnce = false;
            }









        }

        //						if (distance <= -5f && !triggered) {
        //								triggered = true;
        //
        //							
        //								
        //			
        //
        //
        //													
        //														
        //														endPoint += other;
        //														
        //														if (!tunnel) {
        //															
        //																trackfinale = (GameObject)Track.GetValue (1);
        //																trackfinale.transform.position = new Vector3 (position.x, position.y, endPoint);
        //																tunnel = !tunnel;
        //														} else if (tunnel) {
        //														
        //																
        //					trackfinale = (GameObject)Track.GetValue (2);
        //					trackfinale.transform.position=new Vector3 (position.x, position.y, endPoint);
        //																tunnel = !tunnel;		
        //														}
        //
        //							previousTime.transform.position=new Vector3(2.83f,0f,endPoint-100);
        //
        //														Invoke ("setting", 3.5f);
        //												
        //				
        //				
        //
        //								
        //						}








    }

    bool instantiate;
    void OnTriggerEnter(Collider col)
    {


        //				if (col.name.Contains ("TimeFinish")) {
        //					
        //								triggered = true;
        ////			print ("triggered");
        //
        //								if (instantiate) {
        //										
        //											
        //									
        //																
        //				
        //														
        //																
        //																
        //										endPoint += other;
        //										
        //										if (!tunnel) {
        //												
        //				
        //												trackfinale = (GameObject)Track.GetValue (1);
        //												trackfinale.transform.position = new Vector3 (position.x, position.y, endPoint);
        //												tunnel = !tunnel;
        //										} else if (tunnel) {
        //																		
        //											
        //						trackfinale = (GameObject)Track.GetValue (2);
        //						trackfinale.transform.position=new Vector3 (position.x, position.y, endPoint);
        //												//Instantiate ((GameObject)Track.GetValue (4), new Vector3 (position.x, position.y, endPoint), Quaternion.identity);
        //												tunnel = !tunnel;		
        //										}
        //												
        //
        //
        //							
        //										previousTime.transform.position = new Vector3 (2.83f, 0f, endPoint-100);
        //								
        //										instantiate = false;
        //										Invoke ("setting", 5f);
        //														 
        //														
        //
        //								}
        //								
        //
        //						
        //				}
        if (col.tag.Contains("Finish"))
        {


            if (playOnce)
            {

                if (PlayerPrefs.GetInt("levels") != 8 && PlayerPrefs.GetInt("levels") != 13 && PlayerPrefs.GetInt("levels") != 14)
                {

                    camFollow.reverse = true;
                    turnLevelcontrols.levelClear = true;
                    heavyBikeTurnControls.levelClear = true;

                    childPlayer.GetComponent<Animation>().Play("stunt2", PlayMode.StopAll);

                    int cam = Random.Range(0, 3);
                    if (cam == 0)
                        PhycamViews.counter = 15;
                    if (cam == 1)
                        PhycamViews.counter = 20;
                    if (cam == 2)
                        PhycamViews.counter = 19;

                    //PhycamViews.counter = 20;
                    //PhycamViews.counter = 19;//15
                    mainCamera.changeCamView();
                    Invoke("levelclear", 3f);
                }

                else if (PlayerPrefs.GetInt("levels") == 8)
                {
                    if (jeep.health <= 0)
                    {
                        camFollow.reverse = true;
                        turnLevelcontrols.levelClear = true;
                        heavyBikeTurnControls.levelClear = true;

                        childPlayer.GetComponent<Animation>().Play("stunt2", PlayMode.StopAll);

                        int cam = Random.Range(0, 3);
                        if (cam == 0)
                            PhycamViews.counter = 15;
                        if (cam == 1)
                            PhycamViews.counter = 20;
                        if (cam == 2)
                            PhycamViews.counter = 19;

                        //PhycamViews.counter = 20;
                        //PhycamViews.counter = 19;//15
                        mainCamera.changeCamView();
                        Invoke("levelclear", 2.7f);
                    }
                    else
                    {
                        turnLevelcontrols.timeover = true;
                        heavyBikeTurnControls.timeover = true;

                    }
                }
                else if (PlayerPrefs.GetInt("levels") == 13 || PlayerPrefs.GetInt("levels") == 14)
                {

                    if (weaponAI.noOfCarsHit <= 0)
                    {
                        camFollow.reverse = true;
                        turnLevelcontrols.levelClear = true;
                        heavyBikeTurnControls.levelClear = true;

                        childPlayer.GetComponent<Animation>().Play("stunt2", PlayMode.StopAll);

                        int cam = Random.Range(0, 3);
                        if (cam == 0)
                            PhycamViews.counter = 15;
                        if (cam == 1)
                            PhycamViews.counter = 20;
                        if (cam == 2)
                            PhycamViews.counter = 19;


                        mainCamera.changeCamView();
                        Invoke("levelclear", 2.7f);
                    }
                    else
                    {
                        turnLevelcontrols.timeover = true;
                        heavyBikeTurnControls.timeover = true;

                    }
                }
                playOnce = false;
            }







        }

    }



    void gameOver()
    {
        endlessmodeGraphics.gameMode = "GameOver";
        levelClearMode.gameOverFtn();

    }
    bool playOnce = true;

    //	void setting()
    //	{
    //		instantiate = true;
    //		triggered = false;
    //	}


    void levelclear()
    {
        endlessmodeGraphics.gameMode = "LevelComplete";
        levelClearMode.levelCompleteFtn();

    }


}