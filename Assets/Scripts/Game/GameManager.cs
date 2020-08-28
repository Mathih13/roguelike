using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameObject loadingScreen;

    [SerializeField]
    private Slider progressBar;

    [SerializeField]
    private TextMeshProUGUI loadingText;

    float totalSceneProgress;
    float totalBuildProgress;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    private void Awake()
    {
        SceneManager.LoadSceneAsync((int)SceneIndexes.MainMenu, LoadSceneMode.Additive);
    }

    public void LoadGame()
    {
        loadingScreen.SetActive(true);

        SceneManager.UnloadSceneAsync((int)SceneIndexes.MainMenu);
        SceneManager.LoadSceneAsync((int)SceneIndexes.GameScene, LoadSceneMode.Additive);

        StartCoroutine(GetSceneLoadProgress());
        StartCoroutine(TotalLoadProgress());
    }


    private IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;

                foreach (AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;

                loadingText.text = $"Loading scenes";

                yield return null;
            }
        }

    }

    private IEnumerator TotalLoadProgress()
    {
        float totalProgress = 0;

        while (DataManager.Instance == null || !DataManager.Instance.Loaded)
        {
            if (DataManager.Instance == null)
            {
                totalBuildProgress = 0;
            } else
            {
                totalBuildProgress = Mathf.Round(DataManager.Instance.Progress * 100f);
                loadingText.text = $"Building board";

            }

            totalProgress = Mathf.Round((totalSceneProgress + totalBuildProgress) / 2f);
            progressBar.value = Mathf.RoundToInt(totalProgress);
            yield return null;

        }

        loadingScreen.transform.DOScale(0, 3f).OnComplete(() => loadingScreen.SetActive(false));

    }

}

public enum SceneIndexes
{
    SceneManager = 0,
    MainMenu,
    GameScene
}
