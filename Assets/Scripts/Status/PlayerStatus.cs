using UnityEngine;
using System.Collections;
using System;

using UnityEngine.Events;
public class PlayerStatus : MonoBehaviour
{
    //1.데이터 테이블에서 바로 읽어오는 방식
    //2.스크립터블 오브젝트 채로 프리펩화 하는 방식
    //지금은 1번입니다. 2번으로 추후 변경토록 하세요
    //기존 방식인 애니멀데이터 전체를 스크립터블 오브젝트화하는 것은 일반적이지 않습니다
    //각 AnimalData를 스크립터블 오브젝트화 하여 런타임전에 미리 장착하는 방법을 추천합니다.
    public AnimalStatData statData;

    //public AnimalDatabase animalDB;
    //public int currentAnimalID;
    //private bool isGameOver;
    //private AnimalStatus currentAnimal;
    //public Action<PlayerStatus> onDie;

    private bool isInvincible = false;
    public bool isDead;
    public Action onAlive;
    public int defaultLayer;
    public int invincibleLayer;
    public bool IsInvincible => isInvincible;
    public Action<bool> onInvincibleChanged;
    //public int AttackPower => statData != null ? statData.AttackPower : 0;
    public float MoveSpeed => statData != null ? statData.StartSpeed : 0;
    public float JumpPower => statData != null ? statData.Jump : 0;
    private PlayerManager playerManager;
    public bool IsReviving { get; private set; }
    [SerializeField] private GameObject InvisibleEffect;

    public void SetReviving(bool value)
    {
        IsReviving = value;
    }

    private void Start()
    {
        var GameManager = GameObject.FindGameObjectWithTag(Utils.GameManagerTag);
        var GameManager_new = GameManager.GetComponent<GameManager_new>();
        playerManager = GameManager_new.PlayerManager;
        //if (animalDB == null)
        //{
        //    animalDB = FindObjectOfType<AnimalDatabase>();
        //    if (animalDB == null)
        //    {
        //        Debug.LogError("AnimalDatabase를 찾을 수 없습니다.");
        //        return;
        //    }
        //}

        //Init(currentAnimalID, animalDB);
        // animator = GetComponentInChildren<Animator>();
        // defaultLayer = LayerMask.NameToLayer("Player");
        // invincibleLayer = LayerMask.NameToLayer("InvinciblePlayer");
    }

    private void Awake()
    {
        DamagedParticleCollision.onTakeDamage += TakeDamage;
    }

    private void OnDestroy()
    {
        DamagedParticleCollision.onTakeDamage -= TakeDamage;
    }

    public void Initialize()
    {
        // animalData = DataTableManager.animalDataTable.Get(animalID);
        //animalDB = database;
        //SetAnimal(animalID);

        defaultLayer = LayerMask.NameToLayer("Player");
        invincibleLayer = LayerMask.NameToLayer("InvinciblePlayer");
    }

    //public void SetAnimal(int animalID)
    //{
    //    currentAnimal = animalDB.GetAnimalByID(animalID);
    //    currentAnimalID = animalID;

    //    if (currentAnimal != null)
    //    {
    //        Debug.Log($"선택한 캐릭터: {currentAnimal.Name} | 공격력: {currentAnimal.AttackPower} | HP: {currentAnimal.HP}");
    //    }
    //    else
    //    {
    //        Debug.LogError("해당 ID의 캐릭터를 찾을 수 없음!");
    //    }
    //}

    public void SetInvincible(bool value)
    {
        isInvincible = value;
        gameObject.layer = isInvincible ? invincibleLayer : defaultLayer;
        onInvincibleChanged?.Invoke(value);
        if (isInvincible == true)
        {
            InvisibleEffect.SetActive(true);
        }
        else
        {
            InvisibleEffect.SetActive(false);
        }
    }

    [ContextMenu("Toggle Invincible")]
    public void ToggleInvincible()
    {
        SetInvincible(!isInvincible);
    }
    public IEnumerator DisableInvincibilityAfterDelay(PlayerStatus status, float delay)
    {
        yield return new WaitForSeconds(delay);
        status.SetInvincible(false);
    }


    //임시로 고쳐놓은 것이라 수정이 필요합니다.
    // public float GetMoveSpeed() => animalData?.StartSpeed ?? 0f;
    // public float GetJumpPower() => animalData?.Jump ?? 0f;

    [ContextMenu("Damage +1")]
    public void ForceTakeDamage()
    {
        playerManager.OnPlayerDied(this);
    }

    public void TakeDamage(int damage)
    {
        //currentAnimal.HP -= damage;
        //Debug.Log($"{currentAnimal.Name} took {damage} damage. Current HP: {currentAnimal.HP}");

        //if (currentAnimal.HP <= 0) OnDie();
        if (isDead)
        {
            return;
        }
        if (isInvincible) return;
        isDead = true;

        // playerManager.currentPlayerStatus.SetInvincible(true);

        playerManager.OnPlayerDied(this);
    }
    public bool IsDead()
    {
        return isDead;
    }
    public void SetAlive()
    {
        isDead = false;
        onAlive?.Invoke();
    }
    // private void OnDie()
    // {
    //     //if (isGameOver) return;
    //     //isGameOver = true;
    //     StopAllMovements();
    //     var move = GetComponent<PlayerMove>();
    //     move.DisableInput();

    //     if (animator != null)
    //     {
    //         animator.SetTrigger("Die");
    //     }

    //     onDie?.Invoke(this);

    //     Debug.Log($"Player Died");
    //     StartCoroutine(DieAndSwitch());
    // }

    // IEnumerator DieAndSwitch()
    // {

    //     yield return new WaitForSeconds(1.5f);
    //     Destroy(gameObject);
    // }
    // private void StopAllMovements()
    // {
    //     MoveForward[] movingObjects = FindObjectsOfType<MoveForward>();
    //     foreach (var move in movingObjects)
    //     {
    //         move.enabled = false;
    //     }
    //     Debug.Log("All movements stopped.");
    // }
}
