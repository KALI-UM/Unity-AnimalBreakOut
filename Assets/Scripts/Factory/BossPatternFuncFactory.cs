using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class BossPatternFuncFactory
{
    private static readonly Dictionary<BossHpConditionType, Func<float, Func<BossBehaviourController, bool>>>
        BossHpConditions = new()
        {
            { BossHpConditionType.HpRatioLessThan, hpRatio => bossBehaviourController => (bossBehaviourController.BossStatus.currentHp / bossBehaviourController.BossStatus.maxHp) < hpRatio }
        };

    private static readonly Dictionary<BossPatternUseCountConditionType, Func<int, Func<BossBehaviourController, bool>>>
        BossPatternUseCountConditions = new()
            {
                { BossPatternUseCountConditionType.PatternUseCountAtLeast, maxPatternUseCount => bossBehaviourController => bossBehaviourController.PatternUseCount >= maxPatternUseCount}
            };

    private static readonly Dictionary<BossRandomPatternSelectConditionType, Func<float, Func<BossBehaviourController, bool>>>
        BossRandomPatternSelectConditions = new()
            {
                { BossRandomPatternSelectConditionType.RandomValue, chance => bossBehaviourController => bossBehaviourController.BossPatternSelectRandomValue <= chance}
            };

    private static readonly Dictionary<BossStatusConditionType, Func<BossBehaviourController, bool>>
        BossStatusConditions = new()
        {
            { BossStatusConditionType.IsBossDead, bossBehaviourController => bossBehaviourController.BossStatus.isDead},
            { BossStatusConditionType.IsBossAlive, bossBehaviourController => !bossBehaviourController.BossStatus.isDead},
        };

    private static readonly Dictionary<BossAttackPatternActionType, Func<BossBehaviourController, BTNodeState>>
        BossAttackPatternActions = new()
            {
                { BossAttackPatternActionType.TestAttackToLane0, TestAttackToLane0},
                { BossAttackPatternActionType.TestAttackToLane1, TestAttackToLane1},
                { BossAttackPatternActionType.TestAttackToLane2, TestAttackToLane2},

                { BossAttackPatternActionType.Boss1AttackPattern1, Boss1AttackPattern1},
                { BossAttackPatternActionType.Boss1AttackPattern2, Boss1AttackPattern2},
                { BossAttackPatternActionType.Boss1AttackPattern3, Boss1AttackPattern3},

                { BossAttackPatternActionType.Boss1AttackAnimation1, PlayBossAttackPattern1Animation},
                {BossAttackPatternActionType.Boss1AttackAnimation2, PlayBossAttackPattern2Animation},
                { BossAttackPatternActionType.BossDeathAnimation, PlayBossDeathAnimation}
            };

    public static Func<BossBehaviourController, bool> GetBossHpCondition(BossHpConditionType type, float hpValue)
    {
        if (BossHpConditions.TryGetValue(type, out var func))
        {
            return func(hpValue);
        }

        Debug.Assert(false, $"Cant find BossPatternCondition in BossPatternConditionType: {type}");

        return null;
    }

    public static Func<BossBehaviourController, bool> GetBossPatternUseCountCondition(BossPatternUseCountConditionType type, int count)
    {
        if (BossPatternUseCountConditions.TryGetValue(type, out var func))
        {
            return func(count);
        }

        Debug.Assert(false, $"Cant find BossPatternCondition in BossPatternConditionType: {type}");

        return null;
    }

    public static Func<BossBehaviourController, bool> GetBossRandomPatternSelectCondition(BossRandomPatternSelectConditionType type, float chance)
    {
        if (BossRandomPatternSelectConditions.TryGetValue(type, out var func))
        {
            return func(chance);
        }

        Debug.Assert(false, $"Cant find BossPatternCondition in BossPatternConditionType: {type}");

        return null;
    }

    public static Func<BossBehaviourController, bool> GetBossStatusCondition(BossStatusConditionType type)
    {
        if (BossStatusConditions.TryGetValue(type, out var func))
        {
            return func;
        }

        Debug.Assert(false, $"Cant find BossStatusCondition in BossStatusConditionType: {type}");

        return null;
    }

    public static Func<BossBehaviourController, BTNodeState> GetBossAttackPatternAction(BossAttackPatternActionType type)
    {
        if (BossAttackPatternActions.TryGetValue(type, out var func))
        {
            return func;
        }

        Debug.Assert(false, $"Cant find BossAttackPatternAction in BossAttackPatternType: {type}");

        return null;
    }

    private static BTNodeState TestAttackToLane0(BossBehaviourController bossBehaviourController)
    {
        Vector3 attackPosition = bossBehaviourController.GetLaneAttackPosition(0);
        var tempBossProjectile = bossBehaviourController.TempBossProjectilePool.Get();
        tempBossProjectile.TryGetComponent(out TempBossProjectile tempBossProjectileComponent);
        tempBossProjectile.transform.SetParent(bossBehaviourController.transform);
        tempBossProjectileComponent.Initialize(attackPosition,
            bossBehaviourController.LocalDirectionToPlayer,
            5f,
            bossBehaviourController.TempBossProjectilePool,
            bossBehaviourController.TempBossProjectileList,
            bossBehaviourController.ProjectileReleaseParent.transform);
        bossBehaviourController.TempBossProjectileList.Add(tempBossProjectile);

        AfterUsingNormalPattern(bossBehaviourController);

        return BTNodeState.Success;
    }

    private static BTNodeState TestAttackToLane1(BossBehaviourController bossBehaviourController)
    {
        Vector3 attackPosition = bossBehaviourController.GetLaneAttackPosition(1);
        var tempBossProjectile = bossBehaviourController.TempBossProjectilePool.Get();
        tempBossProjectile.TryGetComponent(out TempBossProjectile tempBossProjectileComponent);
        tempBossProjectile.transform.SetParent(bossBehaviourController.transform);
        tempBossProjectileComponent.Initialize(attackPosition,
            bossBehaviourController.LocalDirectionToPlayer,
            5f,
            bossBehaviourController.TempBossProjectilePool,
            bossBehaviourController.TempBossProjectileList,
            bossBehaviourController.ProjectileReleaseParent.transform);
        bossBehaviourController.TempBossProjectileList.Add(tempBossProjectile);

        AfterUsingNormalPattern(bossBehaviourController);

        return BTNodeState.Success;
    }

    private static BTNodeState TestAttackToLane2(BossBehaviourController bossBehaviourController)
    {
        Vector3 attackPosition = bossBehaviourController.GetLaneAttackPosition(2);
        var tempBossProjectile = bossBehaviourController.TempBossProjectilePool.Get();
        tempBossProjectile.TryGetComponent(out TempBossProjectile tempBossProjectileComponent);
        tempBossProjectile.transform.SetParent(bossBehaviourController.transform);
        tempBossProjectileComponent.Initialize(attackPosition,
            bossBehaviourController.LocalDirectionToPlayer,
            5f,
            bossBehaviourController.TempBossProjectilePool,
            bossBehaviourController.TempBossProjectileList,
            bossBehaviourController.ProjectileReleaseParent.transform);
        bossBehaviourController.TempBossProjectileList.Add(tempBossProjectile);

        AfterUsingSpecialPattern(bossBehaviourController);

        return BTNodeState.Success;
    }

    private static BTNodeState Boss1AttackPattern1(BossBehaviourController bossBehaviourController)
    {
        Vector3 attackPosition = bossBehaviourController.GetLaneAttackPosition(Random.Range(0, 3));
        var bossProjectile = bossBehaviourController.BossProjectilePooler.GetBossProjectile(1);
        bossProjectile.transform.SetParent(bossBehaviourController.transform);
        bossProjectile.transform.rotation = Quaternion.LookRotation(bossBehaviourController.LocalDirectionToPlayer, Vector3.up);
        bossProjectile.Initialize(attackPosition,
            bossBehaviourController.LocalDirectionToPlayer,
            8f,
            bossBehaviourController.ProjectileReleaseParent.transform);

        AfterUsingNormalPattern(bossBehaviourController);
        SoundManager.Instance.PlaySfx(SfxClipId.Boss1SwordSlash1);
        return BTNodeState.Success;
    }

    private static BTNodeState Boss1AttackPattern2(BossBehaviourController bossBehaviourController)
    {
        int randPositionIndex = Random.Range(0, 2);

        Vector3 attackPosition = randPositionIndex == 0
            ? (bossBehaviourController.GetLaneAttackPosition(0) + bossBehaviourController.GetLaneAttackPosition(1)) / 2f
            : (bossBehaviourController.GetLaneAttackPosition(1) + bossBehaviourController.GetLaneAttackPosition(2)) / 2f;
        var bossProjectile = bossBehaviourController.BossProjectilePooler.GetBossProjectile(0);
        bossProjectile.transform.SetParent(bossBehaviourController.transform);
        bossProjectile.transform.rotation = Quaternion.LookRotation(bossBehaviourController.LocalDirectionToPlayer, Vector3.up);
        bossProjectile.Initialize(attackPosition,
            bossBehaviourController.LocalDirectionToPlayer,
            0f,
            bossBehaviourController.ProjectileReleaseParent.transform);

        AfterUsingNormalPattern(bossBehaviourController);
        SoundManager.Instance.PlaySfx(SfxClipId.Boss1SwordSlash2);
        return BTNodeState.Success;
    }

    private static BTNodeState Boss1AttackPattern3(BossBehaviourController bossBehaviourController)
    {
        Vector3 attackPosition = bossBehaviourController.GetLaneAttackPosition(1);
        var bossProjectile = bossBehaviourController.BossProjectilePooler.GetBossProjectile(2);
        bossProjectile.transform.SetParent(bossBehaviourController.transform);
        bossProjectile.transform.rotation = Quaternion.LookRotation(bossBehaviourController.LocalDirectionToPlayer, Vector3.up);
        bossProjectile.Initialize(attackPosition,
            bossBehaviourController.LocalDirectionToPlayer,
            10f,
            bossBehaviourController.ProjectileReleaseParent.transform);

        AfterUsingSpecialPattern(bossBehaviourController);
        SoundManager.Instance.PlaySfx(SfxClipId.Boss1SwordSlash3);
        return BTNodeState.Success;
    }

    private static BTNodeState PlayBossAttackPattern1Animation(BossBehaviourController bossBehaviourController)
    {
        var isPlayAnimation = bossBehaviourController.PlayAnimation(Utils.BossAttackPattern2AnimatorString);

        if (!isPlayAnimation)
        {
            Debug.Assert(false, "Cant play Animation.");

            return BTNodeState.Failure;
        }

        return BTNodeState.Success;
    }

    private static BTNodeState PlayBossAttackPattern2Animation(BossBehaviourController bossBehaviourController)
    {
        var isPlayAnimation = bossBehaviourController.PlayAnimation(Utils.BossAttackPattern1AnimatorString);

        if (!isPlayAnimation)
        {
            Debug.Assert(false, "Cant play Animation.");

            return BTNodeState.Failure;
        }

        return BTNodeState.Success;
    }

    private static BTNodeState PlayBossDeathAnimation(BossBehaviourController bossBehaviourController)
    {
        if (bossBehaviourController is null)
        {
            return BTNodeState.Failure;
        }

        var isPlayAnimation = bossBehaviourController.PlayAnimation(Utils.BossDeathAnimatorString);

        if (!isPlayAnimation)
        {
            Debug.Assert(false, "Cant play Animation.");

            return BTNodeState.Failure;
        }

        return BTNodeState.Success;
    }

    private static void AfterUsingNormalPattern(BossBehaviourController bossBehaviourController)
    {
        bossBehaviourController.AddPatternUseCount();
        bossBehaviourController.SetBossPatternSelectRandomValue();
    }

    private static void AfterUsingSpecialPattern(BossBehaviourController bossBehaviourController)
    {
        bossBehaviourController.ClearPatternUseCount();
        bossBehaviourController.SetBossPatternSelectRandomValue();
    }
}