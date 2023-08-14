#pragma strict
var rocket1 : GameObject;

var target : GameObject;
private var targetPos: Vector3 ;
function Start () {
//	InvokeRepeating("Shoot",5,Random.Range(2,5));
//	InvokeRepeating("Target",5,Random.Range(2,3));
}

function Update () {
	//target = GameObject.FindGameObjectWithTag("HeliMissle1");
	
	if(HeliMovement.heliMove == true)
	{
		HeliMovement.heliMove = false;
		Invoke("Shoot" , 1);
		Invoke("Target" , 1);
	}
		rocket1.transform.LookAt (target.transform.position);
}

function Shoot()
{
	Instantiate(rocket1 , transform.position + Vector3(0,-0.5,0), transform.rotation);
}
function Target()
{
targetPos=Vector3(Random.Range(1.2,4.5),0.139,transform.position.z-10);
//transform.position + Vector3(Random.Range(1.2,4.5),-3,-50)

	Instantiate(target , targetPos, transform.rotation);
}