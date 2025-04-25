
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "InGameAssets/AttackSkill Data")]
public class AttackSkillData : SkillData
{
    public SkillElemental skillElemental;
    public float damageRate;
    public int projectileCount;
    public int attackCount;
    public float coolDownTime;
    public float speed;
    public float interval;
    public int effectID;


    public int elementalEffectAttackIndex = 0;
    public ProjectileBehaviour projectileBehaviourPrefab;

#if UNITY_EDITOR
    private string prefabPath = "Assets/Resources/Prefab/Skill/Projectile{0}.prefab";
    private string iconPath = "Assets/Resources/SkillIcon/{0}.png";
    public void SetData(AttackSkillRawData rawData)
    {
        skillType = SkillType.Attack;
        selectPossible = rawData.SelectPossible;

        skillID = rawData.SkillID;
        level = rawData.SkillLevel;
        skillGroup = rawData.SkillGroup;
        skillElemental = (SkillElemental)rawData.Attribute;
        damageRate = rawData.Damage;
        projectileCount = rawData.ProjectileCount;
        attackCount = rawData.AttackCount;
        speed = rawData.Speed;
        interval = rawData.Interval;
        coolDownTime = rawData.CoolTime;
        effectID = rawData.EffectID;

        var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(string.Format(iconPath, rawData.Prefab_Icon));
        iconImage = sprite;

        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(string.Format(prefabPath, skillElemental.ToString()));
        var projectile = prefab.GetComponent<ProjectileBehaviour>();
        projectileBehaviourPrefab = projectile;
    }
#endif
}