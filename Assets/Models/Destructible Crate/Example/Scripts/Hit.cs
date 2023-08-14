using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {



	public Transform player;



	bool once;
	bool timeSlow;
	void Start()
	{
	
		once = true;
	
			player=player.GetComponent<heavyBikeTurns>().player.transform;
	

	}


	void Update()
	{

		if (heavyBikeTurns.boostbool) {
				
			if (Vector3.Distance (transform.position, player.position) < 7) {
				
				DestroyIt ();
			
				
			}
				} else {
						//print (Vector3.Distance (transform.position, player.position));
						if (Vector3.Distance (transform.position, player.position) < 5) {
				//print ("distance: "+(transform.position.x-player.position.x));
				if((transform.position.x-player.position.x)>-0.3f &&(transform.position.x-player.position.x)<0.3f )
				{
					DestroyIt ();
					//print ("xhit");
				}
								
			

						}
				}
	}
	
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag.Equals ("Player")) {
		

				DestroyIt ();
				

		}
		}
	void OnCollisionEnter( Collision collision ) {
		if (collision.gameObject.tag.Equals ("Player")) {
	
						

								DestroyIt ();
		
		//	player.root.GetComponent<findTarget>().camSlow();

						
				}
	}




		void DestroyIt(){
		if (once) {
		



								Rigidbody rb = gameObject.AddComponent<Rigidbody> ();
								rb.constraints = RigidbodyConstraints.None;

				Invoke("disable",3f);
			
		
						
			once=false;		
		}
	}

	void disable()
	{
		transform.gameObject.SetActive (false);
	}
}