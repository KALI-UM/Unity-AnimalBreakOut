using System.Collections.Generic;
using UnityEngine;

public class OutGameManager : MonoBehaviour
{
    #region manager
    
    private List<IManager> managers = new();
    
    private ObjectPoolManager objectPoolManager;
    public ObjectPoolManager ObjectPoolManager => objectPoolManager;
    
    private OutGameUIManager outGameUIManager;
    public OutGameUIManager OutGameUIManager => outGameUIManager;
    
    #endregion

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 9999;
    }
    
    private void Start()
    {
        InitializeManagers();
    }
    
    private void OnDestroy()
    {
        foreach (var manager in managers)
        {
            manager.Clear();
        }
    }
    
#if UNITY_ANDROID
    private void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current != null &&
            UnityEngine.InputSystem.Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log($" CurrCanvas : {outGameUIManager.CurrentSwitchableCanvasType}");
            
            if (outGameUIManager.CurrentSwitchableCanvasType == SwitchableCanvasType.Lobby)
            {
                outGameUIManager.ShowAlertDoubleButtonPanel(AlertPanelInfoDataFactory.GetAlertPanelInfoData(AlertPanelInfoDataType.QuitGame));
            }
            else
            {
                outGameUIManager.SwitchVisualizeSwitchableCanvas(SwitchableCanvasType.Lobby, true);
                outGameUIManager.SwitchActiveSwitchableCanvas(SwitchableCanvasType.Lobby);
                
                MenuPanel.onMenuBottomButtonClicked?.Invoke(SwitchableCanvasType.Lobby);
            }
        }
    }
#endif
    
    private void InitializeManagers()
    {
        objectPoolManager = new ObjectPoolManager();
        managers.Add(ObjectPoolManager);

        GameObject.FindGameObjectWithTag("OutGameUIManager").TryGetComponent(out outGameUIManager);
        outGameUIManager.SetOutGameManager(this);
        managers.Add(outGameUIManager);
        
        foreach (var manager in managers)
        {
            manager.Initialize();
        }
    }
    
    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }
}