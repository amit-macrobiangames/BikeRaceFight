var translationSpeedX:float=0;
var translationSpeedY:float=1;
var translationSpeedZ:float=0;



var moveup: boolean=true;
var movedown: boolean=false;


function Update () {


if( moveup)
transform.Translate(Vector3(translationSpeedX,translationSpeedY,translationSpeedZ)*Time.deltaTime);
else if(movedown)
{
transform.Translate(Vector3(translationSpeedX,-translationSpeedY,translationSpeedZ)*Time.deltaTime);
}

 
  if(transform.position.y>3.75)
{
movedown=true;
moveup=false;
}
else if(transform.position.y<=1 )
{
moveup=true;
movedown=false;
}
}