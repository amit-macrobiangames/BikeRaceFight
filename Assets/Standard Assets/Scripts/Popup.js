private var life :float =1.5 ;
private var BirthTime : float;
//executed initially whenever the script will be called...
function Start() {
	BirthTime=Time.time;
}

function Update () {
	if(Time.time -  BirthTime > life) {
		Destroy(this.gameObject);
	}
	
//	this.transform.position.y += 0.02;
//	this.renderer.material.color.a -= 0.02;
}