using UnityEngine;
using System.Collections;

public class BackFromMenu : MonoBehaviour {
	public Transform bikeSelectionCanvas,bikeSelectionModel, levelSelectionMode;

	// Use this for initialization
	void Start () {

		if (bikeSelection.backToMode ) {
			bikeSelectionModel.gameObject.SetActive(false);
			bikeSelectionCanvas.gameObject.SetActive(false);
			Time.timeScale=1;
			levelSelectionMode.gameObject.SetActive(true);
			storeBtnHandlers.levelSelectionActive=true;
		}

		if (endlessmodeGraphics.ShowLevelAd != 1) {
			
			ShowLevelSelectionBanner();
		}
		else if(endlessmodeGraphics.ShowLevelAd == 1)
		{
			ShowLevelSelectionBanner();
		}

	}
	
		public void Hide_Banner()
		{
			#if !UNITY_EDITOR
			//Ads_Manager.Instance.HideAdmobBanner ();
			#endif
		}

		public void ShowLevelSelectionBanner()
		{
			#if !UNITY_EDITOR
				//Ads_Manager.Instance.ShowAdmobBanner ();
			#endif	
		}


}
