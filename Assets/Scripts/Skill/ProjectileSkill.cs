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

    public override void Perform(AttackPowerStatus attacker, DamageableStatus target, Transform start, Transform destination)
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
            projectile.Fire(start, destination, AttackSkillData.speed);
        }

        lastPerformedTime = Time.time;
        CoolTime = 0f;
    }

    public override IEnumerator coPerform(AttackPowerStatus attacker, DamageableStatus target, Transform start, Transform destination)
    {
        for (int i = 0; i < AttackSkillData.projectileCount; i++)
        {
            if (i != 0)
            {
                yield return new WaitForSeconds(AttackSkillData.interval);
            }

            if (!skillManager.IsSkillTargetValid())
            {
                yield break;
            }

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

            projectile.Fire(start, destination, AttackSkillData.speed);
        }

        lastPerformedTime = Time.time;
        CoolTime = 0f;
    }


}
