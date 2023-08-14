using UnityEngine;
using System.Collections;

public class startMove : MonoBehaviour
{
    public int hitCounter;
    public bool missileHit;
    Transform player;
    public Transform finishLine;
    public bool startMoving;
    public bool carGroup;

    public LensFlare laser, subtle;
    // Use this for initialization
    void Start()
    {
        hitCounter = 0;
        startMoving = false;
        missileHit = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (!finishLine)
            finishLine = GameObject.FindGameObjectWithTag("Finish").transform;
    }

    
    void Update()
    {
        if (transform.position.z - player.position.z <= 10f && transform.position.z - player.position.z >= -0.5f)
        {
            //print("inside");
            if (laser.brightness < 0.33f)
                laser.brightness += 0.01f;
            if (subtle.brightness < 0.33f)
                subtle.brightness += 0.01f;
        }
        else
        {
            laser.brightness = 0.15f;
            subtle.brightness = 0.15f;
        }
        if (!missileHit)
        {
            if (finishLine.position.z - transform.position.z <= 2f)
            {
                startMoving = false;
            }
            else if (transform.position.z - player.position.z <= 40f)
            {
                startMoving = true;
            }
            else if (transform.position.z - player.position.z <= -3f || transform.position.z - player.position.z > 40f)
            {
                startMoving = false;
            }
        }
        else
        {
            startMoving = false;
        }
    }
}
