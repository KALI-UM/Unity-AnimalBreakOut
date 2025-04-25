using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnStatusEffect : StatusEffect
{
    private readonly int effectId = 1601;

    private DamageableStatus target;

    //임시
    public float effectInterval = 2f;

    private Coroutine coPreformBurnEffect = null;

    private int previousSkillId = -1;
    public override bool CanPerform
    {
        get => true;
    }

    public override bool IsPerforming
    {
        get => isPerforming;
    }
    private bool isPerforming;

    private void Start()
    {
        SetEffectData(effectId, SkillElemental.Fire);
        SetDamagerableTarget(GetComponent<DamageableStatus>());
    }

    public override void SetDamagerableTarget(DamageableStatus damageable)
    {
        target = damageable;
    }

    private bool CanPerformID(int skillID)
    {
        return previousSkillId != skillID;
    }

    public override void Perform(int skillID, int elementalAttackPower)
    {
        if (!CanPerformID(skillID))
        {
            return;
        }

        previousSkillId = skillID;

        if (coPreformBurnEffect != null)
        {
            StopCoroutine(coPreformBurnEffect);
        }

        isPerforming = true;

        coPreformBurnEffect = StartCoroutine(CoPerformBurnEffect());


    }


    private IEnumerator CoPerformBurnEffect(int damage)
    {
        for (int i = 0; i < AdditionalStatusEffectData.AttackCount; i++)
        {
            Debug.Log($"화상 효과 damage {i}번째 : {damage*AdditionalStatusEffectData.Damage}");

            target.OnDamage(damage * AdditionalStatusEffectData.Damage);
            yield return new WaitForSeconds(effectInterval);
        }
        isPerforming = false;
        // debuffUI.RemoveDebuff("Burn");
    }
}
