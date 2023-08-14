
private var revertFogState = false;

public var isEnabled:boolean;
function OnPreRender () {
	revertFogState = RenderSettings.fog;
	RenderSettings.fog = isEnabled;
}
 
function OnPostRender () {
	RenderSettings.fog = revertFogState;
}
 
@script AddComponentMenu ("Rendering/Fog Layer")
@script RequireComponent (Camera)