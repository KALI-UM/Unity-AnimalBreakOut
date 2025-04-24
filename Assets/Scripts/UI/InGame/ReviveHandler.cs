using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class ReviveHandler
{
    GameManager_new gameManager;
    private PlayerManager playerManager;
    public static Action<int> OnReviveCounting;

    private GameUIManager uiManager;
    private MonoBehaviour coroutineHost;
    private PausePanelUI pausePanelUI;
    private Coroutine countdownCoroutine;
    private PauseHandler pauseHandler;

    public ReviveHandler(GameManager_new gameManager, PlayerManager playerManager, PausePanelUI pausePanelUI, GameUIManager uiManager, PauseHandler pauseHandler, MonoBehaviour host)


    {
        this.gameManager = gameManager;
        this.playerManager = playerManager;
        this.uiManager = uiManager;
        this.coroutineHost = host;
        this.pausePanelUI = pausePanelUI;
        this.pauseHandler = pauseHandler;
    }

    public void StartReviveCountdown()
    {
        RestartCoroutine(ReviveRoutine());
    }

    public void StartInGameReviveCountdown()
    {
        RestartCoroutine(InGameReviveRoutine());
    }

    private void RestartCoroutine(IEnumerator routine)
    {
        if (countdownCoroutine != null)
            coroutineHost.StopCoroutine(countdownCoroutine);
        countdownCoroutine = coroutineHost.StartCoroutine(routine);
    }

    private IEnumerator ReviveRoutine()
    {
        if (playerManager.lastDeathType == DeathType.DeathZone)
        {
            playerManager.moveForward.transform.SetPositionAndRotation(playerManager.pendingRespawnPosition, playerManager.pendingRespawnRotation);
            playerManager.moveForward.SetDirectionByRotation();
        }

        uiManager.SetPauseButtonInteractable(false);
        pausePanelUI.Hide();
        pausePanelUI.ShowCountdown();

        playerManager.playerStatus.SetInvincible(true);
        var pos = playerManager.playerMove.transform.localPosition;
        pos.y = 0f;
        playerManager.playerMove.transform.localPosition = pos;

        playerManager.playerAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        playerManager.playerAnimator.ResetTrigger("Run");
        playerManager.playerAnimator.SetTrigger("idle");

        playerManager.playerStatus.isDead = false;
        playerManager.playerStatus.SetReviving(true);

        for (int i = 3; i > 0; i--)
        {
            pausePanelUI.UpdateCountdownText(i.ToString());
            yield return new WaitForSecondsRealtime(1);
        }

        uiManager.SetDirectionButtonsInteractable(true);
        gameManager.RestartGameState();

        playerManager.playerAnimator.updateMode = AnimatorUpdateMode.Normal;
        pausePanelUI.HideCountdown();
        playerManager.playerStatus.SetAlive();
        playerManager.playerMove.EnableInput();

        Collider[] hits = Physics.OverlapSphere(playerManager.playerMove.transform.position, 1f);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<SwipeTurnTrigger>(out var trigger))
                trigger.ForceAutoTurnIfInside(playerManager.playerMove.gameObject);
        }

        gameManager.PlayerManager.ResetMoveForward();
        playerManager.playerAnimator.SetTrigger("Run");
        coroutineHost.StartCoroutine(RemoveInvincibilityAfterDelay(2f));

        playerManager.lastDeathType = DeathType.None;
        playerManager.playerStatus.SetReviving(false);
        OnReviveCounting?.Invoke(1);
        countdownCoroutine = null;
        pauseHandler.Resume();
    }


    private IEnumerator InGameReviveRoutine()
    {
        pausePanelUI.Hide();
        pausePanelUI.ShowCountdown();
        uiManager.SetDirectionButtonsInteractable(false);

        for (int i = 3; i > 0; i--)
        {
            pausePanelUI.UpdateCountdownText(i.ToString());
            yield return new WaitForSecondsRealtime(1);
        }

        if (!playerManager.playerStatus.isDead && !playerManager.isInIntroSequence)
        {
            uiManager.SetDirectionButtonsInteractable(true);
            gameManager.RestartGameState();
            pausePanelUI.HideCountdown();
            playerManager.playerStatus.SetAlive();
            playerManager.playerMove.EnableInput();
            gameManager.PlayerManager.ResetMoveForward();
            playerManager.playerAnimator.SetTrigger("Run");
        }
        else
        {
            gameManager.RestartGameState();
            pausePanelUI.HideCountdown();
        }

        countdownCoroutine = null;
        pauseHandler.Resume();
    }


    private IEnumerator RemoveInvincibilityAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (playerManager.playerStatus != null)
        {
            playerManager.playerStatus.SetInvincible(false);
            Debug.Log("무적 상태 해제");
        }
    }
}
