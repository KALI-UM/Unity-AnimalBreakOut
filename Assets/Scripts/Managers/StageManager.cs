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


    //�׽�Ʈ������ ���� �ݺ��ϰ� �Ұ��Դϴ�.
    private void OnSetRoadMode()
    {
        currentStageDataIndex++;
        currentStageDataIndex%= stageDatas.Count;  

        var currStageData = stageDatas[currentStageDataIndex];
        GameManager.RoadMaker.SetRoadMakeMode(currStageData.roadMode, currStageData.roadWayCount);
        GameManager.RoadMaker.SetMapObjectMakeMode(currStageData.itemSetMode);
    }

    [ContextMenu("Boss Stage Exit")]
    private void OnCurrentStageClear()
    {
        OnSetRoadMode();
    }
}