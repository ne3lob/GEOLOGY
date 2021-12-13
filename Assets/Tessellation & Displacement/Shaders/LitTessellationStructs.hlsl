#ifndef UNIVERSAL_LIT_TESSELLATION_STRUCTS_INCLUDED
#define UNIVERSAL_LIT_TESSELLATION_STRUCTS_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

// The input structure of the tessellation vertex shader
struct TessellationAttributes
{
#if HAS_POSITION
	float4 POSITION_ID		: POSITION;
#endif
#if HAS_NORMAL
	float3 NORMAL_ID		: NORMAL;
#endif
#if HAS_TANGENT
	float4 TANGENT_ID		: TANGENT;
#endif
#if HAS_UV0
	float2 UV0_ID			: TEXCOORD0;
#endif
#if HAS_UV1
	float2 UV1_ID			: TEXCOORD1;
#endif
#if HAS_UV2
	float2 UV2_ID			: TEXCOORD2;
#endif
#if HAS_UV3
	float2 UV3_ID			: TEXCOORD3;
#endif
#if HAS_VERTEX_COLOR
	half4 VERTEX_COLOR_ID	: COLOR;
#endif

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

// The output structure of the tessellation vertex shader, and input of the geometry shaders
struct TessellationControlPoint
{
#if HAS_POSITION
	float4 POSITION_ID		: INTERNALTESSPOS;
#endif
#if HAS_NORMAL
	float3 NORMAL_ID		: NORMAL;
#endif
#if HAS_TANGENT
	float4 TANGENT_ID		: TANGENT;
#endif
#if HAS_UV0
	float2 UV0_ID			: TEXCOORD0;
#endif
#if HAS_UV1
	float2 UV1_ID			: TEXCOORD1;
#endif
#if HAS_UV2
	float2 UV2_ID			: TEXCOORD2;
#endif
#if HAS_UV3
	float2 UV3_ID			: TEXCOORD3;
#endif
#if HAS_VERTEX_COLOR
	half4 VERTEX_COLOR_ID	: COLOR;
#endif

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

// The input structure of the tessellation pixel shader. 
// Is just a native varyings struct wrapper with additional data
struct TessellatedVaryings
{
	Varyings nativeVaryings;

#if HAS_VERTEX_COLOR
	half4 VERTEX_COLOR_ID	: COLOR;
#endif
};

#endif // UNIVERSAL_LIT_TESSELLATION_STRUCTS_INCLUDED
