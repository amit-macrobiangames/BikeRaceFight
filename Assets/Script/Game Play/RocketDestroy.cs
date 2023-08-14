using UnityEngine;
public class RocketDestroy : MonoBehaviour
{

    // Use this for initialization
    public static GameObject bike;
    //public AudioClip hitSound;
    //	public GameObject explosion;
    public static bool hit = false;
    //	public AudioClip explosionSound;
    //	bool shieldOn;
    void Awake()
    {
        //shieldOn = false;
        Destroy(gameObject, 12);
        bike = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(bike.transform.position + new Vector3(Random.Range(-1.2f, 1.2f), 0, 0));
    }

    void Update()
    {
        //print ((transform.position.z- bike.transform.position.z));
        if ((transform.position.z - bike.transform.position.z) < -0.5f)
        {
            heavyBikeTurnControls.score += 50;
            turnLevelcontrols.score += 50;
            heavyBikeTurns.opponentCrashedCash += 50;
            heavyBikeTurns.score += 50;
            //			print (heavyBikeTurnControls.score);
            Destroy(gameObject);
        }
        if (!hit)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 50);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            //			ContactPoint contact = col.contacts[0];
            //			if (Application.loadedLevelName.Contains ("endlessMode")) {
            //				shieldOn=endlessmodeControl.shieldOn;
            //			} else if (Application.loadedLevelName.Contains ("heavybikeGame")) {
            //				shieldOn=endlessTallguyControl.shieldOn;
            //			}
            //			print (shieldOn);
            //			if(!shieldOn)
            //			{
            //			if(PlayerPrefs.GetInt ("SoundOff")==0)
            //			{
            //				bike.audio.PlayOneShot(explosionSound);
            //			}
            //				//Instantiate (explosion, contact.point + (contact.normal * 1.0f) , Quaternion.identity);
            //				Instantiate (explosion, contact.point  , Quaternion.identity);

            //			}
            hit = true;
            Destroy(gameObject);
        }

    }




}
