// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
using System.IO;
using System.Linq;

using UnityEngine;
using UnityEditor;


namespace AmazingAssets.CurvedWorld.Editor
{
    [CanEditMultipleObjects]
    public class CurvedWorldBendSettingsDrawer : MaterialPropertyDrawer
    {
        static CurvedWorld.BendType[] shaderSupportedBendTypes;
        static int[] shaderSupportedBendIDs;
        static bool hasNormalTransform;


        CurvedWorld.BendType updateBendType;
        int updateBendID;
        bool updateNormalTransform;
        Material updateMaterial;
        MaterialEditor updateMaterialEditor;


        public void Init(string label)
        {
            EditorUtilities.StringToBendSettings(label, out shaderSupportedBendTypes, out shaderSupportedBendIDs, out hasNormalTransform);
        }
        public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor)
        {
            Material material = (Material)editor.target;
            Init(prop.displayName);


            //Read settings
            CurvedWorld.BendType bendType;
            int bendID;
            bool normalTransform;

            EditorUtilities.GetBendSettingsFromVector(prop.vectorValue, out bendType, out bendID, out normalTransform);

            //Make sure keywords are assinged correctly
            if ((material.IsKeywordEnabled(EditorUtilities.GetKeywordName(bendType)) == false || material.IsKeywordEnabled(EditorUtilities.GetKeywordName(bendID)) == false) &&
               File.Exists(EditorUtilities.GetBendFileLocation(bendType, bendID, Enum.Extension.cginc)))
            {
                if (shaderSupportedBendTypes.Contains(bendType) == false || shaderSupportedBendIDs.Contains(bendID) == false ||
                File.Exists(EditorUtilities.GetBendFileLocation(bendType, bendID, Enum.Extension.cginc)) == false)
                {
                    //Displaying Actions button below
                }
                else
                {
                    EditorUtilities.UpdateMaterialKeyWords(material, bendType, bendID, normalTransform);

                    if (CurvedWorldEditorWindow.activeWindow != null && CurvedWorldEditorWindow.activeWindow.gTab == Enum.Tab.RenderersOverview)
                    {
                        CurvedWorldEditorWindow.activeWindow.RebuildSceneShadersOverview();
                        CurvedWorldEditorWindow.activeWindow.Repaint();
                    }
                }
            }
            if (normalTransform != material.IsKeywordEnabled(Constants.shaderKeywordName_BendTransformNormal))
            {
                if (material.IsKeywordEnabled(Constants.shaderKeywordName_BendTransformNormal))
                    normalTransform = true;
                else
                    normalTransform = false;

                prop.vectorValue = new Vector4((int)bendType, bendID, (hasNormalTransform ? (normalTransform ? 1 : 0) : 0), 0);

                if (CurvedWorldEditorWindow.activeWindow != null && CurvedWorldEditorWindow.activeWindow.gTab == Enum.Tab.RenderersOverview)
                {
                    CurvedWorldEditorWindow.activeWindow.RebuildSceneShadersOverview();
                    CurvedWorldEditorWindow.activeWindow.Repaint();
                }
            }



            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;

            position.height = 18;
            using (new EditorGUIHelper.EditorGUIUtilityLabelWidth(0))
            {
                position.height = 18;

                bendType = (CurvedWorld.BendType)EditorGUI.Popup(new Rect(position.xMin, position.yMin, position.width - 20, position.height), " ", (int)bendType, Constants.bendTypesNamesForMenu);
                EditorGUI.LabelField(new Rect(position.xMin, position.yMin, position.width - 20, position.height), "Bend Type", EditorUtilities.GetBendTypeNameInfo(bendType).forLable, EditorStyles.popup);


                if (GUI.Button(new Rect(position.xMax - 20, position.yMin, 20, position.height), "≡"))
                {
                    GenericMenu menu = new GenericMenu();

                    menu.AddItem(new GUIContent("Find Controller"), false, EditorUtilities.CallbackFindController, (int)bendType + "_" + bendID);
                    menu.AddItem(new GUIContent("Editor Window"), false, EditorUtilities.CallbackOpenCurvedWorldEditorWindowShaderIntegration, (int)bendType + "_" + bendID);
                    menu.AddItem(new GUIContent("Curved World Keywords"), false, EditorUtilities.CallbackAnalyzeShaderCurvedWorldKeywords, material.shader);

                    menu.AddItem(new GUIContent("Reimport Shader"), false, EditorUtilities.CallbackReimportShader, material.shader);


                    menu.ShowAsContext();
                }


                position.yMin += 20;
                position.height = 18;
                bendID = EditorGUI.IntSlider(position, "Bend ID", bendID, 1, Constants.MAX_SUPPORTED_BEND_IDS);

                if (hasNormalTransform)
                {
                    position.yMin += 20;
                    position.height = 18;
                    normalTransform = EditorGUI.Toggle(position, "Normal Transform", normalTransform);
                }
            }


            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                if (bendID < 1)
                    bendID = 1;

                // Set the new value if it has changed
                prop.vectorValue = new Vector4((int)bendType, bendID, (hasNormalTransform ? (normalTransform ? 1 : 0) : 0), 0);

                if (File.Exists(EditorUtilities.GetBendFileLocation(bendType, bendID, Enum.Extension.cginc)))
                {
                    Undo.RecordObjects(editor.targets, "Change Keywords");

                    foreach (Material mat in editor.targets)
                    {
                        EditorUtilities.UpdateMaterialKeyWords(mat, bendType, bendID, normalTransform);
                    }
                }
                else
                {
                    //If file does not exist still adjust keyword for normal transformation
                    foreach (Material mat in editor.targets)
                    {
                        if (normalTransform)
                            mat.EnableKeyword(Constants.shaderKeywordName_BendTransformNormal);
                        else
                            mat.DisableKeyword(Constants.shaderKeywordName_BendTransformNormal);
                    }
                }

                if (CurvedWorldEditorWindow.activeWindow != null && CurvedWorldEditorWindow.activeWindow.gTab == Enum.Tab.RenderersOverview)
                {
                    CurvedWorldEditorWindow.activeWindow.RebuildSceneShadersOverview();
                    CurvedWorldEditorWindow.activeWindow.Repaint();
                }
            }




            if (shaderSupportedBendTypes.Contains(bendType) == false || shaderSupportedBendIDs.Contains(bendID) == false ||
                File.Exists(EditorUtilities.GetBendFileLocation(bendType, bendID, Enum.Extension.cginc)) == false)
            {
                position.yMin += 20;
                position.height = 36;
                EditorGUI.HelpBox(position, "Missing Keywords!", MessageType.Error);

                if (GUI.Button(new Rect(position.xMax - 64 - 5, position.yMin + 9, 64, 18), "Actions"))
                {
                    updateBendType = bendType;
                    updateBendID = bendID;
                    updateNormalTransform = normalTransform;
                    updateMaterial = material;
                    updateMaterialEditor = editor;

                    GenericMenu menu = new GenericMenu();
                    menu.AddItem(new GUIContent("Update Shader"), false, CallBackUpdateShader);
                    menu.ShowAsContext();
                }
            }
        }
        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            Material material = (Material)editor.target;
            Init(prop.displayName);

            //Read settings
            CurvedWorld.BendType bendType;
            int bendID;
            bool enabledNormalTransform;

            EditorUtilities.GetBendSettingsFromVector(prop.vectorValue, out bendType, out bendID, out enabledNormalTransform);


            float height = base.GetPropertyHeight(prop, label, editor) * (2 + (hasNormalTransform ? 1 : 0)) + 2;


            if (shaderSupportedBendTypes.Contains(bendType) == false || shaderSupportedBendIDs.Contains(bendID) == false ||
                File.Exists(EditorUtilities.GetBendFileLocation(bendType, bendID, Enum.Extension.cginc)) == false)
            {
                height += 40;
            }


            return height;
        }
        void CallBackUpdateShader()
        {
            if (File.Exists(EditorUtilities.GetBendFileLocation(updateBendType, updateBendID, Enum.Extension.cginc)) == false)
            {
                EditorUtilities.CreateCGINCFile(updateBendType, updateBendID);
            }


            if (EditorUtilities.AddShaderBendSettings(updateMaterial.shader, updateBendType, updateBendID, Enum.KeywordsCompile.Default, false))
            {
                EditorUtilities.CallbackReimportShader(updateMaterial.shader);

                foreach (Material mat in updateMaterialEditor.targets)
                {
                    EditorUtilities.UpdateMaterialKeyWords(mat, updateBendType, updateBendID, updateNormalTransform);
                }

                if (CurvedWorldEditorWindow.activeWindow != null &&
                    CurvedWorldEditorWindow.activeWindow.gTab == Enum.Tab.CurvedWorldKeywords &&
                    CurvedWorldEditorWindow.activeWindow.gCurvedWorldKeywordsShader == updateMaterial.shader)
                {
                    CurvedWorldEditorWindow.gCurvedWorldKeywordsShaderInfo = null;
                    CurvedWorldEditorWindow.activeWindow.Repaint();
                }
            }

            AssetDatabase.Refresh();

            if (CurvedWorldEditorWindow.activeWindow != null && CurvedWorldEditorWindow.activeWindow.gTab == Enum.Tab.RenderersOverview)
            {
                CurvedWorldEditorWindow.activeWindow.RebuildSceneShadersOverview();
                CurvedWorldEditorWindow.activeWindow.Repaint();
            }
        }
    }
}
