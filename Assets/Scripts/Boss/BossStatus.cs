using System;
using System.Collections;
using UnityEngine;

public class BossStatus : DamageableStatus
{
    public override float currentHp { get; protected set; }
    public override float maxHp { get; protected set; }
    public override bool isDead { get; protected set; }

    public static event Action onBossDead;
    public static event Action onBossDeathAnimationEnded;
    private readonly WaitForSeconds waitTime = new(3f);
    public static Action<int> onBossDeadCounting;

    public static event Action<float, float> onBossCurrentHpChanged;
    private static int BossKillCount = 0;
    
    private BossBehaviourController bossBehaviourController;

    public override void InitializeStatus(float maxHp)
    {
        this.maxHp = maxHp;
        currentHp = maxHp;
        isDead = false;

        TryGetComponent(out bossBehaviourController);
    }

    public override void OnDamage(float damage, SkillElemental attribute)
    {
        onElementalDamaged?.Invoke(damage, attribute);
        OnDamage(damage);
    }

    public override void OnDamage(float damage)
    {
        if (damage < 0)
        {
            Debug.Assert(false, "Invalid Damage value");

            return;
        }

        if (isDead)
        {
            return;
        }

        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0f, maxHp);
        onBossCurrentHpChanged?.Invoke(currentHp, maxHp);
        onDamaged?.Invoke(damage);

        Debug.Log($"Boss HP : {currentHp}/{maxHp}");

        if (Mathf.Approximately(0f, currentHp))
        {
            OnDead();
        }
    }
    
    protected override void OnDead()
    {
        isDead = true;

        onBossDead?.Invoke();
        onBossDeadCounting?.Invoke(1);
        BossKillCount++;
        Debug.Log(BossKillCount);

        var deathFunc = BossPatternFuncFactory.GetBossAttackPatternAction(BossAttackPatternActionType.BossDeathAnimation);
        deathFunc?.Invoke(bossBehaviourController);
        
        bossBehaviourController.ClearBossProjectile();
        
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        yield return waitTime;
        
        onBossDeathAnimationEnded?.Invoke();
        
        Destroy(gameObject);
    }
}