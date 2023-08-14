using UnityEngine;
using System.Collections;

public class flash : MonoBehaviour {
	
	
	




	public Transform leftIndicator,rightIndicator;

	void Start()
	{
		StartCoroutine ("flashLights");
	}




	


	
	
	IEnumerator flashLights()
	{
		while (true) {
						leftIndicator.gameObject.SetActive (false);
						rightIndicator.gameObject.SetActive (false);

						yield return new WaitForSeconds (0.5f);
						leftIndicator.gameObject.SetActive (true);
						rightIndicator.gameObject.SetActive (true);
						yield return new WaitForSeconds (0.5f);
//						leftIndicator.gameObject.SetActive (false);
//						rightIndicator.gameObject.SetActive (false);
//						yield return new WaitForSeconds (0.5f);
//						leftIndicator.gameObject.SetActive (true);
//						rightIndicator.gameObject.SetActive (true);
//						yield return new WaitForSeconds (0.5f);
//						leftIndicator.gameObject.SetActive (false);
//						rightIndicator.gameObject.SetActive (false);
//						yield return new WaitForSeconds (0.5f);
//		
//						leftIndicator.gameObject.SetActive (true);
//						rightIndicator.gameObject.SetActive (true);
//						yield return new WaitForSeconds (0.5f);
//						leftIndicator.gameObject.SetActive (false);
//						rightIndicator.gameObject.SetActive (false);
//						yield return new WaitForSeconds (0.5f);
//						leftIndicator.gameObject.SetActive (true);
//						rightIndicator.gameObject.SetActive (true);
		
				}
		
	}
	
}
