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
    public const string InGameLevelExperienceValueTableName= "InGameLevelExperienceValue_Table";
    public const string PassiveEffectTableName= "PassiveEffect_Table";
    public const string GachaTableName= "Gacha_Table";
    public const string PlayerLevelTableName = "PlayerLevel_Table";
    public const string EnforceAnimalTableName = "EnforceAnimal_Table";
    
    public const string GameManagerTag = "GameManager";

    public const string AnimalSelectedString = "OUTGAME_SELECTED";
    public const string AnimalSelectableString = "OUTGAME_SELECT";

    public const string ItemHumanAnimatorDefaultString = "Default";
    public static readonly int ItemHumanAnimatorDefaultHash = Animator.StringToHash(ItemHumanAnimatorDefaultString);
    public const string ItemHumanAnimatorDeadString = "Dead";
    public static readonly int ItemHumanAnimatorDeadHash = Animator.StringToHash(ItemHumanAnimatorDeadString);

    public const string TrapBombAnimatorJumpAttackString = "JumpAttack";
    public static readonly int TrapBombAnimatorJumpAttackHash = Animator.StringToHash(TrapBombAnimatorJumpAttackString);
    public const string TrapBombAnimatorAttackString = "Attack";
    public static readonly int TrapBombAnimatorAttackHash = Animator.StringToHash(TrapBombAnimatorAttackString);

    public const string BossAttackPattern1AnimatorString = "Attack1";
    public const string BossAttackPattern2AnimatorString = "Attack2";
    public const string BossDeathAnimatorString = "Death";

    public const string StaminaGoldUseStringKey = "OUTGAME_GOLD";
    public const string GachaSingleAdsStringKey = "OUTGAME_ADS";
    public const string AnimalGradeSortDropDownStringKey = "OUTGAME_GRADE";
    public const string AnimalLevelSortDropDownStringKey = "OUTGAME_LEVEL";
    public const string AnimalQuitGameStringKey = "OUTGAME_END";
    public const string AnimalTicketStringKey = "OUTGAME_TICKET_PROCESS";
    public const string AnimalAdsStringKey = "OUTGMAE_CONFIRMADS";
    public const string AnimalTicketBuyStringKey = "OUTGAME_TICKET_BUY";
    public const string AnimalNoMoneyStringKey = "OUTGAME_NO_MONEY";
    public const string AnimalBuyStringKey = "OUTGAME_BUY";
    public const string AnimalUpgradeProcessStringKey = "OUTGAME_UPGRADE_PROCESS";
    public const string AnimalManyStaminaStringKey = "OUTGAME_MANYSTAMINA";
    public const string AnimalAttackPowerStringKey = "OUTGAME_ATTACKPOWER";
    public const string AnimalLevelStringKey = "OUTGAME_LEVEL";
    public const string AnimalSkillStringKey = "OUTGAME_SKILL";
    public const string AnimalEndowmentStringKey = "OUTGAME_ENDOWMENT_EFFECT";
    public const string AnimalUpgradeStringKey = "OUTGAME_UPGRADE";
    public const string AnimalUpgradeCompleteStringKey = "OUTGAME_UPGRADE_COMPLETE";
    public const string AnimalBronzeDuplicateStringKey = "OUTGAME_BRONZETOKENDUPLICATE";
    public const string AnimalSliverDuplicateStringKey = "OUTGAME_SLIVERTOKENDUPLICATE";
    public const string AnimalGoldDuplicateStringKey = "OUTGAME_GOLDTOKENDUPLICATE";
    public const string AnimalTokenChangeStringKey = "OUTGAME_CHANGE";
    public const string CountryIconSpriteKey = "CountryIcon";

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

    public static int GetIndexRandomChanceHitInCumulativeChanceList(List<float> cumulativeChances)
    {
        float randValue = Random.value;
        
        for(int i = 0; i < cumulativeChances.Count; i++)
        {
            if (randValue <= cumulativeChances[i])
            {
                return i;
            }
        }
        
        throw new System.ArgumentException("The sum of input chances must be equal to 1f.", nameof(cumulativeChances));
    }
}