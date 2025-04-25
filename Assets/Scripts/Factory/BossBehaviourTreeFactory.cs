using UnityEngine;

public static class BossBehaviourTreeFactory
{
    public static BehaviorTree<BossBehaviourController> GetBossBehaviorTree
        (BossBehaviourController bossBehaviourController, BossBehaviourTreeType type)
    {
        switch (type)
        {
            case BossBehaviourTreeType.Boss1BehaviourTree:
                {
                    BehaviorTree<BossBehaviourController> boss1BehaviourTree = new();
                    
                    var rootSelector = new SelectorNode<BossBehaviourController>(bossBehaviourController);
                    boss1BehaviourTree.SetRoot(rootSelector);
                    
                    var bossDeadCondition = BossPatternNodeFactory.GetBossConditionNode(bossBehaviourController,
                        BossConditionNodeType.IsBossDeadCondition);
                    rootSelector.AddChild(bossDeadCondition);
                    
                    var phase2Pattern3Condition = BossPatternNodeFactory.GetBossConditionNode(bossBehaviourController, 
                        BossConditionNodeType.Boss1Phase2Pattern3UseCountCondition);
                    var phase2Pattern3Timer = BossPatternNodeFactory.GetBossTimerNode(bossBehaviourController,
                        BossTimerNodeType.Boss1Phase2AttackTimeDelayTimer);
                    var phase2Pattern3Action = BossPatternNodeFactory.GetBossActionNode(bossBehaviourController,
                        BossActionNodeType.Boss1AttackPattern3);
                    phase2Pattern3Timer.SetChild(phase2Pattern3Action);
                    var attackPattern3Timer = BossPatternNodeFactory.GetBossTimerNode(bossBehaviourController,
                        BossTimerNodeType.Boss1AttackPattern3AnimationTimeDelayTimer);
                    var attackPattern3AnimationAction = BossPatternNodeFactory.GetBossActionNode(bossBehaviourController,
                        BossActionNodeType.Boss1AttackAnimation1);
                    attackPattern3Timer.SetChild(attackPattern3AnimationAction);
                    var phase2Pattern3Sequence = new SequenceNode<BossBehaviourController>(bossBehaviourController);
                    phase2Pattern3Sequence.AddChild(phase2Pattern3Condition);
                    phase2Pattern3Sequence.AddChild(attackPattern3Timer);
                    phase2Pattern3Sequence.AddChild(phase2Pattern3Timer);

                    var phase2Pattern1Condition = BossPatternNodeFactory.GetBossConditionNode(bossBehaviourController,
                        BossConditionNodeType.Boss1Phase2Pattern1ChanceCondition);
                    var phase2Pattern1Timer = BossPatternNodeFactory.GetBossTimerNode(bossBehaviourController,
                        BossTimerNodeType.Boss1Phase2AttackTimeDelayTimer);
                    var phase2Pattern1Action = BossPatternNodeFactory.GetBossActionNode(bossBehaviourController,
                        BossActionNodeType.Boss1AttackPattern1);
                    phase2Pattern1Timer.SetChild(phase2Pattern1Action);
                    var attackPattern1Timer = BossPatternNodeFactory.GetBossTimerNode(bossBehaviourController,
                        BossTimerNodeType.Boss1AttackPattern1AnimationTimeDelayTimer);
                    var attackPattern1AnimationAction = BossPatternNodeFactory.GetBossActionNode(bossBehaviourController,
                        BossActionNodeType.Boss1AttackAnimation2);
                    attackPattern1Timer.SetChild(attackPattern1AnimationAction);
                    var phase2Pattern1Sequence = new SequenceNode<BossBehaviourController>(bossBehaviourController);
                    phase2Pattern1Sequence.AddChild(phase2Pattern1Condition);
                    phase2Pattern1Sequence.AddChild(attackPattern1Timer);
                    phase2Pattern1Sequence.AddChild(phase2Pattern1Timer);
                    
                    var phase2Pattern2Condition = BossPatternNodeFactory.GetBossConditionNode(bossBehaviourController,
                        BossConditionNodeType.Boss1Phase2Pattern2ChanceCondition);
                    var phase2Pattern2Timer = BossPatternNodeFactory.GetBossTimerNode(bossBehaviourController,
                        BossTimerNodeType.Boss1Phase2AttackTimeDelayTimer);
                    var phase2Pattern2Action = BossPatternNodeFactory.GetBossActionNode(bossBehaviourController,
                        BossActionNodeType.Boss1AttackPattern2);
                    phase2Pattern2Timer.SetChild(phase2Pattern2Action);
                    var attackPattern2Timer = BossPatternNodeFactory.GetBossTimerNode(bossBehaviourController,
                        BossTimerNodeType.Boss1AttackPattern2AnimationTimeDelayTimer);
                    var attackPattern2AnimationAction = BossPatternNodeFactory.GetBossActionNode(bossBehaviourController,
                        BossActionNodeType.Boss1AttackAnimation1);
                    attackPattern2Timer.SetChild(attackPattern2AnimationAction);
                    var phase2Pattern2Sequence = new SequenceNode<BossBehaviourController>(bossBehaviourController);
                    phase2Pattern2Sequence.AddChild(phase2Pattern2Condition);
                    phase2Pattern2Sequence.AddChild(attackPattern2Timer);
                    phase2Pattern2Sequence.AddChild(phase2Pattern2Timer);
                    
                    var hpRatioCondition = BossPatternNodeFactory.GetBossConditionNode(bossBehaviourController,
                        BossConditionNodeType.Boss1PhaseChangeHpCondition);
                    var bossPatternSelector1 = new SelectorNode<BossBehaviourController>(bossBehaviourController);
                    bossPatternSelector1.AddChild(phase2Pattern3Sequence);
                    bossPatternSelector1.AddChild(phase2Pattern1Sequence);
                    bossPatternSelector1.AddChild(phase2Pattern2Sequence);
                    var phase2Sequence = new SequenceNode<BossBehaviourController>(bossBehaviourController);
                    phase2Sequence.AddChild(hpRatioCondition);
                    phase2Sequence.AddChild(bossPatternSelector1);
                    rootSelector.AddChild(phase2Sequence);
                    
                    var phase1Pattern3Condition = BossPatternNodeFactory.GetBossConditionNode(bossBehaviourController, 
                        BossConditionNodeType.Boss1Phase1Pattern3UseCountCondition);
                    var phase1Pattern3Timer = BossPatternNodeFactory.GetBossTimerNode(bossBehaviourController,
                        BossTimerNodeType.Boss1Phase1AttackTimeDelayTimer);
                    var phase1Pattern3Action = BossPatternNodeFactory.GetBossActionNode(bossBehaviourController,
                        BossActionNodeType.Boss1AttackPattern3);
                    phase1Pattern3Timer.SetChild(phase1Pattern3Action);
                    var attackPattern3Timer2 = BossPatternNodeFactory.GetBossTimerNode(bossBehaviourController,
                        BossTimerNodeType.Boss1AttackPattern3AnimationTimeDelayTimer);
                    var attackPattern3AnimationAction2 = BossPatternNodeFactory.GetBossActionNode(bossBehaviourController,
                        BossActionNodeType.Boss1AttackAnimation1);
                    attackPattern3Timer2.SetChild(attackPattern3AnimationAction2);
                    var phase1Pattern3Sequence = new SequenceNode<BossBehaviourController>(bossBehaviourController);
                    phase1Pattern3Sequence.AddChild(phase1Pattern3Condition);
                    phase1Pattern3Sequence.AddChild(attackPattern3Timer2);
                    phase1Pattern3Sequence.AddChild(phase1Pattern3Timer);
                    
                    var phase1Pattern1Condition = BossPatternNodeFactory.GetBossConditionNode(bossBehaviourController,
                        BossConditionNodeType.Boss1Phase1Pattern1ChanceCondition);
                    var phase1Pattern1Timer = BossPatternNodeFactory.GetBossTimerNode(bossBehaviourController,
                        BossTimerNodeType.Boss1Phase1AttackTimeDelayTimer);
                    var phase1Pattern1Action = BossPatternNodeFactory.GetBossActionNode(bossBehaviourController,
                        BossActionNodeType.Boss1AttackPattern1);
                    phase1Pattern1Timer.SetChild(phase1Pattern1Action);
                    var attackPattern1Timer2 = BossPatternNodeFactory.GetBossTimerNode(bossBehaviourController,
                        BossTimerNodeType.Boss1AttackPattern1AnimationTimeDelayTimer);
                    var attackPattern1AnimationAction2 = BossPatternNodeFactory.GetBossActionNode(bossBehaviourController,
                        BossActionNodeType.Boss1AttackAnimation2);
                    attackPattern1Timer2.SetChild(attackPattern1AnimationAction2);
                    var phase1Pattern1Sequence = new SequenceNode<BossBehaviourController>(bossBehaviourController);
                    phase1Pattern1Sequence.AddChild(phase1Pattern1Condition);
                    phase1Pattern1Sequence.AddChild(attackPattern1Timer2);
                    phase1Pattern1Sequence.AddChild(phase1Pattern1Timer);
                    
                    var phase1Pattern2Condition = BossPatternNodeFactory.GetBossConditionNode(bossBehaviourController,
                        BossConditionNodeType.Boss1Phase1Pattern2ChanceCondition);
                    var phase1Pattern2Timer = BossPatternNodeFactory.GetBossTimerNode(bossBehaviourController,
                        BossTimerNodeType.Boss1Phase1AttackTimeDelayTimer);
                    var phase1Pattern2Action = BossPatternNodeFactory.GetBossActionNode(bossBehaviourController,
                        BossActionNodeType.Boss1AttackPattern2);
                    phase1Pattern2Timer.SetChild(phase1Pattern2Action);
                    var attackPattern2Timer2 = BossPatternNodeFactory.GetBossTimerNode(bossBehaviourController,
                        BossTimerNodeType.Boss1AttackPattern2AnimationTimeDelayTimer);
                    var attackPattern2AnimationAction2 = BossPatternNodeFactory.GetBossActionNode(bossBehaviourController,
                        BossActionNodeType.Boss1AttackAnimation1);
                    attackPattern2Timer2.SetChild(attackPattern2AnimationAction2);
                    var phase1Pattern2Sequence = new SequenceNode<BossBehaviourController>(bossBehaviourController);
                    phase1Pattern2Sequence.AddChild(phase1Pattern2Condition);
                    phase1Pattern2Sequence.AddChild(attackPattern2Timer2);
                    phase1Pattern2Sequence.AddChild(phase1Pattern2Timer);
                    
                    var bossPatternSelector2 = new SelectorNode<BossBehaviourController>(bossBehaviourController);
                    bossPatternSelector2.AddChild(phase1Pattern3Sequence);
                    bossPatternSelector2.AddChild(phase1Pattern1Sequence);
                    bossPatternSelector2.AddChild(phase1Pattern2Sequence);
                    rootSelector.AddChild(bossPatternSelector2);
                        
                    return boss1BehaviourTree;
                }
            case BossBehaviourTreeType.Boss2BehaviourTree:
                {
                    BehaviorTree<BossBehaviourController> boss2BehaviourTree = new();
                    
                    
                    
                    return boss2BehaviourTree;
                }
            case BossBehaviourTreeType.Boss3BehaviourTree:
                {
                    BehaviorTree<BossBehaviourController> boss3BehaviourTree = new();
                    
                    
                    
                    return boss3BehaviourTree;
                }
        }
        
        Debug.Assert(false, "Invalid BossBehaviourTreeType");
        
        return null;
    }
}