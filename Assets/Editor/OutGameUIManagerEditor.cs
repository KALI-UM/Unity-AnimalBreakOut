using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OutGameUIManager))]
public class OutGameUIManagerEditor : Editor
{
    private int addExpValue;

    private SwitchableCanvasType switchableCanvasType;
    private bool isVisibleOtherCanvas;
    private bool isVisibleShowCanvasType = true;

    private DefaultCanvasType selectedDefaultCanvasType = DefaultCanvasType.Lobby;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Add Experience Test", EditorStyles.boldLabel);

        addExpValue = EditorGUILayout.IntField("Experience Amount", addExpValue);

        if (GUILayout.Button("Add Exp"))
        {
            if (Application.isPlaying)
            {
                GameDataManager.Instance.PlayerLevelSystem.AddExperienceValue(addExpValue);
                Debug.Log($"Added exp: {addExpValue}");
            }
            else
            {
                Debug.LogWarning("Exp can only be added while the game is running.");
            }
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Editor Canvas Visualization Test", EditorStyles.boldLabel);

        switchableCanvasType = (SwitchableCanvasType)EditorGUILayout.EnumPopup("Show Canvas Type", switchableCanvasType);
        isVisibleOtherCanvas = EditorGUILayout.Toggle("Is Visible Other Canvas", isVisibleOtherCanvas);
        isVisibleShowCanvasType = EditorGUILayout.Toggle("Is Visible Show Canvas Type", isVisibleShowCanvasType);

        if (GUILayout.Button("Invoke Visualize Canvas"))
        {
            InvokeVisualizeCanvasInEditor();
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Alert Panel Test", EditorStyles.boldLabel);

        OutGameUIManager manager = (OutGameUIManager)target;

        GUI.enabled = Application.isPlaying;

        if (GUILayout.Button("Show Alert Single Button Panel"))
        {
            manager.ShowAlertSingleButtonPanel(AlertPanelInfoDataFactory.GetAlertPanelInfoData(AlertPanelInfoDataType.TestCloseAlertPanelSingle));
        }
        if (GUILayout.Button("Show Alert Double Button Panel"))
        {
            manager.ShowAlertDoubleButtonPanel(AlertPanelInfoDataFactory.GetAlertPanelInfoData(AlertPanelInfoDataType.TestCloseAlertPanelDouble));
        }
        if (GUILayout.Button("Hide Alert Panel Spawn Panel Root"))
        {
            manager.HideAlertPanelSpawnPanelRoot();
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("FullScreen Panel Test", EditorStyles.boldLabel);

        if (GUILayout.Button("Show Gacha FullScreen Panel"))
        {
            manager.ShowFullScreenPanel(FullScreenType.GachaScreen);
        }
        if (GUILayout.Button("Hide FullScreen Panel"))
        {
            manager.HideFullScreenPanel();
        }

        GUI.enabled = true;
    }

    private void InvokeVisualizeCanvasInEditor()
    {
        var allSwitchableCanvases = FindObjectsOfType<SwitchableCanvas>(true);

        foreach (var canvas in allSwitchableCanvases)
        {
            bool visualizeThisCanvas = canvas.switchableCanvasType == switchableCanvasType
                ? isVisibleShowCanvasType
                : isVisibleOtherCanvas;

            var canvasGroup = canvas.GetComponent<CanvasGroup>();

            canvasGroup.alpha = visualizeThisCanvas ? 1f : 0f;
            canvasGroup.interactable = visualizeThisCanvas;
            canvasGroup.blocksRaycasts = visualizeThisCanvas;

            EditorUtility.SetDirty(canvasGroup);
            EditorUtility.SetDirty(canvas);
        }
    }
}