////////////////////////////////////////
// Generated with Better Shaders
//
// Auto-generated shader code, don't hand edit!
//
//   Unity Version: 2021.3.1f1
//   Render Pipeline: URP2021
//   Platform: WindowsEditor
////////////////////////////////////////


Shader "Andicraft/Retro Master (NOT Transparent)"
{
   Properties
   {
      [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
      [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
      [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
      
	TEXTURES ("# Textures", Int) = 0
		[KeywordEnum(Nearest, Linear, N64)] _TextureFiltering ("Texture Filtering Mode", Int) = 0
		[MainTexture] _BaseMap ("Color &&", 2D) = "white" {}
			[MainColor] _Color ("Tint", Color) = (1, 1, 1, 1)
		[Toggle] _EnableMaskMap ("Mask Map", Int) = 1
		_MaskNote1 ("!NOTE Using Color Map Alpha as Smoothness [_EnableMaskMap == 0]", Int) = 0
		_MaskMap ("Mask Map & [_EnableMaskMap]", 2D) = "" {}
		_MaskNote2 ("!NOTE R: Metal G: Smoothness B: Occlusion", Int) = 0
			SMOOTHSLIDER ("-!DRAWER MinMax _SmoothRemap.x _SmoothRemap.y", Int) = 1
		_SmoothRemap ("Remap Smoothness", Vector) = (0, 1, 0, 0)
		[Normal] _BumpMap ("Normal Map &&", 2D) = "bump" {}
			_NormalStrength ("Normal Map Strength", Range(0,2)) = 1
		[Toggle(_EMISSION)] _Emission ("Emission", Int) = 0
			_EmissionMap ("-Emission Map && [_EMISSION]", 2D) = "white" {}
			[HDR] _EmissionColor ("-Emission Color [_EMISSION]", Color) = (0,0,0,1)
			_LightmapEmissionStr ("-Lightmap Emission Strength", Float) = 1
		_TilingOffset ("Tiling/Offset &", Vector) = (1,1,0,0)

	_Lighting ("# Lighting", Int) = 0
		[KeywordEnum(Pixel, Vertex, Texel)] _LightingType ("Lighting Type", Int) = 2
		[KeywordEnum(CookTorrance, Phong, Blinn Phong, Disabled)] _SpecularType ("Specular Type", Int) = 0
		[Toggle] _HalfLambert ("Half Lambert", Int) = 0
		[Toggle] _Reflections ("Reflections", Int) = 1
		[Toggle(_TOONSHADING)] _ToonShading ("Toon Shading", Int) = 0
		_DITHERHEADER ("## Dithering", Int) = 0
		[Toggle] _DitherDiffuse ("-Dither Diffuse Light", Int) = 0
		[Toggle] _DitherSpec ("-Dither Specular Light", Int) = 0
		[Toggle] _DitherAmbient ("-Dither Ambient & Lightmaps", Int) = 0
		[Toggle] _DoubleSizeDither ("-Double Size Dither", Int) = 0


	_Extras ("# Extras", Int) = 0
		[Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull Mode", Float) = 0
		[Toggle()] _VColEmissive ("Vertex Color as Emissive", Int) = 0
			_VertexEmissiveStr ("- Emissive Strength [_VColEmissive]", Float) = 1
		[Toggle(_ALPHATEST_ON)] _AlphaTest ("Alpha Cutoff", Int) = 0
			_Cutoff ("-Alpha Cutoff Threshold [_ALPHATEST_ON]", Range(0,1)) = 0.5
		[Toggle(_VERTEXJITTER)] _VertexJitter ("Vertex Snap", Int) = 0
			_VertexJitterResX ("- Virtual Screen X Resolution [_VERTEXJITTER]", Int) = 320
			_VertexJitterResY ("- Virtual Screen Y Resolution [_VERTEXJITTER]", Int) = 240
		[Toggle(_AFFINE_MAPPING)] _AffineMapping ("Affine UV Mapping", Int) = 0
			_AffineMapFactor ("-Warp Factor [_AFFINE_MAPPING]", Range(0,1)) = 1
		[Toggle] _MaskTint ("Mask Color Tint", Int) = 0
			_TintNote ("!NOTE Using Mask Map A channel to mask color tint value [_MaskTint]", Int) = 0

	_ToonShading ("# Toon Shading [_TOONSHADING]", Int) = 0
		_ToonRamp ("Diffuse Ramp&", 2D) = "white" {}
		_SpecularRamp ("Specular Ramp&", 2D) = "white" {}
		RAMPGENERATOR ("!DRAWER Gradient _RampGen", Int) = 0
		_RampGen ("Ramp Generator", 2D) = "white" {}

	_VertexLighting ("# Vertex Light Settings [_LIGHTINGTYPE_VERTEX]", Int) = 0
		_VertexSpecColor ("Specular Color", Color) = (1, 1, 1, 1)
		[Toggle(_VCOLSMOOTHNESS)] _VColSmoothness ("Use Vertex Color A as Smoothness", Int) = 0
			SMOOTHSLIDER2 ("-!DRAWER MinMax _SmoothRemapVCol.x _SmoothRemapVCol.y [_VCOLSMOOTHNESS]", Int) = 1
			_SmoothRemapVCol ("Remap Smoothness", Vector) = (0, 1, 0, 0)
		_VertexLightSmoothness ("Smoothness [!_VCOLSMOOTHNESS]", Range(0,1)) = 0.5

	[HideInInspector] _AlphaClip("__clip", Float) = 0.0



   }
   SubShader
   {
      Tags { "RenderPipeline"="UniversalPipeline" "RenderType" = "Opaque" "UniversalMaterialType" = "Unlit" "Queue" = "Geometry" }

      

      
        Pass
        {
            Name "Universal Forward"
            Tags 
            { 
                "LightMode" = "UniversalForward"
            }
            Cull Back
            Blend One Zero
            ZTest LEqual
            ZWrite On

            Blend One Zero, One Zero
Cull Back
ZTest LEqual
ZWrite On

            	Cull [_Cull]


            HLSLPROGRAM

               #pragma vertex Vert
   #pragma fragment Frag

            #pragma target 3.0

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma multi_compile_fog
            #pragma multi_compile_instancing
            #pragma instancing_options renderinglayer
    
            // Keywords
            #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile _ DYNAMICLIGHTMAP_ON
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile_fragment _ _REFLECTION_PROBE_BLENDING
            #pragma multi_compile_fragment _ _REFLECTION_PROBE_BOX_PROJECTION
            #pragma multi_compile_fragment _ _SHADOWS_SOFT
            #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
            #pragma multi_compile _ SHADOWS_SHADOWMASK
            #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
            #pragma multi_compile_fragment _ _LIGHT_LAYERS
            #pragma multi_compile_fragment _ DEBUG_DISPLAY
            #pragma multi_compile_fragment _ _LIGHT_COOKIES
            #pragma multi_compile _ _CLUSTERED_RENDERING
            // GraphKeywords: <None>

            #define SHADER_PASS SHADERPASS_FORWARD
            #define VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
            #define _PASSFORWARD 1
            #define _FOG_FRAGMENT 1
            

            
	#pragma multi_compile_local _LIGHTINGTYPE_PIXEL _LIGHTINGTYPE_VERTEX _LIGHTINGTYPE_TEXEL
	#pragma multi_compile_local _TEXTUREFILTERING_NEAREST _TEXTUREFILTERING_LINEAR _TEXTUREFILTERING_N64
	#pragma shader_feature_local _VERTEXJITTER
	#pragma shader_feature_local _VCOLSMOOTHNESS
	#pragma shader_feature_local _TOONSHADING
	#pragma shader_feature_local _EMISSION
	#pragma shader_feature_local _ALPHATEST_ON
	#pragma shader_feature_local _AFFINE_MAPPING
	#undef _UNLIT


   #define _URP 1

#define _UNLIT 1
#define _USINGTEXCOORD1 1


            // this has to be here or specular color will be ignored. Not in SG code
            #if _SIMPLELIT
               #define _SPECULAR_COLOR
            #endif


            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
            

        

               #undef WorldNormalVector
      #define WorldNormalVector(data, normal) mul(normal, data.TBNMatrix)
      
      #define UnityObjectToWorldNormal(normal) mul(GetObjectToWorldMatrix(), normal)

      #define _WorldSpaceLightPos0 _MainLightPosition
      
      #define UNITY_DECLARE_TEX2D(name) TEXTURE2D(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2D_NOSAMPLER(name) TEXTURE2D(name);
      #define UNITY_DECLARE_TEX2DARRAY(name) TEXTURE2D_ARRAY(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(name) TEXTURE2D_ARRAY(name);

      #define UNITY_SAMPLE_TEX2DARRAY(tex,coord)            SAMPLE_TEXTURE2D_ARRAY(tex, sampler##tex, coord.xy, coord.z)
      #define UNITY_SAMPLE_TEX2DARRAY_LOD(tex,coord,lod)    SAMPLE_TEXTURE2D_ARRAY_LOD(tex, sampler##tex, coord.xy, coord.z, lod)
      #define UNITY_SAMPLE_TEX2D(tex, coord)                SAMPLE_TEXTURE2D(tex, sampler##tex, coord)
      #define UNITY_SAMPLE_TEX2D_SAMPLER(tex, samp, coord)  SAMPLE_TEXTURE2D(tex, sampler##samp, coord)

      #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod)   SAMPLE_TEXTURE2D_LOD(tex, sampler_##tex, coord, lod)
      #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) SAMPLE_TEXTURE2D_LOD (tex, sampler##samplertex,coord, lod)
     
      #if defined(UNITY_COMPILER_HLSL)
         #define UNITY_INITIALIZE_OUTPUT(type,name) name = (type)0;
      #else
         #define UNITY_INITIALIZE_OUTPUT(type,name)
      #endif

      #define sampler2D_float sampler2D
      #define sampler2D_half sampler2D

      

      // data across stages, stripped like the above.
      struct VertexToPixel
      {
         float4 pos : SV_POSITION;
         float3 worldPos : TEXCOORD0;
         float3 worldNormal : TEXCOORD1;
         float4 worldTangent : TEXCOORD2;
          float4 texcoord0 : TEXCOORD3;
          float4 texcoord1 : TEXCOORD4;
         // float4 texcoord2 : TEXCOORD5;

         // #if %TEXCOORD3REQUIREKEY%
         // float4 texcoord3 : TEXCOORD6;
         // #endif

         // #if %SCREENPOSREQUIREKEY%
          float4 screenPos : TEXCOORD7;
         // #endif

         // #if %VERTEXCOLORREQUIREKEY%
          half4 vertexColor : COLOR;
         // #endif

         #if defined(LIGHTMAP_ON)
            float2 lightmapUV : TEXCOORD8;
         #endif
         #if defined(DYNAMICLIGHTMAP_ON)
            float2 dynamicLightmapUV : TEXCOORD9;
         #endif
         #if !defined(LIGHTMAP_ON)
            float3 sh : TEXCOORD10;
         #endif

         #if defined(VARYINGS_NEED_FOG_AND_VERTEX_LIGHT)
            float4 fogFactorAndVertexLight : TEXCOORD11;
         #endif

         #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
           float4 shadowCoord : TEXCOORD12;
         #endif

         // #if %EXTRAV2F0REQUIREKEY%
          float4 extraV2F0 : TEXCOORD13;
         // #endif

         // #if %EXTRAV2F1REQUIREKEY%
          float4 extraV2F1 : TEXCOORD14;
         // #endif

         // #if %EXTRAV2F2REQUIREKEY%
          float4 extraV2F2 : TEXCOORD15;
         // #endif

         // #if %EXTRAV2F3REQUIREKEY%
          float4 extraV2F3 : TEXCOORD16;
         // #endif

         // #if %EXTRAV2F4REQUIREKEY%
          float4 extraV2F4 : TEXCOORD17;
         // #endif

         // #if %EXTRAV2F5REQUIREKEY%
         // float4 extraV2F5 : TEXCOORD18;
         // #endif

         // #if %EXTRAV2F6REQUIREKEY%
         // float4 extraV2F6 : TEXCOORD19;
         // #endif

         // #if %EXTRAV2F7REQUIREKEY%
         // float4 extraV2F7 : TEXCOORD20;
         // #endif

         #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
         #endif
         #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
         #endif
         #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
         #endif
         #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
         #endif
      };


         
            
            // data describing the user output of a pixel
            struct Surface
            {
               half3 Albedo;
               half Height;
               half3 Normal;
               half Smoothness;
               half3 Emission;
               half Metallic;
               half3 Specular;
               half Occlusion;
               half SpecularPower; // for simple lighting
               half Alpha;
               float outputDepth; // if written, SV_Depth semantic is used. ShaderData.clipPos.z is unused value
               // HDRP Only
               half SpecularOcclusion;
               half SubsurfaceMask;
               half Thickness;
               half CoatMask;
               half CoatSmoothness;
               half Anisotropy;
               half IridescenceMask;
               half IridescenceThickness;
               int DiffusionProfileHash;
               float SpecularAAThreshold;
               float SpecularAAScreenSpaceVariance;
               // requires _OVERRIDE_BAKEDGI to be defined, but is mapped in all pipelines
               float3 DiffuseGI;
               float3 BackDiffuseGI;
               float3 SpecularGI;
               // requires _OVERRIDE_SHADOWMASK to be defines
               float4 ShadowMask;
            };

            // Data the user declares in blackboard blocks
            struct Blackboard
            {
                
                float blackboardDummyData;
            };

            // data the user might need, this will grow to be big. But easy to strip
            struct ShaderData
            {
               float4 clipPos; // SV_POSITION
               float3 localSpacePosition;
               float3 localSpaceNormal;
               float3 localSpaceTangent;
        
               float3 worldSpacePosition;
               float3 worldSpaceNormal;
               float3 worldSpaceTangent;
               float tangentSign;

               float3 worldSpaceViewDir;
               float3 tangentSpaceViewDir;

               float4 texcoord0;
               float4 texcoord1;
               float4 texcoord2;
               float4 texcoord3;

               float2 screenUV;
               float4 screenPos;

               float4 vertexColor;
               bool isFrontFace;

               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;

               float3x3 TBNMatrix;
               Blackboard blackboard;
            };

            struct VertexData
            {
               #if SHADER_TARGET > 30
               // uint vertexID : SV_VertexID;
               #endif
               float4 vertex : POSITION;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;

               // optimize out mesh coords when not in use by user or lighting system
               #if _URP && (_USINGTEXCOORD1 || _PASSMETA || _PASSFORWARD || _PASSGBUFFER)
                  float4 texcoord1 : TEXCOORD1;
               #endif

               #if _URP && (_USINGTEXCOORD2 || _PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && defined(DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif

               #if _STANDARD && (_USINGTEXCOORD1 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER || _PASSFORWARDADD) && LIGHTMAP_ON)))
                  float4 texcoord1 : TEXCOORD1;
               #endif
               #if _STANDARD && (_USINGTEXCOORD2 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif


               #if _HDRP
                  float4 texcoord1 : TEXCOORD1;
                  float4 texcoord2 : TEXCOORD2;
               #endif

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
                float4 vertexColor : COLOR;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD4; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity    : TEXCOORD5; // Add Precomputed Velocity (Alembic computes velocities on runtime side).
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct TessVertex 
            {
               float4 vertex : INTERNALTESSPOS;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;
               float4 texcoord1 : TEXCOORD1;
               float4 texcoord2 : TEXCOORD2;

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
                float4 vertexColor : COLOR;
               // #endif

               // #if %EXTRAV2F0REQUIREKEY%
                float4 extraV2F0 : TEXCOORD5;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
                float4 extraV2F1 : TEXCOORD6;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
                float4 extraV2F2 : TEXCOORD7;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
                float4 extraV2F3 : TEXCOORD8;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
                float4 extraV2F4 : TEXCOORD9;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // float4 extraV2F5 : TEXCOORD10;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // float4 extraV2F6 : TEXCOORD11;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // float4 extraV2F7 : TEXCOORD12;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD13; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity : TEXCOORD14;
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
               UNITY_VERTEX_OUTPUT_STEREO
            };

            struct ExtraV2F
            {
               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;
               Blackboard blackboard;
               float4 time;
            };


            float3 WorldToTangentSpace(ShaderData d, float3 normal)
            {
               return mul(d.TBNMatrix, normal);
            }

            float3 TangentToWorldSpace(ShaderData d, float3 normal)
            {
               return mul(normal, d.TBNMatrix);
            }

            // in this case, make standard more like SRPs, because we can't fix
            // unity_WorldToObject in HDRP, since it already does macro-fu there

            #if _STANDARD
               float3 TransformWorldToObject(float3 p) { return mul(unity_WorldToObject, float4(p, 1)); };
               float3 TransformObjectToWorld(float3 p) { return mul(unity_ObjectToWorld, float4(p, 1)); };
               float4 TransformWorldToObject(float4 p) { return mul(unity_WorldToObject, p); };
               float4 TransformObjectToWorld(float4 p) { return mul(unity_ObjectToWorld, p); };
               float4x4 GetWorldToObjectMatrix() { return unity_WorldToObject; }
               float4x4 GetObjectToWorldMatrix() { return unity_ObjectToWorld; }
               #if (defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (SHADER_TARGET_SURFACE_ANALYSIS && !SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod) tex.SampleLevel (sampler##tex,coord, lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) tex.SampleLevel (sampler##samplertex,coord, lod)
              #else
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord,lod) tex2D (tex,coord,0,lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord,lod) tex2D (tex,coord,0,lod)
              #endif

               #undef GetWorldToObjectMatrix()

               #define GetWorldToObjectMatrix()   unity_WorldToObject


            #endif

            float3 GetCameraWorldPosition()
            {
               #if _HDRP
                  return GetCameraRelativePositionWS(_WorldSpaceCameraPos);
               #else
                  return _WorldSpaceCameraPos;
               #endif
            }

            #if _GRABPASSUSED
               #if _STANDARD
                  TEXTURE2D(%GRABTEXTURE%);
                  SAMPLER(sampler_%GRABTEXTURE%);
               #endif

               half3 GetSceneColor(float2 uv)
               {
                  #if _STANDARD
                     return SAMPLE_TEXTURE2D(%GRABTEXTURE%, sampler_%GRABTEXTURE%, uv).rgb;
                  #else
                     return SHADERGRAPH_SAMPLE_SCENE_COLOR(uv);
                  #endif
               }
            #endif


      
            #if _STANDARD
               UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
               float GetSceneDepth(float2 uv) { return SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv)); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv)); } 
            #else
               float GetSceneDepth(float2 uv) { return SHADERGRAPH_SAMPLE_SCENE_DEPTH(uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv), _ZBufferParams); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv), _ZBufferParams); } 
            #endif

            float3 GetWorldPositionFromDepthBuffer(float2 uv, float3 worldSpaceViewDir)
            {
               float eye = GetLinearEyeDepth(uv);
               float3 camView = mul((float3x3)GetObjectToWorldMatrix(), transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V)) [2].xyz);

               float dt = dot(worldSpaceViewDir, camView);
               float3 div = worldSpaceViewDir/dt;
               float3 wpos = (eye * div) + GetCameraWorldPosition();
               return wpos;
            }

            #if _HDRP
            float3 ObjectToWorldSpacePosition(float3 pos)
            {
               return GetAbsolutePositionWS(TransformObjectToWorld(pos));
            }
            #else
            float3 ObjectToWorldSpacePosition(float3 pos)
            {
               return TransformObjectToWorld(pos);
            }
            #endif

            #if _STANDARD
               UNITY_DECLARE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture);
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  float4 depthNorms = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture, uv);
                  float3 norms = DecodeViewNormalStereo(depthNorms);
                  norms = mul((float3x3)GetWorldToViewMatrix(), norms) * 0.5 + 0.5;
                  return norms;
               }
            #elif _HDRP
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  NormalData nd;
                  DecodeFromNormalBuffer(_ScreenSize.xy * uv, nd);
                  return nd.normalWS;
               }
            #elif _URP
               #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                  #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareNormalsTexture.hlsl"
               #endif

               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                     return SampleSceneNormals(uv);
                  #else
                     float3 wpos = GetWorldPositionFromDepthBuffer(uv, worldSpaceViewDir);
                     return normalize(-cross(ddx(wpos), ddy(wpos))) * 0.5 + 0.5;
                  #endif

                }
             #endif

             #if _HDRP

               half3 UnpackNormalmapRGorAG(half4 packednormal)
               {
                     // This do the trick
                  packednormal.x *= packednormal.w;

                  half3 normal;
                  normal.xy = packednormal.xy * 2 - 1;
                  normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                  return normal;
               }
               half3 UnpackNormal(half4 packednormal)
               {
                  #if defined(UNITY_NO_DXT5nm)
                     return packednormal.xyz * 2 - 1;
                  #else
                     return UnpackNormalmapRGorAG(packednormal);
                  #endif
               }
               #endif
               #if _HDRP || _URP

               half3 UnpackScaleNormal(half4 packednormal, half scale)
               {
                 #ifndef UNITY_NO_DXT5nm
                   // Unpack normal as DXT5nm (1, y, 1, x) or BC5 (x, y, 0, 1)
                   // Note neutral texture like "bump" is (0, 0, 1, 1) to work with both plain RGB normal and DXT5nm/BC5
                   packednormal.x *= packednormal.w;
                 #endif
                   half3 normal;
                   normal.xy = (packednormal.xy * 2 - 1) * scale;
                   normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                   return normal;
               }	

             #endif


            void GetSun(out float3 lightDir, out float3 color)
            {
               lightDir = float3(0.5, 0.5, 0);
               color = 1;
               #if _HDRP
                  if (_DirectionalLightCount > 0)
                  {
                     DirectionalLightData light = _DirectionalLightDatas[0];
                     lightDir = -light.forward.xyz;
                     color = light.color;
                  }
               #elif _STANDARD
			         lightDir = normalize(_WorldSpaceLightPos0.xyz);
                  color = _LightColor0.rgb;
               #elif _URP
	               Light light = GetMainLight();
	               lightDir = light.direction;
	               color = light.color;
               #endif
            }


            
         CBUFFER_START(UnityPerMaterial)

            

	int _VColEmissive;
	float _VertexEmissiveStr;
	half4 _Color;
	int _HalfLambert;
	int _EnableMaskMap;
	int _ForceNearest;
	int _TextureFiltering;
	int _SpecularType;
	int _Reflections;
	float _NormalStrength;
	float2 _SmoothRemap;
	float2 _SmoothRemapVCol;
	float _VertexLightSmoothness;
	float3 _VertexSpecColor;
	float3 _EmissionColor;
	float4 _TilingOffset;
	float _Cutoff;
	float _AffineMapFactor;
	int _VertexJitterResX;
	int _VertexJitterResY;
	int _DoubleSizeDither;
	int _MaskTint;
	float _LightmapEmissionStr;
	
	int _DitherAmbient;
	int _DitherDiffuse;
	int _DitherSpec;

	float4 _BaseMap_TexelSize;
	float4 _MaskMap_TexelSize;
	float4 _BumpMap_TexelSize;
	float4 _EmissionMap_TexelSize;




         CBUFFER_END

         

         

         #ifdef unity_WorldToObject
#undef unity_WorldToObject
#endif
#ifdef unity_ObjectToWorld
#undef unity_ObjectToWorld
#endif
#define unity_ObjectToWorld GetObjectToWorldMatrix()
#define unity_WorldToObject GetWorldToObjectMatrix()


	/**********
	* TODO:
	* Vertex Colors
	* Meta Pass?
	**********/

	TEXTURE2D(_ToonRamp);
	TEXTURE2D(_SpecularRamp);
	SamplerState sampler_linear_clamp;

	TEXTURE2D(_BaseMap);
	SamplerState sampler_linear_repeat;
	SamplerState sampler_trilinear_repeat;
	SamplerState sampler_point_repeat;

	#if defined(_TEXTUREFILTERING_LINEAR) || defined(PASS_META)
		#define MAINSMP sampler_linear_repeat
	#else
		#define MAINSMP sampler_point_repeat
	#endif

	static const uint bayerPattern[] =
	{
		0, 32,  8, 40,  2, 34, 10, 42,   /* 8x8 Bayer ordered dithering  */
		48, 16, 56, 24, 50, 18, 58, 26,  /* pattern.  Each input pixel   */
		12, 44,  4, 36, 14, 46,  6, 38,  /* is scaled to the 0..63 range */
		60, 28, 52, 20, 62, 30, 54, 22,  /* before looking in this table */
		3, 35, 11, 43,  1, 33,  9, 41,   /* to determine the action.     */
		51, 19, 59, 27, 49, 17, 57, 25,
		15, 47,  7, 39, 13, 45,  5, 37,
		63, 31, 55, 23, 61, 29, 53, 21
	};

	float ditherValue(float2 screenPos)
	{
		uint stippleOffset = ((uint) screenPos.y % 8) * 8 + ((uint) screenPos.x % 8);
		uint byte = bayerPattern[stippleOffset];
		return byte / 64.0f;
	}

	float GetLuminance(float3 col)
	{
		return dot(col, float3(0.299f, 0.587f, 0.114f));
	}

	void ditherColor(inout float3 input, float2 screenPos)
	{
		input.x = input.x < ditherValue(screenPos) ? input.x*0.25 : input.x;
		input.y = input.y < ditherValue(screenPos) ? input.y*0.25 : input.y;
		input.z = input.z < ditherValue(screenPos) ? input.z*0.25 : input.z;
	}

	// This function has been basically ripped straight from https://forum.unity.com/threads/the-quest-for-efficient-per-texel-lighting.529948/
	// So big thanks to GreatestBear, and truepak182, without whom this shader wouldn't be nearly as cool
	float3 CalculateTexelWorldPos(float2 originalUV, float3 originalWorldPos, float4 texelSize)
	{
		// 1.) Calculate how much the texture UV coords need to
		//     shift to be at the center of the nearest texel.
		float2 centerUV = floor(originalUV * (texelSize.zw))/texelSize.zw + (texelSize.xy/2.0);
		float2 dUV = (centerUV - originalUV);
	
		// 2b.) Calculate how much the texture coords vary over fragment space.
		//      This essentially defines a 2x2 matrix that gets
		//      texture space (UV) deltas from fragment space (ST) deltas
		// Note: I call fragment space (S,T) to disambiguate.
		float2 dUVdS = ddx(originalUV);
		float2 dUVdT = ddy(originalUV);
	
		// 2c.) Invert the fragment from texture matrix
		float2x2 dSTdUV = float2x2(dUVdT[1], -dUVdT[0], -dUVdS[1], dUVdS[0])*(1.0f/(dUVdS[0]*dUVdT[1]-dUVdT[0]*dUVdS[1]));
	
	
		// 2d.) Convert the UV delta to a fragment space delta
		float2 dST = mul(dSTdUV , dUV);
	
		// 2e.) Calculate how much the world coords vary over fragment space.
		float3 dXYZdS = ddx(originalWorldPos);
		float3 dXYZdT = ddy(originalWorldPos);
	
		// 2f.) Finally, convert our fragment space delta to a world space delta
		// And be sure to clamp it to SOMETHING in case the derivative calc went insane
		// Here I clamp it to -1 to 1 unit in unity, which should be orders of magnitude greater
		// than the size of any texel.
		float3 dXYZ = clamp(dXYZdS * dST[0] + dXYZdT * dST[1], -1, 1);
	
		// 3.) Transform the snapped UV back to world space
		return originalWorldPos + dXYZ;
	}

	float3 CalculateTexelWorldViewDir(float3 viewDirectionWS, float3 positionWS)
	{
		//Recalculate view direction according to recalculated texelized inputData.positionWS
		float3 viewDirectionWSCorrected = GetCameraPositionWS() - positionWS;
		//This needs to be done to make corrected viewDirectionWSCorrected same length as the original. If omitted, specular will not be displayed properly
		float viewPositionMult = length(viewDirectionWS) / length(viewDirectionWSCorrected);

		return viewDirectionWSCorrected * viewPositionMult;
	}

	float2 SnapUVToTexelCenter(float2 originalUV, float4 texelSize)
	{
		float2 centerUV = floor(originalUV * (texelSize.zw))/texelSize.zw + (texelSize.xy/2.0);
		float2 dUV = (centerUV - originalUV);

		return originalUV + dUV;
	}


	float3 SampleRamp(Texture2D ramp, float coord)
	{
		return SAMPLE_TEXTURE2D_LOD(ramp, sampler_linear_clamp, saturate(float2(pow(coord,0.5), 0.5)), 0).xyz;
	}

	float invLerp(float from, float to, float value){
		return (value - from) / (to - from);
	}

	float remap(float origFrom, float origTo, float targetFrom, float targetTo, float value){
		float rel = invLerp(origFrom, origTo, value);
		return lerp(targetFrom, targetTo, rel);
	}

	float remap01(float targetFrom, float targetTo, float value){
		float rel = invLerp(0, 1, value);
		return lerp(targetFrom, targetTo, rel);

	}

	void MainLight(out float3 direction, out float3 color, out float distanceAtten)
	{
		Light mainLight = GetMainLight();
		direction = mainLight.direction;
		color = mainLight.color;
		distanceAtten = mainLight.distanceAttenuation;
	}

	float MainLightShadows(float3 worldPos, float2 lightmapUV = float2(0.0,0.0), float2 origUV = float2(0.0,0.0))
	{
		float4 shadowCoord = TransformWorldToShadowCoord(worldPos);

		#if !defined (LIGHTMAP_ON) || defined(_LIGHTINGTYPE_VERTEX)
			half4 shadowMask = unity_ProbesOcclusion;
		#else
			lightmapUV = lightmapUV * unity_LightmapST.xy + unity_LightmapST.zw;

			#ifdef _LIGHTINGTYPE_TEXEL
				lightmapUV = CalculateTexelWorldPos(origUV, float3(lightmapUV.x, lightmapUV.y, 1), _BaseMap_TexelSize).xy;
			#endif

			half4 shadowMask = SAMPLE_SHADOWMASK(lightmapUV);
		#endif

		return MainLightShadow(shadowCoord, worldPos, shadowMask, _MainLightOcclusionProbes);
	}

	float3 GetAmbient(float3 worldNormal, float2 lightmapUV, float2 origUV = float2(0.0f, 0.0f))
	{
		#ifdef LIGHTMAP_ON
			lightmapUV = lightmapUV * unity_LightmapST.xy + unity_LightmapST.zw;

			#ifdef _LIGHTINGTYPE_TEXEL
				lightmapUV = CalculateTexelWorldPos(origUV, float3(lightmapUV.x, lightmapUV.y, 1), _BaseMap_TexelSize).xy;
			#endif

			#ifdef _LIGHTINGTYPE_VERTEX
				float4 lightmap = SAMPLE_TEXTURE2D_LOD(unity_Lightmap, sampler_linear_repeat, lightmapUV, 0);
				half3 bakedGI = DecodeLightmap(lightmap,half4(LIGHTMAP_HDR_MULTIPLIER, LIGHTMAP_HDR_EXPONENT, 0.0h, 0.0h));
			#else
				half3 bakedGI = SampleLightmap(lightmapUV, worldNormal);//DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, lightmapUV),half4(LIGHTMAP_HDR_MULTIPLIER, LIGHTMAP_HDR_EXPONENT, 0.0h, 0.0h));
			#endif

			#if defined(_MIXED_LIGHTING_SUBTRACTIVE)
				Light mainLight = GetMainLight();
				bakedGI = SubtractDirectMainLightFromLightmap(mainLight, worldNormal, bakedGI);
			#endif

			return bakedGI;
		#else
			return SampleSH(worldNormal);
		#endif
	}

	float GetSpecular(float3 lightDir, float3 worldNormal, float3 viewDir, float smoothness)
	{
		float SpecSmoothness = exp2(10 * smoothness + 1).x;
		[branch]
		if (_SpecularType == 0) //Cook-Torrance
		{
			float roughness = 1 - smoothness;
			roughness *= roughness;

			float3 h = SafeNormalize(lightDir + viewDir);
			float nh = saturate(dot(worldNormal, h));
			float lh = saturate(dot(lightDir, h));
			float d = nh * nh * (roughness * roughness - 1.0) + 1.00001;
			float normTerm = roughness * 4.0 + 2.0;
			float specTerm = roughness * roughness;
			specTerm /= (d * d) * max(0.1, lh * lh) * normTerm;
			return specTerm*0.04;
		}
		else if (_SpecularType == 1) //Phong
		{
			float3 r = reflect(-lightDir, worldNormal);
			float rdotv = max(0, dot(r, viewDir));

			return pow(rdotv, SpecSmoothness)*smoothness;
		}
		else if (_SpecularType == 2) //Blinn-Phong
		{
			float3 h = SafeNormalize(lightDir+viewDir);
			float ndh = saturate(dot(worldNormal, h));

			return pow(ndh, SpecSmoothness)*smoothness;
		}
		else
		{
			return 0;
		}
	}

	float GetDiffuse(float3 lightDir, float3 worldNormal)
	{
		float diffuse = dot(lightDir, worldNormal);

		if (_HalfLambert)
			diffuse = diffuse * 0.5 + 0.5;

		return max(0,diffuse);
	}


	void AdditionalLights(float3 worldPos, float3 viewDir, float3 worldNormal, float InSmoothness, float3 specColor, out float3 Diffuse, out float3 Specular, float dirOcclusion = 1)
	{
		float3 diff = 0;
		float3 spec = 0;

		#if !defined (LIGHTMAP_ON)
			half4 shadowMask = unity_ProbesOcclusion;
		#else
			half4 shadowMask = half4(1, 1, 1, 1);
		#endif

		int pixelLights = GetAdditionalLightsCount();
		for (int i = 0; i < pixelLights; ++i)
		{
			Light light = GetAdditionalLight(i, worldPos, shadowMask);
			
			#ifdef _TOONSHADING
				float diffMask = GetDiffuse(light.direction, worldNormal) * light.shadowAttenuation * light.distanceAttenuation * dirOcclusion;
				diff += SampleRamp(_ToonRamp, diffMask) * light.color;
			#else
				diff += GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.color * light.shadowAttenuation * dirOcclusion;
			#endif

			#ifdef _TOONSHADING
				float specMask = GetSpecular(light.direction, worldNormal, viewDir, InSmoothness) * light.distanceAttenuation * light.shadowAttenuation * dirOcclusion;
				spec += SampleRamp(_SpecularRamp, specMask) * light.color * specColor;
			#else
				spec += GetSpecular(light.direction, worldNormal, viewDir, InSmoothness) * light.distanceAttenuation * light.color * specColor * light.shadowAttenuation * dirOcclusion;
			#endif
		}
		Diffuse = diff;
		Specular = spec;
	}


	float3 GetReflections(float3 worldNormal, float3 viewDir, float smoothness)
	{
		float perceptualRoughness = 1 - smoothness;
		perceptualRoughness *= perceptualRoughness;
		float mip = PerceptualRoughnessToMipmapLevel(perceptualRoughness);
		float3 sample = SHADERGRAPH_REFLECTION_PROBE(viewDir, worldNormal, mip);
		return sample.rgb;
	}

	void SSAO(ShaderData d, out float directOcclusion, out float indirectOcclusion)
	{
		#if defined(_SCREEN_SPACE_OCCLUSION) && !defined(RETROSHADER_ALPHA)
			float2 uv = d.screenUV;

			#ifdef _LIGHTINGTYPE_TEXEL
				float3 pos = CalculateTexelWorldPos(d.texcoord0, float3(uv.x, uv.y, 1.0), _BaseMap_TexelSize);
				uv = pos.xy;
			#endif

			AmbientOcclusionFactor aoFactor = GetScreenSpaceAmbientOcclusion(uv);
			directOcclusion = aoFactor.directAmbientOcclusion;
			indirectOcclusion = aoFactor.indirectAmbientOcclusion;
		#else
			directOcclusion = 1;
			indirectOcclusion = 1;
		#endif
	}

	// Courtesy of Ben Golus
	// https://forum.unity.com/threads/shader-ideas-for-replicating-n64-bi-linear-filtering.936119/#post-6116207
	half4 sampleN64(Texture2D tex, float2 uv, float4 texelSize)
	{
		// texel coordinates
		float2 texels = uv * texelSize.zw;

		// calculate mip level
		float2 dx = ddx(texels);
		float2 dy = ddy(texels);
		float delta_max_sqr = max(dot(dx, dx), dot(dy, dy));
		float mip = max(0.0, 0.5 * log2(delta_max_sqr));

		// scale texel sizes and texel coordinates to handle mip levels properly
		float scale = pow(2,floor(mip));
		texelSize.xy *= scale;
		texelSize.zw /= scale;
		texels = texels / scale - 0.5;

		// calculate blend for the three points of the tri-filter
		float2 fracTexels = frac(texels);
		float3 blend = float3(
			abs(fracTexels.x+fracTexels.y-1),
			min(abs(fracTexels.xx-float2(0,1)), abs(fracTexels.yy-float2(1,0)))
		);

		// calculate equivalents of point filtered uvs for the three points
		float2 uvA = (floor(texels + fracTexels.yx) + 0.5) * texelSize.xy;
		float2 uvB = (floor(texels) + float2(1.5, 0.5)) * texelSize.xy;
		float2 uvC = (floor(texels) + float2(0.5, 1.5)) * texelSize.xy;

		// sample points
		half4 A = SAMPLE_TEXTURE2D_LOD(tex, MAINSMP, uvA, mip);
		half4 B = SAMPLE_TEXTURE2D_LOD(tex, MAINSMP, uvB, mip);
		half4 C = SAMPLE_TEXTURE2D_LOD(tex, MAINSMP, uvC, mip);

		// blend and return
		return A * blend.x + B * blend.y + C * blend.z;
	}

	half4 sampleTex(Texture2D tex, float2 uv, float4 texelSize)
	{
		#ifdef _TEXTUREFILTERING_N64
			return sampleN64(tex, uv, texelSize);
		#else
			return SAMPLE_TEXTURE2D(tex, MAINSMP, uv);
		#endif
	}

	TEXTURE2D(_MaskMap);
	TEXTURE2D(_BumpMap);
	TEXTURE2D(_EmissionMap);

	void Ext_ModifyVertex0 (inout VertexData v, inout ExtraV2F d)
	{
		float4 clipPos = mul(GetWorldToHClipMatrix(), mul(unity_ObjectToWorld, v.vertex));

		

		#if defined(_LIGHTINGTYPE_VERTEX) && !defined(PASS_META)

			float smoothness = _VertexLightSmoothness;

			#ifdef _VCOLSMOOTHNESS
				smoothness = remap01(_SmoothRemapVCol.x, _SmoothRemapVCol.y, v.vertexColor.a);
			#endif

			float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
			float3 worldNormal = SafeNormalize(mul(unity_ObjectToWorld, v.normal));
			float3 viewDir = normalize(_WorldSpaceCameraPos - worldPos);
			float3 mainLightDir;
			float3 mainLightCol;
			float mainLightAtten;
			MainLight(mainLightDir, mainLightCol, mainLightAtten);

			d.extraV2F0.y = GetDiffuse(mainLightDir, worldNormal) * mainLightAtten;
			d.extraV2F1.xyz = GetAmbient(worldNormal, v.texcoord1);
			d.extraV2F0.z = MainLightShadows(worldPos, v.texcoord1);
			d.extraV2F0.w = GetSpecular(mainLightDir, worldNormal, viewDir, smoothness) * mainLightAtten * d.extraV2F0.z;

			#if !defined (LIGHTMAP_ON)
				half4 shadowMask = unity_ProbesOcclusion;
			#else
				half4 shadowMask = half4(1, 1, 1, 1);
			#endif

			// Have to store each light in a separate vector component so we can apply toon shading in fragment later - toon shading has to be applied per light to make sense
			// This limits us to max 4 lights, would need to use 2 more interpolators for 8
			int pixelLights = GetAdditionalLightsCount();
			Light light;
			if (pixelLights >= 1)
			{
				light = GetAdditionalLight(0, worldPos, shadowMask);
				d.extraV2F3.x = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.x = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
			if (pixelLights >= 2)
			{
				light = GetAdditionalLight(1, worldPos, shadowMask);
				d.extraV2F3.y = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.y = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
			if (pixelLights >= 3)
			{
				light = GetAdditionalLight(2, worldPos, shadowMask);
				d.extraV2F3.z = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.z = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
			if (pixelLights >= 4)
			{
				light = GetAdditionalLight(3, worldPos, shadowMask);
				d.extraV2F3.w = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.w = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
		#endif

		#if defined(_VERTEXJITTER) && !defined(_PASSSHADOW)
			float4 vert = clipPos;
			d.extraV2F2.xyz = mul(UNITY_MATRIX_I_VP, vert);
			vert.xyz = clipPos.xyz/clipPos.w;
			vert.x = floor(_VertexJitterResX * vert.x) / _VertexJitterResX;
			vert.y = floor(_VertexJitterResY * vert.y) / _VertexJitterResY;
			vert.xyz *= clipPos.w;
			v.vertex = mul(unity_WorldToObject, mul(UNITY_MATRIX_I_VP, vert));
		#endif

		d.extraV2F0.x = ComputeFogFactor(clipPos.z);
		v.texcoord0.xy = v.texcoord0.xy * _TilingOffset.xy + _TilingOffset.zw;

		#if defined(_AFFINE_MAPPING)
			_AffineMapFactor = min(1, _AffineMapFactor);
			float warpFactor = max(clipPos.w, 0.02); //Prevents flipping
			warpFactor = lerp(0.5, warpFactor, _AffineMapFactor);

			v.texcoord0.xy *= warpFactor;
			d.extraV2F1.w = warpFactor;
		#endif
	}

	
	void Ext_SurfaceFunction0 (inout Surface o, ShaderData d)
	{
		#if defined(_AFFINE_MAPPING)
			d.texcoord0.xy /= d.extraV2F1.w;
		#endif
		// Set up input data
		float4 mainTex = sampleTex(_BaseMap, d.texcoord0.xy, _BaseMap_TexelSize);
		o.Albedo = mainTex.rgb;
		float4 maskMap = sampleTex(_MaskMap, d.texcoord0.xy, _BaseMap_TexelSize);

		

		if (_MaskTint)
		{
			float3 tintedAlbedo = o.Albedo * _Color.rgb;
			o.Albedo = lerp(o.Albedo, tintedAlbedo, maskMap.a);
		}
		else
		{
			o.Albedo *= _Color.rgb;
		}

		#ifdef _EMISSION
			float3 emission = sampleTex(_EmissionMap, d.texcoord0.xy, _EmissionMap_TexelSize) * _EmissionColor;
			o.Emission = emission * _LightmapEmissionStr;

		#endif

		float2 pixelPos = d.clipPos.xy;//floor(d.screenUV.xy * _ScreenParams.xy);

		#if defined(_LIGHTINGTYPE_TEXEL) && !defined(PASS_META)
			o.Normal = UnpackScaleNormal(SAMPLE_TEXTURE2D(_BumpMap, sampler_linear_repeat, d.texcoord0.xy), _NormalStrength);
		#else
			o.Normal = UnpackScaleNormal(sampleTex(_BumpMap, d.texcoord0.xy, _BumpMap_TexelSize), _NormalStrength);
		#endif

		//o.Normal = SafeNormalize(o.Normal);

		#if defined(_VERTEXJITTER) && !defined(_PASSSHADOW)
			d.worldSpacePosition = d.extraV2F2.xyz;
		#endif

		d.worldSpaceNormal = TransformTangentToWorld(o.Normal, d.TBNMatrix);
		float3 unSnappedViewDir = d.worldSpaceViewDir;
		// Snap world position, view direction, and normal to the texel
		#if defined(_LIGHTINGTYPE_TEXEL) && !defined(PASS_META)
			d.worldSpacePosition = CalculateTexelWorldPos(d.texcoord0.xy, d.worldSpacePosition, _BaseMap_TexelSize);
			d.worldSpaceViewDir = CalculateTexelWorldViewDir(d.worldSpaceViewDir, d.worldSpacePosition);
			d.worldSpaceNormal = SafeNormalize(CalculateTexelWorldPos(d.texcoord0.xy, d.worldSpaceNormal, _BaseMap_TexelSize));
		#endif

		if (!_EnableMaskMap)
		{
			maskMap.r = 0;
			maskMap.g = mainTex.a;
			maskMap.b = 1;
		}

		o.Metallic = maskMap.r;
		o.Smoothness = remap01(_SmoothRemap.x, _SmoothRemap.y, maskMap.g);
		o.Occlusion =  maskMap.b;

		// Store lighting values in these
		float3 mainLightDir;
		float3 mainLightCol;
		float mainLightAtten;
		MainLight(mainLightDir, mainLightCol, mainLightAtten);

		float3 mainLightDiffuse;
		float mainShadow = 1;
		float3 ambient;
		float3 mainSpec = 0;

		float3 addDiffuse = 0;
		float3 addSpec = 0;

		float3 specColor = lerp(1.0, o.Albedo, o.Metallic);
		float reflectStr = lerp(0.04, 1, o.Metallic);
		
		if (_Reflections)
			o.Albedo *= 1 - o.Metallic;

		float3 reflection = 0;
		
		if (_Reflections)
			reflection = GetReflections(d.worldSpaceNormal, d.worldSpaceViewDir, o.Smoothness);

		#if !defined (LIGHTMAP_ON)
			half4 shadowMask = unity_ProbesOcclusion;
		#else
			half4 shadowMask = SAMPLE_SHADOWMASK(d.texcoord1.xy);
		#endif

		//Get lighting data, either from vertex shader or calculate it here
		#if defined(_LIGHTINGTYPE_VERTEX) && !defined(PASS_META)

			float vsmoothness = _VertexLightSmoothness;

			#ifdef _VCOLSMOOTHNESS
				vsmoothness = remap01(_SmoothRemapVCol.x, _SmoothRemapVCol.y, v.vertexColor.a);
			#endif

			o.Smoothness = vsmoothness;

			mainLightDiffuse = min(d.extraV2F0.y, d.extraV2F0.z); // Min(mainLight, shadow)

			#ifdef _TOONSHADING
				mainLightDiffuse = SampleRamp(_ToonRamp, mainLightDiffuse);
			#endif

			mainLightDiffuse *= mainLightCol;
			ambient = d.extraV2F1.xyz;

			#ifndef _TOONSHADING
				mainSpec = d.extraV2F0.w * mainLightCol;
			#else
				mainSpec = SampleRamp(_SpecularRamp, d.extraV2F0.w) * mainLightCol;
			#endif

			float3 worldPos = d.worldSpacePosition;

			int pixelLights = GetAdditionalLightsCount();
			Light light;
			if (pixelLights >= 1)
			{
				light = GetAdditionalLight(0, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.x * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.x) * light.color;
				#endif
				
				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.x * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.x) * light.color * specColor;
				#endif
			}
			if (pixelLights >= 2)
			{
				light = GetAdditionalLight(1, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.y * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.y) * light.color;
				#endif

				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.y * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.y) * light.color * specColor;
				#endif
			}
			if (pixelLights >= 3)
			{
				light = GetAdditionalLight(2, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.z * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.z) * light.color;
				#endif

				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.z * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.z) * light.color * specColor;
				#endif
			}
			if (pixelLights >= 4)
			{
				light = GetAdditionalLight(3, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.w * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.w) * light.color;
				#endif

				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.w * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.w) * light.color * specColor;
				#endif
			}

			mainSpec *= _VertexSpecColor;
			addSpec *= _VertexSpecColor;
			//addDiffuse = d.extraV2F3.xyz;
			//addSpec = d.extraV2F4.xyz;
		#else

			float dirOcclusion, indirOcclusion;
			SSAO(d, dirOcclusion, indirOcclusion);

			mainShadow = MainLightShadows(d.worldSpacePosition, d.texcoord1.xy, d.texcoord0.xy);
			
			#ifndef _TOONSHADING
				mainLightDiffuse = GetDiffuse(d.worldSpaceNormal, mainLightDir) * mainLightAtten * mainLightCol * mainShadow * dirOcclusion;
			#else
				mainLightDiffuse = GetDiffuse(d.worldSpaceNormal, mainLightDir) * mainShadow * dirOcclusion;
				float3 ramp = SampleRamp(_ToonRamp, mainLightDiffuse.x);
				mainLightDiffuse = ramp * mainLightAtten * mainLightCol;
			#endif

			ambient = GetAmbient(d.worldSpaceNormal, d.texcoord1.xy, d.texcoord0.xy) * min(o.Occlusion,indirOcclusion);
			mainSpec = GetSpecular(mainLightDir, d.worldSpaceNormal, d.worldSpaceViewDir, o.Smoothness)*dirOcclusion;

			#ifdef _TOONSHADING
				mainSpec = SampleRamp(_SpecularRamp, mainSpec.x);
			#endif

			mainSpec *= mainLightCol * mainLightAtten * specColor;
			mainSpec *= mainShadow;

			AdditionalLights(d.worldSpacePosition, d.worldSpaceViewDir, d.worldSpaceNormal, o.Smoothness, specColor, addDiffuse, addSpec);

		#endif

		float perceptualRoughness = 1 - o.Smoothness;
		perceptualRoughness *= perceptualRoughness;

		float fresnel = pow(1.0 - saturate(dot(d.worldSpaceNormal, d.worldSpaceViewDir)), 4);
		reflectStr *= lerp(0.1, 1, GetLuminance(mainLightDiffuse));
		reflectStr = lerp(reflectStr, saturate(reflectStr*10), fresnel);

		reflection *= reflectStr * specColor;
		reflection /= perceptualRoughness + 0.5;

		if (_DoubleSizeDither) pixelPos /= 2.0;

		[branch]
		if (_DitherDiffuse)
		{
			ditherColor(mainLightDiffuse, pixelPos);
			ditherColor(addDiffuse, pixelPos);
		}

		[branch]
		if (_DitherSpec)
		{
			ditherColor(mainSpec, pixelPos);
			ditherColor(addSpec, pixelPos);
		}

		[branch]
		if (_DitherAmbient)
			ditherColor(ambient, pixelPos);

		o.Alpha = mainTex.a * _Color.a;

		//o.Albedo *= 1 - saturate(GetLuminance(mainSpec));
		//o.Albedo *= 1 - saturate(GetLuminance(addSpec));

		#if defined(RETROSHADER_ALPHA) && !defined(PASS_META)
			if (_PreserveSpecular)
			{
				float specStrength = saturate(GetLuminance(mainSpec));
				float reflectStrength = saturate(GetLuminance(reflection));
				o.Alpha = max(o.Alpha, max(specStrength, reflectStrength));
			}
		#endif

		#ifdef _ALPHATEST_ON
			clip(step(_Cutoff, o.Alpha)-0.1f);
		#endif

		if (_VColEmissive)
		{
			o.Albedo += d.vertexColor.rgb * max(0,_VertexEmissiveStr);
			o.Emission = d.vertexColor.rgb * max(0,_VertexEmissiveStr);
		}
		else
		{
			o.Albedo *= d.vertexColor.rgb;
		}

		float3 combinedAmbient = ambient * o.Albedo;

		// Apply lighting, but not for lightbaking pass
		#if !defined(PASS_META)
			o.Albedo *= mainLightDiffuse + addDiffuse;
			o.Albedo += combinedAmbient;
			o.Albedo += mainSpec * saturate(GetLuminance(mainLightDiffuse));
			o.Albedo += addSpec * saturate(GetLuminance(addDiffuse));
			o.Albedo += reflection;

			#if defined(_EMISSION)
				o.Albedo += emission;
			#endif
		#endif
		
		#if defined(RETROSHADER_ALPHA) && !defined(PASS_META)
			if (_Refraction)
			{
				float origAlpha = o.Alpha;
				float4 offsetPos = float4(d.worldSpacePosition + d.worldSpaceNormal * _RefractionStr, 1.0);
				offsetPos = mul(GetWorldToHClipMatrix(), offsetPos);
				float scale = 1.0;

				#if UNITY_UV_STARTS_AT_TOP
					scale = -1.0;
				#endif

				float4 grabPos = offsetPos *0.5f;
				grabPos.xy = float2(grabPos.x, grabPos.y*scale) + grabPos.w;
				grabPos.zw = offsetPos.zw;
				grabPos.xy /= grabPos.w;
				grabPos.xy = clamp(grabPos.xy, float2(0.0, 0.0), float2(1.0, 1.0));

				float3 refraction = GetSceneColor(grabPos.xy);
				float2 cubemapBlendFactors = pow(abs((grabPos.xy - 0.5) * 2.0),0.25);
				float blend = smoothstep(0.75f, 1, max(cubemapBlendFactors.x, cubemapBlendFactors.y));
				float3 refractDir = refract(-d.worldSpaceViewDir, d.worldSpaceNormal, 1 - _RefractionStr);
				float3 reflectProbe = GetReflections(-refractDir, refractDir, 1.0);
				refraction = lerp(refraction, reflectProbe, blend);

				o.Albedo = lerp(refraction, o.Albedo, origAlpha);
				o.Alpha = 1;
			}
		#endif

		o.Albedo = MixFog(o.Albedo, d.extraV2F0.x);
	}




        
            void ChainSurfaceFunction(inout Surface l, inout ShaderData d)
            {
                  Ext_SurfaceFunction0(l, d);
                 // Ext_SurfaceFunction1(l, d);
                 // Ext_SurfaceFunction2(l, d);
                 // Ext_SurfaceFunction3(l, d);
                 // Ext_SurfaceFunction4(l, d);
                 // Ext_SurfaceFunction5(l, d);
                 // Ext_SurfaceFunction6(l, d);
                 // Ext_SurfaceFunction7(l, d);
                 // Ext_SurfaceFunction8(l, d);
                 // Ext_SurfaceFunction9(l, d);
		           // Ext_SurfaceFunction10(l, d);
                 // Ext_SurfaceFunction11(l, d);
                 // Ext_SurfaceFunction12(l, d);
                 // Ext_SurfaceFunction13(l, d);
                 // Ext_SurfaceFunction14(l, d);
                 // Ext_SurfaceFunction15(l, d);
                 // Ext_SurfaceFunction16(l, d);
                 // Ext_SurfaceFunction17(l, d);
                 // Ext_SurfaceFunction18(l, d);
		           // Ext_SurfaceFunction19(l, d);
                 // Ext_SurfaceFunction20(l, d);
                 // Ext_SurfaceFunction21(l, d);
                 // Ext_SurfaceFunction22(l, d);
                 // Ext_SurfaceFunction23(l, d);
                 // Ext_SurfaceFunction24(l, d);
                 // Ext_SurfaceFunction25(l, d);
                 // Ext_SurfaceFunction26(l, d);
                 // Ext_SurfaceFunction27(l, d);
                 // Ext_SurfaceFunction28(l, d);
		           // Ext_SurfaceFunction29(l, d);
            }

            void ChainModifyVertex(inout VertexData v, inout VertexToPixel v2p, float4 time)
            {
                 ExtraV2F d;
                 
                 ZERO_INITIALIZE(ExtraV2F, d);
                 ZERO_INITIALIZE(Blackboard, d.blackboard);
                 // due to motion vectors in HDRP, we need to use the last
                 // time in certain spots. So if you are going to use _Time to adjust vertices,
                 // you need to use this time or motion vectors will break. 
                 d.time = time;

                   Ext_ModifyVertex0(v, d);
                 // Ext_ModifyVertex1(v, d);
                 // Ext_ModifyVertex2(v, d);
                 // Ext_ModifyVertex3(v, d);
                 // Ext_ModifyVertex4(v, d);
                 // Ext_ModifyVertex5(v, d);
                 // Ext_ModifyVertex6(v, d);
                 // Ext_ModifyVertex7(v, d);
                 // Ext_ModifyVertex8(v, d);
                 // Ext_ModifyVertex9(v, d);
                 // Ext_ModifyVertex10(v, d);
                 // Ext_ModifyVertex11(v, d);
                 // Ext_ModifyVertex12(v, d);
                 // Ext_ModifyVertex13(v, d);
                 // Ext_ModifyVertex14(v, d);
                 // Ext_ModifyVertex15(v, d);
                 // Ext_ModifyVertex16(v, d);
                 // Ext_ModifyVertex17(v, d);
                 // Ext_ModifyVertex18(v, d);
                 // Ext_ModifyVertex19(v, d);
                 // Ext_ModifyVertex20(v, d);
                 // Ext_ModifyVertex21(v, d);
                 // Ext_ModifyVertex22(v, d);
                 // Ext_ModifyVertex23(v, d);
                 // Ext_ModifyVertex24(v, d);
                 // Ext_ModifyVertex25(v, d);
                 // Ext_ModifyVertex26(v, d);
                 // Ext_ModifyVertex27(v, d);
                 // Ext_ModifyVertex28(v, d);
                 // Ext_ModifyVertex29(v, d);


                 // #if %EXTRAV2F0REQUIREKEY%
                  v2p.extraV2F0 = d.extraV2F0;
                 // #endif

                 // #if %EXTRAV2F1REQUIREKEY%
                  v2p.extraV2F1 = d.extraV2F1;
                 // #endif

                 // #if %EXTRAV2F2REQUIREKEY%
                  v2p.extraV2F2 = d.extraV2F2;
                 // #endif

                 // #if %EXTRAV2F3REQUIREKEY%
                  v2p.extraV2F3 = d.extraV2F3;
                 // #endif

                 // #if %EXTRAV2F4REQUIREKEY%
                  v2p.extraV2F4 = d.extraV2F4;
                 // #endif

                 // #if %EXTRAV2F5REQUIREKEY%
                 // v2p.extraV2F5 = d.extraV2F5;
                 // #endif

                 // #if %EXTRAV2F6REQUIREKEY%
                 // v2p.extraV2F6 = d.extraV2F6;
                 // #endif

                 // #if %EXTRAV2F7REQUIREKEY%
                 // v2p.extraV2F7 = d.extraV2F7;
                 // #endif
            }

            void ChainModifyTessellatedVertex(inout VertexData v, inout VertexToPixel v2p)
            {
               ExtraV2F d;
               ZERO_INITIALIZE(ExtraV2F, d);
               ZERO_INITIALIZE(Blackboard, d.blackboard);

               // #if %EXTRAV2F0REQUIREKEY%
                d.extraV2F0 = v2p.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
                d.extraV2F1 = v2p.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
                d.extraV2F2 = v2p.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
                d.extraV2F3 = v2p.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
                d.extraV2F4 = v2p.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // d.extraV2F5 = v2p.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // d.extraV2F6 = v2p.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // d.extraV2F7 = v2p.extraV2F7;
               // #endif


               // Ext_ModifyTessellatedVertex0(v, d);
               // Ext_ModifyTessellatedVertex1(v, d);
               // Ext_ModifyTessellatedVertex2(v, d);
               // Ext_ModifyTessellatedVertex3(v, d);
               // Ext_ModifyTessellatedVertex4(v, d);
               // Ext_ModifyTessellatedVertex5(v, d);
               // Ext_ModifyTessellatedVertex6(v, d);
               // Ext_ModifyTessellatedVertex7(v, d);
               // Ext_ModifyTessellatedVertex8(v, d);
               // Ext_ModifyTessellatedVertex9(v, d);
               // Ext_ModifyTessellatedVertex10(v, d);
               // Ext_ModifyTessellatedVertex11(v, d);
               // Ext_ModifyTessellatedVertex12(v, d);
               // Ext_ModifyTessellatedVertex13(v, d);
               // Ext_ModifyTessellatedVertex14(v, d);
               // Ext_ModifyTessellatedVertex15(v, d);
               // Ext_ModifyTessellatedVertex16(v, d);
               // Ext_ModifyTessellatedVertex17(v, d);
               // Ext_ModifyTessellatedVertex18(v, d);
               // Ext_ModifyTessellatedVertex19(v, d);
               // Ext_ModifyTessellatedVertex20(v, d);
               // Ext_ModifyTessellatedVertex21(v, d);
               // Ext_ModifyTessellatedVertex22(v, d);
               // Ext_ModifyTessellatedVertex23(v, d);
               // Ext_ModifyTessellatedVertex24(v, d);
               // Ext_ModifyTessellatedVertex25(v, d);
               // Ext_ModifyTessellatedVertex26(v, d);
               // Ext_ModifyTessellatedVertex27(v, d);
               // Ext_ModifyTessellatedVertex28(v, d);
               // Ext_ModifyTessellatedVertex29(v, d);

               // #if %EXTRAV2F0REQUIREKEY%
                v2p.extraV2F0 = d.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
                v2p.extraV2F1 = d.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
                v2p.extraV2F2 = d.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
                v2p.extraV2F3 = d.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
                v2p.extraV2F4 = d.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // v2p.extraV2F5 = d.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // v2p.extraV2F6 = d.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // v2p.extraV2F7 = d.extraV2F7;
               // #endif
            }

            void ChainFinalColorForward(inout Surface l, inout ShaderData d, inout half4 color)
            {
               //   Ext_FinalColorForward0(l, d, color);
               //   Ext_FinalColorForward1(l, d, color);
               //   Ext_FinalColorForward2(l, d, color);
               //   Ext_FinalColorForward3(l, d, color);
               //   Ext_FinalColorForward4(l, d, color);
               //   Ext_FinalColorForward5(l, d, color);
               //   Ext_FinalColorForward6(l, d, color);
               //   Ext_FinalColorForward7(l, d, color);
               //   Ext_FinalColorForward8(l, d, color);
               //   Ext_FinalColorForward9(l, d, color);
               //  Ext_FinalColorForward10(l, d, color);
               //  Ext_FinalColorForward11(l, d, color);
               //  Ext_FinalColorForward12(l, d, color);
               //  Ext_FinalColorForward13(l, d, color);
               //  Ext_FinalColorForward14(l, d, color);
               //  Ext_FinalColorForward15(l, d, color);
               //  Ext_FinalColorForward16(l, d, color);
               //  Ext_FinalColorForward17(l, d, color);
               //  Ext_FinalColorForward18(l, d, color);
               //  Ext_FinalColorForward19(l, d, color);
               //  Ext_FinalColorForward20(l, d, color);
               //  Ext_FinalColorForward21(l, d, color);
               //  Ext_FinalColorForward22(l, d, color);
               //  Ext_FinalColorForward23(l, d, color);
               //  Ext_FinalColorForward24(l, d, color);
               //  Ext_FinalColorForward25(l, d, color);
               //  Ext_FinalColorForward26(l, d, color);
               //  Ext_FinalColorForward27(l, d, color);
               //  Ext_FinalColorForward28(l, d, color);
               //  Ext_FinalColorForward29(l, d, color);
            }

            void ChainFinalGBufferStandard(inout Surface s, inout ShaderData d, inout half4 GBuffer0, inout half4 GBuffer1, inout half4 GBuffer2, inout half4 outEmission, inout half4 outShadowMask)
            {
               //   Ext_FinalGBufferStandard0(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard1(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard2(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard3(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard4(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard5(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard6(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard7(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard8(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard9(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard10(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard11(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard12(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard13(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard14(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard15(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard16(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard17(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard18(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard19(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard20(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard21(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard22(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard23(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard24(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard25(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard26(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard27(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard28(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard29(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
            }



         

         ShaderData CreateShaderData(VertexToPixel i
                  #if NEED_FACING
                     , bool facing
                  #endif
         )
         {
            ShaderData d = (ShaderData)0;
            d.clipPos = i.pos;
            d.worldSpacePosition = i.worldPos;

            d.worldSpaceNormal = normalize(i.worldNormal);
            d.worldSpaceTangent = normalize(i.worldTangent.xyz);
            d.tangentSign = i.worldTangent.w;
            float3 bitangent = cross(i.worldTangent.xyz, i.worldNormal) * d.tangentSign * -1;
            

            d.TBNMatrix = float3x3(d.worldSpaceTangent, bitangent, d.worldSpaceNormal);
            d.worldSpaceViewDir = normalize(_WorldSpaceCameraPos - i.worldPos);

            d.tangentSpaceViewDir = mul(d.TBNMatrix, d.worldSpaceViewDir);
             d.texcoord0 = i.texcoord0;
             d.texcoord1 = i.texcoord1;
            // d.texcoord2 = i.texcoord2;

            // #if %TEXCOORD3REQUIREKEY%
            // d.texcoord3 = i.texcoord3;
            // #endif

            // d.isFrontFace = facing;
            // #if %VERTEXCOLORREQUIREKEY%
             d.vertexColor = i.vertexColor;
            // #endif

            // these rarely get used, so we back transform them. Usually will be stripped.
            #if _HDRP
                // d.localSpacePosition = mul(unity_WorldToObject, float4(GetCameraRelativePositionWS(i.worldPos), 1)).xyz;
            #else
                // d.localSpacePosition = mul(unity_WorldToObject, float4(i.worldPos, 1)).xyz;
            #endif
            // d.localSpaceNormal = normalize(mul((float3x3)unity_WorldToObject, i.worldNormal));
            // d.localSpaceTangent = normalize(mul((float3x3)unity_WorldToObject, i.worldTangent.xyz));

            // #if %SCREENPOSREQUIREKEY%
             d.screenPos = i.screenPos;
             d.screenUV = (i.screenPos.xy / i.screenPos.w);
            // #endif


            // #if %EXTRAV2F0REQUIREKEY%
             d.extraV2F0 = i.extraV2F0;
            // #endif

            // #if %EXTRAV2F1REQUIREKEY%
             d.extraV2F1 = i.extraV2F1;
            // #endif

            // #if %EXTRAV2F2REQUIREKEY%
             d.extraV2F2 = i.extraV2F2;
            // #endif

            // #if %EXTRAV2F3REQUIREKEY%
             d.extraV2F3 = i.extraV2F3;
            // #endif

            // #if %EXTRAV2F4REQUIREKEY%
             d.extraV2F4 = i.extraV2F4;
            // #endif

            // #if %EXTRAV2F5REQUIREKEY%
            // d.extraV2F5 = i.extraV2F5;
            // #endif

            // #if %EXTRAV2F6REQUIREKEY%
            // d.extraV2F6 = i.extraV2F6;
            // #endif

            // #if %EXTRAV2F7REQUIREKEY%
            // d.extraV2F7 = i.extraV2F7;
            // #endif

            return d;
         }
         

         
         #if defined(_PASSSHADOW)
            float3 _LightDirection;
            float3 _LightPosition;
         #endif

         // vertex shader
         VertexToPixel Vert (VertexData v)
         {
           
           VertexToPixel o = (VertexToPixel)0;

           UNITY_SETUP_INSTANCE_ID(v);
           UNITY_TRANSFER_INSTANCE_ID(v, o);
           UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);


#if !_TESSELLATION_ON
           ChainModifyVertex(v, o, _Time);
#endif

            o.texcoord0 = v.texcoord0;
            o.texcoord1 = v.texcoord1;
           // o.texcoord2 = v.texcoord2;

           // #if %TEXCOORD3REQUIREKEY%
           // o.texcoord3 = v.texcoord3;
           // #endif

           // #if %VERTEXCOLORREQUIREKEY%
            o.vertexColor = v.vertexColor;
           // #endif
           
           VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
           o.worldPos = TransformObjectToWorld(v.vertex.xyz);
           o.worldNormal = TransformObjectToWorldNormal(v.normal);
           o.worldTangent = float4(TransformObjectToWorldDir(v.tangent.xyz), v.tangent.w);

          // For some very odd reason, in 2021.2, we can't use Unity's defines, but have to use our own..
          #if _PASSSHADOW
              #if _CASTING_PUNCTUAL_LIGHT_SHADOW
                 float3 lightDirectionWS = normalize(_LightPosition - o.worldPos);
              #else
                 float3 lightDirectionWS = _LightDirection;
              #endif
              // Define shadow pass specific clip position for Universal
              o.pos = TransformWorldToHClip(ApplyShadowBias(o.worldPos, o.worldNormal, lightDirectionWS));
              #if UNITY_REVERSED_Z
                  o.pos.z = min(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #else
                  o.pos.z = max(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #endif
          #elif _PASSMETA
              o.pos = MetaVertexPosition(float4(v.vertex.xyz, 0), v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST);
          #else
              o.pos = TransformWorldToHClip(o.worldPos);
          #endif

          // #if %SCREENPOSREQUIREKEY%
           o.screenPos = ComputeScreenPos(o.pos, _ProjectionParams.x);
          // #endif

          #if _PASSFORWARD || _PASSGBUFFER
              float2 uv1 = v.texcoord1.xy;
              OUTPUT_LIGHTMAP_UV(uv1, unity_LightmapST, o.lightmapUV);
               o.texcoord1.xy = uv1;
              OUTPUT_SH(o.worldNormal, o.sh);
              #if defined(DYNAMICLIGHTMAP_ON)
                   o.dynamicLightmapUV.xy = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
              #endif
          #endif

          #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
              half fogFactor = 0;
              #if defined(_FOG_FRAGMENT)
                fogFactor = ComputeFogFactor(o.pos.z);
              #endif
              #if _BAKEDLIT
                 o.fogFactorAndVertexLight = half4(fogFactor, 0, 0, 0);
              #else
                 half3 vertexLight = VertexLighting(o.worldPos, o.worldNormal);
                 o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);
              #endif
          #endif

          #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
             o.shadowCoord = GetShadowCoord(vertexInput);
          #endif

           return o;
         }


         

#if _UNLIT
   #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Unlit.hlsl"  
#endif

         // fragment shader
         half4 Frag (VertexToPixel IN
            #ifdef _DEPTHOFFSET_ON
              , out float outputDepth : SV_Depth
            #endif
            #if NEED_FACING
               , bool facing : SV_IsFrontFace
            #endif
         ) : SV_Target
         {
           UNITY_SETUP_INSTANCE_ID(IN);
           UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

           ShaderData d = CreateShaderData(IN
                  #if NEED_FACING
                     , facing
                  #endif
               );
           Surface l = (Surface)0;

           #ifdef _DEPTHOFFSET_ON
              l.outputDepth = outputDepth;
           #endif

           l.Albedo = half3(0.5, 0.5, 0.5);
           l.Normal = float3(0,0,1);
           l.Occlusion = 1;
           l.Alpha = 1;

           ChainSurfaceFunction(l, d);

           #ifdef _DEPTHOFFSET_ON
              outputDepth = l.outputDepth;
           #endif

           #if _USESPECULAR || _SIMPLELIT
              float3 specular = l.Specular;
              float metallic = 1;
           #else   
              float3 specular = 0;
              float metallic = l.Metallic;
           #endif


            
           
            InputData inputData = (InputData)0;

            inputData.positionWS = IN.worldPos;
            #if _WORLDSPACENORMAL
              inputData.normalWS = l.Normal;
            #else
              inputData.normalWS = normalize(TangentToWorldSpace(d, l.Normal));
            #endif

            inputData.viewDirectionWS = SafeNormalize(d.worldSpaceViewDir);


            #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
                  inputData.shadowCoord = IN.shadowCoord;
            #elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
                  inputData.shadowCoord = TransformWorldToShadowCoord(IN.worldPos);
            #else
                  inputData.shadowCoord = float4(0, 0, 0, 0);
            #endif
            
#if _BAKEDLIT
            inputData.fogCoord = IN.fogFactorAndVertexLight.x;
            inputData.vertexLighting = 0;
#else
            inputData.fogCoord = InitializeInputDataFog(float4(IN.worldPos, 1.0), IN.fogFactorAndVertexLight.x);
            inputData.vertexLighting = IN.fogFactorAndVertexLight.yzw;
#endif    



            #if defined(_OVERRIDE_BAKEDGI)
               inputData.bakedGI = l.DiffuseGI;
               l.Emission += l.SpecularGI;
            #elif _BAKEDLIT
               inputData.bakedGI = SAMPLE_GI(IN.lightmapUV, IN.sh, inputData.normalWS);
            #else
               #if defined(DYNAMICLIGHTMAP_ON)
                  inputData.bakedGI = SAMPLE_GI(IN.lightmapUV, IN.dynamicLightmapUV.xy, IN.sh, inputData.normalWS);
               #else
                  inputData.bakedGI = SAMPLE_GI(IN.lightmapUV, IN.sh, inputData.normalWS);
               #endif
            #endif
            inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(IN.pos);
            #if !_BAKEDLIT
               inputData.shadowMask = SAMPLE_SHADOWMASK(IN.lightmapUV);
           
               #if defined(_OVERRIDE_SHADOWMASK)
                  float4 mulColor = saturate(dot(l.ShadowMask, _MainLightOcclusionProbes)); //unity_OcclusionMaskSelector));
                  inputData.shadowMask = mulColor;
               #endif
            #else
               inputData.shadowMask = float4(1,1,1,1);
            #endif

            #if defined(DEBUG_DISPLAY)
                #if defined(DYNAMICLIGHTMAP_ON)
                  inputData.dynamicLightmapUV = IN.dynamicLightmapUV.xy;
                #endif
                #if defined(LIGHTMAP_ON)
                  inputData.staticLightmapUV = IN.lightmapUV;
                #else
                  inputData.vertexSH = IN.sh;
                #endif
            #endif

            #if _WORLDSPACENORMAL
              float3 normalTS = WorldToTangentSpace(d, l.Normal);
            #else
              float3 normalTS = l.Normal;
            #endif

            SurfaceData surface         = (SurfaceData)0;
            surface.albedo              = l.Albedo;
            surface.metallic            = saturate(metallic);
            surface.specular            = specular;
            surface.smoothness          = saturate(l.Smoothness),
            surface.occlusion           = l.Occlusion,
            surface.emission            = l.Emission,
            surface.alpha               = saturate(l.Alpha);
            surface.clearCoatMask       = 0;
            surface.clearCoatSmoothness = 1;

            #ifdef _CLEARCOAT
                  surface.clearCoatMask       = saturate(l.CoatMask);
                  surface.clearCoatSmoothness = saturate(l.CoatSmoothness);
            #endif

            #if !_UNLIT
               half4 color = half4(l.Albedo, l.Alpha);
               #ifdef _DBUFFER
                  #if _BAKEDLIT
                     ApplyDecalToBaseColorAndNormal(IN.pos, color, inputData.normalWS);
                  #else
                     ApplyDecalToSurfaceData(IN.pos, surface, inputData);
                  #endif
               #endif
               #if _SIMPLELIT
                  color = UniversalFragmentBlinnPhong(
                     inputData,
                     surface);
               #elif _BAKEDLIT
                  color = UniversalFragmentBakedLit(inputData, color.rgb, color.a, normalTS);
               #else
                  color = UniversalFragmentPBR(inputData, surface);
               #endif

               #if !DISABLEFOG
                  color.rgb = MixFog(color.rgb, inputData.fogCoord);
               #endif

            #else // unlit
               #ifdef _DBUFFER
                  ApplyDecalToSurfaceData(IN.pos, surface, inputData);
               #endif
               half4 color = UniversalFragmentUnlit(inputData, l.Albedo, l.Alpha);
               #if !DISABLEFOG
                  color.rgb = MixFog(color.rgb, inputData.fogCoord);
               #endif
            #endif
            ChainFinalColorForward(l, d, color);

            return color;

         }

         ENDHLSL

      }


      
        Pass
        {
            Name "GBuffer"
            Tags
            {
               "LightMode" = "UniversalGBuffer"
            }
           
             Blend One Zero
             ZTest LEqual
             ZWrite On

            	Cull [_Cull]


            HLSLPROGRAM

               #pragma vertex Vert
   #pragma fragment Frag

            #pragma target 3.0

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma multi_compile_instancing
            #pragma multi_compile_fog
            #pragma instancing_options renderinglayer
            
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile _ DYNAMICLIGHTMAP_ON
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
            #pragma multi_compile_fragment _ _REFLECTION_PROBE_BLENDING
            #pragma multi_compile_fragment _ _REFLECTION_PROBE_BOX_PROJECTION
            #pragma multi_compile_fragment _ _SHADOWS_SOFT
            #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
            #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
            #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
            #pragma multi_compile_fragment _ _GBUFFER_NORMALS_OCT
            #pragma multi_compile_fragment _ _LIGHT_LAYERS
            #pragma multi_compile_fragment _ _RENDER_PASS_ENABLED
            #pragma multi_compile_fragment _ DEBUG_DISPLAY
            #pragma multi_compile _ SHADOWS_SHADOWMASK

            #define _FOG_FRAGMENT 1

            #define VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
            #define SHADERPASS SHADERPASS_GBUFFER
            #define _PASSGBUFFER 1

            
	#pragma multi_compile_local _LIGHTINGTYPE_PIXEL _LIGHTINGTYPE_VERTEX _LIGHTINGTYPE_TEXEL
	#pragma multi_compile_local _TEXTUREFILTERING_NEAREST _TEXTUREFILTERING_LINEAR _TEXTUREFILTERING_N64
	#pragma shader_feature_local _VERTEXJITTER
	#pragma shader_feature_local _VCOLSMOOTHNESS
	#pragma shader_feature_local _TOONSHADING
	#pragma shader_feature_local _EMISSION
	#pragma shader_feature_local _ALPHATEST_ON
	#pragma shader_feature_local _AFFINE_MAPPING
	#undef _UNLIT


   #define _URP 1

#define _UNLIT 1
#define _USINGTEXCOORD1 1


            

            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
            

            

                  #undef WorldNormalVector
      #define WorldNormalVector(data, normal) mul(normal, data.TBNMatrix)
      
      #define UnityObjectToWorldNormal(normal) mul(GetObjectToWorldMatrix(), normal)

      #define _WorldSpaceLightPos0 _MainLightPosition
      
      #define UNITY_DECLARE_TEX2D(name) TEXTURE2D(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2D_NOSAMPLER(name) TEXTURE2D(name);
      #define UNITY_DECLARE_TEX2DARRAY(name) TEXTURE2D_ARRAY(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(name) TEXTURE2D_ARRAY(name);

      #define UNITY_SAMPLE_TEX2DARRAY(tex,coord)            SAMPLE_TEXTURE2D_ARRAY(tex, sampler##tex, coord.xy, coord.z)
      #define UNITY_SAMPLE_TEX2DARRAY_LOD(tex,coord,lod)    SAMPLE_TEXTURE2D_ARRAY_LOD(tex, sampler##tex, coord.xy, coord.z, lod)
      #define UNITY_SAMPLE_TEX2D(tex, coord)                SAMPLE_TEXTURE2D(tex, sampler##tex, coord)
      #define UNITY_SAMPLE_TEX2D_SAMPLER(tex, samp, coord)  SAMPLE_TEXTURE2D(tex, sampler##samp, coord)

      #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod)   SAMPLE_TEXTURE2D_LOD(tex, sampler_##tex, coord, lod)
      #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) SAMPLE_TEXTURE2D_LOD (tex, sampler##samplertex,coord, lod)
     
      #if defined(UNITY_COMPILER_HLSL)
         #define UNITY_INITIALIZE_OUTPUT(type,name) name = (type)0;
      #else
         #define UNITY_INITIALIZE_OUTPUT(type,name)
      #endif

      #define sampler2D_float sampler2D
      #define sampler2D_half sampler2D

      

      // data across stages, stripped like the above.
      struct VertexToPixel
      {
         float4 pos : SV_POSITION;
         float3 worldPos : TEXCOORD0;
         float3 worldNormal : TEXCOORD1;
         float4 worldTangent : TEXCOORD2;
          float4 texcoord0 : TEXCOORD3;
          float4 texcoord1 : TEXCOORD4;
         // float4 texcoord2 : TEXCOORD5;

         // #if %TEXCOORD3REQUIREKEY%
         // float4 texcoord3 : TEXCOORD6;
         // #endif

         // #if %SCREENPOSREQUIREKEY%
          float4 screenPos : TEXCOORD7;
         // #endif

         // #if %VERTEXCOLORREQUIREKEY%
          half4 vertexColor : COLOR;
         // #endif

         #if defined(LIGHTMAP_ON)
            float2 lightmapUV : TEXCOORD8;
         #endif
         #if defined(DYNAMICLIGHTMAP_ON)
            float2 dynamicLightmapUV : TEXCOORD9;
         #endif
         #if !defined(LIGHTMAP_ON)
            float3 sh : TEXCOORD10;
         #endif

         #if defined(VARYINGS_NEED_FOG_AND_VERTEX_LIGHT)
            float4 fogFactorAndVertexLight : TEXCOORD11;
         #endif

         #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
           float4 shadowCoord : TEXCOORD12;
         #endif

         // #if %EXTRAV2F0REQUIREKEY%
          float4 extraV2F0 : TEXCOORD13;
         // #endif

         // #if %EXTRAV2F1REQUIREKEY%
          float4 extraV2F1 : TEXCOORD14;
         // #endif

         // #if %EXTRAV2F2REQUIREKEY%
          float4 extraV2F2 : TEXCOORD15;
         // #endif

         // #if %EXTRAV2F3REQUIREKEY%
          float4 extraV2F3 : TEXCOORD16;
         // #endif

         // #if %EXTRAV2F4REQUIREKEY%
          float4 extraV2F4 : TEXCOORD17;
         // #endif

         // #if %EXTRAV2F5REQUIREKEY%
         // float4 extraV2F5 : TEXCOORD18;
         // #endif

         // #if %EXTRAV2F6REQUIREKEY%
         // float4 extraV2F6 : TEXCOORD19;
         // #endif

         // #if %EXTRAV2F7REQUIREKEY%
         // float4 extraV2F7 : TEXCOORD20;
         // #endif

         #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
         #endif
         #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
         #endif
         #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
         #endif
         #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
         #endif
      };



            
            
            // data describing the user output of a pixel
            struct Surface
            {
               half3 Albedo;
               half Height;
               half3 Normal;
               half Smoothness;
               half3 Emission;
               half Metallic;
               half3 Specular;
               half Occlusion;
               half SpecularPower; // for simple lighting
               half Alpha;
               float outputDepth; // if written, SV_Depth semantic is used. ShaderData.clipPos.z is unused value
               // HDRP Only
               half SpecularOcclusion;
               half SubsurfaceMask;
               half Thickness;
               half CoatMask;
               half CoatSmoothness;
               half Anisotropy;
               half IridescenceMask;
               half IridescenceThickness;
               int DiffusionProfileHash;
               float SpecularAAThreshold;
               float SpecularAAScreenSpaceVariance;
               // requires _OVERRIDE_BAKEDGI to be defined, but is mapped in all pipelines
               float3 DiffuseGI;
               float3 BackDiffuseGI;
               float3 SpecularGI;
               // requires _OVERRIDE_SHADOWMASK to be defines
               float4 ShadowMask;
            };

            // Data the user declares in blackboard blocks
            struct Blackboard
            {
                
                float blackboardDummyData;
            };

            // data the user might need, this will grow to be big. But easy to strip
            struct ShaderData
            {
               float4 clipPos; // SV_POSITION
               float3 localSpacePosition;
               float3 localSpaceNormal;
               float3 localSpaceTangent;
        
               float3 worldSpacePosition;
               float3 worldSpaceNormal;
               float3 worldSpaceTangent;
               float tangentSign;

               float3 worldSpaceViewDir;
               float3 tangentSpaceViewDir;

               float4 texcoord0;
               float4 texcoord1;
               float4 texcoord2;
               float4 texcoord3;

               float2 screenUV;
               float4 screenPos;

               float4 vertexColor;
               bool isFrontFace;

               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;

               float3x3 TBNMatrix;
               Blackboard blackboard;
            };

            struct VertexData
            {
               #if SHADER_TARGET > 30
               // uint vertexID : SV_VertexID;
               #endif
               float4 vertex : POSITION;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;

               // optimize out mesh coords when not in use by user or lighting system
               #if _URP && (_USINGTEXCOORD1 || _PASSMETA || _PASSFORWARD || _PASSGBUFFER)
                  float4 texcoord1 : TEXCOORD1;
               #endif

               #if _URP && (_USINGTEXCOORD2 || _PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && defined(DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif

               #if _STANDARD && (_USINGTEXCOORD1 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER || _PASSFORWARDADD) && LIGHTMAP_ON)))
                  float4 texcoord1 : TEXCOORD1;
               #endif
               #if _STANDARD && (_USINGTEXCOORD2 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif


               #if _HDRP
                  float4 texcoord1 : TEXCOORD1;
                  float4 texcoord2 : TEXCOORD2;
               #endif

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
                float4 vertexColor : COLOR;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD4; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity    : TEXCOORD5; // Add Precomputed Velocity (Alembic computes velocities on runtime side).
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct TessVertex 
            {
               float4 vertex : INTERNALTESSPOS;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;
               float4 texcoord1 : TEXCOORD1;
               float4 texcoord2 : TEXCOORD2;

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
                float4 vertexColor : COLOR;
               // #endif

               // #if %EXTRAV2F0REQUIREKEY%
                float4 extraV2F0 : TEXCOORD5;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
                float4 extraV2F1 : TEXCOORD6;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
                float4 extraV2F2 : TEXCOORD7;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
                float4 extraV2F3 : TEXCOORD8;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
                float4 extraV2F4 : TEXCOORD9;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // float4 extraV2F5 : TEXCOORD10;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // float4 extraV2F6 : TEXCOORD11;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // float4 extraV2F7 : TEXCOORD12;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD13; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity : TEXCOORD14;
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
               UNITY_VERTEX_OUTPUT_STEREO
            };

            struct ExtraV2F
            {
               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;
               Blackboard blackboard;
               float4 time;
            };


            float3 WorldToTangentSpace(ShaderData d, float3 normal)
            {
               return mul(d.TBNMatrix, normal);
            }

            float3 TangentToWorldSpace(ShaderData d, float3 normal)
            {
               return mul(normal, d.TBNMatrix);
            }

            // in this case, make standard more like SRPs, because we can't fix
            // unity_WorldToObject in HDRP, since it already does macro-fu there

            #if _STANDARD
               float3 TransformWorldToObject(float3 p) { return mul(unity_WorldToObject, float4(p, 1)); };
               float3 TransformObjectToWorld(float3 p) { return mul(unity_ObjectToWorld, float4(p, 1)); };
               float4 TransformWorldToObject(float4 p) { return mul(unity_WorldToObject, p); };
               float4 TransformObjectToWorld(float4 p) { return mul(unity_ObjectToWorld, p); };
               float4x4 GetWorldToObjectMatrix() { return unity_WorldToObject; }
               float4x4 GetObjectToWorldMatrix() { return unity_ObjectToWorld; }
               #if (defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (SHADER_TARGET_SURFACE_ANALYSIS && !SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod) tex.SampleLevel (sampler##tex,coord, lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) tex.SampleLevel (sampler##samplertex,coord, lod)
              #else
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord,lod) tex2D (tex,coord,0,lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord,lod) tex2D (tex,coord,0,lod)
              #endif

               #undef GetWorldToObjectMatrix()

               #define GetWorldToObjectMatrix()   unity_WorldToObject


            #endif

            float3 GetCameraWorldPosition()
            {
               #if _HDRP
                  return GetCameraRelativePositionWS(_WorldSpaceCameraPos);
               #else
                  return _WorldSpaceCameraPos;
               #endif
            }

            #if _GRABPASSUSED
               #if _STANDARD
                  TEXTURE2D(%GRABTEXTURE%);
                  SAMPLER(sampler_%GRABTEXTURE%);
               #endif

               half3 GetSceneColor(float2 uv)
               {
                  #if _STANDARD
                     return SAMPLE_TEXTURE2D(%GRABTEXTURE%, sampler_%GRABTEXTURE%, uv).rgb;
                  #else
                     return SHADERGRAPH_SAMPLE_SCENE_COLOR(uv);
                  #endif
               }
            #endif


      
            #if _STANDARD
               UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
               float GetSceneDepth(float2 uv) { return SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv)); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv)); } 
            #else
               float GetSceneDepth(float2 uv) { return SHADERGRAPH_SAMPLE_SCENE_DEPTH(uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv), _ZBufferParams); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv), _ZBufferParams); } 
            #endif

            float3 GetWorldPositionFromDepthBuffer(float2 uv, float3 worldSpaceViewDir)
            {
               float eye = GetLinearEyeDepth(uv);
               float3 camView = mul((float3x3)GetObjectToWorldMatrix(), transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V)) [2].xyz);

               float dt = dot(worldSpaceViewDir, camView);
               float3 div = worldSpaceViewDir/dt;
               float3 wpos = (eye * div) + GetCameraWorldPosition();
               return wpos;
            }

            #if _HDRP
            float3 ObjectToWorldSpacePosition(float3 pos)
            {
               return GetAbsolutePositionWS(TransformObjectToWorld(pos));
            }
            #else
            float3 ObjectToWorldSpacePosition(float3 pos)
            {
               return TransformObjectToWorld(pos);
            }
            #endif

            #if _STANDARD
               UNITY_DECLARE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture);
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  float4 depthNorms = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture, uv);
                  float3 norms = DecodeViewNormalStereo(depthNorms);
                  norms = mul((float3x3)GetWorldToViewMatrix(), norms) * 0.5 + 0.5;
                  return norms;
               }
            #elif _HDRP
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  NormalData nd;
                  DecodeFromNormalBuffer(_ScreenSize.xy * uv, nd);
                  return nd.normalWS;
               }
            #elif _URP
               #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                  #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareNormalsTexture.hlsl"
               #endif

               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                     return SampleSceneNormals(uv);
                  #else
                     float3 wpos = GetWorldPositionFromDepthBuffer(uv, worldSpaceViewDir);
                     return normalize(-cross(ddx(wpos), ddy(wpos))) * 0.5 + 0.5;
                  #endif

                }
             #endif

             #if _HDRP

               half3 UnpackNormalmapRGorAG(half4 packednormal)
               {
                     // This do the trick
                  packednormal.x *= packednormal.w;

                  half3 normal;
                  normal.xy = packednormal.xy * 2 - 1;
                  normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                  return normal;
               }
               half3 UnpackNormal(half4 packednormal)
               {
                  #if defined(UNITY_NO_DXT5nm)
                     return packednormal.xyz * 2 - 1;
                  #else
                     return UnpackNormalmapRGorAG(packednormal);
                  #endif
               }
               #endif
               #if _HDRP || _URP

               half3 UnpackScaleNormal(half4 packednormal, half scale)
               {
                 #ifndef UNITY_NO_DXT5nm
                   // Unpack normal as DXT5nm (1, y, 1, x) or BC5 (x, y, 0, 1)
                   // Note neutral texture like "bump" is (0, 0, 1, 1) to work with both plain RGB normal and DXT5nm/BC5
                   packednormal.x *= packednormal.w;
                 #endif
                   half3 normal;
                   normal.xy = (packednormal.xy * 2 - 1) * scale;
                   normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                   return normal;
               }	

             #endif


            void GetSun(out float3 lightDir, out float3 color)
            {
               lightDir = float3(0.5, 0.5, 0);
               color = 1;
               #if _HDRP
                  if (_DirectionalLightCount > 0)
                  {
                     DirectionalLightData light = _DirectionalLightDatas[0];
                     lightDir = -light.forward.xyz;
                     color = light.color;
                  }
               #elif _STANDARD
			         lightDir = normalize(_WorldSpaceLightPos0.xyz);
                  color = _LightColor0.rgb;
               #elif _URP
	               Light light = GetMainLight();
	               lightDir = light.direction;
	               color = light.color;
               #endif
            }


            
            CBUFFER_START(UnityPerMaterial)

               

	int _VColEmissive;
	float _VertexEmissiveStr;
	half4 _Color;
	int _HalfLambert;
	int _EnableMaskMap;
	int _ForceNearest;
	int _TextureFiltering;
	int _SpecularType;
	int _Reflections;
	float _NormalStrength;
	float2 _SmoothRemap;
	float2 _SmoothRemapVCol;
	float _VertexLightSmoothness;
	float3 _VertexSpecColor;
	float3 _EmissionColor;
	float4 _TilingOffset;
	float _Cutoff;
	float _AffineMapFactor;
	int _VertexJitterResX;
	int _VertexJitterResY;
	int _DoubleSizeDither;
	int _MaskTint;
	float _LightmapEmissionStr;
	
	int _DitherAmbient;
	int _DitherDiffuse;
	int _DitherSpec;

	float4 _BaseMap_TexelSize;
	float4 _MaskMap_TexelSize;
	float4 _BumpMap_TexelSize;
	float4 _EmissionMap_TexelSize;




            CBUFFER_END

            

            

            #ifdef unity_WorldToObject
#undef unity_WorldToObject
#endif
#ifdef unity_ObjectToWorld
#undef unity_ObjectToWorld
#endif
#define unity_ObjectToWorld GetObjectToWorldMatrix()
#define unity_WorldToObject GetWorldToObjectMatrix()


	/**********
	* TODO:
	* Vertex Colors
	* Meta Pass?
	**********/

	TEXTURE2D(_ToonRamp);
	TEXTURE2D(_SpecularRamp);
	SamplerState sampler_linear_clamp;

	TEXTURE2D(_BaseMap);
	SamplerState sampler_linear_repeat;
	SamplerState sampler_trilinear_repeat;
	SamplerState sampler_point_repeat;

	#if defined(_TEXTUREFILTERING_LINEAR) || defined(PASS_META)
		#define MAINSMP sampler_linear_repeat
	#else
		#define MAINSMP sampler_point_repeat
	#endif

	static const uint bayerPattern[] =
	{
		0, 32,  8, 40,  2, 34, 10, 42,   /* 8x8 Bayer ordered dithering  */
		48, 16, 56, 24, 50, 18, 58, 26,  /* pattern.  Each input pixel   */
		12, 44,  4, 36, 14, 46,  6, 38,  /* is scaled to the 0..63 range */
		60, 28, 52, 20, 62, 30, 54, 22,  /* before looking in this table */
		3, 35, 11, 43,  1, 33,  9, 41,   /* to determine the action.     */
		51, 19, 59, 27, 49, 17, 57, 25,
		15, 47,  7, 39, 13, 45,  5, 37,
		63, 31, 55, 23, 61, 29, 53, 21
	};

	float ditherValue(float2 screenPos)
	{
		uint stippleOffset = ((uint) screenPos.y % 8) * 8 + ((uint) screenPos.x % 8);
		uint byte = bayerPattern[stippleOffset];
		return byte / 64.0f;
	}

	float GetLuminance(float3 col)
	{
		return dot(col, float3(0.299f, 0.587f, 0.114f));
	}

	void ditherColor(inout float3 input, float2 screenPos)
	{
		input.x = input.x < ditherValue(screenPos) ? input.x*0.25 : input.x;
		input.y = input.y < ditherValue(screenPos) ? input.y*0.25 : input.y;
		input.z = input.z < ditherValue(screenPos) ? input.z*0.25 : input.z;
	}

	// This function has been basically ripped straight from https://forum.unity.com/threads/the-quest-for-efficient-per-texel-lighting.529948/
	// So big thanks to GreatestBear, and truepak182, without whom this shader wouldn't be nearly as cool
	float3 CalculateTexelWorldPos(float2 originalUV, float3 originalWorldPos, float4 texelSize)
	{
		// 1.) Calculate how much the texture UV coords need to
		//     shift to be at the center of the nearest texel.
		float2 centerUV = floor(originalUV * (texelSize.zw))/texelSize.zw + (texelSize.xy/2.0);
		float2 dUV = (centerUV - originalUV);
	
		// 2b.) Calculate how much the texture coords vary over fragment space.
		//      This essentially defines a 2x2 matrix that gets
		//      texture space (UV) deltas from fragment space (ST) deltas
		// Note: I call fragment space (S,T) to disambiguate.
		float2 dUVdS = ddx(originalUV);
		float2 dUVdT = ddy(originalUV);
	
		// 2c.) Invert the fragment from texture matrix
		float2x2 dSTdUV = float2x2(dUVdT[1], -dUVdT[0], -dUVdS[1], dUVdS[0])*(1.0f/(dUVdS[0]*dUVdT[1]-dUVdT[0]*dUVdS[1]));
	
	
		// 2d.) Convert the UV delta to a fragment space delta
		float2 dST = mul(dSTdUV , dUV);
	
		// 2e.) Calculate how much the world coords vary over fragment space.
		float3 dXYZdS = ddx(originalWorldPos);
		float3 dXYZdT = ddy(originalWorldPos);
	
		// 2f.) Finally, convert our fragment space delta to a world space delta
		// And be sure to clamp it to SOMETHING in case the derivative calc went insane
		// Here I clamp it to -1 to 1 unit in unity, which should be orders of magnitude greater
		// than the size of any texel.
		float3 dXYZ = clamp(dXYZdS * dST[0] + dXYZdT * dST[1], -1, 1);
	
		// 3.) Transform the snapped UV back to world space
		return originalWorldPos + dXYZ;
	}

	float3 CalculateTexelWorldViewDir(float3 viewDirectionWS, float3 positionWS)
	{
		//Recalculate view direction according to recalculated texelized inputData.positionWS
		float3 viewDirectionWSCorrected = GetCameraPositionWS() - positionWS;
		//This needs to be done to make corrected viewDirectionWSCorrected same length as the original. If omitted, specular will not be displayed properly
		float viewPositionMult = length(viewDirectionWS) / length(viewDirectionWSCorrected);

		return viewDirectionWSCorrected * viewPositionMult;
	}

	float2 SnapUVToTexelCenter(float2 originalUV, float4 texelSize)
	{
		float2 centerUV = floor(originalUV * (texelSize.zw))/texelSize.zw + (texelSize.xy/2.0);
		float2 dUV = (centerUV - originalUV);

		return originalUV + dUV;
	}


	float3 SampleRamp(Texture2D ramp, float coord)
	{
		return SAMPLE_TEXTURE2D_LOD(ramp, sampler_linear_clamp, saturate(float2(pow(coord,0.5), 0.5)), 0).xyz;
	}

	float invLerp(float from, float to, float value){
		return (value - from) / (to - from);
	}

	float remap(float origFrom, float origTo, float targetFrom, float targetTo, float value){
		float rel = invLerp(origFrom, origTo, value);
		return lerp(targetFrom, targetTo, rel);
	}

	float remap01(float targetFrom, float targetTo, float value){
		float rel = invLerp(0, 1, value);
		return lerp(targetFrom, targetTo, rel);

	}

	void MainLight(out float3 direction, out float3 color, out float distanceAtten)
	{
		Light mainLight = GetMainLight();
		direction = mainLight.direction;
		color = mainLight.color;
		distanceAtten = mainLight.distanceAttenuation;
	}

	float MainLightShadows(float3 worldPos, float2 lightmapUV = float2(0.0,0.0), float2 origUV = float2(0.0,0.0))
	{
		float4 shadowCoord = TransformWorldToShadowCoord(worldPos);

		#if !defined (LIGHTMAP_ON) || defined(_LIGHTINGTYPE_VERTEX)
			half4 shadowMask = unity_ProbesOcclusion;
		#else
			lightmapUV = lightmapUV * unity_LightmapST.xy + unity_LightmapST.zw;

			#ifdef _LIGHTINGTYPE_TEXEL
				lightmapUV = CalculateTexelWorldPos(origUV, float3(lightmapUV.x, lightmapUV.y, 1), _BaseMap_TexelSize).xy;
			#endif

			half4 shadowMask = SAMPLE_SHADOWMASK(lightmapUV);
		#endif

		return MainLightShadow(shadowCoord, worldPos, shadowMask, _MainLightOcclusionProbes);
	}

	float3 GetAmbient(float3 worldNormal, float2 lightmapUV, float2 origUV = float2(0.0f, 0.0f))
	{
		#ifdef LIGHTMAP_ON
			lightmapUV = lightmapUV * unity_LightmapST.xy + unity_LightmapST.zw;

			#ifdef _LIGHTINGTYPE_TEXEL
				lightmapUV = CalculateTexelWorldPos(origUV, float3(lightmapUV.x, lightmapUV.y, 1), _BaseMap_TexelSize).xy;
			#endif

			#ifdef _LIGHTINGTYPE_VERTEX
				float4 lightmap = SAMPLE_TEXTURE2D_LOD(unity_Lightmap, sampler_linear_repeat, lightmapUV, 0);
				half3 bakedGI = DecodeLightmap(lightmap,half4(LIGHTMAP_HDR_MULTIPLIER, LIGHTMAP_HDR_EXPONENT, 0.0h, 0.0h));
			#else
				half3 bakedGI = SampleLightmap(lightmapUV, worldNormal);//DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, lightmapUV),half4(LIGHTMAP_HDR_MULTIPLIER, LIGHTMAP_HDR_EXPONENT, 0.0h, 0.0h));
			#endif

			#if defined(_MIXED_LIGHTING_SUBTRACTIVE)
				Light mainLight = GetMainLight();
				bakedGI = SubtractDirectMainLightFromLightmap(mainLight, worldNormal, bakedGI);
			#endif

			return bakedGI;
		#else
			return SampleSH(worldNormal);
		#endif
	}

	float GetSpecular(float3 lightDir, float3 worldNormal, float3 viewDir, float smoothness)
	{
		float SpecSmoothness = exp2(10 * smoothness + 1).x;
		[branch]
		if (_SpecularType == 0) //Cook-Torrance
		{
			float roughness = 1 - smoothness;
			roughness *= roughness;

			float3 h = SafeNormalize(lightDir + viewDir);
			float nh = saturate(dot(worldNormal, h));
			float lh = saturate(dot(lightDir, h));
			float d = nh * nh * (roughness * roughness - 1.0) + 1.00001;
			float normTerm = roughness * 4.0 + 2.0;
			float specTerm = roughness * roughness;
			specTerm /= (d * d) * max(0.1, lh * lh) * normTerm;
			return specTerm*0.04;
		}
		else if (_SpecularType == 1) //Phong
		{
			float3 r = reflect(-lightDir, worldNormal);
			float rdotv = max(0, dot(r, viewDir));

			return pow(rdotv, SpecSmoothness)*smoothness;
		}
		else if (_SpecularType == 2) //Blinn-Phong
		{
			float3 h = SafeNormalize(lightDir+viewDir);
			float ndh = saturate(dot(worldNormal, h));

			return pow(ndh, SpecSmoothness)*smoothness;
		}
		else
		{
			return 0;
		}
	}

	float GetDiffuse(float3 lightDir, float3 worldNormal)
	{
		float diffuse = dot(lightDir, worldNormal);

		if (_HalfLambert)
			diffuse = diffuse * 0.5 + 0.5;

		return max(0,diffuse);
	}


	void AdditionalLights(float3 worldPos, float3 viewDir, float3 worldNormal, float InSmoothness, float3 specColor, out float3 Diffuse, out float3 Specular, float dirOcclusion = 1)
	{
		float3 diff = 0;
		float3 spec = 0;

		#if !defined (LIGHTMAP_ON)
			half4 shadowMask = unity_ProbesOcclusion;
		#else
			half4 shadowMask = half4(1, 1, 1, 1);
		#endif

		int pixelLights = GetAdditionalLightsCount();
		for (int i = 0; i < pixelLights; ++i)
		{
			Light light = GetAdditionalLight(i, worldPos, shadowMask);
			
			#ifdef _TOONSHADING
				float diffMask = GetDiffuse(light.direction, worldNormal) * light.shadowAttenuation * light.distanceAttenuation * dirOcclusion;
				diff += SampleRamp(_ToonRamp, diffMask) * light.color;
			#else
				diff += GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.color * light.shadowAttenuation * dirOcclusion;
			#endif

			#ifdef _TOONSHADING
				float specMask = GetSpecular(light.direction, worldNormal, viewDir, InSmoothness) * light.distanceAttenuation * light.shadowAttenuation * dirOcclusion;
				spec += SampleRamp(_SpecularRamp, specMask) * light.color * specColor;
			#else
				spec += GetSpecular(light.direction, worldNormal, viewDir, InSmoothness) * light.distanceAttenuation * light.color * specColor * light.shadowAttenuation * dirOcclusion;
			#endif
		}
		Diffuse = diff;
		Specular = spec;
	}


	float3 GetReflections(float3 worldNormal, float3 viewDir, float smoothness)
	{
		float perceptualRoughness = 1 - smoothness;
		perceptualRoughness *= perceptualRoughness;
		float mip = PerceptualRoughnessToMipmapLevel(perceptualRoughness);
		float3 sample = SHADERGRAPH_REFLECTION_PROBE(viewDir, worldNormal, mip);
		return sample.rgb;
	}

	void SSAO(ShaderData d, out float directOcclusion, out float indirectOcclusion)
	{
		#if defined(_SCREEN_SPACE_OCCLUSION) && !defined(RETROSHADER_ALPHA)
			float2 uv = d.screenUV;

			#ifdef _LIGHTINGTYPE_TEXEL
				float3 pos = CalculateTexelWorldPos(d.texcoord0, float3(uv.x, uv.y, 1.0), _BaseMap_TexelSize);
				uv = pos.xy;
			#endif

			AmbientOcclusionFactor aoFactor = GetScreenSpaceAmbientOcclusion(uv);
			directOcclusion = aoFactor.directAmbientOcclusion;
			indirectOcclusion = aoFactor.indirectAmbientOcclusion;
		#else
			directOcclusion = 1;
			indirectOcclusion = 1;
		#endif
	}

	// Courtesy of Ben Golus
	// https://forum.unity.com/threads/shader-ideas-for-replicating-n64-bi-linear-filtering.936119/#post-6116207
	half4 sampleN64(Texture2D tex, float2 uv, float4 texelSize)
	{
		// texel coordinates
		float2 texels = uv * texelSize.zw;

		// calculate mip level
		float2 dx = ddx(texels);
		float2 dy = ddy(texels);
		float delta_max_sqr = max(dot(dx, dx), dot(dy, dy));
		float mip = max(0.0, 0.5 * log2(delta_max_sqr));

		// scale texel sizes and texel coordinates to handle mip levels properly
		float scale = pow(2,floor(mip));
		texelSize.xy *= scale;
		texelSize.zw /= scale;
		texels = texels / scale - 0.5;

		// calculate blend for the three points of the tri-filter
		float2 fracTexels = frac(texels);
		float3 blend = float3(
			abs(fracTexels.x+fracTexels.y-1),
			min(abs(fracTexels.xx-float2(0,1)), abs(fracTexels.yy-float2(1,0)))
		);

		// calculate equivalents of point filtered uvs for the three points
		float2 uvA = (floor(texels + fracTexels.yx) + 0.5) * texelSize.xy;
		float2 uvB = (floor(texels) + float2(1.5, 0.5)) * texelSize.xy;
		float2 uvC = (floor(texels) + float2(0.5, 1.5)) * texelSize.xy;

		// sample points
		half4 A = SAMPLE_TEXTURE2D_LOD(tex, MAINSMP, uvA, mip);
		half4 B = SAMPLE_TEXTURE2D_LOD(tex, MAINSMP, uvB, mip);
		half4 C = SAMPLE_TEXTURE2D_LOD(tex, MAINSMP, uvC, mip);

		// blend and return
		return A * blend.x + B * blend.y + C * blend.z;
	}

	half4 sampleTex(Texture2D tex, float2 uv, float4 texelSize)
	{
		#ifdef _TEXTUREFILTERING_N64
			return sampleN64(tex, uv, texelSize);
		#else
			return SAMPLE_TEXTURE2D(tex, MAINSMP, uv);
		#endif
	}

	TEXTURE2D(_MaskMap);
	TEXTURE2D(_BumpMap);
	TEXTURE2D(_EmissionMap);

	void Ext_ModifyVertex0 (inout VertexData v, inout ExtraV2F d)
	{
		float4 clipPos = mul(GetWorldToHClipMatrix(), mul(unity_ObjectToWorld, v.vertex));

		

		#if defined(_LIGHTINGTYPE_VERTEX) && !defined(PASS_META)

			float smoothness = _VertexLightSmoothness;

			#ifdef _VCOLSMOOTHNESS
				smoothness = remap01(_SmoothRemapVCol.x, _SmoothRemapVCol.y, v.vertexColor.a);
			#endif

			float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
			float3 worldNormal = SafeNormalize(mul(unity_ObjectToWorld, v.normal));
			float3 viewDir = normalize(_WorldSpaceCameraPos - worldPos);
			float3 mainLightDir;
			float3 mainLightCol;
			float mainLightAtten;
			MainLight(mainLightDir, mainLightCol, mainLightAtten);

			d.extraV2F0.y = GetDiffuse(mainLightDir, worldNormal) * mainLightAtten;
			d.extraV2F1.xyz = GetAmbient(worldNormal, v.texcoord1);
			d.extraV2F0.z = MainLightShadows(worldPos, v.texcoord1);
			d.extraV2F0.w = GetSpecular(mainLightDir, worldNormal, viewDir, smoothness) * mainLightAtten * d.extraV2F0.z;

			#if !defined (LIGHTMAP_ON)
				half4 shadowMask = unity_ProbesOcclusion;
			#else
				half4 shadowMask = half4(1, 1, 1, 1);
			#endif

			// Have to store each light in a separate vector component so we can apply toon shading in fragment later - toon shading has to be applied per light to make sense
			// This limits us to max 4 lights, would need to use 2 more interpolators for 8
			int pixelLights = GetAdditionalLightsCount();
			Light light;
			if (pixelLights >= 1)
			{
				light = GetAdditionalLight(0, worldPos, shadowMask);
				d.extraV2F3.x = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.x = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
			if (pixelLights >= 2)
			{
				light = GetAdditionalLight(1, worldPos, shadowMask);
				d.extraV2F3.y = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.y = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
			if (pixelLights >= 3)
			{
				light = GetAdditionalLight(2, worldPos, shadowMask);
				d.extraV2F3.z = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.z = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
			if (pixelLights >= 4)
			{
				light = GetAdditionalLight(3, worldPos, shadowMask);
				d.extraV2F3.w = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.w = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
		#endif

		#if defined(_VERTEXJITTER) && !defined(_PASSSHADOW)
			float4 vert = clipPos;
			d.extraV2F2.xyz = mul(UNITY_MATRIX_I_VP, vert);
			vert.xyz = clipPos.xyz/clipPos.w;
			vert.x = floor(_VertexJitterResX * vert.x) / _VertexJitterResX;
			vert.y = floor(_VertexJitterResY * vert.y) / _VertexJitterResY;
			vert.xyz *= clipPos.w;
			v.vertex = mul(unity_WorldToObject, mul(UNITY_MATRIX_I_VP, vert));
		#endif

		d.extraV2F0.x = ComputeFogFactor(clipPos.z);
		v.texcoord0.xy = v.texcoord0.xy * _TilingOffset.xy + _TilingOffset.zw;

		#if defined(_AFFINE_MAPPING)
			_AffineMapFactor = min(1, _AffineMapFactor);
			float warpFactor = max(clipPos.w, 0.02); //Prevents flipping
			warpFactor = lerp(0.5, warpFactor, _AffineMapFactor);

			v.texcoord0.xy *= warpFactor;
			d.extraV2F1.w = warpFactor;
		#endif
	}

	
	void Ext_SurfaceFunction0 (inout Surface o, ShaderData d)
	{
		#if defined(_AFFINE_MAPPING)
			d.texcoord0.xy /= d.extraV2F1.w;
		#endif
		// Set up input data
		float4 mainTex = sampleTex(_BaseMap, d.texcoord0.xy, _BaseMap_TexelSize);
		o.Albedo = mainTex.rgb;
		float4 maskMap = sampleTex(_MaskMap, d.texcoord0.xy, _BaseMap_TexelSize);

		

		if (_MaskTint)
		{
			float3 tintedAlbedo = o.Albedo * _Color.rgb;
			o.Albedo = lerp(o.Albedo, tintedAlbedo, maskMap.a);
		}
		else
		{
			o.Albedo *= _Color.rgb;
		}

		#ifdef _EMISSION
			float3 emission = sampleTex(_EmissionMap, d.texcoord0.xy, _EmissionMap_TexelSize) * _EmissionColor;
			o.Emission = emission * _LightmapEmissionStr;

		#endif

		float2 pixelPos = d.clipPos.xy;//floor(d.screenUV.xy * _ScreenParams.xy);

		#if defined(_LIGHTINGTYPE_TEXEL) && !defined(PASS_META)
			o.Normal = UnpackScaleNormal(SAMPLE_TEXTURE2D(_BumpMap, sampler_linear_repeat, d.texcoord0.xy), _NormalStrength);
		#else
			o.Normal = UnpackScaleNormal(sampleTex(_BumpMap, d.texcoord0.xy, _BumpMap_TexelSize), _NormalStrength);
		#endif

		//o.Normal = SafeNormalize(o.Normal);

		#if defined(_VERTEXJITTER) && !defined(_PASSSHADOW)
			d.worldSpacePosition = d.extraV2F2.xyz;
		#endif

		d.worldSpaceNormal = TransformTangentToWorld(o.Normal, d.TBNMatrix);
		float3 unSnappedViewDir = d.worldSpaceViewDir;
		// Snap world position, view direction, and normal to the texel
		#if defined(_LIGHTINGTYPE_TEXEL) && !defined(PASS_META)
			d.worldSpacePosition = CalculateTexelWorldPos(d.texcoord0.xy, d.worldSpacePosition, _BaseMap_TexelSize);
			d.worldSpaceViewDir = CalculateTexelWorldViewDir(d.worldSpaceViewDir, d.worldSpacePosition);
			d.worldSpaceNormal = SafeNormalize(CalculateTexelWorldPos(d.texcoord0.xy, d.worldSpaceNormal, _BaseMap_TexelSize));
		#endif

		if (!_EnableMaskMap)
		{
			maskMap.r = 0;
			maskMap.g = mainTex.a;
			maskMap.b = 1;
		}

		o.Metallic = maskMap.r;
		o.Smoothness = remap01(_SmoothRemap.x, _SmoothRemap.y, maskMap.g);
		o.Occlusion =  maskMap.b;

		// Store lighting values in these
		float3 mainLightDir;
		float3 mainLightCol;
		float mainLightAtten;
		MainLight(mainLightDir, mainLightCol, mainLightAtten);

		float3 mainLightDiffuse;
		float mainShadow = 1;
		float3 ambient;
		float3 mainSpec = 0;

		float3 addDiffuse = 0;
		float3 addSpec = 0;

		float3 specColor = lerp(1.0, o.Albedo, o.Metallic);
		float reflectStr = lerp(0.04, 1, o.Metallic);
		
		if (_Reflections)
			o.Albedo *= 1 - o.Metallic;

		float3 reflection = 0;
		
		if (_Reflections)
			reflection = GetReflections(d.worldSpaceNormal, d.worldSpaceViewDir, o.Smoothness);

		#if !defined (LIGHTMAP_ON)
			half4 shadowMask = unity_ProbesOcclusion;
		#else
			half4 shadowMask = SAMPLE_SHADOWMASK(d.texcoord1.xy);
		#endif

		//Get lighting data, either from vertex shader or calculate it here
		#if defined(_LIGHTINGTYPE_VERTEX) && !defined(PASS_META)

			float vsmoothness = _VertexLightSmoothness;

			#ifdef _VCOLSMOOTHNESS
				vsmoothness = remap01(_SmoothRemapVCol.x, _SmoothRemapVCol.y, v.vertexColor.a);
			#endif

			o.Smoothness = vsmoothness;

			mainLightDiffuse = min(d.extraV2F0.y, d.extraV2F0.z); // Min(mainLight, shadow)

			#ifdef _TOONSHADING
				mainLightDiffuse = SampleRamp(_ToonRamp, mainLightDiffuse);
			#endif

			mainLightDiffuse *= mainLightCol;
			ambient = d.extraV2F1.xyz;

			#ifndef _TOONSHADING
				mainSpec = d.extraV2F0.w * mainLightCol;
			#else
				mainSpec = SampleRamp(_SpecularRamp, d.extraV2F0.w) * mainLightCol;
			#endif

			float3 worldPos = d.worldSpacePosition;

			int pixelLights = GetAdditionalLightsCount();
			Light light;
			if (pixelLights >= 1)
			{
				light = GetAdditionalLight(0, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.x * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.x) * light.color;
				#endif
				
				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.x * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.x) * light.color * specColor;
				#endif
			}
			if (pixelLights >= 2)
			{
				light = GetAdditionalLight(1, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.y * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.y) * light.color;
				#endif

				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.y * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.y) * light.color * specColor;
				#endif
			}
			if (pixelLights >= 3)
			{
				light = GetAdditionalLight(2, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.z * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.z) * light.color;
				#endif

				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.z * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.z) * light.color * specColor;
				#endif
			}
			if (pixelLights >= 4)
			{
				light = GetAdditionalLight(3, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.w * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.w) * light.color;
				#endif

				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.w * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.w) * light.color * specColor;
				#endif
			}

			mainSpec *= _VertexSpecColor;
			addSpec *= _VertexSpecColor;
			//addDiffuse = d.extraV2F3.xyz;
			//addSpec = d.extraV2F4.xyz;
		#else

			float dirOcclusion, indirOcclusion;
			SSAO(d, dirOcclusion, indirOcclusion);

			mainShadow = MainLightShadows(d.worldSpacePosition, d.texcoord1.xy, d.texcoord0.xy);
			
			#ifndef _TOONSHADING
				mainLightDiffuse = GetDiffuse(d.worldSpaceNormal, mainLightDir) * mainLightAtten * mainLightCol * mainShadow * dirOcclusion;
			#else
				mainLightDiffuse = GetDiffuse(d.worldSpaceNormal, mainLightDir) * mainShadow * dirOcclusion;
				float3 ramp = SampleRamp(_ToonRamp, mainLightDiffuse.x);
				mainLightDiffuse = ramp * mainLightAtten * mainLightCol;
			#endif

			ambient = GetAmbient(d.worldSpaceNormal, d.texcoord1.xy, d.texcoord0.xy) * min(o.Occlusion,indirOcclusion);
			mainSpec = GetSpecular(mainLightDir, d.worldSpaceNormal, d.worldSpaceViewDir, o.Smoothness)*dirOcclusion;

			#ifdef _TOONSHADING
				mainSpec = SampleRamp(_SpecularRamp, mainSpec.x);
			#endif

			mainSpec *= mainLightCol * mainLightAtten * specColor;
			mainSpec *= mainShadow;

			AdditionalLights(d.worldSpacePosition, d.worldSpaceViewDir, d.worldSpaceNormal, o.Smoothness, specColor, addDiffuse, addSpec);

		#endif

		float perceptualRoughness = 1 - o.Smoothness;
		perceptualRoughness *= perceptualRoughness;

		float fresnel = pow(1.0 - saturate(dot(d.worldSpaceNormal, d.worldSpaceViewDir)), 4);
		reflectStr *= lerp(0.1, 1, GetLuminance(mainLightDiffuse));
		reflectStr = lerp(reflectStr, saturate(reflectStr*10), fresnel);

		reflection *= reflectStr * specColor;
		reflection /= perceptualRoughness + 0.5;

		if (_DoubleSizeDither) pixelPos /= 2.0;

		[branch]
		if (_DitherDiffuse)
		{
			ditherColor(mainLightDiffuse, pixelPos);
			ditherColor(addDiffuse, pixelPos);
		}

		[branch]
		if (_DitherSpec)
		{
			ditherColor(mainSpec, pixelPos);
			ditherColor(addSpec, pixelPos);
		}

		[branch]
		if (_DitherAmbient)
			ditherColor(ambient, pixelPos);

		o.Alpha = mainTex.a * _Color.a;

		//o.Albedo *= 1 - saturate(GetLuminance(mainSpec));
		//o.Albedo *= 1 - saturate(GetLuminance(addSpec));

		#if defined(RETROSHADER_ALPHA) && !defined(PASS_META)
			if (_PreserveSpecular)
			{
				float specStrength = saturate(GetLuminance(mainSpec));
				float reflectStrength = saturate(GetLuminance(reflection));
				o.Alpha = max(o.Alpha, max(specStrength, reflectStrength));
			}
		#endif

		#ifdef _ALPHATEST_ON
			clip(step(_Cutoff, o.Alpha)-0.1f);
		#endif

		if (_VColEmissive)
		{
			o.Albedo += d.vertexColor.rgb * max(0,_VertexEmissiveStr);
			o.Emission = d.vertexColor.rgb * max(0,_VertexEmissiveStr);
		}
		else
		{
			o.Albedo *= d.vertexColor.rgb;
		}

		float3 combinedAmbient = ambient * o.Albedo;

		// Apply lighting, but not for lightbaking pass
		#if !defined(PASS_META)
			o.Albedo *= mainLightDiffuse + addDiffuse;
			o.Albedo += combinedAmbient;
			o.Albedo += mainSpec * saturate(GetLuminance(mainLightDiffuse));
			o.Albedo += addSpec * saturate(GetLuminance(addDiffuse));
			o.Albedo += reflection;

			#if defined(_EMISSION)
				o.Albedo += emission;
			#endif
		#endif
		
		#if defined(RETROSHADER_ALPHA) && !defined(PASS_META)
			if (_Refraction)
			{
				float origAlpha = o.Alpha;
				float4 offsetPos = float4(d.worldSpacePosition + d.worldSpaceNormal * _RefractionStr, 1.0);
				offsetPos = mul(GetWorldToHClipMatrix(), offsetPos);
				float scale = 1.0;

				#if UNITY_UV_STARTS_AT_TOP
					scale = -1.0;
				#endif

				float4 grabPos = offsetPos *0.5f;
				grabPos.xy = float2(grabPos.x, grabPos.y*scale) + grabPos.w;
				grabPos.zw = offsetPos.zw;
				grabPos.xy /= grabPos.w;
				grabPos.xy = clamp(grabPos.xy, float2(0.0, 0.0), float2(1.0, 1.0));

				float3 refraction = GetSceneColor(grabPos.xy);
				float2 cubemapBlendFactors = pow(abs((grabPos.xy - 0.5) * 2.0),0.25);
				float blend = smoothstep(0.75f, 1, max(cubemapBlendFactors.x, cubemapBlendFactors.y));
				float3 refractDir = refract(-d.worldSpaceViewDir, d.worldSpaceNormal, 1 - _RefractionStr);
				float3 reflectProbe = GetReflections(-refractDir, refractDir, 1.0);
				refraction = lerp(refraction, reflectProbe, blend);

				o.Albedo = lerp(refraction, o.Albedo, origAlpha);
				o.Alpha = 1;
			}
		#endif

		o.Albedo = MixFog(o.Albedo, d.extraV2F0.x);
	}




        
            void ChainSurfaceFunction(inout Surface l, inout ShaderData d)
            {
                  Ext_SurfaceFunction0(l, d);
                 // Ext_SurfaceFunction1(l, d);
                 // Ext_SurfaceFunction2(l, d);
                 // Ext_SurfaceFunction3(l, d);
                 // Ext_SurfaceFunction4(l, d);
                 // Ext_SurfaceFunction5(l, d);
                 // Ext_SurfaceFunction6(l, d);
                 // Ext_SurfaceFunction7(l, d);
                 // Ext_SurfaceFunction8(l, d);
                 // Ext_SurfaceFunction9(l, d);
		           // Ext_SurfaceFunction10(l, d);
                 // Ext_SurfaceFunction11(l, d);
                 // Ext_SurfaceFunction12(l, d);
                 // Ext_SurfaceFunction13(l, d);
                 // Ext_SurfaceFunction14(l, d);
                 // Ext_SurfaceFunction15(l, d);
                 // Ext_SurfaceFunction16(l, d);
                 // Ext_SurfaceFunction17(l, d);
                 // Ext_SurfaceFunction18(l, d);
		           // Ext_SurfaceFunction19(l, d);
                 // Ext_SurfaceFunction20(l, d);
                 // Ext_SurfaceFunction21(l, d);
                 // Ext_SurfaceFunction22(l, d);
                 // Ext_SurfaceFunction23(l, d);
                 // Ext_SurfaceFunction24(l, d);
                 // Ext_SurfaceFunction25(l, d);
                 // Ext_SurfaceFunction26(l, d);
                 // Ext_SurfaceFunction27(l, d);
                 // Ext_SurfaceFunction28(l, d);
		           // Ext_SurfaceFunction29(l, d);
            }

            void ChainModifyVertex(inout VertexData v, inout VertexToPixel v2p, float4 time)
            {
                 ExtraV2F d;
                 
                 ZERO_INITIALIZE(ExtraV2F, d);
                 ZERO_INITIALIZE(Blackboard, d.blackboard);
                 // due to motion vectors in HDRP, we need to use the last
                 // time in certain spots. So if you are going to use _Time to adjust vertices,
                 // you need to use this time or motion vectors will break. 
                 d.time = time;

                   Ext_ModifyVertex0(v, d);
                 // Ext_ModifyVertex1(v, d);
                 // Ext_ModifyVertex2(v, d);
                 // Ext_ModifyVertex3(v, d);
                 // Ext_ModifyVertex4(v, d);
                 // Ext_ModifyVertex5(v, d);
                 // Ext_ModifyVertex6(v, d);
                 // Ext_ModifyVertex7(v, d);
                 // Ext_ModifyVertex8(v, d);
                 // Ext_ModifyVertex9(v, d);
                 // Ext_ModifyVertex10(v, d);
                 // Ext_ModifyVertex11(v, d);
                 // Ext_ModifyVertex12(v, d);
                 // Ext_ModifyVertex13(v, d);
                 // Ext_ModifyVertex14(v, d);
                 // Ext_ModifyVertex15(v, d);
                 // Ext_ModifyVertex16(v, d);
                 // Ext_ModifyVertex17(v, d);
                 // Ext_ModifyVertex18(v, d);
                 // Ext_ModifyVertex19(v, d);
                 // Ext_ModifyVertex20(v, d);
                 // Ext_ModifyVertex21(v, d);
                 // Ext_ModifyVertex22(v, d);
                 // Ext_ModifyVertex23(v, d);
                 // Ext_ModifyVertex24(v, d);
                 // Ext_ModifyVertex25(v, d);
                 // Ext_ModifyVertex26(v, d);
                 // Ext_ModifyVertex27(v, d);
                 // Ext_ModifyVertex28(v, d);
                 // Ext_ModifyVertex29(v, d);


                 // #if %EXTRAV2F0REQUIREKEY%
                  v2p.extraV2F0 = d.extraV2F0;
                 // #endif

                 // #if %EXTRAV2F1REQUIREKEY%
                  v2p.extraV2F1 = d.extraV2F1;
                 // #endif

                 // #if %EXTRAV2F2REQUIREKEY%
                  v2p.extraV2F2 = d.extraV2F2;
                 // #endif

                 // #if %EXTRAV2F3REQUIREKEY%
                  v2p.extraV2F3 = d.extraV2F3;
                 // #endif

                 // #if %EXTRAV2F4REQUIREKEY%
                  v2p.extraV2F4 = d.extraV2F4;
                 // #endif

                 // #if %EXTRAV2F5REQUIREKEY%
                 // v2p.extraV2F5 = d.extraV2F5;
                 // #endif

                 // #if %EXTRAV2F6REQUIREKEY%
                 // v2p.extraV2F6 = d.extraV2F6;
                 // #endif

                 // #if %EXTRAV2F7REQUIREKEY%
                 // v2p.extraV2F7 = d.extraV2F7;
                 // #endif
            }

            void ChainModifyTessellatedVertex(inout VertexData v, inout VertexToPixel v2p)
            {
               ExtraV2F d;
               ZERO_INITIALIZE(ExtraV2F, d);
               ZERO_INITIALIZE(Blackboard, d.blackboard);

               // #if %EXTRAV2F0REQUIREKEY%
                d.extraV2F0 = v2p.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
                d.extraV2F1 = v2p.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
                d.extraV2F2 = v2p.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
                d.extraV2F3 = v2p.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
                d.extraV2F4 = v2p.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // d.extraV2F5 = v2p.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // d.extraV2F6 = v2p.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // d.extraV2F7 = v2p.extraV2F7;
               // #endif


               // Ext_ModifyTessellatedVertex0(v, d);
               // Ext_ModifyTessellatedVertex1(v, d);
               // Ext_ModifyTessellatedVertex2(v, d);
               // Ext_ModifyTessellatedVertex3(v, d);
               // Ext_ModifyTessellatedVertex4(v, d);
               // Ext_ModifyTessellatedVertex5(v, d);
               // Ext_ModifyTessellatedVertex6(v, d);
               // Ext_ModifyTessellatedVertex7(v, d);
               // Ext_ModifyTessellatedVertex8(v, d);
               // Ext_ModifyTessellatedVertex9(v, d);
               // Ext_ModifyTessellatedVertex10(v, d);
               // Ext_ModifyTessellatedVertex11(v, d);
               // Ext_ModifyTessellatedVertex12(v, d);
               // Ext_ModifyTessellatedVertex13(v, d);
               // Ext_ModifyTessellatedVertex14(v, d);
               // Ext_ModifyTessellatedVertex15(v, d);
               // Ext_ModifyTessellatedVertex16(v, d);
               // Ext_ModifyTessellatedVertex17(v, d);
               // Ext_ModifyTessellatedVertex18(v, d);
               // Ext_ModifyTessellatedVertex19(v, d);
               // Ext_ModifyTessellatedVertex20(v, d);
               // Ext_ModifyTessellatedVertex21(v, d);
               // Ext_ModifyTessellatedVertex22(v, d);
               // Ext_ModifyTessellatedVertex23(v, d);
               // Ext_ModifyTessellatedVertex24(v, d);
               // Ext_ModifyTessellatedVertex25(v, d);
               // Ext_ModifyTessellatedVertex26(v, d);
               // Ext_ModifyTessellatedVertex27(v, d);
               // Ext_ModifyTessellatedVertex28(v, d);
               // Ext_ModifyTessellatedVertex29(v, d);

               // #if %EXTRAV2F0REQUIREKEY%
                v2p.extraV2F0 = d.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
                v2p.extraV2F1 = d.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
                v2p.extraV2F2 = d.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
                v2p.extraV2F3 = d.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
                v2p.extraV2F4 = d.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // v2p.extraV2F5 = d.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // v2p.extraV2F6 = d.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // v2p.extraV2F7 = d.extraV2F7;
               // #endif
            }

            void ChainFinalColorForward(inout Surface l, inout ShaderData d, inout half4 color)
            {
               //   Ext_FinalColorForward0(l, d, color);
               //   Ext_FinalColorForward1(l, d, color);
               //   Ext_FinalColorForward2(l, d, color);
               //   Ext_FinalColorForward3(l, d, color);
               //   Ext_FinalColorForward4(l, d, color);
               //   Ext_FinalColorForward5(l, d, color);
               //   Ext_FinalColorForward6(l, d, color);
               //   Ext_FinalColorForward7(l, d, color);
               //   Ext_FinalColorForward8(l, d, color);
               //   Ext_FinalColorForward9(l, d, color);
               //  Ext_FinalColorForward10(l, d, color);
               //  Ext_FinalColorForward11(l, d, color);
               //  Ext_FinalColorForward12(l, d, color);
               //  Ext_FinalColorForward13(l, d, color);
               //  Ext_FinalColorForward14(l, d, color);
               //  Ext_FinalColorForward15(l, d, color);
               //  Ext_FinalColorForward16(l, d, color);
               //  Ext_FinalColorForward17(l, d, color);
               //  Ext_FinalColorForward18(l, d, color);
               //  Ext_FinalColorForward19(l, d, color);
               //  Ext_FinalColorForward20(l, d, color);
               //  Ext_FinalColorForward21(l, d, color);
               //  Ext_FinalColorForward22(l, d, color);
               //  Ext_FinalColorForward23(l, d, color);
               //  Ext_FinalColorForward24(l, d, color);
               //  Ext_FinalColorForward25(l, d, color);
               //  Ext_FinalColorForward26(l, d, color);
               //  Ext_FinalColorForward27(l, d, color);
               //  Ext_FinalColorForward28(l, d, color);
               //  Ext_FinalColorForward29(l, d, color);
            }

            void ChainFinalGBufferStandard(inout Surface s, inout ShaderData d, inout half4 GBuffer0, inout half4 GBuffer1, inout half4 GBuffer2, inout half4 outEmission, inout half4 outShadowMask)
            {
               //   Ext_FinalGBufferStandard0(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard1(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard2(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard3(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard4(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard5(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard6(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard7(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard8(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard9(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard10(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard11(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard12(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard13(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard14(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard15(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard16(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard17(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard18(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard19(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard20(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard21(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard22(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard23(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard24(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard25(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard26(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard27(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard28(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard29(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
            }



            

         ShaderData CreateShaderData(VertexToPixel i
                  #if NEED_FACING
                     , bool facing
                  #endif
         )
         {
            ShaderData d = (ShaderData)0;
            d.clipPos = i.pos;
            d.worldSpacePosition = i.worldPos;

            d.worldSpaceNormal = normalize(i.worldNormal);
            d.worldSpaceTangent = normalize(i.worldTangent.xyz);
            d.tangentSign = i.worldTangent.w;
            float3 bitangent = cross(i.worldTangent.xyz, i.worldNormal) * d.tangentSign * -1;
            

            d.TBNMatrix = float3x3(d.worldSpaceTangent, bitangent, d.worldSpaceNormal);
            d.worldSpaceViewDir = normalize(_WorldSpaceCameraPos - i.worldPos);

            d.tangentSpaceViewDir = mul(d.TBNMatrix, d.worldSpaceViewDir);
             d.texcoord0 = i.texcoord0;
             d.texcoord1 = i.texcoord1;
            // d.texcoord2 = i.texcoord2;

            // #if %TEXCOORD3REQUIREKEY%
            // d.texcoord3 = i.texcoord3;
            // #endif

            // d.isFrontFace = facing;
            // #if %VERTEXCOLORREQUIREKEY%
             d.vertexColor = i.vertexColor;
            // #endif

            // these rarely get used, so we back transform them. Usually will be stripped.
            #if _HDRP
                // d.localSpacePosition = mul(unity_WorldToObject, float4(GetCameraRelativePositionWS(i.worldPos), 1)).xyz;
            #else
                // d.localSpacePosition = mul(unity_WorldToObject, float4(i.worldPos, 1)).xyz;
            #endif
            // d.localSpaceNormal = normalize(mul((float3x3)unity_WorldToObject, i.worldNormal));
            // d.localSpaceTangent = normalize(mul((float3x3)unity_WorldToObject, i.worldTangent.xyz));

            // #if %SCREENPOSREQUIREKEY%
             d.screenPos = i.screenPos;
             d.screenUV = (i.screenPos.xy / i.screenPos.w);
            // #endif


            // #if %EXTRAV2F0REQUIREKEY%
             d.extraV2F0 = i.extraV2F0;
            // #endif

            // #if %EXTRAV2F1REQUIREKEY%
             d.extraV2F1 = i.extraV2F1;
            // #endif

            // #if %EXTRAV2F2REQUIREKEY%
             d.extraV2F2 = i.extraV2F2;
            // #endif

            // #if %EXTRAV2F3REQUIREKEY%
             d.extraV2F3 = i.extraV2F3;
            // #endif

            // #if %EXTRAV2F4REQUIREKEY%
             d.extraV2F4 = i.extraV2F4;
            // #endif

            // #if %EXTRAV2F5REQUIREKEY%
            // d.extraV2F5 = i.extraV2F5;
            // #endif

            // #if %EXTRAV2F6REQUIREKEY%
            // d.extraV2F6 = i.extraV2F6;
            // #endif

            // #if %EXTRAV2F7REQUIREKEY%
            // d.extraV2F7 = i.extraV2F7;
            // #endif

            return d;
         }
         

            
         #if defined(_PASSSHADOW)
            float3 _LightDirection;
            float3 _LightPosition;
         #endif

         // vertex shader
         VertexToPixel Vert (VertexData v)
         {
           
           VertexToPixel o = (VertexToPixel)0;

           UNITY_SETUP_INSTANCE_ID(v);
           UNITY_TRANSFER_INSTANCE_ID(v, o);
           UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);


#if !_TESSELLATION_ON
           ChainModifyVertex(v, o, _Time);
#endif

            o.texcoord0 = v.texcoord0;
            o.texcoord1 = v.texcoord1;
           // o.texcoord2 = v.texcoord2;

           // #if %TEXCOORD3REQUIREKEY%
           // o.texcoord3 = v.texcoord3;
           // #endif

           // #if %VERTEXCOLORREQUIREKEY%
            o.vertexColor = v.vertexColor;
           // #endif
           
           VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
           o.worldPos = TransformObjectToWorld(v.vertex.xyz);
           o.worldNormal = TransformObjectToWorldNormal(v.normal);
           o.worldTangent = float4(TransformObjectToWorldDir(v.tangent.xyz), v.tangent.w);

          // For some very odd reason, in 2021.2, we can't use Unity's defines, but have to use our own..
          #if _PASSSHADOW
              #if _CASTING_PUNCTUAL_LIGHT_SHADOW
                 float3 lightDirectionWS = normalize(_LightPosition - o.worldPos);
              #else
                 float3 lightDirectionWS = _LightDirection;
              #endif
              // Define shadow pass specific clip position for Universal
              o.pos = TransformWorldToHClip(ApplyShadowBias(o.worldPos, o.worldNormal, lightDirectionWS));
              #if UNITY_REVERSED_Z
                  o.pos.z = min(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #else
                  o.pos.z = max(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #endif
          #elif _PASSMETA
              o.pos = MetaVertexPosition(float4(v.vertex.xyz, 0), v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST);
          #else
              o.pos = TransformWorldToHClip(o.worldPos);
          #endif

          // #if %SCREENPOSREQUIREKEY%
           o.screenPos = ComputeScreenPos(o.pos, _ProjectionParams.x);
          // #endif

          #if _PASSFORWARD || _PASSGBUFFER
              float2 uv1 = v.texcoord1.xy;
              OUTPUT_LIGHTMAP_UV(uv1, unity_LightmapST, o.lightmapUV);
               o.texcoord1.xy = uv1;
              OUTPUT_SH(o.worldNormal, o.sh);
              #if defined(DYNAMICLIGHTMAP_ON)
                   o.dynamicLightmapUV.xy = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
              #endif
          #endif

          #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
              half fogFactor = 0;
              #if defined(_FOG_FRAGMENT)
                fogFactor = ComputeFogFactor(o.pos.z);
              #endif
              #if _BAKEDLIT
                 o.fogFactorAndVertexLight = half4(fogFactor, 0, 0, 0);
              #else
                 half3 vertexLight = VertexLighting(o.worldPos, o.worldNormal);
                 o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);
              #endif
          #endif

          #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
             o.shadowCoord = GetShadowCoord(vertexInput);
          #endif

           return o;
         }


            

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/UnityGBuffer.hlsl"

            // fragment shader
            FragmentOutput Frag (VertexToPixel IN
            #ifdef _DEPTHOFFSET_ON
              , out float outputDepth : SV_Depth
            #endif
            #if NEED_FACING
               , bool facing : SV_IsFrontFace
            #endif
            ) 
            {
               UNITY_SETUP_INSTANCE_ID(IN);
               UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

               ShaderData d = CreateShaderData(IN
                  #if NEED_FACING
                     , facing
                  #endif
               );
               Surface l = (Surface)0;

               #ifdef _DEPTHOFFSET_ON
                  l.outputDepth = outputDepth;
               #endif

               l.Albedo = half3(0.5, 0.5, 0.5);
               l.Normal = float3(0,0,1);
               l.Occlusion = 1;
               l.Alpha = 1;

               ChainSurfaceFunction(l, d);

               #ifdef _DEPTHOFFSET_ON
                  outputDepth = l.outputDepth;
               #endif

               #if _USESPECULAR || _SIMPLELIT
                  float3 specular = l.Specular;
                  float metallic = 0;
               #else   
                  float3 specular = 0;
                  float metallic = l.Metallic;
               #endif

               InputData inputData = (InputData)0;

               inputData.positionWS = IN.worldPos;
               #if _WORLDSPACENORMAL
                  inputData.normalWS = l.Normal;
               #else
                  inputData.normalWS = normalize(TangentToWorldSpace(d, l.Normal));
               #endif

               inputData.viewDirectionWS = SafeNormalize(d.worldSpaceViewDir);


               #if defined(MAIN_LIGHT_CALCULATE_SHADOWS)
                   inputData.shadowCoord = TransformWorldToShadowCoord(inputData.positionWS);
               #else
                   inputData.shadowCoord = float4(0, 0, 0, 0);
               #endif

               //inputData.fogCoord = IN.fogFactorAndVertexLight.x;
               InitializeInputDataFog(float4(IN.worldPos, 1.0), IN.fogFactorAndVertexLight.x);
               inputData.vertexLighting = IN.fogFactorAndVertexLight.yzw;


               #if defined(_OVERRIDE_BAKEDGI)
                  inputData.bakedGI = l.DiffuseGI;
                  l.Emission += l.SpecularGI;
               #else
                  #if defined(DYNAMICLIGHTMAP_ON)
                   inputData.bakedGI = SAMPLE_GI(IN.lightmapUV, IN.dynamicLightmapUV.xy, IN.sh, inputData.normalWS);
                  #else
                      inputData.bakedGI = SAMPLE_GI(IN.lightmapUV, IN.sh, inputData.normalWS);
                  #endif
               #endif

               inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(IN.pos);
               inputData.shadowMask = SAMPLE_SHADOWMASK(IN.lightmapUV);

               #if defined(DEBUG_DISPLAY)
                   #if defined(DYNAMICLIGHTMAP_ON)
                     inputData.dynamicLightmapUV = IN.dynamicLightmapUV.xy;
                   #endif
                   #if defined(LIGHTMAP_ON)
                     inputData.staticLightmapUV = IN.lightmapUV;
                   #else
                     inputData.vertexSH = IN.sh;
                   #endif
               #endif

               #ifdef _DBUFFER
                   ApplyDecal(IN.pos,
                       l.Albedo,
                       specular,
                       inputData.normalWS,
                       metallic,
                       l.Occlusion,
                       l.Smoothness);
               #endif

               BRDFData brdfData;
               InitializeBRDFData(l.Albedo, metallic, specular, l.Smoothness, l.Alpha, brdfData);
               Light mainLight = GetMainLight(inputData.shadowCoord, inputData.positionWS, inputData.shadowMask);
               MixRealtimeAndBakedGI(mainLight, inputData.normalWS, inputData.bakedGI, inputData.shadowMask);
               half3 color = GlobalIllumination(brdfData, inputData.bakedGI, l.Occlusion, inputData.positionWS, inputData.normalWS, inputData.viewDirectionWS);

               return BRDFDataToGbuffer(brdfData, inputData, l.Smoothness, l.Emission + color, l.Occlusion);
            }

         ENDHLSL

      }


      
        Pass
        {
            Name "ShadowCaster"
            Tags 
            { 
                "LightMode" = "ShadowCaster"
            }
           
            // Render State
            Blend One Zero, One Zero
            Cull Back
            ZTest LEqual
            ZWrite On
            // ColorMask: <None>

            	Cull [_Cull]


            HLSLPROGRAM

               #pragma vertex Vert
   #pragma fragment Frag

            #pragma target 3.0

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma multi_compile_instancing
  
            #pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW


            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define _PASSSHADOW 1

            
	#pragma multi_compile_local _LIGHTINGTYPE_PIXEL _LIGHTINGTYPE_VERTEX _LIGHTINGTYPE_TEXEL
	#pragma multi_compile_local _TEXTUREFILTERING_NEAREST _TEXTUREFILTERING_LINEAR _TEXTUREFILTERING_N64
	#pragma shader_feature_local _VERTEXJITTER
	#pragma shader_feature_local _VCOLSMOOTHNESS
	#pragma shader_feature_local _TOONSHADING
	#pragma shader_feature_local _EMISSION
	#pragma shader_feature_local _ALPHATEST_ON
	#pragma shader_feature_local _AFFINE_MAPPING
	#undef _UNLIT


   #define _URP 1

#define _UNLIT 1
#define _USINGTEXCOORD1 1

                 
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
            
                  #undef WorldNormalVector
      #define WorldNormalVector(data, normal) mul(normal, data.TBNMatrix)
      
      #define UnityObjectToWorldNormal(normal) mul(GetObjectToWorldMatrix(), normal)

      #define _WorldSpaceLightPos0 _MainLightPosition
      
      #define UNITY_DECLARE_TEX2D(name) TEXTURE2D(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2D_NOSAMPLER(name) TEXTURE2D(name);
      #define UNITY_DECLARE_TEX2DARRAY(name) TEXTURE2D_ARRAY(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(name) TEXTURE2D_ARRAY(name);

      #define UNITY_SAMPLE_TEX2DARRAY(tex,coord)            SAMPLE_TEXTURE2D_ARRAY(tex, sampler##tex, coord.xy, coord.z)
      #define UNITY_SAMPLE_TEX2DARRAY_LOD(tex,coord,lod)    SAMPLE_TEXTURE2D_ARRAY_LOD(tex, sampler##tex, coord.xy, coord.z, lod)
      #define UNITY_SAMPLE_TEX2D(tex, coord)                SAMPLE_TEXTURE2D(tex, sampler##tex, coord)
      #define UNITY_SAMPLE_TEX2D_SAMPLER(tex, samp, coord)  SAMPLE_TEXTURE2D(tex, sampler##samp, coord)

      #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod)   SAMPLE_TEXTURE2D_LOD(tex, sampler_##tex, coord, lod)
      #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) SAMPLE_TEXTURE2D_LOD (tex, sampler##samplertex,coord, lod)
     
      #if defined(UNITY_COMPILER_HLSL)
         #define UNITY_INITIALIZE_OUTPUT(type,name) name = (type)0;
      #else
         #define UNITY_INITIALIZE_OUTPUT(type,name)
      #endif

      #define sampler2D_float sampler2D
      #define sampler2D_half sampler2D

      

      // data across stages, stripped like the above.
      struct VertexToPixel
      {
         float4 pos : SV_POSITION;
         float3 worldPos : TEXCOORD0;
         float3 worldNormal : TEXCOORD1;
         float4 worldTangent : TEXCOORD2;
          float4 texcoord0 : TEXCOORD3;
          float4 texcoord1 : TEXCOORD4;
         // float4 texcoord2 : TEXCOORD5;

         // #if %TEXCOORD3REQUIREKEY%
         // float4 texcoord3 : TEXCOORD6;
         // #endif

         // #if %SCREENPOSREQUIREKEY%
          float4 screenPos : TEXCOORD7;
         // #endif

         // #if %VERTEXCOLORREQUIREKEY%
          half4 vertexColor : COLOR;
         // #endif

         #if defined(LIGHTMAP_ON)
            float2 lightmapUV : TEXCOORD8;
         #endif
         #if defined(DYNAMICLIGHTMAP_ON)
            float2 dynamicLightmapUV : TEXCOORD9;
         #endif
         #if !defined(LIGHTMAP_ON)
            float3 sh : TEXCOORD10;
         #endif

         #if defined(VARYINGS_NEED_FOG_AND_VERTEX_LIGHT)
            float4 fogFactorAndVertexLight : TEXCOORD11;
         #endif

         #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
           float4 shadowCoord : TEXCOORD12;
         #endif

         // #if %EXTRAV2F0REQUIREKEY%
          float4 extraV2F0 : TEXCOORD13;
         // #endif

         // #if %EXTRAV2F1REQUIREKEY%
          float4 extraV2F1 : TEXCOORD14;
         // #endif

         // #if %EXTRAV2F2REQUIREKEY%
          float4 extraV2F2 : TEXCOORD15;
         // #endif

         // #if %EXTRAV2F3REQUIREKEY%
          float4 extraV2F3 : TEXCOORD16;
         // #endif

         // #if %EXTRAV2F4REQUIREKEY%
          float4 extraV2F4 : TEXCOORD17;
         // #endif

         // #if %EXTRAV2F5REQUIREKEY%
         // float4 extraV2F5 : TEXCOORD18;
         // #endif

         // #if %EXTRAV2F6REQUIREKEY%
         // float4 extraV2F6 : TEXCOORD19;
         // #endif

         // #if %EXTRAV2F7REQUIREKEY%
         // float4 extraV2F7 : TEXCOORD20;
         // #endif

         #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
         #endif
         #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
         #endif
         #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
         #endif
         #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
         #endif
      };

         
            
            
            // data describing the user output of a pixel
            struct Surface
            {
               half3 Albedo;
               half Height;
               half3 Normal;
               half Smoothness;
               half3 Emission;
               half Metallic;
               half3 Specular;
               half Occlusion;
               half SpecularPower; // for simple lighting
               half Alpha;
               float outputDepth; // if written, SV_Depth semantic is used. ShaderData.clipPos.z is unused value
               // HDRP Only
               half SpecularOcclusion;
               half SubsurfaceMask;
               half Thickness;
               half CoatMask;
               half CoatSmoothness;
               half Anisotropy;
               half IridescenceMask;
               half IridescenceThickness;
               int DiffusionProfileHash;
               float SpecularAAThreshold;
               float SpecularAAScreenSpaceVariance;
               // requires _OVERRIDE_BAKEDGI to be defined, but is mapped in all pipelines
               float3 DiffuseGI;
               float3 BackDiffuseGI;
               float3 SpecularGI;
               // requires _OVERRIDE_SHADOWMASK to be defines
               float4 ShadowMask;
            };

            // Data the user declares in blackboard blocks
            struct Blackboard
            {
                
                float blackboardDummyData;
            };

            // data the user might need, this will grow to be big. But easy to strip
            struct ShaderData
            {
               float4 clipPos; // SV_POSITION
               float3 localSpacePosition;
               float3 localSpaceNormal;
               float3 localSpaceTangent;
        
               float3 worldSpacePosition;
               float3 worldSpaceNormal;
               float3 worldSpaceTangent;
               float tangentSign;

               float3 worldSpaceViewDir;
               float3 tangentSpaceViewDir;

               float4 texcoord0;
               float4 texcoord1;
               float4 texcoord2;
               float4 texcoord3;

               float2 screenUV;
               float4 screenPos;

               float4 vertexColor;
               bool isFrontFace;

               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;

               float3x3 TBNMatrix;
               Blackboard blackboard;
            };

            struct VertexData
            {
               #if SHADER_TARGET > 30
               // uint vertexID : SV_VertexID;
               #endif
               float4 vertex : POSITION;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;

               // optimize out mesh coords when not in use by user or lighting system
               #if _URP && (_USINGTEXCOORD1 || _PASSMETA || _PASSFORWARD || _PASSGBUFFER)
                  float4 texcoord1 : TEXCOORD1;
               #endif

               #if _URP && (_USINGTEXCOORD2 || _PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && defined(DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif

               #if _STANDARD && (_USINGTEXCOORD1 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER || _PASSFORWARDADD) && LIGHTMAP_ON)))
                  float4 texcoord1 : TEXCOORD1;
               #endif
               #if _STANDARD && (_USINGTEXCOORD2 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif


               #if _HDRP
                  float4 texcoord1 : TEXCOORD1;
                  float4 texcoord2 : TEXCOORD2;
               #endif

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
                float4 vertexColor : COLOR;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD4; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity    : TEXCOORD5; // Add Precomputed Velocity (Alembic computes velocities on runtime side).
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct TessVertex 
            {
               float4 vertex : INTERNALTESSPOS;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;
               float4 texcoord1 : TEXCOORD1;
               float4 texcoord2 : TEXCOORD2;

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
                float4 vertexColor : COLOR;
               // #endif

               // #if %EXTRAV2F0REQUIREKEY%
                float4 extraV2F0 : TEXCOORD5;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
                float4 extraV2F1 : TEXCOORD6;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
                float4 extraV2F2 : TEXCOORD7;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
                float4 extraV2F3 : TEXCOORD8;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
                float4 extraV2F4 : TEXCOORD9;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // float4 extraV2F5 : TEXCOORD10;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // float4 extraV2F6 : TEXCOORD11;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // float4 extraV2F7 : TEXCOORD12;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD13; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity : TEXCOORD14;
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
               UNITY_VERTEX_OUTPUT_STEREO
            };

            struct ExtraV2F
            {
               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;
               Blackboard blackboard;
               float4 time;
            };


            float3 WorldToTangentSpace(ShaderData d, float3 normal)
            {
               return mul(d.TBNMatrix, normal);
            }

            float3 TangentToWorldSpace(ShaderData d, float3 normal)
            {
               return mul(normal, d.TBNMatrix);
            }

            // in this case, make standard more like SRPs, because we can't fix
            // unity_WorldToObject in HDRP, since it already does macro-fu there

            #if _STANDARD
               float3 TransformWorldToObject(float3 p) { return mul(unity_WorldToObject, float4(p, 1)); };
               float3 TransformObjectToWorld(float3 p) { return mul(unity_ObjectToWorld, float4(p, 1)); };
               float4 TransformWorldToObject(float4 p) { return mul(unity_WorldToObject, p); };
               float4 TransformObjectToWorld(float4 p) { return mul(unity_ObjectToWorld, p); };
               float4x4 GetWorldToObjectMatrix() { return unity_WorldToObject; }
               float4x4 GetObjectToWorldMatrix() { return unity_ObjectToWorld; }
               #if (defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (SHADER_TARGET_SURFACE_ANALYSIS && !SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod) tex.SampleLevel (sampler##tex,coord, lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) tex.SampleLevel (sampler##samplertex,coord, lod)
              #else
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord,lod) tex2D (tex,coord,0,lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord,lod) tex2D (tex,coord,0,lod)
              #endif

               #undef GetWorldToObjectMatrix()

               #define GetWorldToObjectMatrix()   unity_WorldToObject


            #endif

            float3 GetCameraWorldPosition()
            {
               #if _HDRP
                  return GetCameraRelativePositionWS(_WorldSpaceCameraPos);
               #else
                  return _WorldSpaceCameraPos;
               #endif
            }

            #if _GRABPASSUSED
               #if _STANDARD
                  TEXTURE2D(%GRABTEXTURE%);
                  SAMPLER(sampler_%GRABTEXTURE%);
               #endif

               half3 GetSceneColor(float2 uv)
               {
                  #if _STANDARD
                     return SAMPLE_TEXTURE2D(%GRABTEXTURE%, sampler_%GRABTEXTURE%, uv).rgb;
                  #else
                     return SHADERGRAPH_SAMPLE_SCENE_COLOR(uv);
                  #endif
               }
            #endif


      
            #if _STANDARD
               UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
               float GetSceneDepth(float2 uv) { return SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv)); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv)); } 
            #else
               float GetSceneDepth(float2 uv) { return SHADERGRAPH_SAMPLE_SCENE_DEPTH(uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv), _ZBufferParams); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv), _ZBufferParams); } 
            #endif

            float3 GetWorldPositionFromDepthBuffer(float2 uv, float3 worldSpaceViewDir)
            {
               float eye = GetLinearEyeDepth(uv);
               float3 camView = mul((float3x3)GetObjectToWorldMatrix(), transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V)) [2].xyz);

               float dt = dot(worldSpaceViewDir, camView);
               float3 div = worldSpaceViewDir/dt;
               float3 wpos = (eye * div) + GetCameraWorldPosition();
               return wpos;
            }

            #if _HDRP
            float3 ObjectToWorldSpacePosition(float3 pos)
            {
               return GetAbsolutePositionWS(TransformObjectToWorld(pos));
            }
            #else
            float3 ObjectToWorldSpacePosition(float3 pos)
            {
               return TransformObjectToWorld(pos);
            }
            #endif

            #if _STANDARD
               UNITY_DECLARE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture);
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  float4 depthNorms = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture, uv);
                  float3 norms = DecodeViewNormalStereo(depthNorms);
                  norms = mul((float3x3)GetWorldToViewMatrix(), norms) * 0.5 + 0.5;
                  return norms;
               }
            #elif _HDRP
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  NormalData nd;
                  DecodeFromNormalBuffer(_ScreenSize.xy * uv, nd);
                  return nd.normalWS;
               }
            #elif _URP
               #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                  #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareNormalsTexture.hlsl"
               #endif

               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                     return SampleSceneNormals(uv);
                  #else
                     float3 wpos = GetWorldPositionFromDepthBuffer(uv, worldSpaceViewDir);
                     return normalize(-cross(ddx(wpos), ddy(wpos))) * 0.5 + 0.5;
                  #endif

                }
             #endif

             #if _HDRP

               half3 UnpackNormalmapRGorAG(half4 packednormal)
               {
                     // This do the trick
                  packednormal.x *= packednormal.w;

                  half3 normal;
                  normal.xy = packednormal.xy * 2 - 1;
                  normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                  return normal;
               }
               half3 UnpackNormal(half4 packednormal)
               {
                  #if defined(UNITY_NO_DXT5nm)
                     return packednormal.xyz * 2 - 1;
                  #else
                     return UnpackNormalmapRGorAG(packednormal);
                  #endif
               }
               #endif
               #if _HDRP || _URP

               half3 UnpackScaleNormal(half4 packednormal, half scale)
               {
                 #ifndef UNITY_NO_DXT5nm
                   // Unpack normal as DXT5nm (1, y, 1, x) or BC5 (x, y, 0, 1)
                   // Note neutral texture like "bump" is (0, 0, 1, 1) to work with both plain RGB normal and DXT5nm/BC5
                   packednormal.x *= packednormal.w;
                 #endif
                   half3 normal;
                   normal.xy = (packednormal.xy * 2 - 1) * scale;
                   normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                   return normal;
               }	

             #endif


            void GetSun(out float3 lightDir, out float3 color)
            {
               lightDir = float3(0.5, 0.5, 0);
               color = 1;
               #if _HDRP
                  if (_DirectionalLightCount > 0)
                  {
                     DirectionalLightData light = _DirectionalLightDatas[0];
                     lightDir = -light.forward.xyz;
                     color = light.color;
                  }
               #elif _STANDARD
			         lightDir = normalize(_WorldSpaceLightPos0.xyz);
                  color = _LightColor0.rgb;
               #elif _URP
	               Light light = GetMainLight();
	               lightDir = light.direction;
	               color = light.color;
               #endif
            }


            
            CBUFFER_START(UnityPerMaterial)

               

	int _VColEmissive;
	float _VertexEmissiveStr;
	half4 _Color;
	int _HalfLambert;
	int _EnableMaskMap;
	int _ForceNearest;
	int _TextureFiltering;
	int _SpecularType;
	int _Reflections;
	float _NormalStrength;
	float2 _SmoothRemap;
	float2 _SmoothRemapVCol;
	float _VertexLightSmoothness;
	float3 _VertexSpecColor;
	float3 _EmissionColor;
	float4 _TilingOffset;
	float _Cutoff;
	float _AffineMapFactor;
	int _VertexJitterResX;
	int _VertexJitterResY;
	int _DoubleSizeDither;
	int _MaskTint;
	float _LightmapEmissionStr;
	
	int _DitherAmbient;
	int _DitherDiffuse;
	int _DitherSpec;

	float4 _BaseMap_TexelSize;
	float4 _MaskMap_TexelSize;
	float4 _BumpMap_TexelSize;
	float4 _EmissionMap_TexelSize;




            CBUFFER_END

            

            

            #ifdef unity_WorldToObject
#undef unity_WorldToObject
#endif
#ifdef unity_ObjectToWorld
#undef unity_ObjectToWorld
#endif
#define unity_ObjectToWorld GetObjectToWorldMatrix()
#define unity_WorldToObject GetWorldToObjectMatrix()


	/**********
	* TODO:
	* Vertex Colors
	* Meta Pass?
	**********/

	TEXTURE2D(_ToonRamp);
	TEXTURE2D(_SpecularRamp);
	SamplerState sampler_linear_clamp;

	TEXTURE2D(_BaseMap);
	SamplerState sampler_linear_repeat;
	SamplerState sampler_trilinear_repeat;
	SamplerState sampler_point_repeat;

	#if defined(_TEXTUREFILTERING_LINEAR) || defined(PASS_META)
		#define MAINSMP sampler_linear_repeat
	#else
		#define MAINSMP sampler_point_repeat
	#endif

	static const uint bayerPattern[] =
	{
		0, 32,  8, 40,  2, 34, 10, 42,   /* 8x8 Bayer ordered dithering  */
		48, 16, 56, 24, 50, 18, 58, 26,  /* pattern.  Each input pixel   */
		12, 44,  4, 36, 14, 46,  6, 38,  /* is scaled to the 0..63 range */
		60, 28, 52, 20, 62, 30, 54, 22,  /* before looking in this table */
		3, 35, 11, 43,  1, 33,  9, 41,   /* to determine the action.     */
		51, 19, 59, 27, 49, 17, 57, 25,
		15, 47,  7, 39, 13, 45,  5, 37,
		63, 31, 55, 23, 61, 29, 53, 21
	};

	float ditherValue(float2 screenPos)
	{
		uint stippleOffset = ((uint) screenPos.y % 8) * 8 + ((uint) screenPos.x % 8);
		uint byte = bayerPattern[stippleOffset];
		return byte / 64.0f;
	}

	float GetLuminance(float3 col)
	{
		return dot(col, float3(0.299f, 0.587f, 0.114f));
	}

	void ditherColor(inout float3 input, float2 screenPos)
	{
		input.x = input.x < ditherValue(screenPos) ? input.x*0.25 : input.x;
		input.y = input.y < ditherValue(screenPos) ? input.y*0.25 : input.y;
		input.z = input.z < ditherValue(screenPos) ? input.z*0.25 : input.z;
	}

	// This function has been basically ripped straight from https://forum.unity.com/threads/the-quest-for-efficient-per-texel-lighting.529948/
	// So big thanks to GreatestBear, and truepak182, without whom this shader wouldn't be nearly as cool
	float3 CalculateTexelWorldPos(float2 originalUV, float3 originalWorldPos, float4 texelSize)
	{
		// 1.) Calculate how much the texture UV coords need to
		//     shift to be at the center of the nearest texel.
		float2 centerUV = floor(originalUV * (texelSize.zw))/texelSize.zw + (texelSize.xy/2.0);
		float2 dUV = (centerUV - originalUV);
	
		// 2b.) Calculate how much the texture coords vary over fragment space.
		//      This essentially defines a 2x2 matrix that gets
		//      texture space (UV) deltas from fragment space (ST) deltas
		// Note: I call fragment space (S,T) to disambiguate.
		float2 dUVdS = ddx(originalUV);
		float2 dUVdT = ddy(originalUV);
	
		// 2c.) Invert the fragment from texture matrix
		float2x2 dSTdUV = float2x2(dUVdT[1], -dUVdT[0], -dUVdS[1], dUVdS[0])*(1.0f/(dUVdS[0]*dUVdT[1]-dUVdT[0]*dUVdS[1]));
	
	
		// 2d.) Convert the UV delta to a fragment space delta
		float2 dST = mul(dSTdUV , dUV);
	
		// 2e.) Calculate how much the world coords vary over fragment space.
		float3 dXYZdS = ddx(originalWorldPos);
		float3 dXYZdT = ddy(originalWorldPos);
	
		// 2f.) Finally, convert our fragment space delta to a world space delta
		// And be sure to clamp it to SOMETHING in case the derivative calc went insane
		// Here I clamp it to -1 to 1 unit in unity, which should be orders of magnitude greater
		// than the size of any texel.
		float3 dXYZ = clamp(dXYZdS * dST[0] + dXYZdT * dST[1], -1, 1);
	
		// 3.) Transform the snapped UV back to world space
		return originalWorldPos + dXYZ;
	}

	float3 CalculateTexelWorldViewDir(float3 viewDirectionWS, float3 positionWS)
	{
		//Recalculate view direction according to recalculated texelized inputData.positionWS
		float3 viewDirectionWSCorrected = GetCameraPositionWS() - positionWS;
		//This needs to be done to make corrected viewDirectionWSCorrected same length as the original. If omitted, specular will not be displayed properly
		float viewPositionMult = length(viewDirectionWS) / length(viewDirectionWSCorrected);

		return viewDirectionWSCorrected * viewPositionMult;
	}

	float2 SnapUVToTexelCenter(float2 originalUV, float4 texelSize)
	{
		float2 centerUV = floor(originalUV * (texelSize.zw))/texelSize.zw + (texelSize.xy/2.0);
		float2 dUV = (centerUV - originalUV);

		return originalUV + dUV;
	}


	float3 SampleRamp(Texture2D ramp, float coord)
	{
		return SAMPLE_TEXTURE2D_LOD(ramp, sampler_linear_clamp, saturate(float2(pow(coord,0.5), 0.5)), 0).xyz;
	}

	float invLerp(float from, float to, float value){
		return (value - from) / (to - from);
	}

	float remap(float origFrom, float origTo, float targetFrom, float targetTo, float value){
		float rel = invLerp(origFrom, origTo, value);
		return lerp(targetFrom, targetTo, rel);
	}

	float remap01(float targetFrom, float targetTo, float value){
		float rel = invLerp(0, 1, value);
		return lerp(targetFrom, targetTo, rel);

	}

	void MainLight(out float3 direction, out float3 color, out float distanceAtten)
	{
		Light mainLight = GetMainLight();
		direction = mainLight.direction;
		color = mainLight.color;
		distanceAtten = mainLight.distanceAttenuation;
	}

	float MainLightShadows(float3 worldPos, float2 lightmapUV = float2(0.0,0.0), float2 origUV = float2(0.0,0.0))
	{
		float4 shadowCoord = TransformWorldToShadowCoord(worldPos);

		#if !defined (LIGHTMAP_ON) || defined(_LIGHTINGTYPE_VERTEX)
			half4 shadowMask = unity_ProbesOcclusion;
		#else
			lightmapUV = lightmapUV * unity_LightmapST.xy + unity_LightmapST.zw;

			#ifdef _LIGHTINGTYPE_TEXEL
				lightmapUV = CalculateTexelWorldPos(origUV, float3(lightmapUV.x, lightmapUV.y, 1), _BaseMap_TexelSize).xy;
			#endif

			half4 shadowMask = SAMPLE_SHADOWMASK(lightmapUV);
		#endif

		return MainLightShadow(shadowCoord, worldPos, shadowMask, _MainLightOcclusionProbes);
	}

	float3 GetAmbient(float3 worldNormal, float2 lightmapUV, float2 origUV = float2(0.0f, 0.0f))
	{
		#ifdef LIGHTMAP_ON
			lightmapUV = lightmapUV * unity_LightmapST.xy + unity_LightmapST.zw;

			#ifdef _LIGHTINGTYPE_TEXEL
				lightmapUV = CalculateTexelWorldPos(origUV, float3(lightmapUV.x, lightmapUV.y, 1), _BaseMap_TexelSize).xy;
			#endif

			#ifdef _LIGHTINGTYPE_VERTEX
				float4 lightmap = SAMPLE_TEXTURE2D_LOD(unity_Lightmap, sampler_linear_repeat, lightmapUV, 0);
				half3 bakedGI = DecodeLightmap(lightmap,half4(LIGHTMAP_HDR_MULTIPLIER, LIGHTMAP_HDR_EXPONENT, 0.0h, 0.0h));
			#else
				half3 bakedGI = SampleLightmap(lightmapUV, worldNormal);//DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, lightmapUV),half4(LIGHTMAP_HDR_MULTIPLIER, LIGHTMAP_HDR_EXPONENT, 0.0h, 0.0h));
			#endif

			#if defined(_MIXED_LIGHTING_SUBTRACTIVE)
				Light mainLight = GetMainLight();
				bakedGI = SubtractDirectMainLightFromLightmap(mainLight, worldNormal, bakedGI);
			#endif

			return bakedGI;
		#else
			return SampleSH(worldNormal);
		#endif
	}

	float GetSpecular(float3 lightDir, float3 worldNormal, float3 viewDir, float smoothness)
	{
		float SpecSmoothness = exp2(10 * smoothness + 1).x;
		[branch]
		if (_SpecularType == 0) //Cook-Torrance
		{
			float roughness = 1 - smoothness;
			roughness *= roughness;

			float3 h = SafeNormalize(lightDir + viewDir);
			float nh = saturate(dot(worldNormal, h));
			float lh = saturate(dot(lightDir, h));
			float d = nh * nh * (roughness * roughness - 1.0) + 1.00001;
			float normTerm = roughness * 4.0 + 2.0;
			float specTerm = roughness * roughness;
			specTerm /= (d * d) * max(0.1, lh * lh) * normTerm;
			return specTerm*0.04;
		}
		else if (_SpecularType == 1) //Phong
		{
			float3 r = reflect(-lightDir, worldNormal);
			float rdotv = max(0, dot(r, viewDir));

			return pow(rdotv, SpecSmoothness)*smoothness;
		}
		else if (_SpecularType == 2) //Blinn-Phong
		{
			float3 h = SafeNormalize(lightDir+viewDir);
			float ndh = saturate(dot(worldNormal, h));

			return pow(ndh, SpecSmoothness)*smoothness;
		}
		else
		{
			return 0;
		}
	}

	float GetDiffuse(float3 lightDir, float3 worldNormal)
	{
		float diffuse = dot(lightDir, worldNormal);

		if (_HalfLambert)
			diffuse = diffuse * 0.5 + 0.5;

		return max(0,diffuse);
	}


	void AdditionalLights(float3 worldPos, float3 viewDir, float3 worldNormal, float InSmoothness, float3 specColor, out float3 Diffuse, out float3 Specular, float dirOcclusion = 1)
	{
		float3 diff = 0;
		float3 spec = 0;

		#if !defined (LIGHTMAP_ON)
			half4 shadowMask = unity_ProbesOcclusion;
		#else
			half4 shadowMask = half4(1, 1, 1, 1);
		#endif

		int pixelLights = GetAdditionalLightsCount();
		for (int i = 0; i < pixelLights; ++i)
		{
			Light light = GetAdditionalLight(i, worldPos, shadowMask);
			
			#ifdef _TOONSHADING
				float diffMask = GetDiffuse(light.direction, worldNormal) * light.shadowAttenuation * light.distanceAttenuation * dirOcclusion;
				diff += SampleRamp(_ToonRamp, diffMask) * light.color;
			#else
				diff += GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.color * light.shadowAttenuation * dirOcclusion;
			#endif

			#ifdef _TOONSHADING
				float specMask = GetSpecular(light.direction, worldNormal, viewDir, InSmoothness) * light.distanceAttenuation * light.shadowAttenuation * dirOcclusion;
				spec += SampleRamp(_SpecularRamp, specMask) * light.color * specColor;
			#else
				spec += GetSpecular(light.direction, worldNormal, viewDir, InSmoothness) * light.distanceAttenuation * light.color * specColor * light.shadowAttenuation * dirOcclusion;
			#endif
		}
		Diffuse = diff;
		Specular = spec;
	}


	float3 GetReflections(float3 worldNormal, float3 viewDir, float smoothness)
	{
		float perceptualRoughness = 1 - smoothness;
		perceptualRoughness *= perceptualRoughness;
		float mip = PerceptualRoughnessToMipmapLevel(perceptualRoughness);
		float3 sample = SHADERGRAPH_REFLECTION_PROBE(viewDir, worldNormal, mip);
		return sample.rgb;
	}

	void SSAO(ShaderData d, out float directOcclusion, out float indirectOcclusion)
	{
		#if defined(_SCREEN_SPACE_OCCLUSION) && !defined(RETROSHADER_ALPHA)
			float2 uv = d.screenUV;

			#ifdef _LIGHTINGTYPE_TEXEL
				float3 pos = CalculateTexelWorldPos(d.texcoord0, float3(uv.x, uv.y, 1.0), _BaseMap_TexelSize);
				uv = pos.xy;
			#endif

			AmbientOcclusionFactor aoFactor = GetScreenSpaceAmbientOcclusion(uv);
			directOcclusion = aoFactor.directAmbientOcclusion;
			indirectOcclusion = aoFactor.indirectAmbientOcclusion;
		#else
			directOcclusion = 1;
			indirectOcclusion = 1;
		#endif
	}

	// Courtesy of Ben Golus
	// https://forum.unity.com/threads/shader-ideas-for-replicating-n64-bi-linear-filtering.936119/#post-6116207
	half4 sampleN64(Texture2D tex, float2 uv, float4 texelSize)
	{
		// texel coordinates
		float2 texels = uv * texelSize.zw;

		// calculate mip level
		float2 dx = ddx(texels);
		float2 dy = ddy(texels);
		float delta_max_sqr = max(dot(dx, dx), dot(dy, dy));
		float mip = max(0.0, 0.5 * log2(delta_max_sqr));

		// scale texel sizes and texel coordinates to handle mip levels properly
		float scale = pow(2,floor(mip));
		texelSize.xy *= scale;
		texelSize.zw /= scale;
		texels = texels / scale - 0.5;

		// calculate blend for the three points of the tri-filter
		float2 fracTexels = frac(texels);
		float3 blend = float3(
			abs(fracTexels.x+fracTexels.y-1),
			min(abs(fracTexels.xx-float2(0,1)), abs(fracTexels.yy-float2(1,0)))
		);

		// calculate equivalents of point filtered uvs for the three points
		float2 uvA = (floor(texels + fracTexels.yx) + 0.5) * texelSize.xy;
		float2 uvB = (floor(texels) + float2(1.5, 0.5)) * texelSize.xy;
		float2 uvC = (floor(texels) + float2(0.5, 1.5)) * texelSize.xy;

		// sample points
		half4 A = SAMPLE_TEXTURE2D_LOD(tex, MAINSMP, uvA, mip);
		half4 B = SAMPLE_TEXTURE2D_LOD(tex, MAINSMP, uvB, mip);
		half4 C = SAMPLE_TEXTURE2D_LOD(tex, MAINSMP, uvC, mip);

		// blend and return
		return A * blend.x + B * blend.y + C * blend.z;
	}

	half4 sampleTex(Texture2D tex, float2 uv, float4 texelSize)
	{
		#ifdef _TEXTUREFILTERING_N64
			return sampleN64(tex, uv, texelSize);
		#else
			return SAMPLE_TEXTURE2D(tex, MAINSMP, uv);
		#endif
	}

	TEXTURE2D(_MaskMap);
	TEXTURE2D(_BumpMap);
	TEXTURE2D(_EmissionMap);

	void Ext_ModifyVertex0 (inout VertexData v, inout ExtraV2F d)
	{
		float4 clipPos = mul(GetWorldToHClipMatrix(), mul(unity_ObjectToWorld, v.vertex));

		

		#if defined(_LIGHTINGTYPE_VERTEX) && !defined(PASS_META)

			float smoothness = _VertexLightSmoothness;

			#ifdef _VCOLSMOOTHNESS
				smoothness = remap01(_SmoothRemapVCol.x, _SmoothRemapVCol.y, v.vertexColor.a);
			#endif

			float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
			float3 worldNormal = SafeNormalize(mul(unity_ObjectToWorld, v.normal));
			float3 viewDir = normalize(_WorldSpaceCameraPos - worldPos);
			float3 mainLightDir;
			float3 mainLightCol;
			float mainLightAtten;
			MainLight(mainLightDir, mainLightCol, mainLightAtten);

			d.extraV2F0.y = GetDiffuse(mainLightDir, worldNormal) * mainLightAtten;
			d.extraV2F1.xyz = GetAmbient(worldNormal, v.texcoord1);
			d.extraV2F0.z = MainLightShadows(worldPos, v.texcoord1);
			d.extraV2F0.w = GetSpecular(mainLightDir, worldNormal, viewDir, smoothness) * mainLightAtten * d.extraV2F0.z;

			#if !defined (LIGHTMAP_ON)
				half4 shadowMask = unity_ProbesOcclusion;
			#else
				half4 shadowMask = half4(1, 1, 1, 1);
			#endif

			// Have to store each light in a separate vector component so we can apply toon shading in fragment later - toon shading has to be applied per light to make sense
			// This limits us to max 4 lights, would need to use 2 more interpolators for 8
			int pixelLights = GetAdditionalLightsCount();
			Light light;
			if (pixelLights >= 1)
			{
				light = GetAdditionalLight(0, worldPos, shadowMask);
				d.extraV2F3.x = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.x = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
			if (pixelLights >= 2)
			{
				light = GetAdditionalLight(1, worldPos, shadowMask);
				d.extraV2F3.y = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.y = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
			if (pixelLights >= 3)
			{
				light = GetAdditionalLight(2, worldPos, shadowMask);
				d.extraV2F3.z = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.z = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
			if (pixelLights >= 4)
			{
				light = GetAdditionalLight(3, worldPos, shadowMask);
				d.extraV2F3.w = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.w = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
		#endif

		#if defined(_VERTEXJITTER) && !defined(_PASSSHADOW)
			float4 vert = clipPos;
			d.extraV2F2.xyz = mul(UNITY_MATRIX_I_VP, vert);
			vert.xyz = clipPos.xyz/clipPos.w;
			vert.x = floor(_VertexJitterResX * vert.x) / _VertexJitterResX;
			vert.y = floor(_VertexJitterResY * vert.y) / _VertexJitterResY;
			vert.xyz *= clipPos.w;
			v.vertex = mul(unity_WorldToObject, mul(UNITY_MATRIX_I_VP, vert));
		#endif

		d.extraV2F0.x = ComputeFogFactor(clipPos.z);
		v.texcoord0.xy = v.texcoord0.xy * _TilingOffset.xy + _TilingOffset.zw;

		#if defined(_AFFINE_MAPPING)
			_AffineMapFactor = min(1, _AffineMapFactor);
			float warpFactor = max(clipPos.w, 0.02); //Prevents flipping
			warpFactor = lerp(0.5, warpFactor, _AffineMapFactor);

			v.texcoord0.xy *= warpFactor;
			d.extraV2F1.w = warpFactor;
		#endif
	}

	
	void Ext_SurfaceFunction0 (inout Surface o, ShaderData d)
	{
		#if defined(_AFFINE_MAPPING)
			d.texcoord0.xy /= d.extraV2F1.w;
		#endif
		// Set up input data
		float4 mainTex = sampleTex(_BaseMap, d.texcoord0.xy, _BaseMap_TexelSize);
		o.Albedo = mainTex.rgb;
		float4 maskMap = sampleTex(_MaskMap, d.texcoord0.xy, _BaseMap_TexelSize);

		

		if (_MaskTint)
		{
			float3 tintedAlbedo = o.Albedo * _Color.rgb;
			o.Albedo = lerp(o.Albedo, tintedAlbedo, maskMap.a);
		}
		else
		{
			o.Albedo *= _Color.rgb;
		}

		#ifdef _EMISSION
			float3 emission = sampleTex(_EmissionMap, d.texcoord0.xy, _EmissionMap_TexelSize) * _EmissionColor;
			o.Emission = emission * _LightmapEmissionStr;

		#endif

		float2 pixelPos = d.clipPos.xy;//floor(d.screenUV.xy * _ScreenParams.xy);

		#if defined(_LIGHTINGTYPE_TEXEL) && !defined(PASS_META)
			o.Normal = UnpackScaleNormal(SAMPLE_TEXTURE2D(_BumpMap, sampler_linear_repeat, d.texcoord0.xy), _NormalStrength);
		#else
			o.Normal = UnpackScaleNormal(sampleTex(_BumpMap, d.texcoord0.xy, _BumpMap_TexelSize), _NormalStrength);
		#endif

		//o.Normal = SafeNormalize(o.Normal);

		#if defined(_VERTEXJITTER) && !defined(_PASSSHADOW)
			d.worldSpacePosition = d.extraV2F2.xyz;
		#endif

		d.worldSpaceNormal = TransformTangentToWorld(o.Normal, d.TBNMatrix);
		float3 unSnappedViewDir = d.worldSpaceViewDir;
		// Snap world position, view direction, and normal to the texel
		#if defined(_LIGHTINGTYPE_TEXEL) && !defined(PASS_META)
			d.worldSpacePosition = CalculateTexelWorldPos(d.texcoord0.xy, d.worldSpacePosition, _BaseMap_TexelSize);
			d.worldSpaceViewDir = CalculateTexelWorldViewDir(d.worldSpaceViewDir, d.worldSpacePosition);
			d.worldSpaceNormal = SafeNormalize(CalculateTexelWorldPos(d.texcoord0.xy, d.worldSpaceNormal, _BaseMap_TexelSize));
		#endif

		if (!_EnableMaskMap)
		{
			maskMap.r = 0;
			maskMap.g = mainTex.a;
			maskMap.b = 1;
		}

		o.Metallic = maskMap.r;
		o.Smoothness = remap01(_SmoothRemap.x, _SmoothRemap.y, maskMap.g);
		o.Occlusion =  maskMap.b;

		// Store lighting values in these
		float3 mainLightDir;
		float3 mainLightCol;
		float mainLightAtten;
		MainLight(mainLightDir, mainLightCol, mainLightAtten);

		float3 mainLightDiffuse;
		float mainShadow = 1;
		float3 ambient;
		float3 mainSpec = 0;

		float3 addDiffuse = 0;
		float3 addSpec = 0;

		float3 specColor = lerp(1.0, o.Albedo, o.Metallic);
		float reflectStr = lerp(0.04, 1, o.Metallic);
		
		if (_Reflections)
			o.Albedo *= 1 - o.Metallic;

		float3 reflection = 0;
		
		if (_Reflections)
			reflection = GetReflections(d.worldSpaceNormal, d.worldSpaceViewDir, o.Smoothness);

		#if !defined (LIGHTMAP_ON)
			half4 shadowMask = unity_ProbesOcclusion;
		#else
			half4 shadowMask = SAMPLE_SHADOWMASK(d.texcoord1.xy);
		#endif

		//Get lighting data, either from vertex shader or calculate it here
		#if defined(_LIGHTINGTYPE_VERTEX) && !defined(PASS_META)

			float vsmoothness = _VertexLightSmoothness;

			#ifdef _VCOLSMOOTHNESS
				vsmoothness = remap01(_SmoothRemapVCol.x, _SmoothRemapVCol.y, v.vertexColor.a);
			#endif

			o.Smoothness = vsmoothness;

			mainLightDiffuse = min(d.extraV2F0.y, d.extraV2F0.z); // Min(mainLight, shadow)

			#ifdef _TOONSHADING
				mainLightDiffuse = SampleRamp(_ToonRamp, mainLightDiffuse);
			#endif

			mainLightDiffuse *= mainLightCol;
			ambient = d.extraV2F1.xyz;

			#ifndef _TOONSHADING
				mainSpec = d.extraV2F0.w * mainLightCol;
			#else
				mainSpec = SampleRamp(_SpecularRamp, d.extraV2F0.w) * mainLightCol;
			#endif

			float3 worldPos = d.worldSpacePosition;

			int pixelLights = GetAdditionalLightsCount();
			Light light;
			if (pixelLights >= 1)
			{
				light = GetAdditionalLight(0, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.x * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.x) * light.color;
				#endif
				
				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.x * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.x) * light.color * specColor;
				#endif
			}
			if (pixelLights >= 2)
			{
				light = GetAdditionalLight(1, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.y * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.y) * light.color;
				#endif

				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.y * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.y) * light.color * specColor;
				#endif
			}
			if (pixelLights >= 3)
			{
				light = GetAdditionalLight(2, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.z * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.z) * light.color;
				#endif

				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.z * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.z) * light.color * specColor;
				#endif
			}
			if (pixelLights >= 4)
			{
				light = GetAdditionalLight(3, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.w * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.w) * light.color;
				#endif

				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.w * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.w) * light.color * specColor;
				#endif
			}

			mainSpec *= _VertexSpecColor;
			addSpec *= _VertexSpecColor;
			//addDiffuse = d.extraV2F3.xyz;
			//addSpec = d.extraV2F4.xyz;
		#else

			float dirOcclusion, indirOcclusion;
			SSAO(d, dirOcclusion, indirOcclusion);

			mainShadow = MainLightShadows(d.worldSpacePosition, d.texcoord1.xy, d.texcoord0.xy);
			
			#ifndef _TOONSHADING
				mainLightDiffuse = GetDiffuse(d.worldSpaceNormal, mainLightDir) * mainLightAtten * mainLightCol * mainShadow * dirOcclusion;
			#else
				mainLightDiffuse = GetDiffuse(d.worldSpaceNormal, mainLightDir) * mainShadow * dirOcclusion;
				float3 ramp = SampleRamp(_ToonRamp, mainLightDiffuse.x);
				mainLightDiffuse = ramp * mainLightAtten * mainLightCol;
			#endif

			ambient = GetAmbient(d.worldSpaceNormal, d.texcoord1.xy, d.texcoord0.xy) * min(o.Occlusion,indirOcclusion);
			mainSpec = GetSpecular(mainLightDir, d.worldSpaceNormal, d.worldSpaceViewDir, o.Smoothness)*dirOcclusion;

			#ifdef _TOONSHADING
				mainSpec = SampleRamp(_SpecularRamp, mainSpec.x);
			#endif

			mainSpec *= mainLightCol * mainLightAtten * specColor;
			mainSpec *= mainShadow;

			AdditionalLights(d.worldSpacePosition, d.worldSpaceViewDir, d.worldSpaceNormal, o.Smoothness, specColor, addDiffuse, addSpec);

		#endif

		float perceptualRoughness = 1 - o.Smoothness;
		perceptualRoughness *= perceptualRoughness;

		float fresnel = pow(1.0 - saturate(dot(d.worldSpaceNormal, d.worldSpaceViewDir)), 4);
		reflectStr *= lerp(0.1, 1, GetLuminance(mainLightDiffuse));
		reflectStr = lerp(reflectStr, saturate(reflectStr*10), fresnel);

		reflection *= reflectStr * specColor;
		reflection /= perceptualRoughness + 0.5;

		if (_DoubleSizeDither) pixelPos /= 2.0;

		[branch]
		if (_DitherDiffuse)
		{
			ditherColor(mainLightDiffuse, pixelPos);
			ditherColor(addDiffuse, pixelPos);
		}

		[branch]
		if (_DitherSpec)
		{
			ditherColor(mainSpec, pixelPos);
			ditherColor(addSpec, pixelPos);
		}

		[branch]
		if (_DitherAmbient)
			ditherColor(ambient, pixelPos);

		o.Alpha = mainTex.a * _Color.a;

		//o.Albedo *= 1 - saturate(GetLuminance(mainSpec));
		//o.Albedo *= 1 - saturate(GetLuminance(addSpec));

		#if defined(RETROSHADER_ALPHA) && !defined(PASS_META)
			if (_PreserveSpecular)
			{
				float specStrength = saturate(GetLuminance(mainSpec));
				float reflectStrength = saturate(GetLuminance(reflection));
				o.Alpha = max(o.Alpha, max(specStrength, reflectStrength));
			}
		#endif

		#ifdef _ALPHATEST_ON
			clip(step(_Cutoff, o.Alpha)-0.1f);
		#endif

		if (_VColEmissive)
		{
			o.Albedo += d.vertexColor.rgb * max(0,_VertexEmissiveStr);
			o.Emission = d.vertexColor.rgb * max(0,_VertexEmissiveStr);
		}
		else
		{
			o.Albedo *= d.vertexColor.rgb;
		}

		float3 combinedAmbient = ambient * o.Albedo;

		// Apply lighting, but not for lightbaking pass
		#if !defined(PASS_META)
			o.Albedo *= mainLightDiffuse + addDiffuse;
			o.Albedo += combinedAmbient;
			o.Albedo += mainSpec * saturate(GetLuminance(mainLightDiffuse));
			o.Albedo += addSpec * saturate(GetLuminance(addDiffuse));
			o.Albedo += reflection;

			#if defined(_EMISSION)
				o.Albedo += emission;
			#endif
		#endif
		
		#if defined(RETROSHADER_ALPHA) && !defined(PASS_META)
			if (_Refraction)
			{
				float origAlpha = o.Alpha;
				float4 offsetPos = float4(d.worldSpacePosition + d.worldSpaceNormal * _RefractionStr, 1.0);
				offsetPos = mul(GetWorldToHClipMatrix(), offsetPos);
				float scale = 1.0;

				#if UNITY_UV_STARTS_AT_TOP
					scale = -1.0;
				#endif

				float4 grabPos = offsetPos *0.5f;
				grabPos.xy = float2(grabPos.x, grabPos.y*scale) + grabPos.w;
				grabPos.zw = offsetPos.zw;
				grabPos.xy /= grabPos.w;
				grabPos.xy = clamp(grabPos.xy, float2(0.0, 0.0), float2(1.0, 1.0));

				float3 refraction = GetSceneColor(grabPos.xy);
				float2 cubemapBlendFactors = pow(abs((grabPos.xy - 0.5) * 2.0),0.25);
				float blend = smoothstep(0.75f, 1, max(cubemapBlendFactors.x, cubemapBlendFactors.y));
				float3 refractDir = refract(-d.worldSpaceViewDir, d.worldSpaceNormal, 1 - _RefractionStr);
				float3 reflectProbe = GetReflections(-refractDir, refractDir, 1.0);
				refraction = lerp(refraction, reflectProbe, blend);

				o.Albedo = lerp(refraction, o.Albedo, origAlpha);
				o.Alpha = 1;
			}
		#endif

		o.Albedo = MixFog(o.Albedo, d.extraV2F0.x);
	}




        
            void ChainSurfaceFunction(inout Surface l, inout ShaderData d)
            {
                  Ext_SurfaceFunction0(l, d);
                 // Ext_SurfaceFunction1(l, d);
                 // Ext_SurfaceFunction2(l, d);
                 // Ext_SurfaceFunction3(l, d);
                 // Ext_SurfaceFunction4(l, d);
                 // Ext_SurfaceFunction5(l, d);
                 // Ext_SurfaceFunction6(l, d);
                 // Ext_SurfaceFunction7(l, d);
                 // Ext_SurfaceFunction8(l, d);
                 // Ext_SurfaceFunction9(l, d);
		           // Ext_SurfaceFunction10(l, d);
                 // Ext_SurfaceFunction11(l, d);
                 // Ext_SurfaceFunction12(l, d);
                 // Ext_SurfaceFunction13(l, d);
                 // Ext_SurfaceFunction14(l, d);
                 // Ext_SurfaceFunction15(l, d);
                 // Ext_SurfaceFunction16(l, d);
                 // Ext_SurfaceFunction17(l, d);
                 // Ext_SurfaceFunction18(l, d);
		           // Ext_SurfaceFunction19(l, d);
                 // Ext_SurfaceFunction20(l, d);
                 // Ext_SurfaceFunction21(l, d);
                 // Ext_SurfaceFunction22(l, d);
                 // Ext_SurfaceFunction23(l, d);
                 // Ext_SurfaceFunction24(l, d);
                 // Ext_SurfaceFunction25(l, d);
                 // Ext_SurfaceFunction26(l, d);
                 // Ext_SurfaceFunction27(l, d);
                 // Ext_SurfaceFunction28(l, d);
		           // Ext_SurfaceFunction29(l, d);
            }

            void ChainModifyVertex(inout VertexData v, inout VertexToPixel v2p, float4 time)
            {
                 ExtraV2F d;
                 
                 ZERO_INITIALIZE(ExtraV2F, d);
                 ZERO_INITIALIZE(Blackboard, d.blackboard);
                 // due to motion vectors in HDRP, we need to use the last
                 // time in certain spots. So if you are going to use _Time to adjust vertices,
                 // you need to use this time or motion vectors will break. 
                 d.time = time;

                   Ext_ModifyVertex0(v, d);
                 // Ext_ModifyVertex1(v, d);
                 // Ext_ModifyVertex2(v, d);
                 // Ext_ModifyVertex3(v, d);
                 // Ext_ModifyVertex4(v, d);
                 // Ext_ModifyVertex5(v, d);
                 // Ext_ModifyVertex6(v, d);
                 // Ext_ModifyVertex7(v, d);
                 // Ext_ModifyVertex8(v, d);
                 // Ext_ModifyVertex9(v, d);
                 // Ext_ModifyVertex10(v, d);
                 // Ext_ModifyVertex11(v, d);
                 // Ext_ModifyVertex12(v, d);
                 // Ext_ModifyVertex13(v, d);
                 // Ext_ModifyVertex14(v, d);
                 // Ext_ModifyVertex15(v, d);
                 // Ext_ModifyVertex16(v, d);
                 // Ext_ModifyVertex17(v, d);
                 // Ext_ModifyVertex18(v, d);
                 // Ext_ModifyVertex19(v, d);
                 // Ext_ModifyVertex20(v, d);
                 // Ext_ModifyVertex21(v, d);
                 // Ext_ModifyVertex22(v, d);
                 // Ext_ModifyVertex23(v, d);
                 // Ext_ModifyVertex24(v, d);
                 // Ext_ModifyVertex25(v, d);
                 // Ext_ModifyVertex26(v, d);
                 // Ext_ModifyVertex27(v, d);
                 // Ext_ModifyVertex28(v, d);
                 // Ext_ModifyVertex29(v, d);


                 // #if %EXTRAV2F0REQUIREKEY%
                  v2p.extraV2F0 = d.extraV2F0;
                 // #endif

                 // #if %EXTRAV2F1REQUIREKEY%
                  v2p.extraV2F1 = d.extraV2F1;
                 // #endif

                 // #if %EXTRAV2F2REQUIREKEY%
                  v2p.extraV2F2 = d.extraV2F2;
                 // #endif

                 // #if %EXTRAV2F3REQUIREKEY%
                  v2p.extraV2F3 = d.extraV2F3;
                 // #endif

                 // #if %EXTRAV2F4REQUIREKEY%
                  v2p.extraV2F4 = d.extraV2F4;
                 // #endif

                 // #if %EXTRAV2F5REQUIREKEY%
                 // v2p.extraV2F5 = d.extraV2F5;
                 // #endif

                 // #if %EXTRAV2F6REQUIREKEY%
                 // v2p.extraV2F6 = d.extraV2F6;
                 // #endif

                 // #if %EXTRAV2F7REQUIREKEY%
                 // v2p.extraV2F7 = d.extraV2F7;
                 // #endif
            }

            void ChainModifyTessellatedVertex(inout VertexData v, inout VertexToPixel v2p)
            {
               ExtraV2F d;
               ZERO_INITIALIZE(ExtraV2F, d);
               ZERO_INITIALIZE(Blackboard, d.blackboard);

               // #if %EXTRAV2F0REQUIREKEY%
                d.extraV2F0 = v2p.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
                d.extraV2F1 = v2p.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
                d.extraV2F2 = v2p.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
                d.extraV2F3 = v2p.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
                d.extraV2F4 = v2p.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // d.extraV2F5 = v2p.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // d.extraV2F6 = v2p.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // d.extraV2F7 = v2p.extraV2F7;
               // #endif


               // Ext_ModifyTessellatedVertex0(v, d);
               // Ext_ModifyTessellatedVertex1(v, d);
               // Ext_ModifyTessellatedVertex2(v, d);
               // Ext_ModifyTessellatedVertex3(v, d);
               // Ext_ModifyTessellatedVertex4(v, d);
               // Ext_ModifyTessellatedVertex5(v, d);
               // Ext_ModifyTessellatedVertex6(v, d);
               // Ext_ModifyTessellatedVertex7(v, d);
               // Ext_ModifyTessellatedVertex8(v, d);
               // Ext_ModifyTessellatedVertex9(v, d);
               // Ext_ModifyTessellatedVertex10(v, d);
               // Ext_ModifyTessellatedVertex11(v, d);
               // Ext_ModifyTessellatedVertex12(v, d);
               // Ext_ModifyTessellatedVertex13(v, d);
               // Ext_ModifyTessellatedVertex14(v, d);
               // Ext_ModifyTessellatedVertex15(v, d);
               // Ext_ModifyTessellatedVertex16(v, d);
               // Ext_ModifyTessellatedVertex17(v, d);
               // Ext_ModifyTessellatedVertex18(v, d);
               // Ext_ModifyTessellatedVertex19(v, d);
               // Ext_ModifyTessellatedVertex20(v, d);
               // Ext_ModifyTessellatedVertex21(v, d);
               // Ext_ModifyTessellatedVertex22(v, d);
               // Ext_ModifyTessellatedVertex23(v, d);
               // Ext_ModifyTessellatedVertex24(v, d);
               // Ext_ModifyTessellatedVertex25(v, d);
               // Ext_ModifyTessellatedVertex26(v, d);
               // Ext_ModifyTessellatedVertex27(v, d);
               // Ext_ModifyTessellatedVertex28(v, d);
               // Ext_ModifyTessellatedVertex29(v, d);

               // #if %EXTRAV2F0REQUIREKEY%
                v2p.extraV2F0 = d.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
                v2p.extraV2F1 = d.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
                v2p.extraV2F2 = d.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
                v2p.extraV2F3 = d.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
                v2p.extraV2F4 = d.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // v2p.extraV2F5 = d.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // v2p.extraV2F6 = d.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // v2p.extraV2F7 = d.extraV2F7;
               // #endif
            }

            void ChainFinalColorForward(inout Surface l, inout ShaderData d, inout half4 color)
            {
               //   Ext_FinalColorForward0(l, d, color);
               //   Ext_FinalColorForward1(l, d, color);
               //   Ext_FinalColorForward2(l, d, color);
               //   Ext_FinalColorForward3(l, d, color);
               //   Ext_FinalColorForward4(l, d, color);
               //   Ext_FinalColorForward5(l, d, color);
               //   Ext_FinalColorForward6(l, d, color);
               //   Ext_FinalColorForward7(l, d, color);
               //   Ext_FinalColorForward8(l, d, color);
               //   Ext_FinalColorForward9(l, d, color);
               //  Ext_FinalColorForward10(l, d, color);
               //  Ext_FinalColorForward11(l, d, color);
               //  Ext_FinalColorForward12(l, d, color);
               //  Ext_FinalColorForward13(l, d, color);
               //  Ext_FinalColorForward14(l, d, color);
               //  Ext_FinalColorForward15(l, d, color);
               //  Ext_FinalColorForward16(l, d, color);
               //  Ext_FinalColorForward17(l, d, color);
               //  Ext_FinalColorForward18(l, d, color);
               //  Ext_FinalColorForward19(l, d, color);
               //  Ext_FinalColorForward20(l, d, color);
               //  Ext_FinalColorForward21(l, d, color);
               //  Ext_FinalColorForward22(l, d, color);
               //  Ext_FinalColorForward23(l, d, color);
               //  Ext_FinalColorForward24(l, d, color);
               //  Ext_FinalColorForward25(l, d, color);
               //  Ext_FinalColorForward26(l, d, color);
               //  Ext_FinalColorForward27(l, d, color);
               //  Ext_FinalColorForward28(l, d, color);
               //  Ext_FinalColorForward29(l, d, color);
            }

            void ChainFinalGBufferStandard(inout Surface s, inout ShaderData d, inout half4 GBuffer0, inout half4 GBuffer1, inout half4 GBuffer2, inout half4 outEmission, inout half4 outShadowMask)
            {
               //   Ext_FinalGBufferStandard0(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard1(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard2(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard3(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard4(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard5(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard6(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard7(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard8(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard9(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard10(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard11(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard12(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard13(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard14(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard15(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard16(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard17(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard18(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard19(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard20(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard21(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard22(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard23(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard24(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard25(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard26(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard27(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard28(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard29(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
            }



            

         ShaderData CreateShaderData(VertexToPixel i
                  #if NEED_FACING
                     , bool facing
                  #endif
         )
         {
            ShaderData d = (ShaderData)0;
            d.clipPos = i.pos;
            d.worldSpacePosition = i.worldPos;

            d.worldSpaceNormal = normalize(i.worldNormal);
            d.worldSpaceTangent = normalize(i.worldTangent.xyz);
            d.tangentSign = i.worldTangent.w;
            float3 bitangent = cross(i.worldTangent.xyz, i.worldNormal) * d.tangentSign * -1;
            

            d.TBNMatrix = float3x3(d.worldSpaceTangent, bitangent, d.worldSpaceNormal);
            d.worldSpaceViewDir = normalize(_WorldSpaceCameraPos - i.worldPos);

            d.tangentSpaceViewDir = mul(d.TBNMatrix, d.worldSpaceViewDir);
             d.texcoord0 = i.texcoord0;
             d.texcoord1 = i.texcoord1;
            // d.texcoord2 = i.texcoord2;

            // #if %TEXCOORD3REQUIREKEY%
            // d.texcoord3 = i.texcoord3;
            // #endif

            // d.isFrontFace = facing;
            // #if %VERTEXCOLORREQUIREKEY%
             d.vertexColor = i.vertexColor;
            // #endif

            // these rarely get used, so we back transform them. Usually will be stripped.
            #if _HDRP
                // d.localSpacePosition = mul(unity_WorldToObject, float4(GetCameraRelativePositionWS(i.worldPos), 1)).xyz;
            #else
                // d.localSpacePosition = mul(unity_WorldToObject, float4(i.worldPos, 1)).xyz;
            #endif
            // d.localSpaceNormal = normalize(mul((float3x3)unity_WorldToObject, i.worldNormal));
            // d.localSpaceTangent = normalize(mul((float3x3)unity_WorldToObject, i.worldTangent.xyz));

            // #if %SCREENPOSREQUIREKEY%
             d.screenPos = i.screenPos;
             d.screenUV = (i.screenPos.xy / i.screenPos.w);
            // #endif


            // #if %EXTRAV2F0REQUIREKEY%
             d.extraV2F0 = i.extraV2F0;
            // #endif

            // #if %EXTRAV2F1REQUIREKEY%
             d.extraV2F1 = i.extraV2F1;
            // #endif

            // #if %EXTRAV2F2REQUIREKEY%
             d.extraV2F2 = i.extraV2F2;
            // #endif

            // #if %EXTRAV2F3REQUIREKEY%
             d.extraV2F3 = i.extraV2F3;
            // #endif

            // #if %EXTRAV2F4REQUIREKEY%
             d.extraV2F4 = i.extraV2F4;
            // #endif

            // #if %EXTRAV2F5REQUIREKEY%
            // d.extraV2F5 = i.extraV2F5;
            // #endif

            // #if %EXTRAV2F6REQUIREKEY%
            // d.extraV2F6 = i.extraV2F6;
            // #endif

            // #if %EXTRAV2F7REQUIREKEY%
            // d.extraV2F7 = i.extraV2F7;
            // #endif

            return d;
         }
         

            
         #if defined(_PASSSHADOW)
            float3 _LightDirection;
            float3 _LightPosition;
         #endif

         // vertex shader
         VertexToPixel Vert (VertexData v)
         {
           
           VertexToPixel o = (VertexToPixel)0;

           UNITY_SETUP_INSTANCE_ID(v);
           UNITY_TRANSFER_INSTANCE_ID(v, o);
           UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);


#if !_TESSELLATION_ON
           ChainModifyVertex(v, o, _Time);
#endif

            o.texcoord0 = v.texcoord0;
            o.texcoord1 = v.texcoord1;
           // o.texcoord2 = v.texcoord2;

           // #if %TEXCOORD3REQUIREKEY%
           // o.texcoord3 = v.texcoord3;
           // #endif

           // #if %VERTEXCOLORREQUIREKEY%
            o.vertexColor = v.vertexColor;
           // #endif
           
           VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
           o.worldPos = TransformObjectToWorld(v.vertex.xyz);
           o.worldNormal = TransformObjectToWorldNormal(v.normal);
           o.worldTangent = float4(TransformObjectToWorldDir(v.tangent.xyz), v.tangent.w);

          // For some very odd reason, in 2021.2, we can't use Unity's defines, but have to use our own..
          #if _PASSSHADOW
              #if _CASTING_PUNCTUAL_LIGHT_SHADOW
                 float3 lightDirectionWS = normalize(_LightPosition - o.worldPos);
              #else
                 float3 lightDirectionWS = _LightDirection;
              #endif
              // Define shadow pass specific clip position for Universal
              o.pos = TransformWorldToHClip(ApplyShadowBias(o.worldPos, o.worldNormal, lightDirectionWS));
              #if UNITY_REVERSED_Z
                  o.pos.z = min(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #else
                  o.pos.z = max(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #endif
          #elif _PASSMETA
              o.pos = MetaVertexPosition(float4(v.vertex.xyz, 0), v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST);
          #else
              o.pos = TransformWorldToHClip(o.worldPos);
          #endif

          // #if %SCREENPOSREQUIREKEY%
           o.screenPos = ComputeScreenPos(o.pos, _ProjectionParams.x);
          // #endif

          #if _PASSFORWARD || _PASSGBUFFER
              float2 uv1 = v.texcoord1.xy;
              OUTPUT_LIGHTMAP_UV(uv1, unity_LightmapST, o.lightmapUV);
               o.texcoord1.xy = uv1;
              OUTPUT_SH(o.worldNormal, o.sh);
              #if defined(DYNAMICLIGHTMAP_ON)
                   o.dynamicLightmapUV.xy = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
              #endif
          #endif

          #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
              half fogFactor = 0;
              #if defined(_FOG_FRAGMENT)
                fogFactor = ComputeFogFactor(o.pos.z);
              #endif
              #if _BAKEDLIT
                 o.fogFactorAndVertexLight = half4(fogFactor, 0, 0, 0);
              #else
                 half3 vertexLight = VertexLighting(o.worldPos, o.worldNormal);
                 o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);
              #endif
          #endif

          #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
             o.shadowCoord = GetShadowCoord(vertexInput);
          #endif

           return o;
         }


            

            // fragment shader
            half4 Frag (VertexToPixel IN
            #ifdef _DEPTHOFFSET_ON
              , out float outputDepth : SV_Depth
            #endif
            #if NEED_FACING
               , bool facing : SV_IsFrontFace
            #endif
            ) : SV_Target
            {
               UNITY_SETUP_INSTANCE_ID(IN);

               ShaderData d = CreateShaderData(IN
                  #if NEED_FACING
                     , facing
                  #endif
               );
               Surface l = (Surface)0;

               #ifdef _DEPTHOFFSET_ON
                  l.outputDepth = outputDepth;
               #endif

               l.Albedo = half3(0.5, 0.5, 0.5);
               l.Normal = float3(0,0,1);
               l.Occlusion = 1;
               l.Alpha = 1;

               ChainSurfaceFunction(l, d);

               #ifdef _DEPTHOFFSET_ON
                  outputDepth = l.outputDepth;
               #endif

             return 0;

            }

         ENDHLSL

      }


      
        Pass
        {
            Name "DepthOnly"
            Tags 
            { 
                "LightMode" = "DepthOnly"
            }
           
            // Render State
            Blend One Zero, One Zero
            Cull Back
            ZTest LEqual
            ZWrite On
            ColorMask 0
            
            	Cull [_Cull]


            HLSLPROGRAM

               #pragma vertex Vert
   #pragma fragment Frag


            #define _PASSDEPTH 1

            #pragma target 3.0
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma multi_compile_instancing
            #pragma multi_compile _ DOTS_INSTANCING_ON

            
	#pragma multi_compile_local _LIGHTINGTYPE_PIXEL _LIGHTINGTYPE_VERTEX _LIGHTINGTYPE_TEXEL
	#pragma multi_compile_local _TEXTUREFILTERING_NEAREST _TEXTUREFILTERING_LINEAR _TEXTUREFILTERING_N64
	#pragma shader_feature_local _VERTEXJITTER
	#pragma shader_feature_local _VCOLSMOOTHNESS
	#pragma shader_feature_local _TOONSHADING
	#pragma shader_feature_local _EMISSION
	#pragma shader_feature_local _ALPHATEST_ON
	#pragma shader_feature_local _AFFINE_MAPPING
	#undef _UNLIT


   #define _URP 1

#define _UNLIT 1
#define _USINGTEXCOORD1 1

            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Version.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"


                  #undef WorldNormalVector
      #define WorldNormalVector(data, normal) mul(normal, data.TBNMatrix)
      
      #define UnityObjectToWorldNormal(normal) mul(GetObjectToWorldMatrix(), normal)

      #define _WorldSpaceLightPos0 _MainLightPosition
      
      #define UNITY_DECLARE_TEX2D(name) TEXTURE2D(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2D_NOSAMPLER(name) TEXTURE2D(name);
      #define UNITY_DECLARE_TEX2DARRAY(name) TEXTURE2D_ARRAY(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(name) TEXTURE2D_ARRAY(name);

      #define UNITY_SAMPLE_TEX2DARRAY(tex,coord)            SAMPLE_TEXTURE2D_ARRAY(tex, sampler##tex, coord.xy, coord.z)
      #define UNITY_SAMPLE_TEX2DARRAY_LOD(tex,coord,lod)    SAMPLE_TEXTURE2D_ARRAY_LOD(tex, sampler##tex, coord.xy, coord.z, lod)
      #define UNITY_SAMPLE_TEX2D(tex, coord)                SAMPLE_TEXTURE2D(tex, sampler##tex, coord)
      #define UNITY_SAMPLE_TEX2D_SAMPLER(tex, samp, coord)  SAMPLE_TEXTURE2D(tex, sampler##samp, coord)

      #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod)   SAMPLE_TEXTURE2D_LOD(tex, sampler_##tex, coord, lod)
      #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) SAMPLE_TEXTURE2D_LOD (tex, sampler##samplertex,coord, lod)
     
      #if defined(UNITY_COMPILER_HLSL)
         #define UNITY_INITIALIZE_OUTPUT(type,name) name = (type)0;
      #else
         #define UNITY_INITIALIZE_OUTPUT(type,name)
      #endif

      #define sampler2D_float sampler2D
      #define sampler2D_half sampler2D

      

      // data across stages, stripped like the above.
      struct VertexToPixel
      {
         float4 pos : SV_POSITION;
         float3 worldPos : TEXCOORD0;
         float3 worldNormal : TEXCOORD1;
         float4 worldTangent : TEXCOORD2;
          float4 texcoord0 : TEXCOORD3;
          float4 texcoord1 : TEXCOORD4;
         // float4 texcoord2 : TEXCOORD5;

         // #if %TEXCOORD3REQUIREKEY%
         // float4 texcoord3 : TEXCOORD6;
         // #endif

         // #if %SCREENPOSREQUIREKEY%
          float4 screenPos : TEXCOORD7;
         // #endif

         // #if %VERTEXCOLORREQUIREKEY%
          half4 vertexColor : COLOR;
         // #endif

         #if defined(LIGHTMAP_ON)
            float2 lightmapUV : TEXCOORD8;
         #endif
         #if defined(DYNAMICLIGHTMAP_ON)
            float2 dynamicLightmapUV : TEXCOORD9;
         #endif
         #if !defined(LIGHTMAP_ON)
            float3 sh : TEXCOORD10;
         #endif

         #if defined(VARYINGS_NEED_FOG_AND_VERTEX_LIGHT)
            float4 fogFactorAndVertexLight : TEXCOORD11;
         #endif

         #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
           float4 shadowCoord : TEXCOORD12;
         #endif

         // #if %EXTRAV2F0REQUIREKEY%
          float4 extraV2F0 : TEXCOORD13;
         // #endif

         // #if %EXTRAV2F1REQUIREKEY%
          float4 extraV2F1 : TEXCOORD14;
         // #endif

         // #if %EXTRAV2F2REQUIREKEY%
          float4 extraV2F2 : TEXCOORD15;
         // #endif

         // #if %EXTRAV2F3REQUIREKEY%
          float4 extraV2F3 : TEXCOORD16;
         // #endif

         // #if %EXTRAV2F4REQUIREKEY%
          float4 extraV2F4 : TEXCOORD17;
         // #endif

         // #if %EXTRAV2F5REQUIREKEY%
         // float4 extraV2F5 : TEXCOORD18;
         // #endif

         // #if %EXTRAV2F6REQUIREKEY%
         // float4 extraV2F6 : TEXCOORD19;
         // #endif

         // #if %EXTRAV2F7REQUIREKEY%
         // float4 extraV2F7 : TEXCOORD20;
         // #endif

         #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
         #endif
         #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
         #endif
         #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
         #endif
         #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
         #endif
      };

         
            
            
            // data describing the user output of a pixel
            struct Surface
            {
               half3 Albedo;
               half Height;
               half3 Normal;
               half Smoothness;
               half3 Emission;
               half Metallic;
               half3 Specular;
               half Occlusion;
               half SpecularPower; // for simple lighting
               half Alpha;
               float outputDepth; // if written, SV_Depth semantic is used. ShaderData.clipPos.z is unused value
               // HDRP Only
               half SpecularOcclusion;
               half SubsurfaceMask;
               half Thickness;
               half CoatMask;
               half CoatSmoothness;
               half Anisotropy;
               half IridescenceMask;
               half IridescenceThickness;
               int DiffusionProfileHash;
               float SpecularAAThreshold;
               float SpecularAAScreenSpaceVariance;
               // requires _OVERRIDE_BAKEDGI to be defined, but is mapped in all pipelines
               float3 DiffuseGI;
               float3 BackDiffuseGI;
               float3 SpecularGI;
               // requires _OVERRIDE_SHADOWMASK to be defines
               float4 ShadowMask;
            };

            // Data the user declares in blackboard blocks
            struct Blackboard
            {
                
                float blackboardDummyData;
            };

            // data the user might need, this will grow to be big. But easy to strip
            struct ShaderData
            {
               float4 clipPos; // SV_POSITION
               float3 localSpacePosition;
               float3 localSpaceNormal;
               float3 localSpaceTangent;
        
               float3 worldSpacePosition;
               float3 worldSpaceNormal;
               float3 worldSpaceTangent;
               float tangentSign;

               float3 worldSpaceViewDir;
               float3 tangentSpaceViewDir;

               float4 texcoord0;
               float4 texcoord1;
               float4 texcoord2;
               float4 texcoord3;

               float2 screenUV;
               float4 screenPos;

               float4 vertexColor;
               bool isFrontFace;

               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;

               float3x3 TBNMatrix;
               Blackboard blackboard;
            };

            struct VertexData
            {
               #if SHADER_TARGET > 30
               // uint vertexID : SV_VertexID;
               #endif
               float4 vertex : POSITION;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;

               // optimize out mesh coords when not in use by user or lighting system
               #if _URP && (_USINGTEXCOORD1 || _PASSMETA || _PASSFORWARD || _PASSGBUFFER)
                  float4 texcoord1 : TEXCOORD1;
               #endif

               #if _URP && (_USINGTEXCOORD2 || _PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && defined(DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif

               #if _STANDARD && (_USINGTEXCOORD1 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER || _PASSFORWARDADD) && LIGHTMAP_ON)))
                  float4 texcoord1 : TEXCOORD1;
               #endif
               #if _STANDARD && (_USINGTEXCOORD2 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif


               #if _HDRP
                  float4 texcoord1 : TEXCOORD1;
                  float4 texcoord2 : TEXCOORD2;
               #endif

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
                float4 vertexColor : COLOR;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD4; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity    : TEXCOORD5; // Add Precomputed Velocity (Alembic computes velocities on runtime side).
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct TessVertex 
            {
               float4 vertex : INTERNALTESSPOS;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;
               float4 texcoord1 : TEXCOORD1;
               float4 texcoord2 : TEXCOORD2;

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
                float4 vertexColor : COLOR;
               // #endif

               // #if %EXTRAV2F0REQUIREKEY%
                float4 extraV2F0 : TEXCOORD5;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
                float4 extraV2F1 : TEXCOORD6;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
                float4 extraV2F2 : TEXCOORD7;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
                float4 extraV2F3 : TEXCOORD8;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
                float4 extraV2F4 : TEXCOORD9;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // float4 extraV2F5 : TEXCOORD10;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // float4 extraV2F6 : TEXCOORD11;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // float4 extraV2F7 : TEXCOORD12;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD13; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity : TEXCOORD14;
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
               UNITY_VERTEX_OUTPUT_STEREO
            };

            struct ExtraV2F
            {
               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;
               Blackboard blackboard;
               float4 time;
            };


            float3 WorldToTangentSpace(ShaderData d, float3 normal)
            {
               return mul(d.TBNMatrix, normal);
            }

            float3 TangentToWorldSpace(ShaderData d, float3 normal)
            {
               return mul(normal, d.TBNMatrix);
            }

            // in this case, make standard more like SRPs, because we can't fix
            // unity_WorldToObject in HDRP, since it already does macro-fu there

            #if _STANDARD
               float3 TransformWorldToObject(float3 p) { return mul(unity_WorldToObject, float4(p, 1)); };
               float3 TransformObjectToWorld(float3 p) { return mul(unity_ObjectToWorld, float4(p, 1)); };
               float4 TransformWorldToObject(float4 p) { return mul(unity_WorldToObject, p); };
               float4 TransformObjectToWorld(float4 p) { return mul(unity_ObjectToWorld, p); };
               float4x4 GetWorldToObjectMatrix() { return unity_WorldToObject; }
               float4x4 GetObjectToWorldMatrix() { return unity_ObjectToWorld; }
               #if (defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (SHADER_TARGET_SURFACE_ANALYSIS && !SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod) tex.SampleLevel (sampler##tex,coord, lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) tex.SampleLevel (sampler##samplertex,coord, lod)
              #else
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord,lod) tex2D (tex,coord,0,lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord,lod) tex2D (tex,coord,0,lod)
              #endif

               #undef GetWorldToObjectMatrix()

               #define GetWorldToObjectMatrix()   unity_WorldToObject


            #endif

            float3 GetCameraWorldPosition()
            {
               #if _HDRP
                  return GetCameraRelativePositionWS(_WorldSpaceCameraPos);
               #else
                  return _WorldSpaceCameraPos;
               #endif
            }

            #if _GRABPASSUSED
               #if _STANDARD
                  TEXTURE2D(%GRABTEXTURE%);
                  SAMPLER(sampler_%GRABTEXTURE%);
               #endif

               half3 GetSceneColor(float2 uv)
               {
                  #if _STANDARD
                     return SAMPLE_TEXTURE2D(%GRABTEXTURE%, sampler_%GRABTEXTURE%, uv).rgb;
                  #else
                     return SHADERGRAPH_SAMPLE_SCENE_COLOR(uv);
                  #endif
               }
            #endif


      
            #if _STANDARD
               UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
               float GetSceneDepth(float2 uv) { return SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv)); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv)); } 
            #else
               float GetSceneDepth(float2 uv) { return SHADERGRAPH_SAMPLE_SCENE_DEPTH(uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv), _ZBufferParams); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv), _ZBufferParams); } 
            #endif

            float3 GetWorldPositionFromDepthBuffer(float2 uv, float3 worldSpaceViewDir)
            {
               float eye = GetLinearEyeDepth(uv);
               float3 camView = mul((float3x3)GetObjectToWorldMatrix(), transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V)) [2].xyz);

               float dt = dot(worldSpaceViewDir, camView);
               float3 div = worldSpaceViewDir/dt;
               float3 wpos = (eye * div) + GetCameraWorldPosition();
               return wpos;
            }

            #if _HDRP
            float3 ObjectToWorldSpacePosition(float3 pos)
            {
               return GetAbsolutePositionWS(TransformObjectToWorld(pos));
            }
            #else
            float3 ObjectToWorldSpacePosition(float3 pos)
            {
               return TransformObjectToWorld(pos);
            }
            #endif

            #if _STANDARD
               UNITY_DECLARE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture);
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  float4 depthNorms = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture, uv);
                  float3 norms = DecodeViewNormalStereo(depthNorms);
                  norms = mul((float3x3)GetWorldToViewMatrix(), norms) * 0.5 + 0.5;
                  return norms;
               }
            #elif _HDRP
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  NormalData nd;
                  DecodeFromNormalBuffer(_ScreenSize.xy * uv, nd);
                  return nd.normalWS;
               }
            #elif _URP
               #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                  #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareNormalsTexture.hlsl"
               #endif

               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                     return SampleSceneNormals(uv);
                  #else
                     float3 wpos = GetWorldPositionFromDepthBuffer(uv, worldSpaceViewDir);
                     return normalize(-cross(ddx(wpos), ddy(wpos))) * 0.5 + 0.5;
                  #endif

                }
             #endif

             #if _HDRP

               half3 UnpackNormalmapRGorAG(half4 packednormal)
               {
                     // This do the trick
                  packednormal.x *= packednormal.w;

                  half3 normal;
                  normal.xy = packednormal.xy * 2 - 1;
                  normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                  return normal;
               }
               half3 UnpackNormal(half4 packednormal)
               {
                  #if defined(UNITY_NO_DXT5nm)
                     return packednormal.xyz * 2 - 1;
                  #else
                     return UnpackNormalmapRGorAG(packednormal);
                  #endif
               }
               #endif
               #if _HDRP || _URP

               half3 UnpackScaleNormal(half4 packednormal, half scale)
               {
                 #ifndef UNITY_NO_DXT5nm
                   // Unpack normal as DXT5nm (1, y, 1, x) or BC5 (x, y, 0, 1)
                   // Note neutral texture like "bump" is (0, 0, 1, 1) to work with both plain RGB normal and DXT5nm/BC5
                   packednormal.x *= packednormal.w;
                 #endif
                   half3 normal;
                   normal.xy = (packednormal.xy * 2 - 1) * scale;
                   normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                   return normal;
               }	

             #endif


            void GetSun(out float3 lightDir, out float3 color)
            {
               lightDir = float3(0.5, 0.5, 0);
               color = 1;
               #if _HDRP
                  if (_DirectionalLightCount > 0)
                  {
                     DirectionalLightData light = _DirectionalLightDatas[0];
                     lightDir = -light.forward.xyz;
                     color = light.color;
                  }
               #elif _STANDARD
			         lightDir = normalize(_WorldSpaceLightPos0.xyz);
                  color = _LightColor0.rgb;
               #elif _URP
	               Light light = GetMainLight();
	               lightDir = light.direction;
	               color = light.color;
               #endif
            }


            
            CBUFFER_START(UnityPerMaterial)

               

	int _VColEmissive;
	float _VertexEmissiveStr;
	half4 _Color;
	int _HalfLambert;
	int _EnableMaskMap;
	int _ForceNearest;
	int _TextureFiltering;
	int _SpecularType;
	int _Reflections;
	float _NormalStrength;
	float2 _SmoothRemap;
	float2 _SmoothRemapVCol;
	float _VertexLightSmoothness;
	float3 _VertexSpecColor;
	float3 _EmissionColor;
	float4 _TilingOffset;
	float _Cutoff;
	float _AffineMapFactor;
	int _VertexJitterResX;
	int _VertexJitterResY;
	int _DoubleSizeDither;
	int _MaskTint;
	float _LightmapEmissionStr;
	
	int _DitherAmbient;
	int _DitherDiffuse;
	int _DitherSpec;

	float4 _BaseMap_TexelSize;
	float4 _MaskMap_TexelSize;
	float4 _BumpMap_TexelSize;
	float4 _EmissionMap_TexelSize;




            CBUFFER_END

            

            

            #ifdef unity_WorldToObject
#undef unity_WorldToObject
#endif
#ifdef unity_ObjectToWorld
#undef unity_ObjectToWorld
#endif
#define unity_ObjectToWorld GetObjectToWorldMatrix()
#define unity_WorldToObject GetWorldToObjectMatrix()


	/**********
	* TODO:
	* Vertex Colors
	* Meta Pass?
	**********/

	TEXTURE2D(_ToonRamp);
	TEXTURE2D(_SpecularRamp);
	SamplerState sampler_linear_clamp;

	TEXTURE2D(_BaseMap);
	SamplerState sampler_linear_repeat;
	SamplerState sampler_trilinear_repeat;
	SamplerState sampler_point_repeat;

	#if defined(_TEXTUREFILTERING_LINEAR) || defined(PASS_META)
		#define MAINSMP sampler_linear_repeat
	#else
		#define MAINSMP sampler_point_repeat
	#endif

	static const uint bayerPattern[] =
	{
		0, 32,  8, 40,  2, 34, 10, 42,   /* 8x8 Bayer ordered dithering  */
		48, 16, 56, 24, 50, 18, 58, 26,  /* pattern.  Each input pixel   */
		12, 44,  4, 36, 14, 46,  6, 38,  /* is scaled to the 0..63 range */
		60, 28, 52, 20, 62, 30, 54, 22,  /* before looking in this table */
		3, 35, 11, 43,  1, 33,  9, 41,   /* to determine the action.     */
		51, 19, 59, 27, 49, 17, 57, 25,
		15, 47,  7, 39, 13, 45,  5, 37,
		63, 31, 55, 23, 61, 29, 53, 21
	};

	float ditherValue(float2 screenPos)
	{
		uint stippleOffset = ((uint) screenPos.y % 8) * 8 + ((uint) screenPos.x % 8);
		uint byte = bayerPattern[stippleOffset];
		return byte / 64.0f;
	}

	float GetLuminance(float3 col)
	{
		return dot(col, float3(0.299f, 0.587f, 0.114f));
	}

	void ditherColor(inout float3 input, float2 screenPos)
	{
		input.x = input.x < ditherValue(screenPos) ? input.x*0.25 : input.x;
		input.y = input.y < ditherValue(screenPos) ? input.y*0.25 : input.y;
		input.z = input.z < ditherValue(screenPos) ? input.z*0.25 : input.z;
	}

	// This function has been basically ripped straight from https://forum.unity.com/threads/the-quest-for-efficient-per-texel-lighting.529948/
	// So big thanks to GreatestBear, and truepak182, without whom this shader wouldn't be nearly as cool
	float3 CalculateTexelWorldPos(float2 originalUV, float3 originalWorldPos, float4 texelSize)
	{
		// 1.) Calculate how much the texture UV coords need to
		//     shift to be at the center of the nearest texel.
		float2 centerUV = floor(originalUV * (texelSize.zw))/texelSize.zw + (texelSize.xy/2.0);
		float2 dUV = (centerUV - originalUV);
	
		// 2b.) Calculate how much the texture coords vary over fragment space.
		//      This essentially defines a 2x2 matrix that gets
		//      texture space (UV) deltas from fragment space (ST) deltas
		// Note: I call fragment space (S,T) to disambiguate.
		float2 dUVdS = ddx(originalUV);
		float2 dUVdT = ddy(originalUV);
	
		// 2c.) Invert the fragment from texture matrix
		float2x2 dSTdUV = float2x2(dUVdT[1], -dUVdT[0], -dUVdS[1], dUVdS[0])*(1.0f/(dUVdS[0]*dUVdT[1]-dUVdT[0]*dUVdS[1]));
	
	
		// 2d.) Convert the UV delta to a fragment space delta
		float2 dST = mul(dSTdUV , dUV);
	
		// 2e.) Calculate how much the world coords vary over fragment space.
		float3 dXYZdS = ddx(originalWorldPos);
		float3 dXYZdT = ddy(originalWorldPos);
	
		// 2f.) Finally, convert our fragment space delta to a world space delta
		// And be sure to clamp it to SOMETHING in case the derivative calc went insane
		// Here I clamp it to -1 to 1 unit in unity, which should be orders of magnitude greater
		// than the size of any texel.
		float3 dXYZ = clamp(dXYZdS * dST[0] + dXYZdT * dST[1], -1, 1);
	
		// 3.) Transform the snapped UV back to world space
		return originalWorldPos + dXYZ;
	}

	float3 CalculateTexelWorldViewDir(float3 viewDirectionWS, float3 positionWS)
	{
		//Recalculate view direction according to recalculated texelized inputData.positionWS
		float3 viewDirectionWSCorrected = GetCameraPositionWS() - positionWS;
		//This needs to be done to make corrected viewDirectionWSCorrected same length as the original. If omitted, specular will not be displayed properly
		float viewPositionMult = length(viewDirectionWS) / length(viewDirectionWSCorrected);

		return viewDirectionWSCorrected * viewPositionMult;
	}

	float2 SnapUVToTexelCenter(float2 originalUV, float4 texelSize)
	{
		float2 centerUV = floor(originalUV * (texelSize.zw))/texelSize.zw + (texelSize.xy/2.0);
		float2 dUV = (centerUV - originalUV);

		return originalUV + dUV;
	}


	float3 SampleRamp(Texture2D ramp, float coord)
	{
		return SAMPLE_TEXTURE2D_LOD(ramp, sampler_linear_clamp, saturate(float2(pow(coord,0.5), 0.5)), 0).xyz;
	}

	float invLerp(float from, float to, float value){
		return (value - from) / (to - from);
	}

	float remap(float origFrom, float origTo, float targetFrom, float targetTo, float value){
		float rel = invLerp(origFrom, origTo, value);
		return lerp(targetFrom, targetTo, rel);
	}

	float remap01(float targetFrom, float targetTo, float value){
		float rel = invLerp(0, 1, value);
		return lerp(targetFrom, targetTo, rel);

	}

	void MainLight(out float3 direction, out float3 color, out float distanceAtten)
	{
		Light mainLight = GetMainLight();
		direction = mainLight.direction;
		color = mainLight.color;
		distanceAtten = mainLight.distanceAttenuation;
	}

	float MainLightShadows(float3 worldPos, float2 lightmapUV = float2(0.0,0.0), float2 origUV = float2(0.0,0.0))
	{
		float4 shadowCoord = TransformWorldToShadowCoord(worldPos);

		#if !defined (LIGHTMAP_ON) || defined(_LIGHTINGTYPE_VERTEX)
			half4 shadowMask = unity_ProbesOcclusion;
		#else
			lightmapUV = lightmapUV * unity_LightmapST.xy + unity_LightmapST.zw;

			#ifdef _LIGHTINGTYPE_TEXEL
				lightmapUV = CalculateTexelWorldPos(origUV, float3(lightmapUV.x, lightmapUV.y, 1), _BaseMap_TexelSize).xy;
			#endif

			half4 shadowMask = SAMPLE_SHADOWMASK(lightmapUV);
		#endif

		return MainLightShadow(shadowCoord, worldPos, shadowMask, _MainLightOcclusionProbes);
	}

	float3 GetAmbient(float3 worldNormal, float2 lightmapUV, float2 origUV = float2(0.0f, 0.0f))
	{
		#ifdef LIGHTMAP_ON
			lightmapUV = lightmapUV * unity_LightmapST.xy + unity_LightmapST.zw;

			#ifdef _LIGHTINGTYPE_TEXEL
				lightmapUV = CalculateTexelWorldPos(origUV, float3(lightmapUV.x, lightmapUV.y, 1), _BaseMap_TexelSize).xy;
			#endif

			#ifdef _LIGHTINGTYPE_VERTEX
				float4 lightmap = SAMPLE_TEXTURE2D_LOD(unity_Lightmap, sampler_linear_repeat, lightmapUV, 0);
				half3 bakedGI = DecodeLightmap(lightmap,half4(LIGHTMAP_HDR_MULTIPLIER, LIGHTMAP_HDR_EXPONENT, 0.0h, 0.0h));
			#else
				half3 bakedGI = SampleLightmap(lightmapUV, worldNormal);//DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, lightmapUV),half4(LIGHTMAP_HDR_MULTIPLIER, LIGHTMAP_HDR_EXPONENT, 0.0h, 0.0h));
			#endif

			#if defined(_MIXED_LIGHTING_SUBTRACTIVE)
				Light mainLight = GetMainLight();
				bakedGI = SubtractDirectMainLightFromLightmap(mainLight, worldNormal, bakedGI);
			#endif

			return bakedGI;
		#else
			return SampleSH(worldNormal);
		#endif
	}

	float GetSpecular(float3 lightDir, float3 worldNormal, float3 viewDir, float smoothness)
	{
		float SpecSmoothness = exp2(10 * smoothness + 1).x;
		[branch]
		if (_SpecularType == 0) //Cook-Torrance
		{
			float roughness = 1 - smoothness;
			roughness *= roughness;

			float3 h = SafeNormalize(lightDir + viewDir);
			float nh = saturate(dot(worldNormal, h));
			float lh = saturate(dot(lightDir, h));
			float d = nh * nh * (roughness * roughness - 1.0) + 1.00001;
			float normTerm = roughness * 4.0 + 2.0;
			float specTerm = roughness * roughness;
			specTerm /= (d * d) * max(0.1, lh * lh) * normTerm;
			return specTerm*0.04;
		}
		else if (_SpecularType == 1) //Phong
		{
			float3 r = reflect(-lightDir, worldNormal);
			float rdotv = max(0, dot(r, viewDir));

			return pow(rdotv, SpecSmoothness)*smoothness;
		}
		else if (_SpecularType == 2) //Blinn-Phong
		{
			float3 h = SafeNormalize(lightDir+viewDir);
			float ndh = saturate(dot(worldNormal, h));

			return pow(ndh, SpecSmoothness)*smoothness;
		}
		else
		{
			return 0;
		}
	}

	float GetDiffuse(float3 lightDir, float3 worldNormal)
	{
		float diffuse = dot(lightDir, worldNormal);

		if (_HalfLambert)
			diffuse = diffuse * 0.5 + 0.5;

		return max(0,diffuse);
	}


	void AdditionalLights(float3 worldPos, float3 viewDir, float3 worldNormal, float InSmoothness, float3 specColor, out float3 Diffuse, out float3 Specular, float dirOcclusion = 1)
	{
		float3 diff = 0;
		float3 spec = 0;

		#if !defined (LIGHTMAP_ON)
			half4 shadowMask = unity_ProbesOcclusion;
		#else
			half4 shadowMask = half4(1, 1, 1, 1);
		#endif

		int pixelLights = GetAdditionalLightsCount();
		for (int i = 0; i < pixelLights; ++i)
		{
			Light light = GetAdditionalLight(i, worldPos, shadowMask);
			
			#ifdef _TOONSHADING
				float diffMask = GetDiffuse(light.direction, worldNormal) * light.shadowAttenuation * light.distanceAttenuation * dirOcclusion;
				diff += SampleRamp(_ToonRamp, diffMask) * light.color;
			#else
				diff += GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.color * light.shadowAttenuation * dirOcclusion;
			#endif

			#ifdef _TOONSHADING
				float specMask = GetSpecular(light.direction, worldNormal, viewDir, InSmoothness) * light.distanceAttenuation * light.shadowAttenuation * dirOcclusion;
				spec += SampleRamp(_SpecularRamp, specMask) * light.color * specColor;
			#else
				spec += GetSpecular(light.direction, worldNormal, viewDir, InSmoothness) * light.distanceAttenuation * light.color * specColor * light.shadowAttenuation * dirOcclusion;
			#endif
		}
		Diffuse = diff;
		Specular = spec;
	}


	float3 GetReflections(float3 worldNormal, float3 viewDir, float smoothness)
	{
		float perceptualRoughness = 1 - smoothness;
		perceptualRoughness *= perceptualRoughness;
		float mip = PerceptualRoughnessToMipmapLevel(perceptualRoughness);
		float3 sample = SHADERGRAPH_REFLECTION_PROBE(viewDir, worldNormal, mip);
		return sample.rgb;
	}

	void SSAO(ShaderData d, out float directOcclusion, out float indirectOcclusion)
	{
		#if defined(_SCREEN_SPACE_OCCLUSION) && !defined(RETROSHADER_ALPHA)
			float2 uv = d.screenUV;

			#ifdef _LIGHTINGTYPE_TEXEL
				float3 pos = CalculateTexelWorldPos(d.texcoord0, float3(uv.x, uv.y, 1.0), _BaseMap_TexelSize);
				uv = pos.xy;
			#endif

			AmbientOcclusionFactor aoFactor = GetScreenSpaceAmbientOcclusion(uv);
			directOcclusion = aoFactor.directAmbientOcclusion;
			indirectOcclusion = aoFactor.indirectAmbientOcclusion;
		#else
			directOcclusion = 1;
			indirectOcclusion = 1;
		#endif
	}

	// Courtesy of Ben Golus
	// https://forum.unity.com/threads/shader-ideas-for-replicating-n64-bi-linear-filtering.936119/#post-6116207
	half4 sampleN64(Texture2D tex, float2 uv, float4 texelSize)
	{
		// texel coordinates
		float2 texels = uv * texelSize.zw;

		// calculate mip level
		float2 dx = ddx(texels);
		float2 dy = ddy(texels);
		float delta_max_sqr = max(dot(dx, dx), dot(dy, dy));
		float mip = max(0.0, 0.5 * log2(delta_max_sqr));

		// scale texel sizes and texel coordinates to handle mip levels properly
		float scale = pow(2,floor(mip));
		texelSize.xy *= scale;
		texelSize.zw /= scale;
		texels = texels / scale - 0.5;

		// calculate blend for the three points of the tri-filter
		float2 fracTexels = frac(texels);
		float3 blend = float3(
			abs(fracTexels.x+fracTexels.y-1),
			min(abs(fracTexels.xx-float2(0,1)), abs(fracTexels.yy-float2(1,0)))
		);

		// calculate equivalents of point filtered uvs for the three points
		float2 uvA = (floor(texels + fracTexels.yx) + 0.5) * texelSize.xy;
		float2 uvB = (floor(texels) + float2(1.5, 0.5)) * texelSize.xy;
		float2 uvC = (floor(texels) + float2(0.5, 1.5)) * texelSize.xy;

		// sample points
		half4 A = SAMPLE_TEXTURE2D_LOD(tex, MAINSMP, uvA, mip);
		half4 B = SAMPLE_TEXTURE2D_LOD(tex, MAINSMP, uvB, mip);
		half4 C = SAMPLE_TEXTURE2D_LOD(tex, MAINSMP, uvC, mip);

		// blend and return
		return A * blend.x + B * blend.y + C * blend.z;
	}

	half4 sampleTex(Texture2D tex, float2 uv, float4 texelSize)
	{
		#ifdef _TEXTUREFILTERING_N64
			return sampleN64(tex, uv, texelSize);
		#else
			return SAMPLE_TEXTURE2D(tex, MAINSMP, uv);
		#endif
	}

	TEXTURE2D(_MaskMap);
	TEXTURE2D(_BumpMap);
	TEXTURE2D(_EmissionMap);

	void Ext_ModifyVertex0 (inout VertexData v, inout ExtraV2F d)
	{
		float4 clipPos = mul(GetWorldToHClipMatrix(), mul(unity_ObjectToWorld, v.vertex));

		

		#if defined(_LIGHTINGTYPE_VERTEX) && !defined(PASS_META)

			float smoothness = _VertexLightSmoothness;

			#ifdef _VCOLSMOOTHNESS
				smoothness = remap01(_SmoothRemapVCol.x, _SmoothRemapVCol.y, v.vertexColor.a);
			#endif

			float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
			float3 worldNormal = SafeNormalize(mul(unity_ObjectToWorld, v.normal));
			float3 viewDir = normalize(_WorldSpaceCameraPos - worldPos);
			float3 mainLightDir;
			float3 mainLightCol;
			float mainLightAtten;
			MainLight(mainLightDir, mainLightCol, mainLightAtten);

			d.extraV2F0.y = GetDiffuse(mainLightDir, worldNormal) * mainLightAtten;
			d.extraV2F1.xyz = GetAmbient(worldNormal, v.texcoord1);
			d.extraV2F0.z = MainLightShadows(worldPos, v.texcoord1);
			d.extraV2F0.w = GetSpecular(mainLightDir, worldNormal, viewDir, smoothness) * mainLightAtten * d.extraV2F0.z;

			#if !defined (LIGHTMAP_ON)
				half4 shadowMask = unity_ProbesOcclusion;
			#else
				half4 shadowMask = half4(1, 1, 1, 1);
			#endif

			// Have to store each light in a separate vector component so we can apply toon shading in fragment later - toon shading has to be applied per light to make sense
			// This limits us to max 4 lights, would need to use 2 more interpolators for 8
			int pixelLights = GetAdditionalLightsCount();
			Light light;
			if (pixelLights >= 1)
			{
				light = GetAdditionalLight(0, worldPos, shadowMask);
				d.extraV2F3.x = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.x = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
			if (pixelLights >= 2)
			{
				light = GetAdditionalLight(1, worldPos, shadowMask);
				d.extraV2F3.y = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.y = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
			if (pixelLights >= 3)
			{
				light = GetAdditionalLight(2, worldPos, shadowMask);
				d.extraV2F3.z = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.z = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
			if (pixelLights >= 4)
			{
				light = GetAdditionalLight(3, worldPos, shadowMask);
				d.extraV2F3.w = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.w = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
		#endif

		#if defined(_VERTEXJITTER) && !defined(_PASSSHADOW)
			float4 vert = clipPos;
			d.extraV2F2.xyz = mul(UNITY_MATRIX_I_VP, vert);
			vert.xyz = clipPos.xyz/clipPos.w;
			vert.x = floor(_VertexJitterResX * vert.x) / _VertexJitterResX;
			vert.y = floor(_VertexJitterResY * vert.y) / _VertexJitterResY;
			vert.xyz *= clipPos.w;
			v.vertex = mul(unity_WorldToObject, mul(UNITY_MATRIX_I_VP, vert));
		#endif

		d.extraV2F0.x = ComputeFogFactor(clipPos.z);
		v.texcoord0.xy = v.texcoord0.xy * _TilingOffset.xy + _TilingOffset.zw;

		#if defined(_AFFINE_MAPPING)
			_AffineMapFactor = min(1, _AffineMapFactor);
			float warpFactor = max(clipPos.w, 0.02); //Prevents flipping
			warpFactor = lerp(0.5, warpFactor, _AffineMapFactor);

			v.texcoord0.xy *= warpFactor;
			d.extraV2F1.w = warpFactor;
		#endif
	}

	
	void Ext_SurfaceFunction0 (inout Surface o, ShaderData d)
	{
		#if defined(_AFFINE_MAPPING)
			d.texcoord0.xy /= d.extraV2F1.w;
		#endif
		// Set up input data
		float4 mainTex = sampleTex(_BaseMap, d.texcoord0.xy, _BaseMap_TexelSize);
		o.Albedo = mainTex.rgb;
		float4 maskMap = sampleTex(_MaskMap, d.texcoord0.xy, _BaseMap_TexelSize);

		

		if (_MaskTint)
		{
			float3 tintedAlbedo = o.Albedo * _Color.rgb;
			o.Albedo = lerp(o.Albedo, tintedAlbedo, maskMap.a);
		}
		else
		{
			o.Albedo *= _Color.rgb;
		}

		#ifdef _EMISSION
			float3 emission = sampleTex(_EmissionMap, d.texcoord0.xy, _EmissionMap_TexelSize) * _EmissionColor;
			o.Emission = emission * _LightmapEmissionStr;

		#endif

		float2 pixelPos = d.clipPos.xy;//floor(d.screenUV.xy * _ScreenParams.xy);

		#if defined(_LIGHTINGTYPE_TEXEL) && !defined(PASS_META)
			o.Normal = UnpackScaleNormal(SAMPLE_TEXTURE2D(_BumpMap, sampler_linear_repeat, d.texcoord0.xy), _NormalStrength);
		#else
			o.Normal = UnpackScaleNormal(sampleTex(_BumpMap, d.texcoord0.xy, _BumpMap_TexelSize), _NormalStrength);
		#endif

		//o.Normal = SafeNormalize(o.Normal);

		#if defined(_VERTEXJITTER) && !defined(_PASSSHADOW)
			d.worldSpacePosition = d.extraV2F2.xyz;
		#endif

		d.worldSpaceNormal = TransformTangentToWorld(o.Normal, d.TBNMatrix);
		float3 unSnappedViewDir = d.worldSpaceViewDir;
		// Snap world position, view direction, and normal to the texel
		#if defined(_LIGHTINGTYPE_TEXEL) && !defined(PASS_META)
			d.worldSpacePosition = CalculateTexelWorldPos(d.texcoord0.xy, d.worldSpacePosition, _BaseMap_TexelSize);
			d.worldSpaceViewDir = CalculateTexelWorldViewDir(d.worldSpaceViewDir, d.worldSpacePosition);
			d.worldSpaceNormal = SafeNormalize(CalculateTexelWorldPos(d.texcoord0.xy, d.worldSpaceNormal, _BaseMap_TexelSize));
		#endif

		if (!_EnableMaskMap)
		{
			maskMap.r = 0;
			maskMap.g = mainTex.a;
			maskMap.b = 1;
		}

		o.Metallic = maskMap.r;
		o.Smoothness = remap01(_SmoothRemap.x, _SmoothRemap.y, maskMap.g);
		o.Occlusion =  maskMap.b;

		// Store lighting values in these
		float3 mainLightDir;
		float3 mainLightCol;
		float mainLightAtten;
		MainLight(mainLightDir, mainLightCol, mainLightAtten);

		float3 mainLightDiffuse;
		float mainShadow = 1;
		float3 ambient;
		float3 mainSpec = 0;

		float3 addDiffuse = 0;
		float3 addSpec = 0;

		float3 specColor = lerp(1.0, o.Albedo, o.Metallic);
		float reflectStr = lerp(0.04, 1, o.Metallic);
		
		if (_Reflections)
			o.Albedo *= 1 - o.Metallic;

		float3 reflection = 0;
		
		if (_Reflections)
			reflection = GetReflections(d.worldSpaceNormal, d.worldSpaceViewDir, o.Smoothness);

		#if !defined (LIGHTMAP_ON)
			half4 shadowMask = unity_ProbesOcclusion;
		#else
			half4 shadowMask = SAMPLE_SHADOWMASK(d.texcoord1.xy);
		#endif

		//Get lighting data, either from vertex shader or calculate it here
		#if defined(_LIGHTINGTYPE_VERTEX) && !defined(PASS_META)

			float vsmoothness = _VertexLightSmoothness;

			#ifdef _VCOLSMOOTHNESS
				vsmoothness = remap01(_SmoothRemapVCol.x, _SmoothRemapVCol.y, v.vertexColor.a);
			#endif

			o.Smoothness = vsmoothness;

			mainLightDiffuse = min(d.extraV2F0.y, d.extraV2F0.z); // Min(mainLight, shadow)

			#ifdef _TOONSHADING
				mainLightDiffuse = SampleRamp(_ToonRamp, mainLightDiffuse);
			#endif

			mainLightDiffuse *= mainLightCol;
			ambient = d.extraV2F1.xyz;

			#ifndef _TOONSHADING
				mainSpec = d.extraV2F0.w * mainLightCol;
			#else
				mainSpec = SampleRamp(_SpecularRamp, d.extraV2F0.w) * mainLightCol;
			#endif

			float3 worldPos = d.worldSpacePosition;

			int pixelLights = GetAdditionalLightsCount();
			Light light;
			if (pixelLights >= 1)
			{
				light = GetAdditionalLight(0, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.x * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.x) * light.color;
				#endif
				
				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.x * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.x) * light.color * specColor;
				#endif
			}
			if (pixelLights >= 2)
			{
				light = GetAdditionalLight(1, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.y * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.y) * light.color;
				#endif

				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.y * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.y) * light.color * specColor;
				#endif
			}
			if (pixelLights >= 3)
			{
				light = GetAdditionalLight(2, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.z * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.z) * light.color;
				#endif

				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.z * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.z) * light.color * specColor;
				#endif
			}
			if (pixelLights >= 4)
			{
				light = GetAdditionalLight(3, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.w * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.w) * light.color;
				#endif

				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.w * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.w) * light.color * specColor;
				#endif
			}

			mainSpec *= _VertexSpecColor;
			addSpec *= _VertexSpecColor;
			//addDiffuse = d.extraV2F3.xyz;
			//addSpec = d.extraV2F4.xyz;
		#else

			float dirOcclusion, indirOcclusion;
			SSAO(d, dirOcclusion, indirOcclusion);

			mainShadow = MainLightShadows(d.worldSpacePosition, d.texcoord1.xy, d.texcoord0.xy);
			
			#ifndef _TOONSHADING
				mainLightDiffuse = GetDiffuse(d.worldSpaceNormal, mainLightDir) * mainLightAtten * mainLightCol * mainShadow * dirOcclusion;
			#else
				mainLightDiffuse = GetDiffuse(d.worldSpaceNormal, mainLightDir) * mainShadow * dirOcclusion;
				float3 ramp = SampleRamp(_ToonRamp, mainLightDiffuse.x);
				mainLightDiffuse = ramp * mainLightAtten * mainLightCol;
			#endif

			ambient = GetAmbient(d.worldSpaceNormal, d.texcoord1.xy, d.texcoord0.xy) * min(o.Occlusion,indirOcclusion);
			mainSpec = GetSpecular(mainLightDir, d.worldSpaceNormal, d.worldSpaceViewDir, o.Smoothness)*dirOcclusion;

			#ifdef _TOONSHADING
				mainSpec = SampleRamp(_SpecularRamp, mainSpec.x);
			#endif

			mainSpec *= mainLightCol * mainLightAtten * specColor;
			mainSpec *= mainShadow;

			AdditionalLights(d.worldSpacePosition, d.worldSpaceViewDir, d.worldSpaceNormal, o.Smoothness, specColor, addDiffuse, addSpec);

		#endif

		float perceptualRoughness = 1 - o.Smoothness;
		perceptualRoughness *= perceptualRoughness;

		float fresnel = pow(1.0 - saturate(dot(d.worldSpaceNormal, d.worldSpaceViewDir)), 4);
		reflectStr *= lerp(0.1, 1, GetLuminance(mainLightDiffuse));
		reflectStr = lerp(reflectStr, saturate(reflectStr*10), fresnel);

		reflection *= reflectStr * specColor;
		reflection /= perceptualRoughness + 0.5;

		if (_DoubleSizeDither) pixelPos /= 2.0;

		[branch]
		if (_DitherDiffuse)
		{
			ditherColor(mainLightDiffuse, pixelPos);
			ditherColor(addDiffuse, pixelPos);
		}

		[branch]
		if (_DitherSpec)
		{
			ditherColor(mainSpec, pixelPos);
			ditherColor(addSpec, pixelPos);
		}

		[branch]
		if (_DitherAmbient)
			ditherColor(ambient, pixelPos);

		o.Alpha = mainTex.a * _Color.a;

		//o.Albedo *= 1 - saturate(GetLuminance(mainSpec));
		//o.Albedo *= 1 - saturate(GetLuminance(addSpec));

		#if defined(RETROSHADER_ALPHA) && !defined(PASS_META)
			if (_PreserveSpecular)
			{
				float specStrength = saturate(GetLuminance(mainSpec));
				float reflectStrength = saturate(GetLuminance(reflection));
				o.Alpha = max(o.Alpha, max(specStrength, reflectStrength));
			}
		#endif

		#ifdef _ALPHATEST_ON
			clip(step(_Cutoff, o.Alpha)-0.1f);
		#endif

		if (_VColEmissive)
		{
			o.Albedo += d.vertexColor.rgb * max(0,_VertexEmissiveStr);
			o.Emission = d.vertexColor.rgb * max(0,_VertexEmissiveStr);
		}
		else
		{
			o.Albedo *= d.vertexColor.rgb;
		}

		float3 combinedAmbient = ambient * o.Albedo;

		// Apply lighting, but not for lightbaking pass
		#if !defined(PASS_META)
			o.Albedo *= mainLightDiffuse + addDiffuse;
			o.Albedo += combinedAmbient;
			o.Albedo += mainSpec * saturate(GetLuminance(mainLightDiffuse));
			o.Albedo += addSpec * saturate(GetLuminance(addDiffuse));
			o.Albedo += reflection;

			#if defined(_EMISSION)
				o.Albedo += emission;
			#endif
		#endif
		
		#if defined(RETROSHADER_ALPHA) && !defined(PASS_META)
			if (_Refraction)
			{
				float origAlpha = o.Alpha;
				float4 offsetPos = float4(d.worldSpacePosition + d.worldSpaceNormal * _RefractionStr, 1.0);
				offsetPos = mul(GetWorldToHClipMatrix(), offsetPos);
				float scale = 1.0;

				#if UNITY_UV_STARTS_AT_TOP
					scale = -1.0;
				#endif

				float4 grabPos = offsetPos *0.5f;
				grabPos.xy = float2(grabPos.x, grabPos.y*scale) + grabPos.w;
				grabPos.zw = offsetPos.zw;
				grabPos.xy /= grabPos.w;
				grabPos.xy = clamp(grabPos.xy, float2(0.0, 0.0), float2(1.0, 1.0));

				float3 refraction = GetSceneColor(grabPos.xy);
				float2 cubemapBlendFactors = pow(abs((grabPos.xy - 0.5) * 2.0),0.25);
				float blend = smoothstep(0.75f, 1, max(cubemapBlendFactors.x, cubemapBlendFactors.y));
				float3 refractDir = refract(-d.worldSpaceViewDir, d.worldSpaceNormal, 1 - _RefractionStr);
				float3 reflectProbe = GetReflections(-refractDir, refractDir, 1.0);
				refraction = lerp(refraction, reflectProbe, blend);

				o.Albedo = lerp(refraction, o.Albedo, origAlpha);
				o.Alpha = 1;
			}
		#endif

		o.Albedo = MixFog(o.Albedo, d.extraV2F0.x);
	}




        
            void ChainSurfaceFunction(inout Surface l, inout ShaderData d)
            {
                  Ext_SurfaceFunction0(l, d);
                 // Ext_SurfaceFunction1(l, d);
                 // Ext_SurfaceFunction2(l, d);
                 // Ext_SurfaceFunction3(l, d);
                 // Ext_SurfaceFunction4(l, d);
                 // Ext_SurfaceFunction5(l, d);
                 // Ext_SurfaceFunction6(l, d);
                 // Ext_SurfaceFunction7(l, d);
                 // Ext_SurfaceFunction8(l, d);
                 // Ext_SurfaceFunction9(l, d);
		           // Ext_SurfaceFunction10(l, d);
                 // Ext_SurfaceFunction11(l, d);
                 // Ext_SurfaceFunction12(l, d);
                 // Ext_SurfaceFunction13(l, d);
                 // Ext_SurfaceFunction14(l, d);
                 // Ext_SurfaceFunction15(l, d);
                 // Ext_SurfaceFunction16(l, d);
                 // Ext_SurfaceFunction17(l, d);
                 // Ext_SurfaceFunction18(l, d);
		           // Ext_SurfaceFunction19(l, d);
                 // Ext_SurfaceFunction20(l, d);
                 // Ext_SurfaceFunction21(l, d);
                 // Ext_SurfaceFunction22(l, d);
                 // Ext_SurfaceFunction23(l, d);
                 // Ext_SurfaceFunction24(l, d);
                 // Ext_SurfaceFunction25(l, d);
                 // Ext_SurfaceFunction26(l, d);
                 // Ext_SurfaceFunction27(l, d);
                 // Ext_SurfaceFunction28(l, d);
		           // Ext_SurfaceFunction29(l, d);
            }

            void ChainModifyVertex(inout VertexData v, inout VertexToPixel v2p, float4 time)
            {
                 ExtraV2F d;
                 
                 ZERO_INITIALIZE(ExtraV2F, d);
                 ZERO_INITIALIZE(Blackboard, d.blackboard);
                 // due to motion vectors in HDRP, we need to use the last
                 // time in certain spots. So if you are going to use _Time to adjust vertices,
                 // you need to use this time or motion vectors will break. 
                 d.time = time;

                   Ext_ModifyVertex0(v, d);
                 // Ext_ModifyVertex1(v, d);
                 // Ext_ModifyVertex2(v, d);
                 // Ext_ModifyVertex3(v, d);
                 // Ext_ModifyVertex4(v, d);
                 // Ext_ModifyVertex5(v, d);
                 // Ext_ModifyVertex6(v, d);
                 // Ext_ModifyVertex7(v, d);
                 // Ext_ModifyVertex8(v, d);
                 // Ext_ModifyVertex9(v, d);
                 // Ext_ModifyVertex10(v, d);
                 // Ext_ModifyVertex11(v, d);
                 // Ext_ModifyVertex12(v, d);
                 // Ext_ModifyVertex13(v, d);
                 // Ext_ModifyVertex14(v, d);
                 // Ext_ModifyVertex15(v, d);
                 // Ext_ModifyVertex16(v, d);
                 // Ext_ModifyVertex17(v, d);
                 // Ext_ModifyVertex18(v, d);
                 // Ext_ModifyVertex19(v, d);
                 // Ext_ModifyVertex20(v, d);
                 // Ext_ModifyVertex21(v, d);
                 // Ext_ModifyVertex22(v, d);
                 // Ext_ModifyVertex23(v, d);
                 // Ext_ModifyVertex24(v, d);
                 // Ext_ModifyVertex25(v, d);
                 // Ext_ModifyVertex26(v, d);
                 // Ext_ModifyVertex27(v, d);
                 // Ext_ModifyVertex28(v, d);
                 // Ext_ModifyVertex29(v, d);


                 // #if %EXTRAV2F0REQUIREKEY%
                  v2p.extraV2F0 = d.extraV2F0;
                 // #endif

                 // #if %EXTRAV2F1REQUIREKEY%
                  v2p.extraV2F1 = d.extraV2F1;
                 // #endif

                 // #if %EXTRAV2F2REQUIREKEY%
                  v2p.extraV2F2 = d.extraV2F2;
                 // #endif

                 // #if %EXTRAV2F3REQUIREKEY%
                  v2p.extraV2F3 = d.extraV2F3;
                 // #endif

                 // #if %EXTRAV2F4REQUIREKEY%
                  v2p.extraV2F4 = d.extraV2F4;
                 // #endif

                 // #if %EXTRAV2F5REQUIREKEY%
                 // v2p.extraV2F5 = d.extraV2F5;
                 // #endif

                 // #if %EXTRAV2F6REQUIREKEY%
                 // v2p.extraV2F6 = d.extraV2F6;
                 // #endif

                 // #if %EXTRAV2F7REQUIREKEY%
                 // v2p.extraV2F7 = d.extraV2F7;
                 // #endif
            }

            void ChainModifyTessellatedVertex(inout VertexData v, inout VertexToPixel v2p)
            {
               ExtraV2F d;
               ZERO_INITIALIZE(ExtraV2F, d);
               ZERO_INITIALIZE(Blackboard, d.blackboard);

               // #if %EXTRAV2F0REQUIREKEY%
                d.extraV2F0 = v2p.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
                d.extraV2F1 = v2p.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
                d.extraV2F2 = v2p.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
                d.extraV2F3 = v2p.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
                d.extraV2F4 = v2p.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // d.extraV2F5 = v2p.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // d.extraV2F6 = v2p.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // d.extraV2F7 = v2p.extraV2F7;
               // #endif


               // Ext_ModifyTessellatedVertex0(v, d);
               // Ext_ModifyTessellatedVertex1(v, d);
               // Ext_ModifyTessellatedVertex2(v, d);
               // Ext_ModifyTessellatedVertex3(v, d);
               // Ext_ModifyTessellatedVertex4(v, d);
               // Ext_ModifyTessellatedVertex5(v, d);
               // Ext_ModifyTessellatedVertex6(v, d);
               // Ext_ModifyTessellatedVertex7(v, d);
               // Ext_ModifyTessellatedVertex8(v, d);
               // Ext_ModifyTessellatedVertex9(v, d);
               // Ext_ModifyTessellatedVertex10(v, d);
               // Ext_ModifyTessellatedVertex11(v, d);
               // Ext_ModifyTessellatedVertex12(v, d);
               // Ext_ModifyTessellatedVertex13(v, d);
               // Ext_ModifyTessellatedVertex14(v, d);
               // Ext_ModifyTessellatedVertex15(v, d);
               // Ext_ModifyTessellatedVertex16(v, d);
               // Ext_ModifyTessellatedVertex17(v, d);
               // Ext_ModifyTessellatedVertex18(v, d);
               // Ext_ModifyTessellatedVertex19(v, d);
               // Ext_ModifyTessellatedVertex20(v, d);
               // Ext_ModifyTessellatedVertex21(v, d);
               // Ext_ModifyTessellatedVertex22(v, d);
               // Ext_ModifyTessellatedVertex23(v, d);
               // Ext_ModifyTessellatedVertex24(v, d);
               // Ext_ModifyTessellatedVertex25(v, d);
               // Ext_ModifyTessellatedVertex26(v, d);
               // Ext_ModifyTessellatedVertex27(v, d);
               // Ext_ModifyTessellatedVertex28(v, d);
               // Ext_ModifyTessellatedVertex29(v, d);

               // #if %EXTRAV2F0REQUIREKEY%
                v2p.extraV2F0 = d.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
                v2p.extraV2F1 = d.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
                v2p.extraV2F2 = d.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
                v2p.extraV2F3 = d.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
                v2p.extraV2F4 = d.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // v2p.extraV2F5 = d.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // v2p.extraV2F6 = d.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // v2p.extraV2F7 = d.extraV2F7;
               // #endif
            }

            void ChainFinalColorForward(inout Surface l, inout ShaderData d, inout half4 color)
            {
               //   Ext_FinalColorForward0(l, d, color);
               //   Ext_FinalColorForward1(l, d, color);
               //   Ext_FinalColorForward2(l, d, color);
               //   Ext_FinalColorForward3(l, d, color);
               //   Ext_FinalColorForward4(l, d, color);
               //   Ext_FinalColorForward5(l, d, color);
               //   Ext_FinalColorForward6(l, d, color);
               //   Ext_FinalColorForward7(l, d, color);
               //   Ext_FinalColorForward8(l, d, color);
               //   Ext_FinalColorForward9(l, d, color);
               //  Ext_FinalColorForward10(l, d, color);
               //  Ext_FinalColorForward11(l, d, color);
               //  Ext_FinalColorForward12(l, d, color);
               //  Ext_FinalColorForward13(l, d, color);
               //  Ext_FinalColorForward14(l, d, color);
               //  Ext_FinalColorForward15(l, d, color);
               //  Ext_FinalColorForward16(l, d, color);
               //  Ext_FinalColorForward17(l, d, color);
               //  Ext_FinalColorForward18(l, d, color);
               //  Ext_FinalColorForward19(l, d, color);
               //  Ext_FinalColorForward20(l, d, color);
               //  Ext_FinalColorForward21(l, d, color);
               //  Ext_FinalColorForward22(l, d, color);
               //  Ext_FinalColorForward23(l, d, color);
               //  Ext_FinalColorForward24(l, d, color);
               //  Ext_FinalColorForward25(l, d, color);
               //  Ext_FinalColorForward26(l, d, color);
               //  Ext_FinalColorForward27(l, d, color);
               //  Ext_FinalColorForward28(l, d, color);
               //  Ext_FinalColorForward29(l, d, color);
            }

            void ChainFinalGBufferStandard(inout Surface s, inout ShaderData d, inout half4 GBuffer0, inout half4 GBuffer1, inout half4 GBuffer2, inout half4 outEmission, inout half4 outShadowMask)
            {
               //   Ext_FinalGBufferStandard0(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard1(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard2(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard3(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard4(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard5(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard6(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard7(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard8(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard9(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard10(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard11(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard12(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard13(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard14(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard15(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard16(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard17(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard18(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard19(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard20(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard21(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard22(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard23(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard24(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard25(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard26(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard27(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard28(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard29(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
            }



            

         ShaderData CreateShaderData(VertexToPixel i
                  #if NEED_FACING
                     , bool facing
                  #endif
         )
         {
            ShaderData d = (ShaderData)0;
            d.clipPos = i.pos;
            d.worldSpacePosition = i.worldPos;

            d.worldSpaceNormal = normalize(i.worldNormal);
            d.worldSpaceTangent = normalize(i.worldTangent.xyz);
            d.tangentSign = i.worldTangent.w;
            float3 bitangent = cross(i.worldTangent.xyz, i.worldNormal) * d.tangentSign * -1;
            

            d.TBNMatrix = float3x3(d.worldSpaceTangent, bitangent, d.worldSpaceNormal);
            d.worldSpaceViewDir = normalize(_WorldSpaceCameraPos - i.worldPos);

            d.tangentSpaceViewDir = mul(d.TBNMatrix, d.worldSpaceViewDir);
             d.texcoord0 = i.texcoord0;
             d.texcoord1 = i.texcoord1;
            // d.texcoord2 = i.texcoord2;

            // #if %TEXCOORD3REQUIREKEY%
            // d.texcoord3 = i.texcoord3;
            // #endif

            // d.isFrontFace = facing;
            // #if %VERTEXCOLORREQUIREKEY%
             d.vertexColor = i.vertexColor;
            // #endif

            // these rarely get used, so we back transform them. Usually will be stripped.
            #if _HDRP
                // d.localSpacePosition = mul(unity_WorldToObject, float4(GetCameraRelativePositionWS(i.worldPos), 1)).xyz;
            #else
                // d.localSpacePosition = mul(unity_WorldToObject, float4(i.worldPos, 1)).xyz;
            #endif
            // d.localSpaceNormal = normalize(mul((float3x3)unity_WorldToObject, i.worldNormal));
            // d.localSpaceTangent = normalize(mul((float3x3)unity_WorldToObject, i.worldTangent.xyz));

            // #if %SCREENPOSREQUIREKEY%
             d.screenPos = i.screenPos;
             d.screenUV = (i.screenPos.xy / i.screenPos.w);
            // #endif


            // #if %EXTRAV2F0REQUIREKEY%
             d.extraV2F0 = i.extraV2F0;
            // #endif

            // #if %EXTRAV2F1REQUIREKEY%
             d.extraV2F1 = i.extraV2F1;
            // #endif

            // #if %EXTRAV2F2REQUIREKEY%
             d.extraV2F2 = i.extraV2F2;
            // #endif

            // #if %EXTRAV2F3REQUIREKEY%
             d.extraV2F3 = i.extraV2F3;
            // #endif

            // #if %EXTRAV2F4REQUIREKEY%
             d.extraV2F4 = i.extraV2F4;
            // #endif

            // #if %EXTRAV2F5REQUIREKEY%
            // d.extraV2F5 = i.extraV2F5;
            // #endif

            // #if %EXTRAV2F6REQUIREKEY%
            // d.extraV2F6 = i.extraV2F6;
            // #endif

            // #if %EXTRAV2F7REQUIREKEY%
            // d.extraV2F7 = i.extraV2F7;
            // #endif

            return d;
         }
         

            
         #if defined(_PASSSHADOW)
            float3 _LightDirection;
            float3 _LightPosition;
         #endif

         // vertex shader
         VertexToPixel Vert (VertexData v)
         {
           
           VertexToPixel o = (VertexToPixel)0;

           UNITY_SETUP_INSTANCE_ID(v);
           UNITY_TRANSFER_INSTANCE_ID(v, o);
           UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);


#if !_TESSELLATION_ON
           ChainModifyVertex(v, o, _Time);
#endif

            o.texcoord0 = v.texcoord0;
            o.texcoord1 = v.texcoord1;
           // o.texcoord2 = v.texcoord2;

           // #if %TEXCOORD3REQUIREKEY%
           // o.texcoord3 = v.texcoord3;
           // #endif

           // #if %VERTEXCOLORREQUIREKEY%
            o.vertexColor = v.vertexColor;
           // #endif
           
           VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
           o.worldPos = TransformObjectToWorld(v.vertex.xyz);
           o.worldNormal = TransformObjectToWorldNormal(v.normal);
           o.worldTangent = float4(TransformObjectToWorldDir(v.tangent.xyz), v.tangent.w);

          // For some very odd reason, in 2021.2, we can't use Unity's defines, but have to use our own..
          #if _PASSSHADOW
              #if _CASTING_PUNCTUAL_LIGHT_SHADOW
                 float3 lightDirectionWS = normalize(_LightPosition - o.worldPos);
              #else
                 float3 lightDirectionWS = _LightDirection;
              #endif
              // Define shadow pass specific clip position for Universal
              o.pos = TransformWorldToHClip(ApplyShadowBias(o.worldPos, o.worldNormal, lightDirectionWS));
              #if UNITY_REVERSED_Z
                  o.pos.z = min(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #else
                  o.pos.z = max(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #endif
          #elif _PASSMETA
              o.pos = MetaVertexPosition(float4(v.vertex.xyz, 0), v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST);
          #else
              o.pos = TransformWorldToHClip(o.worldPos);
          #endif

          // #if %SCREENPOSREQUIREKEY%
           o.screenPos = ComputeScreenPos(o.pos, _ProjectionParams.x);
          // #endif

          #if _PASSFORWARD || _PASSGBUFFER
              float2 uv1 = v.texcoord1.xy;
              OUTPUT_LIGHTMAP_UV(uv1, unity_LightmapST, o.lightmapUV);
               o.texcoord1.xy = uv1;
              OUTPUT_SH(o.worldNormal, o.sh);
              #if defined(DYNAMICLIGHTMAP_ON)
                   o.dynamicLightmapUV.xy = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
              #endif
          #endif

          #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
              half fogFactor = 0;
              #if defined(_FOG_FRAGMENT)
                fogFactor = ComputeFogFactor(o.pos.z);
              #endif
              #if _BAKEDLIT
                 o.fogFactorAndVertexLight = half4(fogFactor, 0, 0, 0);
              #else
                 half3 vertexLight = VertexLighting(o.worldPos, o.worldNormal);
                 o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);
              #endif
          #endif

          #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
             o.shadowCoord = GetShadowCoord(vertexInput);
          #endif

           return o;
         }


            

            // fragment shader
            half4 Frag (VertexToPixel IN
            #ifdef _DEPTHOFFSET_ON
              , out float outputDepth : SV_Depth
            #endif
            #if NEED_FACING
               , bool facing : SV_IsFrontFace
            #endif
            ) : SV_Target
            {
               UNITY_SETUP_INSTANCE_ID(IN);
               UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

               ShaderData d = CreateShaderData(IN
                  #if NEED_FACING
                     , facing
                  #endif
               );
               Surface l = (Surface)0;

               #ifdef _DEPTHOFFSET_ON
                  l.outputDepth = outputDepth;
               #endif

               l.Albedo = half3(0.5, 0.5, 0.5);
               l.Normal = float3(0,0,1);
               l.Occlusion = 1;
               l.Alpha = 1;

               ChainSurfaceFunction(l, d);

               #ifdef _DEPTHOFFSET_ON
                  outputDepth = l.outputDepth;
               #endif

               return 0;

            }

         ENDHLSL

      }


      
        Pass
        {
            Name "Meta"
            Tags 
            { 
                "LightMode" = "Meta"
            }

            Cull Off

            HLSLPROGRAM

               #pragma vertex Vert
   #pragma fragment Frag

            #pragma target 3.0

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
        
            #define SHADERPASS SHADERPASS_META
            #define _PASSMETA 1


            
	#pragma multi_compile_local _LIGHTINGTYPE_PIXEL _LIGHTINGTYPE_VERTEX _LIGHTINGTYPE_TEXEL
	#pragma multi_compile_local _TEXTUREFILTERING_NEAREST _TEXTUREFILTERING_LINEAR _TEXTUREFILTERING_N64
	#pragma shader_feature_local _VERTEXJITTER
	#pragma shader_feature_local _VCOLSMOOTHNESS
	#pragma shader_feature_local _TOONSHADING
	#pragma shader_feature_local _EMISSION
	#pragma shader_feature_local _ALPHATEST_ON
	#pragma shader_feature_local _AFFINE_MAPPING
	#undef _UNLIT


   #define _URP 1

#define _UNLIT 1
#define _USINGTEXCOORD1 1



            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Version.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/MetaInput.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

                  #undef WorldNormalVector
      #define WorldNormalVector(data, normal) mul(normal, data.TBNMatrix)
      
      #define UnityObjectToWorldNormal(normal) mul(GetObjectToWorldMatrix(), normal)

      #define _WorldSpaceLightPos0 _MainLightPosition
      
      #define UNITY_DECLARE_TEX2D(name) TEXTURE2D(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2D_NOSAMPLER(name) TEXTURE2D(name);
      #define UNITY_DECLARE_TEX2DARRAY(name) TEXTURE2D_ARRAY(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(name) TEXTURE2D_ARRAY(name);

      #define UNITY_SAMPLE_TEX2DARRAY(tex,coord)            SAMPLE_TEXTURE2D_ARRAY(tex, sampler##tex, coord.xy, coord.z)
      #define UNITY_SAMPLE_TEX2DARRAY_LOD(tex,coord,lod)    SAMPLE_TEXTURE2D_ARRAY_LOD(tex, sampler##tex, coord.xy, coord.z, lod)
      #define UNITY_SAMPLE_TEX2D(tex, coord)                SAMPLE_TEXTURE2D(tex, sampler##tex, coord)
      #define UNITY_SAMPLE_TEX2D_SAMPLER(tex, samp, coord)  SAMPLE_TEXTURE2D(tex, sampler##samp, coord)

      #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod)   SAMPLE_TEXTURE2D_LOD(tex, sampler_##tex, coord, lod)
      #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) SAMPLE_TEXTURE2D_LOD (tex, sampler##samplertex,coord, lod)
     
      #if defined(UNITY_COMPILER_HLSL)
         #define UNITY_INITIALIZE_OUTPUT(type,name) name = (type)0;
      #else
         #define UNITY_INITIALIZE_OUTPUT(type,name)
      #endif

      #define sampler2D_float sampler2D
      #define sampler2D_half sampler2D

      

      // data across stages, stripped like the above.
      struct VertexToPixel
      {
         float4 pos : SV_POSITION;
         float3 worldPos : TEXCOORD0;
         float3 worldNormal : TEXCOORD1;
         float4 worldTangent : TEXCOORD2;
          float4 texcoord0 : TEXCOORD3;
          float4 texcoord1 : TEXCOORD4;
         // float4 texcoord2 : TEXCOORD5;

         // #if %TEXCOORD3REQUIREKEY%
         // float4 texcoord3 : TEXCOORD6;
         // #endif

         // #if %SCREENPOSREQUIREKEY%
          float4 screenPos : TEXCOORD7;
         // #endif

         // #if %VERTEXCOLORREQUIREKEY%
          half4 vertexColor : COLOR;
         // #endif

         #if defined(LIGHTMAP_ON)
            float2 lightmapUV : TEXCOORD8;
         #endif
         #if defined(DYNAMICLIGHTMAP_ON)
            float2 dynamicLightmapUV : TEXCOORD9;
         #endif
         #if !defined(LIGHTMAP_ON)
            float3 sh : TEXCOORD10;
         #endif

         #if defined(VARYINGS_NEED_FOG_AND_VERTEX_LIGHT)
            float4 fogFactorAndVertexLight : TEXCOORD11;
         #endif

         #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
           float4 shadowCoord : TEXCOORD12;
         #endif

         // #if %EXTRAV2F0REQUIREKEY%
          float4 extraV2F0 : TEXCOORD13;
         // #endif

         // #if %EXTRAV2F1REQUIREKEY%
          float4 extraV2F1 : TEXCOORD14;
         // #endif

         // #if %EXTRAV2F2REQUIREKEY%
          float4 extraV2F2 : TEXCOORD15;
         // #endif

         // #if %EXTRAV2F3REQUIREKEY%
          float4 extraV2F3 : TEXCOORD16;
         // #endif

         // #if %EXTRAV2F4REQUIREKEY%
          float4 extraV2F4 : TEXCOORD17;
         // #endif

         // #if %EXTRAV2F5REQUIREKEY%
         // float4 extraV2F5 : TEXCOORD18;
         // #endif

         // #if %EXTRAV2F6REQUIREKEY%
         // float4 extraV2F6 : TEXCOORD19;
         // #endif

         // #if %EXTRAV2F7REQUIREKEY%
         // float4 extraV2F7 : TEXCOORD20;
         // #endif

         #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
         #endif
         #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
         #endif
         #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
         #endif
         #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
         #endif
      };


            
            
            // data describing the user output of a pixel
            struct Surface
            {
               half3 Albedo;
               half Height;
               half3 Normal;
               half Smoothness;
               half3 Emission;
               half Metallic;
               half3 Specular;
               half Occlusion;
               half SpecularPower; // for simple lighting
               half Alpha;
               float outputDepth; // if written, SV_Depth semantic is used. ShaderData.clipPos.z is unused value
               // HDRP Only
               half SpecularOcclusion;
               half SubsurfaceMask;
               half Thickness;
               half CoatMask;
               half CoatSmoothness;
               half Anisotropy;
               half IridescenceMask;
               half IridescenceThickness;
               int DiffusionProfileHash;
               float SpecularAAThreshold;
               float SpecularAAScreenSpaceVariance;
               // requires _OVERRIDE_BAKEDGI to be defined, but is mapped in all pipelines
               float3 DiffuseGI;
               float3 BackDiffuseGI;
               float3 SpecularGI;
               // requires _OVERRIDE_SHADOWMASK to be defines
               float4 ShadowMask;
            };

            // Data the user declares in blackboard blocks
            struct Blackboard
            {
                
                float blackboardDummyData;
            };

            // data the user might need, this will grow to be big. But easy to strip
            struct ShaderData
            {
               float4 clipPos; // SV_POSITION
               float3 localSpacePosition;
               float3 localSpaceNormal;
               float3 localSpaceTangent;
        
               float3 worldSpacePosition;
               float3 worldSpaceNormal;
               float3 worldSpaceTangent;
               float tangentSign;

               float3 worldSpaceViewDir;
               float3 tangentSpaceViewDir;

               float4 texcoord0;
               float4 texcoord1;
               float4 texcoord2;
               float4 texcoord3;

               float2 screenUV;
               float4 screenPos;

               float4 vertexColor;
               bool isFrontFace;

               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;

               float3x3 TBNMatrix;
               Blackboard blackboard;
            };

            struct VertexData
            {
               #if SHADER_TARGET > 30
               // uint vertexID : SV_VertexID;
               #endif
               float4 vertex : POSITION;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;

               // optimize out mesh coords when not in use by user or lighting system
               #if _URP && (_USINGTEXCOORD1 || _PASSMETA || _PASSFORWARD || _PASSGBUFFER)
                  float4 texcoord1 : TEXCOORD1;
               #endif

               #if _URP && (_USINGTEXCOORD2 || _PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && defined(DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif

               #if _STANDARD && (_USINGTEXCOORD1 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER || _PASSFORWARDADD) && LIGHTMAP_ON)))
                  float4 texcoord1 : TEXCOORD1;
               #endif
               #if _STANDARD && (_USINGTEXCOORD2 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif


               #if _HDRP
                  float4 texcoord1 : TEXCOORD1;
                  float4 texcoord2 : TEXCOORD2;
               #endif

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
                float4 vertexColor : COLOR;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD4; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity    : TEXCOORD5; // Add Precomputed Velocity (Alembic computes velocities on runtime side).
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct TessVertex 
            {
               float4 vertex : INTERNALTESSPOS;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;
               float4 texcoord1 : TEXCOORD1;
               float4 texcoord2 : TEXCOORD2;

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
                float4 vertexColor : COLOR;
               // #endif

               // #if %EXTRAV2F0REQUIREKEY%
                float4 extraV2F0 : TEXCOORD5;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
                float4 extraV2F1 : TEXCOORD6;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
                float4 extraV2F2 : TEXCOORD7;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
                float4 extraV2F3 : TEXCOORD8;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
                float4 extraV2F4 : TEXCOORD9;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // float4 extraV2F5 : TEXCOORD10;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // float4 extraV2F6 : TEXCOORD11;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // float4 extraV2F7 : TEXCOORD12;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD13; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity : TEXCOORD14;
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
               UNITY_VERTEX_OUTPUT_STEREO
            };

            struct ExtraV2F
            {
               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;
               Blackboard blackboard;
               float4 time;
            };


            float3 WorldToTangentSpace(ShaderData d, float3 normal)
            {
               return mul(d.TBNMatrix, normal);
            }

            float3 TangentToWorldSpace(ShaderData d, float3 normal)
            {
               return mul(normal, d.TBNMatrix);
            }

            // in this case, make standard more like SRPs, because we can't fix
            // unity_WorldToObject in HDRP, since it already does macro-fu there

            #if _STANDARD
               float3 TransformWorldToObject(float3 p) { return mul(unity_WorldToObject, float4(p, 1)); };
               float3 TransformObjectToWorld(float3 p) { return mul(unity_ObjectToWorld, float4(p, 1)); };
               float4 TransformWorldToObject(float4 p) { return mul(unity_WorldToObject, p); };
               float4 TransformObjectToWorld(float4 p) { return mul(unity_ObjectToWorld, p); };
               float4x4 GetWorldToObjectMatrix() { return unity_WorldToObject; }
               float4x4 GetObjectToWorldMatrix() { return unity_ObjectToWorld; }
               #if (defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (SHADER_TARGET_SURFACE_ANALYSIS && !SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod) tex.SampleLevel (sampler##tex,coord, lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) tex.SampleLevel (sampler##samplertex,coord, lod)
              #else
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord,lod) tex2D (tex,coord,0,lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord,lod) tex2D (tex,coord,0,lod)
              #endif

               #undef GetWorldToObjectMatrix()

               #define GetWorldToObjectMatrix()   unity_WorldToObject


            #endif

            float3 GetCameraWorldPosition()
            {
               #if _HDRP
                  return GetCameraRelativePositionWS(_WorldSpaceCameraPos);
               #else
                  return _WorldSpaceCameraPos;
               #endif
            }

            #if _GRABPASSUSED
               #if _STANDARD
                  TEXTURE2D(%GRABTEXTURE%);
                  SAMPLER(sampler_%GRABTEXTURE%);
               #endif

               half3 GetSceneColor(float2 uv)
               {
                  #if _STANDARD
                     return SAMPLE_TEXTURE2D(%GRABTEXTURE%, sampler_%GRABTEXTURE%, uv).rgb;
                  #else
                     return SHADERGRAPH_SAMPLE_SCENE_COLOR(uv);
                  #endif
               }
            #endif


      
            #if _STANDARD
               UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
               float GetSceneDepth(float2 uv) { return SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv)); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv)); } 
            #else
               float GetSceneDepth(float2 uv) { return SHADERGRAPH_SAMPLE_SCENE_DEPTH(uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv), _ZBufferParams); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv), _ZBufferParams); } 
            #endif

            float3 GetWorldPositionFromDepthBuffer(float2 uv, float3 worldSpaceViewDir)
            {
               float eye = GetLinearEyeDepth(uv);
               float3 camView = mul((float3x3)GetObjectToWorldMatrix(), transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V)) [2].xyz);

               float dt = dot(worldSpaceViewDir, camView);
               float3 div = worldSpaceViewDir/dt;
               float3 wpos = (eye * div) + GetCameraWorldPosition();
               return wpos;
            }

            #if _HDRP
            float3 ObjectToWorldSpacePosition(float3 pos)
            {
               return GetAbsolutePositionWS(TransformObjectToWorld(pos));
            }
            #else
            float3 ObjectToWorldSpacePosition(float3 pos)
            {
               return TransformObjectToWorld(pos);
            }
            #endif

            #if _STANDARD
               UNITY_DECLARE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture);
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  float4 depthNorms = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture, uv);
                  float3 norms = DecodeViewNormalStereo(depthNorms);
                  norms = mul((float3x3)GetWorldToViewMatrix(), norms) * 0.5 + 0.5;
                  return norms;
               }
            #elif _HDRP
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  NormalData nd;
                  DecodeFromNormalBuffer(_ScreenSize.xy * uv, nd);
                  return nd.normalWS;
               }
            #elif _URP
               #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                  #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareNormalsTexture.hlsl"
               #endif

               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                     return SampleSceneNormals(uv);
                  #else
                     float3 wpos = GetWorldPositionFromDepthBuffer(uv, worldSpaceViewDir);
                     return normalize(-cross(ddx(wpos), ddy(wpos))) * 0.5 + 0.5;
                  #endif

                }
             #endif

             #if _HDRP

               half3 UnpackNormalmapRGorAG(half4 packednormal)
               {
                     // This do the trick
                  packednormal.x *= packednormal.w;

                  half3 normal;
                  normal.xy = packednormal.xy * 2 - 1;
                  normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                  return normal;
               }
               half3 UnpackNormal(half4 packednormal)
               {
                  #if defined(UNITY_NO_DXT5nm)
                     return packednormal.xyz * 2 - 1;
                  #else
                     return UnpackNormalmapRGorAG(packednormal);
                  #endif
               }
               #endif
               #if _HDRP || _URP

               half3 UnpackScaleNormal(half4 packednormal, half scale)
               {
                 #ifndef UNITY_NO_DXT5nm
                   // Unpack normal as DXT5nm (1, y, 1, x) or BC5 (x, y, 0, 1)
                   // Note neutral texture like "bump" is (0, 0, 1, 1) to work with both plain RGB normal and DXT5nm/BC5
                   packednormal.x *= packednormal.w;
                 #endif
                   half3 normal;
                   normal.xy = (packednormal.xy * 2 - 1) * scale;
                   normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                   return normal;
               }	

             #endif


            void GetSun(out float3 lightDir, out float3 color)
            {
               lightDir = float3(0.5, 0.5, 0);
               color = 1;
               #if _HDRP
                  if (_DirectionalLightCount > 0)
                  {
                     DirectionalLightData light = _DirectionalLightDatas[0];
                     lightDir = -light.forward.xyz;
                     color = light.color;
                  }
               #elif _STANDARD
			         lightDir = normalize(_WorldSpaceLightPos0.xyz);
                  color = _LightColor0.rgb;
               #elif _URP
	               Light light = GetMainLight();
	               lightDir = light.direction;
	               color = light.color;
               #endif
            }


            
            CBUFFER_START(UnityPerMaterial)

               

	int _VColEmissive;
	float _VertexEmissiveStr;
	half4 _Color;
	int _HalfLambert;
	int _EnableMaskMap;
	int _ForceNearest;
	int _TextureFiltering;
	int _SpecularType;
	int _Reflections;
	float _NormalStrength;
	float2 _SmoothRemap;
	float2 _SmoothRemapVCol;
	float _VertexLightSmoothness;
	float3 _VertexSpecColor;
	float3 _EmissionColor;
	float4 _TilingOffset;
	float _Cutoff;
	float _AffineMapFactor;
	int _VertexJitterResX;
	int _VertexJitterResY;
	int _DoubleSizeDither;
	int _MaskTint;
	float _LightmapEmissionStr;
	
	int _DitherAmbient;
	int _DitherDiffuse;
	int _DitherSpec;

	float4 _BaseMap_TexelSize;
	float4 _MaskMap_TexelSize;
	float4 _BumpMap_TexelSize;
	float4 _EmissionMap_TexelSize;




            CBUFFER_END

            

            

            #ifdef unity_WorldToObject
#undef unity_WorldToObject
#endif
#ifdef unity_ObjectToWorld
#undef unity_ObjectToWorld
#endif
#define unity_ObjectToWorld GetObjectToWorldMatrix()
#define unity_WorldToObject GetWorldToObjectMatrix()


	/**********
	* TODO:
	* Vertex Colors
	* Meta Pass?
	**********/

	TEXTURE2D(_ToonRamp);
	TEXTURE2D(_SpecularRamp);
	SamplerState sampler_linear_clamp;

	TEXTURE2D(_BaseMap);
	SamplerState sampler_linear_repeat;
	SamplerState sampler_trilinear_repeat;
	SamplerState sampler_point_repeat;

	#if defined(_TEXTUREFILTERING_LINEAR) || defined(PASS_META)
		#define MAINSMP sampler_linear_repeat
	#else
		#define MAINSMP sampler_point_repeat
	#endif

	static const uint bayerPattern[] =
	{
		0, 32,  8, 40,  2, 34, 10, 42,   /* 8x8 Bayer ordered dithering  */
		48, 16, 56, 24, 50, 18, 58, 26,  /* pattern.  Each input pixel   */
		12, 44,  4, 36, 14, 46,  6, 38,  /* is scaled to the 0..63 range */
		60, 28, 52, 20, 62, 30, 54, 22,  /* before looking in this table */
		3, 35, 11, 43,  1, 33,  9, 41,   /* to determine the action.     */
		51, 19, 59, 27, 49, 17, 57, 25,
		15, 47,  7, 39, 13, 45,  5, 37,
		63, 31, 55, 23, 61, 29, 53, 21
	};

	float ditherValue(float2 screenPos)
	{
		uint stippleOffset = ((uint) screenPos.y % 8) * 8 + ((uint) screenPos.x % 8);
		uint byte = bayerPattern[stippleOffset];
		return byte / 64.0f;
	}

	float GetLuminance(float3 col)
	{
		return dot(col, float3(0.299f, 0.587f, 0.114f));
	}

	void ditherColor(inout float3 input, float2 screenPos)
	{
		input.x = input.x < ditherValue(screenPos) ? input.x*0.25 : input.x;
		input.y = input.y < ditherValue(screenPos) ? input.y*0.25 : input.y;
		input.z = input.z < ditherValue(screenPos) ? input.z*0.25 : input.z;
	}

	// This function has been basically ripped straight from https://forum.unity.com/threads/the-quest-for-efficient-per-texel-lighting.529948/
	// So big thanks to GreatestBear, and truepak182, without whom this shader wouldn't be nearly as cool
	float3 CalculateTexelWorldPos(float2 originalUV, float3 originalWorldPos, float4 texelSize)
	{
		// 1.) Calculate how much the texture UV coords need to
		//     shift to be at the center of the nearest texel.
		float2 centerUV = floor(originalUV * (texelSize.zw))/texelSize.zw + (texelSize.xy/2.0);
		float2 dUV = (centerUV - originalUV);
	
		// 2b.) Calculate how much the texture coords vary over fragment space.
		//      This essentially defines a 2x2 matrix that gets
		//      texture space (UV) deltas from fragment space (ST) deltas
		// Note: I call fragment space (S,T) to disambiguate.
		float2 dUVdS = ddx(originalUV);
		float2 dUVdT = ddy(originalUV);
	
		// 2c.) Invert the fragment from texture matrix
		float2x2 dSTdUV = float2x2(dUVdT[1], -dUVdT[0], -dUVdS[1], dUVdS[0])*(1.0f/(dUVdS[0]*dUVdT[1]-dUVdT[0]*dUVdS[1]));
	
	
		// 2d.) Convert the UV delta to a fragment space delta
		float2 dST = mul(dSTdUV , dUV);
	
		// 2e.) Calculate how much the world coords vary over fragment space.
		float3 dXYZdS = ddx(originalWorldPos);
		float3 dXYZdT = ddy(originalWorldPos);
	
		// 2f.) Finally, convert our fragment space delta to a world space delta
		// And be sure to clamp it to SOMETHING in case the derivative calc went insane
		// Here I clamp it to -1 to 1 unit in unity, which should be orders of magnitude greater
		// than the size of any texel.
		float3 dXYZ = clamp(dXYZdS * dST[0] + dXYZdT * dST[1], -1, 1);
	
		// 3.) Transform the snapped UV back to world space
		return originalWorldPos + dXYZ;
	}

	float3 CalculateTexelWorldViewDir(float3 viewDirectionWS, float3 positionWS)
	{
		//Recalculate view direction according to recalculated texelized inputData.positionWS
		float3 viewDirectionWSCorrected = GetCameraPositionWS() - positionWS;
		//This needs to be done to make corrected viewDirectionWSCorrected same length as the original. If omitted, specular will not be displayed properly
		float viewPositionMult = length(viewDirectionWS) / length(viewDirectionWSCorrected);

		return viewDirectionWSCorrected * viewPositionMult;
	}

	float2 SnapUVToTexelCenter(float2 originalUV, float4 texelSize)
	{
		float2 centerUV = floor(originalUV * (texelSize.zw))/texelSize.zw + (texelSize.xy/2.0);
		float2 dUV = (centerUV - originalUV);

		return originalUV + dUV;
	}


	float3 SampleRamp(Texture2D ramp, float coord)
	{
		return SAMPLE_TEXTURE2D_LOD(ramp, sampler_linear_clamp, saturate(float2(pow(coord,0.5), 0.5)), 0).xyz;
	}

	float invLerp(float from, float to, float value){
		return (value - from) / (to - from);
	}

	float remap(float origFrom, float origTo, float targetFrom, float targetTo, float value){
		float rel = invLerp(origFrom, origTo, value);
		return lerp(targetFrom, targetTo, rel);
	}

	float remap01(float targetFrom, float targetTo, float value){
		float rel = invLerp(0, 1, value);
		return lerp(targetFrom, targetTo, rel);

	}

	void MainLight(out float3 direction, out float3 color, out float distanceAtten)
	{
		Light mainLight = GetMainLight();
		direction = mainLight.direction;
		color = mainLight.color;
		distanceAtten = mainLight.distanceAttenuation;
	}

	float MainLightShadows(float3 worldPos, float2 lightmapUV = float2(0.0,0.0), float2 origUV = float2(0.0,0.0))
	{
		float4 shadowCoord = TransformWorldToShadowCoord(worldPos);

		#if !defined (LIGHTMAP_ON) || defined(_LIGHTINGTYPE_VERTEX)
			half4 shadowMask = unity_ProbesOcclusion;
		#else
			lightmapUV = lightmapUV * unity_LightmapST.xy + unity_LightmapST.zw;

			#ifdef _LIGHTINGTYPE_TEXEL
				lightmapUV = CalculateTexelWorldPos(origUV, float3(lightmapUV.x, lightmapUV.y, 1), _BaseMap_TexelSize).xy;
			#endif

			half4 shadowMask = SAMPLE_SHADOWMASK(lightmapUV);
		#endif

		return MainLightShadow(shadowCoord, worldPos, shadowMask, _MainLightOcclusionProbes);
	}

	float3 GetAmbient(float3 worldNormal, float2 lightmapUV, float2 origUV = float2(0.0f, 0.0f))
	{
		#ifdef LIGHTMAP_ON
			lightmapUV = lightmapUV * unity_LightmapST.xy + unity_LightmapST.zw;

			#ifdef _LIGHTINGTYPE_TEXEL
				lightmapUV = CalculateTexelWorldPos(origUV, float3(lightmapUV.x, lightmapUV.y, 1), _BaseMap_TexelSize).xy;
			#endif

			#ifdef _LIGHTINGTYPE_VERTEX
				float4 lightmap = SAMPLE_TEXTURE2D_LOD(unity_Lightmap, sampler_linear_repeat, lightmapUV, 0);
				half3 bakedGI = DecodeLightmap(lightmap,half4(LIGHTMAP_HDR_MULTIPLIER, LIGHTMAP_HDR_EXPONENT, 0.0h, 0.0h));
			#else
				half3 bakedGI = SampleLightmap(lightmapUV, worldNormal);//DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, lightmapUV),half4(LIGHTMAP_HDR_MULTIPLIER, LIGHTMAP_HDR_EXPONENT, 0.0h, 0.0h));
			#endif

			#if defined(_MIXED_LIGHTING_SUBTRACTIVE)
				Light mainLight = GetMainLight();
				bakedGI = SubtractDirectMainLightFromLightmap(mainLight, worldNormal, bakedGI);
			#endif

			return bakedGI;
		#else
			return SampleSH(worldNormal);
		#endif
	}

	float GetSpecular(float3 lightDir, float3 worldNormal, float3 viewDir, float smoothness)
	{
		float SpecSmoothness = exp2(10 * smoothness + 1).x;
		[branch]
		if (_SpecularType == 0) //Cook-Torrance
		{
			float roughness = 1 - smoothness;
			roughness *= roughness;

			float3 h = SafeNormalize(lightDir + viewDir);
			float nh = saturate(dot(worldNormal, h));
			float lh = saturate(dot(lightDir, h));
			float d = nh * nh * (roughness * roughness - 1.0) + 1.00001;
			float normTerm = roughness * 4.0 + 2.0;
			float specTerm = roughness * roughness;
			specTerm /= (d * d) * max(0.1, lh * lh) * normTerm;
			return specTerm*0.04;
		}
		else if (_SpecularType == 1) //Phong
		{
			float3 r = reflect(-lightDir, worldNormal);
			float rdotv = max(0, dot(r, viewDir));

			return pow(rdotv, SpecSmoothness)*smoothness;
		}
		else if (_SpecularType == 2) //Blinn-Phong
		{
			float3 h = SafeNormalize(lightDir+viewDir);
			float ndh = saturate(dot(worldNormal, h));

			return pow(ndh, SpecSmoothness)*smoothness;
		}
		else
		{
			return 0;
		}
	}

	float GetDiffuse(float3 lightDir, float3 worldNormal)
	{
		float diffuse = dot(lightDir, worldNormal);

		if (_HalfLambert)
			diffuse = diffuse * 0.5 + 0.5;

		return max(0,diffuse);
	}


	void AdditionalLights(float3 worldPos, float3 viewDir, float3 worldNormal, float InSmoothness, float3 specColor, out float3 Diffuse, out float3 Specular, float dirOcclusion = 1)
	{
		float3 diff = 0;
		float3 spec = 0;

		#if !defined (LIGHTMAP_ON)
			half4 shadowMask = unity_ProbesOcclusion;
		#else
			half4 shadowMask = half4(1, 1, 1, 1);
		#endif

		int pixelLights = GetAdditionalLightsCount();
		for (int i = 0; i < pixelLights; ++i)
		{
			Light light = GetAdditionalLight(i, worldPos, shadowMask);
			
			#ifdef _TOONSHADING
				float diffMask = GetDiffuse(light.direction, worldNormal) * light.shadowAttenuation * light.distanceAttenuation * dirOcclusion;
				diff += SampleRamp(_ToonRamp, diffMask) * light.color;
			#else
				diff += GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.color * light.shadowAttenuation * dirOcclusion;
			#endif

			#ifdef _TOONSHADING
				float specMask = GetSpecular(light.direction, worldNormal, viewDir, InSmoothness) * light.distanceAttenuation * light.shadowAttenuation * dirOcclusion;
				spec += SampleRamp(_SpecularRamp, specMask) * light.color * specColor;
			#else
				spec += GetSpecular(light.direction, worldNormal, viewDir, InSmoothness) * light.distanceAttenuation * light.color * specColor * light.shadowAttenuation * dirOcclusion;
			#endif
		}
		Diffuse = diff;
		Specular = spec;
	}


	float3 GetReflections(float3 worldNormal, float3 viewDir, float smoothness)
	{
		float perceptualRoughness = 1 - smoothness;
		perceptualRoughness *= perceptualRoughness;
		float mip = PerceptualRoughnessToMipmapLevel(perceptualRoughness);
		float3 sample = SHADERGRAPH_REFLECTION_PROBE(viewDir, worldNormal, mip);
		return sample.rgb;
	}

	void SSAO(ShaderData d, out float directOcclusion, out float indirectOcclusion)
	{
		#if defined(_SCREEN_SPACE_OCCLUSION) && !defined(RETROSHADER_ALPHA)
			float2 uv = d.screenUV;

			#ifdef _LIGHTINGTYPE_TEXEL
				float3 pos = CalculateTexelWorldPos(d.texcoord0, float3(uv.x, uv.y, 1.0), _BaseMap_TexelSize);
				uv = pos.xy;
			#endif

			AmbientOcclusionFactor aoFactor = GetScreenSpaceAmbientOcclusion(uv);
			directOcclusion = aoFactor.directAmbientOcclusion;
			indirectOcclusion = aoFactor.indirectAmbientOcclusion;
		#else
			directOcclusion = 1;
			indirectOcclusion = 1;
		#endif
	}

	// Courtesy of Ben Golus
	// https://forum.unity.com/threads/shader-ideas-for-replicating-n64-bi-linear-filtering.936119/#post-6116207
	half4 sampleN64(Texture2D tex, float2 uv, float4 texelSize)
	{
		// texel coordinates
		float2 texels = uv * texelSize.zw;

		// calculate mip level
		float2 dx = ddx(texels);
		float2 dy = ddy(texels);
		float delta_max_sqr = max(dot(dx, dx), dot(dy, dy));
		float mip = max(0.0, 0.5 * log2(delta_max_sqr));

		// scale texel sizes and texel coordinates to handle mip levels properly
		float scale = pow(2,floor(mip));
		texelSize.xy *= scale;
		texelSize.zw /= scale;
		texels = texels / scale - 0.5;

		// calculate blend for the three points of the tri-filter
		float2 fracTexels = frac(texels);
		float3 blend = float3(
			abs(fracTexels.x+fracTexels.y-1),
			min(abs(fracTexels.xx-float2(0,1)), abs(fracTexels.yy-float2(1,0)))
		);

		// calculate equivalents of point filtered uvs for the three points
		float2 uvA = (floor(texels + fracTexels.yx) + 0.5) * texelSize.xy;
		float2 uvB = (floor(texels) + float2(1.5, 0.5)) * texelSize.xy;
		float2 uvC = (floor(texels) + float2(0.5, 1.5)) * texelSize.xy;

		// sample points
		half4 A = SAMPLE_TEXTURE2D_LOD(tex, MAINSMP, uvA, mip);
		half4 B = SAMPLE_TEXTURE2D_LOD(tex, MAINSMP, uvB, mip);
		half4 C = SAMPLE_TEXTURE2D_LOD(tex, MAINSMP, uvC, mip);

		// blend and return
		return A * blend.x + B * blend.y + C * blend.z;
	}

	half4 sampleTex(Texture2D tex, float2 uv, float4 texelSize)
	{
		#ifdef _TEXTUREFILTERING_N64
			return sampleN64(tex, uv, texelSize);
		#else
			return SAMPLE_TEXTURE2D(tex, MAINSMP, uv);
		#endif
	}

	TEXTURE2D(_MaskMap);
	TEXTURE2D(_BumpMap);
	TEXTURE2D(_EmissionMap);

	void Ext_ModifyVertex0 (inout VertexData v, inout ExtraV2F d)
	{
		float4 clipPos = mul(GetWorldToHClipMatrix(), mul(unity_ObjectToWorld, v.vertex));

		

		#if defined(_LIGHTINGTYPE_VERTEX) && !defined(PASS_META)

			float smoothness = _VertexLightSmoothness;

			#ifdef _VCOLSMOOTHNESS
				smoothness = remap01(_SmoothRemapVCol.x, _SmoothRemapVCol.y, v.vertexColor.a);
			#endif

			float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
			float3 worldNormal = SafeNormalize(mul(unity_ObjectToWorld, v.normal));
			float3 viewDir = normalize(_WorldSpaceCameraPos - worldPos);
			float3 mainLightDir;
			float3 mainLightCol;
			float mainLightAtten;
			MainLight(mainLightDir, mainLightCol, mainLightAtten);

			d.extraV2F0.y = GetDiffuse(mainLightDir, worldNormal) * mainLightAtten;
			d.extraV2F1.xyz = GetAmbient(worldNormal, v.texcoord1);
			d.extraV2F0.z = MainLightShadows(worldPos, v.texcoord1);
			d.extraV2F0.w = GetSpecular(mainLightDir, worldNormal, viewDir, smoothness) * mainLightAtten * d.extraV2F0.z;

			#if !defined (LIGHTMAP_ON)
				half4 shadowMask = unity_ProbesOcclusion;
			#else
				half4 shadowMask = half4(1, 1, 1, 1);
			#endif

			// Have to store each light in a separate vector component so we can apply toon shading in fragment later - toon shading has to be applied per light to make sense
			// This limits us to max 4 lights, would need to use 2 more interpolators for 8
			int pixelLights = GetAdditionalLightsCount();
			Light light;
			if (pixelLights >= 1)
			{
				light = GetAdditionalLight(0, worldPos, shadowMask);
				d.extraV2F3.x = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.x = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
			if (pixelLights >= 2)
			{
				light = GetAdditionalLight(1, worldPos, shadowMask);
				d.extraV2F3.y = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.y = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
			if (pixelLights >= 3)
			{
				light = GetAdditionalLight(2, worldPos, shadowMask);
				d.extraV2F3.z = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.z = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
			if (pixelLights >= 4)
			{
				light = GetAdditionalLight(3, worldPos, shadowMask);
				d.extraV2F3.w = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.w = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
		#endif

		#if defined(_VERTEXJITTER) && !defined(_PASSSHADOW)
			float4 vert = clipPos;
			d.extraV2F2.xyz = mul(UNITY_MATRIX_I_VP, vert);
			vert.xyz = clipPos.xyz/clipPos.w;
			vert.x = floor(_VertexJitterResX * vert.x) / _VertexJitterResX;
			vert.y = floor(_VertexJitterResY * vert.y) / _VertexJitterResY;
			vert.xyz *= clipPos.w;
			v.vertex = mul(unity_WorldToObject, mul(UNITY_MATRIX_I_VP, vert));
		#endif

		d.extraV2F0.x = ComputeFogFactor(clipPos.z);
		v.texcoord0.xy = v.texcoord0.xy * _TilingOffset.xy + _TilingOffset.zw;

		#if defined(_AFFINE_MAPPING)
			_AffineMapFactor = min(1, _AffineMapFactor);
			float warpFactor = max(clipPos.w, 0.02); //Prevents flipping
			warpFactor = lerp(0.5, warpFactor, _AffineMapFactor);

			v.texcoord0.xy *= warpFactor;
			d.extraV2F1.w = warpFactor;
		#endif
	}

	
	void Ext_SurfaceFunction0 (inout Surface o, ShaderData d)
	{
		#if defined(_AFFINE_MAPPING)
			d.texcoord0.xy /= d.extraV2F1.w;
		#endif
		// Set up input data
		float4 mainTex = sampleTex(_BaseMap, d.texcoord0.xy, _BaseMap_TexelSize);
		o.Albedo = mainTex.rgb;
		float4 maskMap = sampleTex(_MaskMap, d.texcoord0.xy, _BaseMap_TexelSize);

		

		if (_MaskTint)
		{
			float3 tintedAlbedo = o.Albedo * _Color.rgb;
			o.Albedo = lerp(o.Albedo, tintedAlbedo, maskMap.a);
		}
		else
		{
			o.Albedo *= _Color.rgb;
		}

		#ifdef _EMISSION
			float3 emission = sampleTex(_EmissionMap, d.texcoord0.xy, _EmissionMap_TexelSize) * _EmissionColor;
			o.Emission = emission * _LightmapEmissionStr;

		#endif

		float2 pixelPos = d.clipPos.xy;//floor(d.screenUV.xy * _ScreenParams.xy);

		#if defined(_LIGHTINGTYPE_TEXEL) && !defined(PASS_META)
			o.Normal = UnpackScaleNormal(SAMPLE_TEXTURE2D(_BumpMap, sampler_linear_repeat, d.texcoord0.xy), _NormalStrength);
		#else
			o.Normal = UnpackScaleNormal(sampleTex(_BumpMap, d.texcoord0.xy, _BumpMap_TexelSize), _NormalStrength);
		#endif

		//o.Normal = SafeNormalize(o.Normal);

		#if defined(_VERTEXJITTER) && !defined(_PASSSHADOW)
			d.worldSpacePosition = d.extraV2F2.xyz;
		#endif

		d.worldSpaceNormal = TransformTangentToWorld(o.Normal, d.TBNMatrix);
		float3 unSnappedViewDir = d.worldSpaceViewDir;
		// Snap world position, view direction, and normal to the texel
		#if defined(_LIGHTINGTYPE_TEXEL) && !defined(PASS_META)
			d.worldSpacePosition = CalculateTexelWorldPos(d.texcoord0.xy, d.worldSpacePosition, _BaseMap_TexelSize);
			d.worldSpaceViewDir = CalculateTexelWorldViewDir(d.worldSpaceViewDir, d.worldSpacePosition);
			d.worldSpaceNormal = SafeNormalize(CalculateTexelWorldPos(d.texcoord0.xy, d.worldSpaceNormal, _BaseMap_TexelSize));
		#endif

		if (!_EnableMaskMap)
		{
			maskMap.r = 0;
			maskMap.g = mainTex.a;
			maskMap.b = 1;
		}

		o.Metallic = maskMap.r;
		o.Smoothness = remap01(_SmoothRemap.x, _SmoothRemap.y, maskMap.g);
		o.Occlusion =  maskMap.b;

		// Store lighting values in these
		float3 mainLightDir;
		float3 mainLightCol;
		float mainLightAtten;
		MainLight(mainLightDir, mainLightCol, mainLightAtten);

		float3 mainLightDiffuse;
		float mainShadow = 1;
		float3 ambient;
		float3 mainSpec = 0;

		float3 addDiffuse = 0;
		float3 addSpec = 0;

		float3 specColor = lerp(1.0, o.Albedo, o.Metallic);
		float reflectStr = lerp(0.04, 1, o.Metallic);
		
		if (_Reflections)
			o.Albedo *= 1 - o.Metallic;

		float3 reflection = 0;
		
		if (_Reflections)
			reflection = GetReflections(d.worldSpaceNormal, d.worldSpaceViewDir, o.Smoothness);

		#if !defined (LIGHTMAP_ON)
			half4 shadowMask = unity_ProbesOcclusion;
		#else
			half4 shadowMask = SAMPLE_SHADOWMASK(d.texcoord1.xy);
		#endif

		//Get lighting data, either from vertex shader or calculate it here
		#if defined(_LIGHTINGTYPE_VERTEX) && !defined(PASS_META)

			float vsmoothness = _VertexLightSmoothness;

			#ifdef _VCOLSMOOTHNESS
				vsmoothness = remap01(_SmoothRemapVCol.x, _SmoothRemapVCol.y, v.vertexColor.a);
			#endif

			o.Smoothness = vsmoothness;

			mainLightDiffuse = min(d.extraV2F0.y, d.extraV2F0.z); // Min(mainLight, shadow)

			#ifdef _TOONSHADING
				mainLightDiffuse = SampleRamp(_ToonRamp, mainLightDiffuse);
			#endif

			mainLightDiffuse *= mainLightCol;
			ambient = d.extraV2F1.xyz;

			#ifndef _TOONSHADING
				mainSpec = d.extraV2F0.w * mainLightCol;
			#else
				mainSpec = SampleRamp(_SpecularRamp, d.extraV2F0.w) * mainLightCol;
			#endif

			float3 worldPos = d.worldSpacePosition;

			int pixelLights = GetAdditionalLightsCount();
			Light light;
			if (pixelLights >= 1)
			{
				light = GetAdditionalLight(0, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.x * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.x) * light.color;
				#endif
				
				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.x * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.x) * light.color * specColor;
				#endif
			}
			if (pixelLights >= 2)
			{
				light = GetAdditionalLight(1, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.y * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.y) * light.color;
				#endif

				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.y * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.y) * light.color * specColor;
				#endif
			}
			if (pixelLights >= 3)
			{
				light = GetAdditionalLight(2, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.z * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.z) * light.color;
				#endif

				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.z * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.z) * light.color * specColor;
				#endif
			}
			if (pixelLights >= 4)
			{
				light = GetAdditionalLight(3, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.w * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.w) * light.color;
				#endif

				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.w * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.w) * light.color * specColor;
				#endif
			}

			mainSpec *= _VertexSpecColor;
			addSpec *= _VertexSpecColor;
			//addDiffuse = d.extraV2F3.xyz;
			//addSpec = d.extraV2F4.xyz;
		#else

			float dirOcclusion, indirOcclusion;
			SSAO(d, dirOcclusion, indirOcclusion);

			mainShadow = MainLightShadows(d.worldSpacePosition, d.texcoord1.xy, d.texcoord0.xy);
			
			#ifndef _TOONSHADING
				mainLightDiffuse = GetDiffuse(d.worldSpaceNormal, mainLightDir) * mainLightAtten * mainLightCol * mainShadow * dirOcclusion;
			#else
				mainLightDiffuse = GetDiffuse(d.worldSpaceNormal, mainLightDir) * mainShadow * dirOcclusion;
				float3 ramp = SampleRamp(_ToonRamp, mainLightDiffuse.x);
				mainLightDiffuse = ramp * mainLightAtten * mainLightCol;
			#endif

			ambient = GetAmbient(d.worldSpaceNormal, d.texcoord1.xy, d.texcoord0.xy) * min(o.Occlusion,indirOcclusion);
			mainSpec = GetSpecular(mainLightDir, d.worldSpaceNormal, d.worldSpaceViewDir, o.Smoothness)*dirOcclusion;

			#ifdef _TOONSHADING
				mainSpec = SampleRamp(_SpecularRamp, mainSpec.x);
			#endif

			mainSpec *= mainLightCol * mainLightAtten * specColor;
			mainSpec *= mainShadow;

			AdditionalLights(d.worldSpacePosition, d.worldSpaceViewDir, d.worldSpaceNormal, o.Smoothness, specColor, addDiffuse, addSpec);

		#endif

		float perceptualRoughness = 1 - o.Smoothness;
		perceptualRoughness *= perceptualRoughness;

		float fresnel = pow(1.0 - saturate(dot(d.worldSpaceNormal, d.worldSpaceViewDir)), 4);
		reflectStr *= lerp(0.1, 1, GetLuminance(mainLightDiffuse));
		reflectStr = lerp(reflectStr, saturate(reflectStr*10), fresnel);

		reflection *= reflectStr * specColor;
		reflection /= perceptualRoughness + 0.5;

		if (_DoubleSizeDither) pixelPos /= 2.0;

		[branch]
		if (_DitherDiffuse)
		{
			ditherColor(mainLightDiffuse, pixelPos);
			ditherColor(addDiffuse, pixelPos);
		}

		[branch]
		if (_DitherSpec)
		{
			ditherColor(mainSpec, pixelPos);
			ditherColor(addSpec, pixelPos);
		}

		[branch]
		if (_DitherAmbient)
			ditherColor(ambient, pixelPos);

		o.Alpha = mainTex.a * _Color.a;

		//o.Albedo *= 1 - saturate(GetLuminance(mainSpec));
		//o.Albedo *= 1 - saturate(GetLuminance(addSpec));

		#if defined(RETROSHADER_ALPHA) && !defined(PASS_META)
			if (_PreserveSpecular)
			{
				float specStrength = saturate(GetLuminance(mainSpec));
				float reflectStrength = saturate(GetLuminance(reflection));
				o.Alpha = max(o.Alpha, max(specStrength, reflectStrength));
			}
		#endif

		#ifdef _ALPHATEST_ON
			clip(step(_Cutoff, o.Alpha)-0.1f);
		#endif

		if (_VColEmissive)
		{
			o.Albedo += d.vertexColor.rgb * max(0,_VertexEmissiveStr);
			o.Emission = d.vertexColor.rgb * max(0,_VertexEmissiveStr);
		}
		else
		{
			o.Albedo *= d.vertexColor.rgb;
		}

		float3 combinedAmbient = ambient * o.Albedo;

		// Apply lighting, but not for lightbaking pass
		#if !defined(PASS_META)
			o.Albedo *= mainLightDiffuse + addDiffuse;
			o.Albedo += combinedAmbient;
			o.Albedo += mainSpec * saturate(GetLuminance(mainLightDiffuse));
			o.Albedo += addSpec * saturate(GetLuminance(addDiffuse));
			o.Albedo += reflection;

			#if defined(_EMISSION)
				o.Albedo += emission;
			#endif
		#endif
		
		#if defined(RETROSHADER_ALPHA) && !defined(PASS_META)
			if (_Refraction)
			{
				float origAlpha = o.Alpha;
				float4 offsetPos = float4(d.worldSpacePosition + d.worldSpaceNormal * _RefractionStr, 1.0);
				offsetPos = mul(GetWorldToHClipMatrix(), offsetPos);
				float scale = 1.0;

				#if UNITY_UV_STARTS_AT_TOP
					scale = -1.0;
				#endif

				float4 grabPos = offsetPos *0.5f;
				grabPos.xy = float2(grabPos.x, grabPos.y*scale) + grabPos.w;
				grabPos.zw = offsetPos.zw;
				grabPos.xy /= grabPos.w;
				grabPos.xy = clamp(grabPos.xy, float2(0.0, 0.0), float2(1.0, 1.0));

				float3 refraction = GetSceneColor(grabPos.xy);
				float2 cubemapBlendFactors = pow(abs((grabPos.xy - 0.5) * 2.0),0.25);
				float blend = smoothstep(0.75f, 1, max(cubemapBlendFactors.x, cubemapBlendFactors.y));
				float3 refractDir = refract(-d.worldSpaceViewDir, d.worldSpaceNormal, 1 - _RefractionStr);
				float3 reflectProbe = GetReflections(-refractDir, refractDir, 1.0);
				refraction = lerp(refraction, reflectProbe, blend);

				o.Albedo = lerp(refraction, o.Albedo, origAlpha);
				o.Alpha = 1;
			}
		#endif

		o.Albedo = MixFog(o.Albedo, d.extraV2F0.x);
	}




        
            void ChainSurfaceFunction(inout Surface l, inout ShaderData d)
            {
                  Ext_SurfaceFunction0(l, d);
                 // Ext_SurfaceFunction1(l, d);
                 // Ext_SurfaceFunction2(l, d);
                 // Ext_SurfaceFunction3(l, d);
                 // Ext_SurfaceFunction4(l, d);
                 // Ext_SurfaceFunction5(l, d);
                 // Ext_SurfaceFunction6(l, d);
                 // Ext_SurfaceFunction7(l, d);
                 // Ext_SurfaceFunction8(l, d);
                 // Ext_SurfaceFunction9(l, d);
		           // Ext_SurfaceFunction10(l, d);
                 // Ext_SurfaceFunction11(l, d);
                 // Ext_SurfaceFunction12(l, d);
                 // Ext_SurfaceFunction13(l, d);
                 // Ext_SurfaceFunction14(l, d);
                 // Ext_SurfaceFunction15(l, d);
                 // Ext_SurfaceFunction16(l, d);
                 // Ext_SurfaceFunction17(l, d);
                 // Ext_SurfaceFunction18(l, d);
		           // Ext_SurfaceFunction19(l, d);
                 // Ext_SurfaceFunction20(l, d);
                 // Ext_SurfaceFunction21(l, d);
                 // Ext_SurfaceFunction22(l, d);
                 // Ext_SurfaceFunction23(l, d);
                 // Ext_SurfaceFunction24(l, d);
                 // Ext_SurfaceFunction25(l, d);
                 // Ext_SurfaceFunction26(l, d);
                 // Ext_SurfaceFunction27(l, d);
                 // Ext_SurfaceFunction28(l, d);
		           // Ext_SurfaceFunction29(l, d);
            }

            void ChainModifyVertex(inout VertexData v, inout VertexToPixel v2p, float4 time)
            {
                 ExtraV2F d;
                 
                 ZERO_INITIALIZE(ExtraV2F, d);
                 ZERO_INITIALIZE(Blackboard, d.blackboard);
                 // due to motion vectors in HDRP, we need to use the last
                 // time in certain spots. So if you are going to use _Time to adjust vertices,
                 // you need to use this time or motion vectors will break. 
                 d.time = time;

                   Ext_ModifyVertex0(v, d);
                 // Ext_ModifyVertex1(v, d);
                 // Ext_ModifyVertex2(v, d);
                 // Ext_ModifyVertex3(v, d);
                 // Ext_ModifyVertex4(v, d);
                 // Ext_ModifyVertex5(v, d);
                 // Ext_ModifyVertex6(v, d);
                 // Ext_ModifyVertex7(v, d);
                 // Ext_ModifyVertex8(v, d);
                 // Ext_ModifyVertex9(v, d);
                 // Ext_ModifyVertex10(v, d);
                 // Ext_ModifyVertex11(v, d);
                 // Ext_ModifyVertex12(v, d);
                 // Ext_ModifyVertex13(v, d);
                 // Ext_ModifyVertex14(v, d);
                 // Ext_ModifyVertex15(v, d);
                 // Ext_ModifyVertex16(v, d);
                 // Ext_ModifyVertex17(v, d);
                 // Ext_ModifyVertex18(v, d);
                 // Ext_ModifyVertex19(v, d);
                 // Ext_ModifyVertex20(v, d);
                 // Ext_ModifyVertex21(v, d);
                 // Ext_ModifyVertex22(v, d);
                 // Ext_ModifyVertex23(v, d);
                 // Ext_ModifyVertex24(v, d);
                 // Ext_ModifyVertex25(v, d);
                 // Ext_ModifyVertex26(v, d);
                 // Ext_ModifyVertex27(v, d);
                 // Ext_ModifyVertex28(v, d);
                 // Ext_ModifyVertex29(v, d);


                 // #if %EXTRAV2F0REQUIREKEY%
                  v2p.extraV2F0 = d.extraV2F0;
                 // #endif

                 // #if %EXTRAV2F1REQUIREKEY%
                  v2p.extraV2F1 = d.extraV2F1;
                 // #endif

                 // #if %EXTRAV2F2REQUIREKEY%
                  v2p.extraV2F2 = d.extraV2F2;
                 // #endif

                 // #if %EXTRAV2F3REQUIREKEY%
                  v2p.extraV2F3 = d.extraV2F3;
                 // #endif

                 // #if %EXTRAV2F4REQUIREKEY%
                  v2p.extraV2F4 = d.extraV2F4;
                 // #endif

                 // #if %EXTRAV2F5REQUIREKEY%
                 // v2p.extraV2F5 = d.extraV2F5;
                 // #endif

                 // #if %EXTRAV2F6REQUIREKEY%
                 // v2p.extraV2F6 = d.extraV2F6;
                 // #endif

                 // #if %EXTRAV2F7REQUIREKEY%
                 // v2p.extraV2F7 = d.extraV2F7;
                 // #endif
            }

            void ChainModifyTessellatedVertex(inout VertexData v, inout VertexToPixel v2p)
            {
               ExtraV2F d;
               ZERO_INITIALIZE(ExtraV2F, d);
               ZERO_INITIALIZE(Blackboard, d.blackboard);

               // #if %EXTRAV2F0REQUIREKEY%
                d.extraV2F0 = v2p.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
                d.extraV2F1 = v2p.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
                d.extraV2F2 = v2p.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
                d.extraV2F3 = v2p.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
                d.extraV2F4 = v2p.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // d.extraV2F5 = v2p.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // d.extraV2F6 = v2p.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // d.extraV2F7 = v2p.extraV2F7;
               // #endif


               // Ext_ModifyTessellatedVertex0(v, d);
               // Ext_ModifyTessellatedVertex1(v, d);
               // Ext_ModifyTessellatedVertex2(v, d);
               // Ext_ModifyTessellatedVertex3(v, d);
               // Ext_ModifyTessellatedVertex4(v, d);
               // Ext_ModifyTessellatedVertex5(v, d);
               // Ext_ModifyTessellatedVertex6(v, d);
               // Ext_ModifyTessellatedVertex7(v, d);
               // Ext_ModifyTessellatedVertex8(v, d);
               // Ext_ModifyTessellatedVertex9(v, d);
               // Ext_ModifyTessellatedVertex10(v, d);
               // Ext_ModifyTessellatedVertex11(v, d);
               // Ext_ModifyTessellatedVertex12(v, d);
               // Ext_ModifyTessellatedVertex13(v, d);
               // Ext_ModifyTessellatedVertex14(v, d);
               // Ext_ModifyTessellatedVertex15(v, d);
               // Ext_ModifyTessellatedVertex16(v, d);
               // Ext_ModifyTessellatedVertex17(v, d);
               // Ext_ModifyTessellatedVertex18(v, d);
               // Ext_ModifyTessellatedVertex19(v, d);
               // Ext_ModifyTessellatedVertex20(v, d);
               // Ext_ModifyTessellatedVertex21(v, d);
               // Ext_ModifyTessellatedVertex22(v, d);
               // Ext_ModifyTessellatedVertex23(v, d);
               // Ext_ModifyTessellatedVertex24(v, d);
               // Ext_ModifyTessellatedVertex25(v, d);
               // Ext_ModifyTessellatedVertex26(v, d);
               // Ext_ModifyTessellatedVertex27(v, d);
               // Ext_ModifyTessellatedVertex28(v, d);
               // Ext_ModifyTessellatedVertex29(v, d);

               // #if %EXTRAV2F0REQUIREKEY%
                v2p.extraV2F0 = d.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
                v2p.extraV2F1 = d.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
                v2p.extraV2F2 = d.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
                v2p.extraV2F3 = d.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
                v2p.extraV2F4 = d.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // v2p.extraV2F5 = d.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // v2p.extraV2F6 = d.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // v2p.extraV2F7 = d.extraV2F7;
               // #endif
            }

            void ChainFinalColorForward(inout Surface l, inout ShaderData d, inout half4 color)
            {
               //   Ext_FinalColorForward0(l, d, color);
               //   Ext_FinalColorForward1(l, d, color);
               //   Ext_FinalColorForward2(l, d, color);
               //   Ext_FinalColorForward3(l, d, color);
               //   Ext_FinalColorForward4(l, d, color);
               //   Ext_FinalColorForward5(l, d, color);
               //   Ext_FinalColorForward6(l, d, color);
               //   Ext_FinalColorForward7(l, d, color);
               //   Ext_FinalColorForward8(l, d, color);
               //   Ext_FinalColorForward9(l, d, color);
               //  Ext_FinalColorForward10(l, d, color);
               //  Ext_FinalColorForward11(l, d, color);
               //  Ext_FinalColorForward12(l, d, color);
               //  Ext_FinalColorForward13(l, d, color);
               //  Ext_FinalColorForward14(l, d, color);
               //  Ext_FinalColorForward15(l, d, color);
               //  Ext_FinalColorForward16(l, d, color);
               //  Ext_FinalColorForward17(l, d, color);
               //  Ext_FinalColorForward18(l, d, color);
               //  Ext_FinalColorForward19(l, d, color);
               //  Ext_FinalColorForward20(l, d, color);
               //  Ext_FinalColorForward21(l, d, color);
               //  Ext_FinalColorForward22(l, d, color);
               //  Ext_FinalColorForward23(l, d, color);
               //  Ext_FinalColorForward24(l, d, color);
               //  Ext_FinalColorForward25(l, d, color);
               //  Ext_FinalColorForward26(l, d, color);
               //  Ext_FinalColorForward27(l, d, color);
               //  Ext_FinalColorForward28(l, d, color);
               //  Ext_FinalColorForward29(l, d, color);
            }

            void ChainFinalGBufferStandard(inout Surface s, inout ShaderData d, inout half4 GBuffer0, inout half4 GBuffer1, inout half4 GBuffer2, inout half4 outEmission, inout half4 outShadowMask)
            {
               //   Ext_FinalGBufferStandard0(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard1(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard2(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard3(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard4(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard5(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard6(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard7(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard8(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard9(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard10(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard11(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard12(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard13(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard14(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard15(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard16(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard17(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard18(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard19(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard20(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard21(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard22(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard23(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard24(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard25(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard26(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard27(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard28(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard29(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
            }



            

         ShaderData CreateShaderData(VertexToPixel i
                  #if NEED_FACING
                     , bool facing
                  #endif
         )
         {
            ShaderData d = (ShaderData)0;
            d.clipPos = i.pos;
            d.worldSpacePosition = i.worldPos;

            d.worldSpaceNormal = normalize(i.worldNormal);
            d.worldSpaceTangent = normalize(i.worldTangent.xyz);
            d.tangentSign = i.worldTangent.w;
            float3 bitangent = cross(i.worldTangent.xyz, i.worldNormal) * d.tangentSign * -1;
            

            d.TBNMatrix = float3x3(d.worldSpaceTangent, bitangent, d.worldSpaceNormal);
            d.worldSpaceViewDir = normalize(_WorldSpaceCameraPos - i.worldPos);

            d.tangentSpaceViewDir = mul(d.TBNMatrix, d.worldSpaceViewDir);
             d.texcoord0 = i.texcoord0;
             d.texcoord1 = i.texcoord1;
            // d.texcoord2 = i.texcoord2;

            // #if %TEXCOORD3REQUIREKEY%
            // d.texcoord3 = i.texcoord3;
            // #endif

            // d.isFrontFace = facing;
            // #if %VERTEXCOLORREQUIREKEY%
             d.vertexColor = i.vertexColor;
            // #endif

            // these rarely get used, so we back transform them. Usually will be stripped.
            #if _HDRP
                // d.localSpacePosition = mul(unity_WorldToObject, float4(GetCameraRelativePositionWS(i.worldPos), 1)).xyz;
            #else
                // d.localSpacePosition = mul(unity_WorldToObject, float4(i.worldPos, 1)).xyz;
            #endif
            // d.localSpaceNormal = normalize(mul((float3x3)unity_WorldToObject, i.worldNormal));
            // d.localSpaceTangent = normalize(mul((float3x3)unity_WorldToObject, i.worldTangent.xyz));

            // #if %SCREENPOSREQUIREKEY%
             d.screenPos = i.screenPos;
             d.screenUV = (i.screenPos.xy / i.screenPos.w);
            // #endif


            // #if %EXTRAV2F0REQUIREKEY%
             d.extraV2F0 = i.extraV2F0;
            // #endif

            // #if %EXTRAV2F1REQUIREKEY%
             d.extraV2F1 = i.extraV2F1;
            // #endif

            // #if %EXTRAV2F2REQUIREKEY%
             d.extraV2F2 = i.extraV2F2;
            // #endif

            // #if %EXTRAV2F3REQUIREKEY%
             d.extraV2F3 = i.extraV2F3;
            // #endif

            // #if %EXTRAV2F4REQUIREKEY%
             d.extraV2F4 = i.extraV2F4;
            // #endif

            // #if %EXTRAV2F5REQUIREKEY%
            // d.extraV2F5 = i.extraV2F5;
            // #endif

            // #if %EXTRAV2F6REQUIREKEY%
            // d.extraV2F6 = i.extraV2F6;
            // #endif

            // #if %EXTRAV2F7REQUIREKEY%
            // d.extraV2F7 = i.extraV2F7;
            // #endif

            return d;
         }
         

            
         #if defined(_PASSSHADOW)
            float3 _LightDirection;
            float3 _LightPosition;
         #endif

         // vertex shader
         VertexToPixel Vert (VertexData v)
         {
           
           VertexToPixel o = (VertexToPixel)0;

           UNITY_SETUP_INSTANCE_ID(v);
           UNITY_TRANSFER_INSTANCE_ID(v, o);
           UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);


#if !_TESSELLATION_ON
           ChainModifyVertex(v, o, _Time);
#endif

            o.texcoord0 = v.texcoord0;
            o.texcoord1 = v.texcoord1;
           // o.texcoord2 = v.texcoord2;

           // #if %TEXCOORD3REQUIREKEY%
           // o.texcoord3 = v.texcoord3;
           // #endif

           // #if %VERTEXCOLORREQUIREKEY%
            o.vertexColor = v.vertexColor;
           // #endif
           
           VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
           o.worldPos = TransformObjectToWorld(v.vertex.xyz);
           o.worldNormal = TransformObjectToWorldNormal(v.normal);
           o.worldTangent = float4(TransformObjectToWorldDir(v.tangent.xyz), v.tangent.w);

          // For some very odd reason, in 2021.2, we can't use Unity's defines, but have to use our own..
          #if _PASSSHADOW
              #if _CASTING_PUNCTUAL_LIGHT_SHADOW
                 float3 lightDirectionWS = normalize(_LightPosition - o.worldPos);
              #else
                 float3 lightDirectionWS = _LightDirection;
              #endif
              // Define shadow pass specific clip position for Universal
              o.pos = TransformWorldToHClip(ApplyShadowBias(o.worldPos, o.worldNormal, lightDirectionWS));
              #if UNITY_REVERSED_Z
                  o.pos.z = min(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #else
                  o.pos.z = max(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #endif
          #elif _PASSMETA
              o.pos = MetaVertexPosition(float4(v.vertex.xyz, 0), v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST);
          #else
              o.pos = TransformWorldToHClip(o.worldPos);
          #endif

          // #if %SCREENPOSREQUIREKEY%
           o.screenPos = ComputeScreenPos(o.pos, _ProjectionParams.x);
          // #endif

          #if _PASSFORWARD || _PASSGBUFFER
              float2 uv1 = v.texcoord1.xy;
              OUTPUT_LIGHTMAP_UV(uv1, unity_LightmapST, o.lightmapUV);
               o.texcoord1.xy = uv1;
              OUTPUT_SH(o.worldNormal, o.sh);
              #if defined(DYNAMICLIGHTMAP_ON)
                   o.dynamicLightmapUV.xy = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
              #endif
          #endif

          #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
              half fogFactor = 0;
              #if defined(_FOG_FRAGMENT)
                fogFactor = ComputeFogFactor(o.pos.z);
              #endif
              #if _BAKEDLIT
                 o.fogFactorAndVertexLight = half4(fogFactor, 0, 0, 0);
              #else
                 half3 vertexLight = VertexLighting(o.worldPos, o.worldNormal);
                 o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);
              #endif
          #endif

          #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
             o.shadowCoord = GetShadowCoord(vertexInput);
          #endif

           return o;
         }


            

            // fragment shader
            half4 Frag (VertexToPixel IN
               #if NEED_FACING
                  , bool facing : SV_IsFrontFace
               #endif
            ) : SV_Target
            {
               UNITY_SETUP_INSTANCE_ID(IN);

               ShaderData d = CreateShaderData(IN
                  #if NEED_FACING
                     , facing
                  #endif
               );

               Surface l = (Surface)0;

               l.Albedo = half3(0.5, 0.5, 0.5);
               l.Normal = float3(0,0,1);
               l.Occlusion = 1;
               l.Alpha = 1;

               ChainSurfaceFunction(l, d);

               MetaInput metaInput = (MetaInput)0;
               metaInput.Albedo = l.Albedo;
               metaInput.Emission = l.Emission;

               return MetaFragment(metaInput);

            }

         ENDHLSL

      }


      
        Pass
        {
            Name "DepthNormals"
            Tags
            {
               "LightMode" = "DepthNormals"
            }
    
            // Render State
            Cull Back
            Blend One Zero
            ZTest LEqual
            ZWrite On

            	Cull [_Cull]


            HLSLPROGRAM

               #pragma vertex Vert
   #pragma fragment Frag

            #pragma target 3.0

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma multi_compile_fog
            #pragma multi_compile_instancing
            #pragma multi_compile_fragment _ _GBUFFER_NORMALS_OCT // forward-only variant


            #define SHADERPASS SHADERPASS_DEPTHNORMALSONLY
            #define _PASSDEPTH 1
            #define _PASSDEPTHNORMALS 1


            
	#pragma multi_compile_local _LIGHTINGTYPE_PIXEL _LIGHTINGTYPE_VERTEX _LIGHTINGTYPE_TEXEL
	#pragma multi_compile_local _TEXTUREFILTERING_NEAREST _TEXTUREFILTERING_LINEAR _TEXTUREFILTERING_N64
	#pragma shader_feature_local _VERTEXJITTER
	#pragma shader_feature_local _VCOLSMOOTHNESS
	#pragma shader_feature_local _TOONSHADING
	#pragma shader_feature_local _EMISSION
	#pragma shader_feature_local _ALPHATEST_ON
	#pragma shader_feature_local _AFFINE_MAPPING
	#undef _UNLIT


   #define _URP 1

#define _UNLIT 1
#define _USINGTEXCOORD1 1


            // this has to be here or specular color will be ignored. Not in SG code
            #if _SIMPLELIT
               #define _SPECULAR_COLOR
            #endif


            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Version.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"


        

               #undef WorldNormalVector
      #define WorldNormalVector(data, normal) mul(normal, data.TBNMatrix)
      
      #define UnityObjectToWorldNormal(normal) mul(GetObjectToWorldMatrix(), normal)

      #define _WorldSpaceLightPos0 _MainLightPosition
      
      #define UNITY_DECLARE_TEX2D(name) TEXTURE2D(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2D_NOSAMPLER(name) TEXTURE2D(name);
      #define UNITY_DECLARE_TEX2DARRAY(name) TEXTURE2D_ARRAY(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(name) TEXTURE2D_ARRAY(name);

      #define UNITY_SAMPLE_TEX2DARRAY(tex,coord)            SAMPLE_TEXTURE2D_ARRAY(tex, sampler##tex, coord.xy, coord.z)
      #define UNITY_SAMPLE_TEX2DARRAY_LOD(tex,coord,lod)    SAMPLE_TEXTURE2D_ARRAY_LOD(tex, sampler##tex, coord.xy, coord.z, lod)
      #define UNITY_SAMPLE_TEX2D(tex, coord)                SAMPLE_TEXTURE2D(tex, sampler##tex, coord)
      #define UNITY_SAMPLE_TEX2D_SAMPLER(tex, samp, coord)  SAMPLE_TEXTURE2D(tex, sampler##samp, coord)

      #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod)   SAMPLE_TEXTURE2D_LOD(tex, sampler_##tex, coord, lod)
      #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) SAMPLE_TEXTURE2D_LOD (tex, sampler##samplertex,coord, lod)
     
      #if defined(UNITY_COMPILER_HLSL)
         #define UNITY_INITIALIZE_OUTPUT(type,name) name = (type)0;
      #else
         #define UNITY_INITIALIZE_OUTPUT(type,name)
      #endif

      #define sampler2D_float sampler2D
      #define sampler2D_half sampler2D

      

      // data across stages, stripped like the above.
      struct VertexToPixel
      {
         float4 pos : SV_POSITION;
         float3 worldPos : TEXCOORD0;
         float3 worldNormal : TEXCOORD1;
         float4 worldTangent : TEXCOORD2;
          float4 texcoord0 : TEXCOORD3;
          float4 texcoord1 : TEXCOORD4;
         // float4 texcoord2 : TEXCOORD5;

         // #if %TEXCOORD3REQUIREKEY%
         // float4 texcoord3 : TEXCOORD6;
         // #endif

         // #if %SCREENPOSREQUIREKEY%
          float4 screenPos : TEXCOORD7;
         // #endif

         // #if %VERTEXCOLORREQUIREKEY%
          half4 vertexColor : COLOR;
         // #endif

         #if defined(LIGHTMAP_ON)
            float2 lightmapUV : TEXCOORD8;
         #endif
         #if defined(DYNAMICLIGHTMAP_ON)
            float2 dynamicLightmapUV : TEXCOORD9;
         #endif
         #if !defined(LIGHTMAP_ON)
            float3 sh : TEXCOORD10;
         #endif

         #if defined(VARYINGS_NEED_FOG_AND_VERTEX_LIGHT)
            float4 fogFactorAndVertexLight : TEXCOORD11;
         #endif

         #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
           float4 shadowCoord : TEXCOORD12;
         #endif

         // #if %EXTRAV2F0REQUIREKEY%
          float4 extraV2F0 : TEXCOORD13;
         // #endif

         // #if %EXTRAV2F1REQUIREKEY%
          float4 extraV2F1 : TEXCOORD14;
         // #endif

         // #if %EXTRAV2F2REQUIREKEY%
          float4 extraV2F2 : TEXCOORD15;
         // #endif

         // #if %EXTRAV2F3REQUIREKEY%
          float4 extraV2F3 : TEXCOORD16;
         // #endif

         // #if %EXTRAV2F4REQUIREKEY%
          float4 extraV2F4 : TEXCOORD17;
         // #endif

         // #if %EXTRAV2F5REQUIREKEY%
         // float4 extraV2F5 : TEXCOORD18;
         // #endif

         // #if %EXTRAV2F6REQUIREKEY%
         // float4 extraV2F6 : TEXCOORD19;
         // #endif

         // #if %EXTRAV2F7REQUIREKEY%
         // float4 extraV2F7 : TEXCOORD20;
         // #endif

         #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
         #endif
         #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
         #endif
         #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
         #endif
         #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
         #endif
      };


         
            
            // data describing the user output of a pixel
            struct Surface
            {
               half3 Albedo;
               half Height;
               half3 Normal;
               half Smoothness;
               half3 Emission;
               half Metallic;
               half3 Specular;
               half Occlusion;
               half SpecularPower; // for simple lighting
               half Alpha;
               float outputDepth; // if written, SV_Depth semantic is used. ShaderData.clipPos.z is unused value
               // HDRP Only
               half SpecularOcclusion;
               half SubsurfaceMask;
               half Thickness;
               half CoatMask;
               half CoatSmoothness;
               half Anisotropy;
               half IridescenceMask;
               half IridescenceThickness;
               int DiffusionProfileHash;
               float SpecularAAThreshold;
               float SpecularAAScreenSpaceVariance;
               // requires _OVERRIDE_BAKEDGI to be defined, but is mapped in all pipelines
               float3 DiffuseGI;
               float3 BackDiffuseGI;
               float3 SpecularGI;
               // requires _OVERRIDE_SHADOWMASK to be defines
               float4 ShadowMask;
            };

            // Data the user declares in blackboard blocks
            struct Blackboard
            {
                
                float blackboardDummyData;
            };

            // data the user might need, this will grow to be big. But easy to strip
            struct ShaderData
            {
               float4 clipPos; // SV_POSITION
               float3 localSpacePosition;
               float3 localSpaceNormal;
               float3 localSpaceTangent;
        
               float3 worldSpacePosition;
               float3 worldSpaceNormal;
               float3 worldSpaceTangent;
               float tangentSign;

               float3 worldSpaceViewDir;
               float3 tangentSpaceViewDir;

               float4 texcoord0;
               float4 texcoord1;
               float4 texcoord2;
               float4 texcoord3;

               float2 screenUV;
               float4 screenPos;

               float4 vertexColor;
               bool isFrontFace;

               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;

               float3x3 TBNMatrix;
               Blackboard blackboard;
            };

            struct VertexData
            {
               #if SHADER_TARGET > 30
               // uint vertexID : SV_VertexID;
               #endif
               float4 vertex : POSITION;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;

               // optimize out mesh coords when not in use by user or lighting system
               #if _URP && (_USINGTEXCOORD1 || _PASSMETA || _PASSFORWARD || _PASSGBUFFER)
                  float4 texcoord1 : TEXCOORD1;
               #endif

               #if _URP && (_USINGTEXCOORD2 || _PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && defined(DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif

               #if _STANDARD && (_USINGTEXCOORD1 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER || _PASSFORWARDADD) && LIGHTMAP_ON)))
                  float4 texcoord1 : TEXCOORD1;
               #endif
               #if _STANDARD && (_USINGTEXCOORD2 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif


               #if _HDRP
                  float4 texcoord1 : TEXCOORD1;
                  float4 texcoord2 : TEXCOORD2;
               #endif

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
                float4 vertexColor : COLOR;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD4; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity    : TEXCOORD5; // Add Precomputed Velocity (Alembic computes velocities on runtime side).
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct TessVertex 
            {
               float4 vertex : INTERNALTESSPOS;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;
               float4 texcoord1 : TEXCOORD1;
               float4 texcoord2 : TEXCOORD2;

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
                float4 vertexColor : COLOR;
               // #endif

               // #if %EXTRAV2F0REQUIREKEY%
                float4 extraV2F0 : TEXCOORD5;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
                float4 extraV2F1 : TEXCOORD6;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
                float4 extraV2F2 : TEXCOORD7;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
                float4 extraV2F3 : TEXCOORD8;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
                float4 extraV2F4 : TEXCOORD9;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // float4 extraV2F5 : TEXCOORD10;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // float4 extraV2F6 : TEXCOORD11;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // float4 extraV2F7 : TEXCOORD12;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD13; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity : TEXCOORD14;
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
               UNITY_VERTEX_OUTPUT_STEREO
            };

            struct ExtraV2F
            {
               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;
               Blackboard blackboard;
               float4 time;
            };


            float3 WorldToTangentSpace(ShaderData d, float3 normal)
            {
               return mul(d.TBNMatrix, normal);
            }

            float3 TangentToWorldSpace(ShaderData d, float3 normal)
            {
               return mul(normal, d.TBNMatrix);
            }

            // in this case, make standard more like SRPs, because we can't fix
            // unity_WorldToObject in HDRP, since it already does macro-fu there

            #if _STANDARD
               float3 TransformWorldToObject(float3 p) { return mul(unity_WorldToObject, float4(p, 1)); };
               float3 TransformObjectToWorld(float3 p) { return mul(unity_ObjectToWorld, float4(p, 1)); };
               float4 TransformWorldToObject(float4 p) { return mul(unity_WorldToObject, p); };
               float4 TransformObjectToWorld(float4 p) { return mul(unity_ObjectToWorld, p); };
               float4x4 GetWorldToObjectMatrix() { return unity_WorldToObject; }
               float4x4 GetObjectToWorldMatrix() { return unity_ObjectToWorld; }
               #if (defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (SHADER_TARGET_SURFACE_ANALYSIS && !SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod) tex.SampleLevel (sampler##tex,coord, lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) tex.SampleLevel (sampler##samplertex,coord, lod)
              #else
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord,lod) tex2D (tex,coord,0,lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord,lod) tex2D (tex,coord,0,lod)
              #endif

               #undef GetWorldToObjectMatrix()

               #define GetWorldToObjectMatrix()   unity_WorldToObject


            #endif

            float3 GetCameraWorldPosition()
            {
               #if _HDRP
                  return GetCameraRelativePositionWS(_WorldSpaceCameraPos);
               #else
                  return _WorldSpaceCameraPos;
               #endif
            }

            #if _GRABPASSUSED
               #if _STANDARD
                  TEXTURE2D(%GRABTEXTURE%);
                  SAMPLER(sampler_%GRABTEXTURE%);
               #endif

               half3 GetSceneColor(float2 uv)
               {
                  #if _STANDARD
                     return SAMPLE_TEXTURE2D(%GRABTEXTURE%, sampler_%GRABTEXTURE%, uv).rgb;
                  #else
                     return SHADERGRAPH_SAMPLE_SCENE_COLOR(uv);
                  #endif
               }
            #endif


      
            #if _STANDARD
               UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
               float GetSceneDepth(float2 uv) { return SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv)); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv)); } 
            #else
               float GetSceneDepth(float2 uv) { return SHADERGRAPH_SAMPLE_SCENE_DEPTH(uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv), _ZBufferParams); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv), _ZBufferParams); } 
            #endif

            float3 GetWorldPositionFromDepthBuffer(float2 uv, float3 worldSpaceViewDir)
            {
               float eye = GetLinearEyeDepth(uv);
               float3 camView = mul((float3x3)GetObjectToWorldMatrix(), transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V)) [2].xyz);

               float dt = dot(worldSpaceViewDir, camView);
               float3 div = worldSpaceViewDir/dt;
               float3 wpos = (eye * div) + GetCameraWorldPosition();
               return wpos;
            }

            #if _HDRP
            float3 ObjectToWorldSpacePosition(float3 pos)
            {
               return GetAbsolutePositionWS(TransformObjectToWorld(pos));
            }
            #else
            float3 ObjectToWorldSpacePosition(float3 pos)
            {
               return TransformObjectToWorld(pos);
            }
            #endif

            #if _STANDARD
               UNITY_DECLARE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture);
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  float4 depthNorms = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture, uv);
                  float3 norms = DecodeViewNormalStereo(depthNorms);
                  norms = mul((float3x3)GetWorldToViewMatrix(), norms) * 0.5 + 0.5;
                  return norms;
               }
            #elif _HDRP
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  NormalData nd;
                  DecodeFromNormalBuffer(_ScreenSize.xy * uv, nd);
                  return nd.normalWS;
               }
            #elif _URP
               #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                  #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareNormalsTexture.hlsl"
               #endif

               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                     return SampleSceneNormals(uv);
                  #else
                     float3 wpos = GetWorldPositionFromDepthBuffer(uv, worldSpaceViewDir);
                     return normalize(-cross(ddx(wpos), ddy(wpos))) * 0.5 + 0.5;
                  #endif

                }
             #endif

             #if _HDRP

               half3 UnpackNormalmapRGorAG(half4 packednormal)
               {
                     // This do the trick
                  packednormal.x *= packednormal.w;

                  half3 normal;
                  normal.xy = packednormal.xy * 2 - 1;
                  normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                  return normal;
               }
               half3 UnpackNormal(half4 packednormal)
               {
                  #if defined(UNITY_NO_DXT5nm)
                     return packednormal.xyz * 2 - 1;
                  #else
                     return UnpackNormalmapRGorAG(packednormal);
                  #endif
               }
               #endif
               #if _HDRP || _URP

               half3 UnpackScaleNormal(half4 packednormal, half scale)
               {
                 #ifndef UNITY_NO_DXT5nm
                   // Unpack normal as DXT5nm (1, y, 1, x) or BC5 (x, y, 0, 1)
                   // Note neutral texture like "bump" is (0, 0, 1, 1) to work with both plain RGB normal and DXT5nm/BC5
                   packednormal.x *= packednormal.w;
                 #endif
                   half3 normal;
                   normal.xy = (packednormal.xy * 2 - 1) * scale;
                   normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                   return normal;
               }	

             #endif


            void GetSun(out float3 lightDir, out float3 color)
            {
               lightDir = float3(0.5, 0.5, 0);
               color = 1;
               #if _HDRP
                  if (_DirectionalLightCount > 0)
                  {
                     DirectionalLightData light = _DirectionalLightDatas[0];
                     lightDir = -light.forward.xyz;
                     color = light.color;
                  }
               #elif _STANDARD
			         lightDir = normalize(_WorldSpaceLightPos0.xyz);
                  color = _LightColor0.rgb;
               #elif _URP
	               Light light = GetMainLight();
	               lightDir = light.direction;
	               color = light.color;
               #endif
            }


            
         CBUFFER_START(UnityPerMaterial)

            

	int _VColEmissive;
	float _VertexEmissiveStr;
	half4 _Color;
	int _HalfLambert;
	int _EnableMaskMap;
	int _ForceNearest;
	int _TextureFiltering;
	int _SpecularType;
	int _Reflections;
	float _NormalStrength;
	float2 _SmoothRemap;
	float2 _SmoothRemapVCol;
	float _VertexLightSmoothness;
	float3 _VertexSpecColor;
	float3 _EmissionColor;
	float4 _TilingOffset;
	float _Cutoff;
	float _AffineMapFactor;
	int _VertexJitterResX;
	int _VertexJitterResY;
	int _DoubleSizeDither;
	int _MaskTint;
	float _LightmapEmissionStr;
	
	int _DitherAmbient;
	int _DitherDiffuse;
	int _DitherSpec;

	float4 _BaseMap_TexelSize;
	float4 _MaskMap_TexelSize;
	float4 _BumpMap_TexelSize;
	float4 _EmissionMap_TexelSize;




         CBUFFER_END

         

         

         #ifdef unity_WorldToObject
#undef unity_WorldToObject
#endif
#ifdef unity_ObjectToWorld
#undef unity_ObjectToWorld
#endif
#define unity_ObjectToWorld GetObjectToWorldMatrix()
#define unity_WorldToObject GetWorldToObjectMatrix()


	/**********
	* TODO:
	* Vertex Colors
	* Meta Pass?
	**********/

	TEXTURE2D(_ToonRamp);
	TEXTURE2D(_SpecularRamp);
	SamplerState sampler_linear_clamp;

	TEXTURE2D(_BaseMap);
	SamplerState sampler_linear_repeat;
	SamplerState sampler_trilinear_repeat;
	SamplerState sampler_point_repeat;

	#if defined(_TEXTUREFILTERING_LINEAR) || defined(PASS_META)
		#define MAINSMP sampler_linear_repeat
	#else
		#define MAINSMP sampler_point_repeat
	#endif

	static const uint bayerPattern[] =
	{
		0, 32,  8, 40,  2, 34, 10, 42,   /* 8x8 Bayer ordered dithering  */
		48, 16, 56, 24, 50, 18, 58, 26,  /* pattern.  Each input pixel   */
		12, 44,  4, 36, 14, 46,  6, 38,  /* is scaled to the 0..63 range */
		60, 28, 52, 20, 62, 30, 54, 22,  /* before looking in this table */
		3, 35, 11, 43,  1, 33,  9, 41,   /* to determine the action.     */
		51, 19, 59, 27, 49, 17, 57, 25,
		15, 47,  7, 39, 13, 45,  5, 37,
		63, 31, 55, 23, 61, 29, 53, 21
	};

	float ditherValue(float2 screenPos)
	{
		uint stippleOffset = ((uint) screenPos.y % 8) * 8 + ((uint) screenPos.x % 8);
		uint byte = bayerPattern[stippleOffset];
		return byte / 64.0f;
	}

	float GetLuminance(float3 col)
	{
		return dot(col, float3(0.299f, 0.587f, 0.114f));
	}

	void ditherColor(inout float3 input, float2 screenPos)
	{
		input.x = input.x < ditherValue(screenPos) ? input.x*0.25 : input.x;
		input.y = input.y < ditherValue(screenPos) ? input.y*0.25 : input.y;
		input.z = input.z < ditherValue(screenPos) ? input.z*0.25 : input.z;
	}

	// This function has been basically ripped straight from https://forum.unity.com/threads/the-quest-for-efficient-per-texel-lighting.529948/
	// So big thanks to GreatestBear, and truepak182, without whom this shader wouldn't be nearly as cool
	float3 CalculateTexelWorldPos(float2 originalUV, float3 originalWorldPos, float4 texelSize)
	{
		// 1.) Calculate how much the texture UV coords need to
		//     shift to be at the center of the nearest texel.
		float2 centerUV = floor(originalUV * (texelSize.zw))/texelSize.zw + (texelSize.xy/2.0);
		float2 dUV = (centerUV - originalUV);
	
		// 2b.) Calculate how much the texture coords vary over fragment space.
		//      This essentially defines a 2x2 matrix that gets
		//      texture space (UV) deltas from fragment space (ST) deltas
		// Note: I call fragment space (S,T) to disambiguate.
		float2 dUVdS = ddx(originalUV);
		float2 dUVdT = ddy(originalUV);
	
		// 2c.) Invert the fragment from texture matrix
		float2x2 dSTdUV = float2x2(dUVdT[1], -dUVdT[0], -dUVdS[1], dUVdS[0])*(1.0f/(dUVdS[0]*dUVdT[1]-dUVdT[0]*dUVdS[1]));
	
	
		// 2d.) Convert the UV delta to a fragment space delta
		float2 dST = mul(dSTdUV , dUV);
	
		// 2e.) Calculate how much the world coords vary over fragment space.
		float3 dXYZdS = ddx(originalWorldPos);
		float3 dXYZdT = ddy(originalWorldPos);
	
		// 2f.) Finally, convert our fragment space delta to a world space delta
		// And be sure to clamp it to SOMETHING in case the derivative calc went insane
		// Here I clamp it to -1 to 1 unit in unity, which should be orders of magnitude greater
		// than the size of any texel.
		float3 dXYZ = clamp(dXYZdS * dST[0] + dXYZdT * dST[1], -1, 1);
	
		// 3.) Transform the snapped UV back to world space
		return originalWorldPos + dXYZ;
	}

	float3 CalculateTexelWorldViewDir(float3 viewDirectionWS, float3 positionWS)
	{
		//Recalculate view direction according to recalculated texelized inputData.positionWS
		float3 viewDirectionWSCorrected = GetCameraPositionWS() - positionWS;
		//This needs to be done to make corrected viewDirectionWSCorrected same length as the original. If omitted, specular will not be displayed properly
		float viewPositionMult = length(viewDirectionWS) / length(viewDirectionWSCorrected);

		return viewDirectionWSCorrected * viewPositionMult;
	}

	float2 SnapUVToTexelCenter(float2 originalUV, float4 texelSize)
	{
		float2 centerUV = floor(originalUV * (texelSize.zw))/texelSize.zw + (texelSize.xy/2.0);
		float2 dUV = (centerUV - originalUV);

		return originalUV + dUV;
	}


	float3 SampleRamp(Texture2D ramp, float coord)
	{
		return SAMPLE_TEXTURE2D_LOD(ramp, sampler_linear_clamp, saturate(float2(pow(coord,0.5), 0.5)), 0).xyz;
	}

	float invLerp(float from, float to, float value){
		return (value - from) / (to - from);
	}

	float remap(float origFrom, float origTo, float targetFrom, float targetTo, float value){
		float rel = invLerp(origFrom, origTo, value);
		return lerp(targetFrom, targetTo, rel);
	}

	float remap01(float targetFrom, float targetTo, float value){
		float rel = invLerp(0, 1, value);
		return lerp(targetFrom, targetTo, rel);

	}

	void MainLight(out float3 direction, out float3 color, out float distanceAtten)
	{
		Light mainLight = GetMainLight();
		direction = mainLight.direction;
		color = mainLight.color;
		distanceAtten = mainLight.distanceAttenuation;
	}

	float MainLightShadows(float3 worldPos, float2 lightmapUV = float2(0.0,0.0), float2 origUV = float2(0.0,0.0))
	{
		float4 shadowCoord = TransformWorldToShadowCoord(worldPos);

		#if !defined (LIGHTMAP_ON) || defined(_LIGHTINGTYPE_VERTEX)
			half4 shadowMask = unity_ProbesOcclusion;
		#else
			lightmapUV = lightmapUV * unity_LightmapST.xy + unity_LightmapST.zw;

			#ifdef _LIGHTINGTYPE_TEXEL
				lightmapUV = CalculateTexelWorldPos(origUV, float3(lightmapUV.x, lightmapUV.y, 1), _BaseMap_TexelSize).xy;
			#endif

			half4 shadowMask = SAMPLE_SHADOWMASK(lightmapUV);
		#endif

		return MainLightShadow(shadowCoord, worldPos, shadowMask, _MainLightOcclusionProbes);
	}

	float3 GetAmbient(float3 worldNormal, float2 lightmapUV, float2 origUV = float2(0.0f, 0.0f))
	{
		#ifdef LIGHTMAP_ON
			lightmapUV = lightmapUV * unity_LightmapST.xy + unity_LightmapST.zw;

			#ifdef _LIGHTINGTYPE_TEXEL
				lightmapUV = CalculateTexelWorldPos(origUV, float3(lightmapUV.x, lightmapUV.y, 1), _BaseMap_TexelSize).xy;
			#endif

			#ifdef _LIGHTINGTYPE_VERTEX
				float4 lightmap = SAMPLE_TEXTURE2D_LOD(unity_Lightmap, sampler_linear_repeat, lightmapUV, 0);
				half3 bakedGI = DecodeLightmap(lightmap,half4(LIGHTMAP_HDR_MULTIPLIER, LIGHTMAP_HDR_EXPONENT, 0.0h, 0.0h));
			#else
				half3 bakedGI = SampleLightmap(lightmapUV, worldNormal);//DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, lightmapUV),half4(LIGHTMAP_HDR_MULTIPLIER, LIGHTMAP_HDR_EXPONENT, 0.0h, 0.0h));
			#endif

			#if defined(_MIXED_LIGHTING_SUBTRACTIVE)
				Light mainLight = GetMainLight();
				bakedGI = SubtractDirectMainLightFromLightmap(mainLight, worldNormal, bakedGI);
			#endif

			return bakedGI;
		#else
			return SampleSH(worldNormal);
		#endif
	}

	float GetSpecular(float3 lightDir, float3 worldNormal, float3 viewDir, float smoothness)
	{
		float SpecSmoothness = exp2(10 * smoothness + 1).x;
		[branch]
		if (_SpecularType == 0) //Cook-Torrance
		{
			float roughness = 1 - smoothness;
			roughness *= roughness;

			float3 h = SafeNormalize(lightDir + viewDir);
			float nh = saturate(dot(worldNormal, h));
			float lh = saturate(dot(lightDir, h));
			float d = nh * nh * (roughness * roughness - 1.0) + 1.00001;
			float normTerm = roughness * 4.0 + 2.0;
			float specTerm = roughness * roughness;
			specTerm /= (d * d) * max(0.1, lh * lh) * normTerm;
			return specTerm*0.04;
		}
		else if (_SpecularType == 1) //Phong
		{
			float3 r = reflect(-lightDir, worldNormal);
			float rdotv = max(0, dot(r, viewDir));

			return pow(rdotv, SpecSmoothness)*smoothness;
		}
		else if (_SpecularType == 2) //Blinn-Phong
		{
			float3 h = SafeNormalize(lightDir+viewDir);
			float ndh = saturate(dot(worldNormal, h));

			return pow(ndh, SpecSmoothness)*smoothness;
		}
		else
		{
			return 0;
		}
	}

	float GetDiffuse(float3 lightDir, float3 worldNormal)
	{
		float diffuse = dot(lightDir, worldNormal);

		if (_HalfLambert)
			diffuse = diffuse * 0.5 + 0.5;

		return max(0,diffuse);
	}


	void AdditionalLights(float3 worldPos, float3 viewDir, float3 worldNormal, float InSmoothness, float3 specColor, out float3 Diffuse, out float3 Specular, float dirOcclusion = 1)
	{
		float3 diff = 0;
		float3 spec = 0;

		#if !defined (LIGHTMAP_ON)
			half4 shadowMask = unity_ProbesOcclusion;
		#else
			half4 shadowMask = half4(1, 1, 1, 1);
		#endif

		int pixelLights = GetAdditionalLightsCount();
		for (int i = 0; i < pixelLights; ++i)
		{
			Light light = GetAdditionalLight(i, worldPos, shadowMask);
			
			#ifdef _TOONSHADING
				float diffMask = GetDiffuse(light.direction, worldNormal) * light.shadowAttenuation * light.distanceAttenuation * dirOcclusion;
				diff += SampleRamp(_ToonRamp, diffMask) * light.color;
			#else
				diff += GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.color * light.shadowAttenuation * dirOcclusion;
			#endif

			#ifdef _TOONSHADING
				float specMask = GetSpecular(light.direction, worldNormal, viewDir, InSmoothness) * light.distanceAttenuation * light.shadowAttenuation * dirOcclusion;
				spec += SampleRamp(_SpecularRamp, specMask) * light.color * specColor;
			#else
				spec += GetSpecular(light.direction, worldNormal, viewDir, InSmoothness) * light.distanceAttenuation * light.color * specColor * light.shadowAttenuation * dirOcclusion;
			#endif
		}
		Diffuse = diff;
		Specular = spec;
	}


	float3 GetReflections(float3 worldNormal, float3 viewDir, float smoothness)
	{
		float perceptualRoughness = 1 - smoothness;
		perceptualRoughness *= perceptualRoughness;
		float mip = PerceptualRoughnessToMipmapLevel(perceptualRoughness);
		float3 sample = SHADERGRAPH_REFLECTION_PROBE(viewDir, worldNormal, mip);
		return sample.rgb;
	}

	void SSAO(ShaderData d, out float directOcclusion, out float indirectOcclusion)
	{
		#if defined(_SCREEN_SPACE_OCCLUSION) && !defined(RETROSHADER_ALPHA)
			float2 uv = d.screenUV;

			#ifdef _LIGHTINGTYPE_TEXEL
				float3 pos = CalculateTexelWorldPos(d.texcoord0, float3(uv.x, uv.y, 1.0), _BaseMap_TexelSize);
				uv = pos.xy;
			#endif

			AmbientOcclusionFactor aoFactor = GetScreenSpaceAmbientOcclusion(uv);
			directOcclusion = aoFactor.directAmbientOcclusion;
			indirectOcclusion = aoFactor.indirectAmbientOcclusion;
		#else
			directOcclusion = 1;
			indirectOcclusion = 1;
		#endif
	}

	// Courtesy of Ben Golus
	// https://forum.unity.com/threads/shader-ideas-for-replicating-n64-bi-linear-filtering.936119/#post-6116207
	half4 sampleN64(Texture2D tex, float2 uv, float4 texelSize)
	{
		// texel coordinates
		float2 texels = uv * texelSize.zw;

		// calculate mip level
		float2 dx = ddx(texels);
		float2 dy = ddy(texels);
		float delta_max_sqr = max(dot(dx, dx), dot(dy, dy));
		float mip = max(0.0, 0.5 * log2(delta_max_sqr));

		// scale texel sizes and texel coordinates to handle mip levels properly
		float scale = pow(2,floor(mip));
		texelSize.xy *= scale;
		texelSize.zw /= scale;
		texels = texels / scale - 0.5;

		// calculate blend for the three points of the tri-filter
		float2 fracTexels = frac(texels);
		float3 blend = float3(
			abs(fracTexels.x+fracTexels.y-1),
			min(abs(fracTexels.xx-float2(0,1)), abs(fracTexels.yy-float2(1,0)))
		);

		// calculate equivalents of point filtered uvs for the three points
		float2 uvA = (floor(texels + fracTexels.yx) + 0.5) * texelSize.xy;
		float2 uvB = (floor(texels) + float2(1.5, 0.5)) * texelSize.xy;
		float2 uvC = (floor(texels) + float2(0.5, 1.5)) * texelSize.xy;

		// sample points
		half4 A = SAMPLE_TEXTURE2D_LOD(tex, MAINSMP, uvA, mip);
		half4 B = SAMPLE_TEXTURE2D_LOD(tex, MAINSMP, uvB, mip);
		half4 C = SAMPLE_TEXTURE2D_LOD(tex, MAINSMP, uvC, mip);

		// blend and return
		return A * blend.x + B * blend.y + C * blend.z;
	}

	half4 sampleTex(Texture2D tex, float2 uv, float4 texelSize)
	{
		#ifdef _TEXTUREFILTERING_N64
			return sampleN64(tex, uv, texelSize);
		#else
			return SAMPLE_TEXTURE2D(tex, MAINSMP, uv);
		#endif
	}

	TEXTURE2D(_MaskMap);
	TEXTURE2D(_BumpMap);
	TEXTURE2D(_EmissionMap);

	void Ext_ModifyVertex0 (inout VertexData v, inout ExtraV2F d)
	{
		float4 clipPos = mul(GetWorldToHClipMatrix(), mul(unity_ObjectToWorld, v.vertex));

		

		#if defined(_LIGHTINGTYPE_VERTEX) && !defined(PASS_META)

			float smoothness = _VertexLightSmoothness;

			#ifdef _VCOLSMOOTHNESS
				smoothness = remap01(_SmoothRemapVCol.x, _SmoothRemapVCol.y, v.vertexColor.a);
			#endif

			float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
			float3 worldNormal = SafeNormalize(mul(unity_ObjectToWorld, v.normal));
			float3 viewDir = normalize(_WorldSpaceCameraPos - worldPos);
			float3 mainLightDir;
			float3 mainLightCol;
			float mainLightAtten;
			MainLight(mainLightDir, mainLightCol, mainLightAtten);

			d.extraV2F0.y = GetDiffuse(mainLightDir, worldNormal) * mainLightAtten;
			d.extraV2F1.xyz = GetAmbient(worldNormal, v.texcoord1);
			d.extraV2F0.z = MainLightShadows(worldPos, v.texcoord1);
			d.extraV2F0.w = GetSpecular(mainLightDir, worldNormal, viewDir, smoothness) * mainLightAtten * d.extraV2F0.z;

			#if !defined (LIGHTMAP_ON)
				half4 shadowMask = unity_ProbesOcclusion;
			#else
				half4 shadowMask = half4(1, 1, 1, 1);
			#endif

			// Have to store each light in a separate vector component so we can apply toon shading in fragment later - toon shading has to be applied per light to make sense
			// This limits us to max 4 lights, would need to use 2 more interpolators for 8
			int pixelLights = GetAdditionalLightsCount();
			Light light;
			if (pixelLights >= 1)
			{
				light = GetAdditionalLight(0, worldPos, shadowMask);
				d.extraV2F3.x = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.x = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
			if (pixelLights >= 2)
			{
				light = GetAdditionalLight(1, worldPos, shadowMask);
				d.extraV2F3.y = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.y = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
			if (pixelLights >= 3)
			{
				light = GetAdditionalLight(2, worldPos, shadowMask);
				d.extraV2F3.z = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.z = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
			if (pixelLights >= 4)
			{
				light = GetAdditionalLight(3, worldPos, shadowMask);
				d.extraV2F3.w = GetDiffuse(light.direction, worldNormal) * light.distanceAttenuation * light.shadowAttenuation;
				d.extraV2F4.w = GetSpecular(light.direction, worldNormal, viewDir, smoothness) * light.distanceAttenuation * light.shadowAttenuation;
			}
		#endif

		#if defined(_VERTEXJITTER) && !defined(_PASSSHADOW)
			float4 vert = clipPos;
			d.extraV2F2.xyz = mul(UNITY_MATRIX_I_VP, vert);
			vert.xyz = clipPos.xyz/clipPos.w;
			vert.x = floor(_VertexJitterResX * vert.x) / _VertexJitterResX;
			vert.y = floor(_VertexJitterResY * vert.y) / _VertexJitterResY;
			vert.xyz *= clipPos.w;
			v.vertex = mul(unity_WorldToObject, mul(UNITY_MATRIX_I_VP, vert));
		#endif

		d.extraV2F0.x = ComputeFogFactor(clipPos.z);
		v.texcoord0.xy = v.texcoord0.xy * _TilingOffset.xy + _TilingOffset.zw;

		#if defined(_AFFINE_MAPPING)
			_AffineMapFactor = min(1, _AffineMapFactor);
			float warpFactor = max(clipPos.w, 0.02); //Prevents flipping
			warpFactor = lerp(0.5, warpFactor, _AffineMapFactor);

			v.texcoord0.xy *= warpFactor;
			d.extraV2F1.w = warpFactor;
		#endif
	}

	
	void Ext_SurfaceFunction0 (inout Surface o, ShaderData d)
	{
		#if defined(_AFFINE_MAPPING)
			d.texcoord0.xy /= d.extraV2F1.w;
		#endif
		// Set up input data
		float4 mainTex = sampleTex(_BaseMap, d.texcoord0.xy, _BaseMap_TexelSize);
		o.Albedo = mainTex.rgb;
		float4 maskMap = sampleTex(_MaskMap, d.texcoord0.xy, _BaseMap_TexelSize);

		

		if (_MaskTint)
		{
			float3 tintedAlbedo = o.Albedo * _Color.rgb;
			o.Albedo = lerp(o.Albedo, tintedAlbedo, maskMap.a);
		}
		else
		{
			o.Albedo *= _Color.rgb;
		}

		#ifdef _EMISSION
			float3 emission = sampleTex(_EmissionMap, d.texcoord0.xy, _EmissionMap_TexelSize) * _EmissionColor;
			o.Emission = emission * _LightmapEmissionStr;

		#endif

		float2 pixelPos = d.clipPos.xy;//floor(d.screenUV.xy * _ScreenParams.xy);

		#if defined(_LIGHTINGTYPE_TEXEL) && !defined(PASS_META)
			o.Normal = UnpackScaleNormal(SAMPLE_TEXTURE2D(_BumpMap, sampler_linear_repeat, d.texcoord0.xy), _NormalStrength);
		#else
			o.Normal = UnpackScaleNormal(sampleTex(_BumpMap, d.texcoord0.xy, _BumpMap_TexelSize), _NormalStrength);
		#endif

		//o.Normal = SafeNormalize(o.Normal);

		#if defined(_VERTEXJITTER) && !defined(_PASSSHADOW)
			d.worldSpacePosition = d.extraV2F2.xyz;
		#endif

		d.worldSpaceNormal = TransformTangentToWorld(o.Normal, d.TBNMatrix);
		float3 unSnappedViewDir = d.worldSpaceViewDir;
		// Snap world position, view direction, and normal to the texel
		#if defined(_LIGHTINGTYPE_TEXEL) && !defined(PASS_META)
			d.worldSpacePosition = CalculateTexelWorldPos(d.texcoord0.xy, d.worldSpacePosition, _BaseMap_TexelSize);
			d.worldSpaceViewDir = CalculateTexelWorldViewDir(d.worldSpaceViewDir, d.worldSpacePosition);
			d.worldSpaceNormal = SafeNormalize(CalculateTexelWorldPos(d.texcoord0.xy, d.worldSpaceNormal, _BaseMap_TexelSize));
		#endif

		if (!_EnableMaskMap)
		{
			maskMap.r = 0;
			maskMap.g = mainTex.a;
			maskMap.b = 1;
		}

		o.Metallic = maskMap.r;
		o.Smoothness = remap01(_SmoothRemap.x, _SmoothRemap.y, maskMap.g);
		o.Occlusion =  maskMap.b;

		// Store lighting values in these
		float3 mainLightDir;
		float3 mainLightCol;
		float mainLightAtten;
		MainLight(mainLightDir, mainLightCol, mainLightAtten);

		float3 mainLightDiffuse;
		float mainShadow = 1;
		float3 ambient;
		float3 mainSpec = 0;

		float3 addDiffuse = 0;
		float3 addSpec = 0;

		float3 specColor = lerp(1.0, o.Albedo, o.Metallic);
		float reflectStr = lerp(0.04, 1, o.Metallic);
		
		if (_Reflections)
			o.Albedo *= 1 - o.Metallic;

		float3 reflection = 0;
		
		if (_Reflections)
			reflection = GetReflections(d.worldSpaceNormal, d.worldSpaceViewDir, o.Smoothness);

		#if !defined (LIGHTMAP_ON)
			half4 shadowMask = unity_ProbesOcclusion;
		#else
			half4 shadowMask = SAMPLE_SHADOWMASK(d.texcoord1.xy);
		#endif

		//Get lighting data, either from vertex shader or calculate it here
		#if defined(_LIGHTINGTYPE_VERTEX) && !defined(PASS_META)

			float vsmoothness = _VertexLightSmoothness;

			#ifdef _VCOLSMOOTHNESS
				vsmoothness = remap01(_SmoothRemapVCol.x, _SmoothRemapVCol.y, v.vertexColor.a);
			#endif

			o.Smoothness = vsmoothness;

			mainLightDiffuse = min(d.extraV2F0.y, d.extraV2F0.z); // Min(mainLight, shadow)

			#ifdef _TOONSHADING
				mainLightDiffuse = SampleRamp(_ToonRamp, mainLightDiffuse);
			#endif

			mainLightDiffuse *= mainLightCol;
			ambient = d.extraV2F1.xyz;

			#ifndef _TOONSHADING
				mainSpec = d.extraV2F0.w * mainLightCol;
			#else
				mainSpec = SampleRamp(_SpecularRamp, d.extraV2F0.w) * mainLightCol;
			#endif

			float3 worldPos = d.worldSpacePosition;

			int pixelLights = GetAdditionalLightsCount();
			Light light;
			if (pixelLights >= 1)
			{
				light = GetAdditionalLight(0, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.x * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.x) * light.color;
				#endif
				
				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.x * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.x) * light.color * specColor;
				#endif
			}
			if (pixelLights >= 2)
			{
				light = GetAdditionalLight(1, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.y * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.y) * light.color;
				#endif

				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.y * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.y) * light.color * specColor;
				#endif
			}
			if (pixelLights >= 3)
			{
				light = GetAdditionalLight(2, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.z * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.z) * light.color;
				#endif

				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.z * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.z) * light.color * specColor;
				#endif
			}
			if (pixelLights >= 4)
			{
				light = GetAdditionalLight(3, worldPos, shadowMask);
				#ifndef _TOONSHADING
					addDiffuse += d.extraV2F3.w * light.color;
				#else
					addDiffuse += SampleRamp(_ToonRamp, d.extraV2F3.w) * light.color;
				#endif

				#ifndef _TOONSHADING
					addSpec += d.extraV2F4.w * light.color * specColor;
				#else
					addSpec += SampleRamp(_SpecularRamp, d.extraV2F4.w) * light.color * specColor;
				#endif
			}

			mainSpec *= _VertexSpecColor;
			addSpec *= _VertexSpecColor;
			//addDiffuse = d.extraV2F3.xyz;
			//addSpec = d.extraV2F4.xyz;
		#else

			float dirOcclusion, indirOcclusion;
			SSAO(d, dirOcclusion, indirOcclusion);

			mainShadow = MainLightShadows(d.worldSpacePosition, d.texcoord1.xy, d.texcoord0.xy);
			
			#ifndef _TOONSHADING
				mainLightDiffuse = GetDiffuse(d.worldSpaceNormal, mainLightDir) * mainLightAtten * mainLightCol * mainShadow * dirOcclusion;
			#else
				mainLightDiffuse = GetDiffuse(d.worldSpaceNormal, mainLightDir) * mainShadow * dirOcclusion;
				float3 ramp = SampleRamp(_ToonRamp, mainLightDiffuse.x);
				mainLightDiffuse = ramp * mainLightAtten * mainLightCol;
			#endif

			ambient = GetAmbient(d.worldSpaceNormal, d.texcoord1.xy, d.texcoord0.xy) * min(o.Occlusion,indirOcclusion);
			mainSpec = GetSpecular(mainLightDir, d.worldSpaceNormal, d.worldSpaceViewDir, o.Smoothness)*dirOcclusion;

			#ifdef _TOONSHADING
				mainSpec = SampleRamp(_SpecularRamp, mainSpec.x);
			#endif

			mainSpec *= mainLightCol * mainLightAtten * specColor;
			mainSpec *= mainShadow;

			AdditionalLights(d.worldSpacePosition, d.worldSpaceViewDir, d.worldSpaceNormal, o.Smoothness, specColor, addDiffuse, addSpec);

		#endif

		float perceptualRoughness = 1 - o.Smoothness;
		perceptualRoughness *= perceptualRoughness;

		float fresnel = pow(1.0 - saturate(dot(d.worldSpaceNormal, d.worldSpaceViewDir)), 4);
		reflectStr *= lerp(0.1, 1, GetLuminance(mainLightDiffuse));
		reflectStr = lerp(reflectStr, saturate(reflectStr*10), fresnel);

		reflection *= reflectStr * specColor;
		reflection /= perceptualRoughness + 0.5;

		if (_DoubleSizeDither) pixelPos /= 2.0;

		[branch]
		if (_DitherDiffuse)
		{
			ditherColor(mainLightDiffuse, pixelPos);
			ditherColor(addDiffuse, pixelPos);
		}

		[branch]
		if (_DitherSpec)
		{
			ditherColor(mainSpec, pixelPos);
			ditherColor(addSpec, pixelPos);
		}

		[branch]
		if (_DitherAmbient)
			ditherColor(ambient, pixelPos);

		o.Alpha = mainTex.a * _Color.a;

		//o.Albedo *= 1 - saturate(GetLuminance(mainSpec));
		//o.Albedo *= 1 - saturate(GetLuminance(addSpec));

		#if defined(RETROSHADER_ALPHA) && !defined(PASS_META)
			if (_PreserveSpecular)
			{
				float specStrength = saturate(GetLuminance(mainSpec));
				float reflectStrength = saturate(GetLuminance(reflection));
				o.Alpha = max(o.Alpha, max(specStrength, reflectStrength));
			}
		#endif

		#ifdef _ALPHATEST_ON
			clip(step(_Cutoff, o.Alpha)-0.1f);
		#endif

		if (_VColEmissive)
		{
			o.Albedo += d.vertexColor.rgb * max(0,_VertexEmissiveStr);
			o.Emission = d.vertexColor.rgb * max(0,_VertexEmissiveStr);
		}
		else
		{
			o.Albedo *= d.vertexColor.rgb;
		}

		float3 combinedAmbient = ambient * o.Albedo;

		// Apply lighting, but not for lightbaking pass
		#if !defined(PASS_META)
			o.Albedo *= mainLightDiffuse + addDiffuse;
			o.Albedo += combinedAmbient;
			o.Albedo += mainSpec * saturate(GetLuminance(mainLightDiffuse));
			o.Albedo += addSpec * saturate(GetLuminance(addDiffuse));
			o.Albedo += reflection;

			#if defined(_EMISSION)
				o.Albedo += emission;
			#endif
		#endif
		
		#if defined(RETROSHADER_ALPHA) && !defined(PASS_META)
			if (_Refraction)
			{
				float origAlpha = o.Alpha;
				float4 offsetPos = float4(d.worldSpacePosition + d.worldSpaceNormal * _RefractionStr, 1.0);
				offsetPos = mul(GetWorldToHClipMatrix(), offsetPos);
				float scale = 1.0;

				#if UNITY_UV_STARTS_AT_TOP
					scale = -1.0;
				#endif

				float4 grabPos = offsetPos *0.5f;
				grabPos.xy = float2(grabPos.x, grabPos.y*scale) + grabPos.w;
				grabPos.zw = offsetPos.zw;
				grabPos.xy /= grabPos.w;
				grabPos.xy = clamp(grabPos.xy, float2(0.0, 0.0), float2(1.0, 1.0));

				float3 refraction = GetSceneColor(grabPos.xy);
				float2 cubemapBlendFactors = pow(abs((grabPos.xy - 0.5) * 2.0),0.25);
				float blend = smoothstep(0.75f, 1, max(cubemapBlendFactors.x, cubemapBlendFactors.y));
				float3 refractDir = refract(-d.worldSpaceViewDir, d.worldSpaceNormal, 1 - _RefractionStr);
				float3 reflectProbe = GetReflections(-refractDir, refractDir, 1.0);
				refraction = lerp(refraction, reflectProbe, blend);

				o.Albedo = lerp(refraction, o.Albedo, origAlpha);
				o.Alpha = 1;
			}
		#endif

		o.Albedo = MixFog(o.Albedo, d.extraV2F0.x);
	}




        
            void ChainSurfaceFunction(inout Surface l, inout ShaderData d)
            {
                  Ext_SurfaceFunction0(l, d);
                 // Ext_SurfaceFunction1(l, d);
                 // Ext_SurfaceFunction2(l, d);
                 // Ext_SurfaceFunction3(l, d);
                 // Ext_SurfaceFunction4(l, d);
                 // Ext_SurfaceFunction5(l, d);
                 // Ext_SurfaceFunction6(l, d);
                 // Ext_SurfaceFunction7(l, d);
                 // Ext_SurfaceFunction8(l, d);
                 // Ext_SurfaceFunction9(l, d);
		           // Ext_SurfaceFunction10(l, d);
                 // Ext_SurfaceFunction11(l, d);
                 // Ext_SurfaceFunction12(l, d);
                 // Ext_SurfaceFunction13(l, d);
                 // Ext_SurfaceFunction14(l, d);
                 // Ext_SurfaceFunction15(l, d);
                 // Ext_SurfaceFunction16(l, d);
                 // Ext_SurfaceFunction17(l, d);
                 // Ext_SurfaceFunction18(l, d);
		           // Ext_SurfaceFunction19(l, d);
                 // Ext_SurfaceFunction20(l, d);
                 // Ext_SurfaceFunction21(l, d);
                 // Ext_SurfaceFunction22(l, d);
                 // Ext_SurfaceFunction23(l, d);
                 // Ext_SurfaceFunction24(l, d);
                 // Ext_SurfaceFunction25(l, d);
                 // Ext_SurfaceFunction26(l, d);
                 // Ext_SurfaceFunction27(l, d);
                 // Ext_SurfaceFunction28(l, d);
		           // Ext_SurfaceFunction29(l, d);
            }

            void ChainModifyVertex(inout VertexData v, inout VertexToPixel v2p, float4 time)
            {
                 ExtraV2F d;
                 
                 ZERO_INITIALIZE(ExtraV2F, d);
                 ZERO_INITIALIZE(Blackboard, d.blackboard);
                 // due to motion vectors in HDRP, we need to use the last
                 // time in certain spots. So if you are going to use _Time to adjust vertices,
                 // you need to use this time or motion vectors will break. 
                 d.time = time;

                   Ext_ModifyVertex0(v, d);
                 // Ext_ModifyVertex1(v, d);
                 // Ext_ModifyVertex2(v, d);
                 // Ext_ModifyVertex3(v, d);
                 // Ext_ModifyVertex4(v, d);
                 // Ext_ModifyVertex5(v, d);
                 // Ext_ModifyVertex6(v, d);
                 // Ext_ModifyVertex7(v, d);
                 // Ext_ModifyVertex8(v, d);
                 // Ext_ModifyVertex9(v, d);
                 // Ext_ModifyVertex10(v, d);
                 // Ext_ModifyVertex11(v, d);
                 // Ext_ModifyVertex12(v, d);
                 // Ext_ModifyVertex13(v, d);
                 // Ext_ModifyVertex14(v, d);
                 // Ext_ModifyVertex15(v, d);
                 // Ext_ModifyVertex16(v, d);
                 // Ext_ModifyVertex17(v, d);
                 // Ext_ModifyVertex18(v, d);
                 // Ext_ModifyVertex19(v, d);
                 // Ext_ModifyVertex20(v, d);
                 // Ext_ModifyVertex21(v, d);
                 // Ext_ModifyVertex22(v, d);
                 // Ext_ModifyVertex23(v, d);
                 // Ext_ModifyVertex24(v, d);
                 // Ext_ModifyVertex25(v, d);
                 // Ext_ModifyVertex26(v, d);
                 // Ext_ModifyVertex27(v, d);
                 // Ext_ModifyVertex28(v, d);
                 // Ext_ModifyVertex29(v, d);


                 // #if %EXTRAV2F0REQUIREKEY%
                  v2p.extraV2F0 = d.extraV2F0;
                 // #endif

                 // #if %EXTRAV2F1REQUIREKEY%
                  v2p.extraV2F1 = d.extraV2F1;
                 // #endif

                 // #if %EXTRAV2F2REQUIREKEY%
                  v2p.extraV2F2 = d.extraV2F2;
                 // #endif

                 // #if %EXTRAV2F3REQUIREKEY%
                  v2p.extraV2F3 = d.extraV2F3;
                 // #endif

                 // #if %EXTRAV2F4REQUIREKEY%
                  v2p.extraV2F4 = d.extraV2F4;
                 // #endif

                 // #if %EXTRAV2F5REQUIREKEY%
                 // v2p.extraV2F5 = d.extraV2F5;
                 // #endif

                 // #if %EXTRAV2F6REQUIREKEY%
                 // v2p.extraV2F6 = d.extraV2F6;
                 // #endif

                 // #if %EXTRAV2F7REQUIREKEY%
                 // v2p.extraV2F7 = d.extraV2F7;
                 // #endif
            }

            void ChainModifyTessellatedVertex(inout VertexData v, inout VertexToPixel v2p)
            {
               ExtraV2F d;
               ZERO_INITIALIZE(ExtraV2F, d);
               ZERO_INITIALIZE(Blackboard, d.blackboard);

               // #if %EXTRAV2F0REQUIREKEY%
                d.extraV2F0 = v2p.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
                d.extraV2F1 = v2p.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
                d.extraV2F2 = v2p.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
                d.extraV2F3 = v2p.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
                d.extraV2F4 = v2p.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // d.extraV2F5 = v2p.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // d.extraV2F6 = v2p.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // d.extraV2F7 = v2p.extraV2F7;
               // #endif


               // Ext_ModifyTessellatedVertex0(v, d);
               // Ext_ModifyTessellatedVertex1(v, d);
               // Ext_ModifyTessellatedVertex2(v, d);
               // Ext_ModifyTessellatedVertex3(v, d);
               // Ext_ModifyTessellatedVertex4(v, d);
               // Ext_ModifyTessellatedVertex5(v, d);
               // Ext_ModifyTessellatedVertex6(v, d);
               // Ext_ModifyTessellatedVertex7(v, d);
               // Ext_ModifyTessellatedVertex8(v, d);
               // Ext_ModifyTessellatedVertex9(v, d);
               // Ext_ModifyTessellatedVertex10(v, d);
               // Ext_ModifyTessellatedVertex11(v, d);
               // Ext_ModifyTessellatedVertex12(v, d);
               // Ext_ModifyTessellatedVertex13(v, d);
               // Ext_ModifyTessellatedVertex14(v, d);
               // Ext_ModifyTessellatedVertex15(v, d);
               // Ext_ModifyTessellatedVertex16(v, d);
               // Ext_ModifyTessellatedVertex17(v, d);
               // Ext_ModifyTessellatedVertex18(v, d);
               // Ext_ModifyTessellatedVertex19(v, d);
               // Ext_ModifyTessellatedVertex20(v, d);
               // Ext_ModifyTessellatedVertex21(v, d);
               // Ext_ModifyTessellatedVertex22(v, d);
               // Ext_ModifyTessellatedVertex23(v, d);
               // Ext_ModifyTessellatedVertex24(v, d);
               // Ext_ModifyTessellatedVertex25(v, d);
               // Ext_ModifyTessellatedVertex26(v, d);
               // Ext_ModifyTessellatedVertex27(v, d);
               // Ext_ModifyTessellatedVertex28(v, d);
               // Ext_ModifyTessellatedVertex29(v, d);

               // #if %EXTRAV2F0REQUIREKEY%
                v2p.extraV2F0 = d.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
                v2p.extraV2F1 = d.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
                v2p.extraV2F2 = d.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
                v2p.extraV2F3 = d.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
                v2p.extraV2F4 = d.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // v2p.extraV2F5 = d.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // v2p.extraV2F6 = d.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // v2p.extraV2F7 = d.extraV2F7;
               // #endif
            }

            void ChainFinalColorForward(inout Surface l, inout ShaderData d, inout half4 color)
            {
               //   Ext_FinalColorForward0(l, d, color);
               //   Ext_FinalColorForward1(l, d, color);
               //   Ext_FinalColorForward2(l, d, color);
               //   Ext_FinalColorForward3(l, d, color);
               //   Ext_FinalColorForward4(l, d, color);
               //   Ext_FinalColorForward5(l, d, color);
               //   Ext_FinalColorForward6(l, d, color);
               //   Ext_FinalColorForward7(l, d, color);
               //   Ext_FinalColorForward8(l, d, color);
               //   Ext_FinalColorForward9(l, d, color);
               //  Ext_FinalColorForward10(l, d, color);
               //  Ext_FinalColorForward11(l, d, color);
               //  Ext_FinalColorForward12(l, d, color);
               //  Ext_FinalColorForward13(l, d, color);
               //  Ext_FinalColorForward14(l, d, color);
               //  Ext_FinalColorForward15(l, d, color);
               //  Ext_FinalColorForward16(l, d, color);
               //  Ext_FinalColorForward17(l, d, color);
               //  Ext_FinalColorForward18(l, d, color);
               //  Ext_FinalColorForward19(l, d, color);
               //  Ext_FinalColorForward20(l, d, color);
               //  Ext_FinalColorForward21(l, d, color);
               //  Ext_FinalColorForward22(l, d, color);
               //  Ext_FinalColorForward23(l, d, color);
               //  Ext_FinalColorForward24(l, d, color);
               //  Ext_FinalColorForward25(l, d, color);
               //  Ext_FinalColorForward26(l, d, color);
               //  Ext_FinalColorForward27(l, d, color);
               //  Ext_FinalColorForward28(l, d, color);
               //  Ext_FinalColorForward29(l, d, color);
            }

            void ChainFinalGBufferStandard(inout Surface s, inout ShaderData d, inout half4 GBuffer0, inout half4 GBuffer1, inout half4 GBuffer2, inout half4 outEmission, inout half4 outShadowMask)
            {
               //   Ext_FinalGBufferStandard0(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard1(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard2(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard3(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard4(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard5(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard6(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard7(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard8(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard9(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard10(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard11(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard12(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard13(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard14(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard15(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard16(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard17(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard18(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard19(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard20(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard21(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard22(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard23(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard24(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard25(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard26(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard27(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard28(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard29(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
            }



         

         ShaderData CreateShaderData(VertexToPixel i
                  #if NEED_FACING
                     , bool facing
                  #endif
         )
         {
            ShaderData d = (ShaderData)0;
            d.clipPos = i.pos;
            d.worldSpacePosition = i.worldPos;

            d.worldSpaceNormal = normalize(i.worldNormal);
            d.worldSpaceTangent = normalize(i.worldTangent.xyz);
            d.tangentSign = i.worldTangent.w;
            float3 bitangent = cross(i.worldTangent.xyz, i.worldNormal) * d.tangentSign * -1;
            

            d.TBNMatrix = float3x3(d.worldSpaceTangent, bitangent, d.worldSpaceNormal);
            d.worldSpaceViewDir = normalize(_WorldSpaceCameraPos - i.worldPos);

            d.tangentSpaceViewDir = mul(d.TBNMatrix, d.worldSpaceViewDir);
             d.texcoord0 = i.texcoord0;
             d.texcoord1 = i.texcoord1;
            // d.texcoord2 = i.texcoord2;

            // #if %TEXCOORD3REQUIREKEY%
            // d.texcoord3 = i.texcoord3;
            // #endif

            // d.isFrontFace = facing;
            // #if %VERTEXCOLORREQUIREKEY%
             d.vertexColor = i.vertexColor;
            // #endif

            // these rarely get used, so we back transform them. Usually will be stripped.
            #if _HDRP
                // d.localSpacePosition = mul(unity_WorldToObject, float4(GetCameraRelativePositionWS(i.worldPos), 1)).xyz;
            #else
                // d.localSpacePosition = mul(unity_WorldToObject, float4(i.worldPos, 1)).xyz;
            #endif
            // d.localSpaceNormal = normalize(mul((float3x3)unity_WorldToObject, i.worldNormal));
            // d.localSpaceTangent = normalize(mul((float3x3)unity_WorldToObject, i.worldTangent.xyz));

            // #if %SCREENPOSREQUIREKEY%
             d.screenPos = i.screenPos;
             d.screenUV = (i.screenPos.xy / i.screenPos.w);
            // #endif


            // #if %EXTRAV2F0REQUIREKEY%
             d.extraV2F0 = i.extraV2F0;
            // #endif

            // #if %EXTRAV2F1REQUIREKEY%
             d.extraV2F1 = i.extraV2F1;
            // #endif

            // #if %EXTRAV2F2REQUIREKEY%
             d.extraV2F2 = i.extraV2F2;
            // #endif

            // #if %EXTRAV2F3REQUIREKEY%
             d.extraV2F3 = i.extraV2F3;
            // #endif

            // #if %EXTRAV2F4REQUIREKEY%
             d.extraV2F4 = i.extraV2F4;
            // #endif

            // #if %EXTRAV2F5REQUIREKEY%
            // d.extraV2F5 = i.extraV2F5;
            // #endif

            // #if %EXTRAV2F6REQUIREKEY%
            // d.extraV2F6 = i.extraV2F6;
            // #endif

            // #if %EXTRAV2F7REQUIREKEY%
            // d.extraV2F7 = i.extraV2F7;
            // #endif

            return d;
         }
         

         
         #if defined(_PASSSHADOW)
            float3 _LightDirection;
            float3 _LightPosition;
         #endif

         // vertex shader
         VertexToPixel Vert (VertexData v)
         {
           
           VertexToPixel o = (VertexToPixel)0;

           UNITY_SETUP_INSTANCE_ID(v);
           UNITY_TRANSFER_INSTANCE_ID(v, o);
           UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);


#if !_TESSELLATION_ON
           ChainModifyVertex(v, o, _Time);
#endif

            o.texcoord0 = v.texcoord0;
            o.texcoord1 = v.texcoord1;
           // o.texcoord2 = v.texcoord2;

           // #if %TEXCOORD3REQUIREKEY%
           // o.texcoord3 = v.texcoord3;
           // #endif

           // #if %VERTEXCOLORREQUIREKEY%
            o.vertexColor = v.vertexColor;
           // #endif
           
           VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
           o.worldPos = TransformObjectToWorld(v.vertex.xyz);
           o.worldNormal = TransformObjectToWorldNormal(v.normal);
           o.worldTangent = float4(TransformObjectToWorldDir(v.tangent.xyz), v.tangent.w);

          // For some very odd reason, in 2021.2, we can't use Unity's defines, but have to use our own..
          #if _PASSSHADOW
              #if _CASTING_PUNCTUAL_LIGHT_SHADOW
                 float3 lightDirectionWS = normalize(_LightPosition - o.worldPos);
              #else
                 float3 lightDirectionWS = _LightDirection;
              #endif
              // Define shadow pass specific clip position for Universal
              o.pos = TransformWorldToHClip(ApplyShadowBias(o.worldPos, o.worldNormal, lightDirectionWS));
              #if UNITY_REVERSED_Z
                  o.pos.z = min(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #else
                  o.pos.z = max(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #endif
          #elif _PASSMETA
              o.pos = MetaVertexPosition(float4(v.vertex.xyz, 0), v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST);
          #else
              o.pos = TransformWorldToHClip(o.worldPos);
          #endif

          // #if %SCREENPOSREQUIREKEY%
           o.screenPos = ComputeScreenPos(o.pos, _ProjectionParams.x);
          // #endif

          #if _PASSFORWARD || _PASSGBUFFER
              float2 uv1 = v.texcoord1.xy;
              OUTPUT_LIGHTMAP_UV(uv1, unity_LightmapST, o.lightmapUV);
               o.texcoord1.xy = uv1;
              OUTPUT_SH(o.worldNormal, o.sh);
              #if defined(DYNAMICLIGHTMAP_ON)
                   o.dynamicLightmapUV.xy = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
              #endif
          #endif

          #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
              half fogFactor = 0;
              #if defined(_FOG_FRAGMENT)
                fogFactor = ComputeFogFactor(o.pos.z);
              #endif
              #if _BAKEDLIT
                 o.fogFactorAndVertexLight = half4(fogFactor, 0, 0, 0);
              #else
                 half3 vertexLight = VertexLighting(o.worldPos, o.worldNormal);
                 o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);
              #endif
          #endif

          #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
             o.shadowCoord = GetShadowCoord(vertexInput);
          #endif

           return o;
         }


         

         // fragment shader
         half4 Frag (VertexToPixel IN
            #ifdef _DEPTHOFFSET_ON
              , out float outputDepth : SV_Depth
            #endif
            #if NEED_FACING
               , bool facing : SV_IsFrontFace
            #endif
         ) : SV_Target
         {
           UNITY_SETUP_INSTANCE_ID(IN);
           UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

           ShaderData d = CreateShaderData(IN
                  #if NEED_FACING
                     , facing
                  #endif
               );
           Surface l = (Surface)0;

           #ifdef _DEPTHOFFSET_ON
              l.outputDepth = outputDepth;
           #endif

           l.Albedo = half3(0.5, 0.5, 0.5);
           l.Normal = float3(0,0,1);
           l.Occlusion = 1;
           l.Alpha = 1;

           ChainSurfaceFunction(l, d);

           #ifdef _DEPTHOFFSET_ON
              outputDepth = l.outputDepth;
           #endif

          #if defined(_GBUFFER_NORMALS_OCT)
              float3 normalWS = d.worldSpaceNormal;
              float2 octNormalWS = PackNormalOctQuadEncode(normalWS);           // values between [-1, +1], must use fp32 on some platforms
              float2 remappedOctNormalWS = saturate(octNormalWS * 0.5 + 0.5);   // values between [ 0,  1]
              half3 packedNormalWS = PackFloat2To888(remappedOctNormalWS);      // values between [ 0,  1]
              return half4(packedNormalWS, 0.0);
          #else
              return half4(NormalizeNormalPerPixel(d.worldSpaceNormal), 0.0);
          #endif

         
         }

         ENDHLSL

      }


      


      

   }
   
   
   CustomEditor "Needle.MarkdownShaderGUI"
}
