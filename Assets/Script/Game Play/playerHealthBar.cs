using UnityEngine;
using UnityEngine.UI;
public class playerHealthBar : MonoBehaviour
{
    public GameObject barLineGameObject;
    public Text speedText, distanceText;
    public Transform target;
    //public Texture[] healthbar = new Texture[4];
    //private GUITexture healthComponent;
    public Slider healthSlider;
    int counter, tempHealth;
    bool playerDeath;
    string gameOver;
    int bikeNo;
    float levelClearLoc;
    float distance;
    public static float Remainingdistance;
    string distanceRemained;
    bool levelClear;
    public Transform levelClearTransform;
    bool ramp, jump;
    float speed;
    private heavyBikeTurns heavyBikeTurnsScript;

    void Start()
    {
        ramp = false;
        jump = false;
        heavyBikeTurnsScript = target.GetComponent<heavyBikeTurns>();
      
        target = heavyBikeTurnsScript.player.transform;

        // healthComponent = this.GetComponent<GUITexture>();
        tempHealth = 4;
        levelClearLoc = GetTargetPosition();
        levelClearTransform.position = new Vector3(2.85f, -0.075f, levelClearLoc);//135,630,1130
        if (target.name.Contains("Fat"))
        {
            bikeNo = 0;
        }
        else if (target.name.Contains("Player"))
        {
            bikeNo = 1;
        }
        InvokeRepeating("UpdateDistance", 1, 0.1f);
    }
    float GetTargetPosition()
    {
        float[] pos = new float[] { 0, -300, -250, -200, -150, -100, -50, -410, 200, 250, 300, 350, 450, 550, 650, 750, 750, 750, 500, 700, 900};
#if UNITY_EDITOR
        Debug.Log("Distance: " + pos[PlayerPrefs.GetInt("levels")]);
#endif
        return pos[PlayerPrefs.GetInt("levels")];
    }
    void SetHealthVal(int _health)
    {
        if (!tempHealth.Equals(_health))
        {
            tempHealth = _health;
            healthSlider.value = _health;
        }
    }
    void Update()
    {
        gameOver = endlessmodeGraphics.gameMode;
        counter = heavyBikeTurnsScript.counter;
        ramp = heavyBikeTurnsScript.flyoverStart;
        jump = heavyBikeTurnsScript.isJumping;

        if (bikeNo == 1)
        {
            levelClear = heavyBikeTurnControls.levelClear;
            playerDeath = heavyBikeTurnControls.isdead;
        }
        else
        {
            levelClear = turnLevelcontrols.levelClear;
            playerDeath = turnLevelcontrols.isdead;
        }
        if (levelClear)
        {
            gameObject.SetActive(false);
        }
        if (!playerDeath && !levelClear)
        {
            /*             if (counter <= 0)
                            healthComponent.texture = healthbar[0];
                        else if (counter == 1)
                            healthComponent.texture = healthbar[1];
                        else if (counter == 2)
                            healthComponent.texture = healthbar[2];
                        else if (counter >= 3)
                            healthComponent.texture = healthbar[3]; */
            if (counter <= 0)
                SetHealthVal(3);
            else if (counter == 1)
                SetHealthVal(2);
            else if (counter == 2)
                SetHealthVal(1);
            else if (counter >= 3)
                SetHealthVal(0);
        }
        else if (playerDeath)
        {
            SetHealthVal(0);
        }
        //print (ramp);
        if (gameOver.Equals("GameOver") || levelClear || jump || ramp)
        {
            if (healthSlider.enabled == true)
                healthSlider.enabled = false;
            //healthComponent.enabled = false;
        }
        else if (gameOver.Equals("Idle") && !jump && !ramp && !levelClear)
        {
            if (healthSlider.enabled == false)
                healthSlider.enabled = true;
            //healthComponent.enabled = true;
        }
        distance = Mathf.Round(levelClearLoc - target.position.z);
        Remainingdistance = distance;
        //distanceRemained = distance + " m";
        speed = Mathf.Round(target.root.GetComponent<Rigidbody>().velocity.magnitude * 7.5f);

        if (!ramp && !levelClear && counter != 3 && PhycamViews.counter != 11 && PhycamViews.counter != 12)
        {
            if (!barLineGameObject.activeInHierarchy)
                barLineGameObject.SetActive(true);
        }
        else
        {
            if (barLineGameObject.activeInHierarchy)
                barLineGameObject.SetActive(false);
        }

        //speedText.text = speed + "Km/h";
        //distanceText.text = distanceRemained;
    }
    void UpdateDistance()
    {
        distanceRemained = distance + " m";
        speedText.text = speed + "Km/h";
        distanceText.text = distanceRemained;
    }
}
