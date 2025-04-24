using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameUIManager))]
public class InGameUIManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Generate UIElement Enum"))
        {
            var sb = new StringBuilder();
            sb.AppendLine(@"public enum UIElementEnums");
            sb.AppendLine(@"{");
            var uiManager = (GameUIManager)target;

            UpdateUIElementsList(uiManager);

            //sb.AppendLine($"\tNone,");
            for (int i = 0; i < uiManager.uiElements.Count; i++)
            {
                sb.AppendLine($"\t{uiManager.uiElements[i].name},");
            }
            sb.AppendLine(@"}");

            var path = EditorUtility.SaveFilePanel("Save", "Assets/Scripts/Defines", "UIElementEnums.cs", "cs");
            using (var fs = new FileStream(path, FileMode.Create))
            {
                using (var writer = new StreamWriter(fs))
                {
                    writer.Write(sb.ToString());
                }
            }
            AssetDatabase.Refresh();
        }

        base.OnInspectorGUI();
    }

    private void UpdateUIElementsList(GameUIManager gameUIMamager)
    {
        var uiElementsArray = gameUIMamager.GetComponentsInChildren<UIElement>(true);
        gameUIMamager.uiElements = uiElementsArray.ToList();
    }
}

