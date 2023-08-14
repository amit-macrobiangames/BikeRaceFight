
using UnityEngine;

public class FbLink : MonoBehaviour {
	public string package_name;
	// Use this for initialization
	void Start () {
//		Application.OpenURL ("market://details?id="+package_name);

		openApp ();

	}

	void openApp()
	{
		bool fail = false;
		string bundleId = package_name; // your target bundle id
		AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");

		AndroidJavaObject launchIntent = null;
		try
		{
			launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage",bundleId);
		}
		catch (System.Exception e)
		{
			fail = true;
		}

		if (fail)
		{ //open app in store
			Application.OpenURL("market://details?id="+package_name);
		}
		else //open the app
			ca.Call("startActivity",launchIntent);

		up.Dispose();
		ca.Dispose();
		packageManager.Dispose();
		launchIntent.Dispose();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
