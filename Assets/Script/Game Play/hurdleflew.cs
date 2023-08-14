using UnityEngine;
using System.Collections;

public class hurdleflew : MonoBehaviour {
	bool forceonce;
	bool cone;

	// Use this for initialization
	void Start () {
		forceonce = true;
		cone = false;
	
//		if (transform.name.Contains("cone")) {
//			cone=true;		
//		}

	}
	
	// Update is called once per frame


	void OnCollisionEnter(Collision col)
	{
		//print (col.transform.tag);
		if(col.transform.tag.Equals("Player") || col.transform.tag.Equals("PlayerAttack") )
		{
			if(forceonce)
			{
				Rigidbody rb=	gameObject.AddComponent<Rigidbody>();
				rb.constraints=RigidbodyConstraints.None;
				rb.velocity = new Vector3 (0, 0, 100);
				rb.AddForce (new Vector3 (0, 0, 100), ForceMode.Force);//=-1*gameObject.rigidbody.velocity;
				
		
				forceonce=false;
				Invoke("disable",3f);
		
				if(cone)
				{
					transform.GetChild(0).gameObject.SetActive(false);
				}



			}

		}
}

	void disable()
	{
		transform.gameObject.SetActive (false);
	}
}
