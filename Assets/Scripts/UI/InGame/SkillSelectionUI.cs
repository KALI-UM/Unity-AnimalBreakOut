using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSelectionUI : UIElement
{
    private int skillButtonCount = 3;

    [SerializeField]
    private GameObject skillButtonPrefab;

    [SerializeField]
    private GameObject skillListGameObject;

    private List<SkillButton> skillButtons = new();
    private List<SkillData> skillDatas = new();
    [SerializeField] private Button rerollButton;

    [SerializeField] private TMP_Text rerollCountText;
    private int rerollCount;

    private int priority = 1;

    public override void Initialize()
    {
        base.Initialize();

        rerollCount = gameManager.SkillManager.SkillSelectionRerollMaxCount;

        for (int i = 0; i < skillButtonCount; i++)
        {
            var skillButton = Instantiate(skillButtonPrefab, skillListGameObject.transform).GetComponent<SkillButton>();
            skillButtons.Add(skillButton);

            var index = i;
            skillButton.InitializeButtonAction(() =>
            {
                SelectSkill(index);
                SoundManager.Instance.PlaySfx(SfxClipId.SkillSelect);
            });
        }
        gameObject.SetActive(false);

        rerollButton.onClick.RemoveAllListeners();
        // rerollButton.onClick.AddListener(OnRerollButtonClicked);
        rerollButton.onClick.AddListener(() =>
             {
                 OnRerollButtonClicked();
                 SoundManager.Instance.PlaySfx(SfxClipId.SkillReroll);
             }); ;
        UpdateRerollUI();
    }

    public override void Show()
    {
        base.Show();

        gameObject.SetActive(true);
        UpdateRandomSkillDatas();
    }

    private void UpdateRandomSkillDatas()
    {
        skillDatas.Clear();
        gameManager.SkillManager.SkillSelectionSystem.GetRandomSkillDatas(skillButtonCount, skillDatas);

        for (int i = 0; i < skillButtonCount; i++)
        {
            skillButtons[i].UpdateSkillButton(skillDatas[i]);
        }
    }

    private void SelectSkill(int index)
    {
        gameManager.SkillManager.SkillSelectionSystem.AddSkill(priority++, skillDatas[index]);

        gameObject.SetActive(false);
        gameManager.RestartGameState();
        var playerStatus = gameManager.PlayerManager.playerStatus;
        playerStatus.SetInvincible(true);

        gameManager.PlayerManager.StartCoroutine(playerStatus.DisableInvincibilityAfterDelay(playerStatus, 1f));

    }

    private void OnRerollButtonClicked()
    {
        if (rerollCount <= 0) return;

        rerollCount--;
        UpdateRandomSkillDatas();
        UpdateRerollUI();
    }

    private void UpdateRerollUI()
    {
        rerollCountText.text = $"{rerollCount}";
        rerollButton.interactable = rerollCount > 0;
    }

}
