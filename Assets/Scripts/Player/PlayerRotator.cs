using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    public float rotateDuration = 5f;
    private PlayerMove playerMove;

    public Action onRotationStart;
    public Action onRotationEnd;

    private bool isRotating = false;
    public void SetPlayerMove(PlayerMove playerMove)
    {
        this.playerMove = playerMove;
        this.playerMove.onRotate += Rotate;

        onRotationStart += playerMove.DisableInput;
        onRotationEnd += playerMove.EnableInput;
    }

    public void Rotate(Vector3 pivot, float angle)
    {
        if (!isRotating)
        {
            StartCoroutine(RotateRoutine(pivot, angle));
        }
    }

    private IEnumerator RotateRoutine(Vector3 pivot, float angle)
    {
        isRotating = true;
        onRotationStart?.Invoke();

        MoveForward moveForward = playerMove.transform.parent.GetComponent<MoveForward>();
        moveForward.enabled = false;
        
        int nextLane = GetTurnAfterLaneIndex(moveForward, pivot, angle);
        if (nextLane != 1)
        {
            bool isPassed = IsPlayerPassedPivot(moveForward, pivot);
            if (isPassed)
            {
                moveForward.transform.position = pivot + moveForward.direction;
            }
            else
            {
                moveForward.transform.position = pivot - moveForward.direction;
            }
        }
        else
        {
            moveForward.transform.position = pivot;
        }
       
        //�÷��̾� ������ ȸ���ϴ� ����
        float elapsed = 0f;
        float currentAngle = 0f;
        while (elapsed < rotateDuration)
        {
            float deltaAngle = (elapsed / rotateDuration) * angle - currentAngle;
            playerMove.transform.Rotate(new Vector3(0, deltaAngle, 0));
            currentAngle += deltaAngle;
            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }


        //�÷��̾��Ʈ�� ������ ȸ���ϰ� ����� ȸ���� ���½�Ų��.
        moveForward.transform.RotateAround(pivot,Vector3.up, angle);
        playerMove.transform.localRotation = Quaternion.identity;

        //���� ��ġ�� �������ش�.
        playerMove.MoveLaneIndexImmediate(nextLane);

        if (moveForward != null)
        {
            moveForward.enabled = true;
        }
        isRotating = false;

       
        moveForward.RotateForwardDirection(angle);
        onRotationEnd?.Invoke();
    }

    private bool IsPlayerPassedPivot(MoveForward player, Vector3 pivot)
    {
        var playerToPivot = pivot - player.transform.position;
        bool isPlayerPassed = Vector3.Dot(playerToPivot, player.direction) < 0f;
        return isPlayerPassed;
    }

    private int GetTurnAfterLaneIndex(MoveForward player, Vector3 pivot, float angle)
    {
        //�� Ÿ�ϻ���� 1,1�̶�� �����Ͽ� ����
        var playerToPivot = pivot - player.transform.position;
        if (Vector3.Magnitude(playerToPivot) <= 0.5f)
        {
            //�÷��̾�� �Ǻ��� �Ÿ��� 0.5 �̸��̸� 1�� ����
            return 1;
        }
        else
        {
            //�÷��̾ �Ǻ��� ���� ��� 0�Ǵ� 2�ε� ȸ�� ���⿡���� ������ġ�� 0���� 2���� �����ȴ�.
            bool isPassed = IsPlayerPassedPivot(player, pivot);
            if (isPassed)
            {
                return angle > 0f ? 0 : 2;
            }
            else
            {
                return angle > 0f ? 2 : 0;
            }
        }
    }
}
