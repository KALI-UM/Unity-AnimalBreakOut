using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class AlertPanelCancelButtonFuncFactory
{
    private static readonly Dictionary<AlertPanelCancelButtonFuncType, UnityAction>
        alertPanelCancelButtonFuncs = new()
        {
            {
                AlertPanelCancelButtonFuncType.CloseAlertPanel, () =>
                {
                    GameObject.FindGameObjectWithTag("OutGameManager").TryGetComponent(out OutGameManager outGameManager);
                    outGameManager.OutGameUIManager.HideAlertPanelSpawnPanelRoot();
                }
            }
        };
    
    public static UnityAction GetAlertPanelCancelButtonFunc(AlertPanelCancelButtonFuncType type)
    {
        if (alertPanelCancelButtonFuncs.TryGetValue(type, out var func))
        {
            return func;
        }

        Debug.Assert(false, $"Cant find AlertPanelCancelButtonFunc in AlertPanelCancelButtonFuncType: {type}");
        
        return null;
    }
}