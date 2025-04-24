using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManagerTest : MonoBehaviour
{
    [SerializeField]
    private AttackSkillData fire;

    [SerializeField]
    private AttackSkillData ice;

    [SerializeField]
    private AttackSkillData electicity;

    [SerializeField]
    private SupportSkillData support;

    [SerializeField]
    private SkillManager skillManager;

    private SkillFactory skillFactory;

    private int priority = 0;

    private void Start()
    {
        
        skillManager.SkillSelectionSystem.AddSkill(priority++, fire);

    }

    public void AddSkill()
    {
        //var skill = skillFactory.CreateSkill(fire);
        //skillManager.SkillSelectionSystem.AddSkill(priority++, skill);

        var skill1 = skillFactory.CreateSkill(ice);
        skillManager.SkillSelectionSystem.AddSkill(priority++, skill1);
    }

}
