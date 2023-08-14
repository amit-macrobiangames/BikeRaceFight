
#pragma strict
var bomb : GameObject;
var heliBombHit : boolean;
var explotion : GameObject;
function Start () {
	heliBombHit = false;
	bomb = GameObject.FindGameObjectWithTag("HeliMissle1");
		transform.LookAt (bomb.transform.position);

}

function Update () {
	
	//if (!heliBombHit) {
			transform.Translate (Vector3.forward *  100 * Time.deltaTime );

			//	}
	
//	if(gameObject.transform.position.y <= 3.15)
//	{
//		explotion.active = true;
//	}
}

function OnCollisionEnter(col : Collision)
	{
		

		if (col.gameObject.tag == "maincar") {
						print ("Heli bomb Hitted");
						
						heliBombHit = true;
						//RocketDestroy.hit = true;
						explotion.active = true;
						Destroy (gameObject);
						

				}

	}
