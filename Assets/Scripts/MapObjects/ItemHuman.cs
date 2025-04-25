using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemHuman : ItemBase
{
    public override ObjectType ObjectType { get; protected set; } = ObjectType.None;
    protected override BaseCollisionBehaviour CollisionBehaviour { get; set; } //충돌 

    public override ItemType ItemType { get; protected set; } = ItemType.None; //아이템 타입

    public HumanItemType HumanItemType { get; private set; } = HumanItemType.None; // 인간 타입

    [SerializeField] private HumanItemType humanItemType;
    private ItemDataTable.ItemData itemStatData;
    private bool onCollision;
    private const float inActiveTimeDelay = 5f;

    private float inActiveTimer;
    private const float rotationSpeed = 540f;
    private const float moveSpeed = 15f;

    private Animator animator;
    private InGameCountManager inGameCountManager;

    public int Score => itemStatData?.Score ?? 0;

    private void Awake()
    {
        TryGetComponent(out animator);

        var GameManager = GameObject.FindGameObjectWithTag(Utils.GameManagerTag);
        var GameManager_new = GameManager.GetComponent<GameManager_new>();
        inGameCountManager = GameManager_new.InGameCountManager;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Utils.PlayerTag) && !onCollision)
        {
            onCollision = true;

            StartCoroutine(OnCollisionCoroutine());

            base.OnTriggerEnter(other);
        }
    }

    private IEnumerator OnCollisionCoroutine()
    {
        animator.Play(Utils.ItemHumanAnimatorDeadHash, 1);

        int randomDir = Random.value < 0.5f ? -1 : 1;

        Vector3 moveDirection = transform.rotation * ((Vector3.back) + (Vector3.up / 2f) + ((Vector3.right / 6f) * randomDir)).normalized;

        while (inActiveTimer < inActiveTimeDelay)
        {
            float delta = Time.deltaTime;
            inActiveTimer += delta;

            transform.Rotate(rotationSpeed * delta, 0f, 0f);

            transform.position += moveDirection * (moveSpeed * delta);

            yield return null;
        }

        gameObject.SetActive(false);
    }

    public void Initialize()
    {
        if (humanItemType == HumanItemType.None || humanItemType == HumanItemType.Count)
        {
            Debug.Assert(false, "Invalid HumanItemType");

            return;
        }

        ObjectType = ObjectType.Item;

        ItemType = ItemType.Human;

        itemStatData = DataTableManager.itemDataTable.Get((int)this.humanItemType);
        HumanItemType = this.humanItemType;
        CollisionBehaviour = CollisionBehaviourFactory.GetHumanBehaviour(this.humanItemType);
        CollisionBehaviour.SetScoreToAdd(itemStatData.Score);

        animator.Play(Utils.ItemHumanAnimatorDefaultHash, 1);

        inActiveTimer = 0f;
        onCollision = false;
    }

}