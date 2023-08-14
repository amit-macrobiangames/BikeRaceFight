using UnityEngine;

public class missileRocket : MonoBehaviour {
	bool bombHit;
	public GameObject explotion;
	public ParticleSystem explosion;
	bool once;
	Vector3 startingPosition;
	public cameraShake camshakeScript;
	// Use this for initialization
	void Start () {
		bombHit = false;
		once = true;
		startingPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		//if (!bombHit) {
			
			transform.Translate (Vector3.forward * 6f*Time.deltaTime);
			
		//}
		
		if(gameObject.transform.position.y <= 0.35f && once)
		{
			
			explotion.SetActive(true);
			bombHit=true;
			explosion.Play ();
			camshakeScript.enabled=true;
			camshakeScript.shakeAmount = 0.05f;
			camshakeScript.shakeDuration = 0.05f;
		
			once=false;
		}
		else if(gameObject.transform.position.y <= -3f )
		{
			reset();

			transform.root.gameObject.SetActive(false);
		}
	}

	void reset()
	{
		once = true;
		transform.localPosition = startingPosition;
		explotion.SetActive(false);
		explosion.Stop ();
	}
	void OnCollisionEnter(Collision col )
	{
		if (col.gameObject.tag == "Player") {
			//	print ("bombHitted");
			
			bombHit = true;
			//RocketDestroy.hit = true;
			explotion.SetActive(true);
			explosion.Play ();
			//Destroy (gameObject);
			
		}
		
	}
}




