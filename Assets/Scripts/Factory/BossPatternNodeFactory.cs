using System.Collections.Generic;
using UnityEngine;

public static class BossPatternNodeFactory
{
    private const float Boss1PhaseChangeHpValue = 0.36f;
    
    private const float Boss1Phase1AttackTimeDelay = 3f;
    private const float Boss1Phase2AttackTimeDelay = 1.5f;
    private const float Boss1AttackPattern1AnimationTimeDelay = 2.2f;
    private const float Boss1AttackPattern2AnimationTimeDelay = 1.6f;
    private const float Boss1AttackPattern3AnimationTimeDelay = 1.6f;
    
    private const float Boss1Phase1Pattern1Chance = 0.7f;
    private const float Boss1Phase1Pattern2Chance = 0.3f;
    private const int Boss1Phase1Pattern3UseCount = 5;
    
    private const float Boss1Phase2Pattern1Chance = 0.5f;
    private const float Boss1Phase2Pattern2Chance = 0.5f;
    private const int Boss1Phase2Pattern3UseCount = 6;
    
    private const float Boss1AttackPattern1DelayTime = 0.5f;
    private const float Boss1AttackPattern2DelayTime = 0.5f;
    private const float Boss1AttackPattern3DelayTime = 0.5f;

    public static ConditionNode<BossBehaviourController> GetBossConditionNode(BossBehaviourController bossBehaviourController, BossConditionNodeType type)
    {
        List<float> boss1Phase1Chances = new();
        boss1Phase1Chances.Add(Boss1Phase1Pattern1Chance);
        boss1Phase1Chances.Add(Boss1Phase1Pattern2Chance);
        var cumulativeBoss1Phase1Chances = Utils.ToCumulativeChanceList(boss1Phase1Chances);
        
        List<float> boss1Phase2Chances = new();
        boss1Phase2Chances.Add(Boss1Phase2Pattern1Chance);
        boss1Phase2Chances.Add(Boss1Phase2Pattern2Chance);
        var cumulativeBoss1Phase2Chances = Utils.ToCumulativeChanceList(boss1Phase2Chances);
        
        
        switch (type)
        {
            case BossConditionNodeType.Boss1PhaseChangeHpCondition:
                {
                    return new(bossBehaviourController, 
                        BossPatternFuncFactory.GetBossHpCondition(BossHpConditionType.HpRatioLessThan, Boss1PhaseChangeHpValue));
                }
            case BossConditionNodeType.Boss1Phase1Pattern3UseCountCondition:
                {
                    return new(bossBehaviourController, 
                        BossPatternFuncFactory.GetBossPatternUseCountCondition(BossPatternUseCountConditionType.PatternUseCountAtLeast, Boss1Phase1Pattern3UseCount));
                }
            case BossConditionNodeType.Boss1Phase2Pattern3UseCountCondition:
                {
                    return new(bossBehaviourController, 
                        BossPatternFuncFactory.GetBossPatternUseCountCondition(BossPatternUseCountConditionType.PatternUseCountAtLeast, Boss1Phase2Pattern3UseCount));
                }
            case BossConditionNodeType.Boss1Phase1Pattern1ChanceCondition:
                {
                    return new(bossBehaviourController,
                        BossPatternFuncFactory .GetBossRandomPatternSelectCondition(BossRandomPatternSelectConditionType.RandomValue, cumulativeBoss1Phase1Chances[0]));
                }
            case BossConditionNodeType.Boss1Phase1Pattern2ChanceCondition:
                {
                    return new(bossBehaviourController, 
                        BossPatternFuncFactory.GetBossRandomPatternSelectCondition(BossRandomPatternSelectConditionType.RandomValue, cumulativeBoss1Phase1Chances[1]));
                }
            case BossConditionNodeType.Boss1Phase2Pattern1ChanceCondition:
                {
                    return new(bossBehaviourController, 
                        BossPatternFuncFactory.GetBossRandomPatternSelectCondition(BossRandomPatternSelectConditionType.RandomValue, cumulativeBoss1Phase2Chances[0]));
                }
            case BossConditionNodeType.Boss1Phase2Pattern2ChanceCondition:
                {
                    return new(bossBehaviourController, 
                        BossPatternFuncFactory.GetBossRandomPatternSelectCondition(BossRandomPatternSelectConditionType.RandomValue, cumulativeBoss1Phase2Chances[1]));
                }
            case BossConditionNodeType.IsBossDeadCondition:
                {
                    return new(bossBehaviourController,
                        BossPatternFuncFactory.GetBossStatusCondition(BossStatusConditionType.IsBossDead));
                }
        }
        
        Debug.Assert(false, "Invalid BossConditionNodeType");
        
        return null;
    }

    public static TimerNode<BossBehaviourController> GetBossTimerNode(BossBehaviourController bossBehaviourController, BossTimerNodeType type)
    {
        switch (type)
        {
            case BossTimerNodeType.Boss1Phase1AttackTimeDelayTimer:
                {
                    return new(bossBehaviourController, Boss1Phase1AttackTimeDelay);
                }
            case BossTimerNodeType.Boss1Phase2AttackTimeDelayTimer:
                {
                    return new(bossBehaviourController, Boss1Phase2AttackTimeDelay);
                }
            case BossTimerNodeType.Boss1AttackPattern1AnimationTimeDelayTimer:
                {
                    return new(bossBehaviourController, Boss1AttackPattern1AnimationTimeDelay);
                }
            case BossTimerNodeType.Boss1AttackPattern2AnimationTimeDelayTimer:
                {
                    return new(bossBehaviourController, Boss1AttackPattern2AnimationTimeDelay);
                }
            case BossTimerNodeType.Boss1AttackPattern3AnimationTimeDelayTimer:
                {
                    return new(bossBehaviourController, Boss1AttackPattern3AnimationTimeDelay);
                }
        }
        
        Debug.Assert(false, "Invalid BossTimerNodeType");
        
        return null;
    }

    public static DelayNode<BossBehaviourController> GetBossDelayNode(BossBehaviourController bossBehaviourController, BossDelayNodeType type)
    {
        switch (type)
        {
            case BossDelayNodeType.Boss1AttackPattern1Delay:
                {
                    return new(bossBehaviourController,Boss1AttackPattern1DelayTime);
                }
            case BossDelayNodeType.Boss1AttackPattern2Delay:
                {
                    return new(bossBehaviourController,Boss1AttackPattern2DelayTime);
                }
            case BossDelayNodeType.Boss1AttackPattern3Delay:
                {
                    return new(bossBehaviourController,Boss1AttackPattern3DelayTime);
                }
        }
        
        Debug.Assert(false, "Invalid BossDelayNodeType");
        
        return null;
    }

    public static ActionNode<BossBehaviourController> GetBossActionNode(BossBehaviourController bossBehaviourController, BossActionNodeType type)
    {
        switch (type)
        {
            case BossActionNodeType.Boss1AttackPattern1:
                {
                    return new(bossBehaviourController, 
                        BossPatternFuncFactory.GetBossAttackPatternAction(BossAttackPatternActionType.TestAttackToLane0));
                }
            case BossActionNodeType.Boss1AttackPattern2:
                {
                    return new(bossBehaviourController,
                        BossPatternFuncFactory.GetBossAttackPatternAction(BossAttackPatternActionType.TestAttackToLane1));
                }
            case BossActionNodeType.Boss1AttackPattern3:
                {
                    return new(bossBehaviourController, 
                        BossPatternFuncFactory.GetBossAttackPatternAction(BossAttackPatternActionType.TestAttackToLane2));
                }
            case BossActionNodeType.Boss1AttackAnimation1:
                {
                    return new(bossBehaviourController,
                        BossPatternFuncFactory.GetBossAttackPatternAction(BossAttackPatternActionType.Boss1AttackAnimation1));
                }
            case BossActionNodeType.Boss1AttackAnimation2:
                {
                    return new(bossBehaviourController,
                        BossPatternFuncFactory.GetBossAttackPatternAction(BossAttackPatternActionType.Boss1AttackAnimation2));
                }
            case BossActionNodeType.Boss1DeathAnimation:
                {
                    return new(bossBehaviourController,
                        BossPatternFuncFactory.GetBossAttackPatternAction(BossAttackPatternActionType.BossDeathAnimation));
                }
        }
        
        Debug.Assert(false, "Invalid BossActionNodeType");
        
        return null;
    }
}