using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> tables = new Dictionary<string, DataTable>();

    static DataTableManager()
    {
        var mapObjectsDataTable = new MapObjectsDataTable();
        mapObjectsDataTable.Load(Utils.MapObjectsTableName);
        tables.Add(Utils.MapObjectsTableName, mapObjectsDataTable);

        var rewardItemsDataTable = new RewardItemsDataTable();
        rewardItemsDataTable.Load(Utils.RewardItemsTableName);
        tables.Add(Utils.RewardItemsTableName, rewardItemsDataTable);

        var animalDataTable = new AnimalDataTable();
        animalDataTable.Load(Utils.AnimalTableName);
        tables.Add(Utils.AnimalTableName, animalDataTable);

        var itemDataTable = new ItemDataTable();
        itemDataTable.Load(Utils.ItemTableName);
        tables.Add(Utils.ItemTableName, itemDataTable);

        var additionalStatusEffectTable = new AdditionalStatusEffectDataTable();
        additionalStatusEffectTable.Load(Utils.AdditionalStatusEffectTableName);
        tables.Add(Utils.AdditionalStatusEffectTableName, additionalStatusEffectTable);
    }

    public static MapObjectsDataTable mapObjectsDataTable => Get<MapObjectsDataTable>(Utils.MapObjectsTableName);
    public static RewardItemsDataTable rewardItemsDataTable => Get<RewardItemsDataTable>(Utils.RewardItemsTableName);

    public static AnimalDataTable animalDataTable => Get<AnimalDataTable>(Utils.AnimalTableName);
    public static ItemDataTable itemDataTable => Get<ItemDataTable>(Utils.ItemTableName);

    public static AdditionalStatusEffectDataTable additionalStatusEffectDataTable => Get<AdditionalStatusEffectDataTable>(Utils.AdditionalStatusEffectTableName);

    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {
            Debug.LogError($"No Table found for {id}");
            return null;
        }
        return tables[id] as T;
    }
}