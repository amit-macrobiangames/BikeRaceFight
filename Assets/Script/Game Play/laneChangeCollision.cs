using UnityEngine;
using System.Collections;

public class laneChangeCollision : MonoBehaviour {


	void OnCollisionEnter(Collision col)
	{ 
		
		
		if (col.transform.tag.Contains ("Player") || col.transform.tag.Contains ("PlayerAttack")) {
			transform.parent.GetComponent<newLevelCarMove>().movingLeft=false;
			transform.parent.GetComponent<newLevelCarMove>().movingRight=false;
		}
	}
}
