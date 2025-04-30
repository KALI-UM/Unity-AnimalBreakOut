using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    
    public void SetDescriptionTextAndButtonAction(AlertPanelInfoData alertPanelInfoData)
    {
        descriptionText.text = alertPanelInfoData.description;

        if (confirmButton is not null)
        {
            confirmButton.onClick.RemoveAllListeners();
            confirmButton.onClick.AddListener(alertPanelInfoData.confirmButtonAction);
        }
        
        if (cancelButton is not null)
        {
            cancelButton.onClick.RemoveAllListeners();
            cancelButton.onClick.AddListener(alertPanelInfoData.cancelButtonAction);
        }
    }
}