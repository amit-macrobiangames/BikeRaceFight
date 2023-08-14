using UnityEngine;
using System.Collections;


public class OpponentWeaponAI : MonoBehaviour {

	public bool isAttacking;
	public GameObject  normalPlayer, weaponisedPlayer,batNormal,axeNormal,batWeaponised,axeWeaponised,axeLeftInner,axeRightInner,batLeftInner,batRightInner;
	public AnimationClip batRight,batLeft;
	bool BatLeftAttack,BatRightAttack,axeLeftAttack,axeRightAttack;

	bool crash,isdead;
	public AudioClip axeSound,batSound;
	
	int bikeID;



	
	void Awake()
	{
		if (PlayerPrefs.GetInt ("levels") == 19) {
						this.enabled = true;
			batNormal.SetActive(true);
			batWeaponised.SetActive(true);
			axeNormal.SetActive(true);
			batWeaponised.SetActive(true);
				}
				else
						this.enabled = false;

		
		if (transform.name.Contains ("opponentTallguys")) {
			bikeID=1;
		} else
			bikeID=0;
	

		
	}
	void Start () {

		isAttacking = false;


	

		weaponisedPlayer.GetComponent<Animation>() [batLeft.name].speed = 2f;
		weaponisedPlayer.GetComponent<Animation>() [batRight.name].speed = 2f;
	
	}




	public void playerBatLeftAttack()
	{
		resetAll();
		BatLeftAttack = true;
		turnPlayerOff ();
		BatAttack ();
	}
	public void playerBatRightAttack()
	{
		resetAll();
		BatRightAttack = true;
		turnPlayerOff ();
		BatAttack ();
	}
	public void playerAxeRightAttack()
	{
		resetAll();
		axeRightAttack = true;
		turnPlayerOff();
		BatAttack ();
	}

	public void playerAxeLeftAttack()
	{
		resetAll();
		axeLeftAttack = true;
		turnPlayerOff();
		
		BatAttack ();
	}
	// Update is called once per frame
	void Update () {
	if(bikeID==0)
	{
		if (transform.Find ("opponentFatguy").GetComponent<knockOutControl> ().unstability) {
			resetAll();
				isAttacking = false;
				normalPlayer.SetActive (true);
				weaponisedPlayer.SetActive (false);
				batNormal.SetActive (true);
				axeNormal.SetActive (true);
			
					



		}
	}
	else if(bikeID==1)
	{
		if (transform.Find ("opponentTallguys").GetComponent<knockOutControl> ().unstability) {

			resetAll();
				isAttacking = false;
				normalPlayer.SetActive (true);
				weaponisedPlayer.SetActive (false);
				batNormal.SetActive (true);
				axeNormal.SetActive (true);
			
		}
	}

//		if (Input.GetKeyDown (KeyCode.Z)) {
//			playerBatLeftAttack();
//			
//		}
//		if (Input.GetKeyDown (KeyCode.X)) {
//			playerBatRightAttack();
//			
//		}
//		

		
		
		

		
		
		
//		if (Input.GetKeyDown (KeyCode.N)) {
//			playerAxeLeftAttack();
//			
//		}	
//		if (Input.GetKeyDown (KeyCode.M)) {
//			playerAxeRightAttack();
//			
//		}	
//		
//		
		



	}
	
	

	
	

	
	

	


	

	
	
	


	
	void 	resetAll()
	{
		CancelInvoke();
		

		
		batWeaponised.SetActive (true);
		
		axeWeaponised.SetActive (true);
		batLeftInner.SetActive (false);
		
		batRightInner.SetActive (false);
		
		
		axeLeftInner.SetActive (false);
		
		axeRightInner.SetActive (false);
		
	
		
		axeRightAttack = false;
		axeLeftAttack = false;
		BatLeftAttack = false;
		BatRightAttack = false;
	
		
	

	
		
		
		
		
	}
	
	
	void BatAttack()
	{
		
		Invoke ("ChangeBat", 0.4f/2);
		Invoke ("ChangeBatBack", 1.1f/2);
		
		
		
		Invoke ("turnPlayerOn", batLeft.length/2f);
		
		
		
		
		if (BatLeftAttack || axeLeftAttack) {
			
			weaponisedPlayer.transform.GetComponent<Animation>().Play (batLeft.name, PlayMode.StopAll);
			if(BatLeftAttack)
				Invoke ("LeftbatAttackEnd", batLeft.length/2);
			if(axeLeftAttack)
				Invoke ("LeftAxeAttackEnd", batLeft.length/2);
		} 
		
		else if (BatRightAttack || axeRightAttack) {
			weaponisedPlayer.transform.GetComponent<Animation>().Play (batRight.name, PlayMode.StopAll);
			if(BatRightAttack)
				Invoke ("RightbatAttackEnd", batLeft.length/2);
			if(axeRightAttack)
				Invoke ("RightAxeAttackEnd", batLeft.length/2);
		}
		
		
		
		
	}
	
	
	
	
	void ChangeBat()
	{
	
		if(BatLeftAttack || BatRightAttack)
			batWeaponised.SetActive (false);
		if(axeLeftAttack || axeRightAttack)
			axeWeaponised.SetActive (false);
		
		if(BatLeftAttack)
			batLeftInner.SetActive (true);
		else if(BatRightAttack)
			batRightInner.SetActive (true);
		
		if(axeLeftAttack)
			axeLeftInner.SetActive (true);
		else if(axeRightAttack)
			axeRightInner.SetActive (true);
	}
	
	
	
	void ChangeBatBack()
	{

		if(BatLeftAttack || BatRightAttack)
			batWeaponised.SetActive (true);
		if(axeLeftAttack || axeRightAttack)
			axeWeaponised.SetActive (true);
		
		
		if(BatLeftAttack)
			batLeftInner.SetActive (false);
		else if(BatRightAttack)
			batRightInner.SetActive (false);
		
		
		if(axeLeftAttack)
			axeLeftInner.SetActive (false);
		else if(axeRightAttack)
			axeRightInner.SetActive (false);
	}
	
	
	void LeftbatAttackEnd()
	{
		

		BatLeftAttack = false;
	}
	
	
	void LeftAxeAttackEnd()
	{
		
	
		axeLeftAttack = false;
	}
	
	
	
	void RightbatAttackEnd()
	{
		
		BatRightAttack = false;
	
		
	}
	
	void RightAxeAttackEnd()
	{
		
		axeRightAttack = false;

	}
	
	
	
	void turnPlayerOff()
	{
		isAttacking = true;
		normalPlayer.SetActive (false);
		weaponisedPlayer.SetActive (true);
		weaponisedPlayer.transform.Find ("biker-v3").transform.GetComponent<Renderer>().material.mainTexture = normalPlayer.GetComponent<Renderer>().material.mainTexture;

		batNormal.SetActive (false);
		axeNormal.SetActive (false);


		
	}
	
	void turnPlayerOn()
	{
		isAttacking = false;
		normalPlayer.SetActive (true);
		weaponisedPlayer.SetActive (false);
		batNormal.SetActive (true);
		axeNormal.SetActive (true);
		if(bikeID==0)
		transform.Find ("opponentFatguy").GetComponent<knockOutControl> ().attackOn ();
	else if(bikeID==1)
			transform.Find ("opponentTallguys").GetComponent<knockOutControl> ().attackOn ();
		
	}
	
	
	
	
	
	
	
}
