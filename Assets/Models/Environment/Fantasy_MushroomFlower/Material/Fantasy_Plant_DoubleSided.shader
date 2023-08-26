// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Fantasy_Plant_DoubleSided"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_Albedo_Color("Albedo_Color", Color) = (1,1,1,1)
		_Specular("Specular", 2D) = "white" {}
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_Normal("Normal", 2D) = "bump" {}
		_AO("AO", 2D) = "white" {}
		_Emission("Emission", 2D) = "white" {}
		[HDR]_Emission_Color("Emission_Color", Color) = (0.9559735,0.9559735,0.9559735,1)
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))//ASE Sampler Macros
		#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
		#else//ASE Sampling Macros
		#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex2D(tex,coord)
		#endif//ASE Sampling Macros

		#pragma surface surf StandardSpecular keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		UNITY_DECLARE_TEX2D_NOSAMPLER(_Normal);
		SamplerState sampler_Normal;
		uniform float4 _Albedo_Color;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_Albedo);
		SamplerState sampler_Albedo;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_Emission);
		SamplerState sampler_Emission;
		uniform float4 _Emission_Color;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_Specular);
		SamplerState sampler_Specular;
		uniform float _Smoothness;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_AO);
		SamplerState sampler_AO;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			o.Normal = UnpackNormal( SAMPLE_TEXTURE2D( _Normal, sampler_Normal, i.uv_texcoord ) );
			float4 tex2DNode3 = SAMPLE_TEXTURE2D( _Albedo, sampler_Albedo, i.uv_texcoord );
			o.Albedo = ( _Albedo_Color * tex2DNode3 ).rgb;
			o.Emission = ( SAMPLE_TEXTURE2D( _Emission, sampler_Emission, i.uv_texcoord ) * _Emission_Color ).rgb;
			o.Specular = SAMPLE_TEXTURE2D( _Specular, sampler_Specular, i.uv_texcoord ).rgb;
			o.Smoothness = _Smoothness;
			o.Occlusion = SAMPLE_TEXTURE2D( _AO, sampler_AO, i.uv_texcoord ).r;
			o.Alpha = 1;
			clip( tex2DNode3.a - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18400
38;239;1229;607;1331.626;91.33762;1.804464;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-1144.415,436.7489;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-645.9623,-138.7384;Inherit;True;Property;_Albedo;Albedo;0;0;Create;True;0;0;False;0;False;-1;c873f0a8fabf78c478cf49f7ff68e29f;c189080ec9ef43a4f9dd06d69bfb9119;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-561.3004,-331.8419;Inherit;False;Property;_Albedo_Color;Albedo_Color;1;0;Create;True;0;0;False;0;False;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;9;-631.7277,765.1172;Inherit;True;Property;_Emission;Emission;6;0;Create;True;0;0;False;0;False;-1;fc2b35c8a18b4254994aa21bc6c634a5;329b86b36e819c4478fdd9b948a71dc7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;10;-546.8116,967.7151;Inherit;False;Property;_Emission_Color;Emission_Color;7;1;[HDR];Create;True;0;0;False;0;False;0.9559735,0.9559735,0.9559735,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;6;-644.5327,88.58817;Inherit;True;Property;_Specular;Specular;2;0;Create;True;0;0;False;0;False;-1;bb597a8f68e0b8d4790edac11f6227a3;a233a159db21b0c49b01f8932949e535;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;7;-640.2643,310.5411;Inherit;True;Property;_Normal;Normal;4;0;Create;True;0;0;False;0;False;-1;c622e925e3ede2045abb343f0228be77;ecfb4b45967662645af88ade9b582133;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;8;-642.3985,541.0305;Inherit;True;Property;_AO;AO;5;0;Create;True;0;0;False;0;False;-1;09fe87bf0149f604297ff6c3da869b89;d4a69f3d66d444c479491447574c24c5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-258.6759,-199.979;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-254.4083,852.1629;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-329.3626,157.1039;Inherit;False;Property;_Smoothness;Smoothness;3;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;199.822,-80.52528;Float;False;True;-1;2;ASEMaterialInspector;0;0;StandardSpecular;Fantasy_Plant_DoubleSided;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;8;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;True;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;1;4;0
WireConnection;9;1;4;0
WireConnection;6;1;4;0
WireConnection;7;1;4;0
WireConnection;8;1;4;0
WireConnection;11;0;5;0
WireConnection;11;1;3;0
WireConnection;12;0;9;0
WireConnection;12;1;10;0
WireConnection;0;0;11;0
WireConnection;0;1;7;0
WireConnection;0;2;12;0
WireConnection;0;3;6;0
WireConnection;0;4;13;0
WireConnection;0;5;8;0
WireConnection;0;10;3;4
ASEEND*/
//CHKSM=F543D2D2C8F6BF5F4E013119C6363CE4AF045C84