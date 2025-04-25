// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
using UnityEngine;
using UnityEditor;


namespace AmazingAssets.CurvedWorld.Editor
{
    [CustomEditor(typeof(CurvedWorld.CurvedWorldCamera))]
    [CanEditMultipleObjects]
    public class CurvedWorldCameraEditor : UnityEditor.Editor
    {
        SerializedProperty matrixType;
        SerializedProperty fieldOfView;
        SerializedProperty size;
        SerializedProperty nearClipPlaneSameAsCamera;
        SerializedProperty nearClipPlane;
        SerializedProperty drawGizmos;


        void OnEnable()
        {
            matrixType = serializedObject.FindProperty("matrixType");
            fieldOfView = serializedObject.FindProperty("fieldOfView");
            size = serializedObject.FindProperty("size");
            nearClipPlaneSameAsCamera = serializedObject.FindProperty("nearClipPlaneSameAsCamera");
            nearClipPlane = serializedObject.FindProperty("nearClipPlane");
            drawGizmos = serializedObject.FindProperty("drawGizmos");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();


            GUILayout.Space(5);
            EditorGUILayout.PropertyField(matrixType);

            if (matrixType.enumValueIndex == (int)CurvedWorld.CurvedWorldCamera.MatrixType.Perspective)
                EditorGUILayout.PropertyField(fieldOfView);
            else
                EditorGUILayout.PropertyField(size);

            if (matrixType.enumValueIndex == (int)CurvedWorld.CurvedWorldCamera.MatrixType.Orthographic)
            {
                nearClipPlaneSameAsCamera.boolValue = EditorGUILayout.IntPopup("Near Clip Plane", nearClipPlaneSameAsCamera.boolValue ? 1 : 0, new string[] { "Custom", "Same As Camera" }, new int[] { 0, 1 }) == 1 ? true : false;
                if (nearClipPlaneSameAsCamera.boolValue == false)
                {
                    using (new EditorGUIHelper.EditorGUIIndentLevel(1))
                    {
                        EditorGUILayout.PropertyField(nearClipPlane, new GUIContent("Value"));
                    }
                }
            }


            EditorGUILayout.PropertyField(drawGizmos);


            serializedObject.ApplyModifiedProperties();
        }
    }
}
