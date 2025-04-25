using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(BossStatus))]
public class BossBehaviourController : MonoBehaviour
{
    private GameManager_new gameManager;
    
    private GameObject playerRoot;
    
    [SerializeField] private GameObject tempBossProjectilePrefab;
    
    private Lane lane;
    public Lane Lane { get; private set; }
    
    private Vector3 localDirectionToPlayer;
    public Vector3 LocalDirectionToPlayer { get; private set; }
    
    private ObjectPool<GameObject> tempBossProjectilePool;
    public ObjectPool<GameObject> TempBossProjectilePool { get; private set; }

    private GameObject projectileReleaseParent;
    public GameObject ProjectileReleaseParent { get; private set; }
    
    private readonly List<GameObject> tempBossProjectileList = new();
    public List<GameObject> TempBossProjectileList { get; private set; }
    
    private BossStatus bossStatus;
    public BossStatus BossStatus { get; private set; }
    
    public int PatternUseCount { get; private set; }
    
    public float BossPatternSelectRandomValue { get; private set; }
    
    private BehaviorTree<BossBehaviourController> behaviorTree;
    
    private Animator animator;
    
    private void Start()
    {
        playerRoot = GameObject.FindGameObjectWithTag("PlayerParent");
        playerRoot.TryGetComponent(out lane);
        Lane = lane;
        
        localDirectionToPlayer = (playerRoot.transform.position - transform.position).normalized;
        LocalDirectionToPlayer = localDirectionToPlayer;
        
        transform.localPosition = BossManager.spawnLocalPosition;
        
        GameObject.FindGameObjectWithTag("GameManager").TryGetComponent(out gameManager);
        tempBossProjectilePool = gameManager.ObjectPoolManager.CreateObjectPool(tempBossProjectilePrefab,
            () => Instantiate(tempBossProjectilePrefab),
            obj => { obj.SetActive(true); },
            obj => { obj.SetActive(false); });
        TempBossProjectilePool = tempBossProjectilePool;

        projectileReleaseParent = GameObject.FindGameObjectWithTag("ProjectileParent");
        ProjectileReleaseParent = projectileReleaseParent;
        
        TryGetComponent(out bossStatus);
        BossStatus = bossStatus;

        PatternUseCount = 0;
        SetBossPatternSelectRandomValue();

        TempBossProjectileList = tempBossProjectileList;
        
        TryGetComponent(out animator);
    }

    private void OnDestroy()
    {
        ClearBossProjectile();
    }

    public void ClearBossProjectile()
    {
        foreach (var tempBossProjectile in tempBossProjectileList)
        {
            if (tempBossProjectile == null)
            {
                continue;
            }
            
            tempBossProjectile.transform.SetParent(projectileReleaseParent.transform);
            tempBossProjectilePool.Release(tempBossProjectile);
        }
        
        tempBossProjectileList.Clear();
    }
    
    private void Update()
    {
        if (behaviorTree is not null)
        {
            behaviorTree.Update();
        }
    }

    public void InitBehaviorTree(BossBehaviourTreeType bossBehaviourTreeType)
    {
        behaviorTree = BossBehaviourTreeFactory.GetBossBehaviorTree(this, bossBehaviourTreeType);
    }

    public void AddPatternUseCount()
    {
        ++PatternUseCount;
    }

    public void ClearPatternUseCount()
    {
        PatternUseCount = 0;
    }

    public void SetBossPatternSelectRandomValue()
    {
        BossPatternSelectRandomValue = Random.value;
    }

    public Vector3 GetLaneAttackPosition(int laneIndex)
    {
        Vector3 lanePosition = Lane.LaneIndexToPosition(laneIndex);

        bool isYFlipped = Mathf.Approximately(Mathf.Repeat(transform.eulerAngles.y, 360f), 180f);

        float fixedX = isYFlipped ? lanePosition.x : -lanePosition.x;

        return new Vector3(fixedX / transform.localScale.x, 
            lanePosition.y / transform.localScale.y, 
            lanePosition.z / transform.localScale.z);
    }
    
    public bool PlayAnimation(string animationName)
    {
        if (animator is null)
        {
            Debug.Assert(false, "Animator is null");
            
            return false;
        }
        
        int stringNameHash = Animator.StringToHash(animationName);

        bool exists = false;
        foreach (var param in animator.parameters)
        {
            if (param.nameHash == stringNameHash)
            {
                exists = true;
                
                break;
            }
        }

        if (!exists)
        {
            Debug.Assert(false, $"Animation name '{animationName}' not exists in animator.");
            
            return false;
        }

        animator.SetTrigger(stringNameHash);
        
        return true;
    }
}