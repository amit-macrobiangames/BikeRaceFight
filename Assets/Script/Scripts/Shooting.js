#pragma strict
var rocket : GameObject;
public var car : GameObject;
//var bullet : GameObject;
function Start () {

InvokeRepeating("Shoot",5,Random.Range(2,5));

}

function Update () {
transform.LookAt(car.transform.position);
//Instantiate(bullet , transform.position + Vector3(0,-3,0), transform.rotation);
}

function Shoot()
{
	Instantiate(rocket , transform.position + Vector3(0,-1.5,0), transform.rotation);
}
