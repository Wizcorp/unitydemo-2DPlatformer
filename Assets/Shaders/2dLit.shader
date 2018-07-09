Shader "Custom/2D Lit" {
	Properties {
		// Sprite and tint
		_Color ("Color", Color) = (1,1,1,1)
		[NoScaleOffset] _MainTex ("Sprite", 2D) = "white" {}

		// To define how dark should a base directional light be
		_MinValue ("Minimum Value", Float) = 0.2
	}

	SubShader {
		Tags {
			"Queue"="Transparent" "RenderType"="Transparent"
		}

		Cull Off

		Pass {
			Tags {
				"LightMode"="ForwardBase"
			}

			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma target 3.0

			#pragma multi_compile_fwdadd

			#pragma vertex Vert
			#pragma fragment Frag

			#define FORWARD_BASE_PASS

			#include "2dLit.cginc"

			ENDCG
		}

		Pass {
			Tags {
				"LightMode"="ForwardAdd"
			}

			Blend SrcAlpha One
			ZWrite Off

			CGPROGRAM

			#pragma target 3.0

			#pragma multi_compile_fwdadd

			#pragma vertex Vert
			#pragma fragment Frag

			#include "2dLit.cginc"

			ENDCG
		}
	}
}
