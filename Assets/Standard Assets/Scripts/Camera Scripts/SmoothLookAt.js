#pragma strict

var targetObj : GameObject;
var speed : int = 5;
 
function Update(){
  transform.LookAt(targetObj.transform);
    transform.Translate(Vector3.right*5 * Time.deltaTime);
}




