#pragma strict

var rot : Vector3;

function Start () {

	rot = Vector3.zero;
	transform.localScale = new Vector3(1,1,1);
}

function FixedUpdate () {
//	//transform.Rotate(transform.rotation.x , 0, 0);
//	
	rot.x += 20;
	rot.y = 0;
	rot.z = 0;
	
	transform.localEulerAngles = rot;

//transform.RotateAround(Vector3.left,10);

}