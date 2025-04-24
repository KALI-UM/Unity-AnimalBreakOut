using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public const string PlayerTag = "Player";
    public const string PlayerRootName = "PlayerRoot";

    public const string MapObjectsTableName = "MapObjects_Table";
    public const string RewardItemsTableName = "RewardItems_Table";
    public const string AnimalTableName = "Animal_Table";
    public const string ItemTableName = "Item_Table";
    public const string AttackSkillTableName = "AttackSkill_Table";
    public const string SupportSkillTableName = "SupportSkill_Table";
    public const string AdditionalStatusEffectTableName= "AdditionalStatusEffect_Table";
    public const string GameManagerTag = "GameManager";

    public const string AnimalSelectedString = "선택됨";
    public const string AnimalSelectableString = "선택하기";

    public const string ItemHumanAnimatorDefaultString = "Default";
    public const string ItemHumanAnimatorDeadString = "Dead";

    public const float GameStartWaitTime = 1f;

    public static bool IsChanceHit(float chance)
    {
        return Random.value <= chance;
    }

    /// <summary>
    /// The order of probabilities must match the order of their corresponding enum values.
    /// </summary>
    public static int GetEnumIndexByChance(List<float> chances)
    {
        if (chances == null || chances.Count == 0)
        {
            throw new System.ArgumentException("The chances list is null or empty.", nameof(chances));
        }

        float randValue = Random.value;

        float sum = 0f;
        for (int i = 0; i < chances.Count; i++)
        {
            sum += chances[i];

            if (randValue <= sum)
            {
                return i;
            }
        }

        throw new System.ArgumentException("The sum of input chances must be equal to 1f.", nameof(chances));
    }

    public static List<float> ToCumulativeChanceList(List<float> chances)
    {
        if (chances == null || chances.Count == 0)
        {
            throw new System.ArgumentException("The chances list is null or empty.", nameof(chances));
        }
        
        List<float> cumulativeChances = new(chances.Count);
        
        float cumulativeChance = 0f;

        for (int i = 0; i < chances.Count; i++)
        {
            cumulativeChance += chances[i];
            cumulativeChances.Add(cumulativeChance);
        }

        if (!Mathf.Approximately(cumulativeChance, 1f))
        {
            Debug.Assert(false, "The sum of input chances must be equal to 1f.");
        }

        return cumulativeChances;
    }

    public static int GetIndexRandomChanceHitInList(List<float> chances)
    {
        List<float> cumulativeChances = ToCumulativeChanceList(chances);
        float randValue = Random.value;
        
        for(int i = 0; i < cumulativeChances.Count; i++)
        {
            if (randValue <= cumulativeChances[i])
            {
                return i;
            }
        }
        
        throw new System.ArgumentException("The sum of input chances must be equal to 1f.", nameof(chances));
    }
}