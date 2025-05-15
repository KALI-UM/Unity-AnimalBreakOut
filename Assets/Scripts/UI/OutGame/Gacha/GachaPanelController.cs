using System.Collections.Generic;
using UnityEngine;

public class GachaPanelController : MonoBehaviour
{
    [SerializeField] private List<GameObject> gachaPanels;

    private OutGameUIManager outGameUIManager;

    private int currentGachaPanelIndex;

    private void Awake()
    {
        OutGameUIManager.onGachaScreenActive += OnGachaScreenActiveHandler;
    }

    private void Start()
    {
        GameObject.FindGameObjectWithTag("OutGameManager").TryGetComponent(out OutGameManager outGameManager);
        outGameUIManager = outGameManager.OutGameUIManager;
    }

    private void OnDestroy()
    {
        OutGameUIManager.onGachaScreenActive -= OnGachaScreenActiveHandler;
    }

    public void ShowNextGachaPanel()
    {
        ++currentGachaPanelIndex;

        if (currentGachaPanelIndex == gachaPanels.Count - 1)
        {
            SoundManager.Instance.PlaySfx(SfxClipId.GachaResult);
        }
        
        if (currentGachaPanelIndex >= gachaPanels.Count)
        {
            currentGachaPanelIndex = 0;

            for (int i = 0; i < gachaPanels.Count; ++i)
            {
                gachaPanels[i].SetActive(false);
            }

            outGameUIManager.HideFullScreenPanel();

            return;
        }

        SetActiveCurrentGachaPanel();
    }

    private void OnGachaScreenActiveHandler()
    {
        if (gachaPanels.Count == 0)
        {
            Debug.Assert(false, "GachaPanels is empty.");

            return;
        }

        currentGachaPanelIndex = 0;

        SetActiveCurrentGachaPanel();
    }

    private void SetActiveCurrentGachaPanel()
    {
        for (int i = 0; i < gachaPanels.Count; ++i)
        {
            if (i == currentGachaPanelIndex)
            {
                gachaPanels[i].SetActive(true);
            }
            else
            {
                gachaPanels[i].SetActive(false);
            }
        }
    }
}