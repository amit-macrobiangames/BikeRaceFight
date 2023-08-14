using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class staminaBarCollision : MonoBehaviour {
	float[] xAxisLanes =new float[4];
	public Transform player;
	public Transform scorePopup;

	public AudioClip stuntCollect;



	float distance;
	// Use this for initialization
	void Start () {
	

		xAxisLanes [0] = 6.174322f;
		xAxisLanes [1] = 2.718078f;
		xAxisLanes [2] = -0.38569f;
		xAxisLanes [3] = 4f;


		if (bikeSelection.bikeCount == 2 || bikeSelection.bikeCount == 1) {
			distance=400.0f;
		}
		else if (bikeSelection.bikeCount == 4 || bikeSelection.bikeCount == 3 || bikeSelection.bikeCount == 7) {
			distance=700.0f;
			
		}
		else if (bikeSelection.bikeCount == 5 || bikeSelection.bikeCount == 6 || bikeSelection.bikeCount == 8) {
			
			distance=1000.0f;
			
		}
		else if (bikeSelection.bikeCount == 9 ) {
			
			
			distance=1300.0f;
		}
		else if (bikeSelection.bikeCount == 10 ) {
			
			distance=1600.0f;
			
		}
		
		if (SceneManager.GetActiveScene().name.Contains ("level5") || SceneManager.GetActiveScene().name.Contains("KnockOut")) {
			player=player.GetComponent<heavyBikeTurns>().player.transform;		
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if ((transform.position.z - player.position.z) < -0.5f) {
			int xaxis=Random.Range(0,3);
			transform.position=new Vector3(xAxisLanes[xaxis],0.7293835f,transform.position.z+distance);
			
		}


	}
	
//	void OnTriggerEnter(Collider col)
//	{
//
//		if (col.gameObject.tag.Equals ("Player")) {
//			if(player.audio.enabled)
//			player.audio.PlayOneShot(stuntCollect);
//			scorePopup.GetComponent<TextMesh>().text = "+";
//			scorePopup.GetComponent<TextMesh>().color=new Color(204,0,0,255);
//			Instantiate(scorePopup,player.transform.position+new Vector3(0f,2f,4f),Quaternion.identity);
//
//			if( Application.loadedLevelName.Contains("endlessMode") || Application.loadedLevelName.Contains ("harley"))
//			{
//				player.GetComponent<endlessmodeControl>().staminaBarCollision=true;
//			}
//
//			else if(Application.loadedLevelName.Contains("heavybikeGame"))
//			{
//				player.GetComponent<endlessTallguyControl>().staminaBarCollision=true;
//			}
//			else if(Application.loadedLevelName.Contains("level5") || Application.loadedLevelName.Contains("KnockOut") )
//			{
//				if(player.name.Contains("Player"))
//				player.GetComponent<heavyBikeTurnControls>().staminaBarCollision=true;
//				else
//				player.GetComponent<turnLevelcontrols>().staminaBarCollision=true;
//			}
//
//
//
//			int xaxis=Random.Range(0,3);
//			transform.position=new Vector3(xAxisLanes[xaxis],0.7293835f,transform.position.z+distance);
//			scorePopup.GetComponent<TextMesh> ().color = new Color (253, 246, 0, 255);
//		}
//		
//		
//		
//		
//		
//		
//	}
}
