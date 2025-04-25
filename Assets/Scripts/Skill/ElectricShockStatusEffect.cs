using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricShockStatusEffect : StatusEffect
{
    private readonly int effectId = 1603;

    [SerializeField]
    private SkillData effectSkillData;
    private ISkill effectSkill;
    private DamageableStatus target;

    public int stackMaxCount = 6;
    private int currentStackCount;


    public override bool CanPerform
    {
        get => currentStackCount >= stackMaxCount;

        get => !isPerforming;

    }

    public override bool IsPerforming
    {
        get => isPerforming;
    }
    private bool isPerforming;

    private void Start()
    {
        SetEffectData(effectId, SkillElemental.Thunder);
        SetDamagerableTarget(GetComponent<DamageableStatus>());
        isPerforming = false;
        currentStackCount = 0;
    }

    public override void InitializeSkillManager(SkillManager skillManager)
    {
        base.InitializeSkillManager(skillManager);
        effectSkill = skillManager.SkillFactory.CreateSkill(effectSkillData);
    }

    //public void SetEffectSkill(ISkill skill)
    //{
    //    effectSkill = skill;
    //}

    public override void SetDamagerableTarget(DamageableStatus damageable)
    {
        target = damageable;
        target.onElementalDamaged += AddElectricStack;
    }

    //스택은 번개 속성 스킬을 맞췄을 때 1스택씩 쌓이고 6스택을 쌓으면 번개 속성 타겟팅 추가 스킬이 발동된다.
    public override void Perform(int skillID, int elementalAttackPower)
    {
        if (CanPerform)
        {
            isPerforming = true;
            currentStackCount = 0;
        }
    }

    private void AddElectricStack(float damage, SkillElemental elemental)
    {
        if (elemental != Elemental || !IsPerforming)
        {
            return;
        }

        currentStackCount++;
        Debug.Log($"전기스택 {currentStackCount}번째");
        if (currentStackCount >= stackMaxCount)
        {
            PerformEffectSkill();
        }
    }

    private void PerformEffectSkill()
    {
        isPerforming = false;
        // debuffUI.RemoveDebuff("Thunder");
        Debug.Log($"전기 효과 damage : {damage}");
        target.OnDamage(damage);

        //skillManager.PerformSkill(effectSkill);

    }

}
