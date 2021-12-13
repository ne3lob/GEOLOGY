#ifndef TESSELLATION_INCLUDED
#define TESSELLATION_INCLUDED

#if defined(SHADER_API_GLCORE) || defined(SHADER_API_GLES3) || defined(SHADER_API_METAL) || defined(SHADER_API_VULKAN) || defined(SHADER_API_GLES)
#define UNITY_COMPILER_HLSL
#define UNITY_COMPILER_HLSLCC
#elif defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE)
#define UNITY_COMPILER_HLSL
#else
#define UNITY_COMPILER_CG
#endif

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Blur.hlsl"

// Define a macro, used to set the maximum tessellation factor
#define MAX_TESSELLATION_FACTORS 64.0 

// The structure of the surface subdivision factor, the general structure, and the UnityTessellationFactor structure defined in the unity buildIn shader
struct TessellationFactors
{
	float edge[3]	: SV_TessFactor;		//The subdivision factor of the three sides of the triangle
	float inside	: SV_InsideTessFactor; // The subdivision factor inside the triangle
};

// Geometry aux method
float3 ProjectPointOnPlane(float3 position, float3 planePosition, float3 planeNormal)
{
	return position - (dot(position - planePosition, planeNormal) * planeNormal);
}

// Clipping function
bool TriangleIsBelowClipPlane(float3 p0, float3 p1, float3 p2, int planeIndex, float bias) 
{
	float4 plane = unity_CameraWorldClipPlanes[planeIndex];
	return
		dot(float4(p0, 1), plane) < bias &&
		dot(float4(p1, 1), plane) < bias &&
		dot(float4(p2, 1), plane) < bias;
}

// Triangle clip checker function
bool TriangleIsCulled(float3 p0, float3 p1, float3 p2, float bias) 
{
	return
		TriangleIsBelowClipPlane(p0, p1, p2, 0, bias) ||
		TriangleIsBelowClipPlane(p0, p1, p2, 1, bias) ||
		TriangleIsBelowClipPlane(p0, p1, p2, 2, bias) ||
		TriangleIsBelowClipPlane(p0, p1, p2, 3, bias);
}

// Calculate tessellation factor
float TessellationFactor(float3 p0, float3 p1, float2 uv0, float2 uv1)
{
#if _TESSELLATIONMAP
	float2 interpolatedUV = TRANSFORM_TEX(((uv0 + uv1) * 0.5).xy, TESSELLATION_TRANSFORM_SOURCE);
#else
	float2 interpolatedUV = half2(0.0, 0.0);
#endif
	float tessellation = SampleTessellation(interpolatedUV);

#if _TESSELLATION_EDGE
	float edgeLength = distance(p0, p1);
	float3 edgeCenter = (p0 + p1) * 0.5;
	float viewDistance = max(0.0, distance(edgeCenter, _WorldSpaceCameraPos) - _TessellationEdgeDistanceOffset);
	float factor = edgeLength * _ScreenParams.y / (_TessellationEdgeLength * viewDistance);
#elif _TESSELLATION_DISTANCE
	float3 edgeCenter = (p0 + p1) * 0.5;
	float viewDistance = max(0.0, distance(edgeCenter, _WorldSpaceCameraPos));
	float rawFactor = clamp(1.0 - (viewDistance - _TessellationDistanceMin) / (_TessellationDistanceMax - _TessellationDistanceMin), 0.01, 1.0);
	float factor = (_TessellationFactorMin - _TessellationFactorMax) * rawFactor + _TessellationFactorMax;
#else	// UNIFORM
	float factor = _TessellationFactor;
#endif
	return min(factor * tessellation, MAX_TESSELLATION_FACTORS);
}

// Phong tessellaton
float3 PhongTessellation(float3 positionWS, float3 p0, float3 p1, float3 p2, float3 n0, float3 n1, float3 n2, float3 baryCoords, float shape)
{
	float3 c0 = ProjectPointOnPlane(positionWS, p0, n0);
	float3 c1 = ProjectPointOnPlane(positionWS, p1, n1);
	float3 c2 = ProjectPointOnPlane(positionWS, p2, n2);

	float3 phongPositionWS = baryCoords.x * c0 + baryCoords.y * c1 + baryCoords.z * c2;

	return lerp(positionWS, phongPositionWS, shape);
}

// Bypass unity shader compiler bug but not compatible with GPU Instancing
float3 SafeTransformObjectToWorld(float3 positionOS)
{
	return mul(unity_ObjectToWorld, float4(positionOS, 1.0)).xyz;
}

// Patch function
TessellationFactors HullConstant(InputPatch<TessellationControlPoint, 3> input)
{
	UNITY_SETUP_INSTANCE_ID(input[0]);

#if defined(UNITY_COMPILER_HLSLCC)	// Temporal workaround until Unity fixes its shader compiler (it mess up GPU Instancing)
	float3 p0 = SafeTransformObjectToWorld(input[0].POSITION_ID.xyz);
	float3 p1 = SafeTransformObjectToWorld(input[1].POSITION_ID.xyz);
	float3 p2 = SafeTransformObjectToWorld(input[2].POSITION_ID.xyz);
#else
	float3 p0 = TransformObjectToWorld(input[0].POSITION_ID.xyz);
	float3 p1 = TransformObjectToWorld(input[1].POSITION_ID.xyz);
	float3 p2 = TransformObjectToWorld(input[2].POSITION_ID.xyz);
#endif

	TessellationFactors output;

#if _TESSELLATION_CLIPPING
	float bias = -_TessellationTriangleClipBias * _HeightStrength * 2.0;
	if (TriangleIsCulled(p0, p1, p2, bias)) 
	{
		output.edge[0] = output.edge[1] = output.edge[2] = output.inside = 0;
	}
	else
	{
#endif
		output.edge[0] = TessellationFactor(p1, p2, input[1].GEOMETRY_UV_CHANNEL, input[2].GEOMETRY_UV_CHANNEL);
		output.edge[1] = TessellationFactor(p2, p0, input[2].GEOMETRY_UV_CHANNEL, input[0].GEOMETRY_UV_CHANNEL);
		output.edge[2] = TessellationFactor(p0, p1, input[0].GEOMETRY_UV_CHANNEL, input[1].GEOMETRY_UV_CHANNEL);
		output.inside =
				(output.edge[0] +
				output.edge[1] +
				output.edge[2]) / 3.0;
#if _TESSELLATION_CLIPPING
	}
#endif

	return output;
}

#endif
