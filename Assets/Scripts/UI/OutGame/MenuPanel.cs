using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    public static Action<SwitchableCanvasType> onMenuBottomButtonClicked;
    
    [SerializeField] private Button shopBottomButton;
    [SerializeField] private Button lobbyBottomButton;
    [SerializeField] private Button animalBottomButton;

    private readonly List<Button> menuPaButtonList = new();
    
    private void Start()
    {
        menuPaButtonList.Add(shopBottomButton);
        menuPaButtonList.Add(lobbyBottomButton);
        menuPaButtonList.Add(animalBottomButton);
        
        shopBottomButton.onClick.RemoveAllListeners();
        shopBottomButton.onClick.AddListener(() => onMenuBottomButtonClicked?.Invoke(SwitchableCanvasType.Shop));
        
        lobbyBottomButton.onClick.RemoveAllListeners();
        lobbyBottomButton.onClick.AddListener(() => onMenuBottomButtonClicked?.Invoke(SwitchableCanvasType.Lobby));
        
        animalBottomButton.onClick.RemoveAllListeners();
        animalBottomButton.onClick.AddListener(() => onMenuBottomButtonClicked?.Invoke(SwitchableCanvasType.Animal));
        
        onMenuBottomButtonClicked += OnMenuBottomButtonClickedHandler;
    }
    
    private void OnDestroy()
    {
        onMenuBottomButtonClicked -= OnMenuBottomButtonClickedHandler;
    }
    
    private void OnMenuBottomButtonClickedHandler(SwitchableCanvasType switchableCanvasType)
    {
        for(int i = 0; i < menuPaButtonList.Count; ++i)
        {
            if ((int)switchableCanvasType == i)
            {
                menuPaButtonList[i].interactable = false;
            }
            else
            {
                menuPaButtonList[i].interactable = true;
            }
        }
    }
}