using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class StageManager : InGameManager
{

    [ReadOnly]
    public int currentStageDataIndex=0;

    [SerializeField]
    private List<StageData> stageDatas = new List<StageData>();

    public override void Initialize()
    {
        base.Initialize();
        GameManager.RoadMaker.onCurrentRoadWayEmpty += OnSetRoadMode;
    }

    public void SetInitialRoadMode()
    {
        var currStageData = stageDatas[currentStageDataIndex];
        GameManager.RoadMaker.SetRoadMakeMode(currStageData.roadMode, currStageData.roadWayCount);
        GameManager.RoadMaker.SetMapObjectMakeMode(currStageData.itemSetMode);
    }


    //테스트용으로 무한 반복하게 할것입니다.
    private void OnSetRoadMode()
    {
        currentStageDataIndex++;
        currentStageDataIndex%= stageDatas.Count;  

        var currStageData = stageDatas[currentStageDataIndex];

        if(currStageData.isBossStage)
        {
            OnBossStageStart();
        }

        GameManager.RoadMaker.SetRoadMakeMode(currStageData.roadMode, currStageData.roadWayCount);
        GameManager.RoadMaker.SetMapObjectMakeMode(currStageData.itemSetMode);
    }

    [ContextMenu("Boss Stage Exit")]
    private void OnCurrentStageClear()
    {
        OnSetRoadMode();
    }

    private void OnBossStageStart()
    {

    }
}