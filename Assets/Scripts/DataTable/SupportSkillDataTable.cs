using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SupportSkillRawData
{
    public int SupportID { get; set; }
    public int SupportType { get; set; }
    public string SupportGroup { get; set; }
    public int Level { get; set; }
    

    public float Value { get; set; }

    public int NameID { get; set; }
    public int DescriptionID { get; set; }
    public string Prefab { get; set; }

    public bool SelectPossible { get; set; }
}

public class SupportSkillDataTable : DataTable
{

    private static readonly Dictionary<int, SupportSkillRawData> table = new();

    public List<int> Keys
    {
        get => keys;
    }
    private List<int> keys;

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<SupportSkillRawData>(textAsset.text);
        table.Clear();
        foreach (var data in list)
        {
            if (!table.ContainsKey(data.SupportID))
            {
                table.Add(data.SupportID, data);
            }
            else
            {
                Debug.LogError($"Ű �ߺ�: {data.SupportID}");
            }
        }

        keys = table.Keys.ToList();
    }

    public SupportSkillRawData Get(int itemID)
    {
        if (!table.ContainsKey(itemID))
            return null;
        return table[itemID];
    }
}
