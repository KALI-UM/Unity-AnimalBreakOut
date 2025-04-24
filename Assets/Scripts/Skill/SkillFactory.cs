using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillFactory
{
    private Dictionary<string, SkillDataLevelGroup> skillDataTable = new();

    public IReadOnlyList<string> SkillGroupKeys
    {
        get => skillListGroupKeys;
    }
    private List<string> skillListGroupKeys;

    public int SkillGroupCount
    {
        get => SkillGroupKeys.Count;
    }


    public class SkillDataLevelGroup
    {
        public SkillDataLevelGroup()
        {
            skillDatas = new();
        }
        public void AddToGroup(SkillData skillData)
        {
            skillDatas.Add(skillData);
        }

        public void LevelSort()
        {
            skillDatas.Sort((a, b) => a.level.CompareTo(b.level));
        }

        public SkillData GetSkill(int level)
        {
            return skillDatas[level - 1];
        }

        private List<SkillData> skillDatas;
    }


    public SkillFactory()
    {
        InitializeSkillData();
    }

    private void InitializeSkillData()
    {
        //Resources폴더에 있는 스킬데이터들을 가져온다
        AttackSkillData[] attackSkillDatas = Resources.LoadAll<AttackSkillData>("ScriptableData/Skill/");
        SupportSkillData[] supportSkillDatas = Resources.LoadAll<SupportSkillData>("ScriptableData/Skill/");

        foreach (var skillData in supportSkillDatas)
        {
            if (skillDataTable.ContainsKey(skillData.skillGroup))
            {
                skillDataTable[skillData.skillGroup].AddToGroup(skillData);
            }
            else
            {
                var newGroup = new SkillDataLevelGroup();
                newGroup.AddToGroup(skillData);
                skillDataTable.Add(skillData.skillGroup, newGroup);
            }
        }

        foreach (var skillData in attackSkillDatas)
        {
            if (skillDataTable.ContainsKey(skillData.skillGroup))
            {
                skillDataTable[skillData.skillGroup].AddToGroup(skillData);
            }
            else
            {
                var newGroup = new SkillDataLevelGroup();
                newGroup.AddToGroup(skillData);
                skillDataTable.Add(skillData.skillGroup, newGroup);
            }
        }

        //데이터가 항상 순서대로 있을거라 확신할 수 없어 한번 정렬
        foreach (var item in skillDataTable)
        {
            item.Value.LevelSort();
        }

        skillListGroupKeys = skillDataTable.Keys.ToList();
    }

    public SkillData GetSkillData(string skillGroup, int level)
    {
        return skillDataTable[skillGroup].GetSkill(level);
    }

    public SkillData GetSkillData(int index, int level)
    {
        return GetSkillData(SkillGroupKeys[index], level);
    }


    public ISkill CreateSkill(SkillData skillData)
    {
        if (skillData.skillType == SkillType.Attack)
        {
            return CreateAttackSkill(skillData as AttackSkillData);

        }
        else if (skillData.skillType == SkillType.Support)
        {
            return CreateSupportSkill(skillData as SupportSkillData);
        }
        else
        {
            return null;
        }
    }

    public ProjectileSkill CreateAttackSkill(AttackSkillData skillData)
    {
        return new ProjectileSkill(skillData);
    }

    public SupportSkill CreateSupportSkill(SupportSkillData skillData)
    {
        switch (skillData.skillTarget)
        {
            case SupportSkillTarget.AttackPower:
                {
                    return new AttackPowerSupportSkill(skillData);
                }
            case SupportSkillTarget.CoolDownTime:
                {
                    return new CoolDownSupportSkill(skillData);
                }
            case SupportSkillTarget.Experience:
                {
                    return new ExperienceSupportSkill(skillData);
                }

            case SupportSkillTarget.ElementalFirePower:
            case SupportSkillTarget.ElementalIcePower:
            case SupportSkillTarget.ElementalThunderPower:
                {
                    return new ElementalAttackPowerSupportSkill(skillData);
                }

        }
        return new AttackPowerSupportSkill(skillData);
    }

}
