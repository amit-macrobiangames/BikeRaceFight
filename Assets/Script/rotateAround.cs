using UnityEngine;
using System.Collections;

public class rotateAround : MonoBehaviour {

	
	public Transform targetObj;
	public int speed  = 5;
	
	void FixedUpdate(){

		transform.LookAt(targetObj);
		//transform.position += new Vector3 (5f* Time.fixedDeltaTime,0,0);
		transform.Translate(Vector3.right*speed * Time.deltaTime);
		transform.eulerAngles = new Vector3 (30.5f,transform.eulerAngles.y,transform.eulerAngles.z);
	}
	
	
	
	


}
