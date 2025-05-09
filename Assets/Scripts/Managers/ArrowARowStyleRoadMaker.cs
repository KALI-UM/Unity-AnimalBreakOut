//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using static MapObjectManager;
//using UnityEngine.Pool;

////���
//public class ArrowARowStyleRoadMaker : RoadManager
//{
//    public GameObject[] roadSegmentPrefabs;
//    private List<ObjectPool<GameObject>> roadSegmentPools = new();

//    private PlayerMove player;
//    [SerializeField]
//    private MapObjectManager mapObjectManager;

//    public RoadChunk roadChunkPrefab;
//    [ReadOnly]
//    public RoadChunk currentRoadChunk;

//    private List<RoadChunk> activeRoadChunks = new();


//    private void Start()
//    {
//        enabled = false;
//        var relayRunManager = GameObject.FindObjectOfType<GameManager>();

//        relayRunManager.onPlayerSpawned += (playerStatus) => GetPlayer(playerStatus.gameObject.GetComponent<PlayerMove>());
//        relayRunManager.onPlayerDied += (playerStatus) => enabled = false;

//        for (int i = 0; i < roadSegmentPrefabs.Count(); i++)
//        {
//            var index = i;
//            roadSegmentPools.Add(ObjectPoolManager.Instance.CreateObjectPool(roadSegmentPrefabs[index], () => InstantiateRoadSegment(roadSegmentPrefabs[index]), OnGetRoadSegment, OnReleaseMapUnit));
//        }

//        CreateRoadChunk(0, Vector3.zero);
//    }

//    public void GetPlayer(PlayerMove player)
//    {
//        this.player = player;
//        enabled = true;
//    }

//    public RoadSegment GetRoadSegment(WayType type)
//    {
//        return roadSegmentPools[(int)type].Get().GetComponent<RoadSegment>();
//    }

//    public void ReleaseRoadSegment(RoadSegment roadSegment)
//    {
//        roadSegmentPools[(int)roadSegment.directionType].Release(roadSegment.gameObject);
//    }

//    private GameObject InstantiateRoadSegment(GameObject roadSegment)
//    {
//        var unit = Instantiate(roadSegment, gameObject.transform);
//        return unit;
//    }
//    private void OnGetRoadSegment(GameObject roadSegment)
//    {
//        roadSegment.GetComponent<RoadSegment>().Reset();
//        roadSegment.transform.rotation = Quaternion.identity;
//        roadSegment.gameObject.SetActive(true);
//    }
//    private void OnReleaseMapUnit(GameObject roadSegment)
//    {
//        roadSegment.gameObject.SetActive(false);
//    }

//    public RoadChunk CreateRoadChunk(int startIndex, Vector3 startPosition, MapObjectsBlueprint mapBlueprint)
//    {
//        var roadChunk = Instantiate(roadChunkPrefab, startPosition, Quaternion.identity, transform);
//        roadChunk.SetRoadManager(this);
//        bool isLeft = (UnityEngine.Random.Range(0, 100) % 2 == 0);
//        var roadChunkInoformation = isLeft ? new RoadChunkInformaion(startPosition, left: mapBlueprint.wallIndex / 10) : new RoadChunkInformaion(startPosition, right: mapBlueprint.wallIndex / 10);
//        roadChunk.CreateRoadChunk(startIndex, roadChunkInoformation);
//        return roadChunk;
//    }

//    public RoadChunk CreateRoadChunk(int startIndex, Vector3 startPosition)
//    {
//        var roadChunk = Instantiate(roadChunkPrefab, startPosition, Quaternion.identity, transform);
//        roadChunk.SetRoadManager(this);
//        var roadChunkInoformation = new RoadChunkInformaion(startPosition);
//        roadChunk.CreateRoadChunk(startIndex, roadChunkInoformation);
//        return roadChunk;
//    }


//    private void CreateMapObjects(RoadChunk roadChunk, MapObjectsBlueprint mapBlueprint)
//    {
//        int rows = mapBlueprint.objectsConstructors.GetLength(0);
//        int cols = mapBlueprint.objectsConstructors.GetLength(1);

//        for (int r = 0; r < rows; r++)
//        {
//            for (int c = 0; c < cols; c++)
//            {
//                mapBlueprint.objectsConstructors[r, c]?.Invoke(roadChunk.GetRoadSegmentTilePosition(r, c));
//            }
//        }

//    }

//    //public RoadChunk CreateNextRoadChunk(int startIndex, Vector3 startPosition, out MapObjectsBlueprint mapObjectsBlueprint)
//    //{
//    //    mapObjectsBlueprint = mapObjectManager.GenerateMapObjectInformation(10 * 20, 3);
//    //    var roadChunk = CreateRoadChunk(startIndex, startPosition, mapObjectsBlueprint);
//    //    CreateMapObjects(roadChunk, mapObjectsBlueprint);
//    //    activeRoadChunks.Add(roadChunk);
//    //    return roadChunk;
//    //}

//    public RoadChunk CreateRoadLeftChunk(int startIndex, Vector3 startPosition, MapObjectsBlueprint mapObjectsBlueprint)
//    {
//        var roadChunk = CreateRoadChunk(startIndex, startPosition, mapObjectsBlueprint);
//        roadChunk.transform.Rotate(new Vector3(0, -90f, 0));
//        activeRoadChunks.Add(roadChunk);
//        return roadChunk;
//    }

//    public RoadChunk CreateRoadRightChunk(int startIndex, Vector3 startPosition, MapObjectsBlueprint mapObjectsBlueprint)
//    {
//        var roadChunk = CreateRoadChunk(startIndex, startPosition, mapObjectsBlueprint);
//        roadChunk.transform.Rotate(new Vector3(0, 90f, 0));
//        activeRoadChunks.Add(roadChunk);
//        return roadChunk;
//    }

//    public void ReleasePassedRoadChunks()
//    {
//        var releaseList = new List<RoadChunk>();
//        foreach (var roadChunk in activeRoadChunks)
//        {
//            if (roadChunk.roadSegments[roadChunk.ChunkSize.y - 1][1].transform.position.z + 50f < player.transform.position.z)
//            {
//                releaseList.Add(roadChunk);
//            }
//        }

//        foreach (var roadChunk in releaseList)
//        {
//            activeRoadChunks.Remove(roadChunk);
//            roadChunk.ReleaseRoadSegments();
//            Destroy(roadChunk);
//        }
//    }
//}
