using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDebuffUIController : UIElement
{
    [SerializeField] private Transform debuffIconArea;
    [SerializeField] private GameObject debuffIconPrefab;

    [SerializeField] private List<DebuffIconInfo> debuffIcons = new();

    private Dictionary<string, DebuffIcon> activeIcons = new();
    private Dictionary<string, Sprite> debuffIconMap = new();

    private void Awake()
    {
        foreach (var info in debuffIcons)
        {
            if (!debuffIconMap.ContainsKey(info.debuffId))
            {
                debuffIconMap.Add(info.debuffId, info.icon);
            }
        }
        BossStatus.onBossDead += ClearAllDebuffs;
    }
    private void OnDestroy()
    {
        BossStatus.onBossDead -= ClearAllDebuffs;
    }
    private const float iconSpacing = 60f;

    public DebuffIcon AddDebuff(string debuffId)
    {
        if (activeIcons.ContainsKey(debuffId))
            return activeIcons[debuffId];

        GameObject iconObj = Instantiate(debuffIconPrefab, debuffIconArea);

        int count = activeIcons.Count; // 이미 몇 개 있는지 세기
        iconObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(count * iconSpacing, 0); // x축 이동

        if (debuffIconMap.TryGetValue(debuffId, out var icon))
        {
            iconObj.GetComponentInChildren<Image>().sprite = icon;
        }

        var debuffIcon = iconObj.GetComponent<DebuffIcon>();
        activeIcons[debuffId] = debuffIcon;

        return debuffIcon;
    }


    public void RemoveDebuff(string debuffId)
    {
        if (!activeIcons.TryGetValue(debuffId, out var debuffIcon)) return;

        Destroy(debuffIcon.gameObject);
        activeIcons.Remove(debuffId);
    }

    public void ClearAllDebuffs()
    {

        foreach (var debuffIcon in activeIcons.Values)
        {
            Destroy(debuffIcon.gameObject);
        }
        activeIcons.Clear();
    }
}

[System.Serializable]
public class DebuffIconInfo
{
    public string debuffId;
    public Sprite icon;
}
