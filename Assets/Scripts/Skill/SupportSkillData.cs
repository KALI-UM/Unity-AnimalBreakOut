
using UnityEngine;

[CreateAssetMenu(menuName = "InGameAssets/SupportSkill Data")]

public class SupportSkillData : SkillData
{
    public SupportSkillTarget skillTarget;
    public float rate;

    public void SetData(SupportSkillRawData rawData)
    {
        skillType = SkillType.Support;

        skillID = rawData.SupportID;
        level = rawData.Level;
        skillGroup = rawData.SupportGroup;
        iconPrefab = rawData.Prefab;
        skillTarget = (SupportSkillTarget)rawData.SupportType;
        rate = rawData.Value;
    }
}