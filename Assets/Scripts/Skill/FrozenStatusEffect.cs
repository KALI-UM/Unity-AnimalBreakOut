using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenStatusEffect : StatusEffect
{
    private readonly int effectId = 1602;
    private DamageableStatus target;

    private int currentCount;

    public override bool CanPerform
    {
        get => !IsPerforming;
    }
    public override bool IsPerforming
    {
        get => isPerforming;
    }
    private bool isPerforming;
    private int attackPower;

    private void Start()
    {
        SetEffectData(effectId, SkillElemental.Ice);
        SetDamagerableTarget(GetComponent<DamageableStatus>());
        isPerforming = false;
    }

    public override void SetDamagerableTarget(DamageableStatus damageable)
    {
        target = damageable;
        target.onElementalDamaged += PerformFrozenEffect;
    }

    //냉기 상태에서는 얼음 속성을 제외한 다른 속성 스킬로 공격을 했을 때 얼음 속성 1회 추가 데미지를 주는 효과를 갖고 있다.
    public override void Perform(int skillID, int elementalAttackPower)
    {
        if (CanPerform)
        {
            isPerforming = true;
            currentCount = 0;
            attackPower = elementalAttackPower;
        }
    }

    private void PerformFrozenEffect(float damage, SkillElemental attribute)
    {
        if (attribute == Elemental || !IsPerforming)
        {
            return;
        }

        currentCount++;

        Debug.Log($"얼음 효과 damage : {attackPower *AdditionalStatusEffectData.Damage}");
        target.OnDamage(attackPower * AdditionalStatusEffectData.Damage);

        if (currentCount >= AdditionalStatusEffectData.AttackCount)
        {
            isPerforming = false;
        }
        // debuffUI.RemoveDebuff("Freeze");
    }
}
