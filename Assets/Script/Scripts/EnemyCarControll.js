//#pragma strict
//// This class will controll enemy car also this class will active and deavtive fire and explotion effects 
//// and also changing camera effects (camera Rotation) after explotion.
//// and has GUI Restart button.
//
//public  var playerCar : GameObject;
//var enemyCar : GameObject;
//var rocket : GameObject;
//
//
//
//
//
//var val : float;
//
//var carSound : int;
//
//var check : boolean;
//
//
//var lastPosition : float;
//
////private var csScript : RocketDestroy;
//
//
//
//
//var soundcheck : boolean;
//
//var time : int;
//
////var shieldCar1 : GameObject;
////var shieldCar2 : GameObject;
////var shieldCar3 : GameObject;
////var shieldOn : GameObject;
////var shieldOff : GameObject;
////var shieldButton : GameObject;
////var shieldBar : RectTransform;
////var carLifeBar : RectTransform;
//
//
////var shieldText : GameObject;
//
////var shieldLifeBar : RectTransform;
////public static var shieldHit : boolean;
//public static var carLifeCheck : boolean;
//
////var startCounter : GameObject;
//
//
////static var adCheck : boolean;
////private var adShow : boolean;
//
//function Start () {
//
////shieldHit = false;
//carLifeCheck = false;
//check=true;
//
//soundcheck = false;
//Time.timeScale = 1;
//
//
//
//
//	
//	carSound = PlayerPrefs.GetInt("SoundCheck");
//	
//	
//
//
//	if(PlayerPrefs.GetInt("SoundCheck") == 0)
//	{
//	
//	}
//}
//
//function Update () {
//	
//
//	//if(!Button_PlayScreen.isPaused){
//	if(playerCar.transform.rotation.y >= 60 && playerCar.transform.rotation.y <= 300)
//	{
//		playerCar.transform.rotation.y = 0;
//	}
//	
//	
//	//cam3 = playerCar.transform.Find("Main Camera3");
//	time = Time.timeSinceLevelLoad;
//	if(time <=4)
//	{
//	
////		if(pausedScreen.active == false)
////		{
////			if(time <= 1)
////			{
////				GameObject.Find("Start_Count").guiText.text = "3";
////				
////			}
////			else if(time > 1 && time <= 2)
////			{
////				GameObject.Find("Start_Count").guiText.text = "2";
////			}
////			else if(time > 2 && time <= 3)
////			{
////				GameObject.Find("Start_Count").guiText.text = "1";
////			}
////			else if(time > 3 && time <= 4)
////			{
////				GameObject.Find("Start_Count").guiText.text = "Go!!";
////			
////				shieldText.active = false;
////				
////			}
////		}
////		else if(pausedScreen.active == true)
////		{
////			shieldText.active = false;
////		
////		}
//	
//		
//	}
//	else if(time >4 && time <=6)
//	{
//	//	startCounter.active = false;
//
//	}
//	
////	playerCar.rigidbody.constraints = RigidbodyConstraints.FreezeRotationY || RigidbodyConstraints.FreezeRotationZ ;
//if(check==true)
//	Invoke("rand",1);
//enemyCar.transform.position.z = playerCar.transform.position.z + 100;
//
//
//
//check=false;
////if(RocketDestroy.hit == true && (shieldCar1.active == false && shieldCar2.active == false && shieldCar3.active == false))
//if(RocketDestroy.hit == true )
//{
//	//particle.active = true;
////	if(carLifeBar.localScale.x <= 0.1)
////	{
////		Invoke("GameoverDialog", 2);
////		
////		if(!soundcheck)
////		{
////			soundcheck = true;
////		}
////		
////		
////		playerCar.transform.Find("Audio").active = false;
////		shieldButton.active = false;
////	}
////	else if(carLifeBar.localScale.x >= 0 && LifesScript.carLifeBarStatus == 0)
////	{
////		Invoke("GameoverDialog", 2);
////		carLifeBar.localScale.x = 0;
////		if(!soundcheck)
////		{
////			soundcheck = true;
////		}
////		
////		
////		playerCar.transform.Find("Audio").active = false;
////		shieldButton.active = false;
////	}
////	else if(carLifeBar.localScale.x >= 0.1 && LifesScript.carLifeBarStatus >= 1)
////	{
////		RocketDestroy.hit = false;
////		carLifeCheck = true;
////	}
//	
//	
//
//	
//	
//}
////////////////////////////////////////////// Shiled Life Logic ///////////////////////////////////////////////////////////////
////else if(RocketDestroy.hit == true && (shieldCar1.active == true || shieldCar2.active == true || shieldCar3.active == true))
//else if(RocketDestroy.hit == true )
//{
//	
//	//playerCar.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
//	RocketDestroy.hit = false;
//	Invoke("SetBack" , 1);
//
//	//if(shieldBar.localScale.x >= 0.1)
//	//{
//		//shieldHit = true;
//	//}
//	
//
//	
//	
//	
//}
//else{
//	if(enemyCar.transform.position.x <= (-0.7) || enemyCar.transform.position.x >= 5.2)
//	{
//		Invoke("rand",2);
//		
//		if(enemyCar.transform.position.x <= (-0.7))
//		{
//			enemyCar.transform.position.x = -0.69;
//		}
//		else if( enemyCar.transform.position.x >= 5.2)
//		{
//			enemyCar.transform.position.x = 5.1;
//		}
//	}
//	enemyCar.transform.Translate(val*Time.deltaTime*15,0,0 );
//}
////if(shieldBar.localScale.x <= 0.1)
////	{
////		
////		shieldCar1.active = false;
////		shieldCar2.active = false;
////		shieldCar3.active = false;
////		shieldOn.active = false;
////	}
//
////if(Button_PlayScreen.isPaused == false && RocketDestroy.hit != true)
//	
//	if( RocketDestroy.hit != true)
//	
//{
////	if(Button_PlayScreen.activeCam == 1)
////	{
////		cam1.active = true;
////		cam1.GetComponent(AudioListener).volume = 1.0f;
////	}
////	else if(Button_PlayScreen.activeCam == 2)
////	{
////		cam2.active = true;
////		cam2.GetComponent(AudioListener).volume = 1.0f;
////	}
////	else if(Button_PlayScreen.activeCam == 3)
////	{
////		cam3.active = true;
////		cam3.GetComponent(AudioListener).volume = 1.0f;
////	}
//	
//	
//	
//}
//
////}
////if (Button_PlayScreen.isPaused == true || endGameScreen.active == true)
////if (endGameScreen.active == true)
////{
////
////	if(cam1.active == true)
////	{
////		cam1.GetComponent(AudioListener).volume = 0.0f;
////	}
////	else if (cam2.active == true)
////	{
////		cam2.GetComponent(AudioListener).volume = 0.0f;
////		
////	}
////	else if (cam3.active == true)
////	{
////		cam3.GetComponent(AudioListener).volume = 0.0f;
////	}
////	
////}
//
//
////if(Button_PlayScreen.isRestart == true)
////{
////
////
////	RocketDestroy.hit = false;
////	Button_PlayScreen.isRestart = false;
////}
//
//
//}
//function rand(){
//	val = Random.Range(-0.3,0.5);
//	check=true;
//}
//
//
////function GameoverDialog(){
////
////
////	endGameScreen.active = true;
////	gamveOver_RestartButton.active = true;
////	gameOver_MenuButton.active = true;
////	pausedButton.active = false;
////	
////	musicOnButton.active = false;
////	musicOffButton.active = false;
////	
////	
////	
////
////	
////}
//
//function SetBack()
//{
////	playerCar.rigidbody.constraints = RigidbodyConstraints.None;
//}
//
