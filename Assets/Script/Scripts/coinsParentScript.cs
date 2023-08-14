using UnityEngine;

public class coinsParentScript : MonoBehaviour
{
    public bool resetChild;
    //public Transform player;
    public float upperRange, lowerRange;
    // Use this for initialization
    public Transform levelClearCheckPoint;
    float xValue;

    // Update is called once per frame
    void Update()
    {
        //		if (tiltControl.startTiltAfterCam) {
        //			transform.position+=new Vector3(0,0,7.5f*Time.deltaTime);		
        //		}
        //		print ((levelClearCheckPoint.transform.position.z- transform.position.z));
        if ((transform.position.z + 20) < endlessmodeGraphics.instance.player.position.z)
        {// player.position.z
            resetChild = true;
            xValue = Random.Range(lowerRange, upperRange);
            transform.position += new Vector3(0, 0, 200);
            transform.position = new Vector3(xValue, transform.position.y, transform.position.z);
            if ((levelClearCheckPoint.transform.position.z - transform.position.z) < 0)
            {
                gameObject.SetActive(false);
            }
            Invoke("resetOff", 1f);
        }
    }

    void resetOff()
    {
        resetChild = false;
    }
}
