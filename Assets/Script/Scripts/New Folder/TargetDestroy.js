#pragma strict



function Update () {
	if(HeliMovement.heliMove == true)
	{
		Destroy (gameObject);
	}
}