using System;
using UnityEngine;

namespace UnityEditor.Rendering.Universal.ShaderGUI
{
    internal class LitTessellationShader : BaseShaderGUI
    {
        // Constants
        private const string k_KeyPrefix = "URP:Material:UI_State:";

        // Header foldout states
        private SavedBool m_DetailInputsFoldout;
        private SavedBool m_GeometryFoldout;
        private SavedBool m_GeometryAdvancedFoldout;

        // Properties
        private LitGUI.LitProperties litProperties;
        private LitDetailGUI.LitProperties litDetailProperties;
        private LitTessellationGUI.LitTessellationProperties litTessellationProperties;

        // On open event
        public override void OnOpenGUI(Material material, MaterialEditor materialEditor)
        {
            base.OnOpenGUI(material, materialEditor);

            // Create key string for editor prefs
            var m_HeaderStateKey = k_KeyPrefix + material.shader.name;

            // Foldouts
            m_DetailInputsFoldout = new SavedBool($"{headerStateKey}.DetailInputsFoldout", true);
            m_GeometryFoldout = new SavedBool($"{m_HeaderStateKey}.GeometryOptionsFoldout", true);
            m_GeometryAdvancedFoldout = new SavedBool($"{m_HeaderStateKey}.GeometryAdvancedOptionsFoldout", true);
        }

        // Collect properties from the material properties
        public override void FindProperties(MaterialProperty[] properties)
        {
            base.FindProperties(properties);

            // Properties structs
            litProperties = new LitGUI.LitProperties(properties);
            litDetailProperties = new LitDetailGUI.LitProperties(properties);
            litTessellationProperties = new LitTessellationGUI.LitTessellationProperties(properties);
        }

        // Material changed check
        public override void MaterialChanged(Material material)
        {
            // Check for material
            if (material == null)
            {
                throw new ArgumentNullException("material");
            }

            // Set keywords
            SetMaterialKeywords(material, LitTessellationGUI.SetMaterialKeywords);
        }

        // Material main surface options
        public override void DrawSurfaceOptions(Material material)
        {
            if (material == null)
                throw new ArgumentNullException("material");

            // Use default labelWidth
            EditorGUIUtility.labelWidth = 0f;

            // Detect any changes to the material
            EditorGUI.BeginChangeCheck();
            if (litProperties.workflowMode != null)
            {
                DoPopup(LitGUI.Styles.workflowModeText, litProperties.workflowMode, Enum.GetNames(typeof(LitGUI.WorkflowMode)));
            }
            if (EditorGUI.EndChangeCheck())
            {
                foreach (var obj in blendModeProp.targets)
                    MaterialChanged((Material)obj);
            }
            base.DrawSurfaceOptions(material);
        }

        // Material main surface inputs
        public override void DrawSurfaceInputs(Material material)
        {
            base.DrawSurfaceInputs(material);

            LitGUI.Inputs(litProperties, materialEditor, material);
            DrawEmissionProperties(material, true);
            DrawTileOffset(materialEditor, baseMapProp);
        }

        // Material main advanced options
        public override void DrawAdvancedOptions(Material material)
        {
            if (litProperties.reflections != null && litProperties.highlights != null)
            {
                EditorGUI.BeginChangeCheck();
                materialEditor.ShaderProperty(litProperties.highlights, LitGUI.Styles.highlightsText);
                materialEditor.ShaderProperty(litProperties.reflections, LitGUI.Styles.reflectionsText);
                if(EditorGUI.EndChangeCheck())
                {
                    MaterialChanged(material);
                }
            }

            base.DrawAdvancedOptions(material);
        }

        // Material additional foldouts
        public override void DrawAdditionalFoldouts(Material material)
        {
            m_DetailInputsFoldout.value = EditorGUILayout.BeginFoldoutHeaderGroup(m_DetailInputsFoldout.value, LitDetailGUI.Styles.detailInputs);
            if (m_DetailInputsFoldout.value)
            {
                LitDetailGUI.DoDetailArea(litDetailProperties, materialEditor);
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            m_GeometryFoldout.value = EditorGUILayout.BeginFoldoutHeaderGroup(m_GeometryFoldout.value, LitTessellationGUI.Styles.geometryLabel);
            if (m_GeometryFoldout.value)
            {
                DrawGeometryInputsFoldout(material);
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            m_GeometryAdvancedFoldout.value = EditorGUILayout.BeginFoldoutHeaderGroup(m_GeometryAdvancedFoldout.value, LitTessellationGUI.Styles.geometryAdvancedLabel);
            if (m_GeometryAdvancedFoldout.value)
            {
                DrawGeometryAdvancedFoldout(material);
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        // Material geometry inputs foldout
        public virtual void DrawGeometryInputsFoldout(Material material)
        {
            LitTessellationGUI.GeometryInputs(litTessellationProperties, materialEditor, material);
        }

        // Material geometry advanced foldout
        public virtual void DrawGeometryAdvancedFoldout(Material material)
        {
            LitTessellationGUI.GeometryAdvanced(litTessellationProperties, materialEditor, material);
        }

        // New material assignment
        public override void AssignNewShaderToMaterial(Material material, Shader oldShader, Shader newShader)
        {
            if (material == null)
                throw new ArgumentNullException("material");

            // _Emission property is lost after assigning Standard shader to the material
            // thus transfer it before assigning the new shader
            if (material.HasProperty("_Emission"))
            {
                material.SetColor("_EmissionColor", material.GetColor("_Emission"));
            }

            base.AssignNewShaderToMaterial(material, oldShader, newShader);

            if (oldShader == null || !oldShader.name.Contains("Legacy Shaders/"))
            {
                SetupMaterialBlendMode(material);
                return;
            }

            SurfaceType surfaceType = SurfaceType.Opaque;
            BlendMode blendMode = BlendMode.Alpha;
            if (oldShader.name.Contains("/Transparent/Cutout/"))
            {
                surfaceType = SurfaceType.Opaque;
                material.SetFloat("_AlphaClip", 1);
            }
            else if (oldShader.name.Contains("/Transparent/"))
            {
                // NOTE: legacy shaders did not provide physically based transparency
                // therefore Fade mode
                surfaceType = SurfaceType.Transparent;
                blendMode = BlendMode.Alpha;
            }
            material.SetFloat("_Surface", (float)surfaceType);
            material.SetFloat("_Blend", (float)blendMode);

            if (oldShader.name.Equals("Standard (Specular setup)"))
            {
                material.SetFloat("_WorkflowMode", (float)LitGUI.WorkflowMode.Specular);
                Texture texture = material.GetTexture("_SpecGlossMap");
                if (texture != null)
                    material.SetTexture("_MetallicSpecGlossMap", texture);
            }
            else
            {
                material.SetFloat("_WorkflowMode", (float)LitGUI.WorkflowMode.Metallic);
                Texture texture = material.GetTexture("_MetallicGlossMap");
                if (texture != null)
                    material.SetTexture("_MetallicSpecGlossMap", texture);
            }

            MaterialChanged(material);
        }

        // Expose tile and offset drawing method
        public static void ExposedDrawTileOffset(MaterialEditor materialEditor, MaterialProperty textureProp) =>
            DrawTileOffset(materialEditor, textureProp);
    }
}
