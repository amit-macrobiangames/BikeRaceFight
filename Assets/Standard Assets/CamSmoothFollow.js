/*
This camera smoothes out rotation around the y-axis and height.
Horizontal Distance to the target is always fixed.

There are many different ways to smooth the rotation but doing it this way gives you a lot of control over how the camera behaves.

For every of those smoothed values we calculate the wanted value and the current value.
Then we smooth it using the Lerp function.
Then we apply the smoothed values to the transform's position.
*/

// The target we are following
var target : Transform;
// The distance in the x-z plane to the target
var distance = 10.0;
// the height we want the camera to be above the target
var height = 5.0;
public static var reverse:boolean;
// How much we 
var heightDamping = 2.0;
var rotationDamping = 3.0;
  var backAngle:float=0.0f;

;

// Place the script in the Camera-Control group in the component menu
@script AddComponentMenu("Camera-Control/Smooth Follow")

function LateUpdate () {
	// Early out if we don't have a target
	if (!target)
		return;
	
	if (reverse)
            {
                backAngle = 0;

            }
            else
            {
                backAngle = 180;
            }
	// Calculate the current rotation angles
	var wantedRotationAngle = target.eulerAngles.y+backAngle;
	var wantedHeight = target.position.y + height;
		
	var currentRotationAngle = transform.eulerAngles.y;
	var currentHeight = transform.position.y;
	
	// Damp the rotation around the y-axis
	currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
//print(wantedRotationAngle+ " : "+rotationDamping*Time.deltaTime);
	// Damp the height
	currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);

	// Convert the angle into a rotation
	
	

	var currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
	
	// Set the position of the camera on the x-z plane to:
	// distance meters behind the target
	transform.position = target.position;
	transform.position -= currentRotation * Vector3.forward * distance;

	// Set the height of the camera
	transform.position.y = currentHeight;
	
	// Always look at the target
	transform.LookAt (target);
	
}	
	
	
	
//	var yAngle:float = Mathf.SmoothDampAngle(transform.eulerAngles.y,target.eulerAngles.y-180,  yVelocity, smooth);
//
//
//            var xAngle:float = Mathf.SmoothDampAngle(transform.eulerAngles.x,target.eulerAngles.x + Angle,  xVelocity, smooth);
//			//print(yAngle+": "+xAngle);
//            // Position at the target
//            var position:Vector3 = target.position;
//            // Then offset by distance behind the new angle
//            position += Quaternion.Euler(Angle, yAngle, 0) * Vector3(0, 0, -distance);
//            // Apply the position
//            //  transform.position = position;
//
//            // Look at the target
//            transform.eulerAngles =  Vector3(Angle, yAngle, 0);
//
//            var direction = transform.rotation * -Vector3.forward;
//              var targetDistance = AdjustLineOfSight(target.position +  Vector3(0, haight, 0), direction);
//
//            transform.position = target.position +  Vector3(0, haight, 0) + direction* targetDistance ;
//            
//            
//
//}
//
//
//  public var lineOfSightMask:LayerMask = 0;
//
//
//
//  function AdjustLineOfSight(target:Vector3, direction:Vector3 ):float
//   {
//
//
//       var hit: RaycastHit;
//
//        if (Physics.Raycast(target, direction,  hit, distance, lineOfSightMask.value))
//            return hit.distance;
//        else
//            return distance;
//
//    }