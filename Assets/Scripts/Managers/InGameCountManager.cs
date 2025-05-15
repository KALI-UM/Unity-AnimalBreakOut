using UnityEngine;

public class InGameCountManager : InGameManager
{
    //앞으로 스코어는 인게임카운터내에만 있습니다
    public ScoreSystem ScoreSystem
    {
        get;
        private set;
    }

    public int coinCount;
    //public long ScoreCount;
    public long ExpCount;
    public int ReviveCount;
    public int DeadCount;

    public int JumpCount;
    public int PlayGameCount;
    public int BossDeadCount;

    public override void SetGameManager(GameManager_new gameManager)
    {
        base.SetGameManager(gameManager);
        ScoreSystem = new ScoreSystem();
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    private void Awake()
    {
        BaseCollisionBehaviour.OnCoinAcquired += OnCoinAcquiredHandler;
        //BaseCollisionBehaviour.OnScoreChanged += OnScoreAcquiredHandler;
        PlayerMove.OnJumpCounting += OnJumpCountingHandler;
        ReviveHandler.OnReviveCounting += OnReviveAcquiredHandler;
        GameUIManager.OnGamePlayCounting += OnGamePlayCouningHandler;
        PlayerManager.OnDeadCounting += OnPlayerDeadCountingHandler;
        BossStatus.onBossDeadCounting += OnBossDeadCountingHandler;
    }

    private void OnDestroy()
    {
        BaseCollisionBehaviour.OnCoinAcquired -= OnCoinAcquiredHandler;
        //BaseCollisionBehaviour.OnScoreChanged -= OnScoreAcquiredHandler;
        PlayerMove.OnJumpCounting -= OnJumpCountingHandler;
        ReviveHandler.OnReviveCounting -= OnReviveAcquiredHandler;
        GameUIManager.OnGamePlayCounting -= OnGamePlayCouningHandler;
        PlayerManager.OnDeadCounting -= OnPlayerDeadCountingHandler;
        BossStatus.onBossDeadCounting -= OnBossDeadCountingHandler;
    }

    private void OnCoinAcquiredHandler(int amount)
    {
        coinCount += amount;
        //Debug.Log(coinCount);
    }

    //private void OnScoreAcquiredHandler(long amount)
    //{
    //    ScoreCount += amount;
    //    //Debug.Log(ScoreCount);
    //}

    private void OnReviveAcquiredHandler(int amount)
    {
        ReviveCount += amount;
    }

    private void OnJumpCountingHandler(int amount)
    {
        JumpCount += amount;
    }
    private void OnGamePlayCouningHandler(int amount)
    {
        PlayGameCount += amount;
    }
    private void OnPlayerDeadCountingHandler(int amount)
    {
        DeadCount += amount;
    }
    private void OnBossDeadCountingHandler(int amount)
    {
        BossDeadCount += amount;
    }
}