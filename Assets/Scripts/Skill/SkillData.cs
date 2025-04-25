using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SkillType
{
    Attack,
    Support,
}

public enum SkillElemental
{
    None,
    Fire,
    Ice,
    Thunder,
}

public enum SupportSkillTarget
{
    None,
    Experience,
    AttackPower,
    CoolDownTime,
    ElementalStart,
    ElementalFirePower,
    ElementalIcePower,
    ElementalThunderPower,
}


public class SkillData : ScriptableObject
{
    public int skillID;
    public SkillType skillType;
    public int level;
    public string skillGroup;
    public Sprite iconImage;

    public bool selectPossible;
}



