using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceStatus : MonoBehaviour, IItemTaker
{
    public int level = 1;
    private int maxLevel = 20;

    public float ExperienceRatio
    {
        get => Mathf.Clamp01(ExperienceValue / experienceToNextLevel);
    }

    public float ExperienceValue
    {
        get;
        private set;
    } = 0;

    public bool IsMaxLevel
    {
        get => level >= maxLevel;
    }

    private float additionalExperienceRate = 0f;

    //임시
    private int experienceToNextLevel = 10;

    public Action<int, int> onLevelChange; //level, maxexp
    public Action<int, int> onAddValue; //add, sum

    private void Start()
    {
        InitializeValue();
    }

    public void InitializeValue()
    {
        onLevelChange(level, experienceToNextLevel);
        onAddValue(0, (int)ExperienceValue);
    }

    public void AddAdditionalExperienceRateValue(float value)
    {
        additionalExperienceRate += value;
    }

    public void AddValue(int value)
    {
        if (IsMaxLevel)
        {
            return;
        }

        ExperienceValue += value + value * additionalExperienceRate;


        if (ExperienceValue >= experienceToNextLevel)
        {
            ExperienceValue -= experienceToNextLevel;
            LevelUp();
        }
        onAddValue?.Invoke(value, (int)ExperienceValue);
    }

    private void LevelUp()
    {
        level = Mathf.Clamp(level + 1, 1, maxLevel);
        onLevelChange?.Invoke(level, experienceToNextLevel);
    }

    public void ApplyItem(int value)
    {
        AddValue(value);
    }
}
