using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceSupportSkill : SupportSkill
{
    private ExperienceStatus targetStatus;

    public ExperienceSupportSkill(SupportSkillData supportSkillData):base(supportSkillData)
    {
    }

    public override void Perform(AttackPowerStatus attacker, DamageableStatus target, Transform start, Transform destination = null)
    {
        base.Perform(attacker, target, start);

        targetStatus = attacker.gameObject.GetComponent<ExperienceStatus>();
        targetStatus.AddAdditionalExperienceRateValue(SupportSkillData.rate);
    }

    public override void UpgradeLevel()
    {
        if (Level >= ISkill.maxLevel)
        {
            return;
        }

        targetStatus.AddAdditionalExperienceRateValue(-SupportSkillData.rate);
        base.UpgradeLevel();
        targetStatus.AddAdditionalExperienceRateValue(SupportSkillData.rate);
    }
}
