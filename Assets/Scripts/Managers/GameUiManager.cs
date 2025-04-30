using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class GameUIManager : InGameManager
{
    public RotateButtonController rotateButtonController;

    public static event Action onShowGameOverPanel;
    private PauseHandler pauseHandler;
    private ReviveHandler reviveHandler;


    public static Action<int> OnGamePlayCounting;

    public List<UIElement> uiElements;

    [SerializeField] private PausePanelUI pausePanelUI;
    [SerializeField] private ResultPanelUI resultPanelUI;
    [SerializeField] private PauseButtonUI pauseButtonUI;
    [SerializeField] private RotateButtonUI rotateButtonUI;
    [SerializeField] private InputUIBinder inputUIBinder;
    [SerializeField] public RunStageUI runStageUI;
    [SerializeField] public BossWayUI bossWayUI;
    [SerializeField] public BossTimeLimit bossTimeLimit;
    [SerializeField] public BossDebuffUIController bossDebuffUI;
    [SerializeField] public SupportSlotUI supportSlotUI;
    [SerializeField] public AttackSlotUI attackSlotUI;
    [SerializeField] private Button koreanButton; // 임시 한글 설정 버튼
    [SerializeField] private Button englishButton; //임시 영어 설정 버튼
    private void Start() // 임시 한글 영어 설정 버튼
    {
        koreanButton.onClick.AddListener(() => LocalizationUtility.ChangeLocaleNow("Korean (South Korea) (ko-KR)"));
        englishButton.onClick.AddListener(() => LocalizationUtility.ChangeLocaleNow("English (United States) (en-US)"));
    }

    public override void Initialize()
    {
        base.Initialize();
        GameManager.AddGameStateEnterAction(GameManager_new.GameState.GameOver, ShowGameOverPanel);
        GameManager.AddGameStateEnterAction(GameManager_new.GameState.GameReady, () => SetDirectionButtonsInteractable(false));
        //GameManager.AddGameStateEnterAction(GameManager_new.GameState.GameReStart, () => CountDown());
        // GameManager.AddGameStateEnterAction(GameManager_new.GameState.GamePlay, () => SetDirectionButtonsInteractable(true));

        pauseHandler = new PauseHandler(GameManager, GameManager.PlayerManager, pausePanelUI, this);
        reviveHandler = new ReviveHandler(GameManager, GameManager.PlayerManager, pausePanelUI, this, pauseHandler, this);
        inputUIBinder.Bind(GameManager.PlayerManager.playerMove);

        //자식으로 딸린 모든 UIElements들을 탐색한다.
        //var elements = gameObject.GetComponentsInChildren<UIElement>(true);
        //uiElements = elements.ToList();
        //UIManager Editor상으로 UIElementsEnum을 Generate해주면서 uiElements리스트를 채워준다.

        //UIElements들에 매니저를 세팅
        foreach (var element in uiElements)
        {
            element.SetUIManager(GameManager, this);
        }
        //Start보다 이전에 값을 세팅해야하는 UI들의 UIElements은 Initialze에서 해주면 된다
        foreach (var element in uiElements)
        {
            element.Initialize();
        }
    }

    public void ShowUIElement(UIElementEnums uIElementsEnum)
    {
        uiElements[(int)uIElementsEnum].Show();
    }

    public void ConnectPlayerMove(PlayerMove move)
    {
        inputUIBinder.Bind(move);
    }
    public void ShowGameOverPanel()
    {
        onShowGameOverPanel?.Invoke();
        resultPanelUI.Show();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainTitleButtonClicked()
    {
        SceneManager.LoadScene("MainTitleScene");
    }
    private void OnPauseButtonClicked()
    {
        pauseHandler.TogglePause();
    }
    public void SetPauseButtonInteractable(bool interactable)
    {
        pauseButtonUI.SetInteractable(interactable);
    }

    public void Pause()
    {
        pauseHandler.TogglePause();
    }
    public void SetDirectionButtonsInteractable(bool interactable)
    {
        inputUIBinder.SetInteractable(interactable);
        pauseButtonUI.SetInteractable(interactable);
        rotateButtonUI.SetInteractable(interactable);
    }
    public void ShowRotateButton() => rotateButtonUI.Show();
    public void UnShowRotateButton() => rotateButtonUI.Hide();
    // private Coroutine coCountDown = null;

    public void CountDown()
    {
        reviveHandler.StartReviveCountdown();
    }

    public void InGameCountDown()
    {
        reviveHandler.StartInGameReviveCountdown();
    }
    public void SetLastDeathType(DeathType type)
    {
        GameManager.PlayerManager.lastDeathType = type;
    }


    public void RequestContinue()
    {
        CountDown(); // 부활 시작
    }

    public void RequestGiveUp()
    {
        GameManager.SetGameState(GameManager_new.GameState.GameOver);
        OnGamePlayCounting?.Invoke(1);

    }

}