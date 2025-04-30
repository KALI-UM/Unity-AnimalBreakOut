using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    private Lane lane;

    public float moveForwardSum;

    private void Awake()
    {
        direction.Normalize();
        lane = GetComponent<Lane>();
    }

    private void Start()
    {
        // enabled = false;
        // var gameManager = GameObject.FindObjectOfType<GameManager>();
        // gameManager.onPlayerSpawned += (playerStatus) =>
        // {
        //     if (playerStatus.gameObject == gameObject)
        //     {
        //         enabled = true;
        //     }
        // };
        // gameManager.onPlayerDied += (playerStatus) =>
        // {
        //     if (playerStatus.gameObject == gameObject)
        //     {
        //         enabled = false;
        //     }
        // };
    }

    private void Update()
    {
        var nextPosition = transform.position + direction * Time.deltaTime * speed; //이거의 길이 얼마나갓나 이걸 더해 총 얼마나갔는가 
        moveForwardSum += Time.deltaTime * speed;

        transform.position = nextPosition;
    }
    public void RotateForwardDirection(float angle)
    {
        direction = Quaternion.AngleAxis(angle, Vector3.up) * direction;
        direction.Normalize();
    }

    public void SetDirectionByRotation()
    {
        direction = transform.forward.normalized;
        Debug.Log($"[MoveForward] 이동 방향을 회전값 기준으로 설정: {direction}");
    }

    public void AddSpeed(float value)
    { 
        speed += value;

        speed = Mathf.Clamp(speed, 5f, 12f);
    }
}
