using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MapObjectManager : InGameManager
{
    [SerializeField] private GameObject trapBombPrefab;
    [SerializeField] private GameObject trapHolePrefab;
    
    [SerializeField] private List<GameObject> itemRewardCoinPrefabs;
    
    [SerializeField] private List<GameObject> itemHumanPrefabs;
    
    [SerializeField] private GameObject itemPenaltyCoinPrefab;

    private ObjectPool<GameObject> trapBombPool;
    private ObjectPool<GameObject> trapHolePool;
    
    private List<ObjectPool<GameObject>> itemRewardCoinPoolList = new();
    private List<ObjectPool<GameObject>> itemHumanPoolList = new();
    
    private ObjectPool<GameObject> itemPenaltyCoinPool;

    private const float maxHillHeight = 1f;

    private List<float> rewardItemSpawnChances = new();
    private float bronzeCoinSpawnChance = 0.6f;
    private float sliverCoinSpawnChance = 0.3f;
    private float goldCoinSpawnChance = 0.1f;

    private List<float> humanSpawnChances = new();
    private float juniorResearcherSpawnChance = 0.6f;
    private float researcherSpawnChance = 0.3f;
    private float seniorResearcherSpawnChance = 0.1f;

    private List<float> penaltyCoinSpawnChances = new();
    private float ghostCoinSpawnChance = 0.5f;
    private float poisonCoinSpawnChance = 0.2f;
    private float skullCoinSpawnChance = 0.15f;
    private float fireCoinSpawnChance = 0.1f;
    private float blackHoleCoinSpawnChance = 0.05f;

    private Dictionary<int, MapObjectsBlueprint> generateMapObjectInformationDictionary = new();
    private Dictionary<int, List<RewardItemBlueprint>> generateRewardItemInformationDictionary = new();

    [SerializeField]
    private GameObject roadTransformRoot;

    public int MinMapObjectId { get; private set; } = 0;
    public int MinRewardItemId { get; private set; } = 0;
    public int MaxMapObjectId { get; private set; } = 0;
    public int MaxRewardItemId { get; private set; } = 0;

    private readonly Queue<int> nextMapObjectsPrefabIdQueue = new();
    private readonly List<int> mapObjectsPrefabIds = new();
    public const int maxPrefabIdQueueSize = 9; // Original value is 12

    [SerializeField] private AnimationCurve hillCurve;
    
    private void Awake()
    {
        SetMaxMapObjectId(DataTableManager.mapObjectsDataTable.maxId);
        SetMinMapObjectId(DataTableManager.mapObjectsDataTable.minId);
        SetMaxRewardItemId(DataTableManager.rewardItemsDataTable.maxId);
        SetMinRewardItemId(DataTableManager.rewardItemsDataTable.minId);
    }

    public override void Initialize()
    {
        base.Initialize();

        InitializeMapObjectObjectPools();
    }

    private void Start()
    {
        GenerateMapObjectInformation(20, 3);
        GenerateRewardItemInformation();
    }

    private void InitializeMapObjectObjectPools()
    {
        rewardItemSpawnChances.Add(bronzeCoinSpawnChance);
        rewardItemSpawnChances.Add(sliverCoinSpawnChance);
        rewardItemSpawnChances.Add(goldCoinSpawnChance);

        humanSpawnChances.Add(juniorResearcherSpawnChance);
        humanSpawnChances.Add(researcherSpawnChance);
        humanSpawnChances.Add(seniorResearcherSpawnChance);

        penaltyCoinSpawnChances.Add(ghostCoinSpawnChance);
        penaltyCoinSpawnChances.Add(poisonCoinSpawnChance);
        penaltyCoinSpawnChances.Add(skullCoinSpawnChance);
        penaltyCoinSpawnChances.Add(fireCoinSpawnChance);
        penaltyCoinSpawnChances.Add(blackHoleCoinSpawnChance);

        // wallPool = GameManager.ObjectPoolManager.CreateObjectPool(wallPrefab,
        //     () => Instantiate(wallPrefab, roadTransformRoot.transform),
        //     obj => { obj.SetActive(true); },
        //     obj => { obj.SetActive(false); });

        trapBombPool = GameManager.ObjectPoolManager.CreateObjectPool(trapBombPrefab,
            () => Instantiate(trapBombPrefab),
            obj => { obj.SetActive(true); },
            obj => { obj.SetActive(false); });

        trapHolePool = GameManager.ObjectPoolManager.CreateObjectPool(trapHolePrefab,
            () => Instantiate(trapHolePrefab),
            obj => { obj.SetActive(true); },
            obj => { obj.SetActive(false); });

        for (int i = 0; i < itemRewardCoinPrefabs.Count; ++i)
        {
            int index = i;
            
            ObjectPool<GameObject> itemRewardCoinPool = GameManager.ObjectPoolManager.CreateObjectPool(itemRewardCoinPrefabs[index],
                () => Instantiate(itemRewardCoinPrefabs[index]),
                obj => { obj.SetActive(true); },
                obj => { obj.SetActive(false); });
            
            itemRewardCoinPoolList.Add(itemRewardCoinPool);       
        }

        for (int i = 0; i < itemHumanPrefabs.Count; ++i)
        {
            int index = i;
            
            ObjectPool<GameObject> itemHumanPool = GameManager.ObjectPoolManager.CreateObjectPool(itemHumanPrefabs[index],
                () => Instantiate(itemHumanPrefabs[index]),
                obj => { obj.SetActive(true); },
                obj => { obj.SetActive(false); });
            
            itemHumanPoolList.Add(itemHumanPool);
        }            

        itemPenaltyCoinPool = GameManager.ObjectPoolManager.CreateObjectPool(itemPenaltyCoinPrefab,
            () => Instantiate(itemPenaltyCoinPrefab),
            obj => { obj.SetActive(true); },
            obj => { obj.SetActive(false); });
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

    public struct MapObjectsBlueprint
    {
        public ObjectType[,] objectsTypes;
        public Func<Vector3, CollidableMapObject>[,] objectsConstructors;
    }

    private void GenerateMapObjectInformation(int rows, int cols)
    {
        foreach (var entry in DataTableManager.mapObjectsDataTable.GetTableEntries())
        {
            MapObjectsBlueprint mapObjectsBlueprint = new MapObjectsBlueprint();
            mapObjectsBlueprint.objectsTypes = new ObjectType[rows, cols];

            int prefabID = entry.Key;
            List<MapObjectData> dataList = entry.Value;


            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; j++)
                {
                    mapObjectsBlueprint.objectsTypes[i, j] = ObjectType.None;
                }
            }

            //mapObjectsBlueprint.wallIndex = rows - 1;
            mapObjectsBlueprint.objectsConstructors = new Func<Vector3, CollidableMapObject>[rows, cols];

            //SetCreateWallAction(mapObjectsBlueprint.objectsTypes, mapObjectsBlueprint.objectsConstructors);
            for (int i = 0; i < dataList.Count; ++i)
            {
                var data = dataList[i];

                switch (data.Obj_Type)
                {
                    case MapObjectCSVType.Bomb:
                        {
                            SetCreateBombAction(mapObjectsBlueprint.objectsTypes, mapObjectsBlueprint.objectsConstructors,
                                data.Coor2, data.Coor1);
                        }
                        break;
                    case MapObjectCSVType.Hole:
                        {
                            SetCreateHoleAction(mapObjectsBlueprint.objectsTypes, mapObjectsBlueprint.objectsConstructors,
                            data.Coor2, data.Coor1);
                        }
                        break;
                    case MapObjectCSVType.Human:
                        {
                            SetCreateRandomHumanAction(mapObjectsBlueprint.objectsTypes, mapObjectsBlueprint.objectsConstructors,
                                data.Coor2, data.Coor1);
                        }
                        break;
                    case MapObjectCSVType.PenaltyCoin:
                        {
                            SetCreateRandomPenaltyCoinAction(mapObjectsBlueprint.objectsTypes, mapObjectsBlueprint.objectsConstructors,
                                data.Coor2, data.Coor1);
                        }
                        break;
                }
            }

            generateMapObjectInformationDictionary.Add(prefabID, mapObjectsBlueprint);
        }
    }

    public struct RewardItemBlueprint
    {
        public (int, int)[] startEndCoordinate; // Size : 2
        public int itemGroupCount; // Reward Item Count In Same Group

        public Func<Vector3, Vector3, int, CollidableMapObject[]> rewardItemConstructors;
    }

    private void GenerateRewardItemInformation()
    {
        foreach (var entry in DataTableManager.rewardItemsDataTable.GetTableEntries())
        {
            int prefabID = entry.Key;
            var dataList = entry.Value;

            for (int i = 0; i < dataList.Count; ++i)
            {
                RewardItemBlueprint rewardItemBlueprint = new RewardItemBlueprint();
                rewardItemBlueprint.startEndCoordinate = new (int, int)[2];

                var data = dataList[i];
                rewardItemBlueprint.startEndCoordinate[0] = (data.Start_Coor2, data.Start_Coor1);
                rewardItemBlueprint.startEndCoordinate[1] = (data.End_Coor2, data.End_Coor1);
                rewardItemBlueprint.itemGroupCount = data.Count;
                if (data.Pattern == RewardCoinPatternCSVType.Straight)
                {
                    rewardItemBlueprint.rewardItemConstructors = CreateRandomRewardCoin;
                }
                else if (data.Pattern == RewardCoinPatternCSVType.Hill)
                {
                    rewardItemBlueprint.rewardItemConstructors = CreateRandomRewardCoinWithHill;
                }

                if (i == 0)
                {
                    List<RewardItemBlueprint> rewardItemBlueprints = new();
                    rewardItemBlueprints.Add(rewardItemBlueprint);
                    generateRewardItemInformationDictionary.Add(prefabID, rewardItemBlueprints);
                }
                else
                {
                    generateRewardItemInformationDictionary[prefabID].Add(rewardItemBlueprint);
                }
            }
        }
    }

    public MapObjectsBlueprint GetMapObjectsBlueprint(int id)
    {
        if (generateMapObjectInformationDictionary.TryGetValue(id, out MapObjectsBlueprint blueprint))
        {
            return blueprint;
        }
        else
        {
            throw new KeyNotFoundException($" 맵 오브젝트 키 '{id}' 를 찾을 수 없습니다.");
        }
    }

    public List<RewardItemBlueprint> GetRewardItemBlueprint(int id)
    {
        if (generateRewardItemInformationDictionary.TryGetValue(id, out List<RewardItemBlueprint> blueprints))
        {
            return blueprints;
        }
        else
        {
            throw new KeyNotFoundException($" 리워드 아이템 '{id}' 를 찾을 수 없습니다.");
        }
    }
    
    private void SetCreateBombAction(ObjectType[,] objectTypes, Func<Vector3, CollidableMapObject>[,] createMapObjectActionArray, int row, int col)
    {
        if (objectTypes[row, col] != ObjectType.None)
        {
            Debug.Assert(false, $"Object {objectTypes[row, col].ToString()} is Already Exist in : [{row}, {col}]");

            return;
        }

        objectTypes[row, col] = ObjectType.TrapBomb;
        createMapObjectActionArray[row, col] = CreateBomb;
    }

    private CollidableMapObject CreateBomb(Vector3 position)
    {
        var bomb = trapBombPool.Get();
        bomb.SetActive(true);
        bomb.transform.SetPositionAndRotation(position, Quaternion.identity);
        bomb.TryGetComponent(out TrapBomb trapComponent);
        trapComponent.Initialize();
        trapComponent.SetPool(trapBombPool);
        return trapComponent;
    }

    private void SetCreateHoleAction(ObjectType[,] objectTypes, Func<Vector3, CollidableMapObject>[,] createMapObjectActionArray, int row, int col)
    {
        if (objectTypes[row, col] != ObjectType.None)
        {
            Debug.Assert(false, $"Object {objectTypes[row, col].ToString()} is Already Exist in : [{row}, {col}]");

            return;
        }

        objectTypes[row, col] = ObjectType.TrapHole;
        createMapObjectActionArray[row, col] = CreateHole;
    }

    private CollidableMapObject CreateHole(Vector3 position)
    {
        var hole = trapHolePool.Get();
        hole.SetActive(true);
        hole.transform.SetPositionAndRotation(position, Quaternion.identity);
        hole.TryGetComponent(out TrapHole trapComponent);
        trapComponent.Initialize();
        trapComponent.SetPool(trapHolePool);
        return trapComponent;
    }

    private CollidableMapObject[] CreateRandomRewardCoin(Vector3 startPosition, Vector3 endPosition, int itemCount)
    {
        var array = new CollidableMapObject[itemCount];
        
        for (int i = 0; i < itemCount; ++i)
        {
            var rewardItemPrefabIndex = Utils.GetIndexRandomChanceHitInList(rewardItemSpawnChances);
            
            var rewardCoin = itemRewardCoinPoolList[rewardItemPrefabIndex].Get();
            rewardCoin.SetActive(true);
            rewardCoin.transform.SetPositionAndRotation(Vector3.Lerp(startPosition, endPosition, (float)i / (itemCount - 1)), Quaternion.identity);
            rewardCoin.TryGetComponent(out ItemRewardCoin itemRewardCoinComponent);
            itemRewardCoinComponent.Initialize();
            itemRewardCoinComponent.SetPool(itemRewardCoinPoolList[rewardItemPrefabIndex]);
            array[i] = itemRewardCoinComponent;
        }
        
        return array;
    }

    private CollidableMapObject[] CreateRandomRewardCoinWithHill(Vector3 startPosition, Vector3 endPosition, int itemCount)
    {
        float maxHeight = maxHillHeight;

        var array = new CollidableMapObject[itemCount];
        
        for (int i = 0; i < itemCount; ++i)
        {
            Vector3 spawnPosition = Vector3.Lerp(startPosition, endPosition, (float)i / (itemCount - 1));

            float t = (float)i / (itemCount - 1);
            float curveValue = hillCurve.Evaluate(t);
            spawnPosition.y += curveValue * maxHeight;

            var rewardItemPrefabIndex = Utils.GetIndexRandomChanceHitInList(rewardItemSpawnChances);
        
            var rewardCoin = itemRewardCoinPoolList[rewardItemPrefabIndex].Get();
            rewardCoin.SetActive(true);
            rewardCoin.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
            rewardCoin.TryGetComponent(out ItemRewardCoin itemRewardCoinComponent);
            itemRewardCoinComponent.Initialize();
            itemRewardCoinComponent.SetPool(itemRewardCoinPoolList[rewardItemPrefabIndex]);
            array[i] = itemRewardCoinComponent;
        }
        
        return array;
    }

    private void SetCreateRandomHumanAction(ObjectType[,] objectTypes, Func<Vector3, CollidableMapObject>[,] createMapObjectActionArray, int row, int col)
    {
        if (objectTypes[row, col] != ObjectType.None)
        {
            Debug.Assert(false, $"Object {objectTypes[row, col].ToString()} is Already Exist in : [{row}, {col}]");

            return;
        }

        objectTypes[row, col] = ObjectType.Item;
        createMapObjectActionArray[row, col] = CreateRandomHuman;
    }

    private CollidableMapObject CreateRandomHuman(Vector3 position)
    {
        var humanPrefabIndex = Utils.GetIndexRandomChanceHitInList(humanSpawnChances);
        var human = itemHumanPoolList[humanPrefabIndex].Get();
        human.SetActive(true);
        human.transform.SetPositionAndRotation(position, Quaternion.identity);
        human.TryGetComponent(out ItemHuman itemHumanComponent);
        itemHumanComponent.Initialize();
        itemHumanComponent.SetPool(itemHumanPoolList[humanPrefabIndex]);
        return itemHumanComponent;
    }

    private void SetCreateRandomPenaltyCoinAction(ObjectType[,] objectTypes, Func<Vector3, CollidableMapObject>[,] createMapObjectActionArray, int row, int col)
    {
        if (objectTypes[row, col] != ObjectType.None)
        {
            Debug.Assert(false, $"Object {objectTypes[row, col].ToString()} is Already Exist in : [{row}, {col}]");

            return;
        }

        objectTypes[row, col] = ObjectType.Item;
        createMapObjectActionArray[row, col] = CreateRandomPenaltyCoin;
    }

    private CollidableMapObject CreateRandomPenaltyCoin(Vector3 position)
    {
        var penaltyCoin = itemPenaltyCoinPool.Get();
        penaltyCoin.SetActive(true);
        penaltyCoin.transform.SetPositionAndRotation(position, Quaternion.identity);
        penaltyCoin.TryGetComponent(out ItemPenaltyCoin itemPenaltyCoinComponent);
        itemPenaltyCoinComponent.Initialize((PenaltyCoinItemType)Utils.GetEnumIndexByChance(penaltyCoinSpawnChances));
        itemPenaltyCoinComponent.SetPool(itemPenaltyCoinPool);
        return itemPenaltyCoinComponent;
    }

    public int GetNextRandomMapObjectsPrefabId()
    {
        if ((MinMapObjectId != MinRewardItemId) || (MaxMapObjectId != MaxRewardItemId))
        {
            Debug.Assert(false, "MapObjectId is different from RewardItemId");

            return -1;
        }

        if (nextMapObjectsPrefabIdQueue.Count == 0)
        {
            mapObjectsPrefabIds.Clear();
            for (int i = MinMapObjectId; i <= MaxMapObjectId; ++i)
            {
                mapObjectsPrefabIds.Add(i);
            }

            if (mapObjectsPrefabIds.Count < maxPrefabIdQueueSize)
            {
                Debug.Assert(false, "PrefabId Count is smaller than maxPrefabIdQueueSize");

                return -1;
            }

            while (mapObjectsPrefabIds.Count > maxPrefabIdQueueSize)
            {
                int randomIndex = UnityEngine.Random.Range(0, mapObjectsPrefabIds.Count);
                mapObjectsPrefabIds.RemoveAt(randomIndex);
            }

            while (mapObjectsPrefabIds.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, mapObjectsPrefabIds.Count);
                nextMapObjectsPrefabIdQueue.Enqueue(mapObjectsPrefabIds[randomIndex]);
                mapObjectsPrefabIds.RemoveAt(randomIndex);
            }
        }

        return nextMapObjectsPrefabIdQueue.Dequeue();
    }
}