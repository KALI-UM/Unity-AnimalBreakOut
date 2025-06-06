using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldAnimalTokenKeySystem : ISaveLoad
{
    public GoldAnimalTokenKeySystem()
    {
        Load(SaveLoadSystem.Instance.CurrentSaveData.goldAnimalTokenKeySystemSave);
        SaveLoadSystem.Instance.RegisterOnSaveAction(this);
        
        SceneManager.sceneLoaded += OnChangeSceneHandler;
    }

    ~GoldAnimalTokenKeySystem()
    {
        SceneManager.sceneLoaded -= OnChangeSceneHandler;
    }

    private void OnChangeSceneHandler(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "MainTitleScene")
        {
            onGoldChanged?.Invoke(CurrentGolds);
            onBronzeTokenChanged?.Invoke(CurrentBronzeToken);
            onSliverTokenChanged?.Invoke(CurrentSliverToken);
            onGoldTokenChanged?.Invoke(CurrentGoldToken);
            onKeyChanged?.Invoke(CurrentKey);
        }
    }
    
    public DataSourceType SaveDataSouceType
    {
        get => DataSourceType.Local;
    }

    public long CurrentGolds
    {
        get;
        private set;
    }

    public const long minGold = 0;
    public const long maxGold = 99999999999;

    public static Action<long> onGoldChanged;

    public int CurrentBronzeToken
    {
        get;
        private set;
    }

    public int CurrentSliverToken
    {
        get;
        private set;
    }

    public int CurrentGoldToken
    {
        get;
        private set;
    }


    public const int minToken = 0;
    public const int maxToken = 9999;

    public static Action<int> onBronzeTokenChanged;
    public static Action<int> onSliverTokenChanged;
    public static Action<int> onGoldTokenChanged;

    public int CurrentKey
    {
        get;
        private set;
    }

    public const int minKey = 0;
    public const int maxKey = 9999;

    public static Action<int> onKeyChanged;

    public void SetInitialValue(long gold, int bronzeToken, int sliverToken, int goldToken, int key)
    {
        CurrentGolds = gold;
        onGoldChanged?.Invoke(CurrentGolds);

        CurrentBronzeToken = bronzeToken;
        onBronzeTokenChanged?.Invoke(CurrentBronzeToken);

        CurrentSliverToken = sliverToken;
        onSliverTokenChanged?.Invoke(CurrentSliverToken);

        CurrentGoldToken = goldToken;
        onGoldTokenChanged?.Invoke(CurrentGoldToken);

        CurrentKey = key;
        onKeyChanged?.Invoke(CurrentKey);
    }

    public void AddGold(long value)
    {
        CurrentGolds += value;
        CurrentGolds = Math.Clamp(CurrentGolds, minGold, maxGold);

        Debug.Log($"Add gold : {value}, CurrentGolds : {CurrentGolds}");
        onGoldChanged?.Invoke(CurrentGolds);
    }

    public bool PayGold(long value)
    {
        if (CurrentGolds < value)
        {
            Debug.Log($"Not enough gold! -> current gold : {CurrentGolds}, payment : {value} X");
            return false;
        }

        CurrentGolds -= value;
        Debug.Log($"pay gold success! -> current gold : {CurrentGolds}");
        onGoldChanged?.Invoke(CurrentGolds);

        return true;
    }

    public void AddBronzeToken(int value)
    {
        CurrentBronzeToken += value;
        CurrentBronzeToken = Math.Clamp(CurrentBronzeToken, minToken, maxToken);

        Debug.Log($"Add BronzeToken : {value}, CurrentBronzeToken : {CurrentBronzeToken}");
        onBronzeTokenChanged?.Invoke(CurrentBronzeToken);
    }

    public bool PayBronzeToken(int value)
    {
        if (CurrentBronzeToken < value)
        {
            Debug.Log($"Not enough bronze token! -> current bronze token : {CurrentBronzeToken}, payment : {value} X");
            return false;
        }

        CurrentBronzeToken -= value;
        Debug.Log($"pay bronze token success! -> current bronze token : {CurrentBronzeToken}");
        onBronzeTokenChanged?.Invoke(CurrentBronzeToken);

        return true;
    }

    public void AddSliverToken(int value)
    {
        CurrentSliverToken += value;
        CurrentSliverToken = Math.Clamp(CurrentSliverToken, minToken, maxToken);

        Debug.Log($"Add SliverToken : {value}, CurrentSliverToken : {CurrentSliverToken}");
        onSliverTokenChanged?.Invoke(CurrentSliverToken);
    }

    public bool PaySliverToken(int value)
    {
        if (CurrentSliverToken < value)
        {
            Debug.Log($"Not enough sliver token! -> current sliver token : {CurrentSliverToken}, payment : {value} X");
            return false;
        }

        CurrentSliverToken -= value;
        Debug.Log($"pay sliver token success! -> current sliver token : {CurrentSliverToken}");
        onSliverTokenChanged?.Invoke(CurrentSliverToken);

        return true;
    }

    public void AddGoldToken(int value)
    {
        CurrentGoldToken += value;
        CurrentGoldToken = Math.Clamp(CurrentGoldToken, minToken, maxToken);

        Debug.Log($"Add GoldToken : {value}, CurrentGoldToken : {CurrentGoldToken}");
        onGoldTokenChanged?.Invoke(CurrentGoldToken);
    }

    public bool PayGoldToken(int value)
    {
        if (CurrentGoldToken < value)
        {
            Debug.Log($"Not enough gold token! -> current gold token : {CurrentGoldToken}, payment : {value} X");
            return false;
        }

        CurrentGoldToken -= value;
        Debug.Log($"pay gold token success! -> current gold token : {CurrentGoldToken}");
        onGoldTokenChanged?.Invoke(CurrentGoldToken);

        return true;
    }

    public int GetCurrentToken(int grade)
    {
        if (grade == 1)
        {
            return CurrentBronzeToken;
        }
        else if (grade == 2)
        {
            return CurrentSliverToken;
        }
        else
        {
            return CurrentGoldToken;
        }
    }

    public void AddToken(int grade, int value)
    {
        if (grade == 1)
        {
            AddBronzeToken(value);
        }
        else if (grade == 2)
        {
            AddSliverToken(value);
        }
        else
        {
            AddGoldToken(value);
        }

    }

    public bool PayToken(int grade, int value)
    {
        if (grade == 1)
        {
            return PayBronzeToken(value);
        }
        else if (grade == 2)
        {
            return PaySliverToken(value);
        }
        else
        {
            return PayGoldToken(value);
        }
    }

    public void AddKey(int value)
    {
        CurrentKey += value;
        CurrentKey = Math.Clamp(CurrentKey, minKey, maxKey);

        Debug.Log($"Add Key : {value}, CurrentKey : {CurrentKey}");
        onKeyChanged?.Invoke(CurrentKey);
    }

    public bool PayKey(int value)
    {
        if (CurrentKey < value)
        {
            Debug.Log($"Not enough key! -> current key : {CurrentKey}, payment : {value} X");

            return false;
        }

        CurrentKey -= value;
        Debug.Log($"pay key success! -> current key : {CurrentKey}");
        onKeyChanged?.Invoke(CurrentKey);

        return true;
    }

    public void Save()
    {
        var saveData = SaveLoadSystem.Instance.CurrentSaveData.goldAnimalTokenKeySystemSave = new();
        saveData.currentGold = CurrentGolds;
        saveData.currentBronzeToken = CurrentBronzeToken;
        saveData.currentSilverToken = CurrentSliverToken;
        saveData.currentGoldToken = CurrentGoldToken;
        saveData.currentKey = CurrentKey;
    }

    public void Load()
    {
        SetInitialValue(1000, 0, 0, 0, 0);
    }

    public void Load(GoldAnimalTokenKeySystemSave saveData)
    {
        if (saveData == null)
        {
            Load();
            return;
        }

        SetInitialValue(saveData.currentGold, saveData.currentBronzeToken, saveData.currentSilverToken, saveData.currentGoldToken, saveData.currentKey);
    }
}