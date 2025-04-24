using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPriorityComparer : Comparer<SkillPriorityItem>
{
    public override int Compare(SkillPriorityItem x, SkillPriorityItem y)
    {
        //������ �� ū Item�� ����
        //���� ���������� ���� ���� Item
        if (x.skill.Level != y.skill.Level)
        {
            return -(x.skill.Level - y.skill.Level);
        }
        else
        {
            return x.priority.CompareTo(y.priority);
        }
    }
}

