using UnityEngine;
using System.Collections;

public class isMove : MonoBehaviour {
	Transform player;
	Vector3 pos;
    public bool bikerAhead;
    public static bool rotateTire;
	public Transform WheelFL,wheelRL,wheelFR,wheelRR;
	int carWheelType;
	public bool opposite;
	Transform oppositeCarToMove;
	bool bike,low;

	// Use this for initialization
	void Start () {
		rotateTire = false;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		pos = transform.localPosition;
		opposite=false;
		if (transform.parent.name.Contains ("opposite")) {
			opposite=true;	
			oppositeCarToMove=transform.parent.GetChild(0).transform;
		}
//		if (transform.name.Equals ("Model_Trucks_CementTruck") || transform.name.Equals("dumper_truck") || transform.name.Equals("Red_Car") || transform.name.Equals("Van_deliver")) {
//			carWheelType=1;
//		}
		 if (transform.name.Contains ("bike")) {
			bike=true;		
		}
		if (transform.name.Contains ("Low")) {
			low=true;		
		}


	}
	
	// Update is called once per frame
	void Update () {
	 
		if (transform.position.z - player.position.z <= - 10f) {
		
			//Destroy (transform.gameObject);
			//transform.localPosition = pos;
			rotateTire=false;


				GetComponent<AudioSource>().Pause();
				GetComponent<AudioSource>().enabled=false;
				
			


		}
	
		if (!endlessmodeGraphics.gameMode.Equals ("Idle")) {
					
								GetComponent<AudioSource>().Pause ();
								GetComponent<AudioSource>().enabled = false;
			
						
				}
		if (rotateTire) {


		if(bike)
			{

				WheelFL.Rotate(0,0,400*Time.deltaTime);	
				wheelRL.Rotate(0,0,400*Time.deltaTime);	
				wheelRR.Rotate(0,0,400*Time.deltaTime);	
				wheelFR.Rotate(0,0,400*Time.deltaTime);	

			}
			else if (low )
			{

				WheelFL.Rotate(0,0,200*Time.deltaTime);	
				wheelRL.Rotate(0,0,200*Time.deltaTime);	
				wheelRR.Rotate(0,0,200*Time.deltaTime);	
				wheelFR.Rotate(0,0,200*Time.deltaTime);
				
			}
			else
			{

			WheelFL.Rotate(200*Time.deltaTime,0,0);	
			wheelRL.Rotate(200*Time.deltaTime,0,0);	
			wheelRR.Rotate(200*Time.deltaTime,0,0);	
			wheelFR.Rotate(200*Time.deltaTime,0,0);	
			}
			if(opposite)
			{
				oppositeCarToMove.GetComponent<oppositeCar>().translate=true;
				opposite=false;
			}

		}
	

	}

    void OnTriggerEnter(Collider col)
    {
        if (col.name.Contains("carTrigger"))
        {
            int random = Random.Range(0, 6);
            if (random % 2 == 0)
            {
                findTarget.carBrake = true;
            }
            else
            {
                findTarget.carBrake = false;

            }
        }
        print(findTarget.carBrake);
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag.Contains("Opponent"))
        {
            bikerAhead = true;
            Invoke("startAfterBiker", 1.5f);
        }
    }
    void startAfterBiker()
    {
        bikerAhead = false;
    }
}
