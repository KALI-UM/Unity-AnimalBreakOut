
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "InGameAssets/SupportSkill Data")]

public class SupportSkillData : SkillData
{
    public SupportSkillTarget skillTarget;
    public float rate;

#if UNITY_EDITOR
    private string iconPath = "Assets/Resources/SkillIcon/{0}.png";

    public void SetData(SupportSkillRawData rawData)
    {
        skillType = SkillType.Support;
        selectPossible = rawData.SelectPossible;

        skillID = rawData.SupportID;
        level = rawData.Level;
        skillGroup = rawData.SupportGroup;

        var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(string.Format(iconPath, rawData.Prefab));
        iconImage = sprite;
        skillTarget = (SupportSkillTarget)rawData.SupportType;
        rate = rawData.Value;
    }
#endif
}