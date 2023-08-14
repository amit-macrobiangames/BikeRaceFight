using UnityEngine;
using System.Collections;

public class TM : MonoBehaviour {

	// Use this for initialization
	private Vector2 Previous;
	private Vector2 Current;
	private Vector2 Mag;
	//private float speed;
	private float StartTime;
	private bool FingerUp;



	public int zoomspeed = 3;
	public Camera selectedCamera;
	public float MINSCALE = 2.0F;
	public float MAXSCALE = 5.0F;
	public float minPinchSpeed = 5.0F;
	public float varianceInDistances = 5.0F;
	private float touchDelta = 0.0F;
	private Vector2 prevDist = new Vector2(0,0);
	private Vector2 curDist = new Vector2(0,0);
	private float speedTouch0 = 0.0F;
	private float speedTouch1 = 0.0F;
	public float upside;
	public float downside;
	public float rightside;
	public float leftside;

	private bool zoom;


	private float _doubleTapTimeD;
	private bool dt;
	private Vector3 temp;
	//public GameObject boundaryLeft;
	//public GameObject boundaryRight;
	//public GameObject boundaryUp;
	//public GameObject boundaryDown;
	//public GameObject boundaryUpRight;
	//public GameObject boundaryUpLeft;
	//public GameObject boundaryDownRight;
	//public GameObject boundaryDownLeft;

	
	void Start () {
	
		TouchHandler.stimulateMouseTouches = true;
		TouchHandler.SharedHandler ().onTouchesBegin += Begin1;
		TouchHandler.SharedHandler ().onTouchesEnded += Ended1;
		TouchHandler.SharedHandler ().onTouchesCancelled += Cancelled1;
		TouchHandler.SharedHandler ().onTouchesMoved += Moved1;
		TouchHandler.SharedHandler ().onTouchesStationary += Stationary1;

	}
	
	// Update is called once per frame
	void Update () {

		//print (Mag.y);

		/////////////////pinch zoom///////////////////
		if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved) 
		{
			dt = false;
			zoom = true;
			curDist = Input.GetTouch(0).position - Input.GetTouch(1).position; //current distance between finger touches
			prevDist = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition)); //difference in previous locations using delta positions
			touchDelta = curDist.magnitude - prevDist.magnitude;
			speedTouch0 = Input.GetTouch(0).deltaPosition.magnitude / Input.GetTouch(0).deltaTime;
			speedTouch1 = Input.GetTouch(1).deltaPosition.magnitude / Input.GetTouch(1).deltaTime;
			
			
//			if ((touchDelta  <= 1) && (speedTouch0 > minPinchSpeed) && (speedTouch1 > minPinchSpeed))
//			{
//				
//				selectedCamera.fieldOfView = Mathf.Clamp(selectedCamera.fieldOfView + (1 * zoomspeed),40,90);
//				print ("zoomout");
//			}
//			
//			else if ((touchDelta  > 1) && (speedTouch0 > minPinchSpeed) && (speedTouch1 > minPinchSpeed))
//			{
//				//if(!boundaryDown.renderer.isVisible && !boundaryDownLeft.renderer.isVisible && !boundaryDownRight.renderer.isVisible && !boundaryLeft.renderer.isVisible && !boundaryRight.renderer.isVisible && !boundaryUp.renderer.isVisible && !boundaryUpLeft.renderer.isVisible && !boundaryUpRight.renderer.isVisible)
//				selectedCamera.fieldOfView = Mathf.Clamp(selectedCamera.fieldOfView - (1 * zoomspeed),40,90);
//				print ("zoomin");
//			}
			Mag = Vector2.zero;
			
		} 
		else if(Input.touchCount == 0)
			zoom = false;
		////////////////////////////////////double tapping//////////////////
		bool doubleTapD = false;
		
		#region doubleTapD
		
		if (Input.GetButtonDown("Fire1"))
		{
			if (Time.time < _doubleTapTimeD + .3f)
			{
				float dis = (Input.mousePosition-temp).magnitude;
				if(dis < 25)
				{
					doubleTapD = true;
				}

			}
			_doubleTapTimeD = Time.time;

				temp = Input.mousePosition;
		}
		
		#endregion
		
		if (doubleTapD)
		{
			print("DoubleTapD");

			dt = true;
		}
		if(dt)
		{
			//if(selectedCamera.fieldOfView > 60)
			//selectedCamera.fieldOfView = Mathf.Lerp(selectedCamera.fieldOfView,40,Time.deltaTime * 3);
			//selectedCamera.transform.position = new Vector3(Mathf.Lerp(selectedCamera.transform.position.x,temp.x,Time.deltaTime*5),selectedCamera.transform.position.y,Mathf.Lerp(selectedCamera.transform.position.z,temp.y,Time.deltaTime*5));
//			else
//				selectedCamera.fieldOfView = Mathf.Lerp(selectedCamera.fieldOfView,90,Time.deltaTime * 3);
		}


		////////////////////////////////////////swiping//////////////////////
		if(!zoom)
		{
			if (FingerUp)
			{
				Mag.x = Mathf.Lerp (Mag.x, 0, Time.deltaTime * 3);
				Mag.y = Mathf.Lerp (Mag.y, 0, Time.deltaTime * 3);
			}
			if(Camera.main.transform.localPosition.x <= rightside && Camera.main.transform.localPosition.x >= leftside)
//			if(!boundaryLeft.renderer.isVisible && !boundaryRight.renderer.isVisible)
			{

				//print (Mag.x);
//				print("Should move");
				Camera.main.transform.position = new Vector3(Mathf.Lerp(Camera.main.transform.position.x,Camera.main.transform.position.x - Mag.x,Time.deltaTime *0.025f),Camera.main.transform.position.y,Camera.main.transform.position.z);
			}
			if(Camera.main.transform.localPosition.z <= upside && Camera.main.transform.localPosition.z >= downside)
			//if(!boundaryUp.renderer.isVisible && !boundaryDown.renderer.isVisible)
			{

				Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,Mathf.Lerp(Camera.main.transform.position.z,Camera.main.transform.position.z + Mag.y,Time.deltaTime *0.025f));
			}
			if (Camera.main.transform.localPosition.x > rightside)
				Camera.main.transform.localPosition = new Vector3 (rightside-0.01f, Camera.main.transform.localPosition.y, Camera.main.transform.localPosition.z);
			if (Camera.main.transform.localPosition.x < leftside)
				Camera.main.transform.localPosition = new Vector3 (leftside+0.01f, Camera.main.transform.localPosition.y, Camera.main.transform.localPosition.z);
			if (Camera.main.transform.localPosition.z > upside)
				Camera.main.transform.localPosition = new Vector3 (Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, upside-0.01f ) ;
			if (Camera.main.transform.localPosition.z < downside)
				Camera.main.transform.localPosition = new Vector3 (Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y,downside+0.01f);
		
			////////////////////////////////////// NEW CODE ////////////////////////////////
		
//			if(!boundaryLeft.renderer.isVisible && Mag.x > 0 && !boundaryUpLeft.renderer.isVisible && !boundaryDownLeft.renderer.isVisible)
//			{
//				print ("moving rightside");
//				Camera.main.transform.position = new Vector3(Mathf.Lerp(Camera.main.transform.position.x,Camera.main.transform.position.x - Mag.x,Time.deltaTime *0.05f),Camera.main.transform.position.y,Camera.main.transform.position.z);
//			}
//			if(!boundaryRight.renderer.isVisible && Mag.x < 0 && !boundaryUpRight.renderer.isVisible && !boundaryDownRight.renderer.isVisible){
//
//				Camera.main.transform.position = new Vector3(Mathf.Lerp(Camera.main.transform.position.x,Camera.main.transform.position.x - Mag.x,Time.deltaTime *0.05f),Camera.main.transform.position.y,Camera.main.transform.position.z);
//			}
//
//			if(!boundaryUp.renderer.isVisible && Mag.y > 0){
//				print ("moving Upside");
//				Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,Mathf.Lerp(Camera.main.transform.position.z,Camera.main.transform.position.z + Mag.y,Time.deltaTime *0.05f));
//			}
//			if(!boundaryDown.renderer.isVisible && Mag.y < 0){
//				print ("moving Downside");
//				Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,Mathf.Lerp(Camera.main.transform.position.z,Camera.main.transform.position.z + Mag.y,Time.deltaTime *0.05f));
//
//			}

			////////////////////////////////////// NEW CODE ////////////////////////////////

		}

		
		

	

	}
	void Begin1(int fid, Vector2 pos)
	{
		if(!zoom)
		{
			Current = pos;
			Previous = pos;
			StartTime = Time.time;
			Mag = Vector2.zero;
		}

	}
	void Ended1(int fid, Vector2 pos)
	{

		if(!zoom)
		{
			//		print("x" + Mag.x);
			//		print("y" + Mag.y);
//			speed = Mag.magnitude / (Time.time - StartTime);
			//		print (speed.ToString ());
			//Mag = Vector2.zero;
			FingerUp = true;
			Previous = Vector2.zero;
			Current = Vector2.zero;
//			StartTime = 0;
		}

	}
	void Cancelled1(int fid, Vector2 pos)
	{
		if(!zoom)
		{

		}
	}
	void Moved1(int fid, Vector2 pos)
	{
		dt = false;
		if(!zoom)
		{
			if(Input.GetButton("Fire1"))
			{
				Current = pos;
				if (Current.x > Previous.x) 
				{
					//				if(Camera.main.transform.localPosition.x <= 2.84)
					//					Camera.main.transform.Translate(Vector3.right * Time.deltaTime* 2000);
					
				}
				if(Current.x < Previous.x)
				{
					//				if(Camera.main.transform.localPosition.x >= -4441)
					//					Camera.main.transform.Translate(Vector3.left * Time.deltaTime* 2000);
				}
				if(Current.y > Previous.y)
				{
					//				if(Camera.main.transform.localPosition.z >= -4609)
					//					Camera.main.transform.Translate(Vector3.down * Time.deltaTime* 2000);
				}
				if(Current.y < Previous.y)
				{
					//				if(Camera.main.transform.localPosition.z <= 4623)
					//					Camera.main.transform.Translate(Vector3.up * Time.deltaTime* 2000);
				}
				Mag = Mag + (Current - Previous);
				Previous = Current;
			}
		}



	}
	void Stationary1(int fid, Vector2 pos)
	{
		if(!zoom)
		{

		}

	}
	
//	void OnDestroy()
//	{
//
//		TouchHandler.SharedHandler ().onTouchesBegin -= Begin1;
//		TouchHandler.SharedHandler ().onTouchesEnded -= Ended1;
//		TouchHandler.SharedHandler ().onTouchesCancelled -= Cancelled1;
//		TouchHandler.SharedHandler ().onTouchesMoved -= Moved1;
//		TouchHandler.SharedHandler ().onTouchesStationary -= Stationary1;
//	}

}
