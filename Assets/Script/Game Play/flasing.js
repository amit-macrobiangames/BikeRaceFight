//var image : Texture2D;
// //This is the image to display on the screen.
// var flashLength : float;
// var imageScreenCoords : Rect;
// //You will probably want to make this more adaptive to the user's screen.
// private var imageObject : GUITexture;   
// //This is property syntax is js.  It looks wierd, but it's a getter just like C#        
// function get ImageObject() : GUITexture {
//     if(imageObject == null) {
//         var gameObj : GameObject = new GameObject("Image Object");
//         gameObj.transform.localScale = new Vector3(0, 0, 1);
//         imageObject = gameObj.AddComponent(GUITexture);
//         imageObject.texture = image;
//         imageObject.pixelInset = imageScreenCoords;
//     }
//     return imageObject;
// }
// //This works, but if you are against properties (or my implementation) then you can change it.
// //It is not a critical part of this example.
// function FlashImage () {
//     ImageObject.enabled = true;
//     Invoke("HideImage", flashLength);
// }
// function HideImage () {
//     ImageObject.enabled = false;
// }   
 
// Texture2D t = new Texture2D( 1, 1 ); 
// Color currentBlendColor = new Color( 1, 0, 0, 1 ); // Opaque red 
// Color fromColor = new Color( 1, 0, 0, 1 ); // Opaque red 
// Color toColor = new Color( 1, 0, 0, 0 ); // transparent red 
// float speed = 5;

//Set colour to red t.SetPixel( 0, 0, Color.white );

//void OnGUI() { // Now each GUI draw the texture and blend it in. 
//currentBlendColor = Color.Lerp( currentBlendColor , toColor, Time.deltaTime * speed ); // Set GUI color GUI.color = currentBlendColor;
//}
// Draw fade GUI.DrawTexture(Rect(0,0,Screen.width,Screen.Height), t, ScaleMode.ScaleToFit);