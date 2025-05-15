using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class AlertPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    
    private ObjectPool<GameObject> alertPanelObjectPool;
    private List<GameObject> alertPanelList;
    private GameObject releaseParent;
    
    public void SetDescriptionTextAndButtonAction(AlertPanelInfoData alertPanelInfoData)
    {
        descriptionText.text = alertPanelInfoData.description;

        if (confirmButton is not null)
        {
            confirmButton.onClick.RemoveAllListeners();
            confirmButton.onClick.AddListener(alertPanelInfoData.confirmButtonAction);
            confirmButton.onClick.AddListener(() => SoundManager.Instance.PlaySfx(SfxClipId.ButtonTouch));
        }
        
        if (cancelButton is not null)
        {
            cancelButton.onClick.RemoveAllListeners();
            cancelButton.onClick.AddListener(alertPanelInfoData.cancelButtonAction);
            cancelButton.onClick.AddListener(() => SoundManager.Instance.PlaySfx(SfxClipId.ButtonTouch));
        }
    }

    private void OnDestroy()
    {
        if (confirmButton is not null)
        {
            confirmButton.onClick.RemoveAllListeners();
        }
        
        if (cancelButton is not null)
        {
            cancelButton.onClick.RemoveAllListeners();
        }
    }

    public void SetReleaseBySelf(ObjectPool<GameObject> alertPanelObjectPool, List<GameObject> alertPanelList, GameObject releaseParent)
    {
        this.alertPanelObjectPool = alertPanelObjectPool;
        this.alertPanelList = alertPanelList;
        this.releaseParent = releaseParent;
    }

    public void Release()
    {
        alertPanelList.Remove(transform.parent.gameObject);
        transform.parent.SetParent(releaseParent.transform);
        alertPanelObjectPool.Release(transform.parent.gameObject);
    }

    public void SetInteractableButton(bool interactable)
    {
        confirmButton.interactable = interactable;
        cancelButton.interactable = interactable;
    }
}