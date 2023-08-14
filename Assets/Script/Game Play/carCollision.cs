using UnityEngine;
using System.Collections;

public class carCollision : MonoBehaviour {

//
	void OnCollisionEnter(Collision hit)
	{ 
		/*              in case of two car collision                                 */



		//print (hit.collider.name+ " "+ hit.collider.gameObject.layer );
		if(hit.collider.gameObject.layer==14 && (hit.collider.name.Contains("Car") ||hit.collider.name.Contains("barrel") ))
				 {
//			print (hit.transform.position.z+"  car collision "+ transform.position.z);
			if((transform.position.z-hit.transform.position.z)>0)
			{

				transform.parent.Translate (Vector3.back * 65);
				Invoke("disable",5f);
				
			}


				}



		}

	void disable()
	{
		transform.parent.gameObject.SetActive(false);
	}
}
