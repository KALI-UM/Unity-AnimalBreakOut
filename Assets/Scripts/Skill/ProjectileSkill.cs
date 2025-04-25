using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileSkill : AttackSkill
{
    private float lastPerformedTime = 0;

    public ProjectileSkill(AttackSkillData attackSkillData) : base(attackSkillData)
    {

    }


    public override void Perform(Transform attackerTrs, Transform targetTrs, AttackPowerStatus attacker, DamageableStatus target)
    {
        if (!skillManager.IsSkillTargetValid())
        {
            return;
        }
        //간격이라던지 그런건 일단 차치하고 작성
        for (int i = 0; i < AttackSkillData.projectileCount; i++)
        {
            var projectile = UnityEngine.Object.Instantiate(AttackSkillData.projectileBehaviourPrefab.gameObject, skillManager.transform).GetComponent<ProjectileBehaviour>();
            projectile.InitializeSkilManager(skillManager);
            if (i != AttackSkillData.elementalEffectAttackIndex)
            {
                projectile.onArrival += () => ApplyOnlyDamage(attacker, target, AttackSkillData.attackCount);
            }
            else
            {
                projectile.onArrival += () => ApplyDamageAndElementalEffect(attacker, target, AttackSkillData.attackCount);
            }
            projectile.Fire(attackerTrs, targetTrs, AttackSkillData.speed);
        }

        lastPerformedTime = Time.time;
        CoolTime = 0f;
    }

    public void ApplyOnlyDamage(AttackPowerStatus attacker, DamageableStatus target, int count)
    {
        if (!skillManager.IsSkillTargetValid())
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            AttackDamage(attacker.GetElementalAdditionalAttackPower(AttackSkillData.skillElemental) * AttackSkillData.damageRate, target);
        }
    }

    public void ApplyDamageAndElementalEffect(AttackPowerStatus attacker, DamageableStatus target, int count)
    {
        if (!skillManager.IsSkillTargetValid())
        {
            return;
        }

        ApplyElementalEffect(attacker,target, AttackSkillData.skillElemental);
        for (int i = 0; i < count; i++)
        {
            AttackDamage(attacker.GetElementalAdditionalAttackPower(AttackSkillData.skillElemental) * AttackSkillData.damageRate, target);
        }
    }
}
