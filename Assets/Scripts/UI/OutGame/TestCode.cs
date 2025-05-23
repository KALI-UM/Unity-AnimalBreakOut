using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestCode : MonoBehaviour
{
    public GameObject parent;
    public GameObject instantiateObj;

    public GameObject parent2;
    public GameObject instantiateObj2;

    public List<GameObject> objList1 = new List<GameObject>();
    public List<GameObject> objList2 = new List<GameObject>();
    
    public static event Action OnAddAnimalInfoPanel; 
    public static event Action OnRemoveAnimalInfoPanel;

    private void Start()
    {
        // for (int i = 0; i < 4; i++)
        // {
        //     objList1.Add(Instantiate(instantiateObj, parent.transform));
        // }
        //
        // for (int i = 0; i < 4; i++)
        // {
        //     objList2.Add(Instantiate(instantiateObj2, parent2.transform));
        // }
        //
        // OnAddAnimalInfoPanel?.Invoke();
    }

    public void InstantiatePrefab1()
    {
        var obj = Instantiate(instantiateObj, parent.transform);
        objList1.Add(obj);
        
        OnAddAnimalInfoPanel?.Invoke();
    }

    public void InstantiatePrefab2()
    {
        var obj = Instantiate(instantiateObj2, parent2.transform);
        objList2.Add(obj);
        
        OnAddAnimalInfoPanel?.Invoke();
    }

    public void DestroyRandomObjFromList1()
    {
        if (objList1.Count == 0) return;
        var randIndex = Random.Range(0, objList1.Count);
        var obj = objList1[randIndex];
        objList1.RemoveAt(randIndex);
        Destroy(obj);
        
        OnRemoveAnimalInfoPanel?.Invoke();
    }

    public void DestroyRandomObjFromList2()
    {
        if (objList2.Count == 0) return;
        var randIndex = Random.Range(0, objList2.Count);
        var obj = objList2[randIndex];
        objList2.RemoveAt(randIndex);
        Destroy(obj);
        
        OnRemoveAnimalInfoPanel?.Invoke();
    }
}