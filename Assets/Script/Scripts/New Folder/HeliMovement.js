#pragma strict
var enemyJeep : GameObject;
public static var heliMove : boolean;
function Start () {
	heliMove = false;
}

function Update () {

//	if(!Button_PlayScreen.isPaused){
		//if(RocketDestroy.hit == false)
		//{
			transform.Translate(0,0, -3);
		//}	
		
//		print((enemyJeep.transform.position.z - 20));
		if(transform.position.z < enemyJeep.transform.position.z - 5)
		{
			transform.position.z += 200;
			heliMove = true;
		}
		
	//}
}