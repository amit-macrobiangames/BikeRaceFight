#pragma strict

function Start () {


//	audio.volume = 0;
		if(PlayerPrefs.GetInt("SoundOff")==0)
		{
			
				GetComponent.<AudioSource>().volume = 1;
			}
			else 
			{
				GetComponent.<AudioSource>().volume = 0;
			}

}
