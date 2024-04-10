using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressBar;
    public float fillSpeed = 0.1f;

    private void Start()
    {
        progressBar.value = 0f;
    }

    private void Update()
    {
        if (progressBar.value < 5f)
        {
            progressBar.value += fillSpeed * Time.deltaTime;
        }
    }
    public bool IsProgressBarFull()
    {
        return progressBar.value >= 1f;
    }

    public void LoadScene(int levelIndex)
    {
        StartCoroutine(LoadSceneAsynchronously(levelIndex));
    }
    IEnumerator LoadSceneAsynchronously(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            progressBar.value = operation.progress;
            yield return null;
        }
    }
}
