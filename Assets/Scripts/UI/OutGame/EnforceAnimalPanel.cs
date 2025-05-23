using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class EnforceAnimalPanel : MonoBehaviour
{
    private EnforceAnimalManager enforceAnimalManager;
    private OutGameUIManager outGameUIManager;
    private bool isFindOtherComponent = false;

    public AnimalUserData animalUserData { get; private set; }

    [SerializeField] private Button enforceButton;
    public Button EnforceButton => enforceButton;

    [SerializeField] private Image animalImage;
    [SerializeField] private Image starImage;
    [SerializeField] private Image tokenImage;
    [SerializeField] private Image goldImage;

    [SerializeField] private TMP_Text animalNameText;

    [SerializeField] private TMP_Text attackPowerText;

    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text skillText;
    [SerializeField] private TMP_Text passiveText;
    [SerializeField] private TMP_Text requiredTokenText;
    [SerializeField] private TMP_Text enforceText;
    [SerializeField] private TMP_Text goldCostText;

    private TokenType requiredTokenType;
    private int requiredTokenCount;
    private long requiredGoldCount;
    
    [SerializeField] DetectTouchInOtherUIScreenDoHideAllAlertPanel detectTouchInOtherUIScreenDoHideAllAlertPanel;
    
    public static event Action<TokenType ,int, long, EnforceAnimalPanel> onEnforceInfoSet;

    private void Start()
    {
        enforceButton.onClick.AddListener(() =>
        {
            onEnforceInfoSet?.Invoke(requiredTokenType, requiredTokenCount, requiredGoldCount, this);

            outGameUIManager.ShowAlertDoubleButtonPanel(AlertPanelInfoDataFactory.GetAlertPanelInfoData(AlertPanelInfoDataType.EnforceAnimal));

            enforceButton.interactable = false;
        });
    }

    private void OnDestroy()
    {
        enforceButton.onClick.RemoveAllListeners();
    }
    
    public void SetTargetAnimalUserData(AnimalUserData animalUserData)
    {
        if (!isFindOtherComponent)
        {
            GameObject.FindGameObjectWithTag("OutGameManager").TryGetComponent(out OutGameManager outGameManager);
            enforceAnimalManager = outGameManager.EnforceAnimalManager;
            outGameUIManager = outGameManager.OutGameUIManager;
            
            isFindOtherComponent = true;
        }
        
        this.animalUserData = animalUserData;
        
        if (!animalUserData.IsMaxLevel)
        {
            //공격력, 비용 텍스트를 업데이트한다
            enforceAnimalManager.ExpectedEnforcedAnimalUserData(animalUserData.AnimalStatData, animalUserData.Level + 1, out int enforcedAttackPower, out int tokenCost, out int goldCost);
            bool isPossible = enforceAnimalManager.IsEnforceAnimalPossible(animalUserData, out bool hasEnoughTokens, out bool hasEnoughGolds);
           
            enforceButton.interactable = isPossible;
            
            SetAttackPowerText(animalUserData.AttackPower, enforcedAttackPower);
            
            SetByRequiredToken(GameDataManager.Instance.GoldAnimalTokenKeySystem.GetCurrentToken(animalUserData.AnimalStatData.Grade), tokenCost, hasEnoughTokens);
            
            SetByGoldCost(goldCost, hasEnoughGolds);
            
            requiredTokenType = (TokenType)animalUserData.AnimalStatData.Grade;
            requiredTokenCount = tokenCost;
            requiredGoldCount = goldCost;
        }
        else
        {
            SetAttackPowerText(animalUserData.AttackPower, 0);
            
            SetByRequiredToken(GameDataManager.Instance.GoldAnimalTokenKeySystem.GetCurrentToken(animalUserData.AnimalStatData.Grade), 0, false);
            
            SetByGoldCost(0, false);
            
            enforceButton.interactable = false;
        }

        SetAnimalImage(animalUserData.AnimalStatData.iconImage);
        SetEnforceText(LocalizationUtility.GetLZString(LocalizationUtility.defaultStringTableName, Utils.AnimalUpgradeStringKey));
        SetPassiveText(DataTableManager.passiveEffectDataTable.GetPassiveEffectData((int)animalUserData.AnimalStatData.passive, animalUserData.AnimalStatData.Grade, animalUserData.Level), animalUserData.Level);
        SetAnimalNameText(LocalizationUtility.GetLZString(LocalizationUtility.defaultStringTableName, animalUserData.AnimalStatData.StringID));
        SetLevelText(animalUserData.Level, animalUserData.Level + 1);
        SetSkillText();
        SetStarImage(animalUserData.AnimalStatData.starIconImage);
        SetTokenImage(animalUserData.AnimalStatData.tokenIconImage);
    }
    
    public void SetAnimalNameText(string text)
    {
        animalNameText.text = text;
    }

    public void SetAttackPowerText(int power, int enforcedAttackPower)
    {
        if (!animalUserData.IsMaxLevel)
        {
            attackPowerText.text = $"{LocalizationUtility.GetLZString(LocalizationUtility.defaultStringTableName, Utils.AnimalAttackPowerStringKey)}   {power} -> {enforcedAttackPower}";
        }
        else
        {
            attackPowerText.text = $"{LocalizationUtility.GetLZString(LocalizationUtility.defaultStringTableName, Utils.AnimalAttackPowerStringKey)}   {power}(MAX)";
        }
    }

    public void SetLevelText(int level, int nextLevel)
    {
        if (!animalUserData.IsMaxLevel)
        {
            levelText.text = $"{LocalizationUtility.GetLZString(LocalizationUtility.defaultStringTableName, Utils.AnimalLevelStringKey)}   Lv.{level} -> Lv.{nextLevel}";
        }
        else
        {
            levelText.text = $"{LocalizationUtility.GetLZString(LocalizationUtility.defaultStringTableName, Utils.AnimalLevelStringKey)}   Lv.{level}(MAX)";
        }
    }

    public void SetSkillText()
    {
        skillText.text = $"{LocalizationUtility.GetLZString(LocalizationUtility.defaultStringTableName, Utils.AnimalSkillStringKey)}   {LocalizationUtility.GetLZString(LocalizationUtility.defaultStringTableName, animalUserData.AnimalStatData.SkillData.nameID, animalUserData.AnimalStatData.SkillData.level)}";
    }

    public void SetPassiveText(PassiveEffectData passiveEffectData, int level)
    {
        float value = (PassiveType)passiveEffectData.PassiveType is PassiveType.SkillDamage or PassiveType.ResultScoreUp or PassiveType.CoinValue ? passiveEffectData.Value * 100f : passiveEffectData.Value;

        if (level <= 4)
        {
            passiveText.text = $"{LocalizationUtility.GetLZString(LocalizationUtility.defaultStringTableName, Utils.AnimalEndowmentStringKey)}\n<color=grey>{LocalizationUtility.GetLZString(LocalizationUtility.defaultStringTableName, passiveEffectData.StringID, value)}</color>";
        }
        else
        {
            passiveText.text = $"{LocalizationUtility.GetLZString(LocalizationUtility.defaultStringTableName, Utils.AnimalEndowmentStringKey)}\n{LocalizationUtility.GetLZString(LocalizationUtility.defaultStringTableName, passiveEffectData.StringID, value)}"; 
        }
    }
    
    public void SetByRequiredToken(int currentCost, int costToNeed, bool hasEnoughTokens)
    {
        if (!animalUserData.IsMaxLevel)
        {
            if (!hasEnoughTokens)
            {
                requiredTokenText.text = $"<color=red>{currentCost}</color> / {costToNeed}";
            }
            else
            {
                requiredTokenText.text = $"{currentCost} / {costToNeed}";
            }
        }
        else
        {
            requiredTokenText.text = $"{currentCost} / (MAX)";
        }
    }

    public void SetEnforceText(string text)
    {
        enforceText.text = text;
    }

    public void SetByGoldCost(int cost, bool possible)
    {
        if (!animalUserData.IsMaxLevel)
        {
            goldCostText.text = cost.ToString();
            goldCostText.color = possible ? Color.white : Color.red;
        }
        else
        {
            goldCostText.text = "(MAX)";
        }
    }
    
    public void SetAnimalImage(Sprite animalImage)
    {
        this.animalImage.sprite = animalImage;
    }

    public void SetStarImage(Sprite starImage)
    {
        this.starImage.sprite = starImage;
    }

    public void SetTokenImage(Sprite tokenImage)
    {
        this.tokenImage.sprite = tokenImage;
    }

    public void SetGoldImage(Sprite goldImage)
    {
        this.goldImage.sprite = goldImage;
    }

    public void SetEnforceButtonInteractive()
    {
        bool isPossible = enforceAnimalManager.IsEnforceAnimalPossible(animalUserData, out bool hasEnoughTokens, out bool hasEnoughGolds);
        
        enforceButton.interactable = isPossible;
    }
}