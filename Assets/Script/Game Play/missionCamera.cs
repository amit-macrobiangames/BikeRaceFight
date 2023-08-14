using UnityEngine;
using System.Collections;

public class missionCamera : MonoBehaviour {
	
	// Use this for initialization
	public Transform target;
	public int distance;
	
	//public GUITexture cross;
	
	
	void Start () {
		if(target==null)
			target = GameObject.Find ("CamHelper").transform;
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (!target)
			return;
		
		transform.position = target.position - (target.forward.normalized*distance);
		transform.LookAt (target);
		
		//		RaycastHit hit1;
		//		
		//		if (Physics.Raycast (transform.position, transform.forward, out hit1)) {	
		//			Debug.DrawLine (transform.position, hit1.point, Color.green);
		//			if(hit1.collider.gameObject.name == "Gunship")
		//			{
		//				cross.color = Color.black;
		//			}
		//			else
		//				cross.color = Color.white;
		//		}
		//		else
		//			cross.color = Color.white;
	}
}
