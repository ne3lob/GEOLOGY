Shader "Universal Render Pipeline/Custom/Lit Tessellation"
{
	Properties
	{
		// Specular vs Metallic workflow
		[HideInInspector] _WorkflowMode("WorkflowMode", Float) = 1.0

		[MainTexture] _BaseMap("Albedo", 2D) = "white" {}
		[MainColor] _BaseColor("Color", Color) = (1,1,1,1)

		_Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

		_Smoothness("Smoothness", Range(0.0, 1.0)) = 0.5
		_GlossMapScale("Smoothness Scale", Range(0.0, 1.0)) = 1.0
		_SmoothnessTextureChannel("Smoothness texture channel", Float) = 0

		_Metallic("Metallic", Range(0.0, 1.0)) = 0.0
		_MetallicGlossMap("Metallic", 2D) = "white" {}

		_SpecColor("Specular", Color) = (0.2, 0.2, 0.2)
		_SpecGlossMap("Specular", 2D) = "white" {}

		[ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
		[ToggleOff] _EnvironmentReflections("Environment Reflections", Float) = 1.0

		_BumpScale("Scale", Float) = 1.0
		_BumpMap("Normal Map", 2D) = "bump" {}

        _Parallax("Scale", Range(0.005, 0.08)) = 0.005
        _ParallaxMap("Height Map", 2D) = "black" {}

		_OcclusionStrength("Strength", Range(0.0, 1.0)) = 1.0
		_OcclusionMap("Occlusion", 2D) = "white" {}

		[HDR] _EmissionColor("Color", Color) = (0,0,0)
		_EmissionMap("Emission", 2D) = "white" {}

        _DetailMask("Detail Mask", 2D) = "white" {}
        _DetailAlbedoMapScale("Scale", Range(0.0, 2.0)) = 1.0
        _DetailAlbedoMap("Detail Albedo x2", 2D) = "linearGrey" {}
        _DetailNormalMapScale("Scale", Range(0.0, 2.0)) = 1.0
        [Normal] _DetailNormalMap("Normal Map", 2D) = "bump" {}

        // SRP batching compatibility for Clear Coat (Not used in Lit)
        [HideInInspector] _ClearCoatMask("_ClearCoatMask", Float) = 0.0
        [HideInInspector] _ClearCoatSmoothness("_ClearCoatSmoothness", Float) = 0.0

		// Blending state
		[HideInInspector] _Surface("__surface", Float) = 0.0
		[HideInInspector] _Blend("__blend", Float) = 0.0
		[HideInInspector] _AlphaClip("__clip", Float) = 0.0
		[HideInInspector] _SrcBlend("__src", Float) = 1.0
		[HideInInspector] _DstBlend("__dst", Float) = 0.0
		[HideInInspector] _ZWrite("__zw", Float) = 1.0
		[HideInInspector] _Cull("__cull", Float) = 2.0

		_ReceiveShadows("Receive Shadows", Float) = 1.0

		// Editmode props
		[HideInInspector] _QueueOffset("Queue offset", Float) = 0.0

		// ObsoleteProperties
		[HideInInspector] _MainTex("BaseMap", 2D) = "white" {}
		[HideInInspector] _Color("Base Color", Color) = (1, 1, 1, 1)
		[HideInInspector] _GlossMapScale("Smoothness", Float) = 0.0
		[HideInInspector] _Glossiness("Smoothness", Float) = 0.0
		[HideInInspector] _GlossyReflections("EnvironmentReflections", Float) = 0.0

        [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}

		// ********* Geometry *************

		// Vertex Color
        _VertexColor("VertexColor", Float) = 0.0
		_VertexColorMode("VertexColorMode", Float) = 0.0
        _VertexColorBlending("VertexColorBlending", Float) = 1.0

		// Tessellation
		_TessellationMap("Tessellation", 2D) = "white" {}
		_TessellationScale("Tessellation Scale", Range(0.0, 1.0)) = 1.0
		_TessellationMode("TessellationMode", Float) = 0.0

		// Uniform Tessellation
		_TessellationFactor("TessellationFactor", Range(1.0, 64.0)) = 1.0

		// Distance Tessellation
		_TessellationFactorMin("TessellationFactorMin", Range(1.0, 64.0)) = 1.0
		_TessellationFactorMax("TessellationFactorMax", Range(1.0, 64.0)) = 1.0
		_TessellationDistanceMin("TessellationDistanceMin", Float) = 1.0
		_TessellationDistanceMax("TessellationDistanceMax", Float) = 10.0

		// Edge Length Tessellation
		_TessellationEdgeLength("TessellationEdgeLength", Range(0.05, 32.0)) = 6
		_TessellationEdgeDistanceOffset("TessellationEdgeDistanceOffset", Range(0.0, 10.0)) = 0.5

		// Tessellation Postpro
		_TessellationPostPro("TessellationPostPro", Float) = 0.0
		_TessellationPhongShape("TessellationPhongShape", Range(0.0, 1.0)) = 0.0

		// Clipping
		_TessellationTriangleClipping("TessellationTriangleClipping", Float) = 1.0
		_TessellationTriangleClipBias("TessellationTriangleClipBias", Range(-1.0, 1.0)) = 0.0

		// Displacement
		_HeightMap("Height", 2D) = "black" {}
		_HeightStrength("Height Strength", Float) = 0.0
		_HeightBase("Height Base", Range(-1.0, 1.0)) = 0.0

		// Displacement Blur
		_HeightBlurMode("Height Blur Mode", Float) = 0.0
		_HeightBlurSize("Height Blur Size", Range(0.0, 1.0)) = 0.0
		_HeightBlurSamples("Height Blur Samples", Range(1.0, 60.0)) = 8.0
		_HeightBlurGaussStandardDeviation("Standard Deviation", Range(0.001, 0.1)) = 0.02

		// Advanced Geometry
		_GeometryCustomST("Geometry Custom ST", Float) = 0.0
        _GeometryCustomUVChannel("Geometry Custom UV Channel", Float) = 0.0
		_GeometryUVChannel("Geometry UV Channel", Float) = 0.0
	}

	SubShader
	{
		// Universal Pipeline tag is required. If Universal render pipeline is not set in the graphics settings
		// this Subshader will fail. One can add a subshader below or fallback to Standard built-in to make this
		// material work with both Universal Render Pipeline and Builtin Unity Pipeline
        Tags{"RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" "UniversalMaterialType" = "Lit" "IgnoreProjector" = "True" "ShaderModel"="4.5"}
		LOD 300
		
        // ------------------------------------------------------------------
        //  Forward pass. Shades all light in a single pass. GI + emission + Fog
        Pass
        {
            // Lightmode matches the ShaderPassName set in UniversalRenderPipeline.cs. SRPDefaultUnlit and passes with
            // no LightMode tag are also rendered by Universal Render Pipeline
            Name "ForwardLit"
            Tags{"LightMode" = "UniversalForward"}

            Blend[_SrcBlend][_DstBlend]
            ZWrite[_ZWrite]
            Cull[_Cull]

            HLSLPROGRAM
            #pragma exclude_renderers gles gles3 glcore
            #pragma target 4.6
			
            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local _NORMALMAP
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _ALPHAPREMULTIPLY_ON
            #pragma shader_feature_local_fragment _EMISSION
            #pragma shader_feature_local_fragment _METALLICSPECGLOSSMAP
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature_local_fragment _OCCLUSIONMAP
            #pragma shader_feature_local _PARALLAXMAP
            #pragma shader_feature_local _ _DETAIL_MULX2 _DETAIL_SCALED
            #pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature_local_fragment _ENVIRONMENTREFLECTIONS_OFF
            #pragma shader_feature_local_fragment _SPECULAR_SETUP
            #pragma shader_feature_local _RECEIVE_SHADOWS_OFF

            // -------------------------------------
            // Universal Pipeline keywords
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile_fragment _ _SHADOWS_SOFT
            #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
            #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
            #pragma multi_compile _ SHADOWS_SHADOWMASK

            // -------------------------------------
            // Unity defined keywords
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile_fog

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma multi_compile _ DOTS_INSTANCING_ON

			// -------------------------------------
			// Geometry keywords
            #pragma shader_feature _VERTEX_COLOR
			#pragma shader_feature _VERTEX_COLOR_MULTIPLY
            #pragma shader_feature _VERTEX_COLOR_ADDITIVE
            #pragma shader_feature _VERTEX_COLOR_SUBTRACTIVE
            #pragma shader_feature _VERTEX_COLOR_REPLACE
			#pragma shader_feature _TESSELLATIONMAP
			#pragma shader_feature _TESSELLATION_EDGE
			#pragma shader_feature _TESSELLATION_DISTANCE
			#pragma shader_feature _TESSELLATION_PHONG
			#pragma shader_feature _TESSELLATION_CLIPPING
			#pragma shader_feature _HEIGHTMAP
			#pragma shader_feature _HEIGHTMAP_BLUR_BOX
			#pragma shader_feature _HEIGHTMAP_BLUR_GAUSS
			#pragma shader_feature _GEOMETRY_CUSTOM_ST
            #pragma shader_feature _GEOMETRY_CUSTOM_UV_CHANNEL
			#pragma shader_feature _GEOMETRY_UV_CHANNEL_0
			#pragma shader_feature _GEOMETRY_UV_CHANNEL_1
			#pragma shader_feature _GEOMETRY_UV_CHANNEL_2
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_3

			// Vertex Inputs
			#define VERTEX_POSITION_OS 1
			#define VERTEX_NORMAL_OS 1
			#define VERTEX_TANGENT_OS 1
			#define VERTEX_TEXCOORD 1
			#define VERTEX_LIGHTMAP_UV 1

			// Geometry Inputs
			#define GEOMETRY_UV2 1
            #define GEOMETRY_UV3 1

            // Pixel Inputs
            #define PIXEL_VERTEX_COLOR 1
            
            #define PASS_FORWARD
			#define PassVertexProgram LitPassVertex
			#define PassFragmentProgram LitPassFragment
			
			#pragma vertex TessellatedVertex
			#pragma hull TessellatedHull
			#pragma domain TessellatedDomain
			#pragma fragment TessellatedFragment

            #include "LitTessellationInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitForwardPass.hlsl"
            #include "LitTessellationStructs.hlsl"
            #include "LitTessellationPasses.hlsl"

            ENDHLSL
        }

        // ------------------------------------------------------------------
        //  Forward pass. Shadow casting.
        Pass
        {
            Name "ShadowCaster"
            Tags{"LightMode" = "ShadowCaster"}

            ZWrite On
            ZTest LEqual
            ColorMask 0
            Cull[_Cull]

            HLSLPROGRAM
            #pragma exclude_renderers gles gles3 glcore
            #pragma target 4.6

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma multi_compile _ DOTS_INSTANCING_ON

            // -------------------------------------
            // Geometry keywords
            #pragma shader_feature _TESSELLATIONMAP
            #pragma shader_feature _TESSELLATION_EDGE
            #pragma shader_feature _TESSELLATION_DISTANCE
            #pragma shader_feature _TESSELLATION_PHONG
            #pragma shader_feature _TESSELLATION_CLIPPING
            #pragma shader_feature _HEIGHTMAP
            #pragma shader_feature _HEIGHTMAP_BLUR_BOX
            #pragma shader_feature _HEIGHTMAP_BLUR_GAUSS
            #pragma shader_feature _GEOMETRY_CUSTOM_ST
            #pragma shader_feature _GEOMETRY_CUSTOM_UV_CHANNEL
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_0
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_1
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_2
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_3

            // Vertex Inputs
            #define VERTEX_POSITION_OS 1
            #define VERTEX_NORMAL_OS 1
            #define VERTEX_TEXCOORD 1

            // Geometry Inputs
            #define GEOMETRY_UV1 1
            #define GEOMETRY_UV2 1
            #define GEOMETRY_UV3 1
            
            #define PASS_SHADOW
            #define PassVertexProgram ShadowPassVertex
            #define PassFragmentProgram ShadowPassFragment

            #pragma vertex TessellatedVertex
            #pragma hull TessellatedHull
            #pragma domain TessellatedDomain
            #pragma fragment TessellatedFragment

            #include "LitTessellationInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/ShadowCasterPass.hlsl"
            #include "LitTessellationStructs.hlsl"
            #include "LitTessellationPasses.hlsl"

            ENDHLSL
        }
        
		// ------------------------------------------------------------------
		// GBuffer
        Pass
        {
            // Lightmode matches the ShaderPassName set in UniversalRenderPipeline.cs. SRPDefaultUnlit and passes with
            // no LightMode tag are also rendered by Universal Render Pipeline
            Name "GBuffer"
            Tags{"LightMode" = "UniversalGBuffer"}

            ZWrite[_ZWrite]
            ZTest LEqual
            Cull[_Cull]

            HLSLPROGRAM
            #pragma exclude_renderers gles gles3 glcore
            #pragma target 4.6

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local _NORMALMAP
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            //#pragma shader_feature_local_fragment _ALPHAPREMULTIPLY_ON
            #pragma shader_feature_local_fragment _EMISSION
            #pragma shader_feature_local_fragment _METALLICSPECGLOSSMAP
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature_local_fragment _OCCLUSIONMAP
            #pragma shader_feature_local _PARALLAXMAP
            #pragma shader_feature_local _ _DETAIL_MULX2 _DETAIL_SCALED

            #pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature_local_fragment _ENVIRONMENTREFLECTIONS_OFF
            #pragma shader_feature_local_fragment _SPECULAR_SETUP
            #pragma shader_feature_local _RECEIVE_SHADOWS_OFF

            // -------------------------------------
            // Universal Pipeline keywords
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
            //#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            //#pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
            #pragma multi_compile _ SHADOWS_SHADOWMASK

            // -------------------------------------
            // Unity defined keywords
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile_fragment _ _GBUFFER_NORMALS_OCT

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma multi_compile _ DOTS_INSTANCING_ON

			// -------------------------------------
			// Geometry keywords
            #pragma shader_feature _VERTEX_COLOR
			#pragma shader_feature _VERTEX_COLOR_MULTIPLY
            #pragma shader_feature _VERTEX_COLOR_ADDITIVE
            #pragma shader_feature _VERTEX_COLOR_SUBTRACTIVE
            #pragma shader_feature _VERTEX_COLOR_REPLACE
			#pragma shader_feature _TESSELLATIONMAP
			#pragma shader_feature _TESSELLATION_EDGE
			#pragma shader_feature _TESSELLATION_DISTANCE
			#pragma shader_feature _TESSELLATION_PHONG
			#pragma shader_feature _TESSELLATION_CLIPPING
			#pragma shader_feature _HEIGHTMAP
			#pragma shader_feature _HEIGHTMAP_BLUR_BOX
			#pragma shader_feature _HEIGHTMAP_BLUR_GAUSS
			#pragma shader_feature _GEOMETRY_CUSTOM_ST
            #pragma shader_feature _GEOMETRY_CUSTOM_UV_CHANNEL
			#pragma shader_feature _GEOMETRY_UV_CHANNEL_0
			#pragma shader_feature _GEOMETRY_UV_CHANNEL_1
			#pragma shader_feature _GEOMETRY_UV_CHANNEL_2
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_3

			// Vertex Inputs
			#define VERTEX_POSITION_OS 1
			#define VERTEX_NORMAL_OS 1
			#define VERTEX_TANGENT_OS 1
			#define VERTEX_TEXCOORD 1
			#define VERTEX_LIGHTMAP_UV 1

			// Geometry Inputs
			#define GEOMETRY_UV2 1
            #define GEOMETRY_UV3 1

            // Pixel Inputs
            #define PIXEL_VERTEX_COLOR 1
            
            #define PASS_GBUFFER
			#define PassVertexProgram LitGBufferPassVertex
			#define PassFragmentProgram LitGBufferPassFragment
			
			#pragma vertex TessellatedVertex
			#pragma hull TessellatedHull
			#pragma domain TessellatedDomain
			#pragma fragment TessellatedFragment

            #include "LitTessellationInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitGBufferPass.hlsl"
            #include "LitTessellationStructs.hlsl"
            #include "LitTessellationPasses.hlsl"

            ENDHLSL
        }
            
		// ------------------------------------------------------------------
		// Depth only pass.
        Pass
        {
            Name "DepthOnly"
            Tags{"LightMode" = "DepthOnly"}

            ZWrite On
            ColorMask 0
            Cull[_Cull]

            HLSLPROGRAM
            #pragma exclude_renderers gles gles3 glcore
            #pragma target 4.6

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma multi_compile _ DOTS_INSTANCING_ON

            // -------------------------------------
            // Geometry keywords
            #pragma shader_feature _TESSELLATIONMAP
            #pragma shader_feature _TESSELLATION_EDGE
            #pragma shader_feature _TESSELLATION_DISTANCE
            #pragma shader_feature _TESSELLATION_PHONG
            #pragma shader_feature _TESSELLATION_CLIPPING
            #pragma shader_feature _HEIGHTMAP
            #pragma shader_feature _HEIGHTMAP_BLUR_BOX
            #pragma shader_feature _HEIGHTMAP_BLUR_GAUSS
            #pragma shader_feature _GEOMETRY_CUSTOM_ST
            #pragma shader_feature _GEOMETRY_CUSTOM_UV_CHANNEL
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_0
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_1
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_2
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_3

            // Vertex Inputs
            #define VERTEX_POSITION 1
            #define VERTEX_TEXCOORD 1

            // Geometry Inputs
            #define GEOMETRY_NORMAL 1
            #define GEOMETRY_UV1 1
            #define GEOMETRY_UV2 1
            #define GEOMETRY_UV3 1

            #define PASS_DEPTH
            #define PassVertexProgram DepthOnlyVertex
            #define PassFragmentProgram DepthOnlyFragment

            #pragma vertex TessellatedVertex
            #pragma hull TessellatedHull
            #pragma domain TessellatedDomain
            #pragma fragment TessellatedFragment

            #include "LitTessellationInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/DepthOnlyPass.hlsl"
            #include "LitTessellationStructs.hlsl"
            #include "LitTessellationPasses.hlsl"

            ENDHLSL
        }

        // ------------------------------------------------------------------
        // This pass is used when drawing to a _CameraNormalsTexture texture
        Pass
        {
            Name "DepthNormals"
            Tags{"LightMode" = "DepthNormals"}

            ZWrite On
            Cull[_Cull]

            HLSLPROGRAM
            #pragma exclude_renderers gles gles3 glcore
            #pragma target 4.6

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local _NORMALMAP
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma multi_compile _ DOTS_INSTANCING_ON

            // -------------------------------------
            // Geometry keywords
            #pragma shader_feature _TESSELLATIONMAP
            #pragma shader_feature _TESSELLATION_EDGE
            #pragma shader_feature _TESSELLATION_DISTANCE
            #pragma shader_feature _TESSELLATION_PHONG
            #pragma shader_feature _TESSELLATION_CLIPPING
            #pragma shader_feature _HEIGHTMAP
            #pragma shader_feature _HEIGHTMAP_BLUR_BOX
            #pragma shader_feature _HEIGHTMAP_BLUR_GAUSS
            #pragma shader_feature _GEOMETRY_CUSTOM_ST
            #pragma shader_feature _GEOMETRY_CUSTOM_UV_CHANNEL
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_0
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_1
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_2
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_3

            // Vertex Inputs
            #define VERTEX_POSITION_OS 1
            #define VERTEX_TANGENT_OS 1
            #define VERTEX_TEXCOORD 1
            #define VERTEX_NORMAL 1

            // Geometry Inputs
            #define GEOMETRY_NORMAL 1
            #define GEOMETRY_UV1 1
            #define GEOMETRY_UV2 1
            #define GEOMETRY_UV3 1

            #define PASS_DEPTH_NORMALS
            #define PassVertexProgram DepthNormalsVertex
            #define PassFragmentProgram DepthNormalsFragment

            #pragma vertex TessellatedVertex
            #pragma hull TessellatedHull
            #pragma domain TessellatedDomain
            #pragma fragment TessellatedFragment

            #include "LitTessellationInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/DepthNormalsPass.hlsl"
            #include "LitTessellationStructs.hlsl"
            #include "LitTessellationPasses.hlsl"

            ENDHLSL
        }

		// ------------------------------------------------------------------
		// Lightmap baking.
        Pass
        {
            Name "Meta"
            Tags{"LightMode" = "Meta"}

            Cull Off

            HLSLPROGRAM
            #pragma exclude_renderers gles gles3 glcore
            #pragma target 4.6

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local_fragment _SPECULAR_SETUP
            #pragma shader_feature_local_fragment _EMISSION
            #pragma shader_feature_local_fragment _METALLICSPECGLOSSMAP
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature_local _ _DETAIL_MULX2 _DETAIL_SCALED

            #pragma shader_feature_local_fragment _SPECGLOSSMAP

            // -------------------------------------
            // Geometry keywords
            #pragma shader_feature _VERTEX_COLOR
            #pragma shader_feature _VERTEX_COLOR_MULTIPLY
            #pragma shader_feature _VERTEX_COLOR_ADDITIVE
            #pragma shader_feature _VERTEX_COLOR_SUBTRACTIVE
            #pragma shader_feature _VERTEX_COLOR_REPLACE
            #pragma shader_feature _TESSELLATIONMAP
            #pragma shader_feature _TESSELLATION_EDGE
            #pragma shader_feature _TESSELLATION_DISTANCE
            #pragma shader_feature _TESSELLATION_PHONG
            #pragma shader_feature _TESSELLATION_CLIPPING
            #pragma shader_feature _HEIGHTMAP
            #pragma shader_feature _HEIGHTMAP_BLUR_BOX
            #pragma shader_feature _HEIGHTMAP_BLUR_GAUSS
            #pragma shader_feature _GEOMETRY_CUSTOM_ST
            #pragma shader_feature _GEOMETRY_CUSTOM_UV_CHANNEL
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_0
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_1
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_2
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_3

            // Vertex Inputs
            #define VERTEX_POSITION_OS 1
            #define VERTEX_NORMAL_OS 1
            #if _TANGENT_TO_WORLD
            #define VERTEX_TANGENT_OS 1
            #endif
            #define VERTEX_UV0 1
            #define VERTEX_UV1 1
            #define VERTEX_UV2 1

            // Geometry Inputs
            #define GEOMETRY_UV3 1

            // Pixel Inputs
            #define PIXEL_VERTEX_COLOR 1

            #define PassVertexProgram UniversalVertexMeta
            #define PassFragmentProgram UniversalFragmentMeta

            #pragma vertex TessellatedVertex
            #pragma hull TessellatedHull
            #pragma domain TessellatedDomain
            #pragma fragment TessellatedFragment

            #include "LitTessellationInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitMetaPass.hlsl"
            #include "LitTessellationStructs.hlsl"
            #include "LitTessellationPasses.hlsl"

            ENDHLSL
        }

		// ------------------------------------------------------------------
		// 2D pass.
        Pass
        {
            Name "Universal2D"
            Tags{ "LightMode" = "Universal2D" }

            Blend[_SrcBlend][_DstBlend]
            ZWrite[_ZWrite]
            Cull[_Cull]

            HLSLPROGRAM
            #pragma exclude_renderers gles gles3 glcore
            #pragma target 4.5

            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _ALPHAPREMULTIPLY_ON
            
            #include "LitTessellationInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/Utils/Universal2D.hlsl"
            ENDHLSL
        }
    }

    SubShader
    {
        // Universal Pipeline tag is required. If Universal render pipeline is not set in the graphics settings
        // this Subshader will fail. One can add a subshader below or fallback to Standard built-in to make this
        // material work with both Universal Render Pipeline and Builtin Unity Pipeline
        Tags{"RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" "UniversalMaterialType" = "Lit" "IgnoreProjector" = "True" "ShaderModel" = "4.6"}
        LOD 300

        // ------------------------------------------------------------------
        //  Forward pass. Shades all light in a single pass. GI + emission + Fog
        Pass
        {
            // Lightmode matches the ShaderPassName set in UniversalRenderPipeline.cs. SRPDefaultUnlit and passes with
            // no LightMode tag are also rendered by Universal Render Pipeline
            Name "ForwardLit"
            Tags{"LightMode" = "UniversalForward"}

            Blend[_SrcBlend][_DstBlend]
            ZWrite[_ZWrite]
            Cull[_Cull]

            HLSLPROGRAM
            #pragma only_renderers gles gles3 glcore d3d11
            #pragma target 4.6

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local _NORMALMAP
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _ALPHAPREMULTIPLY_ON
            #pragma shader_feature_local_fragment _EMISSION
            #pragma shader_feature_local_fragment _METALLICSPECGLOSSMAP
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature_local_fragment _OCCLUSIONMAP
            #pragma shader_feature_local _PARALLAXMAP
            #pragma shader_feature_local _ _DETAIL_MULX2 _DETAIL_SCALED

            #pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature_local_fragment _ENVIRONMENTREFLECTIONS_OFF
            #pragma shader_feature_local_fragment _SPECULAR_SETUP
            #pragma shader_feature_local _RECEIVE_SHADOWS_OFF

            // -------------------------------------
            // Universal Pipeline keywords
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile_fragment _ _SHADOWS_SOFT
            #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
            #pragma multi_compile _ SHADOWS_SHADOWMASK
            #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION

            // -------------------------------------
            // Unity defined keywords
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile_fog

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing

            // -------------------------------------
            // Geometry keywords
            #pragma shader_feature _VERTEX_COLOR
            #pragma shader_feature _VERTEX_COLOR_MULTIPLY
            #pragma shader_feature _VERTEX_COLOR_ADDITIVE
            #pragma shader_feature _VERTEX_COLOR_SUBTRACTIVE
            #pragma shader_feature _VERTEX_COLOR_REPLACE
            #pragma shader_feature _TESSELLATIONMAP
            #pragma shader_feature _TESSELLATION_EDGE
            #pragma shader_feature _TESSELLATION_DISTANCE
            #pragma shader_feature _TESSELLATION_PHONG
            #pragma shader_feature _TESSELLATION_CLIPPING
            #pragma shader_feature _HEIGHTMAP
            #pragma shader_feature _HEIGHTMAP_BLUR_BOX
            #pragma shader_feature _HEIGHTMAP_BLUR_GAUSS
            #pragma shader_feature _GEOMETRY_CUSTOM_ST
            #pragma shader_feature _GEOMETRY_CUSTOM_UV_CHANNEL
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_0
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_1
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_2
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_3

            // Vertex Inputs
            #define VERTEX_POSITION_OS 1
            #define VERTEX_NORMAL_OS 1
            #define VERTEX_TANGENT_OS 1
            #define VERTEX_TEXCOORD 1
            #define VERTEX_LIGHTMAP_UV 1

            // Geometry Inputs
            #define GEOMETRY_UV2 1
            #define GEOMETRY_UV3 1

            // Pixel Inputs
            #define PIXEL_VERTEX_COLOR 1

            #define PASS_FORWARD
            #define PassVertexProgram LitPassVertex
            #define PassFragmentProgram LitPassFragment

            #pragma vertex TessellatedVertex
            #pragma hull TessellatedHull
            #pragma domain TessellatedDomain
            #pragma fragment TessellatedFragment

            #include "LitTessellationInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitForwardPass.hlsl"
            #include "LitTessellationStructs.hlsl"
            #include "LitTessellationPasses.hlsl"

            ENDHLSL
        }

        // ------------------------------------------------------------------
        //  Forward pass. Shadow casting.
        Pass
        {
            Name "ShadowCaster"
            Tags{"LightMode" = "ShadowCaster"}

            ZWrite On
            ZTest LEqual
            ColorMask 0
            Cull[_Cull]

            HLSLPROGRAM
            #pragma only_renderers gles gles3 glcore d3d11
            #pragma target 4.6

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            // -------------------------------------
            // Universal Pipeline keywords
            // This is used during shadow map generation to differentiate between directional and punctual light shadows, as they use different formulas to apply Normal Bias
            #pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing

            // -------------------------------------
            // Geometry keywords
            #pragma shader_feature _TESSELLATIONMAP
            #pragma shader_feature _TESSELLATION_EDGE
            #pragma shader_feature _TESSELLATION_DISTANCE
            #pragma shader_feature _TESSELLATION_PHONG
            #pragma shader_feature _TESSELLATION_CLIPPING
            #pragma shader_feature _HEIGHTMAP
            #pragma shader_feature _HEIGHTMAP_BLUR_BOX
            #pragma shader_feature _HEIGHTMAP_BLUR_GAUSS
            #pragma shader_feature _GEOMETRY_CUSTOM_ST
            #pragma shader_feature _GEOMETRY_CUSTOM_UV_CHANNEL
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_0
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_1
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_2
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_3

            // Vertex Inputs
            #define VERTEX_POSITION_OS 1
            #define VERTEX_NORMAL_OS 1
            #define VERTEX_TEXCOORD 1

            // Geometry Inputs
            #define GEOMETRY_UV1 1
            #define GEOMETRY_UV2 1
            #define GEOMETRY_UV3 1
                  
            #define PASS_SHADOW
            #define PassVertexProgram ShadowPassVertex
            #define PassFragmentProgram ShadowPassFragment

            #pragma vertex TessellatedVertex
            #pragma hull TessellatedHull
            #pragma domain TessellatedDomain
            #pragma fragment TessellatedFragment

            #include "LitTessellationInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/ShadowCasterPass.hlsl"
            #include "LitTessellationStructs.hlsl"
            #include "LitTessellationPasses.hlsl"

            ENDHLSL
        }
                  
        // ------------------------------------------------------------------
        // Depth only pass.
        Pass
        {
            Name "DepthOnly"
            Tags{"LightMode" = "DepthOnly"}

            ZWrite On
            ColorMask 0
            Cull[_Cull]

            HLSLPROGRAM
            #pragma only_renderers gles gles3 glcore d3d11
            #pragma target 4.6

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing

            // -------------------------------------
            // Geometry keywords
            #pragma shader_feature _TESSELLATIONMAP
            #pragma shader_feature _TESSELLATION_EDGE
            #pragma shader_feature _TESSELLATION_DISTANCE
            #pragma shader_feature _TESSELLATION_PHONG
            #pragma shader_feature _TESSELLATION_CLIPPING
            #pragma shader_feature _HEIGHTMAP
            #pragma shader_feature _HEIGHTMAP_BLUR_BOX
            #pragma shader_feature _HEIGHTMAP_BLUR_GAUSS
            #pragma shader_feature _GEOMETRY_CUSTOM_ST
            #pragma shader_feature _GEOMETRY_CUSTOM_UV_CHANNEL
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_0
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_1
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_2
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_3

            // Vertex Inputs
            #define VERTEX_POSITION 1
            #define VERTEX_TEXCOORD 1

            // Geometry Inputs
            #define GEOMETRY_NORMAL 1
            #define GEOMETRY_UV1 1
            #define GEOMETRY_UV2 1
            #define GEOMETRY_UV3 1

            #define PASS_DEPTH
            #define PassVertexProgram DepthOnlyVertex
            #define PassFragmentProgram DepthOnlyFragment

            #pragma vertex TessellatedVertex
            #pragma hull TessellatedHull
            #pragma domain TessellatedDomain
            #pragma fragment TessellatedFragment

            #include "LitTessellationInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/DepthOnlyPass.hlsl"
            #include "LitTessellationStructs.hlsl"
            #include "LitTessellationPasses.hlsl"

            ENDHLSL
        }

        // ------------------------------------------------------------------
        // This pass is used when drawing to a _CameraNormalsTexture texture
        Pass
        {
            Name "DepthNormals"
            Tags{"LightMode" = "DepthNormals"}

            ZWrite On
            Cull[_Cull]

            HLSLPROGRAM
            #pragma only_renderers gles gles3 glcore d3d11
            #pragma target 4.6

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local _NORMALMAP
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing

            // -------------------------------------
            // Geometry keywords
            #pragma shader_feature _TESSELLATIONMAP
            #pragma shader_feature _TESSELLATION_EDGE
            #pragma shader_feature _TESSELLATION_DISTANCE
            #pragma shader_feature _TESSELLATION_PHONG
            #pragma shader_feature _TESSELLATION_CLIPPING
            #pragma shader_feature _HEIGHTMAP
            #pragma shader_feature _HEIGHTMAP_BLUR_BOX
            #pragma shader_feature _HEIGHTMAP_BLUR_GAUSS
            #pragma shader_feature _GEOMETRY_CUSTOM_ST
            #pragma shader_feature _GEOMETRY_CUSTOM_UV_CHANNEL
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_0
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_1
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_2
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_3

            // Vertex Inputs
            #define VERTEX_POSITION_OS 1
            #define VERTEX_TANGENT_OS 1
            #define VERTEX_TEXCOORD 1
            #define VERTEX_NORMAL 1

            // Geometry Inputs
            #define GEOMETRY_NORMAL 1
            #define GEOMETRY_UV1 1
            #define GEOMETRY_UV2 1
            #define GEOMETRY_UV3 1

            #define PASS_DEPTH_NORMALS
            #define PassVertexProgram DepthNormalsVertex
            #define PassFragmentProgram DepthNormalsFragment

            #pragma vertex TessellatedVertex
            #pragma hull TessellatedHull
            #pragma domain TessellatedDomain
            #pragma fragment TessellatedFragment

            #include "LitTessellationInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/DepthNormalsPass.hlsl"
            #include "LitTessellationStructs.hlsl"
            #include "LitTessellationPasses.hlsl"

            ENDHLSL
        }

        // ------------------------------------------------------------------
        // Lightmap baking.
        Pass
        {
            Name "Meta"
            Tags{"LightMode" = "Meta"}

            Cull Off

            HLSLPROGRAM
            #pragma only_renderers gles gles3 glcore d3d11
            #pragma target 4.6

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local_fragment _SPECULAR_SETUP
            #pragma shader_feature_local_fragment _EMISSION
            #pragma shader_feature_local_fragment _METALLICSPECGLOSSMAP
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature_local _ _DETAIL_MULX2 _DETAIL_SCALED

            #pragma shader_feature_local_fragment _SPECGLOSSMAP

            // -------------------------------------
            // Geometry keywords
            #pragma shader_feature _VERTEX_COLOR
            #pragma shader_feature _VERTEX_COLOR_MULTIPLY
            #pragma shader_feature _VERTEX_COLOR_ADDITIVE
            #pragma shader_feature _VERTEX_COLOR_SUBTRACTIVE
            #pragma shader_feature _VERTEX_COLOR_REPLACE
            #pragma shader_feature _TESSELLATIONMAP
            #pragma shader_feature _TESSELLATION_EDGE
            #pragma shader_feature _TESSELLATION_DISTANCE
            #pragma shader_feature _TESSELLATION_PHONG
            #pragma shader_feature _TESSELLATION_CLIPPING
            #pragma shader_feature _HEIGHTMAP
            #pragma shader_feature _HEIGHTMAP_BLUR_BOX
            #pragma shader_feature _HEIGHTMAP_BLUR_GAUSS
            #pragma shader_feature _GEOMETRY_CUSTOM_ST
            #pragma shader_feature _GEOMETRY_CUSTOM_UV_CHANNEL
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_0
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_1
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_2
            #pragma shader_feature _GEOMETRY_UV_CHANNEL_3

            // Vertex Inputs
            #define VERTEX_POSITION_OS 1
            #define VERTEX_NORMAL_OS 1
            #if _TANGENT_TO_WORLD
            #define VERTEX_TANGENT_OS 1
            #endif
            #define VERTEX_UV0 1
            #define VERTEX_UV1 1
            #define VERTEX_UV2 1

            // Geometry Inputs
            #define GEOMETRY_UV3 1

            // Pixel Inputs
            #define PIXEL_VERTEX_COLOR 1

            #define PassVertexProgram UniversalVertexMeta
            #define PassFragmentProgram UniversalFragmentMeta

            #pragma vertex TessellatedVertex
            #pragma hull TessellatedHull
            #pragma domain TessellatedDomain
            #pragma fragment TessellatedFragment

            #include "LitTessellationInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitMetaPass.hlsl"
            #include "LitTessellationStructs.hlsl"
            #include "LitTessellationPasses.hlsl"

            ENDHLSL
        }

        // ------------------------------------------------------------------
        // 2D pass.
        Pass
        {
            Name "Universal2D"
            Tags{ "LightMode" = "Universal2D" }

            Blend[_SrcBlend][_DstBlend]
            ZWrite[_ZWrite]
            Cull[_Cull]

            HLSLPROGRAM
            #pragma only_renderers gles gles3 glcore d3d11
            #pragma target 2.0

            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _ALPHAPREMULTIPLY_ON
                  
            #include "LitTessellationInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/Utils/Universal2D.hlsl"
            ENDHLSL
        }
    }

    FallBack "Hidden/Universal Render Pipeline/FallbackError"
        
	CustomEditor "UnityEditor.Rendering.Universal.ShaderGUI.LitTessellationShader"
}
