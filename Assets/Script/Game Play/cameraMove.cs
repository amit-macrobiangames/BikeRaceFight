using UnityEngine;
using System.Collections;

public class cameraMove : MonoBehaviour
{
    public static cameraMove instance;
    public Transform camera1, car1;
    bool move;
    Vector3 velocity;
    Vector3 velocity1;
    Transform targetCam, targetCar;
    public Transform[] carsTar, cameras;
    public Transform Target;
    public float RotationSpeed, transformSpeed;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;

    void Awake()
    {
        instance = this;
        Target = carsTar[0];
        camera1 = cameras[0];
        move = true;
    }
    public void moveToward()
    {
        move = true;
    }

    public void cameraSwitchFtn(int carNum)
    {
        Target = carsTar[carNum];
        camera1 = cameras[carNum];
        move = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            //transform.LookAt(car1);
            _direction = (Target.position - transform.position).normalized;//.normalized;

            //create the rotation we need to be in to look at the target
            _lookRotation = Quaternion.LookRotation(_direction);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
            transform.position = Vector3.SmoothDamp(transform.position, camera1.position, ref velocity, .2f);

            //transform.eulerAngles=Vector3.SmoothDamp(transform.eulerAngles ,camera1.eulerAngles,ref velocity1,.1f);
            //transform.position=Vector3.MoveTowards(transform.position ,camera1.position,.1f);
            //	transform.eulerAngles=Vector3.RotateTowards(transform.eulerAngles ,camera1.eulerAngles,.1f,.5f);
            //transform.position = Vector3.Lerp (transform.position ,camera1.position,.1f);
            //transform.eulerAngles = Vector3.Lerp (transform.eulerAngles ,camera1.eulerAngles,.1f);
        }
    }
}
