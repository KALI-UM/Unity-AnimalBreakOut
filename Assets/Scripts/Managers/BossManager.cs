using System;
using UnityEngine;

public class BossManager : InGameManager
{
    [SerializeField] private GameObject bossPrefab;
    private BossStatus bossStatus;
    
    [SerializeField] private GameObject parentGameObjectToSpawnBoss;
    public static readonly Vector3 spawnLocalPosition = new(0f, 0.25f, 10f);
    
    private GameManager_new gameManager;
    
    public static event Action<BossStatus> onSpawnBoss;
    
    private void Start()
    {
        GameObject.FindGameObjectWithTag("GameManager").TryGetComponent(out gameManager);
        
        GameManager.StageManager.onBossStageEnter += SpawnBoss;
    }

    private void OnDestroy()
    {
        gameManager.StageManager.onBossStageEnter -= SpawnBoss;
    }

    private void SpawnBoss()
    {
        var boss = Instantiate(bossPrefab, parentGameObjectToSpawnBoss.transform);
        boss.transform.localPosition = spawnLocalPosition;
        boss.TryGetComponent(out bossStatus);
        boss.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);

        bossStatus.InitializeStatus(50000f);
        boss.TryGetComponent(out BossBehaviourController bossBehaviourController);
        bossBehaviourController.InitBehaviorTree(BossBehaviourTreeType.Boss1BehaviourTree);

        onSpawnBoss?.Invoke(bossStatus);
    }
}