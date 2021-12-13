using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEditor.Rendering.Universal.ShaderGUI
{
    public static class LitTessellationGUI
    {
        public enum VertexColorMode
        {
            Multiply = 0,
            Additive = 1,
            Subtractive = 2,
            Replace = 3
        }

        public enum TessellationMode
        {
            Uniform = 0,
            EdgeLength = 1,
            Distance = 2
        }

        public enum TessellationPostPro
        {
            None,
            Phong
        }

        public enum HeightmapBlurMode
        {
            None = 0,
            Box = 1,
            Gaussian = 2
        }

        public enum UVChannel
        {
            uv0,
            uv1,
            uv2,
            uv3
        }

        public static class Styles
        {
            public static GUIContent vertexColorText = new GUIContent("Vertex Color",
                "Enables vertex color.");

            public static GUIContent vertexColorModeText = new GUIContent("Mode",
                "Sets the way vertex color is blended into the base color.");

            public static GUIContent vertexColorBlendingText = new GUIContent("Blending",
                "Vertex color weight in blending calculation.");

            public static GUIContent geometryLabel = new GUIContent("Geometry Inputs",
                "These settings setup the geometry options of the shader.");

            public static GUIContent tessellationMapText = new GUIContent("Tessellation Map",
                "Tessellation map used to mask the tessellation itself. If no map is assigned the tessellation will be applied without restrictions.");

            public static GUIContent tessellationModeText = new GUIContent("Mode",
                "Tessellation Mode.");

            public static GUIContent tessellationFactorText = new GUIContent("Factor",
                "Controls the degree of subdivision done by the GPU.");

            public static GUIContent tessellationFactorMinText = new GUIContent("Factor Min Dist",
                "Controls the degree of subdivision done by the GPU at the distance set as Min.");

            public static GUIContent tessellationFactorMaxText = new GUIContent("Factor Max Dist",
                "Controls the degree of subdivision done by the GPU at the distance set as Max");

            public static GUIContent tessellationDistanceMinText = new GUIContent("Distance Min",
                "Minimum distance in meters to the camera where maximum tessellation should occur.");

            public static GUIContent tessellationDistanceMaxText = new GUIContent("Distance Max",
                "Maximum distance in meters to the camera where minimum tessellation should occur.");

            public static GUIContent tessellationEdgeLengthText = new GUIContent("Edge Length",
                "Desired final tessellated edge length in pixels.");

            public static GUIContent tessellationEdgeDistanceOffsetText = new GUIContent("Distance Offset",
                "Position camera offset in meters from which the edge length calculations are done. Useful for decreasing near topology changes when moving camera.");

            public static GUIContent tessellationPostProText = new GUIContent("Post Processing",
                "Applies a mesh post-tessellation processing technique.");

            public static GUIContent tessellationPhongShapeText = new GUIContent("Shape",
                "Phong tessellation shape factor.");

            public static GUIContent tessellationTriangleClippingText = new GUIContent("Triangle Clipping",
                "Avoid rendering and tessellation processing to out of screen geometry.");

            public static GUIContent tessellationTriangleClipBiasText = new GUIContent("Bias",
                "Fine-tune the primitive clipping. Decreasing this value will cause to increase the range of clipping.");

            public static GUIContent heightText = new GUIContent("Height Map",
                "Sets a height map to implement displacement.");

            public static GUIContent heightBaseText = new GUIContent("Base",
                "Sets a height map base (bias).");

            public static GUIContent heightBlurModeText = new GUIContent("Blur",
                "Sets a height map blur mode.");

            public static GUIContent heightBlurSizeText = new GUIContent("Size",
                "Sets a height map blur size.");

            public static GUIContent heightBlurSamplesText = new GUIContent("Samples",
                "Sets a height map blur samples.");

            public static GUIContent heightBlurGaussStandardDeviationText = new GUIContent("Standard Deviation",
                "Sets a height map gaussian blur standard deviation.");

            public static GUIContent geometryAdvancedLabel = new GUIContent("Geometry Advanced",
                "These settings setups the geometry advanced options.");

            public static GUIContent geometryCustomSTText = new GUIContent("Custom Tiling and Offset",
                "Allows the geometry maps to use customs Tiling and Offsets.");

            public static GUIContent geometryCustomUVChannelText = new GUIContent("Custom UV Channel",
                "Set the UV channel used for tessellation and displacement samplings.");

            public static GUIContent geometryUVChannelText = new GUIContent("Channel",
                "Set the UV channel used for tessellation and displacement samplings.");
        }

        public struct LitTessellationProperties
        {
            public MaterialProperty vertexColor;
            public MaterialProperty vertexColorMode;
            public MaterialProperty vertexColorBlending;
            public MaterialProperty tessellationMap;
            public MaterialProperty tessellationScale;
            public MaterialProperty tessellationMode;
            public MaterialProperty tessellationFactor;
            public MaterialProperty tessellationFactorMin;
            public MaterialProperty tessellationFactorMax;
            public MaterialProperty tessellationDistanceMin;
            public MaterialProperty tessellationDistanceMax;
            public MaterialProperty tessellationEdgeLength;
            public MaterialProperty tessellationEdgeDistanceOffset;
            public MaterialProperty tessellationPostPro;
            public MaterialProperty tessellationPhongShape;
            public MaterialProperty tessellationTriangleClipping;
            public MaterialProperty tessellationTriangleClipBias;
            public MaterialProperty heightMap;
            public MaterialProperty heightStrength;
            public MaterialProperty heightBase;
            public MaterialProperty heightBlurMode;
            public MaterialProperty heightBlurSize;
            public MaterialProperty heightBlurSamples;
            public MaterialProperty heightBlurGaussStandardDeviation;
            public MaterialProperty geometryCustomST;
            public MaterialProperty geometryCustomUVChannel;
            public MaterialProperty geometryUVChannel;

            public LitTessellationProperties(MaterialProperty[] properties)
            {
                vertexColor = BaseShaderGUI.FindProperty("_VertexColor", properties);
                vertexColorMode = BaseShaderGUI.FindProperty("_VertexColorMode", properties);
                vertexColorBlending = BaseShaderGUI.FindProperty("_VertexColorBlending", properties);
                tessellationMap = BaseShaderGUI.FindProperty("_TessellationMap", properties);
                tessellationScale = BaseShaderGUI.FindProperty("_TessellationScale", properties);
                tessellationMode = BaseShaderGUI.FindProperty("_TessellationMode", properties);
                tessellationFactor = BaseShaderGUI.FindProperty("_TessellationFactor", properties);
                tessellationFactorMin = BaseShaderGUI.FindProperty("_TessellationFactorMin", properties);
                tessellationFactorMax = BaseShaderGUI.FindProperty("_TessellationFactorMax", properties);
                tessellationDistanceMin = BaseShaderGUI.FindProperty("_TessellationDistanceMin", properties);
                tessellationDistanceMax = BaseShaderGUI.FindProperty("_TessellationDistanceMax", properties);
                tessellationEdgeLength = BaseShaderGUI.FindProperty("_TessellationEdgeLength", properties);
                tessellationEdgeDistanceOffset = BaseShaderGUI.FindProperty("_TessellationEdgeDistanceOffset", properties);
                tessellationPostPro = BaseShaderGUI.FindProperty("_TessellationPostPro", properties);
                tessellationPhongShape = BaseShaderGUI.FindProperty("_TessellationPhongShape", properties);
                tessellationTriangleClipping = BaseShaderGUI.FindProperty("_TessellationTriangleClipping", properties);
                tessellationTriangleClipBias = BaseShaderGUI.FindProperty("_TessellationTriangleClipBias", properties);
                heightMap = BaseShaderGUI.FindProperty("_HeightMap", properties, false);
                heightStrength = BaseShaderGUI.FindProperty("_HeightStrength", properties, false);
                heightBase = BaseShaderGUI.FindProperty("_HeightBase", properties, false);
                heightBlurMode = BaseShaderGUI.FindProperty("_HeightBlurMode", properties, false);
                heightBlurSize = BaseShaderGUI.FindProperty("_HeightBlurSize", properties, false);
                heightBlurSamples = BaseShaderGUI.FindProperty("_HeightBlurSamples", properties, false);
                heightBlurGaussStandardDeviation = BaseShaderGUI.FindProperty("_HeightBlurGaussStandardDeviation", properties, false);
                geometryCustomST = BaseShaderGUI.FindProperty("_GeometryCustomST", properties, false);
                geometryCustomUVChannel = BaseShaderGUI.FindProperty("_GeometryCustomUVChannel", properties, false);
                geometryUVChannel = BaseShaderGUI.FindProperty("_GeometryUVChannel", properties, false);
            }
        }

        public static void GeometryInputs(LitTessellationProperties properties, MaterialEditor materialEditor, Material material)
        {
            // vertex color
            DoVertexColor(properties, materialEditor, material);

            // tesellation
            DoTessellation(properties, materialEditor, material);

            // height
            DoHeight(properties, materialEditor, material);
        }

        public static void DoVertexColor(LitTessellationProperties properties, MaterialEditor materialEditor, Material material)
        {
            // Vertex color
            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = properties.vertexColor.hasMixedValue;
            var vertexColor = properties.vertexColor.floatValue == 1f;
            vertexColor = EditorGUILayout.Toggle(Styles.vertexColorText, vertexColor);
            if (EditorGUI.EndChangeCheck())
            {
                properties.vertexColor.floatValue = vertexColor ? 1f : 0f;
            }
            EditorGUI.showMixedValue = false;

            if (vertexColor)
            {
                EditorGUI.indentLevel++;
                EditorGUI.indentLevel++;

                // Vertex Color Mode
                EditorGUI.BeginChangeCheck();
                EditorGUI.showMixedValue = properties.vertexColorMode.hasMixedValue;
                var vertexColorMode = (VertexColorMode)properties.vertexColorMode.floatValue;
                vertexColorMode = (VertexColorMode)EditorGUILayout.EnumPopup(Styles.vertexColorModeText, vertexColorMode);
                if (EditorGUI.EndChangeCheck())
                {
                    properties.vertexColorMode.floatValue = (float)vertexColorMode;
                }
                EditorGUI.showMixedValue = false;

                // Vertex Color Blending
                EditorGUI.BeginChangeCheck();
                EditorGUI.showMixedValue = properties.vertexColorBlending.hasMixedValue;
                var vertexColorBlending = EditorGUILayout.Slider(Styles.vertexColorBlendingText, properties.vertexColorBlending.floatValue, 0f, 1f);
                if (EditorGUI.EndChangeCheck())
                {
                    properties.vertexColorBlending.floatValue = vertexColorBlending;
                }
                EditorGUI.showMixedValue = false;

                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
            }
        }

        public static void DoTessellation(LitTessellationProperties properties, MaterialEditor materialEditor, Material material)
        {
            // Tessellation Map
            materialEditor.TexturePropertySingleLine(Styles.tessellationMapText, properties.tessellationMap, properties.tessellationScale);

            EditorGUI.indentLevel++;
            EditorGUI.indentLevel++;

            // Tessellation Mode
            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = properties.tessellationMode.hasMixedValue;
            var tessellationMode = (TessellationMode)properties.tessellationMode.floatValue;
            tessellationMode = (TessellationMode)EditorGUILayout.EnumPopup(Styles.tessellationModeText, tessellationMode);
            if (EditorGUI.EndChangeCheck())
            {
                properties.tessellationMode.floatValue = (float)tessellationMode;
            }
            EditorGUI.showMixedValue = false;

            // Tessellation Mode: Uniform
            if (tessellationMode == TessellationMode.Uniform)
            {
                EditorGUI.indentLevel++;

                // Factor
                EditorGUI.BeginChangeCheck();
                EditorGUI.showMixedValue = properties.tessellationFactor.hasMixedValue;
                var tessellationFactor = EditorGUILayout.Slider(Styles.tessellationFactorText, properties.tessellationFactor.floatValue, 1f, 64f);
                if (EditorGUI.EndChangeCheck())
                {
                    properties.tessellationFactor.floatValue = tessellationFactor;
                }
                EditorGUI.showMixedValue = false;

                EditorGUI.indentLevel--;
            }

            // Tessellation Mode: Edge Length
            else if (tessellationMode == TessellationMode.EdgeLength)
            {
                EditorGUI.indentLevel++;

                // Edge Length
                EditorGUI.BeginChangeCheck();
                EditorGUI.showMixedValue = properties.tessellationEdgeLength.hasMixedValue;
                var tessellationEdgeLength = EditorGUILayout.Slider(Styles.tessellationEdgeLengthText, properties.tessellationEdgeLength.floatValue, .05f, 32f);
                if (EditorGUI.EndChangeCheck())
                {
                    properties.tessellationEdgeLength.floatValue = tessellationEdgeLength;
                }
                EditorGUI.showMixedValue = false;

                // Distance Offset
                EditorGUI.BeginChangeCheck();
                EditorGUI.showMixedValue = properties.tessellationEdgeDistanceOffset.hasMixedValue;
                var tessellationDistanceOffset = EditorGUILayout.Slider(Styles.tessellationEdgeDistanceOffsetText, properties.tessellationEdgeDistanceOffset.floatValue, 0f, 10f);
                if (EditorGUI.EndChangeCheck())
                {
                    properties.tessellationEdgeDistanceOffset.floatValue = tessellationDistanceOffset;
                }
                EditorGUI.showMixedValue = false;

                EditorGUI.indentLevel--;
            }

            // Tessellation Mode: Distance
            else if (tessellationMode == TessellationMode.Distance)
            {
                EditorGUI.indentLevel++;

                // Factor Min
                EditorGUI.BeginChangeCheck();
                EditorGUI.showMixedValue = properties.tessellationFactorMin.hasMixedValue;
                var tessellationFactorMin = EditorGUILayout.Slider(Styles.tessellationFactorMinText, properties.tessellationFactorMin.floatValue, 1f, 64f);
                if (EditorGUI.EndChangeCheck())
                {
                    properties.tessellationFactorMin.floatValue = tessellationFactorMin;
                }
                EditorGUI.showMixedValue = false;

                // Factor Max
                EditorGUI.BeginChangeCheck();
                EditorGUI.showMixedValue = properties.tessellationFactorMax.hasMixedValue;
                var tessellationFactorMax = EditorGUILayout.Slider(Styles.tessellationFactorMaxText, properties.tessellationFactorMax.floatValue, 1f, 64f);
                if (EditorGUI.EndChangeCheck())
                {
                    properties.tessellationFactorMax.floatValue = tessellationFactorMax;
                }
                EditorGUI.showMixedValue = false;

                // Distance Min
                EditorGUI.BeginChangeCheck();
                EditorGUI.showMixedValue = properties.tessellationDistanceMin.hasMixedValue;
                var tessellationDistanceMin = EditorGUILayout.FloatField(Styles.tessellationDistanceMinText, properties.tessellationDistanceMin.floatValue);
                tessellationDistanceMin = Mathf.Max(0, tessellationDistanceMin);
                if (EditorGUI.EndChangeCheck())
                {
                    properties.tessellationDistanceMin.floatValue = tessellationDistanceMin;
                }
                EditorGUI.showMixedValue = false;

                // Distance Max
                EditorGUI.BeginChangeCheck();
                EditorGUI.showMixedValue = properties.tessellationDistanceMax.hasMixedValue;
                var tessellationDistanceMax = EditorGUILayout.FloatField(Styles.tessellationDistanceMaxText, properties.tessellationDistanceMax.floatValue);
                tessellationDistanceMax = Mathf.Max(tessellationDistanceMin, tessellationDistanceMax);
                if (EditorGUI.EndChangeCheck())
                {
                    properties.tessellationDistanceMax.floatValue = tessellationDistanceMax;
                }
                EditorGUI.showMixedValue = false;

                EditorGUI.indentLevel--;
            }

            // Tessellation Post Pro
            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = properties.tessellationPostPro.hasMixedValue;
            var tessellationPostPro = (TessellationPostPro)properties.tessellationPostPro.floatValue;
            tessellationPostPro = (TessellationPostPro)EditorGUILayout.EnumPopup(Styles.tessellationPostProText, tessellationPostPro);
            if (EditorGUI.EndChangeCheck())
            {
                properties.tessellationPostPro.floatValue = (float)tessellationPostPro;
            }
            EditorGUI.showMixedValue = false;

            // Tessellation Post Pro: Phong
            if (tessellationPostPro == TessellationPostPro.Phong)
            {
                EditorGUI.indentLevel++;

                // Factor
                EditorGUI.BeginChangeCheck();
                EditorGUI.showMixedValue = properties.tessellationPhongShape.hasMixedValue;
                var tessellationPhongShape = EditorGUILayout.Slider(Styles.tessellationPhongShapeText, properties.tessellationPhongShape.floatValue, 0f, 1f);
                if (EditorGUI.EndChangeCheck())
                {
                    properties.tessellationPhongShape.floatValue = tessellationPhongShape;
                }
                EditorGUI.showMixedValue = false;

                EditorGUI.indentLevel--;
            }

            // Triangle Clipping
            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = properties.tessellationTriangleClipping.hasMixedValue;
            var tessellationTriangleClipping = properties.tessellationTriangleClipping.floatValue == 1f;
            tessellationTriangleClipping = EditorGUILayout.Toggle(Styles.tessellationTriangleClippingText, tessellationTriangleClipping);
            if (EditorGUI.EndChangeCheck())
            {
                properties.tessellationTriangleClipping.floatValue = tessellationTriangleClipping ? 1f : 0f;
            }
            EditorGUI.showMixedValue = false;

            // Triangle Clipping Bias
            if (tessellationTriangleClipping)
            {
                EditorGUI.indentLevel++;

                EditorGUI.BeginChangeCheck();
                EditorGUI.showMixedValue = properties.tessellationTriangleClipBias.hasMixedValue;
                var tessellationTriangleClipBias = EditorGUILayout.Slider(Styles.tessellationTriangleClipBiasText, properties.tessellationTriangleClipBias.floatValue, -1f, 1f);
                if (EditorGUI.EndChangeCheck())
                {
                    properties.tessellationTriangleClipBias.floatValue = tessellationTriangleClipBias;
                }
                EditorGUI.showMixedValue = false;

                EditorGUI.indentLevel--;
            }

            EditorGUI.indentLevel--;
            EditorGUI.indentLevel--;
        }

        public static void DoHeight(LitTessellationProperties properties, MaterialEditor materialEditor, Material material)
        {
            if (properties.heightMap != null)
            {
                // Heightmap and Height Strength
                materialEditor.TexturePropertySingleLine(Styles.heightText, properties.heightMap, properties.heightMap.textureValue != null ? properties.heightStrength : null);

                EditorGUI.indentLevel++;
                EditorGUI.indentLevel++;

                // Height Base
                EditorGUI.BeginChangeCheck();
                EditorGUI.showMixedValue = properties.heightBase.hasMixedValue;
                var heightBase = EditorGUILayout.Slider(Styles.heightBaseText, properties.heightBase.floatValue, -1f, 1f);
                if (EditorGUI.EndChangeCheck())
                {
                    properties.heightBase.floatValue = heightBase;
                }
                EditorGUI.showMixedValue = false;

                // Height blur mode
                EditorGUI.BeginChangeCheck();
                EditorGUI.showMixedValue = properties.heightBlurMode.hasMixedValue;
                var heightBlurMode = (HeightmapBlurMode)properties.heightBlurMode.floatValue;
                heightBlurMode = (HeightmapBlurMode)EditorGUILayout.EnumPopup(Styles.heightBlurModeText, heightBlurMode);
                if (EditorGUI.EndChangeCheck())
                {
                    properties.heightBlurMode.floatValue = (float)heightBlurMode;
                }
                EditorGUI.showMixedValue = false;

                // Height blur modes
                if (heightBlurMode != HeightmapBlurMode.None)
                {
                    EditorGUI.indentLevel++;

                    EditorGUI.BeginChangeCheck();
                    EditorGUI.showMixedValue = properties.heightBlurSize.hasMixedValue;
                    var heightBlurSize = EditorGUILayout.Slider(Styles.heightBlurSizeText, properties.heightBlurSize.floatValue, 0f, 1.0f);
                    if (EditorGUI.EndChangeCheck())
                        properties.heightBlurSize.floatValue = heightBlurSize;
                    EditorGUI.showMixedValue = false;

                    EditorGUI.BeginChangeCheck();
                    EditorGUI.showMixedValue = properties.heightBlurSamples.hasMixedValue;
                    var heightBlurSamples = EditorGUILayout.IntSlider(Styles.heightBlurSamplesText, (int)properties.heightBlurSamples.floatValue, 1, 60);
                    if (EditorGUI.EndChangeCheck())
                        properties.heightBlurSamples.floatValue = heightBlurSamples;
                    EditorGUI.showMixedValue = false;

                    // Gaussian blur
                    if (heightBlurMode == HeightmapBlurMode.Gaussian)
                    {
                        EditorGUI.BeginChangeCheck();
                        EditorGUI.showMixedValue = properties.heightBlurGaussStandardDeviation.hasMixedValue;
                        var heightBlurGaussStandardDeviation = EditorGUILayout.Slider(Styles.heightBlurGaussStandardDeviationText, properties.heightBlurGaussStandardDeviation.floatValue, 0.001f, 0.1f);
                        if (EditorGUI.EndChangeCheck())
                            properties.heightBlurGaussStandardDeviation.floatValue = heightBlurGaussStandardDeviation;
                        EditorGUI.showMixedValue = false;
                    }

                    EditorGUILayout.HelpBox("Heightmap blur may cause performance issues on heavy tessellated meshes. Avoid using it if possible.", MessageType.Warning);

                    EditorGUI.indentLevel--;
                }

                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
            }
        }

        public static void GeometryAdvanced(LitTessellationProperties properties, MaterialEditor materialEditor, Material material)
        {
            // Geometry Custom ST
            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = properties.geometryCustomST.hasMixedValue;
            var geometryCustomST = EditorGUILayout.Toggle(Styles.geometryCustomSTText, properties.geometryCustomST.floatValue == 1f) ? 1f : 0f;
            if (EditorGUI.EndChangeCheck())
            {
                properties.geometryCustomST.floatValue = geometryCustomST;
            }
            EditorGUI.showMixedValue = false;

            // Tile and offset
            if (geometryCustomST == 1f)
            {
                LitTessellationShader.ExposedDrawTileOffset(materialEditor, properties.tessellationMap);
            }

            // Geometry Custom UV Channel
            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = properties.geometryCustomUVChannel.hasMixedValue;
            var geometryCustomUVChannel = EditorGUILayout.Toggle(Styles.geometryCustomUVChannelText, properties.geometryCustomUVChannel.floatValue == 1f) ? 1f : 0f;
            if (EditorGUI.EndChangeCheck())
            {
                properties.geometryCustomUVChannel.floatValue = geometryCustomUVChannel;
            }
            EditorGUI.showMixedValue = false;

            // UV Channel
            if (geometryCustomUVChannel == 1)
            {
                EditorGUI.indentLevel++;
                EditorGUI.indentLevel++;

                EditorGUI.BeginChangeCheck();
                EditorGUI.showMixedValue = properties.geometryUVChannel.hasMixedValue;
                var geometryUVChannel = (UVChannel)properties.geometryUVChannel.floatValue;
                geometryUVChannel = (UVChannel)EditorGUILayout.EnumPopup(Styles.geometryUVChannelText, geometryUVChannel);
                if (EditorGUI.EndChangeCheck())
                {
                    properties.geometryUVChannel.floatValue = (float)geometryUVChannel;
                }
                EditorGUI.showMixedValue = false;

                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
            }
        }

        public static void SetMaterialKeywords(Material material)
        {
            // Base material keywords
            LitGUI.SetMaterialKeywords(material);
            LitDetailGUI.SetMaterialKeywords(material);

            // Vertex Color
            var vertexColor = false;
            if (material.HasProperty("_VertexColor"))
            {
                vertexColor = material.GetFloat("_VertexColor") == 1f;
            }
            CoreUtils.SetKeyword(material, "_VERTEX_COLOR", vertexColor);

            //Vertex Color Mode
            var vertexColorMode = VertexColorMode.Multiply;
            if (material.HasProperty("_VertexColorMode"))
            {
                vertexColorMode = (VertexColorMode)material.GetFloat("_VertexColorMode");
            }
            CoreUtils.SetKeyword(material, "_VERTEX_COLOR_MULTIPLY", vertexColorMode == VertexColorMode.Multiply);
            CoreUtils.SetKeyword(material, "_VERTEX_COLOR_ADDITIVE", vertexColorMode == VertexColorMode.Additive);
            CoreUtils.SetKeyword(material, "_VERTEX_COLOR_SUBTRACTIVE", vertexColorMode == VertexColorMode.Subtractive);
            CoreUtils.SetKeyword(material, "_VERTEX_COLOR_REPLACE", vertexColorMode == VertexColorMode.Replace);

            // Tessellation Map
            var tessellationMap = false;
            if (material.HasProperty("_TessellationMap"))
            {
                tessellationMap = material.GetTexture("_TessellationMap") != null;
            }
            CoreUtils.SetKeyword(material, "_TESSELLATIONMAP", tessellationMap);

            // Tessellation Mode
            var tessellationMode = TessellationMode.Uniform;
            if (material.HasProperty("_TessellationMode"))
            {
                tessellationMode = (TessellationMode)material.GetFloat("_TessellationMode");
            }
            CoreUtils.SetKeyword(material, "_TESSELLATION_EDGE", tessellationMode == TessellationMode.EdgeLength);
            CoreUtils.SetKeyword(material, "_TESSELLATION_DISTANCE", tessellationMode == TessellationMode.Distance);

            // Tessellation Post Pro
            var tessellationPostPro = TessellationPostPro.None;
            if (material.HasProperty("_TessellationPostPro"))
            {
                tessellationPostPro = (TessellationPostPro)material.GetFloat("_TessellationPostPro");
            }
            CoreUtils.SetKeyword(material, "_TESSELLATION_PHONG", tessellationPostPro == TessellationPostPro.Phong);

            // Tessellation Clipping
            var tessellationTriangleClipping = false;
            if (material.HasProperty("_TessellationTriangleClipping"))
            {
                tessellationTriangleClipping = material.GetFloat("_TessellationTriangleClipping") == 1f;
            }
            CoreUtils.SetKeyword(material, "_TESSELLATION_CLIPPING", tessellationTriangleClipping);

            // Heightmap
            var heightmap = false;
            if (material.HasProperty("_HeightMap"))
            {
                heightmap = material.GetTexture("_HeightMap") != null;
            }
            CoreUtils.SetKeyword(material, "_HEIGHTMAP", heightmap);

            // Height Blur
            var heightBlurMode = HeightmapBlurMode.None;
            if (material.HasProperty("_HeightBlurMode"))
            {
                heightBlurMode = (HeightmapBlurMode)material.GetFloat("_HeightBlurMode");
            }
            CoreUtils.SetKeyword(material, "_HEIGHTMAP_BLUR_BOX", heightBlurMode == HeightmapBlurMode.Box);
            CoreUtils.SetKeyword(material, "_HEIGHTMAP_BLUR_GAUSS", heightBlurMode == HeightmapBlurMode.Gaussian);

            // Geometry Custom ST
            if (material.HasProperty("_GeometryCustomST"))
            {
                CoreUtils.SetKeyword(material, "_GEOMETRY_CUSTOM_ST", material.GetFloat("_GeometryCustomST") == 1.0f);
            }

            // Geometry Custom UV Channel
            var customUVChannel = false;
            if (material.HasProperty("_GeometryCustomUVChannel"))
            {
                customUVChannel = material.GetFloat("_GeometryCustomUVChannel") == 1.0f;
            }
            CoreUtils.SetKeyword(material, "_GEOMETRY_CUSTOM_UV_CHANNEL", customUVChannel);

            // Geometry UV Channel
            var geometryUVChannel = UVChannel.uv0;
            if (material.HasProperty("_GeometryUVChannel"))
            {
                geometryUVChannel = (UVChannel)material.GetFloat("_GeometryUVChannel");
            }
            CoreUtils.SetKeyword(material, "_GEOMETRY_UV_CHANNEL_0", geometryUVChannel == UVChannel.uv0);
            CoreUtils.SetKeyword(material, "_GEOMETRY_UV_CHANNEL_1", geometryUVChannel == UVChannel.uv1);
            CoreUtils.SetKeyword(material, "_GEOMETRY_UV_CHANNEL_2", geometryUVChannel == UVChannel.uv2);
            CoreUtils.SetKeyword(material, "_GEOMETRY_UV_CHANNEL_3", geometryUVChannel == UVChannel.uv3);
        }
    }
}
