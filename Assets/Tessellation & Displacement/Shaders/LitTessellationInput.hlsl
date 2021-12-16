#ifndef UNIVERSAL_LIT_TESSELLATION_INPUT_INCLUDED
#define UNIVERSAL_LIT_TESSELLATION_INPUT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/ParallaxMapping.hlsl"
#include "Library/Blur.hlsl"

#if defined(_DETAIL_MULX2) || defined(_DETAIL_SCALED)
#define _DETAIL
#endif

CBUFFER_START(UnityPerMaterial)
float4 _BaseMap_ST;
float4 _DetailAlbedoMap_ST;
half4 _BaseColor;
half4 _SpecColor;
half4 _EmissionColor;
half _Cutoff;
half _Smoothness;
half _Metallic;
half _BumpScale;
half _Parallax;
half _OcclusionStrength;
half _ClearCoatMask;
half _ClearCoatSmoothness;
half _DetailAlbedoMapScale;
half _DetailNormalMapScale;
half _Surface;

half _VertexColorBlending;
float4 _TessellationMap_ST;
half _TessellationScale;
half _TessellationFactor;
half _TessellationFactorMin;
half _TessellationFactorMax;
half _TessellationDistanceMin;
half _TessellationDistanceMax;
half _TessellationEdgeLength;
half _TessellationEdgeDistanceOffset;
half _TessellationPhongShape;
half _TessellationTriangleClipBias;
half _HeightStrength;
half _HeightBase;
half _HeightBlurSize;
half _HeightBlurSamples;
half _HeightBlurGaussStandardDeviation;
half _GeometryCustomST;
half _GeometryUVChannel;
CBUFFER_END

CBUFFER_START(CustomPerMaterial)
half4 _VertexColor;
CBUFFER_END

// NOTE: Do not ifdef the properties for dots instancing, but ifdef the actual usage.
// Otherwise you might break CPU-side as property constant-buffer offsets change per variant.
// NOTE: Dots instancing is orthogonal to the constant buffer above.
#ifdef UNITY_DOTS_INSTANCING_ENABLED
UNITY_DOTS_INSTANCING_START(MaterialPropertyMetadata)
    UNITY_DOTS_INSTANCED_PROP(float4, _BaseColor)
    UNITY_DOTS_INSTANCED_PROP(float4, _SpecColor)
    UNITY_DOTS_INSTANCED_PROP(float4, _EmissionColor)
    UNITY_DOTS_INSTANCED_PROP(float , _Cutoff)
    UNITY_DOTS_INSTANCED_PROP(float , _Smoothness)
    UNITY_DOTS_INSTANCED_PROP(float , _Metallic)
    UNITY_DOTS_INSTANCED_PROP(float , _BumpScale)
    UNITY_DOTS_INSTANCED_PROP(float , _Parallax)
    UNITY_DOTS_INSTANCED_PROP(float , _OcclusionStrength)
    UNITY_DOTS_INSTANCED_PROP(float , _ClearCoatMask)
    UNITY_DOTS_INSTANCED_PROP(float , _ClearCoatSmoothness)
    UNITY_DOTS_INSTANCED_PROP(float , _DetailAlbedoMapScale)
    UNITY_DOTS_INSTANCED_PROP(float , _DetailNormalMapScale)
    UNITY_DOTS_INSTANCED_PROP(float , _Surface)

    UNITY_DOTS_INSTANCED_PROP(float , _VertexColorBlending)
    UNITY_DOTS_INSTANCED_PROP(float , _TessellationScale)
    UNITY_DOTS_INSTANCED_PROP(float , _TessellationFactor)
    UNITY_DOTS_INSTANCED_PROP(float , _TessellationFactorMin)
    UNITY_DOTS_INSTANCED_PROP(float , _TessellationFactorMax)
    UNITY_DOTS_INSTANCED_PROP(float , _TessellationDistanceMin)
    UNITY_DOTS_INSTANCED_PROP(float , _TessellationDistanceMax)
    UNITY_DOTS_INSTANCED_PROP(float , _TessellationEdgeLength)
    UNITY_DOTS_INSTANCED_PROP(float , _TessellationEdgeDistanceOffset)
    UNITY_DOTS_INSTANCED_PROP(float , _TessellationPhongShape)
    UNITY_DOTS_INSTANCED_PROP(float , _TessellationTriangleClipBias)
    UNITY_DOTS_INSTANCED_PROP(float , _HeightStrength)
    UNITY_DOTS_INSTANCED_PROP(float , _HeightBase)
    UNITY_DOTS_INSTANCED_PROP(float , _HeightBlurSize)
    UNITY_DOTS_INSTANCED_PROP(float , _HeightBlurSamples)
    UNITY_DOTS_INSTANCED_PROP(float , _HeightBlurGaussStandardDeviation)
    UNITY_DOTS_INSTANCED_PROP(float , _GeometryUVChannel)
UNITY_DOTS_INSTANCING_END(MaterialPropertyMetadata)

#define _BaseColor              UNITY_ACCESS_DOTS_INSTANCED_PROP_FROM_MACRO(float4 , Metadata__BaseColor)
#define _SpecColor              UNITY_ACCESS_DOTS_INSTANCED_PROP_FROM_MACRO(float4 , Metadata__SpecColor)
#define _EmissionColor          UNITY_ACCESS_DOTS_INSTANCED_PROP_FROM_MACRO(float4 , Metadata__EmissionColor)
#define _Cutoff                 UNITY_ACCESS_DOTS_INSTANCED_PROP_FROM_MACRO(float  , Metadata__Cutoff)
#define _Smoothness             UNITY_ACCESS_DOTS_INSTANCED_PROP_FROM_MACRO(float  , Metadata__Smoothness)
#define _Metallic               UNITY_ACCESS_DOTS_INSTANCED_PROP_FROM_MACRO(float  , Metadata__Metallic)
#define _BumpScale              UNITY_ACCESS_DOTS_INSTANCED_PROP_FROM_MACRO(float  , Metadata__BumpScale)
#define _Parallax               UNITY_ACCESS_DOTS_INSTANCED_PROP_FROM_MACRO(float  , Metadata__Parallax)
#define _OcclusionStrength      UNITY_ACCESS_DOTS_INSTANCED_PROP_FROM_MACRO(float  , Metadata__OcclusionStrength)
#define _ClearCoatMask          UNITY_ACCESS_DOTS_INSTANCED_PROP_FROM_MACRO(float  , Metadata__ClearCoatMask)
#define _ClearCoatSmoothness    UNITY_ACCESS_DOTS_INSTANCED_PROP_FROM_MACRO(float  , Metadata__ClearCoatSmoothness)
#define _DetailAlbedoMapScale   UNITY_ACCESS_DOTS_INSTANCED_PROP_FROM_MACRO(float  , Metadata__DetailAlbedoMapScale)
#define _DetailNormalMapScale   UNITY_ACCESS_DOTS_INSTANCED_PROP_FROM_MACRO(float  , Metadata__DetailNormalMapScale)
#define _Surface                UNITY_ACCESS_DOTS_INSTANCED_PROP_FROM_MACRO(float  , Metadata__Surface)

#define _VertexColorBlending                UNITY_DOTS_INSTANCED_PROP(float , _VertexColorBlending)
#define _TessellationScale                  UNITY_DOTS_INSTANCED_PROP(float , _TessellationScale)
#define _TessellationFactor                 UNITY_DOTS_INSTANCED_PROP(float , _TessellationFactor)
#define _TessellationFactorMin              UNITY_DOTS_INSTANCED_PROP(float , _TessellationFactorMin)
#define _TessellationFactorMax              UNITY_DOTS_INSTANCED_PROP(float , _TessellationFactorMax)
#define _TessellationDistanceMin            UNITY_DOTS_INSTANCED_PROP(float , _TessellationDistanceMin)
#define _TessellationDistanceMax            UNITY_DOTS_INSTANCED_PROP(float , _TessellationDistanceMax)
#define _TessellationEdgeLength             UNITY_DOTS_INSTANCED_PROP(float , _TessellationEdgeLength)
#define _TessellationEdgeDistanceOffset     UNITY_DOTS_INSTANCED_PROP(float , _TessellationEdgeDistanceOffset)
#define _TessellationPhongShape             UNITY_DOTS_INSTANCED_PROP(float , _TessellationPhongShape)
#define _TessellationTriangleClipBias       UNITY_DOTS_INSTANCED_PROP(float , _TessellationTriangleClipBias)
#define _HeightStrength                     UNITY_DOTS_INSTANCED_PROP(float , _HeightStrength)
#define _HeightBase                         UNITY_DOTS_INSTANCED_PROP(float , _HeightBase)
#define _HeightBlurSize                     UNITY_DOTS_INSTANCED_PROP(float , _HeightBlurSize)
#define _HeightBlurSamples                  UNITY_DOTS_INSTANCED_PROP(float , _HeightBlurSamples)
#define _HeightBlurGaussStandardDeviation   UNITY_DOTS_INSTANCED_PROP(float , _HeightBlurGaussStandardDeviation)
#define _GeometryUVChannel                  UNITY_DOTS_INSTANCED_PROP(float , _GeometryUVChannel)
#endif

TEXTURE2D(_ParallaxMap);        SAMPLER(sampler_ParallaxMap);
TEXTURE2D(_OcclusionMap);       SAMPLER(sampler_OcclusionMap);
TEXTURE2D(_DetailMask);         SAMPLER(sampler_DetailMask);
TEXTURE2D(_DetailAlbedoMap);    SAMPLER(sampler_DetailAlbedoMap);
TEXTURE2D(_DetailNormalMap);    SAMPLER(sampler_DetailNormalMap);
TEXTURE2D(_MetallicGlossMap);   SAMPLER(sampler_MetallicGlossMap);
TEXTURE2D(_SpecGlossMap);       SAMPLER(sampler_SpecGlossMap);
TEXTURE2D(_ClearCoatMap);       SAMPLER(sampler_ClearCoatMap);

TEXTURE2D(_HeightMap);			SAMPLER(sampler_HeightMap);
TEXTURE2D(_TessellationMap);	SAMPLER(sampler_TessellationMap);

#ifdef _SPECULAR_SETUP
#define SAMPLE_METALLICSPECULAR(uv) SAMPLE_TEXTURE2D(_SpecGlossMap, sampler_SpecGlossMap, uv)
#else
#define SAMPLE_METALLICSPECULAR(uv) SAMPLE_TEXTURE2D(_MetallicGlossMap, sampler_MetallicGlossMap, uv)
#endif

#ifdef _GEOMETRY_CUSTOM_ST
#define TESSELLATION_TRANSFORM_SOURCE _TessellationMap
#else
#define TESSELLATION_TRANSFORM_SOURCE _BaseMap
#endif

// ***** Structures field names keywords ******
// Are required to support the different fields each pass has without creating custom structs for each of them.

// Position
#if VERTEX_POSITION
#define HAS_POSITION 1
#define HAS_VERTEX_POSITION 1
#define POSITION_ID position

#elif VERTEX_POSITION_OS
#define HAS_POSITION 1
#define HAS_VERTEX_POSITION 1
#define POSITION_ID positionOS

#elif GEOMETRY_POSITION
#define HAS_POSITION 1
#define HAS_GEOMETRY_POSITION 1
#define POSITION_ID position
#endif

// Normal
#if VERTEX_NORMAL
#define HAS_NORMAL 1
#define HAS_VERTEX_NORMAL 1
#define NORMAL_ID normal

#elif VERTEX_NORMAL_OS
#define HAS_NORMAL 1
#define HAS_VERTEX_NORMAL 1
#define NORMAL_ID normalOS

#elif GEOMETRY_NORMAL
#define HAS_NORMAL 1
#define HAS_GEOMETRY_NORMAL 1
#define NORMAL_ID normal
#endif

// UV0
#if VERTEX_UV0
#define HAS_UV0 1
#define HAS_VERTEX_UV0 1
#define UV0_ID uv0

#elif VERTEX_TEXCOORD
#define HAS_UV0 1
#define HAS_VERTEX_UV0 1
#define UV0_ID texcoord

#elif GEOMETRY_UV0
#define HAS_UV0 1
#define HAS_GEOMETRY_UV0 1
#define UV0_ID uv0
#endif

// UV1
#if VERTEX_UV1
#define HAS_UV1 1
#define HAS_VERTEX_UV1 1
#define UV1_ID uv1

#elif VERTEX_LIGHTMAP_UV
#define HAS_UV1 1
#define HAS_VERTEX_UV1 1
#define UV1_ID lightmapUV

#elif GEOMETRY_UV1
#define HAS_UV1 1
#define HAS_GEOMETRY_UV1 1
#define UV1_ID uv1
#endif

// UV2
#if VERTEX_UV2
#define HAS_UV2 1
#define HAS_VERTEX_UV2 1
#define UV2_ID uv2

#elif defined(GEOMETRY_UV2) && defined(_GEOMETRY_UV_CHANNEL_2)
#define HAS_UV2 1
#define HAS_GEOMETRY_UV2 1
#define UV2_ID uv2
#endif

// UV3
#if VERTEX_UV3
#define HAS_UV3 1
#define HAS_VERTEX_UV3 1
#define UV3_ID uv3

#elif defined(GEOMETRY_UV3) && defined(_GEOMETRY_UV_CHANNEL_3)
#define HAS_UV3 1
#define HAS_GEOMETRY_UV3 1
#define UV3_ID uv3
#endif

// Tangent
#if VERTEX_TANGENT
#define HAS_TANGENT 1
#define HAS_VERTEX_TANGENT 1
#define TANGENT_ID tangent

#elif VERTEX_TANGENT_OS
#define HAS_TANGENT 1
#define HAS_VERTEX_TANGENT 1
#define TANGENT_ID tangentOS

#elif GEOMETRY_TANGENT_OS
#define HAS_TANGENT 1
#define HAS_GEOMETRY_TANGENT 1
#define TANGENT_ID tangent
#endif

// Vertex Color
#if defined(PIXEL_VERTEX_COLOR) && defined(_VERTEX_COLOR)
#define HAS_VERTEX_COLOR 1
#define VERTEX_COLOR_ID color
#endif

// UV Channel
#ifdef _GEOMETRY_CUSTOM_UV_CHANNEL
#ifdef _GEOMETRY_UV_CHANNEL_1
#define GEOMETRY_UV_CHANNEL UV1_ID
#elif _GEOMETRY_UV_CHANNEL_2
#define GEOMETRY_UV_CHANNEL UV2_ID
#elif _GEOMETRY_UV_CHANNEL_3
#define GEOMETRY_UV_CHANNEL UV3_ID
#else
#define GEOMETRY_UV_CHANNEL UV0_ID
#endif
#else
#define GEOMETRY_UV_CHANNEL UV0_ID
#endif

// ***** End Structures field names keywords ******

half4 SampleMetallicSpecGloss(float2 uv, half albedoAlpha)
{
    half4 specGloss;

#ifdef _METALLICSPECGLOSSMAP
    specGloss = SAMPLE_METALLICSPECULAR(uv);
    #ifdef _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
        specGloss.a = albedoAlpha * _Smoothness;
    #else
        specGloss.a *= _Smoothness;
    #endif
#else // _METALLICSPECGLOSSMAP
    #if _SPECULAR_SETUP
        specGloss.rgb = _SpecColor.rgb;
    #else
        specGloss.rgb = _Metallic.rrr;
    #endif

    #ifdef _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
        specGloss.a = albedoAlpha * _Smoothness;
    #else
        specGloss.a = _Smoothness;
    #endif
#endif

    return specGloss;
}

half SampleOcclusion(float2 uv)
{
#ifdef _OCCLUSIONMAP
// TODO: Controls things like these by exposing SHADER_QUALITY levels (low, medium, high)
#if defined(SHADER_API_GLES)
    return SAMPLE_TEXTURE2D(_OcclusionMap, sampler_OcclusionMap, uv).g;
#else
    half occ = SAMPLE_TEXTURE2D(_OcclusionMap, sampler_OcclusionMap, uv).g;
    return LerpWhiteTo(occ, _OcclusionStrength);
#endif
#else
    return 1.0;
#endif
}


// Returns clear coat parameters
// .x/.r == mask
// .y/.g == smoothness
half2 SampleClearCoat(float2 uv)
{
#if defined(_CLEARCOAT) || defined(_CLEARCOATMAP)
    half2 clearCoatMaskSmoothness = half2(_ClearCoatMask, _ClearCoatSmoothness);

#if defined(_CLEARCOATMAP)
    clearCoatMaskSmoothness *= SAMPLE_TEXTURE2D(_ClearCoatMap, sampler_ClearCoatMap, uv).rg;
#endif

    return clearCoatMaskSmoothness;
#else
    return half2(0.0, 1.0);
#endif  // _CLEARCOAT
}

void ApplyPerPixelDisplacement(half3 viewDirTS, inout float2 uv)
{
#if defined(_PARALLAXMAP)
    uv += ParallaxMapping(TEXTURE2D_ARGS(_ParallaxMap, sampler_ParallaxMap), viewDirTS, _Parallax, uv);
#endif
}

// Used for scaling detail albedo. Main features:
// - Depending if detailAlbedo brightens or darkens, scale magnifies effect.
// - No effect is applied if detailAlbedo is 0.5.
half3 ScaleDetailAlbedo(half3 detailAlbedo, half scale)
{
    // detailAlbedo = detailAlbedo * 2.0h - 1.0h;
    // detailAlbedo *= _DetailAlbedoMapScale;
    // detailAlbedo = detailAlbedo * 0.5h + 0.5h;
    // return detailAlbedo * 2.0f;

    // A bit more optimized
    return 2.0h * detailAlbedo * scale - scale + 1.0h;
}

half3 ApplyDetailAlbedo(float2 detailUv, half3 albedo, half detailMask)
{
#if defined(_DETAIL)
    half3 detailAlbedo = SAMPLE_TEXTURE2D(_DetailAlbedoMap, sampler_DetailAlbedoMap, detailUv).rgb;

    // In order to have same performance as builtin, we do scaling only if scale is not 1.0 (Scaled version has 6 additional instructions)
#if defined(_DETAIL_SCALED)
    detailAlbedo = ScaleDetailAlbedo(detailAlbedo, _DetailAlbedoMapScale);
#else
    detailAlbedo = 2.0h * detailAlbedo;
#endif

    return albedo * LerpWhiteTo(detailAlbedo, detailMask);
#endif
}

half3 ApplyDetailNormal(float2 detailUv, half3 normalTS, half detailMask)
{
#if defined(_DETAIL)
#if BUMP_SCALE_NOT_SUPPORTED
    half3 detailNormalTS = UnpackNormal(SAMPLE_TEXTURE2D(_DetailNormalMap, sampler_DetailNormalMap, detailUv));
#else
    half3 detailNormalTS = UnpackNormalScale(SAMPLE_TEXTURE2D(_DetailNormalMap, sampler_DetailNormalMap, detailUv), _DetailNormalMapScale);
#endif

    // With UNITY_NO_DXT5nm unpacked vector is not normalized for BlendNormalRNM
    // For visual consistancy we going to do in all cases
    detailNormalTS = normalize(detailNormalTS);

    return lerp(normalTS, BlendNormalRNM(normalTS, detailNormalTS), detailMask); // todo: detailMask should lerp the angle of the quaternion rotation, not the normals
#endif
}

inline void InitializeStandardLitSurfaceData(float2 uv, out SurfaceData outSurfaceData)
{
    half4 albedoAlpha = SampleAlbedoAlpha(uv, TEXTURE2D_ARGS(_BaseMap, sampler_BaseMap));
    outSurfaceData.alpha = Alpha(albedoAlpha.a, _BaseColor, _Cutoff);

    half4 specGloss = SampleMetallicSpecGloss(uv, albedoAlpha.a);
    outSurfaceData.albedo = albedoAlpha.rgb * _BaseColor.rgb;

#if HAS_VERTEX_COLOR
#if _VERTEX_COLOR_MULTIPLY
    outSurfaceData.albedo.rgb = outSurfaceData.albedo.rgb * (1 - _VertexColorBlending) + outSurfaceData.albedo.rgb * _VertexColor.rgb * _VertexColorBlending;
#elif _VERTEX_COLOR_ADDITIVE
    outSurfaceData.albedo.rgb = saturate(outSurfaceData.albedo.rgb + _VertexColor.rgb * _VertexColorBlending);
#elif _VERTEX_COLOR_SUBTRACTIVE
    outSurfaceData.albedo.rgb = saturate(outSurfaceData.albedo.rgb - _VertexColor.rgb * _VertexColorBlending);
#elif _VERTEX_COLOR_REPLACE
    outSurfaceData.albedo.rgb = lerp(outSurfaceData.albedo.rgb, _VertexColor.rgb, _VertexColorBlending);
#endif
#endif

#if _SPECULAR_SETUP
    outSurfaceData.metallic = 1.0h;
    outSurfaceData.specular = specGloss.rgb;
#else
    outSurfaceData.metallic = specGloss.r;
    outSurfaceData.specular = half3(0.0h, 0.0h, 0.0h);
#endif

    outSurfaceData.smoothness = specGloss.a;
    outSurfaceData.normalTS = SampleNormal(uv, TEXTURE2D_ARGS(_BumpMap, sampler_BumpMap), _BumpScale);
    outSurfaceData.occlusion = SampleOcclusion(uv);
    outSurfaceData.emission = SampleEmission(uv, _EmissionColor.rgb, TEXTURE2D_ARGS(_EmissionMap, sampler_EmissionMap));

#if defined(_CLEARCOAT) || defined(_CLEARCOATMAP)
    half2 clearCoat = SampleClearCoat(uv);
    outSurfaceData.clearCoatMask       = clearCoat.r;
    outSurfaceData.clearCoatSmoothness = clearCoat.g;
#else
    outSurfaceData.clearCoatMask       = 0.0h;
    outSurfaceData.clearCoatSmoothness = 0.0h;
#endif

#if defined(_DETAIL)
    half detailMask = SAMPLE_TEXTURE2D(_DetailMask, sampler_DetailMask, uv).a;
    float2 detailUv = uv * _DetailAlbedoMap_ST.xy + _DetailAlbedoMap_ST.zw;
    outSurfaceData.albedo = ApplyDetailAlbedo(detailUv, outSurfaceData.albedo, detailMask);
    outSurfaceData.normalTS = ApplyDetailNormal(detailUv, outSurfaceData.normalTS, detailMask);

#endif
}

half SampleTessellation(float2 uv)
{
#ifdef _TESSELLATIONMAP
    half sampledTessellation = max(SAMPLE_TEXTURE2D_LOD(_TessellationMap, sampler_TessellationMap, uv, 0).g, 0.01);
	return lerp(1.0, sampledTessellation, _TessellationScale);
#else
	return max(_TessellationScale, 0.01);
#endif
}

half SampleHeight(float2 uv)
{
#ifdef _HEIGHTMAP
#if defined(SHADER_API_GLES)
	return SAMPLE_TEXTURE2D_LOD(_HeightMap, sampler_HeightMap, uv, 0).b;
#else
#ifdef _HEIGHTMAP_BLUR_BOX
	return (SAMPLE_TEXTURE2D_LOD_BLUR_BOX(_HeightMap, sampler_HeightMap, uv, 0, _HeightBlurSize, _HeightBlurSamples).b + _HeightBase) * _HeightStrength;
#elif _HEIGHTMAP_BLUR_GAUSS
	return (SAMPLE_TEXTURE2D_LOD_BLUR_GAUSS(_HeightMap, sampler_HeightMap, uv, 0, _HeightBlurSize, _HeightBlurSamples, _HeightBlurGaussStandardDeviation * 0.1).b + _HeightBase) * _HeightStrength;
#else
	return (SAMPLE_TEXTURE2D_LOD(_HeightMap, sampler_HeightMap, uv, 0).b + _HeightBase) * _HeightStrength;
#endif
#endif
#else
	return 0.0;
#endif
}

half SampleHeightWorldCoordinates(float3 positionWS, float3 normalWS)
{
#ifndef _HEIGHTMAP
	return 0.0;
#else

#ifdef _HEIGHTMAP_BLUR_BOX
#define SAMPLE(uv) (SAMPLE_TEXTURE2D_LOD(_HeightMap, sampler_HeightMap, uv, 0).b + _HeightBase) * _HeightStrength
#elif _HEIGHTMAP_BLUR_GAUSS
#define SAMPLE(uv) (SAMPLE_TEXTURE2D_LOD_BLUR_BOX(_HeightMap, sampler_HeightMap, uv, 0, _HeightBlurSize, _HeightBlurSamples).b + _HeightBase) * _HeightStrength
#else
#define SAMPLE(uv) (SAMPLE_TEXTURE2D_LOD_BLUR_GAUSS(_HeightMap, sampler_HeightMap, uv, 0, _HeightBlurSize, _HeightBlurSamples, _HeightBlurGaussStandardDeviation * 0.1).b + _HeightBase) * _HeightStrength
#endif

	half height0 = SAMPLE(positionWS.xy);
    half height1 = SAMPLE(positionWS.zx);
    half height2 = SAMPLE(positionWS.zy);

    return lerp(lerp(height1, height0, normalWS.z), height2, normalWS.x);

#endif
}

#endif // UNIVERSAL_LIT_TESSELLATION_INPUT_INCLUDED
