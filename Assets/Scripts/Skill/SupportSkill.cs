using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SupportSkill : ISkill
{
    public SkillData SkillData
    {
        get => SupportSkillData;
    }

    public SupportSkillData SupportSkillData
    {
        get;
        private set;
    }

    public bool IsReady
    {
        get => true;
    }

    //public float CoolTime
    //{
    //    get => 0f;
    //}

    //public float CoolDownRemaining
    //{
    //    get => 0f;
    //}

    //public float CoolTimeRatio
    //{
    //    get => 1f;
    //}

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
    }

    public SupportSkill(SupportSkillData supportSkillData)
    {
        SupportSkillData = supportSkillData;
    }

    public virtual void Perform(Transform attackerTrs, Transform targetTrs, AttackPowerStatus attacker = null, DamageableStatus target = null)
    {
        Debug.Log($"{SupportSkillData.skillTarget} : rate {SupportSkillData.rate * 100f}%");
    }

    public virtual void Update()
    {
        
    }

    public virtual void UpgradeLevel()
    {
        var nextSkillData = skillManager.SkillFactory.GetSkillData(SkillData.skillGroup, Level + 1);
    }

}
