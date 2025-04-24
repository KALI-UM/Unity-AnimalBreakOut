using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private TextMeshProUGUI levelText;

    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private Image fillTimerImage;

    private ISkill targetSkill;
    private AttackSkill attackSkill;


    private void Update()
    {
        if (targetSkill.SkillData.skillType == SkillType.Attack)
        {
            UpdateFillTimer(attackSkill.CoolTimeRatio, attackSkill.CoolDownRemaining);
        }
    }

    public void SetTargetSkill(ISkill skill)
    {
        targetSkill = skill;
        levelText.text = skill.Level.ToString();

        if (targetSkill.SkillData.skillType == SkillType.Attack)
        {
            attackSkill = skill as AttackSkill;
        }
        else if (targetSkill.SkillData.skillType == SkillType.Support)
        {
            UpdateFillTimer(1f, 0f);
        }
    }

    //private static readonly string timeFormatString = $"{}";
    private void UpdateFillTimer(float ratio, float time)
    {
        if (time <= 0)
        {
            //숫자 0일때는 text안뜨게함
            timerText.gameObject.SetActive(false);
        }
        else
        {
            timerText.gameObject.SetActive(true);
            timerText.text = time.ToString("F1");

        }
        fillTimerImage.fillAmount = 1f - ratio;
    }
}
