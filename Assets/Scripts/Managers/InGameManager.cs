using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour, IManager
{
    //public이어도 되나?
    protected GameManager_new GameManager
    {
        get;
        private set;
    }

    public virtual void SetGameManager(GameManager_new gameManager)
    {
        GameManager = gameManager;
    }

    public virtual void Initialize()
    {

    }

    public virtual void Clear()
    {

    }

}
