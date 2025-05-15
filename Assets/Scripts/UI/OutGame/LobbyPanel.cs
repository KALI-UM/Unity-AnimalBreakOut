using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyPanel : MonoBehaviour
{
    [SerializeField] private Button gameStartButton;

    private readonly int staminaRequiredToStartGame = 1;
    private bool isStaminaEnoughToStartGame;

    [SerializeField] private Image animalImage;
    
    public static event Action<int> onGameStartButtonClicked;

    private void Awake()
    {
        GameDataManager.onSetStartAnimalIDInGameDataManager += OnSetStartAnimalIDInGameDataManagerHandler;
        StaminaSystem.onStaminaChanged += OnStaminaChangedInGameDataManagerHandler;
    }

    private void OnDestroy()
    {
        GameDataManager.onSetStartAnimalIDInGameDataManager -= OnSetStartAnimalIDInGameDataManagerHandler;
        StaminaSystem.onStaminaChanged -= OnStaminaChangedInGameDataManagerHandler;
        
        gameStartButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        //var gameDataManagerStartAnimalId = GameDataManager.Instance.StartAnimalID;
        //GameDataManager.Instance.AnimalUserDataList가 해당 역할을 합니다.

        //if (GameDataManager.Instance.AnimalUserDataList.CurrentAnimalID == 0 || !IsContainsAnimalIdInTable(GameDataManager.Instance.AnimalUserDataList.CurrentAnimalID))
        if (GameDataManager.Instance.AnimalUserDataList.CurrentAnimalID == 0 || !IsContainsAnimalIdInTable(GameDataManager.Instance.AnimalUserDataList.CurrentAnimalID) || !(GameDataManager.Instance.StaminaSystem.CurrentStamina>= staminaRequiredToStartGame))
        {
            gameStartButton.interactable = false;
        }
        else
        {
            gameStartButton.interactable = true;
        }

        gameStartButton.onClick.AddListener(() =>
        {
            onGameStartButtonClicked?.Invoke(staminaRequiredToStartGame);
            gameStartButton.interactable = false;
            SceneManager.LoadScene("Run");
        }
            );
    }
    
    private void OnSetStartAnimalIDInGameDataManagerHandler(int animalID, int currentStamina, AnimalUserData animalUserData)
    {
        if (animalID == 0 || !IsContainsAnimalIdInTable(animalID))
        {
            gameStartButton.interactable = false;

            Debug.Assert(false, $"Invalid animal ID{animalID}");
            
            return;
        }
        
        animalImage.sprite = animalUserData.AnimalStatData.iconImage;

        if (currentStamina < staminaRequiredToStartGame)
        {
            gameStartButton.interactable = false;
            
            return;
        }

        gameStartButton.interactable = true;
    }

    private bool IsContainsAnimalIdInTable(int animalID)
    {
        var animalIdList = DataTableManager.animalDataTable.GetAnimalIDs();

        return animalIdList.Contains(animalID);
    }

    private void OnStaminaChangedInGameDataManagerHandler(int currentStamina, int maxstaminaCanFilled)
    {
        if (currentStamina >= staminaRequiredToStartGame)
        {
            gameStartButton.interactable = true;
            isStaminaEnoughToStartGame = true;
        }
        else
        {
            gameStartButton.interactable = false;
            isStaminaEnoughToStartGame = false;
        }
    }
}