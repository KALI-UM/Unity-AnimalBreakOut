using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class AttackSkillRawData
{
    public int SkillID { get; set; }
    public int SkillType { get; set; }
    public string SkillGroup { get; set; }
    public int SkillLevel { get; set; }
    public int Attribute { get; set; }
    public float Damage { get; set; }
    public int ProjectileCount { get; set; }
    public int AttackCount { get; set; }
    public float CoolTime { get; set; }
    public float Speed { get; set; }
    public float Interval { get; set; }
    public int NameID { get; set; }
    public int DescriptionID { get; set; }
    public int EffectID { get; set; }
    public string Prefab { get; set; }
}
public class AttackSkillDataTable : DataTable
{

    private static readonly Dictionary<int, AttackSkillRawData> table = new();

    public List<int> Keys
    {
        get => keys;
    }
    private List<int> keys;

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<AttackSkillRawData>(textAsset.text);
        table.Clear();
        foreach (var data in list)
        {
            if (!table.ContainsKey(data.SkillID))
            {
                table.Add(data.SkillID, data);
            }
            else
            {
                Debug.LogError($"Ű �ߺ�: {data.SkillID}");
            }
        }

        keys = table.Keys.ToList();
    }

    public AttackSkillRawData Get(int itemID)
    {
        if (!table.ContainsKey(itemID))
            return null;
        return table[itemID];
    }

}
