private var life :float =2 ;
private var BirthTime : float;
private var textmesh: String;
//executed initially whenever the script will be called...
function Start() {
	BirthTime=Time.time;
	textmesh=	transform.GetComponent(TextMesh).text ;
//	print(textmesh);
}

function Update () {
	if(Time.time -  BirthTime > life) {
		Destroy(this.gameObject);
	}
	
	this.transform.position.y += 0.5 ;
	this.transform.position.z += 0.3;
	

}