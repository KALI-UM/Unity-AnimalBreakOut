using System.Collections;
using UnityEngine;
using TMPro;
using System;
public enum DeathType
{
    None,
    Normal,
    DeathZone
}
public class PlayerManager : InGameManager
{
    //이 아이디 기준으로 플레이어를 생성함
    private int animalID = 100301;//100301;

    public GameObject playerRootGameObject;
    public GameObject playerGameObject;

    #region player component caching

    [ReadOnly]
    public MoveForward moveForward;

    [ReadOnly]
    public ExperienceStatus playerExperience;

    [ReadOnly]
    public PlayerStatus playerStatus;

    [ReadOnly]
    public AttackPowerStatus playerAttack;

    [ReadOnly]
    public PlayerMove playerMove;

    [ReadOnly]
    public Animator playerAnimator;
    #endregion

    public Action onPlayerDead;
    public Action onPlayerRespawn;
    public static Action<int> OnDeadCounting;

    public ReviveContinueUI reviveContinueUI;
    private PlayerRotator playerRotator;

    public Vector3 pendingRespawnPosition;
    public Quaternion pendingRespawnRotation;
    private Vector3 pendingForward;
    public DeathType lastDeathType = DeathType.None;
    private bool isDead;
    public bool isInIntroSequence { get; set; } = true;
    public void EndIntroSequence() => isInIntroSequence = false;
    // [SerializeField] public TMP_Text countdownText;
    private void Awake()
    {
        playerRotator = GetComponent<PlayerRotator>();

    }
    public override void Initialize()
    {
        base.Initialize();

        InitializePlayerComponents();
        GameManager.AddGameStateStartAction(GameManager_new.GameState.WaitLoading, () => DisablePlayer(playerStatus));
        GameManager.AddGameStateStartAction(GameManager_new.GameState.GameReady, () => DisablePlayer(playerStatus));
        GameManager.AddGameStateStartAction(GameManager_new.GameState.GamePlay, () => EnablePlayer(playerStatus));
        GameManager.AddGameStateExitAction(GameManager_new.GameState.GamePlay, () => DisablePlayer(playerStatus));


        // GameManager.AddGameStateEnterAction(GameManager_new.GameState.GameReStart, () => ContinuePlayerWithCountdown(gameUIManager.countdownText));


    }


    private void InitializePlayerComponents()
    {
        moveForward = playerRootGameObject.GetComponent<MoveForward>();

        playerAttack = playerGameObject.GetComponent<AttackPowerStatus>();
        playerExperience = playerGameObject.GetComponent<ExperienceStatus>();
        playerStatus = playerGameObject.GetComponent<PlayerStatus>();
        playerMove = playerGameObject.GetComponent<PlayerMove>();

        var statData = Resources.Load<AnimalStatData>("Stats/Animal_" + animalID);
        playerStatus.statData = statData;
        playerStatus.Initialize();
        playerAttack.InitializeValue(statData.AttackPower);

        playerRotator.SetPlayerMove(playerMove);

        //앞으로 가는 속도랑 옆으로 가는 속도랑 다르지 않나 일단 임시로 해놨는데 추후 확인해서 수정 요함 
        //moveForward.speed = playerStatus.MoveSpeed;
        moveForward.speed = playerStatus.MoveSpeed;

        //playerMove.moveSpeed = 5f;

    }

    public void SetPlayer()
    {
        animalID = GameDataManager.Instance.StartAnimalID;
        Debug.Log($"Set Player Start With Animal ID: {animalID}");

        ActivatePlayer();

        GameObject prefab = LoadManager.Instance.GetCharacterPrefab(animalID);
        if (prefab != null)
        {

            GameObject character = Instantiate(prefab, playerGameObject.transform);
            playerAnimator = character.GetComponent<Animator>();
            playerMove.SetAnimator(playerAnimator);

            GameManager.UIManager?.ConnectPlayerMove(this.playerMove); // 버튼 연결

            Debug.Log($"Player {animalID} spawned successfully.");

        }
        else
        {
            Debug.LogError($"Character prefab not found for ID {animalID}.");
        }
    }

    public void ActivatePlayer()
    {
        ResetMoveForward();


    }

    public void OnPlayerDied(PlayerStatus status)
    {
        Debug.Log($"Player Died: {status.name}");
        // 죽기 전 위치 저장 (DeathZone이 아닌 경우)
        if (lastDeathType != DeathType.DeathZone)
        {
            SetPendingRespawnInfo(moveForward.transform.position, moveForward.transform.rotation, moveForward.transform.forward);
            lastDeathType = DeathType.Normal;
        }

        if (lastDeathType == DeathType.DeathZone)
        {
            playerMove.canTurn = false;
        }
        GameManager.UIManager.UnShowRotateButton();
        StopAllMovements();
        DisablePlayer(status);
        PlayDeathAnimation();
        StartCoroutine(DieAndSwitch(status));
        OnDeadCounting?.Invoke(1);
        onPlayerDead?.Invoke();

    }


    public void ResetMoveForward()
    {
        if (GameManager.StageManager.IsPlayerInBossStage)
        {
            moveForward.enabled = false;
        }
        else
        {
            moveForward.enabled = true;
        }
    }

    //스테이지매니저에서 필요해서 퍼블릭으로 변경
    public void StopAllMovements()
    {
        moveForward.enabled = false;
        Debug.Log("All movements stopped.");
    }

    private void DisablePlayer(PlayerStatus playerStatus)
    {
        playerMove.DisableInput();  // 입력 비활성화
        // GameManager.UIManager?.SetDirectionButtonsInteractable(false);
    }
    private void EnablePlayer(PlayerStatus playerStatus)
    {

        playerMove.EnableInput();  // 입력 활성화
        // GameManager.UIManager?.SetDirectionButtonsInteractable(true);
    }

    private void PlayDeathAnimation()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Die");
            Debug.Log("Death animation triggered.");
        }
        else
        {
            Debug.LogError("Animator not found. Unable to play death animation.");
        }
    }
    private void PlayRunAnimaition()
    {
        playerAnimator.SetTrigger("Run");
    }

    private IEnumerator DieAndSwitch(PlayerStatus playerStatus)
    {
        GameManager.UIManager.SetDirectionButtonsInteractable(false);
        yield return new WaitForSeconds(1.5f);
        GameManager.SetGameState(GameManager_new.GameState.GameStop);
        if (reviveContinueUI != null)
        {
            reviveContinueUI.Show();
        }
        else
        {
            Debug.LogError("ReviveContinueUI를 찾을 수 없습니다!");
            GameManager.OnGameOver();
        }
        //  Destroy(playerStatus.gameObject);
        // Debug.Log($"Player {playerStatus.name} destroyed.");
    }

    public void SetPendingRespawnInfo(Vector3 position, Quaternion rotation, Vector3 forward)
    {
        pendingRespawnPosition = position;
        pendingRespawnRotation = rotation;
        pendingForward = forward.normalized;
    }

    // public void ContinuePlayer()
    // {
    //     if (currentPlayerStatus == null)
    //     {
    //         Debug.LogError("부활할 플레이어가 없습니다.");
    //         return;
    //     }

    //     var moveForward = currentPlayerStatus.GetComponentInParent<MoveForward>();

    //     if (lastDeathType == DeathType.DeathZone)
    //     {
    //         moveForward.transform.SetPositionAndRotation(pendingRespawnPosition, pendingRespawnRotation);
    //         moveForward.SetDirectionByRotation();
    //     }

    //     currentPlayerStatus.SetInvincible(true);

    //     currentPlayerMove.DisableInput();
    //     moveForward.enabled = false;

    //     currentPlayerMove.EnableInput();
    //     currentPlayerAnimator.SetTrigger("Run");
    //     ActivatePlayer(currentPlayerStatus);
    //     Debug.Log("플레이어 부활 및 무적 상태 설정");

    //     StartCoroutine(RemoveInvincibilityAfterDelay(2f)); //2초무적

    //     lastDeathType = DeathType.None;
    // }

    private IEnumerator RemoveInvincibilityAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (playerStatus != null)
        {
            playerStatus.SetInvincible(false);
            Debug.Log("무적 상태 해제");
        }
    }
    public void SetLastDeathType(DeathType type)
    {
        lastDeathType = type;
    }
    public void ContinuePlayerWithCountdown(TMP_Text countdownText)
    {
        // GameManager.SetTimeScale(0);

        if (playerStatus == null)
        {
            Debug.LogError("부활할 플레이어가 없습니다.");
            return;
        }


        if (lastDeathType == DeathType.DeathZone)
        {
            moveForward.transform.SetPositionAndRotation(pendingRespawnPosition, pendingRespawnRotation);
            moveForward.SetDirectionByRotation();
        }

        // 무적 유지 (계속 유지됨)
        //currentPlayerStatus.SetInvincible(true);

        // 이동 및 입력 비활성화
        playerMove.DisableInput();
        moveForward.enabled = false;

        // 애니메이션은 Run으로
        // currentPlayerAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        playerAnimator.SetTrigger("idle");


        ActivatePlayer();


        //  gameUIManager.CountDown();

    }

    // private IEnumerator ResumeAfterCountdown(TMP_Text countdoext, MoveForward moveForward)
    // {
    //     GameManager.UIManager?.SetDirectionButtonsInteractable(false);
    //     GameManager.SetTimeScale(0);
    //     countdownText.gameObject.SetActive(true);

    //     for (int i = 3; i > 0; i--)
    //     {
    //         countdownText.text = i.ToString();
    //         yield return new WaitForSecondsRealtime(1);
    //     }

    //     countdownText.gameObject.SetActive(false);
    //     GameManager.SetTimeScale(1);

    //     // 이동 및 입력 복원
    //     GameManager.UIManager?.SetDirectionButtonsInteractable(true);
    //     currentPlayerStatus.SetAlive();
    //     currentPlayerMove.EnableInput();
    //     moveForward.enabled = true;
    //     currentPlayerAnimator.updateMode = AnimatorUpdateMode.Normal; // 스케일 영향 받게 함 임시 처리 라 수정 해야함
    //     currentPlayerAnimator.SetTrigger("Run");

    //     // 무적 해제는 따로 2초 후
    //     StartCoroutine(RemoveInvincibilityAfterDelay(2f));

    //     lastDeathType = DeathType.None;
    //     Debug.Log("플레이어 3초 후 부활 처리 완료");
    // }

}