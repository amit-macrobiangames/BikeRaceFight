using UnityEngine;
using System.Collections;

public class level1PlayerDetection : MonoBehaviour {
	public Transform otherTransform;

	level1Harley script1;
	string childName;

	// Use this for initialization
	void Start () {

			script1=transform.GetComponent<level1Harley>();	
			childName="opponentFatguy";


	}
	
	// Update is called once per frame
	void Update () {
		var relativePoint = transform.InverseTransformPoint(otherTransform.position);
			print (relativePoint.x);
		if (relativePoint.x <= -2.25f) {
			//print ("Object is to the left "+ relativePoint.x);
			

				
				script1.playerLeftSide=true;
				script1.playerRightSide=false;
				script1.playerAhead=false;
				transform.Find(childName).GetComponent<knockOutControl>().left=false;
				//print ("Object is to the left "+ relativePoint.x);

			
		} 
		else if (relativePoint.x >=2.25f) {

		
				
				script1.playerLeftSide=false;
				script1.playerRightSide=true;
				script1.playerAhead=false;
				transform.Find(childName).GetComponent<knockOutControl>().left=true;
				//print ("Object is to the left "+ relativePoint.x);

			//print ("Object is to the right "+ relativePoint.x);
			
		} 
		else {

		

				script1.playerAhead=true;
				script1.playerLeftSide=false;
				script1.playerRightSide=false;

		}
	}
}




