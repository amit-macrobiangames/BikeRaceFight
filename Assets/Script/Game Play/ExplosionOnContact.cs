using UnityEngine;
using System.Collections;

public class ExplosionOnContact : MonoBehaviour {
	
	// class that causes a gameobject to explode and be destroyed on impact with the ground
	
	//public GameObject ExplosionParticle;
	// array containing all the individual  components of a gameobject you want to break off
	//for the car this was tires, headlights, body etc... for the robot this was arms, hips, toes etc...
	public GameObject[] ObjectsParts = new GameObject[0];
	
	// the initial delay before we begin modifying the drag for our exploding object,
	//basically the longer the delay is  the further the objects fly before we slow down their acceleration.
	public float DragChangeDelay = 0f;
	
	public PhysicMaterial MetallicMat = null;
	
	public float gMass = 0f;
	public float gAngularDrag= 0f;
	public float gDrag = 0f;
	
	public float gMaxAngularVelocity = 0f;
	
	// post explosion Drag is the first Drag increase, best used to reel in
	//those flying objects before they get too far off screen
	public float PostExplosionDrag = 0f;
	public float PostExplosionAngularDrag = 0f;
	
	public float angle =  0f;
	
	public float rotationSpeed = 0f;
	// Use this for initialization
	void Start () {
		// here we create the randomness of our rotation
		rotationSpeed = Random.Range(1 ,1000);
		
		angle	= Random.Range(1f ,20f);
		Explode(transform.position);
	}
	
	void Update(){
		// this is the code that implements our random rotation
		//transform.Rotate(new Vector3( 1f,0f,1f)*  Time.deltaTime * rotationSpeed ,angle,Space.Self);
		
	}
	
	void Explode( Vector3 CollisionPosition){
		
		// Once We've blown up our car we show our explosion particle
		//Instantiate(ExplosionParticle, CollisionPosition,Quaternion.identity);
		
		// for every invidivual object want to break off  we populated this array and now we unparent them all.
		for(int i = 0 ; i < ObjectsParts.Length; i++){
			
			// here we bullet proof just to make sure the object of choice has a parent
			if(ObjectsParts[i].transform.parent != null){
				
				// we set all objects to nulll in terms of being parented
				
				//we remove the parent
				ObjectsParts[i].transform.parent = null;
				
				// we add a box collider, I just think Box Colliders collide the best for dramatic effect
				ObjectsParts[i].gameObject.AddComponent<BoxCollider>();
				
				// we make sure the colide is not a trigger
				ObjectsParts[i].gameObject.GetComponent<Collider>().isTrigger =false;
				
				// we add a rigidbody to give them some level of physics manipulation
				//and allows for the objects to bounce off the ground and react to gravity
				ObjectsParts[i].gameObject.AddComponent<Rigidbody>();
				
				//we turn gravity on
				ObjectsParts[i].gameObject.GetComponent<Rigidbody>().useGravity =true;
				
				//here we limit their angular velocity to once again control the explosive effect
				ObjectsParts[i].gameObject.GetComponent<Rigidbody>().SetMaxAngularVelocity(gMaxAngularVelocity);
				
				// and here we continue assigning other rigidbody properties and a physics material
				//to control / manage bounciness and friction
				ObjectsParts[i].gameObject.GetComponent<Rigidbody>().mass =gMass;
				ObjectsParts[i].gameObject.GetComponent<Rigidbody>().drag =gDrag;
				ObjectsParts[i].gameObject.GetComponent<Rigidbody>().angularDrag =gAngularDrag;	
				
				ObjectsParts[i].gameObject.GetComponent<Collider>().material = MetallicMat;
				
			}
		}
		
		StartCoroutine("Tweakvelocity");
		// we then instantiate our particle effect and apply force on all of our components
		
	}
	
	void OnCollisionEnter( Collision other){
		
		if ( other.gameObject.name =="Ground" )
			Explode(transform.position);
		
	}
	
	IEnumerator Tweakvelocity(){
		// here is where we step through our degrees of drag 
		
		yield return new WaitForSeconds(DragChangeDelay);
		for(int i = 0 ; i < ObjectsParts.Length; i++){
			
			if(ObjectsParts[i].GetComponent<Rigidbody>() != null){
				
				// we set all objects to nulll in terms of being parented
				
				ObjectsParts[i].gameObject.GetComponent<Rigidbody>().drag = PostExplosionDrag;
				ObjectsParts[i].gameObject.GetComponent<Rigidbody>().angularDrag =PostExplosionAngularDrag;	
				
				yield return new WaitForSeconds(DragChangeDelay);
				
				ObjectsParts[i].gameObject.GetComponent<Rigidbody>().drag = 0f;
				ObjectsParts[i].gameObject.GetComponent<Rigidbody>().angularDrag =0f;
			}
		}
	}
	
}