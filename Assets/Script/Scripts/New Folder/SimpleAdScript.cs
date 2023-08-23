using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class SimpleAdScript : MonoBehaviour
{
	void Start ()
	{
		Advertisement.Initialize ("58056");
		
		//StartCoroutine (ShowAdWhenReady ());
	}

	public void show(){
		//Advertisement.Show ();
	}
	IEnumerator ShowAdWhenReady()
	{
		//while (!Advertisement.IsReady ())
			yield return null;
		
		//Advertisement.Show ();
	}
}