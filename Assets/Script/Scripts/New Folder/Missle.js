#pragma strict

var bombHit : boolean;
var explotion : GameObject;

function Start () {
	bombHit = false;


}

function Update () {
	
	if (!bombHit) {
		
			transform.Translate (Vector3.forward * 2 * 0.8);

				}
	
	if(gameObject.transform.position.y <= 3.15)
	{

		explotion.active = true;

	}
	
	

}

function OnCollisionEnter(col : Collision)
	{
		

		if (col.gameObject.tag == "maincar") {
					//	print ("bombHitted");
						
						bombHit = true;
						//RocketDestroy.hit = true;
				
						Destroy (gameObject);
						

				}

	}
