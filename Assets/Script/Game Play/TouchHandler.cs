using UnityEngine;
using System.Collections;


public class TouchHandler : MonoBehaviour
{
	public static bool stimulateMouseTouches = false;
	public static float frequency = 20f;
	private static TouchHandler handler = null;
	private float lastTime = 0f;

	// TOUCHES BEGAN
	public delegate void TTouches(int fingerID, Vector2 touchPosition);

	public TTouches onTouchesBegin;
	public TTouches onTouchesMoved;
	public TTouches onTouchesCancelled;
	public TTouches onTouchesEnded;
	public TTouches onTouchesStationary;

	public void Awake()
	{

		lastTime = Time.time;
	}


	void OnApplicationQuit()
	{
		TouchHandler.SharedHandler ().onTouchesBegin = null;
		TouchHandler.SharedHandler ().onTouchesEnded = null;
		TouchHandler.SharedHandler ().onTouchesCancelled = null;
		TouchHandler.SharedHandler ().onTouchesMoved = null;
		TouchHandler.SharedHandler ().onTouchesStationary = null;
		Destroy (gameObject);
	}

	public static TouchHandler SharedHandler()
	{
		if(handler == null)
		{
			GameObject go = new GameObject("TouchHandler");
			go.AddComponent<TouchHandler>();
			DontDestroyOnLoad(go);
			handler = go.GetComponent<TouchHandler>();
		}
		return handler;
	}



	void Update()
	{
		//if(Time.time - lastTime < 1/frequency) return;

		if((Application.platform == RuntimePlatform.WindowsEditor || 
		   Application.platform == RuntimePlatform.OSXEditor ||
		   Application.platform == RuntimePlatform.OSXPlayer ||
		   Application.platform == RuntimePlatform.WindowsPlayer) && stimulateMouseTouches == true)
		{
			int fid = 0;

			Vector2 position = new Vector2(Input.mousePosition.x,Screen.height-Input.mousePosition.y);
					
			// Button 0
			if(Input.GetMouseButtonDown(0)  && onTouchesBegin != null)
			{
				fid = 0;
				onTouchesBegin(fid, position);
			}
			else if(Input.GetMouseButtonUp(0)  && onTouchesEnded != null)
			{
				fid = 0;
				onTouchesEnded(fid, position);
			}
		
			// Button 1
			if(Input.GetMouseButtonDown(1)  && onTouchesBegin != null)
			{
				fid = 1;
				onTouchesBegin(fid, position);
			}
			else if(Input.GetMouseButtonUp(1)  && onTouchesEnded != null)
			{
				fid = 1;
				onTouchesEnded(fid, position);
			}
		
			// Button 2
			if(Input.GetMouseButtonDown(2)  && onTouchesBegin != null)
			{
				fid = 2;
				onTouchesBegin(fid, position);
			}
			else if(Input.GetMouseButtonUp(2)  && onTouchesEnded != null)
			{
				fid = 2;
				onTouchesEnded(fid, position);
			}
		
			// No Button Simulation on MOVE AND END
			else if((Mathf.Abs(Input.GetAxis("Mouse X")) > 0 || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0)  && onTouchesMoved != null) 
			{
				fid = -1;
				onTouchesMoved(fid, position);
			}
			else if((Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height) && onTouchesCancelled != null) 
			{
				fid = -1;
				onTouchesCancelled(fid, position);
			}


		}

		else
		{
			foreach(Touch touch in Input.touches)
			{
				int fid = touch.fingerId;
			
				Vector2 position = new Vector2(touch.position.x,Screen.height-touch.position.y);
			
				if(touch.phase == TouchPhase.Began && onTouchesBegin != null)
					onTouchesBegin(fid, position);
			
				else if(touch.phase == TouchPhase.Moved && onTouchesMoved != null)
					onTouchesMoved(fid, position);
			
				else if(touch.phase == TouchPhase.Canceled && onTouchesCancelled != null)
					onTouchesCancelled(fid, position);
			
				else if(touch.phase == TouchPhase.Ended && onTouchesEnded != null)
					onTouchesEnded(fid, position);

				else if(touch.phase == TouchPhase.Stationary && onTouchesStationary != null)
					onTouchesStationary(fid, position);
			}
		}
	}
}
