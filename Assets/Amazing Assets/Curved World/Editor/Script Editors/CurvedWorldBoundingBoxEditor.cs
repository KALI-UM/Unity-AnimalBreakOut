// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
using UnityEngine;
using UnityEditor;


namespace AmazingAssets.CurvedWorld.Editor
{
    [CustomEditor(typeof(CurvedWorld.CurvedWorldBoundingBox))]
    [CanEditMultipleObjects]
    public class CurvedWorldBoundingBoxEditor : UnityEditor.Editor
    {
        SerializedProperty scale;
        SerializedProperty drawGizmos;


        void OnEnable()
        {
            scale = serializedObject.FindProperty("scale");
            drawGizmos = serializedObject.FindProperty("drawGizmos");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();


            GUILayout.Space(5);
            using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
            {
                EditorGUILayout.PropertyField(scale);

                if (GUILayout.Button("Recalculate", GUILayout.MaxWidth(90)))
                {
                    ((CurvedWorld.CurvedWorldBoundingBox)target).RecalculateBounds();
                }
            }
            EditorGUILayout.PropertyField(drawGizmos);

            if (scale.floatValue < 1)
                scale.floatValue = 1;


            serializedObject.ApplyModifiedProperties();
        }
    }
}
