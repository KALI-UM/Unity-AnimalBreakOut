using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelectionSystem
{
    private SkillManager skillManager;
    private List<ISkill> skills;

    private List<List<ISkill>> skillTypes = new();
    private List<int> canSelectSkillCount = new();

    public Action<List<ISkill>> onSkillListUpdated;

    //없는 스킬 LV0, 있는 스킬 LV1~maxLV
    private Dictionary<string, int> selectableSkillGroupTable = new();

    public SkillSelectionSystem(SkillManager manager, List<ISkill> skills)
    {
        skillManager = manager;
        this.skills = skills;

        foreach (SkillType type in Enum.GetValues(typeof(SkillType)))
        {
            foreach (var item in skillManager.SkillFactory.GetSkillGroupKeys(type))
            {
                selectableSkillGroupTable.Add(item, 0);
            }
            skillTypes.Add(new());
            canSelectSkillCount.Add(skillManager.GetSkillTypeMaxCount(type));
        }
    }

    public bool IsSkillExist(ISkill skill)
    {
        return IsSkillExist(skill.Id);
    }

    public bool IsSkillExist(int skillId)
    {
        return skills.Exists((target) => target.Id == skillId);
    }

    public bool IsSkillExist(string skillGroupName)
    {
        return skills.Exists((target) => target.SkillGroup == skillGroupName);
    }

    public bool TryGetSkill(string skillGroupName, out ISkill skill)
    {
        int index = skills.FindIndex((target) => target.SkillGroup == skillGroupName);
        if (index == -1)
        {
            skill = null;
            return false;
        }
        else
        {
            skill = skills[index];
            return true;
        }
    }

    public void AddSkill(int priority, ISkill skill)
    {
        var item = new SkillPriorityItem(priority, skill);
        skills.Add(skill);
        onSkillListUpdated?.Invoke(skills);

        skill.InitializeSkilManager(skillManager);
        if (skill.SkillData.skillType == SkillType.Attack)
        {
            AttackSkill attackSkill = skill as AttackSkill;
            attackSkill.AddOnReadyAction(() => skillManager.AddSkillToReadyQueue(item));
            attackSkill.OnReady();
        }
        else if (skill.SkillData.skillType == SkillType.Support)
        {
            skillManager.PerformSkill(skill);
        }


        selectableSkillGroupTable[skill.SkillGroup] = skill.Level;
    }

    public void AddSkill(int priority, SkillData skillData)
    {
        if (selectableSkillGroupTable[skillData.skillGroup] != 0)
        {
            //이미 존재하는 스킬은 업그레이드한다.
            TryGetSkill(skillData.skillGroup, out ISkill skill);
            AddUpgradedSkill(skill);
        }
        else
        {
            //새로 삽입하는 스킬
            AddNewSkill(priority, skillData);
        }
    }


    public void AddUpgradedSkill(ISkill skill)
    {
        UpgradeSkill(skill);
        if (skill.Level >= skillManager.SkillFactory.GetSkillMaxLevel(skill.SkillData.skillType, skill.SkillGroup))
        {
            canSelectSkillCount[(int)skill.SkillData.skillType]--;
        }

        onSkillListUpdated?.Invoke(skills);
    }

    private void AddNewSkill(int priority, SkillData skillData)
    {
        var skill = skillManager.SkillFactory.CreateSkill(skillData);
        var item = new SkillPriorityItem(priority, skill);
        skills.Add(skill);
        selectableSkillGroupTable[skillData.skillGroup] = skillData.level;
        onSkillListUpdated?.Invoke(skills);

        skill.InitializeSkilManager(skillManager);

        if (skillData.skillType == SkillType.Attack)
        {
            AttackSkill attackSkill = skill as AttackSkill;
            attackSkill.AddOnReadyAction(() => skillManager.AddSkillToReadyQueue(item));
            attackSkill.OnReady();

        }
        else if (skillData.skillType == SkillType.Support)
        {
            skillManager.PerformSkill(skill);
        }

        skillTypes[(int)skillData.skillType].Add(skill);
    }

    public void UpgradeSkill(ISkill skill)
    {
        skill.UpgradeLevel();
        selectableSkillGroupTable[skill.SkillGroup] = skill.Level;
    }

    public string GetRandomSkillGroup(SkillType type)
    {
        int randomIndex = UnityEngine.Random.Range(0, skillManager.SkillFactory.GetSkillGroupKeys(type).Count);
        string skillGroupName = skillManager.SkillFactory.GetSkillGroupKeys(type)[randomIndex];

        if (skillManager.SkillFactory.GetSkillData(type, skillGroupName, 1).selectPossible)
        {
            return skillGroupName;
        }
        else
        {
            return GetRandomSkillGroup(type);
        }

    }

    public string GetExistkillGroup(SkillType type)
    {
        int randomIndex = UnityEngine.Random.Range(0, skillTypes[(int)type].Count);
        string skillGroupName = skillTypes[(int)type][randomIndex].SkillGroup;

        return skillGroupName;
    }

    public void GetRandomSkillDatas(int count, List<SkillData> skillDatas)
    {
        //두개뿐이라 가능한 코드
        int attackCount = UnityEngine.Random.Range(0, Mathf.Min(count + 1, canSelectSkillCount[(int)SkillType.Attack]));
        int supportCount = count - attackCount;

        GetRandomSkillDatas(attackCount, SkillType.Attack, skillDatas);
        GetRandomSkillDatas(supportCount, SkillType.Support, skillDatas);
    }

    private void GetRandomSkillDatas(int count, SkillType type, List<SkillData> skillDatas)
    {
        if (skillTypes[(int)type].Count >= skillManager.GetSkillTypeMaxCount(type))
        {
            GetExistSkillDatas(count, type, skillDatas);
            return;
        }

        for (int i = 0; i < count; i++)
        {
            while (true)
            {
                string targetSkillGroup = GetRandomSkillGroup(type);

                int targetSkillLevel = selectableSkillGroupTable[targetSkillGroup];
                if (targetSkillLevel >= skillManager.SkillFactory.GetSkillMaxLevel(type, targetSkillGroup))
                {
                    continue;
                }

                if (skillDatas.FindIndex((skill) => skill.skillGroup == targetSkillGroup) != -1)
                {
                    continue;
                }

                var skillData = skillManager.SkillFactory.GetSkillData(type, targetSkillGroup, targetSkillLevel + 1);
                skillDatas.Add(skillData);
                break;
            }
        }
    }

    private void GetExistSkillDatas(int count, SkillType type, List<SkillData> skillDatas)
    {
        for (int i = 0; i < count; i++)
        {
            while (true)
            {
                string targetSkillGroup = GetExistkillGroup(type);

                int targetSkillLevel = selectableSkillGroupTable[targetSkillGroup];
                if (targetSkillLevel >= skillManager.SkillFactory.GetSkillMaxLevel(type, targetSkillGroup))
                {
                    continue;
                }

                if (skillDatas.FindIndex((skill) => skill.skillGroup == targetSkillGroup) != -1)
                {
                    continue;
                }

                var skillData = skillManager.SkillFactory.GetSkillData(type, targetSkillGroup, targetSkillLevel + 1);
                skillDatas.Add(skillData);
                break;
            }
        }
    }
}
