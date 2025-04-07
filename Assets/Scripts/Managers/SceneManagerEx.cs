using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx : Singleton<SceneManagerEx>
{
    public Action onLoadComplete;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("Run") || scene.name.Contains("InGame"))
        {
            StartCoroutine(LoadInGameResources());
        }
    }

    private IEnumerator LoadInGameResources()
    {
        // �ε�â Additive�� ����
        yield return SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);

        // �ε� ����
        bool isLoadingComplete = false;
        LoadManager.Instance.LoadInGameResource(() =>
        {
            isLoadingComplete = true;
        });

        // �ε� �Ϸ� ���
        while (!isLoadingComplete)
        {
            yield return null;
        }

        Debug.Log("Game Resources Loaded!");

        // �ε� �Ϸ� �� �ε�â ����
        yield return SceneManager.UnloadSceneAsync("LoadingScene");

        // �ε� �Ϸ�
        onLoadComplete?.Invoke();
    }
}
