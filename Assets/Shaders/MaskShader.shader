Shader "Custom/Mask" {
	Properties {
		_MainTex ("Main Texture",2D) = "white" {}
		_Mask("MaskTexture",2D)= "white" {}
		
		
	}
	SubShader {
	Tags { "Queue"= "Transparent"}
	Lighting On
	Zwrite Off
	Blend SrcAlpha OneMinusSrcAlpha
	
	Pass{
	
	SetTexture[_Mask]{combine texture}
	SetTexture[_MainTex]{combine texture, previous}
	 //renderer.material.SetTextureScale("_MainTex", new Vector2(-1,1));
	}
	
		
	} 
	}
