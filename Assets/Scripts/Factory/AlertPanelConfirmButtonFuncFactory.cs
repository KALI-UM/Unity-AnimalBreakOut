using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class AlertPanelConfirmButtonFuncFactory
{
    private static readonly Dictionary<AlertPanelConfirmButtonFuncType, UnityAction>
        alertPanelConfirmButtonFuncs = new()
        {
            {
                AlertPanelConfirmButtonFuncType.CloseAlertPanel, () =>
                {
                    GameObject.FindGameObjectWithTag("OutGameManager").TryGetComponent(out OutGameManager outGameManager);
                    outGameManager.OutGameUIManager.HideAlertPanelSpawnPanelRoot();
                }
            },
            {
                AlertPanelConfirmButtonFuncType.QuitGame, Application.Quit
            }
        };
    
    public static UnityAction GetAlertPanelConfirmButtonFunc(AlertPanelConfirmButtonFuncType type)
    {
        if (alertPanelConfirmButtonFuncs.TryGetValue(type, out var func))
        {
            return func;
        }

        Debug.Assert(false, $"Cant find AlertPanelConfirmButtonFunc in AlertPanelConfirmButtonFuncType: {type}");
        
        return null;
    }
}