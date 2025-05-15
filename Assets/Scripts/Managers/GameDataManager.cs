using System;
using System.Collections.Generic;
using UnityCommunity.UnitySingleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDataManager : PersistentMonoSingleton<GameDataManager>
{
    #region globalDataSystems

    public PlayerAccountData PlayerAccountData
    {
        get;
        private set;
    }

    public GoldAnimalTokenKeySystem GoldAnimalTokenKeySystem
    {
        get;
        private set;
    }

    public PlayerLevelSystem PlayerLevelSystem
    {
        get;
        private set;
    }

    public StaminaSystem StaminaSystem
    {
        get;
        private set;
    }

    #endregion

    private float additionalScoreGoldRate;
    public float GetAdditionalScoreGoldRate()
    {
        return additionalScoreGoldRate;
    }
    public static readonly Dictionary<int, int> expToLevelUpDictionary = new();
    public static readonly Dictionary<int, int> maxStaminaByLevelDictionary = new();
    public static readonly Dictionary<int, LevelUpRewardData> levelUpRewardDataDictionary = new();
    public readonly int maxLevel = 5;
    private static bool isAddToDictInInitialize;

    public int currentLevel { get; private set; }
    public int nextExp { get; private set; }
    public int currentExp { get; private set; }

    private OutGameUIManager outGameUIManager;
    private OutGameManager outGameManager;
    private GameManager_new gameManager;

    //동물당 해금 여부, 강화여부 등을 들고있는 데이터다
    public AnimalUserDataList AnimalUserDataList
    {
        get;
        private set;
    }

    //여기저기서 쓰는 곳이 많아보여서 일단 이렇게 봉합하였습니다, 이렇게 바꾸어도 되나요?
    public int startAnimalID
    {
        get => AnimalUserDataList.CurrentAnimalID;
        //private set;
    }
    //public int StartAnimalID => startAnimalID;

    public static event Action<int, int, AnimalUserData> onSetStartAnimalIDInGameDataManager;

    public int MinMapObjectId { get; private set; } = 0;
    public int MinRewardItemId { get; private set; } = 0;
    public int MaxMapObjectId { get; private set; } = 0;
    public int MaxRewardItemId { get; private set; } = 0;

    public static event Action<int, int> onStaminaChangedInGameDataManager;
    //public static event Action<long> OnGoldChangedInGameDataManager;
    public const int minStamina = 0;
    public const int maxStamina = 999;

    private LevelUpInfoData initialData;

    public const long keyPrice = 5000;

    public long staminaGoldUseCost { get; private set; }
    public int staminaToAdd { get; private set; }

    public TokenType requiredTokenType { get; private set; }
    public int requiredTokenCount { get; private set; }
    public long requiredGoldCount { get; private set; }

    public EnforceAnimalPanel targetEnforceAnimalPanel { get; private set; }
    
    public int frameRateIndex { get; private set; }
    public LanguageSettingType languageSettingType { get; private set; }
    
    public static event Action onLocaleChange;

    private Button gachaSingleAdsButton;
    public Button GachaSingleAdsButton { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        QualitySettings.vSyncCount = 0;
        SettingPanel.onFrameRateIndexChanged += OnFrameRateIndexChangedHandler;
        SettingPanel.onLanguageSettingTypeChanged += OnLanguageSettingTypeChangedHandler;
    }

    public override void InitializeSingleton()
    {
        base.InitializeSingleton();
        SaveLoadSystem.Instance.Load();

        LocalizationUtility.PreloadLocalizedTables();

        //플레이어 정보를 관리하는 시스템
        PlayerAccountData = new();
        PlayerAccountData.Load(SaveLoadSystem.Instance.CurrentSaveData.playerAccountDataSave);

        //골드,토큰을 관리하는 시스템
        GoldAnimalTokenKeySystem = new();
        GoldAnimalTokenKeySystem.Load(SaveLoadSystem.Instance.CurrentSaveData.goldAnimalTokenKeySystemSave);

        //플레이어 레벨, 경험치를 관리하는 시스템
        PlayerLevelSystem = new();
        PlayerLevelSystem.Load(SaveLoadSystem.Instance.CurrentSaveData.playerLevelSystemSave);

        //스테미나를 관리하는 시스템
        StaminaSystem = new();
        StaminaSystem.Load(SaveLoadSystem.Instance.CurrentSaveData.staminaSystemSave);

        //동물당 해금 여부, 강화여부 등을 들고있는 데이터 초기화
        AnimalUserDataList = new();
        AnimalUserDataList.Load(SaveLoadSystem.Instance.CurrentSaveData.animalUserDataTableSave);
        
        SetFrameRateIndex(PlayerAccountData.frameRateIndex);
        OnFrameRateIndexChangedHandler(frameRateIndex);

        SetMaxMapObjectId(DataTableManager.mapObjectsDataTable.maxId);
        SetMinMapObjectId(DataTableManager.mapObjectsDataTable.minId);
        SetMaxRewardItemId(DataTableManager.rewardItemsDataTable.maxId);
        SetMinRewardItemId(DataTableManager.rewardItemsDataTable.minId);
    }

    private void Start()
    {
        UnlockedAnimalPanel.onSetStartAnimalIDInPanel += OnSetAnimalIDInPanel;

        SceneManager.sceneLoaded += OnChangeSceneHandler;

        LobbyPanel.onGameStartButtonClicked += OnGameStartButtonClickedHandler;

        OutGameUIManager.onAnimalUnlockPanelInstantiated += onAnimalUnlockPanelInstantiatedHandler;

        GachaManager.onTokenAdded += OnTokenAddedHandler;

        StaminaGoldUseButton.onStaminaGoldUseButtonClicked += OnStaminaGoldUseButtonClickedHandler;

        onSetStartAnimalIDInGameDataManager?.Invoke(startAnimalID, StaminaSystem.CurrentStamina
        , AnimalUserDataList.GetAnimalUserData(startAnimalID));

        EnforceAnimalPanel.onEnforceInfoSet += OnEnforceInfoSetHandler;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnChangeSceneHandler;

        UnlockedAnimalPanel.onSetStartAnimalIDInPanel -= OnSetAnimalIDInPanel;

        SceneManager.sceneLoaded -= OnChangeSceneHandler;

        LobbyPanel.onGameStartButtonClicked -= OnGameStartButtonClickedHandler;

        OutGameUIManager.onAnimalUnlockPanelInstantiated -= onAnimalUnlockPanelInstantiatedHandler;

        GachaManager.onTokenAdded -= OnTokenAddedHandler;

        StaminaGoldUseButton.onStaminaGoldUseButtonClicked -= OnStaminaGoldUseButtonClickedHandler;

        EnforceAnimalPanel.onEnforceInfoSet -= OnEnforceInfoSetHandler;

        SettingPanel.onFrameRateIndexChanged -= OnFrameRateIndexChangedHandler;
        
        SettingPanel.onLanguageSettingTypeChanged -= OnLanguageSettingTypeChangedHandler;
    }


    private void SetMaxMapObjectId(int maxMapObjectCount)
    {
        MaxMapObjectId = maxMapObjectCount;
    }

    private void SetMinMapObjectId(int minMapObjectCount)
    {
        MinMapObjectId = minMapObjectCount;
    }

    private void SetMaxRewardItemId(int maxRewardItemCount)
    {
        MaxRewardItemId = maxRewardItemCount;
    }
    private void SetMinRewardItemId(int minRewardItemCount)
    {
        MinRewardItemId = minRewardItemCount;
    }

    public void Initialize()
    {
        if (SceneManager.GetActiveScene().name == "MainTitleScene")
        {
            TryFindOutGameObject();
        }
        else if (SceneManager.GetActiveScene().name == "Run")
        {
            TryFindGameManager();
        }

        //ClearInGameData();
    }

    private void TryFindOutGameObject()
    {
        //Debug.Assert(GameObject.FindGameObjectWithTag("OutGameUIManager").TryGetComponent(out outGameUIManager), "Cant find OutGameUIManager");
        GameObject.FindGameObjectWithTag("OutGameManager").TryGetComponent(out outGameManager);
        outGameUIManager = outGameManager.OutGameUIManager;

        var btn = GameObject.FindGameObjectWithTag("AdsButton");
        btn.TryGetComponent(out gachaSingleAdsButton);
        GachaSingleAdsButton = gachaSingleAdsButton;
    }

    private void TryFindGameManager()
    {
        Debug.Assert(GameObject.FindGameObjectWithTag("GameManager").TryGetComponent(out gameManager)
        , "Cant find GameManager");
    }

    public void ClearAdditionalScoreGoldRate()
    {
        additionalScoreGoldRate = 0f;
    }

    public void AddAdditionalScoreGoldRate(float rate)
    {
        additionalScoreGoldRate += rate;
    }


    public void ApplyRunResult(long score, float playTime)
    {
        //수정필

        //임시 값 적용
        var resultGold = score / 10;

        GoldAnimalTokenKeySystem.AddGold(resultGold + Mathf.FloorToInt(resultGold * additionalScoreGoldRate));
        PlayerLevelSystem.AddExperienceValue(40 + Mathf.RoundToInt(playTime * 0.31f));
    }

    private void OnChangeSceneHandler(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "MainTitleScene")
        {
            TryFindOutGameObject();

            OnFrameRateIndexChangedHandler(frameRateIndex);
        }
        else if (SceneManager.GetActiveScene().name == "Run")
        {
            TryFindGameManager();

            OnFrameRateIndexChangedHandler(frameRateIndex);
        }

        Time.timeScale = 1;
    }

    private void OnSetAnimalIDInPanel(int id)
    {
        //startAnimalID = id;

        AnimalUserDataList.SetCurrentAnimalPlayer(id);
        onSetStartAnimalIDInGameDataManager?.Invoke(id, StaminaSystem.CurrentStamina, 
            AnimalUserDataList.GetAnimalUserData(startAnimalID));
    }

    private void OnGameStartButtonClickedHandler(int staminaRequiredToStartGame)
    {
        if (StaminaSystem.CurrentStamina < staminaRequiredToStartGame)
        {
            Debug.Assert(false, "Stamina is low to start game");

            return;
        }

        StaminaSystem.PayStamina(staminaRequiredToStartGame);
    }

    private void onAnimalUnlockPanelInstantiatedHandler(GameObject animalUnlockPanel)
    {
        onSetStartAnimalIDInGameDataManager?.Invoke(startAnimalID, StaminaSystem.CurrentStamina,
            AnimalUserDataList.GetAnimalUserData(startAnimalID));
    }


    private void onExpChangedInSameLevelHandler(int currentLevel, int nextExp, int currentExp)
    {
        this.currentLevel = currentLevel;
        this.nextExp = nextExp;
        this.currentExp = currentExp;
        initialData.SaveLevelUpInfoData(this.currentLevel, this.nextExp, this.currentExp);
    }

    public Coroutine StartAddStaminaCoroutine()
    {
        return StartCoroutine(StaminaSystem.CoAddStamina());
    }
    
    private void OnTokenAddedHandler(TokenType type, int tokenValue)
    {
        switch (type)
        {
            case TokenType.BronzeToken:
                {
                    GoldAnimalTokenKeySystem.AddBronzeToken(tokenValue);

                    return;
                }
            case TokenType.SilverToken:
                {
                    GoldAnimalTokenKeySystem.AddSliverToken(tokenValue);

                    return;
                }
            case TokenType.GoldToken:
                {
                    GoldAnimalTokenKeySystem.AddGoldToken(tokenValue);

                    return;
                }
        }

        Debug.Assert(false, "Invalid tokenType");
    }

    private void OnStaminaGoldUseButtonClickedHandler(long staminaGoldUseCost, int staminaToAdd)
    {
        this.staminaGoldUseCost = staminaGoldUseCost;
        this.staminaToAdd = staminaToAdd;
    }

    private void OnEnforceInfoSetHandler(TokenType requiredTokenType, int requiredTokenCount,
        long requiredGoldCount, EnforceAnimalPanel targetEnforceAnimalPanel)
    {
        this.requiredTokenType = requiredTokenType;
        this.requiredTokenCount = requiredTokenCount;
        this.requiredGoldCount = requiredGoldCount;
        this.targetEnforceAnimalPanel = targetEnforceAnimalPanel;
    }
    
    private void OnFrameRateIndexChangedHandler(int frameRateIndex)
    {
        this.frameRateIndex = frameRateIndex;
        
        switch ((FrameRateType)frameRateIndex)
        {
            case FrameRateType.Frame30:
                {
                    Application.targetFrameRate = 30;
                }
                break;
            case FrameRateType.Frame60:
                {
                    Application.targetFrameRate = 60;
                }
                break;
            case FrameRateType.Frame120:
                {
                    Application.targetFrameRate = 120;
                }
                break;
            default:
                {
                    Debug.Assert(false, "FrameRateType is not defined.");
                }
                break;
        }
        
        Debug.Log($"Frame Rate Index : {frameRateIndex}");
    }

    public void SetFrameRateIndex(int frameRateIndex)
    {
        this.frameRateIndex = frameRateIndex;
    }

    private void OnLanguageSettingTypeChangedHandler(LanguageSettingType languageSettingType)
    {
        SetLanguageSettingType(languageSettingType);
    }

    public void SetLanguageSettingType(LanguageSettingType languageSettingType)
    {
        this.languageSettingType = languageSettingType;

        switch (this.languageSettingType)
        {
            case LanguageSettingType.Korean:
                {
                    //StartCoroutine(LocalizationUtility.LocaleChange("Korean (South Korea) (ko-KR)"));
                    LocalizationUtility.ChangeLocaleNow("Korean (South Korea) (ko-KR)");
                }
                break;
            case LanguageSettingType.English:
                {
                    //StartCoroutine(LocalizationUtility.LocaleChange("English (United States) (en-US)"));
                    LocalizationUtility.ChangeLocaleNow("English (United States) (en-US)");
                }
                break;
            default:
                {
                    Debug.Assert(false, "LanguageSettingType is not defined.");
                }
                break;
        }
        
        onLocaleChange?.Invoke();
    }
}