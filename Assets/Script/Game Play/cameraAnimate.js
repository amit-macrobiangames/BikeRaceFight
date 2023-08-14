#pragma strict


var fader: GameObject;
function Start () {
transform.GetComponent.<Animation>() ["camera1"].speed = 0.35f;
transform.GetComponent.<Animation>() ["camera2"].speed = 0.35f;
transform.GetComponent.<Animation>() ["camera3"].speed = 0.35f;
transform.GetComponent.<Animation>() ["camera4"].speed = 0.25f;
transform.GetComponent.<Animation>() ["camera9"].speed = 0.75f;
GetComponent.<Animation>().Play ("camera2", PlayMode.StopAll);
Invoke("turnShaderOn",4);
Invoke("firstAnimCompleted",5f);
}
function firstAnimCompleted()
{
print("inside");
//animation.Stop();
GetComponent.<Animation>().Stop("camera1");

//animation.Play ("camera3", PlayMode.StopAll);
}


function turnShaderOn()
{
fader.SetActive(true);
}
function Update () {

}

public function playCamera2()
{
print("cam2");
GetComponent.<Animation>().Play ("camera2", PlayMode.StopAll);
Invoke("AnimationCompleted",3.25f);
}




public function playCamera3()
{
print("cam3");
GetComponent.<Animation>().Play ("camera3", PlayMode.StopAll); // add camera5 here
Invoke("AnimationCompleted",2.25f);
}


public function playCamera4()
{
print("cam4");
GetComponent.<Animation>().Play ("camera4", PlayMode.StopAll); // add camera5 here
Invoke("AnimationCompleted",2.25f);
}


public function playCamera5()
{
print("cam5");
GetComponent.<Animation>().Play ("camera5", PlayMode.StopAll); // add camera5 here
Invoke("AnimationCompleted",6.5f);
}
public function playCamera6()
{
print("cam6");
GetComponent.<Animation>().Play ("camera6", PlayMode.StopAll); // add camera5 here
Invoke("AnimationCompleted",6.5f);
}

public function playCamera7()
{
print("cam7");
GetComponent.<Animation>().Play ("camera7", PlayMode.StopAll); // add camera5 here
Invoke("AnimationCompleted",3.5f);
}
public function playCamera8()
{
print("cam7");
GetComponent.<Animation>().Play ("camera8", PlayMode.StopAll); // add camera5 here
Invoke("AnimationCompleted",3.5f);
}

public function playCamera9()
{
print("cam9");
GetComponent.<Animation>().Play ("camera9", PlayMode.StopAll); // add camera5 here
Invoke("AnimationCompleted",6.5f);
}

function AnimationCompleted()
{
fader.SetActive(true);
}




