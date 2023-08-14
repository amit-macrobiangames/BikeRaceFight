
var delayTime:float=0;
var delayPlusTime:float=0;
var rampDownTime:float=1;
var origMinEmission:float;
var origMaxEmission:float;

function Start () {

// origMinEmission=GetComponent.<ParticleEmitter>().minEmission;//khuram
// origMaxEmission=GetComponent.<ParticleEmitter>().maxEmission;
// GetComponent.<ParticleEmitter>().emit=false;

}

function Update () {
if((delayTime+delayPlusTime)>0) delayTime-=Time.deltaTime;


// if(delayTime<=0 && GetComponent.<ParticleEmitter>().emit==false)
// {

// GetComponent.<ParticleEmitter>().emit=true;//khuram
// }

if((delayTime+delayPlusTime)<=0){
// GetComponent.<ParticleEmitter>().minEmission=origMinEmission*rampDownTime;//khuram
// GetComponent.<ParticleEmitter>().maxEmission=origMaxEmission*rampDownTime;
rampDownTime-=Time.deltaTime;
if(rampDownTime<0){ rampDownTime=0;}
}

}