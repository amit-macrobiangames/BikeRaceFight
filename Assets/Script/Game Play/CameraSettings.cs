using UnityEngine;
using System.Collections;

public class CameraSettings : MonoBehaviour
{
	
	
	public  Transform target,player;
	public float smooth = 0.3f;
	public float distance = 5.0f;
	  public float haight =1.0f;
	public float Angle = 20;
	
	public static bool reverse;
	private float yVelocity = 0.0f;
	public static bool rampstart, rampstart2, rampstart3;
	public static bool jumpstart, jumpstart2, jumpstart3;
	//public static int Switch;
	private float backAngle = 0;
	//public static bool afterboost; 

	void Start()
	{
		haight = 1f;
		reverse = false;
		rampstart = false;
		rampstart2 = false;
		rampstart3 = false;

	}
	
	
	
	
	void Update()
	{

			
		//print (jumpstart+ " : "+ jumpstart2+ " "+ jumpstart3+ " "+ rampstart+ " "+ rampstart2+ " "+ rampstart3);

				if (reverse)
				{
					backAngle = 180;
				distance=8f;
			haight=0f;
			Angle=32f;
					
				}








				else if(jumpstart){
			haight=0.5f;
				
			//distance=4f;
			backAngle = 50;
				}


				else if(jumpstart2){
			haight=0.5f;
			//distance=4f;
			//backAngle = 220;
			backAngle = -50;

				}
				

		else if(jumpstart3){
					haight=0.5f;
			//distance=4f;	
			backAngle = -40;
				}








		else if(rampstart){
			haight=0f;
			
			backAngle = -70;
		
		}
		else if(rampstart2){
			haight=0f;
		backAngle = -90;
		
			
			
		}
		else if(rampstart3){
			haight=0f;
			backAngle = 70;

		}
				else 
				{
					backAngle = 0;
			haight=1f;
			//distance=5f;
				}
			

			//backAngle=120;
			
			//120
			
			float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y,target.eulerAngles.y + backAngle, ref yVelocity, smooth);
			
			
			

			
			Vector3 position = target.position;
			
			position += Quaternion.Euler(50, yAngle, 0) * new Vector3(0, 0, 5);
			
			
			transform.eulerAngles = new Vector3(Angle, yAngle, 0); 
			var direction = transform.rotation * -Vector3.forward;
			
			
				var targetDistance = AdjustLineOfSight(target.position + new Vector3(0,haight, 0), direction);
				
				transform.position = target.position + new Vector3(0, haight, 0) + direction * targetDistance;
			

			
			
		
	}
	
	
	
	
	public LayerMask lineOfSightMask = 0;
	
	
	float AdjustLineOfSight(Vector3 target, Vector3 direction)
	{
		
		
		RaycastHit hit;
		
		if (Physics.Raycast(target, direction, out hit, distance, lineOfSightMask.value))
			return hit.distance;
		else
			return distance;
		
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}
