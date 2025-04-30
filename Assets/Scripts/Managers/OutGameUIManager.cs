using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class OutGameUIManager : MonoBehaviour, IManager
{
    private OutGameManager outGameManager;
    
    public static event Action<bool> onSwitchActiveLayoutGroupControllers;
    public static event Action<bool> onSwitchActiveDefaultCanvases;
    public static event Action<SwitchableCanvasType> onSwitchActiveSwitchableCanvas;
    public static event Action<SwitchableCanvasType, bool, bool> onSwitchVisualizeSwitchableCanvas;
    
    [SerializeField] private GameObject unlockAnimalPanelPrefab;
    private ObjectPool<GameObject> unlockAnimalPanelPool;
    public static event Action<GameObject> onAnimalUnlockPanelInstantiated;
    private readonly List<GameObject> animalUnlockPanelList = new();

    public SwitchableCanvasType CurrentSwitchableCanvasType { get; set; }

    [SerializeField] private GameObject alertPanelSpawnPanelRoot;
    [SerializeField] private GameObject alertPanelSpawnPanel;
    
    [SerializeField] private GameObject alertSingleButtonPanel;
    [SerializeField] private GameObject alertDoubleButtonPanel;
    private ObjectPool<GameObject> alertSingleButtonPanelPool;
    private ObjectPool<GameObject> alertDoubleButtonPanelPool;
    [SerializeField] private GameObject alertPanelReleaseParent;
    
    private readonly List<GameObject> alertSingleButtonPanelList = new();
    private readonly List<GameObject> alertDoubleButtonPanelList = new();

    private void Start()
    {
        StartCoroutine(DisableAfterFrameAllLayoutGroup(SwitchableCanvasType.Lobby));

        CurrentSwitchableCanvasType = SwitchableCanvasType.Lobby;
        
        alertSingleButtonPanelPool = outGameManager.ObjectPoolManager.CreateObjectPool(alertSingleButtonPanel,
            () => Instantiate(alertSingleButtonPanel),
            obj => { obj.SetActive(true); },
            obj => { obj.SetActive(false); });
        
        alertDoubleButtonPanelPool = outGameManager.ObjectPoolManager.CreateObjectPool(alertDoubleButtonPanel,
            () => Instantiate(alertDoubleButtonPanel),
            obj => { obj.SetActive(true); },
            obj => { obj.SetActive(false); });
        
        // TempCode //
        
        unlockAnimalPanelPool = outGameManager.ObjectPoolManager.CreateObjectPool(unlockAnimalPanelPrefab,
            () => Instantiate(unlockAnimalPanelPrefab),
            obj => { obj.SetActive(true); },
            obj => { obj.SetActive(false); });

        var animalIdList = DataTableManager.animalDataTable.GetAnimalIDs();
        for (int i = 0; i < animalIdList.Count; ++i)
        {
            var unlockAnimalPanel = unlockAnimalPanelPool.Get();
            unlockAnimalPanel.TryGetComponent(out UnlockedAnimalPanel animalUnlockPanel);
            animalUnlockPanel.SetAnimalStatData(DataTableManager.animalDataTable.Get(animalIdList[i]));
            onAnimalUnlockPanelInstantiated?.Invoke(unlockAnimalPanel);
            unlockAnimalPanel.transform.localScale = Vector3.one;
            animalUnlockPanelList.Add(unlockAnimalPanel);
        }   
        
        // TempCode //
    }
    
    public void Initialize()
    {
        GameDataManager.Instance.Initialize();
    }

    public void Clear()
    {
        
    }
    
    public void EnableAllLayoutGroup(SwitchableCanvasType showCanvasType)
    {
        SwitchActiveDefaultCanvas(true);
        
        SwitchActiveLayoutGroupController(true);
        
        SwitchActiveSwitchableCanvas(showCanvasType);
    }
    
    public void DisableAllLayoutGroup(SwitchableCanvasType showCanvasType)
    {
        SwitchActiveDefaultCanvas(true);
        
        SwitchActiveLayoutGroupController(false);
        
        SwitchActiveSwitchableCanvas(showCanvasType);
    }
    
    public IEnumerator DisableAfterFrameAllLayoutGroup(SwitchableCanvasType showCanvasType)
    {
        SwitchActiveDefaultCanvas(true);
        
        SwitchVisualizeSwitchableCanvas(showCanvasType, false);
        
        yield return null;

        SwitchActiveLayoutGroupController(false);

        SwitchVisualizeSwitchableCanvas(showCanvasType, true);
        
        SwitchActiveSwitchableCanvas(showCanvasType);
    }

    public void SwitchVisualizeSwitchableCanvas(SwitchableCanvasType showCanvasType, bool isVisibleOtherCanvas, bool isVisibleShowCanvasType = true)
    {
        onSwitchVisualizeSwitchableCanvas?.Invoke(showCanvasType, isVisibleOtherCanvas, isVisibleShowCanvasType);
    }
    
    public void SwitchActiveSwitchableCanvas(SwitchableCanvasType type)
    {
        onSwitchActiveSwitchableCanvas?.Invoke(type);
    }
    
    private void SwitchActiveDefaultCanvas(bool isActive)
    {
        onSwitchActiveDefaultCanvases?.Invoke(isActive);
    }

    private void SwitchActiveLayoutGroupController(bool isActive)
    { 
        onSwitchActiveLayoutGroupControllers?.Invoke(isActive);
    }

    public void SetOutGameManager(OutGameManager outGameManager)
    {
        this.outGameManager = outGameManager;
    }

    public void ShowAlertSingleButtonPanel()
    {
        alertPanelSpawnPanelRoot.SetActive(true);

        var alertPanel = alertSingleButtonPanelPool.Get();
        alertPanel.transform.SetParent(alertPanelSpawnPanel.transform);
        alertPanel.transform.localPosition = Vector3.zero;
        
        alertSingleButtonPanelList.Add(alertPanel);
    }
    
    public void ShowAlertDoubleButtonPanel()
    {
        alertPanelSpawnPanelRoot.SetActive(true);
        
        var alertPanel = alertDoubleButtonPanelPool.Get();
        alertPanel.transform.SetParent(alertPanelSpawnPanel.transform);  
        alertPanel.transform.localPosition = Vector3.zero;
        
        alertDoubleButtonPanelList.Add(alertPanel);
    }

    public void HideAlertPanelSpawnPanelRoot()
    {
        for (int i = 0; i < alertSingleButtonPanelList.Count; ++i)
        {
            alertSingleButtonPanelList[i].transform.SetParent(alertPanelReleaseParent.transform);
            alertSingleButtonPanelPool.Release(alertSingleButtonPanelList[i]);
        }
        
        for (int i = 0; i < alertDoubleButtonPanelList.Count; ++i)
        {
            alertDoubleButtonPanelList[i].transform.SetParent(alertPanelReleaseParent.transform);
            alertDoubleButtonPanelPool.Release(alertDoubleButtonPanelList[i]);
        }
        
        alertSingleButtonPanelList.Clear();
        alertDoubleButtonPanelList.Clear();
        
        alertPanelSpawnPanelRoot.SetActive(false);
    }
}