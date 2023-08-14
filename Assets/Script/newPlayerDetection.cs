using UnityEngine;
using System.Collections;

public class newPlayerDetection : MonoBehaviour {
	public Transform otherTransform;

	newLevelHarley script1;
	string childName;
	public bool leftBike;
	// Use this for initialization
	void Start () {

			script1=transform.GetComponent<newLevelHarley>();	
			childName="opponentFatguy";


	}
	
	// Update is called once per frame
	void Update () {
		var relativePoint = transform.InverseTransformPoint(otherTransform.position);


	
//			print ("relativex  "+relativePoint.x);
		if (relativePoint.x < -0.35f) {
		
			if (leftBike) {
		
					//print ("player on right side ");
				if(script1.FollowPlayer)
				{
					transform.position-=new Vector3(0,0,5);
					transform.position=new Vector3(0.95f,transform.position.y,transform.position.z);
				}

			}
				
				script1.playerLeftSide=true;
				script1.playerRightSide=false;
				script1.playerAhead=false;
				transform.Find(childName).GetComponent<knockOutControl>().left=false;
			//	print ("Object is to the left "+ relativePoint.x);

			
		} 
		else if (relativePoint.x > 0.35f) {

		//	print ("Object is to the right "+ relativePoint.x);
			if (!leftBike) {

					//print ("player on left");
				if(script1.FollowPlayer)
				{
					transform.position-=new Vector3(0,0,5);
					transform.position=new Vector3(4.75f,transform.position.y,transform.position.z);
				}
			}
				
				script1.playerLeftSide=false;
				script1.playerRightSide=true;
				script1.playerAhead=false;
				transform.Find(childName).GetComponent<knockOutControl>().left=true;

		} 
		else {
		//	print ("Object is directly ahead "+ relativePoint.x);

				script1.playerAhead=true;
				script1.playerLeftSide=false;
				script1.playerRightSide=false;

		}
	}
}




