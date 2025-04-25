using System;
using UnityEngine;

public abstract class AttackSkill : ISkill
{
    public SkillData SkillData
    {
        get => AttackSkillData;
    }

    public AttackSkillData AttackSkillData
    {
        get;
        private set;
    }

    public bool IsReady
    {
        get => (CoolTime >= AttackSkillData.coolDownTime);
    }

    #region cooldownTime

    public float CoolTime
    {
        get;
        protected set;
    }

    public float CoolDownRemaining
    {
        get => CoolDownTime - CoolTime;
    }

    public float CoolTimeRatio
    {
        get => Mathf.Clamp01(CoolTime / CoolDownTime);
    }

    public float CoolDownTime
    {
        get => AttackSkillData.coolDownTime - AttackSkillData.coolDownTime * skillManager.GlobalCoolDownTimeRate;
    }

    #endregion

    protected Action onReady;

    public int Id
    {
        get => SkillData.skillID;
    }

    public string SkillGroup
    {
        get => SkillData.skillGroup;
    }

    public int Level
    {
        get => SkillData.level;
    }

    protected SkillManager skillManager;
    public void InitializeSkilManager(SkillManager skillManager)
    {
        this.skillManager = skillManager;
        CoolTime = CoolDownTime;
    }

    public AttackSkill(AttackSkillData attackSkillData)
    {
        AttackSkillData = attackSkillData;
    }

    public abstract void Perform(Transform attackerTrs, Transform targetTrs, AttackPowerStatus attacker, DamageableStatus target);

    protected virtual void AttackDamage(float damage, DamageableStatus target)
    {
        target.OnDamage(damage, AttackSkillData.skillElemental);
    }

    protected void ApplyElementalEffect(AttackPowerStatus attacker, DamageableStatus target, SkillElemental elemental)
    {
        var ui = skillManager.gameManager.UIManager.bossDebuffUI;
        string debuffId = null;

        switch (elemental)
        {
            case SkillElemental.Fire:
                var burn = target.GetComponent<BurnStatusEffect>();
                burn?.SetDebuffUI(ui);
                burn?.Perform(Id);
                debuffId = "Burn";
                break;

            case SkillElemental.Ice:
                var freeze = target.GetComponent<FrozenStatusEffect>();
                freeze?.SetDebuffUI(ui);
                freeze?.Perform(Id);
                debuffId = "Freeze";
                break;

            case SkillElemental.Thunder:
                var shock = target.GetComponent<ElectricShockStatusEffect>();
                shock?.SetDebuffUI(ui);
                shock?.Perform(Id);
                debuffId = "Thunder";
                break;
        }
        ShowDebuffIcon(debuffId, SkillData.iconImage);
    }

    private void ShowDebuffIcon(string debuffId, Sprite icon)
    {
        var gameManager = skillManager.gameManager;
        if (gameManager != null && gameManager.StageManager.IsPlayerInBossStage)
        {
            gameManager.UIManager.bossDebuffUI.AddDebuff(debuffId);
        }
    }



    public void Update()
    {
        UpdateCoolTime();
    }

    public void UpgradeLevel()
    {
        var nextSkillData = skillManager.SkillFactory.GetSkillData(SkillData.skillType, SkillData.skillGroup, Level + 1);
        AttackSkillData = nextSkillData as AttackSkillData;
    }

    private void UpdateCoolTime()
    {
        if (CoolTimeRatio < 1f)
        {
            CoolTime += Time.deltaTime;

            if (CoolTimeRatio >= 1f)
            {
                CoolTime = Mathf.Clamp(CoolTime, 0, AttackSkillData.coolDownTime);
                OnReady();
            }
        }
    }

    public virtual void AddOnReadyAction(Action onReady)
    {
        this.onReady += onReady;
    }

    public virtual void OnReady()
    {
        onReady?.Invoke();
    }
}
