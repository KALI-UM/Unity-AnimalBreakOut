using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public abstract void SetDamagerableTarget(DamageableStatus damageable);


    protected BossDebuffUIController debuffUI;

    public void SetDebuffUI(BossDebuffUIController ui)
    {
        debuffUI = ui;
    }

    public SkillElemental Elemental
    {
        get;
        private set;
    }


    public AdditionalStatusEffectData AdditionalStatusEffectData
    {
        get;
        private set;
    }

    public abstract bool CanPerform
    {
        get;
    }

    public abstract bool IsPerforming
    {
        get;
    }

    public void SetEffectData(int effectId, SkillElemental skillElemental)
    {
        Elemental = skillElemental;
        AdditionalStatusEffectData = DataTableManager.additionalStatusEffectDataTable.Get(effectId);
    }

    protected SkillManager skillManager;
    public virtual void InitializeSkillManager(SkillManager skillManager)
    {
        this.skillManager = skillManager;
    }

    public abstract void Perform(int skillID, int elementalAttackPower);
}
