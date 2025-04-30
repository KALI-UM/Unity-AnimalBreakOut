public enum MapObjectCSVType
{
    Bomb = 1,
    Hole,
    Human,
    PenaltyCoin,
}

public enum RewardCoinPatternCSVType
{
    Straight = 1,
    Hill,
}

public enum WayType
{
    Straight = 0,
    Left,
    Right,
    UnavoidableWall,

    Count
}

public enum ObjectType
{
    None = -1,

    Item = 0,
    TrapBomb,
    TrapHole,
    Wall,
    ItemTrapMixed,

    Count
}

public enum ItemType
{
    None = -1,

    RewardCoin = 0,
    PenaltyCoin,
    Human,

    Count
}

public enum RewardCoinItemType
{
    None = -1,

    BronzeCoin = 110101,
    SilverCoin,
    GoldCoin,
    
    
    PlatinumCoin,
    DiamondCoin,

    Count
}

public enum HumanItemType
{
    None = -1,

    JuniorResearcher = 110301,
    Researcher,
    SeniorResearcher,

    Count
}


public enum PenaltyCoinItemType
{
    None = -1,

    GhostCoin = 110201,
    PoisonCoin,
    SkullCoin,
    FireCoin,
    BlackHoleCoin,

    Count
}

public enum TrapType
{
    None = -1,

    Bomb = 0,
    Hole,

    Count
}

public enum WallType
{
    None = -1,

    NormalWall = 0,
    ReinforcedWall,

    Count
}

public enum DefaultCanvasType
{
    Shop = 0,
    Lobby,
    Animal,

    Menu,
}

public enum SwitchableCanvasType
{
    Shop = 0,
    Lobby,
    Animal,
}

public enum SwitchableShopPanelType
{
    NormalGacha,
    SpecialGacha,
    Stamina,
}

public enum BTNodeState
{
    Success,
    Failure,
    Running
}

public enum BossHpConditionType
{
    HpRatioLessThan,
}

public enum BossPatternUseCountConditionType
{ 
    PatternUseCountAtLeast,
}

public enum BossRandomPatternSelectConditionType
{
    RandomValue,
}

public enum BossStatusConditionType
{
    IsBossDead,
    IsBossAlive,
}

public enum BossAttackPatternActionType
{
    TestAttackToLane0 = -3,
    TestAttackToLane1 = -2,
    TestAttackToLane2 = -1,
    
    Boss1AttackAnimation1,
    Boss1AttackAnimation2,
    
    Boss1AttackPattern1,
    Boss1AttackPattern2,
    Boss1AttackPattern3,
    
    BossDeathAnimation,
}

public enum BossConditionNodeType
{
    Boss1PhaseChangeHpCondition,
    Boss1Phase1Pattern3UseCountCondition,
    Boss1Phase2Pattern3UseCountCondition,
    Boss1Phase1Pattern1ChanceCondition,
    Boss1Phase1Pattern2ChanceCondition,
    Boss1Phase2Pattern1ChanceCondition,
    Boss1Phase2Pattern2ChanceCondition,
    
    IsBossDeadCondition,
    IsBossAliveCondition,
}

public enum BossTimerNodeType
{
    Boss1Phase1AttackTimeDelayTimer,
    Boss1Phase2AttackTimeDelayTimer,
    Boss1AttackPattern1AnimationTimeDelayTimer,
    Boss1AttackPattern2AnimationTimeDelayTimer,
    Boss1AttackPattern3AnimationTimeDelayTimer,
}

public enum BossDelayNodeType
{
    Boss1AttackPattern1Delay,
    Boss1AttackPattern2Delay,
    Boss1AttackPattern3Delay,
}

public enum BossActionNodeType
{
    Boss1AttackPattern1,
    Boss1AttackPattern2,
    Boss1AttackPattern3,
    Boss1AttackAnimation1,
    Boss1AttackAnimation2,
    Boss1DeathAnimation,
}

public enum BossBehaviourTreeType
{
    Boss1BehaviourTree,
    Boss2BehaviourTree,
    Boss3BehaviourTree,
}

public enum AlertPanelConfirmButtonFuncType
{
    CloseAlertPanel,
    QuitGame,
}

public enum AlertPanelCancelButtonFuncType
{
    CloseAlertPanel,
}

public enum AlertPanelInfoDataType
{
    TestCloseAlertPanelSingle = -2,
    TestCloseAlertPanelDouble,
    
    QuitGame,
}