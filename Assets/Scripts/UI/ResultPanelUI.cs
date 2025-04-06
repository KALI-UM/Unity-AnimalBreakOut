using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ResultPanelUI : MonoBehaviour
{
    public GameObject GameResultPanel;
    public Button ReStartButton;
    public Button GoMainButton;

    //private GameManager gameManager;
    //UI�Ŵ����� ���� ���ӸŴ����� �����ϴ� ������� �ٲټ���
    [SerializeField]
    private GameManager_new gameManager;

    void Start()
    {
        ReStartButton.onClick.RemoveAllListeners();
        ReStartButton.onClick.AddListener(OnReStartButtonClicked);

        GoMainButton.onClick.RemoveAllListeners();
        GoMainButton.onClick.AddListener(OnGoMainButtonClicked);
    }

    private void OnReStartButtonClicked()
    {
        gameManager.SetGameState(GameManager_new.GameState.GameReStart);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameResultPanel.SetActive(false);

        Time.timeScale = 1;
    }
    private void OnGoMainButtonClicked()
    {
        //GameObject gameManagerObject = GameObject.Find("GmManager");
        //GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
        //gameManager.LoadScene("MainTitleScene");
        
        SceneManager.LoadScene("MainTitleScene");
        GameResultPanel.SetActive(false);
    }
}
