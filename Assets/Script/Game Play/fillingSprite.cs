using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class fillingSprite : MonoBehaviour
{
    public Text Helmets;
    public endlessmodeGraphics gameOverMode;
    float adder = 0.0f;
    bool reset;
    public static float deltaTime;
    private static float _lastframetime;
    // Use this for initialization
    void Start()
    {
        _lastframetime = Time.realtimeSinceStartup;
        //	Helmets.text=PlayerPrefs.GetInt("helmets")+"X";
    }
    public void revivalStart()
    {

        reset = true;
        //Helmets.text=PlayerPrefs.GetInt("helmets")+"X";
    }

    // Update is called once per frame
    void Update()
    {
        if (endlessmodeGraphics.gameMode.Equals("Revival"))
        {
            if (PlayerPrefs.GetInt("helmets") >= 0)
                Helmets.text = PlayerPrefs.GetInt("helmets") + "X";
            else
                Helmets.text = "0" + "X";
            if (reset)
            {
                _lastframetime = Time.realtimeSinceStartup;
                transform.GetComponent<Image>().fillAmount = 0;
                adder = 0;
                reset = false;
            }
            //			print (Time.realtimeSinceStartup+"  "+deltaTime+ "   "+_lastframetime+ "   "+adder);
            deltaTime = (Time.realtimeSinceStartup - _lastframetime) / 10;
            _lastframetime = Time.realtimeSinceStartup;

            if (!revivePlayer.stopTimer)
            {
                if (adder <= 1f)
                {
                    adder += deltaTime;



                    if (adder > 1)
                        adder = 1;
                    refill();

                }
                if (adder >= 1)
                {

                    endlessmodeGraphics.gameMode = "GameOver";
                    gameOverMode.gameOverFtn();
                    transform.parent.gameObject.SetActive(false);


                }

            }
        }
    }

    void refill()
    {
        if (endlessmodeGraphics.gameMode.Equals("Revival"))
            transform.GetComponent<Image>().fillAmount = adder;
    }
}
