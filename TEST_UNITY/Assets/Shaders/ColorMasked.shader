Shader "Custom/ColorMasked" {
	Properties {
		_Color ("ColorMask", Color) = (0,0,0,0)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MaskTex("Mask (RGB)", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		//_OcclusionStrength("Strength", Range(0.0, 1.0)) = 1.0
		_OcclusionMap("Occlusion", 2D) = "white" {}
		[LM_Metallic][LM_Glossiness] _MetallicGlossMap("Metallic", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex,_MaskTex,_MetallicGlossMap,_OcclusionMap,_Normal;
		fixed3 _Color;

		struct Input {
			float2 uv_MainTex;
			float2 uv_MaskTex;
			float2 uv_MetallicGlossMap;
			float2 uv_Normal;
			float2 uv_OcclusionMap;
		};

		//half _Glossiness;
		//half _Metallic;
		//fixed4 _Color;
		//half _OcclusionStrength;
		//half _OcclusionMap;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			//fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			//o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;
			//o.Alpha = c.a;
			float4 c = tex2D(_MainTex, IN.uv_MainTex);
			float mask = tex2D(_MaskTex, IN.uv_MainTex).r;
			float4 mr = tex2D(_MetallicGlossMap, IN.uv_MetallicGlossMap);
			float4 n = tex2D(_Normal, IN.uv_Normal);
			float ao = tex2D(_OcclusionMap, IN.uv_OcclusionMap);
			//ao.r = 1 + ao - _OcclusionStrength;
			c.rgb = c.rgb * (1 - mask) + _Color * mask;

			o.Albedo = c.rgb;
			o.Normal = UnpackNormal(n);
			o.Metallic = mr.r;
			o.Smoothness = mr.a;
			o.Occlusion = ao;
			//o.OcclusionStrength = _OcclusionStrength;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
