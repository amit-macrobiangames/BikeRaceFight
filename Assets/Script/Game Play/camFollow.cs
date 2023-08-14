using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class camFollow : MonoBehaviour
{
    public float intendedHaight = 3.0f;
    public float intendedDistance = 3.0f;
    public float intendedBackAngle = 270;

    public float intendedxPosition = 0.0f;


    public Transform target, player;
    public float smooth = 0.3f;
    public float distance = 5.0f;
    public float haight = 3.0f;
    public float Angle = 20;

    public static bool reverse;
    private float yVelocity = 0.0f;

    public static int Switch;
    private float backAngle = 270;
    public float lerpTime;
    public static bool gangster = false;
    private bool isBip01 = false;
    //public static bool afterboost; 
    void Start()
    {
        //GameObject.Find ("AnimationCamera");
        reverse = false;
        lerpTime = 0.25f;
        gangster = false;
        //	choose = true;


        if (SceneManager.GetActiveScene().name.Contains("heavybikeGame"))
        {
            intendedHaight = 3.0f;
            intendedDistance = 6.0f;
            intendedxPosition = 0f;

        }
        if (!target.name.Contains("Bip01"))
        {

            isBip01 = false;
        }
        else if (target.name.Contains("Bip01"))
        {

            isBip01 = true;
        }

    }


    //bool choose;

    void LateUpdate()
    {


        //transform.position = new Vector3(target.transform.position.x , transform.position.y , target.transform.position.z - distance);
        if (gangster)
        {
            //				if(choose)
            //				{
            //					int random=Random.Range(0,20);
            ////					print (random);
            //					if(random<5) // correct
            //						backAngle = 110;
            //					else if(random<10 && random>=5)
            //						backAngle=70; // correct
            //					else if(random<15 && random>=10)//correct
            //						backAngle=200;
            //					else
            //						backAngle=270;
            //					distance=12f;
            //					choose=false;
            //					smooth=0.3f;
            //					haight=1.5f;
            //				}
        }
        else if (reverse)
        {
            backAngle = intendedBackAngle;
            //backAngle = Mathf.Lerp (backAngle, intendedBackAngle, lerpTime);
            distance = Mathf.Lerp(distance, intendedDistance, lerpTime);
            haight = Mathf.Lerp(haight, intendedHaight, lerpTime);
            //distance=3f;
            //haight=0.5f;

        }
        else
        {
            backAngle = intendedBackAngle;
            //backAngle = Mathf.Lerp (backAngle, intendedBackAngle, lerpTime);
            //choose=true;
            //smooth=0.3f;

            distance = Mathf.Lerp(distance, intendedDistance, lerpTime);
            haight = Mathf.Lerp(haight, intendedHaight, lerpTime);
        }




        float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.localEulerAngles.z + backAngle, ref yVelocity, smooth);



        // Position at the target
        Vector3 position = target.position;
        // Then offset by distance behind the new angle
        //	position += Quaternion.Euler (0, 50, yAngle) * new Vector3 (0, 0, 5);
        position += Quaternion.Euler(0, 50, yAngle) * new Vector3(0, 0, intendedDistance);


        transform.eulerAngles = new Vector3(Angle, yAngle, 0); //32.06993f

        var direction = transform.rotation * -Vector3.forward;


        if (!isBip01)
		//isBip01 target.name.Contains("Bip01")
        {
            distance = intendedDistance;
            var targetDistance = AdjustLineOfSight(target.position + new Vector3(0, haight, 0), direction);


            transform.position = target.position + new Vector3(intendedxPosition, haight, 0) + direction * targetDistance;

        }
        else if (isBip01)
        {
            distance = 0.75f;
            var targetDistance = AdjustLineOfSight(target.position + new Vector3(0, 0.25f, 0), direction);


            transform.position = target.position + new Vector3(0, 0.25f, 0) + direction * targetDistance;

        }




    }





    public LayerMask lineOfSightMask = 0;

    float camDistance;
    float AdjustLineOfSight(Vector3 target, Vector3 direction)
    {


        RaycastHit hit;
        camDistance = distance;
        if (distance < 0)
        {
            camDistance = -distance;
        }
        if (Physics.Raycast(target, direction, out hit, camDistance, lineOfSightMask.value))
            return hit.distance;


        else
            return distance;

    }














}
